using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace KT.Common.Classes.Application
{
	/// <summary>
	/// Describes one process at serialization/deserialization.
	/// </summary>
	internal class SerializeProcess
	{
		#region Properties.

		/// <summary>
		/// Stream to work on.
		/// </summary>
		public Stream Stream;

		/// <summary>
		/// Possible common header stream to work on.
		/// </summary>
		public Stream CommonHeaderStream;

		/// <summary>
		/// Object to process.
		/// </summary>
		public object ProcessedObject;

		/// <summary>
		/// Type of the object to process.
		/// <para>Type of the processed object at serialization, type to deserialize to at deserialization.</para>
		/// </summary>
		public Type ObjectType;

		/// <summary>
		/// Common storage of all gathered types meta-data.
		/// </summary>
		public Dictionary<Type, SerializeTypeInfoEntry> TypesData = new Dictionary<Type, SerializeTypeInfoEntry>();

		#endregion


		#region Auxiliary variables.

		#region Process types variables.

		/// <summary>
		/// Type to process.
		/// </summary>
		private List<Type> typesToProcess;

		/// <summary>
		/// Collection of temporary processed types class level attributes.
		/// </summary>
		private Dictionary<Type, KTSerializeAttribute> typesAttributes;

		/// <summary>
		/// Types that were viewed during types adding/not adding to types to process.
		/// </summary>
		private Dictionary<Type, object> decidedTypes;

		/// <summary>
		/// Base class types that were viewed during types parsing.
		/// </summary>
		private Dictionary<Type, object> viewedTypes;

		/// <summary>
		/// Collection of all processed types data of any inheritance.
		/// </summary>
		private Dictionary<Type, SerializeTypeInfoEntry> allTypesData;

		/// <summary>
		/// Collected types inheritance.
		/// </summary>
		private Dictionary<Type, List<Type>> typesInheritance;

		/// <summary>
		/// Types that were viewed during types inheritance parsing.
		/// </summary>
		private Dictionary<Type, object> viewedInheritanceTypes;

		/// <summary>
		/// Class GUIDs that were viewed during parsing.
		/// </summary>
		private Dictionary<Guid, object> classViewedGUIDs;


		/// <summary>
		/// Collection types that were processed during types inheritance parsing.
		/// </summary>
		private Dictionary<Type, SerializeCollectionEntry> collectionTypes;

		/// <summary>
		/// Dictionary types that were processed during types inheritance parsing.
		/// </summary>
		private Dictionary<Type, SerializeDictionaryEntry> dictionaryTypes;

		/// <summary>
		/// List types that were processed during types inheritance parsing.
		/// </summary>
		private Dictionary<Type, SerializeListEntry> listTypes;

		/// <summary>
		/// Array types that were processed during types inheritance parsing.
		/// </summary>
		private Dictionary<Type, SerializeArrayEntry> arrayTypes;


		/// <summary>
		/// Has any new types been processed during current processing.
		/// </summary>
		private bool newTypesProcessed = false;

		/// <summary>
		/// Should common header be processed.
		/// </summary>
		internal bool ShouldProcessCommonHeader = true; // we should parse header, at least at first deserialization

		#endregion


		#region Stored GUIDs.

		#region Classes.

		/// <summary>
		/// Collection of <see cref="Guid"/> of the known class, restored from the currently deserialized text - stored class counter.
		/// <para>Exists all the lifetime of current class.</para>
		/// </summary>
		private Dictionary<Guid, int> storedClassesGUIDs;
		/// <summary>
		/// Mapping of stored class counter - possible info of the known class, restored from the currently deserialized text.
		/// <para>Exists all the lifetime of current class.</para>
		/// </summary>
		private Dictionary<int, SerializeTypeInfoEntry> storedClassesIntMapping;

		/// <summary>
		/// Count of <see cref="storedClassesIntMapping"/>.
		/// </summary>
		private int storedClassesGUIDsLength;
		
		#endregion


		#region Properties.

		/// <summary>
		/// Collection of <see cref="Guid"/> of the known property, restored from the currently deserialized text, separated by class <see cref="Guid"/>.
		/// <para>Exists all the lifetime of current class.</para>
		/// </summary>
		private Dictionary<Guid, List<Guid>> storedPropertiesGUIDs;

		#endregion

		#endregion


		#region Text process variables.

		/// <summary>
		/// Restored text.
		/// </summary>
		private char[] text;

		/// <summary>
		/// Restored text length.
		/// </summary>
		private int textLength;

		/// <summary>
		/// Position of common header in the stream (full stream or a common header one).
		/// </summary>
		private int commonHeaderPosition = -1;

		#endregion

		#endregion  


		#region Constructors.

		/// <summary>
		/// Creates new instance of <see cref="SerializeProcess"/> without parameters.
		/// </summary>
		public SerializeProcess()
		{
			// Nullify common properties.
			this.Stream = null;
			this.ProcessedObject = null;
			this.ObjectType = null;

			// Create functions collections.
			createKnownTypeWrappers();
		}


		/// <summary>
		/// Creates new instance of <see cref="SerializeProcess"/>.
		/// </summary>
		/// <param name="stream">Stream to work on.</param>
		/// <param name="processedObject">Object to process.</param>
		/// <param name="commonHeaderStream">Stream of common header.</param>
		public SerializeProcess(Stream stream, object processedObject, Stream commonHeaderStream)
			: this()
		{
			// Create process objects.
			this.Stream = stream;
			this.CommonHeaderStream = commonHeaderStream;
			this.ProcessedObject = processedObject;
			this.ObjectType = this.ProcessedObject.GetType();
		}


		/// <summary>
		/// Creates new instance of <see cref="SerializeProcess"/>.
		/// </summary>
		/// <param name="stream">Stream to work on.</param>
		/// <param name="objectType">Type of the object to use.</param>
		/// <param name="commonHeaderStream">Stream of common header.</param>
		public SerializeProcess(Stream stream, Type objectType, Stream commonHeaderStream)
			: this()
		{
			// Create process objects.
			this.Stream = stream;
			this.CommonHeaderStream = commonHeaderStream;
			this.ObjectType = objectType;
		}

		#endregion


		#region Public functions.

		#region Serialize().

		/// <summary>
		/// Serializes processed object to given stream, without types processing.
		/// </summary>
		public void Serialize()
		{
			this.Serialize(true);
		}


		/// <summary>
		/// Serializes processed object to given stream, with types processing.
		/// </summary>
		/// <param name="types">Collection of known types to process.</param>
		public void Serialize(List<Type> types)
		{
			// Possibly read common header.
			readCommonHeader();

			// Process given types.
			this.processKnownTypes(types);
			
			this.Serialize(false);
		}

		/// <summary>
		/// Serializes processed object to given stream.
		/// </summary>
		/// <param name="shouldReadCommonHeader">Should common header be read.</param>
		private void Serialize(bool shouldReadCommonHeader)
		{
			// Possibly read common header.
			if (shouldReadCommonHeader) readCommonHeader();

			// Get type info.
			processTypes(this.ObjectType);

			// Possibly get object data.
			if (this.anyTypesProcessed()) processObjectData();
		}

		#endregion


		#region Deserialize().

		/// <summary>
		/// Deserializes object.
		/// </summary>
		public object Deserialize()
		{
			// Read text.
			readDeserializationText();

			return this.Deserialize(true);
		}


		/// <summary>
		/// Deserializes object.
		/// </summary>
		/// <param name="types">Possible collection of types to use.</param>
		public object Deserialize(List<Type> types)
		{
			// Read text.
			readDeserializationText();

			// Possibly read common header.
			readCommonHeader();

			// Process knon types.
			this.processKnownTypes(types);
			this.ShouldProcessCommonHeader = true; // when we are provided with known types, we should process common header

			// Deserialize.
			return this.Deserialize(false);
		}


		/// <summary>
		/// Deserializes object.
		/// </summary>
		/// <param name="shouldReadCommonHeader">Should common header be read.</param>
		public object Deserialize(bool shouldReadCommonHeader)
		{
			// Possibly read common header.
			if (shouldReadCommonHeader) readCommonHeader();

			// Get type info.
			processTypes(this.ObjectType);

			// Possibly get object data.
			if (this.anyTypesProcessed()) restoreObjectData();

			// Set indicator for possible future usage.
			this.ShouldProcessCommonHeader = false;

			// Clear processing variables.
			this.text = null;

			// Return processed object.
			return this.ProcessedObject;
		}

		#endregion

		#endregion


		#region Auxiliary functions.

		#region Get type meta-data.

		#region createProcessTypeProperties().

		/// <summary>
		/// Creates necessary properties to process types metadata.
		/// </summary>
		private void createProcessTypeProperties()
		{
			// Type to process.
			this.typesToProcess = new List<Type>();

			// Class attributes.
			this.typesAttributes = new Dictionary<Type, KTSerializeAttribute>();

			// Types that were viewed during types adding/not adding to types to process.
			this.decidedTypes = new Dictionary<Type, object>();

			// Types that were viewed during types parsing.
			this.viewedTypes = new Dictionary<Type, object>();

			// Collection of all processed types data of any inheritance.
			this.allTypesData = new Dictionary<Type, SerializeTypeInfoEntry>();

			// Collected types inheritance.
			this.typesInheritance = new Dictionary<Type, List<Type>>();

			// Types that were viewed during types inheritance parsing.
			this.viewedInheritanceTypes = new Dictionary<Type, object>();

			// Class GUIDs that were viewed during parsing.
			this.classViewedGUIDs = new Dictionary<Guid, object>();


			// Collection types that were processed during types inheritance parsing.
			this.collectionTypes = new Dictionary<Type, SerializeCollectionEntry>();

			// Dictionary types that were processed during types inheritance parsing.
			this.dictionaryTypes = new Dictionary<Type, SerializeDictionaryEntry>();

			// List types that were processed during types inheritance parsing.
			this.listTypes = new Dictionary<Type, SerializeListEntry>();

			// Array types that were processed during types inheritance parsing.
			this.arrayTypes = new Dictionary<Type, SerializeArrayEntry>();
		}
		
		#endregion


		#region processKnownTypes().

		/// <summary>
		/// Processes types to the types metadata of the serialization process.
		/// </summary>
		/// <param name="types">Types to process.</param>
		private void processKnownTypes(List<Type> types)
		{
			foreach (Type type in types)
			{
				this.processTypes(type);
			}
		}

		#endregion


		#region processTypes().

		/// <summary>
		/// Processes type and adds all necessary its and its descendants meta-data to the collection of meta-data of current serialization process.
		/// </summary>
		/// <param name="initialType">Type to process.</param>
		private void processTypes(Type initialType)
		{
			#region Declare variables.

			// NB.! initialType in always not null here.

			// Initial check.
			if (viewedTypes.ContainsKey(initialType)) return;

			// Try to put initial type to serialization.
			decideIfAddToProcessTypes(initialType);
			// If initial type should not be serialized, we do nothing.
			if (typesToProcess.Count < 1) return;

			// Set indicators.
			this.newTypesProcessed = true;
			this.ShouldProcessCommonHeader = true; // if we have added some types, we should reread header file (when such is present)

			// Collection of temporary processed types, processed at first stage.
			Dictionary<Type, SerializeTypeInfoEntry> tempProcessedTypesData = new Dictionary<Type, SerializeTypeInfoEntry>();

			#endregion


			#region Gather info by types one-by-one.

			try
			{
				while (typesToProcess.Count > 0)
				{
					#region Get current type.

					// Type to process.
					Type currentType = typesToProcess[0];

					#endregion


					#region Gather types inheritance.

					// Inheritance.
					List<Type> parentTypes = new List<Type>() { currentType };
					typesInheritance.Add(currentType, parentTypes);

					{
						// Auxiliary variables.
						Type tempType = currentType;

						while (
							tempType.BaseType != null
							&&
							tempType.BaseType != ObjectTypes.Object
							&&
							tempType.BaseType != ObjectTypes.ValueType
							)
						{
							// Go further.
							tempType = tempType.BaseType;

							// Decide if type is serializable.
							if (Attribute.GetCustomAttribute(tempType, KTSerializer.SerializeAttributeType, false) == null)
							{
								throw new KTSerializeException(String.Format(
									Properties.Resources.Error_TypeIsNotSerializable,
									tempType.Name
									));
							}

							// Add to inheritance.
							parentTypes.Add(tempType);
						}
					}

					#endregion


					#region Process types inheritance.

					// If we have not yet processed this inheritance chain.
					if (!viewedInheritanceTypes.ContainsKey(currentType))
					{
						// Add to viewed types to prevent possible further including.
						viewedTypes[currentType] = null;
						viewedInheritanceTypes.Add(currentType, null);

						foreach (Type tempType in parentTypes)
						{
							// If we have already processed this type (and so, its ancestors), it's useless to repeat it.
							if (!viewedInheritanceTypes.ContainsKey(tempType))
							{
								// If we have not yet viewed this type.
								if (!decidedTypes.ContainsKey(tempType))
								{
									// Add type to processing.
									typesToProcess.Add(tempType);

									// Keep in viewed and decided.
									decidedTypes.Add(tempType, null);

									// Keep type attribute.
									addToTypesAttributes(
										tempType,
										Attribute.GetCustomAttribute(tempType, KTSerializer.SerializeAttributeType, false) as KTSerializeAttribute
										);
								}

								viewedInheritanceTypes.Add(tempType, null);
							}
						}
					}

					#endregion


					#region Process type.

					// Process type.
					SerializeTypeInfoEntry infoEntry = new SerializeTypeInfoEntry();
					processTypeMetaData(currentType, infoEntry);

					// Append entry.
					tempProcessedTypesData[currentType] = infoEntry;

					#endregion


					#region Add types of the properties and fields to types to process.

					foreach (SerializeIDEntry entry in infoEntry.IdEntries)
					{
						// If we have not yet processed this type.
						if (!viewedTypes.ContainsKey(entry.TypeToProcess))
						{
							// Check possible collection.
							bool isSupportedCollection = decideIfAddCollectionType(entry.TypeToProcess);

							// If type of the property or field has necessary attribute.
							if (!isSupportedCollection)
							{
								decideIfAddToProcessTypes(entry.TypeToProcess);
							}
						}
					}
					/**/
					#endregion


					// Remove type from procesing.
					typesToProcess.RemoveAt(0);
				}
			}
			finally
			{
				// Always clear processing collections.
				typesToProcess.Clear();
			}

			#endregion


			#region Combine inheritance chains.

			foreach (KeyValuePair<Type, SerializeTypeInfoEntry> pair in tempProcessedTypesData)
			{
				#region Create type entry.

				// Create new entry to keep result in.
				SerializeTypeInfoEntry newEntry = new SerializeTypeInfoEntry()
				{
					ClassAttribute = this.typesAttributes[pair.Key],
					Type = pair.Key
				};
				// Create type constructor of the entry.
				newEntry.createConstructor();
				// Append entries of the current type.
				combineInfoEntries(newEntry, pair.Value);
				
				// Auxiliary variable.
				Guid classID = newEntry.ClassAttribute.ClassID;

				#endregion


				#region Process inheritance chain.

				List<Type> parentTypes = typesInheritance[pair.Key];

				// NB.! We skip first type which is the same as the current type.
				for (int i = 1; i < parentTypes.Count; i++)
				{
					#region Append attribute entries of the parent type to the collection of entries of the current type.

					Type parentType = parentTypes[i];

					// Try to get entry from the temporary types data.
					SerializeTypeInfoEntry entryInfo;
					if (tempProcessedTypesData.TryGetValue(parentType, out entryInfo))
					{
						combineInfoEntries(newEntry, entryInfo);
					}

					// Otherwise, it means that we have already processed this type in some other type.
					else
					{
						// Try to get info from processed types.
						if (allTypesData.TryGetValue(parentType, out entryInfo))
						{
							combineInfoEntries(newEntry, entryInfo);
						}

						// Do not process anymore, all other parent types have been already processed.
						break;
					}

					#endregion
				}

				#endregion


				#region Process full entry.

				// Keep in all processed types.
				allTypesData.Add(pair.Key, newEntry);
				

				// If current type should be saved in common types collection.
				if (classViewedGUIDs.ContainsKey(classID))
				{
					#region Keep type info.

					// Keep type in serialize process property.
					this.TypesData.Add(pair.Key, newEntry);

					// Try to get class counter from stored info.
					int classCounter = 0;
					if (this.storedClassesGUIDs.TryGetValue(classID, out classCounter))
					{
						newEntry.ClassGuidStartCount = classCounter;
					}
					// Otherwise, add class info to stored classes collections.
					else
					{
						newEntry.ClassGuidStartCount = this.storedClassesGUIDsLength;
						this.storedClassesGUIDs.Add(classID, this.storedClassesGUIDsLength);

						this.storedClassesGUIDsLength++; // it's a length, thus is bigger than the biggest counter
					}

					// Add type to types counters info (always).
					this.storedClassesIntMapping.Add(newEntry.ClassGuidStartCount, newEntry);
					// NB.! "Item with the same key has already been added" exception here may well mean that abstract clas GUID is equal to some end class GUID.
					
					#endregion


					#region Process properties.

					// Set ID entries numbers.
					List<Guid> storedPropertiesList;
					this.storedPropertiesGUIDs.TryGetValue(classID, out storedPropertiesList);
					newEntry.SetIdEntriesNumbers(storedPropertiesList);

					// Keep property entries for some future use.
					this.storedPropertiesGUIDs[classID] = newEntry.StoredPropertiesGUIDs.Keys.ToList();


					// Process property entries finally.
					int idEntriesCount = newEntry.IdEntries.Count;
					for (int i = 0; i < idEntriesCount; i++)
					{
						// Current ID entry.
						SerializeIDEntry idEntry = newEntry.IdEntries[i];

						// Set collection info.
						idEntry.CollectionEntry = getCollectionEntry(idEntry.Type);

						// Set common known type wrapper.
						idEntry.IsTypeInfoEntrySet = setKnownTypeWrapper(idEntry);
					}

					#endregion
				}

				#endregion
			}

			#endregion


			#region Process SerializeIDEntry-s.

			foreach (KeyValuePair<Type, SerializeTypeInfoEntry> pair in this.TypesData)
			{
				int entriesCount = pair.Value.IdEntries.Count;
				for (int i = 0; i < entriesCount; i++)
				{
					// If we have not yet processed this id entry.
					SerializeIDEntry currentEntry = pair.Value.IdEntries[i];
					if (!currentEntry.IsTypeInfoEntrySet)
					{
						// Append SerializeIDEntry to appropriate SerializeTypeInfoEntry.
						this.TypesData.TryGetValue(currentEntry.TypeToProcess, out currentEntry.TypeInfoEntry);

						// Set indicator.
						currentEntry.IsTypeInfoEntrySet = true;
					}
				}
			}

			#endregion


			#region Process collection entries.

			#region Process dictionary entries.

			foreach (KeyValuePair<Type, SerializeDictionaryEntry> pair in this.dictionaryTypes)
			{
				// If we have not yet processed key entry.
				if (!pair.Value.IsTypeInfoEntrySet)
				{
					// Append entries to appropriate SerializeTypeInfoEntry.
					this.TypesData.TryGetValue(pair.Value.KeyItemEntry.TypeToProcess, out pair.Value.KeyItemEntry.TypeInfoEntry);
					this.TypesData.TryGetValue(pair.Value.ValueItemEntry.TypeToProcess, out pair.Value.ValueItemEntry.TypeInfoEntry);

					// Set common known type wrappers.
					setKnownTypeWrapper(pair.Value.KeyItemEntry);
					setKnownTypeWrapper(pair.Value.ValueItemEntry);

					// Set indicator.
					pair.Value.IsTypeInfoEntrySet = true;
				}
			}
			
			#endregion


			#region Process list entries.

			foreach (KeyValuePair<Type, SerializeListEntry> pair in this.listTypes)
			{
				// If we have not yet processed key entry.
				if (!pair.Value.IsTypeInfoEntrySet)
				{
					// Append entry to appropriate SerializeTypeInfoEntry.
					this.TypesData.TryGetValue(pair.Value.ValueItemEntry.TypeToProcess, out pair.Value.ValueItemEntry.TypeInfoEntry);

					// Set common known type wrappers.
					setKnownTypeWrapper(pair.Value.ValueItemEntry);

					// Set indicator.
					pair.Value.IsTypeInfoEntrySet = true;
				}
			}

			#endregion


			#region Process array entries.

			foreach (KeyValuePair<Type, SerializeArrayEntry> pair in this.arrayTypes)
			{
				// If we have not yet processed key entry.
				if (!pair.Value.IsTypeInfoEntrySet)
				{
					// Append entry to appropriate SerializeTypeInfoEntry.
					this.TypesData.TryGetValue(pair.Value.ValueItemEntry.TypeToProcess, out pair.Value.ValueItemEntry.TypeInfoEntry);

					// Set common known type wrappers.
					setKnownTypeWrapper(pair.Value.ValueItemEntry);

					// Set indicator.
					pair.Value.IsTypeInfoEntrySet = true;
				}
			}

			#endregion

			#endregion
		}

		/**/
		#endregion



		#region processTypeMetaData().

		/// <summary>
		/// Binding flags to select class level only meta-info (both public and private).
		/// </summary>
		private static BindingFlags classLevelMetaInfo =
			(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);


		/// <summary>
		/// Processes type meta-data and gathers information, related to the serialization, to the sent collection.
		/// </summary>
		/// <param name="type">Type to process.</param>
		/// <param name="infoEntry">Entry to put gathered data to.</param>
		private void processTypeMetaData(Type type, SerializeTypeInfoEntry infoEntry)
		{
			#region Gather meta-data.

			PropertyInfo[] properties = type.GetProperties(classLevelMetaInfo);
			FieldInfo[] fields = type.GetFields(classLevelMetaInfo);

			#endregion


			#region Get properties with necessary attribute.

			foreach (PropertyInfo property in properties)
			{
				object[] attributes = property.GetCustomAttributes(KTSerializer.IncludeAttributeType, false);
				foreach (object attribute in attributes)
				{
					// Create new entry.
					SerializePropertyIDEntry newEntry = new SerializePropertyIDEntry(true)
					{
						PropertyInfo = property,
						IncludeAttribute = (KTSerializeIncludeAttribute)attribute
					};

					// Add to collections.
					infoEntry.AddIdEntry(newEntry);
				}
			}

			#endregion


			#region Get fields with necessary attribute.

			// Get fields with necessary attribute.
			foreach (FieldInfo field in fields)
			{
				object[] attributes = field.GetCustomAttributes(KTSerializer.IncludeAttributeType, false);
				foreach (object attribute in attributes)
				{
					// Create new entry.
					SerializeFieldIDEntry newEntry = new SerializeFieldIDEntry(true)
					{
						FieldInfo = field,
						IncludeAttribute = (KTSerializeIncludeAttribute)attribute
					};

					// Add to collections.
					infoEntry.AddIdEntry(newEntry);
				}
			}

			#endregion


			// Process type's methods.
			processTypeMethodInfo(type, infoEntry);
		}

		#endregion


		#region processTypeMethodInfo().

		/// <summary>
		/// Processes type <see cref="MethodInfo"/> with necesary attributes.
		/// </summary>
		/// <param name="type">Type to process.</param>
		/// <param name="infoEntry">Entry to put gathered data to.</param>
		private void processTypeMethodInfo(Type type, SerializeTypeInfoEntry infoEntry)
		{
			#region Initial actions.

			// Get type methods.
			MethodInfo[] methods = type.GetMethods(classLevelMetaInfo);
			
			#endregion


			#region Before serialize.

			{
				int counter = 0;
				foreach (MethodInfo method in methods)
				{
					object[] attributes = method.GetCustomAttributes(KTSerializer.BeforeSerializeAttributeType, false);
					if (attributes.Length == 0) continue;

					// Check count.
					counter++;
					if (counter > 1)
					{
						throw new KTSerializeAttributeException(String.Format(
							Properties.Resources.Error_AttributeIsMoreThanOnce,
							KTSerializer.BeforeSerializeAttributeType.Name
							));
					}

					// Process attribute.
					infoEntry.AddBeforeSerialize(method);
				}
			}

			#endregion


			#region After serialize.

			{
				int counter = 0;
				foreach (MethodInfo method in methods)
				{
					object[] attributes = method.GetCustomAttributes(KTSerializer.AfterSerializeAttributeType, false);
					if (attributes.Length == 0) continue;

					// Check count.
					counter++;
					if (counter > 1)
					{
						throw new KTSerializeAttributeException(String.Format(
							Properties.Resources.Error_AttributeIsMoreThanOnce,
							KTSerializer.AfterSerializeAttributeType.Name
							));
					}

					// Process attribute.
					infoEntry.AddAfterSerialize(method);
				}
			}

			#endregion


			#region Before deserialize.

			{
				int counter = 0;
				foreach (MethodInfo method in methods)
				{
					object[] attributes = method.GetCustomAttributes(KTSerializer.BeforeDeserializeAttributeType, false);
					if (attributes.Length == 0) continue;

					// Check count.
					counter++;
					if (counter > 1)
					{
						throw new KTSerializeAttributeException(String.Format(
							Properties.Resources.Error_AttributeIsMoreThanOnce,
							KTSerializer.BeforeDeserializeAttributeType.Name
							));
					}

					// Process attribute.
					infoEntry.AddBeforeDeserialize(method);
				}
			}

			#endregion


			#region After deserialize.

			{
				int counter = 0;
				foreach (MethodInfo method in methods)
				{
					object[] attributes = method.GetCustomAttributes(KTSerializer.AfterDeserializeAttributeType, false);
					if (attributes.Length == 0) continue;

					// Check count.
					counter++;
					if (counter > 1)
					{
						throw new KTSerializeAttributeException(String.Format(
							Properties.Resources.Error_AttributeIsMoreThanOnce,
							KTSerializer.AfterDeserializeAttributeType.Name
							));
					}

					// Process attribute.
					infoEntry.AddAfterDeserialize(method);
				}
			}

			#endregion
		}
		
		#endregion


		#region getCollectionEntry().

		/// <summary>
		/// Tries to get possible collection info for the property/field entry.
		/// </summary>
		/// <param name="type"><see cref="Type"/> to set collection info for.</param>
		/// <returns>Possible collection entry for the given type.</returns>
		private SerializeCollectionEntry getCollectionEntry(Type type)
		{
			// Try to find dictionary entry.
			SerializeCollectionEntry collectionEntry;
			this.collectionTypes.TryGetValue(type, out collectionEntry);
			return collectionEntry;
		}
		
		#endregion


		#region getTypeInfo().

		/// <summary>
		/// Gets type info.
		/// </summary>
		/// <param name="type">Type to process.</param>
		/// <param name="underlyingType">Possible inderlying type of the given type.</param>
		/// <param name="typeToProcess">Real (not nullable) type, taken from the given type.</param>
		private static void getTypeInfo(
			Type type,
			out Type underlyingType,
			out Type typeToProcess
			)
		{
			underlyingType = ObjectHelper.GetUnderlyingType(type);
			typeToProcess = underlyingType != null ? underlyingType : type; // real type
		}

		/**/
		#endregion


		#region setKnownTypeWrapper().

		/// <summary>
		/// Tries to set common known type wrapper for an item entry.
		/// </summary>
		/// <param name="entry">Entry to set wrapper for.</param>
		/// <returns>True if common known type wrapper has been set, false otherwise.</returns>
		private bool setKnownTypeWrapper(SerializeItemEntry entry)
		{
			// NB.! If we have common known type wrapper, we do not need to look for type info.

			if (!this.commonKnownWrappers.TryGetValue(entry.Type, out entry.KnownTypeWrapper))
			{
				// Enum.
				if (entry.Type.IsEnum)
				{
					entry.KnownTypeWrapper = this.enumWrapper;

					entry.IsEnumType = true;
					entry.HasPrimitiveTypeWrapper = true;

					return true;
				}

				// Enum?.
				else if (
					entry.HasUnderlyingType
					&&
					entry.UnderlyingType.IsEnum
					)
				{
					entry.KnownTypeWrapper = this.enumNullableWrapper;

					entry.IsEnumType = true;
					entry.IsEnumNullableType = true;
					entry.HasPrimitiveTypeWrapper = true;

					return true;
				}
				else
				{
					return false;
				}
			}

			// If we have a valid common known type wrapper.
			else
			{
				if (entry.Type != ObjectTypes.Object)
				{
					entry.HasPrimitiveTypeWrapper = true;
				}

				return true;
			}
		}
		
		#endregion



		#region decideIfAddToProcessTypes().

		/// <summary>
		/// Decides if type should be added to types that should be processed to get meta-data.
		/// </summary>
		/// <param name="type">Type to process.</param>
		private void decideIfAddToProcessTypes(Type type)
		{
			#region Get attribute and store it in classes.

			KTSerializeAttribute attribute = Attribute.GetCustomAttribute(type, KTSerializer.SerializeAttributeType, false) as KTSerializeAttribute;

			// We store class GUID always because type may be added to decided t inheritance processing, and that can prevent it from being added to class GUIDs at properties processing.

			if (!viewedTypes.ContainsKey(type))
			{
				if (attribute != null)
				{
					// Check class GUID uniqueness.
					if (classViewedGUIDs.ContainsKey(attribute.ClassID))
					{
						throw new KTSerializeAttributeException(String.Format(
							Properties.Resources.Error_TypeAttributeGuidIsNotUnique,
							attribute.ClassID.ToString()
							));
					}
					else
					{
						// Add to viewed GUIDs mapping.
						this.classViewedGUIDs.Add(attribute.ClassID, null);
					}
				}

				// Add to viewed.
				viewedTypes.Add(type, null);
			}
			
			#endregion


			// If we have not yet viewed this type.
			if (!decidedTypes.ContainsKey(type))
			{
				#region Process in common way.

				// If type of the property or field has necessary attribute.
				if (attribute != null)
				{
					typesToProcess.Add(type);
					addToTypesAttributes(type, attribute);
				}

				#endregion


				#region Process possible generics.

				else
				{
					decideIfAddCollectionType(type);
				}

				#endregion


				#region Final actions.

				// Keep reference.
				this.decidedTypes.Add(type, null);

				#endregion
			}
		}

		#endregion


		#region decideIfAddCollectionType().

		/// <summary>
		/// Decides if type is a supported collection one and should be added to types that should be processed to get meta-data.
		/// </summary>
		/// <param name="type">Type to process.</param>
		/// <returns>True if type is a supported collection, false otherwise.</returns>
		private bool decideIfAddCollectionType(Type type)
		{
			#region Array.

			if (type.IsArray)
			{
				// Process type to entry in a special collection.
				SerializeArrayEntry collectionEntry;
				if (!this.arrayTypes.TryGetValue(type, out collectionEntry))
				{
					// Create entry.
					collectionEntry = new SerializeArrayEntry() { Type = type };
					this.arrayTypes.Add(type, collectionEntry);
					this.collectionTypes.Add(type, collectionEntry);


					// Check array type.
					if (!decideIfAddCollectionType(collectionEntry.ValueItemEntry.Type))
					{
						decideIfAddToProcessTypes(collectionEntry.ValueItemEntry.TypeToProcess);
					}
					else
					{
						// When type itself is a collection.
						collectionEntry.ValueItemEntry.CollectionEntry = getCollectionEntry(collectionEntry.ValueItemEntry.Type);
					}
				}

				return true;
			}

			#endregion


			#region Possible generic collection.

			else
			{
				// Get possible generic type.
				Type genericType = ObjectHelper.GetGenericType(type);

				// Check for supported collections.
				bool result = (genericType != null && KTSerializer.CheckSupportedCollection(genericType));
				if (result)
				{
					#region If it's a dictionary.

					if (genericType == KTSerializer.DictionaryType)
					{
						// Process type to entry in a special collection.
						SerializeDictionaryEntry collectionEntry;
						if (!this.dictionaryTypes.TryGetValue(type, out collectionEntry))
						{
							// Create entry.
							collectionEntry = new SerializeDictionaryEntry() { Type = type };
							this.dictionaryTypes.Add(type, collectionEntry);
							this.collectionTypes.Add(type, collectionEntry);


							// Check key type.
							if (!decideIfAddCollectionType(collectionEntry.KeyItemEntry.Type))
							{
								decideIfAddToProcessTypes(collectionEntry.KeyItemEntry.TypeToProcess);
							}
							else
							{
								// When type itself is a collection.
								collectionEntry.KeyItemEntry.CollectionEntry = getCollectionEntry(collectionEntry.KeyItemEntry.Type);
							}


							// Check value type.
							if (!decideIfAddCollectionType(collectionEntry.ValueItemEntry.Type))
							{
								decideIfAddToProcessTypes(collectionEntry.ValueItemEntry.TypeToProcess);
							}
							else
							{
								// When type itself is a collection.
								collectionEntry.ValueItemEntry.CollectionEntry = getCollectionEntry(collectionEntry.ValueItemEntry.Type);
							}
						}
					}

					#endregion


					#region If it's a list.

					else if (genericType == KTSerializer.ListType)
					{
						// Process type to entry in a special collection.
						SerializeListEntry collectionEntry;
						if (!this.listTypes.TryGetValue(type, out collectionEntry))
						{
							// Create entry.
							collectionEntry = new SerializeListEntry() { Type = type };
							this.listTypes.Add(type, collectionEntry);
							this.collectionTypes.Add(type, collectionEntry);


							// Check list type.
							if (!decideIfAddCollectionType(collectionEntry.ValueItemEntry.Type))
							{
								decideIfAddToProcessTypes(collectionEntry.ValueItemEntry.TypeToProcess);
							}
							else
							{
								// When type itself is a collection.
								collectionEntry.ValueItemEntry.CollectionEntry = getCollectionEntry(collectionEntry.ValueItemEntry.Type);
							}
						}
					}

					#endregion

					// We ignore all other collection types now.
				}

				return result;
			}
			
			#endregion
		}

		#endregion



		#region anyTypesProcessed().

		/// <summary>
		/// If any type meta-data was collected during types processing (that is, if we have anything to serialize/deserialize).
		/// </summary>
		/// <returns>True if any type meta-data was collected, false otherwise.</returns>
		private bool anyTypesProcessed()
		{
			// No types processed or initial type is not serializable.
			if (
				this.allTypesData.Count < 1
				||
				!this.allTypesData.ContainsKey(this.ObjectType)
				) return false;

			return true;
		}

		#endregion


		#region addToTypesAttributes().

		/// <summary>
		/// Add attribute to the necessary processed collections.
		/// </summary>
		/// <param name="type">Type this attribute belongs to.</param>
		/// <param name="attribute">Attribute to add.</param>
		/// <param name="addToViewedGUIDs">Should attribute be added to viewed GUIDs collection.</param>
		private void addToTypesAttributes(Type type, KTSerializeAttribute attribute)
		{
			// Add to collection.
			typesAttributes.Add(type, attribute);
		}
		
		#endregion


		#region combineInfoEntries().

		/// <summary>
		/// Combines info in serialization type entries.
		/// </summary>
		/// <param name="newEntry">Entry to copy info to.</param>
		/// <param name="entryInfo">Entry to copy info from.</param>
		private static void combineInfoEntries(
			SerializeTypeInfoEntry newEntry,
			SerializeTypeInfoEntry entryInfo
			)
		{
			// Add property entries.
			int count = entryInfo.IdEntries.Count;
			for(int i = 0; i < count; i++)
			{
				SerializeIDEntry currentEntry = entryInfo.IdEntries[i];

				// If this is a field entry.
				if (currentEntry is SerializeFieldIDEntry)
				{
					newEntry.AddIdEntry(new SerializeFieldIDEntry(currentEntry));
				}

				// If this is a property entry.
				else if (currentEntry is SerializePropertyIDEntry)
				{
					newEntry.AddIdEntry(new SerializePropertyIDEntry(currentEntry));
				}
			}


			// Copy before/after functions.
			entryInfo.CopyBeforeAfterHandlers(newEntry);
		}
		
		#endregion



		#region readCommonHeader().

		/// <summary>
		/// Reads common header (that is, header with classes, properties and fields information).
		/// </summary>
		private void readCommonHeader()
		{
			// If we have not yet read header.
			// NB.! We read header once per serialization process only. If we do not have a common header, it makes no worse. If we have one, new types are added to necessary collections at time of types processing, so we need not to read stoled info (and if this info is rewritten, it's rewritten from processed types collections).
			if (
				commonHeaderPosition < 0 // if we have had a header, but failed to parse it; seldom situation though
				||
				this.storedClassesGUIDs == null
				)
			{
				// Recreate type processing properties. We need to do it, because we always must process header before processing types.
				createProcessTypeProperties();


				bool hasCommonHeaderStream = (this.CommonHeaderStream != null);
				commonHeaderPosition = 0;

				// Get header info from a special stream.
				if (hasCommonHeaderStream)
				{
					string commonHeaderText = null;
					getTextFromStream(this.CommonHeaderStream, ref commonHeaderText);
					parseCommonHeader(ref commonHeaderText, ref commonHeaderPosition, ref hasCommonHeaderStream);
				}
				// Get header info from the full text.
				else
				{
					string currentText = this.text == null ? null : new string(this.text, 0, this.textLength);
					parseCommonHeader(ref currentText, ref commonHeaderPosition, ref hasCommonHeaderStream);
				}
			}
		}

		#endregion


		#region parseCommonHeader().

		/// <summary>
		/// Parses common header information (that is, header with classes, properties and fields information).
		/// </summary>
		/// <param name="text">Text to process.</param>
		/// <param name="commonHeaderPosition">Common header start position variable to set.</param>
		/// <param name="hasCommonHeaderStream">If a special common header stream exists.</param>
		private void parseCommonHeader(
			ref string text,
			ref int commonHeaderPosition,
			ref bool hasCommonHeaderStream
			)
		{
			#region Initial actions.

			#region Where common header separator stays.

			// No text at all.
			if (text == null)
			{
				commonHeaderPosition = -1;
			}

			// Common header.
			else if (hasCommonHeaderStream)
			{
				// Either at start of file, when there is a classes information; or nowhere.
				commonHeaderPosition = text.Length > 0 ? 0 : -1;
			}

			// One header per serialization.
			else
			{
				commonHeaderPosition = text.LastIndexOf(KTSerializer.LineSeparator);
			}

			#endregion


			#region Set default values.

			this.storedClassesGUIDs = new Dictionary<Guid, int>();
			this.storedClassesIntMapping = new Dictionary<int, SerializeTypeInfoEntry>();
			this.storedClassesGUIDsLength = KTSerializer.CommonKnownTypesMaxPossibleNumber + 1;

			this.storedPropertiesGUIDs = new Dictionary<Guid, List<Guid>>();

			#endregion

			#endregion


			#region Read classes info.

			// If we have no classes, we cannot restore.
			if (commonHeaderPosition < 0) return;

			// If we have serialized any classes GUIDs information.
			string classesGUIDsText =
				hasCommonHeaderStream ?
				text.Substring(commonHeaderPosition) :
				text.Substring(commonHeaderPosition + 1);

			if (classesGUIDsText.Length > 0)
			{
				// Collection of one class infos: class and property GUIDs together.
				string[] guidStrings = classesGUIDsText.Split(KTSerializer.WordSeparator);
				int guidStringsCount = guidStrings.Length;

				// Keep variable.
				this.storedClassesGUIDsLength = KTSerializer.CommonKnownTypesMaxPossibleNumber + guidStringsCount + 1;

				for (int i = 0; i < guidStringsCount; i++)
				{
					#region Auxiliary variables.

					// Current class info string to parse.
					string guidString = guidStrings[i];
					int guidStringLength = guidString.Length;

					if (guidStringLength == 0) continue; // If no info is stored.

					// Collection of property GUIDs of current class.
					List<Guid> propertyGuids = null;
					
					#endregion


					#region Parse class info.

					for (int j = 0; j < guidStringLength; j += 32) // move by one GUID each cycle
					{
						#region Parse class GUID.

						if (j == 0)
						{
							try
							{
								// Parse class GUID.
								int offset = j;
								Guid classGUID = SerializeHelper.ParseHeaderGuid(ref guidString, ref offset);
								this.storedClassesGUIDs.Add(classGUID, i + KTSerializer.CommonKnownTypesMaxPossibleNumber + 1);

								// Keep property GUIDs collection.
								propertyGuids = new List<Guid>();
								this.storedPropertiesGUIDs.Add(classGUID, propertyGuids);
							}
							catch (Exception ex)
							{
								throw new KTSerializeException(
									String.Format(
										Properties.Resources.Error_FailedToDeserializeGuid,
										guidString
										),
									ex
									);
							}
						}

						#endregion


						#region Parse property GUID.

						else
						{
							try
							{
								// Get stored GUID.
								int offset = j;
								Guid propertyGUID = SerializeHelper.ParseHeaderGuid(ref guidString, ref offset);

								// Keep in collection.
								propertyGuids.Add(propertyGUID);
							}
							catch (Exception ex)
							{
								throw new KTSerializeException(
									String.Format(
										Properties.Resources.Error_FailedToDeserializeGuid,
										guidString
										),
									ex
									);
							}
						}

						#endregion
					}
					
					#endregion

				}
			}

			#endregion
		}

		#endregion

		#endregion


		#region Serialize objects data.

		#region processObjectData(), initial.

		/// <summary>
		/// Processes initial object's data for serialization.
		/// </summary>
		private void processObjectData()
		{
			#region Process data.

			// NB.! Working with MemoryStream and StreamWriter is appr. 2-3 times quicker than working with StringBuilder, but it's impossible to get current length from StreamWriter, so this class is useless. And when using StreamWriter.Flush() in necessary places, speed is appr. 7 times worse than with StringBuilder.

			// To put serialized info to.
			StringBuilder sb = new StringBuilder();

			// Start processing.
			SerializeTypeInfoEntry typeInfoEntry;
			if (this.TypesData.TryGetValue(this.ObjectType, out typeInfoEntry))
			{
				processObjectData(this.ProcessedObject, sb, typeInfoEntry);
			}
			
			#endregion


			#region Write common header.

			// If we have a special stream to save common header to.
			if (this.CommonHeaderStream != null)
			{
				// Keep common header position for not to read it again.
				commonHeaderPosition = 0;

				// If we have some new processed types to save.
				if (this.newTypesProcessed)
				{
					// NB.! We presume that stream length can only be expanded, because we can add classes to known only, and no classes or properties can loose their attributes at runtime. If they can loose them, a special mechanism should be implemented outside this class and provide a valid stream.

					// Write result header to StringBuilder.
					StringBuilder headerSB = new StringBuilder();
					writeCommonHeader(headerSB);

					// Save to stream.
					saveStringBuilderToStream(headerSB, this.CommonHeaderStream);
				}
			}
			else
			{
				// Append header separator.
				sb.Append(KTSerializer.LineSeparator);

				// Write result header to StringBuilder.
				writeCommonHeader(sb);
			}
			
			#endregion


			#region Final actions.

			// Put result to stream.
			saveStringBuilderToStream(sb, this.Stream);

			// Set new types indicator.
			this.newTypesProcessed = false;
			
			#endregion


			//string tempResult = sb.ToString(); // debug
		}

		/**/
		#endregion


		#region processObjectData(), one object.

		/// <summary>
		/// Processes object's data for serialization.
		/// </summary>
		/// <param name="value">Object to process.</param>
		/// <param name="sb"><see cref="StringBuilder"/> to put data to.</param>
		/// <param name="typeInfoEntry">Possible entry to use. If not sent, wil be taken from processed entries collection.</param>
		private void processObjectData(
			object value,
			StringBuilder sb,
			SerializeTypeInfoEntry typeInfoEntry
			)
		{
			#region Initial actions.

			// Call before serialize function.
			typeInfoEntry.CallBeforeSerialize(value);

			// Collection of lengths of appended values.
			int idEntriesCount = typeInfoEntry.IdEntriesArray.Length;
			int[] valueLengths = new int[idEntriesCount];

			// String builder lengths variables.
			int startLength = sb.Length;

			#endregion


			#region Get values.

			for (int i = 0; i < idEntriesCount; i++)
			{
				SerializeIDEntry idEntry = typeInfoEntry.IdEntriesArray[i];

				// Get value.
				object o = idEntry.GetValue(value);

				// If we have a value to process.
				if (o != null)
				{
					// Process value.
					processObjectValue(o, sb, idEntry);

					// Gather info about length of the appended information.
					// NB.! If object is null, we do not write anything, thus valueLengths[i] stays 0.
					valueLengths[i] = sb.Length - startLength;

					// Keep initial length.
					startLength = sb.Length;
				}
			}

			#endregion


			#region Write object header.

			// NB.! We write header even if we do not have serializable properties to tell apart null objects and valid objects without properties (which is not the same).

			// Write values lengths.
			for (int i = 0; i < idEntriesCount; i++)
			{
				// Write property entry number.
				SerializeHelper.WriteTypeNumber(sb, ref typeInfoEntry.IdEntriesArray[i].EntryNumber);

				// Write length.
				SerializeHelper.WriteLength(sb, valueLengths[i]);
			}

			// Append length of header with values lengths.
			SerializeHelper.WriteLength(sb, sb.Length - startLength);

			// Append object type.
			SerializeHelper.WriteTypeNumber(sb, ref typeInfoEntry.ClassGuidStartCount);

			// Call after serialize function.
			typeInfoEntry.CallAfterSerialize(value);

			#endregion
		}

		#endregion


		#region processCollectionData().

		/// <summary>
		/// Processes object's data for serialization.
		/// </summary>
		/// <param name="value">Object to process.</param>
		/// <param name="sb"><see cref="StringBuilder"/> to put data to.</param>
		/// <param name="collectionEntry">Collection entry to use.</param>
		private void processCollectionData(
			object value,
			StringBuilder sb,
			SerializeCollectionEntry collectionEntry
			)
		{
			#region Initial actions.

			// Collection of lengths of appended values.
			uint[] valueLengths = null;

			// Indicator.
			bool writeObjectHeader = false;
			// NB.! We do not write header for not supported generic types.

			// String builder lengths variables.
			int startLength = sb.Length,
				endLength = startLength;

			#endregion


			#region If it's a dictionary.

			if (collectionEntry is SerializeDictionaryEntry)
			{
				#region Initial actions.

				SerializeDictionaryEntry dictionaryEntry = (SerializeDictionaryEntry)collectionEntry;

				// Set indicator.
				writeObjectHeader = true;

				// Collection.
				IDictionary iDictionary = value as IDictionary;
				valueLengths = new uint[iDictionary.Count * 2];

				#endregion


				// Process key and value.
				int i = 0;
				foreach (DictionaryEntry pair in iDictionary)
				{
					#region Write key.

					//if (pair.Key != null)
					// is not necessary, dictionary does not accept a null key
					{
						// Process key.
						processObjectValue(pair.Key, sb, dictionaryEntry.KeyItemEntry);

						// Gather info about length of the appended information.
						endLength = sb.Length;
						valueLengths[i] = (uint)(endLength - startLength);

						// Keep initial length.
						startLength = endLength;
					}

					i++;

					#endregion


					#region Write value.

					if (pair.Value != null)
					{
						// Process value.
						processObjectValue(pair.Value, sb, dictionaryEntry.ValueItemEntry);

						// Gather info about length of the appended information.
						endLength = sb.Length;
						valueLengths[i] = (uint)(endLength - startLength);

						// Keep initial length.
						startLength = endLength;
					}

					i++;

					#endregion
				}
			}

			#endregion


			#region If it's a list.

			else if (collectionEntry is SerializeListEntry)
			{
				#region Initial actions.

				SerializeListEntry listEntry = (SerializeListEntry)collectionEntry;

				// Set indicator.
				writeObjectHeader = true;

				// Collection.
				IList iList = value as IList;
				valueLengths = new uint[iList.Count];

				#endregion


				// Process list type.
				for (int i = 0; i < iList.Count; i++)
				{
					// Get current value.
					object entry = iList[i];

					// If we have anything to save.
					if (entry != null)
					{
						// Process value.
						processObjectValue(entry, sb, listEntry.ValueItemEntry);

						// Gather info about length of the appended information.
						endLength = sb.Length;
						valueLengths[i] = (uint)(endLength - startLength);

						// Keep initial length.
						startLength = endLength;
					}
				}
			}

			#endregion


			#region If it's a array.

			else if (collectionEntry is SerializeArrayEntry)
			{
				#region Initial actions.

				SerializeArrayEntry arrayEntry = (SerializeArrayEntry)collectionEntry;

				// Set indicator.
				writeObjectHeader = true;

				// Collection.
				Array array = value as Array;
				int arrayCount = array.GetLength(0);
				valueLengths = new uint[arrayCount];

				#endregion


				// Process array type.
				for (int i = 0; i < arrayCount; i++)
				{
					// Get current value.
					object entry = arrayEntry.GetValue(array, i);

					// If we have anything to save.
					if (entry != null)
					{
						// Process value.
						processObjectValue(entry, sb, arrayEntry.ValueItemEntry);

						// Gather info about length of the appended information.
						endLength = sb.Length;
						valueLengths[i] = (uint)(endLength - startLength);

						// Keep initial length.
						startLength = endLength;
					}
				}
			}

			#endregion


			#region Write object header.

			if (writeObjectHeader)
			{
				// Write values lengths.
				int valueLengthsCount = valueLengths.Length;
				for (int i = 0; i < valueLengthsCount; i++)
				{
					SerializeHelper.WriteLength(sb, valueLengths[i]);
				}

				// Write header length.
				SerializeHelper.WriteLength(sb, sb.Length - endLength);
			}

			#endregion
		}

		#endregion


		#region processObjectValue().

		/// <summary>
		/// Processes object's data for serialization.
		/// </summary>
		/// <param name="o">Object to process.</param>
		/// <param name="sb"><see cref="StringBuilder"/> to put data to.</param>
		/// <param name="SerializeItemEntry">Entry to use.</param>
		private void processObjectValue(
			object o,
			StringBuilder sb,
			SerializeItemEntry idEntry
			)
		{
			// Some primitive or enum type.
			// NB.! We do not check real type, because type is either a primitive type, or an enum, or struct. All of those are not inheritable.
			if (idEntry.HasPrimitiveTypeWrapper)
			{
				serializeValue(o, sb, idEntry.KnownTypeWrapper);
			}

			// Collection.
			else if (idEntry.HasCollectionType)
			{
				processCollectionData(o, sb, idEntry.CollectionEntry);
			}

			// Common type or an object type.
			else
			{
				Type valueType = o.GetType();

				// If entry type is the same as the real type.
				if (valueType == idEntry.Type)
				{
					// If we have a type info.
					if (idEntry.TypeInfoEntry != null)
					{
						processObjectData(o, sb, idEntry.TypeInfoEntry);
					}

					// Possibly use known type handlers from the ID entry.
					else if (idEntry.KnownTypeWrapper != null)
					{
						serializeValue(o, sb, idEntry.KnownTypeWrapper);
					}
				}

				// Process real type.
				else
				{
					// Declare real types.
					Type valueTypeToProcess = null, valueUnderlyingType = null;

					getTypeInfo(
						valueType,
						out valueUnderlyingType,
						out valueTypeToProcess
						);

					// Try to save to real type.
					processTypes(valueTypeToProcess);

					// Serialize.
					serializeValue(ref o, sb, valueType, valueUnderlyingType, valueTypeToProcess);
					// NB.! This part of code is performed when object's type is not the ame as ID entry's one. So it's possible to have, say, Int32 saved as Int64, or, more common, string saved as object. In these cases we can quite have a wrapper for object's type.
				}
			}
		}
		
		#endregion



		#region serializeValue().

		/// <summary>
		/// Processes one value, whose type is not known, for serialization.
		/// </summary>
		/// <param name="value">Value to process.</param>
		/// <param name="sb"><see cref="StringBuilder"/> to put data to.</param>
		/// <param name="type">Value type.</param>
		/// <param name="underlyingType">Possible underlying type when type is a nullable one.</param>
		/// <param name="typeToProcess">Type (main or underlying) that should be processed.</param>
		private void serializeValue(
			ref object value,
			StringBuilder sb,
			Type type,
			Type underlyingType,
			Type typeToProcess
			)
		{
			// NB.! Value cannot be null here.

			#region Try to find serialization function, if we are not provided with one.

			KnownTypeWrapper wrapper;
			this.commonKnownWrappers.TryGetValue(type, out wrapper);

			// If we have not got a valid wrapper.
			if (wrapper == null)
			{
				if (type.IsEnum)
				{
					wrapper = this.enumWrapper;
				}

				// Enum?.
				else if (
					underlyingType != null
					&&
					underlyingType.IsEnum
					)
				{
					wrapper = this.enumNullableWrapper;
				}
			}

			#endregion


			#region Process value.

			// Common known type.
			if (wrapper != null)
			{
				this.serializeValue(value, sb, wrapper);
			}
			else
			{
				// Try to find type info entry for the current type.
				SerializeTypeInfoEntry typeInfoEntry;
				if (this.TypesData.TryGetValue(typeToProcess, out typeInfoEntry))
				{
					processObjectData(value, sb, typeInfoEntry);
				}
			}

			#endregion
		}


		/// <summary>
		/// Processes one value for serialization.
		/// </summary>
		/// <param name="value">Value to process. Cannot be null.</param>
		/// <param name="sb"><see cref="StringBuilder"/> to put data to.</param>
		/// <param name="wrapper">Possible common known type wrapper.</param>
		private void serializeValue(
			object value,
			StringBuilder sb,
			KnownTypeWrapper wrapper
			)
		{
			// NB.! Value cannot be null here.
			wrapper.SerializeFunction(value, sb);
			SerializeHelper.WriteTypeNumber(sb, ref wrapper.TypeNumber);
		}

		#endregion


		#region Serialize value functions.

		#region Common types.

		/// <summary>
		/// Serializes object type.
		/// </summary>
		void serializeValueObject(object value, StringBuilder sb)
		{
			// Do not append anything.
		}


		/// <summary>
		/// Serializes string type.
		/// </summary>
		void serializeValueString(object value, StringBuilder sb)
		{
			// NB.! We do not use SerializeHelper here to skip cloning string value at function call.
			sb.Append((string)value);
		}


		/// <summary>
		/// Serializes char type.
		/// </summary>
		void serializeValueChar(object value, StringBuilder sb)
		{
			SerializeHelper.WriteChar(sb, (char)value);
		}


		/// <summary>
		/// Serializes bool type.
		/// </summary>
		void serializeValueBool(object value, StringBuilder sb)
		{
			SerializeHelper.WriteBool(sb, (bool)value);
		}


		/// <summary>
		/// Serializes <see cref="Guid"/> type.
		/// </summary>
		void serializeValueGuid(object value, StringBuilder sb)
		{
			SerializeHelper.WriteGuid(sb, (Guid)value);
		}


		/// <summary>
		/// Serializes <see cref="TimeSpan"/> type.
		/// </summary>
		void serializeValueTimeSpan(object value, StringBuilder sb)
		{
			SerializeHelper.WriteInt64(sb, ((TimeSpan)value).Ticks);
		}


		/// <summary>
		/// Serializes <see cref="DateTime"/> type.
		/// </summary>
		void serializeValueDateTime(object value, StringBuilder sb)
		{
			SerializeHelper.WriteInt64(sb, ((DateTime)value).Ticks);
		}


		/// <summary>
		/// Serializes an enumerator type.
		/// </summary>
		void serializeValueEnum(object value, StringBuilder sb)
		{
			serializeValueInt32((int)value, sb);
		}


		/// <summary>
		/// Serializes a <see cref="System.Drawing.Color"/> type.
		/// </summary>
		void serializeValueColor(object value, StringBuilder sb)
		{
			SerializeHelper.WriteInt32(sb, ((Color)value).ToArgb());
		}
		
		#endregion


		#region Float.

		/// <summary>
		/// Serializes <see cref="float"/> type.
		/// </summary>
		void serializeValueFloat(object value, StringBuilder sb)
		{
			SerializeHelper.WriteFloat(sb, (float)value);
		}
		/// <summary>
		/// Serializes <see cref="float?"/> type.
		/// </summary>
		void serializeValueFloatNullable(object value, StringBuilder sb)
		{
			if (value != null)
				serializeValueFloat(value, sb);
		}

		#endregion


		#region Double.

		/// <summary>
		/// Serializes <see cref="double"/> type.
		/// </summary>
		void serializeValueDouble(object value, StringBuilder sb)
		{
			SerializeHelper.WriteDouble(sb, (double)value);
		}
		/// <summary>
		/// Serializes <see cref="double?"/> type.
		/// </summary>
		void serializeValueDoubleNullable(object value, StringBuilder sb)
		{
			if (value != null)
				serializeValueDouble(value, sb);
		}

		#endregion


		#region Decimal.

		/// <summary>
		/// Serializes <see cref="decimal"/> type.
		/// </summary>
		void serializeValueDecimal(object value, StringBuilder sb)
		{
			SerializeHelper.WriteDecimal(sb, (decimal)value);
		}
		/// <summary>
		/// Serializes <see cref="decimal?"/> type.
		/// </summary>
		void serializeValueDecimalNullable(object value, StringBuilder sb)
		{
			if (value != null)
				serializeValueDecimal(value, sb);
		}

		#endregion


		#region Byte.

		/// <summary>
		/// Serializes <see cref="Byte"/> type.
		/// </summary>
		void serializeValueByte(object value, StringBuilder sb)
		{
			SerializeHelper.WriteByte(sb, (byte)value);
		}
		/// <summary>
		/// Serializes <see cref="Byte?"/> type.
		/// </summary>
		void serializeValueByteNullable(object value, StringBuilder sb)
		{
			if (value != null)
				serializeValueByte(value, sb);
		}
		
		#endregion


		#region Int.

		/// <summary>
		/// Serializes <see cref="Int32"/> type.
		/// </summary>
		void serializeValueInt32(object value, StringBuilder sb)
		{
			SerializeHelper.WriteInt32(sb, (int)value);
		}
		/// <summary>
		/// Serializes <see cref="Int32?"/> type.
		/// </summary>
		void serializeValueInt32Nullable(object value, StringBuilder sb)
		{
			if (value != null)
				serializeValueInt32(value, sb);
		}


		/// <summary>
		/// Serializes <see cref="UInt32"/> type.
		/// </summary>
		void serializeValueUInt32(object value, StringBuilder sb)
		{
			SerializeHelper.WriteUInt32(sb, (uint)value);
		}
		/// <summary>
		/// Serializes <see cref="UInt32?"/> type.
		/// </summary>
		void serializeValueUInt32Nullable(object value, StringBuilder sb)
		{
			if (value != null)
				serializeValueUInt32(value, sb);
		}
		
		#endregion


		#region Long.

		/// <summary>
		/// Serializes <see cref="Int64"/> type.
		/// </summary>
		void serializeValueInt64(object value, StringBuilder sb)
		{
			SerializeHelper.WriteInt64(sb, (long)value);
		}
		/// <summary>
		/// Serializes <see cref="Int64?"/> type.
		/// </summary>
		void serializeValueInt64Nullable(object value, StringBuilder sb)
		{
			if (value != null)
				serializeValueInt64(value, sb);
		}


		/// <summary>
		/// Serializes <see cref="UInt64"/> type.
		/// </summary>
		void serializeValueUInt64(object value, StringBuilder sb)
		{
			SerializeHelper.WriteUInt64(sb, (ulong)value);
		}
		/// <summary>
		/// Serializes <see cref="UInt64?"/> type.
		/// </summary>
		void serializeValueUInt64Nullable(object value, StringBuilder sb)
		{
			if (value != null)
				serializeValueUInt64(value, sb);
		}

		#endregion


		#region Short.

		/// <summary>
		/// Serializes <see cref="Int16"/> type.
		/// </summary>
		void serializeValueInt16(object value, StringBuilder sb)
		{
			SerializeHelper.WriteInt16(sb, (short)value);
		}
		/// <summary>
		/// Serializes <see cref="Int16?"/> type.
		/// </summary>
		void serializeValueInt16Nullable(object value, StringBuilder sb)
		{
			if (value != null)
				serializeValueInt16(value, sb);
		}


		/// <summary>
		/// Serializes <see cref="UInt16"/> type.
		/// </summary>
		void serializeValueUInt16(object value, StringBuilder sb)
		{
			SerializeHelper.WriteUInt16(sb, (ushort)value);
		}
		/// <summary>
		/// Serializes <see cref="UInt16?"/> type.
		/// </summary>
		void serializeValueUInt16Nullable(object value, StringBuilder sb)
		{
			if (value != null)
				serializeValueUInt16(value, sb);
		}
		
		#endregion
		
		/**/
		#endregion



		#region writeCommonHeader().

		/// <summary>
		/// Writes common header of the serialization.
		/// </summary>
		/// <param name="sb"><see cref="StringBuilder"/> to put result to.</param>
		private void writeCommonHeader(StringBuilder sb)
		{
			// Append GUIDs.
			int counter = 0;


			foreach (KeyValuePair<Guid, int> pair in this.storedClassesGUIDs)
			// NB.! Use stored GUIDs to keep order of stored types at reading and writing common header.
			{
				// Possible separator.
				if (counter > 0) sb.Append(KTSerializer.WordSeparator); // to split classes info apart
				else counter++;

				// Append class GUID.
				sb.Append(pair.Key.ToString(KTSerializer.GuidFormat));

				// Class properties.
				List<Guid> propertyGUIDs = this.storedPropertiesGUIDs[pair.Key];
				// NB.! We do not use class entries, because they may be unparsed at this moment and we can loose position.
				int entriesCount = propertyGUIDs.Count;
				for (int i = 0; i < entriesCount; i++)
				{
					sb.Append(propertyGUIDs[i].ToString(KTSerializer.GuidFormat));
				}
			}
		}

		#endregion


		#region saveStringBuilderToStream().

		/// <summary>
		/// Saves <see cref="StringBuilder"/> to <see cref="Stream"/>.
		/// </summary>
		/// <param name="sb"><see cref="StringBuilder"/> to save.</param>
		/// <param name="stream"><see cref="Stream"/> to save to.</param>
		private static void saveStringBuilderToStream(
			StringBuilder sb,
			Stream stream
			)
		{
			int resultLength;
			byte[] sbBytes = EncodingHelper.GetUTF8Bytes(sb.ToString(), out resultLength);
			stream.Write(
				sbBytes,
				0,
				resultLength
				);

			// Reset position for future use.
			stream.Position = 0;
		}
		
		#endregion

		#endregion


		#region Deserialize object data.

		#region readDeserializationText().

		/// <summary>
		/// Reads deserialization text.
		/// </summary>
		private void readDeserializationText()
		{
			this.text = null; // by default

			// Read text.
			getTextFromStream(this.Stream, ref this.text, ref this.textLength);

			// Initial check.
			if (this.ObjectType == null) return;
		}
		
		#endregion


		#region restoreObjectData(), initial.

		/// <summary>
		/// Restores object's data from serialization.
		/// </summary>
		private void restoreObjectData()
		{
			// If we do not have a valid header, we do not restore properties.
			if (commonHeaderPosition > -1)
			{
				// If we have a special header stream, common header position is always at the end of deserialized text.
				if (this.CommonHeaderStream != null) commonHeaderPosition = this.textLength; // NB.! Without - 1, thus behind the text.

				// If we have stored any type info.
				if (commonHeaderPosition > SerializeHelper.TypeHeaderLength)
				{
					// Restore to type, stored in serialized text.
					int headerTypePosition = getKnownTypePosition(commonHeaderPosition);

					SerializeTypeInfoEntry currentTypeInfoEntry;

					unsafe
					{
						fixed (char* textPointerInitial = text)
						{
							// Pointer to header type position.
							char* headerTypePointer = textPointerInitial + headerTypePosition;

							// If we have a necessary class to restore to.
							if (getKnownTypeInfoEntry(
								SerializeHelper.ParseIntLength(ref headerTypePointer),
								out currentTypeInfoEntry
								))
							{
								// Some null object.
								if (headerTypePosition == 0)
								{
									this.ProcessedObject = null;
								}

								// Normal object.
								else
								{
									this.ProcessedObject = restoreObjectData(
										currentTypeInfoEntry,
										textPointerInitial,
										headerTypePointer
										);
								}
							}
						}
					}
				}
			}
		}

		#endregion


		#region restoreObjectDate(), object.

		/// <summary>
		/// Restores object's data from serialization.
		/// </summary>
		/// <param name="typeInfoEntry">Info entry of the type.</param>
		/// <param name="textStartPointer">Text start pointer.</param>
		/// <param name="typeHeaderPointer">Type header position pointer.</param>
		/// <returns>Object with restored properties.</returns>
		private unsafe object restoreObjectData(
			SerializeTypeInfoEntry typeInfoEntry,
			char* textStartPointer,
			char* typeHeaderPointer
			)
		{
			#region Initial actions.

			// NB.! At this place length is always > 0.

			// Create object initially.
			object o = typeInfoEntry.CreateInstance();

			// Call before deserialize function.
			typeInfoEntry.CallBeforeDeserialize(o);

			#endregion


			#region Restore object properties.

			// Read lengths of header with values lengths.
			typeHeaderPointer -= SerializeHelper.TypeHeaderLength; // NB.! The pointer value is diminished.
			int headerValuesLength = SerializeHelper.ParseIntLength(ref typeHeaderPointer);

			// If we have serialized any header information.
			if (headerValuesLength > 0)
			{
				#region Parse header.

				// Split to GUIDs properties numbers and lengths.
				int[] tempHeaderInfo = null;
				int tempHeaderInfoLength = 0;

				SerializeHelper.SplitUnsafe(
					typeHeaderPointer - headerValuesLength,
					headerValuesLength,
					ref tempHeaderInfo,
					ref tempHeaderInfoLength
					);

				#endregion


				#region Parse object text on basis of header info.

				for (int i = 0; i < tempHeaderInfoLength; i += 2)
				{
					#region Deserialize value.

					// Current value.
					int entryNumber = tempHeaderInfo[i];
					int length = tempHeaderInfo[i + 1];

					// If we do not have a property GUID in type to restore to, we do not consider it as an error and just do not do anything.
					SerializeIDEntry idEntry = null;
					if (entryNumber < typeInfoEntry.IdEntriesIntMappingArray.Length)
					{
						idEntry = typeInfoEntry.IdEntriesIntMappingArray[entryNumber];
					}


					// If we have found a valid entry.
					if (idEntry != null)
					{
						#region Get value.

						object valueToSet;

						// Some nullable value.
						if (length == 0)
						{
							// Null for nullable types, default() for others.
							valueToSet =
								!idEntry.HasUnderlyingType ?
								ObjectHelper.GetDefaultValue(idEntry.TypeToProcess) :
								null;
						}

						// Some not nullable value.
						else
						{
							#region Enumerator.

							if (idEntry.IsEnumType)
							{
								// Enum?.
								if (idEntry.IsEnumNullableType)
								{
									valueToSet =
										idEntry.KnownTypeWrapper
											.DeserializeUnderlyingTypeFunction(textStartPointer, ref length, idEntry.UnderlyingType);
								}
								// Enum.
								else
								{
									valueToSet = idEntry.KnownTypeWrapper.DeserializeFunction(textStartPointer, ref length);
								}
							}

							#endregion


							#region Some collection.

							else if (idEntry.HasCollectionType)
							{
								valueToSet = restoreCollectionProperty(
									textStartPointer,
									ref length,

									idEntry.CollectionEntry,

									idEntry.HasDictionaryType,
									idEntry.HasListType,
									idEntry.HasArrayType
									);
							}

							#endregion


							#region Common type.

							else
							{
								// NB.! Get stored type.
								valueToSet = deserializeUnknownTypeObject(
									ref textStartPointer,
									ref length,
									idEntry.TypeToProcess
									);
							}

							#endregion
						}

						#endregion


						#region Append value to the restored object.

						if (
							valueToSet != null
							||
							idEntry.GetValue(o) != null
							)
						{
							// Set value at last.
							idEntry.SetValue(o, valueToSet);
						}

						#endregion
					}

					#endregion


					#region Final actions.

					// Update counters.
					textStartPointer += length;

					#endregion
				}

				#endregion
			}

			#endregion


			#region Final actions.

			// Call after deserialize function.
			typeInfoEntry.CallAfterDeserialize(o);

			// Return restored object.
			return o;

			#endregion
		}

		#endregion


		#region restoreCollectionProperty().

		/// <summary>
		/// Processes one serialized text of a collection property/field.
		/// </summary>
		/// <param name="textStartPointer">Text start pointer.</param>
		/// <param name="textLength">Length of the text.</param>
		/// 
		/// <param name="collectionEntry">Possible collection entry to process.</param>
		/// 
		/// <param name="isDictionaryType">Is generic type a dictionary type.</param>
		/// <param name="isListType">Is generic type a list type.</param>
		/// <param name="isArrayType">Is type an array type.</param>
		/// 
		/// <returns>Deserialized collection or null, if no deserialization was possible.</returns>
		private unsafe object restoreCollectionProperty(
			char* textStartPointer,
			ref int textLength,

			SerializeCollectionEntry collectionEntry,

			bool isDictionaryType,
			bool isListType,
			bool isArrayType
			)
		{
			// NB.! We tell collections apart by included header. When there is no header, collection is considered null.

			if (textLength > 0)
			{
				#region Initial actions.

				object result = null;

				#endregion


				#region Parse collection length.

				// Derlare variables.
				int[] collectionLengths = null;
				int collectionLengthsCount = 0;

				// Get collection header.
				if (textLength > SerializeHelper.TypeHeaderLength) // if property value has written something except header length
				{
					// Get header length.
					char* textHeaderPointer = textStartPointer + textLength - SerializeHelper.TypeHeaderLength;
					int headerLength = SerializeHelper.ParseIntLength(ref textHeaderPointer);

					// Parse lengths.
					SerializeHelper.SplitUnsafe(
						textHeaderPointer - headerLength,
						headerLength,
						ref collectionLengths,
						ref collectionLengthsCount
						);
				}
				else
				{
					collectionLengths = new int[0];
				}

				#endregion


				// NB.! We do not check for header existance to tell apart null collections and empty collections.

				#region If it's a dictionary.

				if (isDictionaryType)
				{
					#region Initial actions.

					// Get collection entry.
					SerializeDictionaryEntry dictionaryEntry = (SerializeDictionaryEntry)collectionEntry;

					// Restore collection.
					IDictionary dictionary = (IDictionary)dictionaryEntry.CreateInstance(collectionLengthsCount / 2);
					result = dictionary;

					#endregion


					#region Restore keys and values.

					int keyLength, valueLength;

					for (int i = 0; i < collectionLengthsCount; i += 2)
					{
						#region Process key.

						// Restore key.
						keyLength = collectionLengths[i];
						object currentKey = null;

						// Some not nullable key.
						if (keyLength != 0) // is necessary when key type was not known at time of serialization
						{
							if (!dictionaryEntry.KeyItemEntry.HasCollectionType)
							{
								currentKey = deserializeUnknownTypeObject(
									ref textStartPointer,
									ref keyLength,
									dictionaryEntry.KeyItemEntry.TypeToProcess
									);
							}
							else
							{
								currentKey = restoreCollectionProperty(
									textStartPointer,
									ref keyLength,

									dictionaryEntry.KeyItemEntry.CollectionEntry,

									dictionaryEntry.KeyItemEntry.HasDictionaryType,
									dictionaryEntry.KeyItemEntry.HasListType,
									dictionaryEntry.KeyItemEntry.HasArrayType
									);
							}


							// Update counter.
							textStartPointer += keyLength;
						}

						#endregion


						#region Process value.

						// Restore value.
						valueLength = collectionLengths[i + 1];
						object currentValue = null;

						// Some not null value.
						if (valueLength != 0)
						{
							if (!dictionaryEntry.ValueItemEntry.HasCollectionType)
							{
								currentValue = deserializeUnknownTypeObject(
									ref textStartPointer,
									ref valueLength,
									dictionaryEntry.ValueItemEntry.TypeToProcess
									);
							}
							else
							{
								currentValue = restoreCollectionProperty(
									textStartPointer,
									ref valueLength,

									dictionaryEntry.ValueItemEntry.CollectionEntry,

									dictionaryEntry.ValueItemEntry.HasDictionaryType,
									dictionaryEntry.ValueItemEntry.HasListType,
									dictionaryEntry.ValueItemEntry.HasArrayType
									);
							}


							// Update counter.
							textStartPointer += valueLength;
						}

						#endregion


						#region Final actions.

						// Append to collection.
						if (currentKey != null)
							dictionary.Add(currentKey, currentValue);

						#endregion
					}

					#endregion
				}

				#endregion


				#region If it's a list.

				else if (isListType)
				{
					#region Restore collection.

					// Get collection entry.
					SerializeListEntry listEntry = (SerializeListEntry)collectionEntry;

					IList list = (IList)listEntry.CreateInstance(collectionLengthsCount);
					result = list;

					#endregion


					#region Restore values.

					int collectionLength;

					for (int i = 0; i < collectionLengthsCount; i++)
					{
						collectionLength = collectionLengths[i];

						// Some nullable value.
						if (collectionLength == 0)
						{
							list.Add(null);
						}

						// Some not nullable value.
						else
						{
							// Append value.
							if (!listEntry.ValueItemEntry.HasCollectionType)
							{
								list.Add(
									deserializeUnknownTypeObject(
										ref textStartPointer,
										ref collectionLength,
										listEntry.ValueItemEntry.TypeToProcess
										)
									);
							}

							else
							{
								list.Add(
									restoreCollectionProperty(
										textStartPointer,
										ref collectionLength,

										listEntry.ValueItemEntry.CollectionEntry,

										listEntry.ValueItemEntry.HasDictionaryType,
										listEntry.ValueItemEntry.HasListType,
										listEntry.ValueItemEntry.HasArrayType
										)
									);
							}

							// Update counters.
							textStartPointer += collectionLength;

						}
					}

					#endregion
				}

				#endregion


				#region If it's a array.

				else if (isArrayType)
				{
					#region Restore collection.

					// Get collection entry.
					SerializeArrayEntry arrayEntry = (SerializeArrayEntry)collectionEntry;

					Array array = (Array)arrayEntry.CreateInstance(collectionLengthsCount);
					result = array;

					#endregion


					#region Restore values.

					int collectionLength;

					for (int i = 0; i < collectionLengthsCount; i++)
					{
						collectionLength = collectionLengths[i];

						// Some nullable value.
						if (collectionLength == 0)
						{
							arrayEntry.SetValue(array, i, null);
						}

						// Some not nullable value.
						else
						{
							// Append value.
							if (!arrayEntry.ValueItemEntry.HasCollectionType)
							{
								arrayEntry.SetValue(array, i, deserializeUnknownTypeObject(
										ref textStartPointer,
										ref collectionLength,
										arrayEntry.ValueItemEntry.TypeToProcess
										)
									);
							}

							else
							{
								arrayEntry.SetValue(array, i, restoreCollectionProperty(
										textStartPointer,
										ref collectionLength,

										arrayEntry.ValueItemEntry.CollectionEntry,

										arrayEntry.ValueItemEntry.HasDictionaryType,
										arrayEntry.ValueItemEntry.HasListType,
										arrayEntry.ValueItemEntry.HasArrayType
										)
									);
							}

							// Update counters.
							textStartPointer += collectionLength;

						}
					}

					#endregion
				}

				#endregion


				#region Final actions.

				return result;

				#endregion
			}


			#region If there is no header included.

			else
			{
				return null;
			}

			#endregion

		}

		/**/
		#endregion



		#region getKnownTypeInfoEntry().

		/// <summary>
		/// Tries to get known type by its count in the collection of known serializable types.
		/// </summary>
		/// <param name="counter">Counter of the type to get.</param>
		/// <param name="entry">Known type info entry to set.</param>
		/// <returns>True if known type info entry was found, or false, when no such type entry exists.</returns>
		private bool getKnownTypeInfoEntry(int counter, out SerializeTypeInfoEntry entry)
		{
			// Try to get stored info.
			return this.storedClassesIntMapping.TryGetValue(counter, out entry);
		}
		
		#endregion


		#region getTextFromStream().

		/// <summary>
		/// Reads text array from stream. Stream is not closed after reading, its position is reset to 0.
		/// </summary>
		/// <param name="stream"><see cref="Stream"/> to read.</param>
		/// <param name="length">Length of the filled part of the array.</param>
		/// <param name="text">Text array to fill.</param>
		private static void getTextFromStream(Stream stream, ref char[] text, ref int length)
		{
			int size = (int)stream.Length;
			byte[] data = new byte[size];
			stream.Read(data, 0, size);
			text = EncodingHelper.GetUTF8String(data, out length);

			stream.Position = 0;
		}


		/// <summary>
		/// Reads text from stream. Stream is not closed after reading, its position is reset to 0.
		/// </summary>
		/// <param name="stream"><see cref="Stream"/> to read.</param>
		/// <param name="text">Text to fill.</param>
		private static void getTextFromStream(Stream stream, ref string text)
		{
			int size = (int)stream.Length;
			byte[] data = new byte[size];
			stream.Read(data, 0, size);
			text = EncodingHelper.GetUTF8String(data);

			stream.Position = 0;
		}

		/**/
		#endregion



		#region Deserialize type functions.

		#region deserializeUnknownTypeObject().

		/// <summary>
		/// Deserializes object of unknown type.
		/// </summary>
		/// <param name="textStartPointer">Text start pointer.</param>
		/// <param name="textLength">Length of the object text.</param>
		/// <param name="typeToProcess">Desired type to restore to.</param>
		private unsafe object deserializeUnknownTypeObject(
			ref char* textStartPointer,
			ref int textLength,
			Type typeToProcess
			)
		{
			#region Read stored info.

			char* valueNumberPointer = textStartPointer + textLength - SerializeHelper.TypeHeaderLength;
			int typeNumber = SerializeHelper.ParseIntLength(ref valueNumberPointer);
			
			#endregion


			#region If this is a serialized known type (that is, type that was marked as serialized).

			if (typeNumber > KTSerializer.CommonKnownTypesMaxPossibleNumber)
			{
				// Search for the known serialized type entry on basis of the stored type info.
				SerializeTypeInfoEntry currentTypeInfoEntry;

				// If we have a known type info with a necessary attribute and ID, we try to deserialize to it.
				if (getKnownTypeInfoEntry(
					typeNumber,
					out currentTypeInfoEntry))
				{
					return restoreObjectData(
						currentTypeInfoEntry,
						textStartPointer,
						valueNumberPointer
						);
				}
				// Otherwise, we do not restore.
				else
				{
					return ObjectHelper.GetDefaultValue(typeToProcess);
				}
			}

			#endregion


			#region A common primitive type.

			else
			{
				// Get known type on basis of stored info and deserialize to this type.
				object valueToSet =
					this.commonKnownWrappersMapping[typeNumber]
						.DeserializeFunction(textStartPointer, ref textLength);

				// Possibly convert to the necessary type.
				if (
					valueToSet != null
					&&
					valueToSet.GetType() != typeToProcess
					&&
					valueToSet is IConvertible
					&&
					!typeToProcess.IsEnum
					)
				{
					try
					{
						valueToSet = Convert.ChangeType(valueToSet, typeToProcess);
					}
					catch
					{
						valueToSet = ObjectHelper.GetDefaultValue(typeToProcess);
						// NB.! If we failed to convert value, we do not fail, but return a default value instead to have other values restored.
					}
				}

				return valueToSet;
			}

			#endregion
		}

		#endregion


		#region Deserialize value functions.
		
		/// <summary>
		/// Deserializes object type.
		/// </summary>
		private unsafe object deserializeValueObject(char* textPointer, ref int textLength)
		{
			return new object();
		}


		/// <summary>
		/// Deserializes string type.
		/// </summary>
		private unsafe object deserializeValueString(char* textPointer, ref int textLength)
		{
			if (textLength == SerializeHelper.TypeHeaderLength) return ""; // if we have just a type header
			else return getObjectText(ref textPointer, ref textLength);
		}


		/// <summary>
		/// Deserializes char type.
		/// </summary>
		private unsafe object deserializeValueChar(char* textPointer, ref int textLength)
		{
			return *textPointer;
		}


		/// <summary>
		/// Deserializes <see cref="Int32"/> type.
		/// </summary>
		private unsafe object deserializeValueInt32(char* textPointer, ref int textLength)
		{
			return SerializeHelper.ParseInt(ref textPointer);
		}


		/// <summary>
		/// Deserializes <see cref="Int16"/> type.
		/// </summary>
		private unsafe object deserializeValueInt16(char* textPointer, ref int textLength)
		{
			return SerializeHelper.ParseInt16(ref textPointer);
		}


		/// <summary>
		/// Deserializes <see cref="Int64"/> type.
		/// </summary>
		private unsafe object deserializeValueInt64(char* textPointer, ref int textLength)
		{
			return SerializeHelper.ParseInt64(ref textPointer);
		}


		/// <summary>
		/// Deserializes <see cref="float"/> type.
		/// </summary>
		private unsafe object deserializeValueFloat(char* textPointer, ref int textLength)
		{
			return SerializeHelper.ParseFloat(ref textPointer);
		}


		/// <summary>
		/// Deserializes <see cref="double"/> type.
		/// </summary>
		private unsafe object deserializeValueDouble(char* textPointer, ref int textLength)
		{
			return SerializeHelper.ParseDouble(ref textPointer);
		}


		/// <summary>
		/// Deserializes <see cref="decimal"/> type.
		/// </summary>
		private unsafe object deserializeValueDecimal(char* textPointer, ref int textLength)
		{
			return SerializeHelper.ParseDecimal(ref textPointer);
		}


		/// <summary>
		/// Deserializes <see cref="bool"/> type.
		/// </summary>
		private unsafe object deserializeValueBool(char* textPointer, ref int textLength)
		{
			return (*textPointer == 49); // '1'
		}


		/// <summary>
		/// Deserializes <see cref="UInt32"/> type.
		/// </summary>
		private unsafe object deserializeValueUInt32(char* textPointer, ref int textLength)
		{
			return SerializeHelper.ParseUInt(ref textPointer);
		}


		/// <summary>
		/// Deserializes <see cref="UInt16"/> type.
		/// </summary>
		private unsafe object deserializeValueUInt16(char* textPointer, ref int textLength)
		{
			return SerializeHelper.ParseUInt16(ref textPointer);
		}


		/// <summary>
		/// Deserializes <see cref="UInt64"/> type.
		/// </summary>
		private unsafe object deserializeValueUInt64(char* textPointer, ref int textLength)
		{
			return SerializeHelper.ParseUInt64(ref textPointer);
		}


		/// <summary>
		/// Deserializes <see cref="Byte"/> type.
		/// </summary>
		private unsafe object deserializeValueByte(char* textPointer, ref int textLength)
		{
			return SerializeHelper.ParseByte(ref textPointer);
		}


		/// <summary>
		/// Deserializes <see cref="Guid"/> type.
		/// </summary>
		private unsafe object deserializeValueGuid(char* textPointer, ref int textLength)
		{
			return SerializeHelper.ParseGuid(ref textPointer);
		}


		/// <summary>
		/// Deserializes <see cref="DateTime"/> type.
		/// </summary>
		private unsafe object deserializeValueDateTime(char* textPointer, ref int textLength)
		{
			return new DateTime(
				SerializeHelper.ParseInt64(ref textPointer),
				DateTimeKind.Local); // not sure about local time
		}


		/// <summary>
		/// Deserializes <see cref="TimeSpan"/> type.
		/// </summary>
		private unsafe object deserializeValueTimeSpan(char* textPointer, ref int textLength)
		{
			return TimeSpan.FromTicks(
				SerializeHelper.ParseInt64(ref textPointer)
				);
		}


		/// <summary>
		/// Deserializes an enumerable type.
		/// </summary>
		private unsafe object deserializeValueEnum(char* textPointer, ref int textLength)
		{
			return SerializeHelper.ParseInt(ref textPointer);
		}


		/// <summary>
		/// Deserializes a <see cref="System.Drawing.Color"/> type.
		/// </summary>
		private unsafe object deserializeValueColor(char* textPointer, ref int textLength)
		{
			return Color.FromArgb(
				SerializeHelper.ParseInt(ref textPointer)
				);
		}

		/**/
		#endregion

		#endregion


		#region getKnownTypePosition().

		/// <summary>
		/// Gets known type position.
		/// </summary>
		/// <param name="textEnd">End text position to use.</param>
		/// <returns>Known type position.</returns>
		private static int getKnownTypePosition(int textEnd)
		{
			return textEnd - SerializeHelper.TypeHeaderLength;
		}

		#endregion


		#region getObjectText().

		/// <summary>
		/// Gets text of the object on basis of it's postion in the total serialized text.
		/// </summary>
		/// <param name="textStart">Text start position.</param>
		/// <param name="textLength">Full length of the object text to get.</param>
		/// <returns>Object text.</returns>
		private unsafe string getObjectText(ref char* textPointer, ref int textLength)
		{
			return new string(textPointer, 0, textLength - SerializeHelper.TypeHeaderLength);
		}
		
		#endregion

		#endregion


		#region Create known type wrappers.

		#region Wrapper collections.

		/// <summary>
		/// Collection of common known type wrappers.
		/// </summary>
		private Dictionary<Type, KnownTypeWrapper> commonKnownWrappers;

		/// <summary>
		/// Mapping of integer representation of IDs of common types, known to the serializer.
		/// </summary>
		private KnownTypeWrapper[] commonKnownWrappersMapping;

		/// <summary>
		/// Wrapper gor <see cref="Enum"/> types.
		/// </summary>
		private KnownTypeWrapper enumWrapper;

		/// <summary>
		/// Wrapper gor <see cref="Enum?"/> types.
		/// </summary>
		private KnownTypeWrapper enumNullableWrapper;

		#endregion


		#region createKnownTypeWrappers().

		/// <summary>
		/// Creates collections of known type wrappers.
		/// </summary>
		private unsafe void createKnownTypeWrappers()
		{
			// Initiate collections.
			commonKnownWrappers = new Dictionary<Type, KnownTypeWrapper>();
			commonKnownWrappersMapping = new KnownTypeWrapper[KTSerializer.CommonKnownTypesMaxNumber + 1];


			// Create wrappers.
			createKnownTypeWrapper(ObjectTypes.Object, serializeValueObject, deserializeValueObject);
			createKnownTypeWrapper(ObjectTypes.String, serializeValueString, deserializeValueString);
			createKnownTypeWrapper(ObjectTypes.Char, serializeValueChar, deserializeValueChar);

			createKnownTypeWrapper(ObjectTypes.Int32, serializeValueInt32, deserializeValueInt32);
			createKnownTypeWrapper(ObjectTypes.Int32Nullable, serializeValueInt32Nullable, deserializeValueInt32);

			createKnownTypeWrapper(ObjectTypes.Int64, serializeValueInt64, deserializeValueInt64);
			createKnownTypeWrapper(ObjectTypes.Int64Nullable, serializeValueInt64Nullable, deserializeValueInt64);

			createKnownTypeWrapper(ObjectTypes.Int16, serializeValueInt16, deserializeValueInt16);
			createKnownTypeWrapper(ObjectTypes.Int16Nullable, serializeValueInt16Nullable, deserializeValueInt16);

			createKnownTypeWrapper(ObjectTypes.UInt32, serializeValueUInt32, deserializeValueUInt32);
			createKnownTypeWrapper(ObjectTypes.UInt32Nullable, serializeValueUInt32Nullable, deserializeValueUInt32);

			createKnownTypeWrapper(ObjectTypes.UInt64, serializeValueUInt64, deserializeValueUInt64);
			createKnownTypeWrapper(ObjectTypes.UInt64Nullable, serializeValueUInt64Nullable, deserializeValueUInt64);

			createKnownTypeWrapper(ObjectTypes.UInt16, serializeValueUInt16, deserializeValueUInt16);
			createKnownTypeWrapper(ObjectTypes.UInt16Nullable, serializeValueUInt16Nullable, deserializeValueUInt16);

			createKnownTypeWrapper(ObjectTypes.Float, serializeValueFloat, deserializeValueFloat);
			createKnownTypeWrapper(ObjectTypes.FloatNullable, serializeValueFloatNullable, deserializeValueFloat);

			createKnownTypeWrapper(ObjectTypes.Double, serializeValueDouble, deserializeValueDouble);
			createKnownTypeWrapper(ObjectTypes.DoubleNullable, serializeValueDoubleNullable, deserializeValueDouble);

			createKnownTypeWrapper(ObjectTypes.Decimal, serializeValueDecimal, deserializeValueDecimal);
			createKnownTypeWrapper(ObjectTypes.DecimalNullable, serializeValueDecimalNullable, deserializeValueDecimal);

			createKnownTypeWrapper(ObjectTypes.Byte, serializeValueByte, deserializeValueByte);
			createKnownTypeWrapper(ObjectTypes.ByteNullable, serializeValueByteNullable, deserializeValueByte);

			createKnownTypeWrapper(ObjectTypes.Boolean, serializeValueBool, deserializeValueBool);
			createKnownTypeWrapper(ObjectTypes.BooleanNullable, serializeValueBool, deserializeValueBool);

			createKnownTypeWrapper(ObjectTypes.Guid, serializeValueGuid, deserializeValueGuid);
			createKnownTypeWrapper(ObjectTypes.GuidNullable, serializeValueGuid, deserializeValueGuid);

			createKnownTypeWrapper(ObjectTypes.TimeSpan, serializeValueTimeSpan, deserializeValueTimeSpan);
			createKnownTypeWrapper(ObjectTypes.TimeSpanNullable, serializeValueTimeSpan, deserializeValueTimeSpan);

			createKnownTypeWrapper(ObjectTypes.DateTime, serializeValueDateTime, deserializeValueDateTime);
			createKnownTypeWrapper(ObjectTypes.DateTimeNullable, serializeValueDateTime, deserializeValueDateTime);

			createKnownTypeWrapper(ObjectTypes.Color, serializeValueColor, deserializeValueColor);
			createKnownTypeWrapper(ObjectTypes.ColorNullable, serializeValueColor, deserializeValueColor);


			// Enums.
			this.enumWrapper = new KnownTypeWrapper()
			{
				Type = null,
				SerializeFunction = serializeValueEnum,
				DeserializeFunction = deserializeValueEnum,
				TypeNumber = KTSerializer.Int32TypeNumber
			};

			this.enumNullableWrapper = new KnownTypeWrapper()
			{
				Type = null,
				SerializeFunction = serializeValueEnum, // NB.! Not serializeValueEnumNullable.
				DeserializeUnderlyingTypeFunction = (char* textPointer, ref int textLength, Type underlyingType) =>
				{
					if (textLength == 0) return null;
					else return Enum.ToObject(underlyingType, SerializeHelper.ParseInt(ref textPointer));
				},
				TypeNumber = KTSerializer.Int32NullableTypeNumber
			};
		}
		
		#endregion


		#region createKnownTypeWrapper().

		/// <summary>
		/// Creates one known type wrapper.
		/// </summary>
		/// <param name="type">Type of the wrapper.</param>
		/// <param name="serializeFunction">Serialization function of the wrapper.</param>
		/// <param name="deserializeFunction">Deserialization function of the wrapper.</param>
		/// <param name="valueLength">Length of the wrapper type value (in characters).</param>
		private void createKnownTypeWrapper(
			Type type,
			SerializeValueDelegate serializeFunction,
			DeserializeValueDelegate deserializeFunction
			)
		{
			// Create wrapper.
			KnownTypeWrapper wrapper = new KnownTypeWrapper()
			{
				Type = type,
				SerializeFunction = serializeFunction,
				DeserializeFunction = deserializeFunction,
				TypeNumber = KTSerializer.GetCommonKnownTypeNumber(type),
			};

			// Add to necessary collections.
			commonKnownWrappers.Add(type, wrapper);
			commonKnownWrappersMapping[wrapper.TypeNumber] = wrapper;
		}

		#endregion

		#endregion

		#endregion

	}


	#region Delegates.

	/// <summary>
	/// Delegate for a serialization function.
	/// </summary>
	/// <param name="value">Value to process.</param>
	/// <param name="sb"><see cref="StringBuilder"/> to put data to.</param>
	/// <returns>True if type header should be written, false otherwise.</returns>
	internal delegate void SerializeValueDelegate(
			object value,
			StringBuilder sb
			);

	
	/// <summary>
	/// Delegate for a deserialization function.
	/// </summary>
	/// <param name="textLength">Length of the text.</param>
	/// <param name="type">Type to parse to.</param>
	/// <returns>Parsed object.</returns>
	internal unsafe delegate object DeserializeValueDelegate(
			char* textStartPointer,
			ref int textLength
			);


	/// <summary>
	/// Delegate for a deserialization function with an underlying type.
	/// </summary>
	/// <param name="textStartPointer">Text start pointer.</param>
	/// <param name="textLength">Length of the text.</param>
	/// <param name="type">Type to parse to.</param>
	/// <param name="underlyingType">Possible underlying type when type is a nullable one.</param>
	/// <returns>Parsed object.</returns>
	internal unsafe delegate object DeserializeValueUnderlyingTypeDelegate(
			char* textStartPointer,
			ref int textLength,
			Type underlyingType
			);

	#endregion


	#region KnownTypeWrapper.

	/// <summary>
	/// Wrapper for the known types.
	/// </summary>
	internal class KnownTypeWrapper
	{
		/// <summary>
		/// Stored type.
		/// </summary>
		public Type Type;

		/// <summary>
		/// Type number.
		/// </summary>
		public uint TypeNumber;

		/// <summary>
		/// Serialize value function.
		/// </summary>
		public SerializeValueDelegate SerializeFunction;


		/// <summary>
		/// Deserialize value function.
		/// </summary>
		public DeserializeValueDelegate DeserializeFunction;

		/// <summary>
		/// Deserialize value with an underlying type function.
		/// </summary>
		public DeserializeValueUnderlyingTypeDelegate DeserializeUnderlyingTypeFunction;
	}

	#endregion
}
