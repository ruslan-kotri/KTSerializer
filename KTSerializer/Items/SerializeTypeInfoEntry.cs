using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace KT.Common.Classes.Application
{
	/// <summary>
	/// One entry with info about processed type.
	/// </summary>
	internal class SerializeTypeInfoEntry
	{
		#region Public properties.

		#region IdEntries collections.

		/// <summary>
		/// Collection of entries describing the necessary properties and fields of the type.
		/// </summary>
		public List<SerializeIDEntry> IdEntries = new List<SerializeIDEntry>();

		/// <summary>
		/// Array of entries describing the necessary properties and fields of the type. Same as <see cref="IdEntries"/>, in array form for speed.
		/// </summary>
		public SerializeIDEntry[] IdEntriesArray;


		/// <summary>
		/// Collection of mapping of entries describing the necessary properties and fields of the type.
		/// </summary>
		public Dictionary<Guid, SerializeIDEntry> IdEntriesMapping = new Dictionary<Guid, SerializeIDEntry>();

		/// <summary>
		/// Private. Collection of mapping of entry GUID in string form - entry describing the necessary properties and fields of the type.
		/// </summary>
		private Dictionary<string, SerializeIDEntry> idEntriesCodeMapping = new Dictionary<string, SerializeIDEntry>();


		/// <summary>
		/// Collection of mapping of entry GUID's count in stored GUIDs - entry describing the necessary properties and fields of the type.
		/// </summary>
		public Dictionary<int, SerializeIDEntry> IdEntriesIntMapping = new Dictionary<int, SerializeIDEntry>();

		/// <summary>
		/// Array of mapping of entry GUID's count in stored GUIDs - entry describing the necessary properties and fields of the type.
		/// </summary>
		public SerializeIDEntry[] IdEntriesIntMappingArray;


		/// <summary>
		/// Collection of <see cref="Guid"/> of the known property, stored in the serialized text - its counter.
		/// </summary>
		public Dictionary<Guid, int> StoredPropertiesGUIDs = new Dictionary<Guid, int>();

		#endregion


		#region Type, class attribute.

		/// <summary>
		/// Type of the entry.
		/// </summary>
		public Type Type;

		/// <summary>
		/// Class serialization attribute of current type.
		/// </summary>
		public KTSerializeAttribute ClassAttribute;

		/// <summary>
		/// Private. Start number of current type's GUID in the total collection of gathered classes GUIDs.
		/// </summary>
		public int ClassGuidStartCount;

		#endregion


		#region Before/After (de)serialize.

		/// <summary>
		/// Functions to call before serialization.
		/// </summary>
		private List<Action<object>> BeforeSerialize = new List<Action<object>>();

		/// <summary>
		/// Functions to call after serialization.
		/// </summary>
		private List<Action<object>> AfterSerialize = new List<Action<object>>();

		/// <summary>
		/// Functions to call before deserialization.
		/// </summary>
		private List<Action<object>> BeforeDeserialize = new List<Action<object>>();

		/// <summary>
		/// Functions to call after deserialization.
		/// </summary>
		private List<Action<object>> AfterDeserialize = new List<Action<object>>();

		#endregion

		#endregion


		#region Public functions.

		#region AddIdEntry().

		/// <summary>
		/// Adds entry to the collection of property/fields entries.
		/// </summary>
		/// <param name="idEntry">Entry to add.</param>
		public void AddIdEntry(SerializeIDEntry idEntry)
		{
			#region Initial check.

			if (this.idEntriesCodeMapping.ContainsKey(idEntry.IncludeAttribute.FieldIDCode))
			{
				throw new KTSerializeAttributeException(String.Format(
					Properties.Resources.Error_AttributeGuidIsNotUnique,
					idEntry.IncludeAttribute.FieldID
					));
			}

			#endregion


			#region Add to common entries collections.

			this.IdEntries.Add(idEntry);
			this.IdEntriesMapping.Add(idEntry.IncludeAttribute.FieldID, idEntry);
			this.idEntriesCodeMapping.Add(idEntry.IncludeAttribute.FieldIDCode, idEntry);

			#endregion
		}
		
		#endregion


		#region SetIdEntriesNumbers().

		/// <summary>
		/// Sets numbers of property/fields entries.
		/// </summary>
		/// <param name="storedPropertiesList">Possible collection of stored properties <see cref="GUID"/>s.</param>
		public void SetIdEntriesNumbers(List<Guid> storedPropertiesList)
		{
			#region Set stored numbers.

			if (storedPropertiesList != null)
			{
				for (int i = 0; i < storedPropertiesList.Count; i++)
				{
					this.StoredPropertiesGUIDs.Add(storedPropertiesList[i], i);
				}
			}
			
			#endregion


			#region Set numbers of entries.

			int countTo = this.IdEntries.Count;
			for (int i = 0; i < countTo; i++)
			{
				// Current entry.
				SerializeIDEntry idEntry = this.IdEntries[i];

				// Try to get counter from stored info.
				int counter = 0;
				if (this.StoredPropertiesGUIDs.TryGetValue(idEntry.IncludeAttribute.FieldID, out counter))
				{
					idEntry.EntryNumber = counter;
				}
				// Otherwise, add property info to stored properties collections.
				else
				{
					idEntry.EntryNumber = this.StoredPropertiesGUIDs.Count; // it's a length, thus is bigger than the biggest counter
					this.StoredPropertiesGUIDs.Add(idEntry.IncludeAttribute.FieldID, idEntry.EntryNumber);
				}

				// Add to mapping.
				this.IdEntriesIntMapping.Add(idEntry.EntryNumber, idEntry);
			}
			
			#endregion


			#region Final actions.

			// Reset collection with glance to entries number.
			this.IdEntries = this.IdEntries.OrderBy(s => s.EntryNumber).ToList();
			this.IdEntriesArray = this.IdEntries.ToArray();
			

			// Set array of mapping.
			if (countTo == 0)
			{
				this.IdEntriesIntMappingArray = new SerializeIDEntry[0];
			}
			else
			{
				this.IdEntriesIntMappingArray = new SerializeIDEntry[this.IdEntriesArray[countTo - 1].EntryNumber + 1];
				for (int i = 0; i < countTo; i++)
				{
					SerializeIDEntry idEntry = this.IdEntriesArray[i];
					this.IdEntriesIntMappingArray[idEntry.EntryNumber] = idEntry;
				}
			}

			#endregion
		}
		
		#endregion


		#region CreateInstance().

		/// <summary>
		/// Creates new instance of the type of stored info of the entry.
		/// </summary>
		/// <returns>New instance of the entry type.</returns>
		public object CreateInstance()
		{
			return constructor();
		}

		#endregion


		#region Before/after (de)serialize().

		#region Create handlers.

		/// <summary>
		/// Adds method to handlers to call before serilization.
		/// </summary>
		/// <param name="method">Method to add.</param>
		public void AddBeforeSerialize(MethodInfo method)
		{
			this.BeforeSerialize.Add(createBeforeAfterDelegate(method));
		}

		/// <summary>
		/// Adds method to handlers to call after serilization.
		/// </summary>
		/// <param name="method">Method to add.</param>
		public void AddAfterSerialize(MethodInfo method)
		{
			this.AfterSerialize.Add(createBeforeAfterDelegate(method));
		}


		/// <summary>
		/// Adds method to handlers to call before deserilization.
		/// </summary>
		/// <param name="method">Method to add.</param>
		public void AddBeforeDeserialize(MethodInfo method)
		{
			this.BeforeDeserialize.Add(createBeforeAfterDelegate(method));
		}

		/// <summary>
		/// Adds method to handlers to call after deserilization.
		/// </summary>
		/// <param name="method">Method to add.</param>
		public void AddAfterDeserialize(MethodInfo method)
		{
			this.AfterDeserialize.Add(createBeforeAfterDelegate(method));
		}
		
		#endregion


		#region Call handlers.

		/// <summary>
		/// Processes handlers before serialization.
		/// </summary>
		/// <param name="value">Object to call handlers upon.</param>
		public void CallBeforeSerialize(object value)
		{
			if (this.BeforeSerialize.Count > 0)
			{
				for (int i = this.BeforeSerialize.Count - 1; i >= 0; i--)
				{
					this.BeforeSerialize[i](value);
				}
			}
		}


		/// <summary>
		/// Processes handlers after serialization.
		/// </summary>
		/// <param name="value">Object to call handlers upon.</param>
		public void CallAfterSerialize(object value)
		{
			if (this.AfterSerialize.Count > 0)
			{
				for (int i = this.AfterSerialize.Count - 1; i >= 0; i--)
				{
					this.AfterSerialize[i](value);
				}
			}
		}


		/// <summary>
		/// Processes handlers before deserialization.
		/// </summary>
		/// <param name="value">Object to call handlers upon.</param>
		public void CallBeforeDeserialize(object value)
		{
			if (this.BeforeDeserialize.Count > 0)
			{
				for (int i = this.BeforeDeserialize.Count - 1; i >= 0; i--)
				{
					this.BeforeDeserialize[i](value);
				}
			}
		}


		/// <summary>
		/// Processes handlers after deserialization.
		/// </summary>
		/// <param name="value">Object to call handlers upon.</param>
		public void CallAfterDeserialize(object value)
		{
			if (this.AfterDeserialize.Count > 0)
			{
				for (int i = this.AfterDeserialize.Count - 1; i >= 0; i--)
				{
					this.AfterDeserialize[i](value);
				}
			}
		}
		
		#endregion


		#region CopyBeforeAfterHandlers().

		/// <summary>
		/// Copies before/after (de)serialization handlers to the given entry.
		/// </summary>
		/// <param name="newEntry">Entry to copy handlers to.</param>
		public void CopyBeforeAfterHandlers(SerializeTypeInfoEntry newEntry)
		{
			newEntry.BeforeSerialize.AddRange(this.BeforeSerialize);
			newEntry.AfterSerialize.AddRange(this.AfterSerialize);
			newEntry.BeforeDeserialize.AddRange(this.BeforeDeserialize);
			newEntry.AfterDeserialize.AddRange(this.AfterDeserialize);
		}
		
		#endregion

		#endregion


		#region ToString().

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents the current <see cref="System.Object"/>.
		/// </summary>
		/// <returns>A <see cref="System.String"/> that represents the current <see cref="System.Object"/>.</returns>
		public override string ToString()
		{
			return
				this.Type.Name + ", " +
				this.IdEntries.Count;
		}
		
		#endregion

		#endregion


		#region Auxiliary functions.

		#region createConstructor().

		/// <summary>
		/// Delegate to create type instance.
		/// </summary>
		private Func<object> constructor;

		/// <summary>
		/// Creates constructor delegate.
		/// </summary>
		public void createConstructor()
		{
			// Some end class.
			if (!this.Type.IsAbstract)
			{
				// Get defined constructor.
				ConstructorInfo constructorInfo = this.Type.GetConstructor(
					BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
					null, Type.EmptyTypes, null
					);

				// If we do not have a constructor.
				if (constructorInfo == null)
				{
					// Structure.
					// NB.! It's not always convenient to have a constructor for stuctures, so we allow to not to define one.
					if (this.Type.IsValueType)
					{
						constructor = () => { return Activator.CreateInstance(this.Type, true); };
						return;
					}

					// A reference type.
					else
					{
						throw new KTSerializeException(String.Format(
							Properties.Resources.Error_TypeHasNoConstructor,
							this.Type.Name
							));
					}
				}


				// Create delegate.
				DynamicMethod dynamicMethod = new DynamicMethod(
					"InstantiateObject",
					MethodAttributes.Static | MethodAttributes.Public,
					CallingConventions.Standard, ObjectTypes.Object,
					null, this.Type, true);

				ILGenerator generator = dynamicMethod.GetILGenerator();
				generator.Emit(OpCodes.Newobj, constructorInfo);
				generator.Emit(OpCodes.Ret);

				constructor = (Func<object>)dynamicMethod.CreateDelegate(typeof(Func<object>));


				#region Is rewritten.

				// Works, but is a little slower than version above.
				/*NewExpression newExpression = Expression.New(constructorInfo);
				constructor = Expression.Lambda<Func<object>>(newExpression).Compile();
				*/

				#endregion
			}

			// We do not need constructors for abstract classes.
			else
			{
				constructor = null;
			}
		}

		#endregion


		#region createBeforeAfterDelegate().

		/// <summary>
		/// Creates before/after (de)serialization handler on basis of provided method.
		/// </summary>
		/// <param name="method">Method to create handler upon.</param>
		/// <returns>Compiled delegate.</returns>
		private Action<object> createBeforeAfterDelegate(MethodInfo method)
		{
			ParameterExpression instance = Expression.Parameter(ObjectTypes.Object, "t");
			MethodCallExpression methodCall = Expression.Call(
				Expression.Convert(instance, method.DeclaringType),
				method
				);

			return Expression.Lambda<Action<object>>(methodCall, instance).Compile();
		}
		
		#endregion

		#endregion
	}
}
