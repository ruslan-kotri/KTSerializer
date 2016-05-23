using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;

namespace KT.Common.Classes.Application
{
	/// <summary>
	/// Serializes and deserializes objects and graphs of objects to binary format.
	/// </summary>
	public sealed class KTSerializer
	{
		#region Properties.

		#region Text indicators and separators.

		/// <summary>
		/// Separator of lines it a stored file.
		/// </summary>
		internal static char LineSeparator = '\x1A'; // 26, substitute character
		/// <summary>
		/// Separator of words it a stored line.
		/// </summary>
		internal const char WordSeparator = '\x16'; // 22, synchronous idle

		#endregion


		#region Formats.

		/// <summary>
		/// Format for a string representation of a <see cref="Guid"/>.
		/// </summary>
		internal const string GuidFormat = "N"; // no hyphens

		#endregion


		#region References to common variables.

		/// <summary>
		/// <see cref="Encoding"/> to use in operations.
		/// </summary>
		internal static readonly Encoding Encoding = Encoding.UTF8;

		#endregion



		#region Collection types.

		/// <summary>
		/// Type of any dictionary.
		/// </summary>
		internal static Type DictionaryType = typeof(Dictionary<,>);
		// NB.! IDictionary is inappropriate here.

		/// <summary>
		/// Type of any list.
		/// </summary>
		internal static Type ListType = typeof(List<>);
		// NB.! IList is inappropriate here.

		// NB.! HashSet<> does not implement any not generic interfaces that allow to add elements, so it's impossible to restore to HashSet<T> with unknown T.

		#endregion


		#region CommonKnownTypes.

		/// <summary>
		/// Common types, known to the serializer.
		/// </summary>
		internal static Dictionary<Type, uint> CommonKnownTypes;

		/// <summary>
		/// Headers of IDs of common types, known to the serializer.
		/// </summary>
		private static Dictionary<Type, string> commonKnownTypesHeaders;

		/// <summary>
		/// Max number in common types, known to the serializer.
		/// </summary>
		internal static uint CommonKnownTypesMaxNumber;

		/// <summary>
		/// Max possible number in common types, known to the serializer.
		/// </summary>
		internal static int CommonKnownTypesMaxPossibleNumber = 127;


		/// <summary>
		/// Number of <see cref="Int32"/> type.
		/// </summary>
		internal static uint Int32TypeNumber;

		/// <summary>
		/// Number of <see cref="Int32?"/> type.
		/// </summary>
		internal static uint Int32NullableTypeNumber;

		#endregion


		#region KnownTypes.

		/// <summary>
		/// Types, known to the serializer.
		/// </summary>
		internal List<Type> KnownTypes;

		/// <summary>
		/// Mapping of types, known to the serializer.
		/// </summary>
		internal Dictionary<Type, object> KnownTypesMapping;

		#endregion


		#region Attributes types.

		/// <summary>
		/// Type of the <see cref="KTSerializeAttribute"/>.
		/// </summary>
		internal static readonly Type SerializeAttributeType = typeof(KTSerializeAttribute);

		/// <summary>
		/// Type of the <see cref="KTSerializeIncludeAttribute"/>.
		/// </summary>
		internal static readonly Type IncludeAttributeType = typeof(KTSerializeIncludeAttribute);


		/// <summary>
		/// Type of the <see cref="KTBeforeSerializeAttribute"/>.
		/// </summary>
		internal static readonly Type BeforeSerializeAttributeType = typeof(KTBeforeSerializeAttribute);
		/// <summary>
		/// Type of the <see cref="KTAfterSerializeAttribute"/>.
		/// </summary>
		internal static readonly Type AfterSerializeAttributeType = typeof(KTAfterSerializeAttribute);

		/// <summary>
		/// Type of the <see cref="KTBeforeDeserializeAttribute"/>.
		/// </summary>
		internal static readonly Type BeforeDeserializeAttributeType = typeof(KTBeforeDeserializeAttribute);
		/// <summary>
		/// Type of the <see cref="KTAfterDeserializeAttribute"/>.
		/// </summary>
		internal static readonly Type AfterDeserializeAttributeType = typeof(KTAfterDeserializeAttribute);

		#endregion



		#region UseProcessedTypes.

		/// <summary>
		/// Private. Should already processed types be used in next serialization/deserialization without reprocessing.
		/// </summary>
		private bool useProcessedTypes = false;
		/// <summary>
		/// Should already processed types be used in next serialization/deserialization without reprocessing. False by default.
		/// </summary>
		public bool UseProcessedTypes
		{
			get { return useProcessedTypes; }
			set { useProcessedTypes = value; }
		}

		
		#endregion

	
		#region CommonSerializeProcess, ShouldProcessTypes.

		/// <summary>
		/// Common <see cref="SerializeProcess"/> object to use in current processing.
		/// </summary>
		private SerializeProcess CommonSerializeProcess = new SerializeProcess();


		/// <summary>
		/// Should any types be processed in serialization process.
		/// </summary>
		private bool shouldProcessTypes = false;
		/// <summary>
		/// Should any types be processed in serialization process.
		/// </summary>
		public bool ShouldProcessTypes
		{
			get
			{
				return
					this.shouldProcessTypes
					||
					this.CommonSerializeProcess.ShouldProcessCommonHeader
					;
			}
		}

		#endregion



		#region Multithreading.

		/// <summary>
		/// Lock object.
		/// </summary>
		private static object locker = new object();

#if DEBUG

		/// <summary>
		/// Leveled lock object.
		/// </summary>
		private static readonly LeveledLock leveledLocker = new LeveledLock(1, "KTSerializer");

#endif

		#endregion

		#endregion


		#region Static constructor.

		/// <summary>
		/// Static constructor.
		/// </summary>
		static KTSerializer()
		{
			#region Create common known types.

			CommonKnownTypes = new Dictionary<Type, uint>();


			CommonKnownTypes.Add(ObjectTypes.Int32, 0);
			CommonKnownTypes.Add(ObjectTypes.String, 1);

			CommonKnownTypes.Add(ObjectTypes.Float, 2);
			CommonKnownTypes.Add(ObjectTypes.Decimal, 3);
			CommonKnownTypes.Add(ObjectTypes.Double, 4);
			CommonKnownTypes.Add(ObjectTypes.Guid, 5);
			CommonKnownTypes.Add(ObjectTypes.Boolean, 6);

			CommonKnownTypes.Add(ObjectTypes.Int64, 7);

			CommonKnownTypes.Add(ObjectTypes.Char, 8);
			CommonKnownTypes.Add(ObjectTypes.TimeSpan, 9);

			CommonKnownTypes.Add(ObjectTypes.Int16, 10);
			CommonKnownTypes.Add(ObjectTypes.Byte, 12);

			CommonKnownTypes.Add(ObjectTypes.DateTime, 14);

			CommonKnownTypes.Add(ObjectTypes.UInt64, 16);
			CommonKnownTypes.Add(ObjectTypes.UInt32, 18);
			CommonKnownTypes.Add(ObjectTypes.UInt16, 20);

			CommonKnownTypes.Add(ObjectTypes.Int64Nullable, 22);
			CommonKnownTypes.Add(ObjectTypes.Int32Nullable, 24);
			CommonKnownTypes.Add(ObjectTypes.Int16Nullable, 26);
			CommonKnownTypes.Add(ObjectTypes.ByteNullable, 28);
			CommonKnownTypes.Add(ObjectTypes.UInt64Nullable, 30);
			CommonKnownTypes.Add(ObjectTypes.UInt32Nullable, 32);
			CommonKnownTypes.Add(ObjectTypes.UInt16Nullable, 34);

			CommonKnownTypes.Add(ObjectTypes.DoubleNullable, 36);
			CommonKnownTypes.Add(ObjectTypes.DecimalNullable, 38);
			CommonKnownTypes.Add(ObjectTypes.FloatNullable, 40);

			CommonKnownTypes.Add(ObjectTypes.GuidNullable, 42);
			CommonKnownTypes.Add(ObjectTypes.DateTimeNullable, 44);
			CommonKnownTypes.Add(ObjectTypes.TimeSpanNullable, 46);
			CommonKnownTypes.Add(ObjectTypes.BooleanNullable, 48);

			CommonKnownTypes.Add(ObjectTypes.Object, 50);

			CommonKnownTypes.Add(ObjectTypes.Color, 52);
			CommonKnownTypes.Add(ObjectTypes.ColorNullable, 54);


			// Set max number.
			CommonKnownTypesMaxNumber = 54;

			#endregion


			#region Create common types mapping.

			commonKnownTypesHeaders = new Dictionary<Type, string>();

			foreach (KeyValuePair<Type, uint> pair in CommonKnownTypes)
			{
				commonKnownTypesHeaders.Add(pair.Key, LineSeparator.ToString() + pair.Value.ToString());
			}
			
			#endregion


			#region Common variables.

			Int32TypeNumber = KTSerializer.GetCommonKnownTypeNumber(ObjectTypes.Int32);
			Int32NullableTypeNumber = KTSerializer.GetCommonKnownTypeNumber(ObjectTypes.Int32Nullable);
			
			#endregion
		}

		#endregion


		#region Instance.

		/// <summary>
		/// Inner singleton <see cref="SearchHelper"/> instance.
		/// </summary>
		private static KTSerializer instance;

		/// <summary>
		/// Gets singleton object of <see cref="SearchHelper"/> class. Thread-safe.
		/// </summary>
		/// <returns><see cref="SearchHelper"/> instance.</returns>
		public static KTSerializer Instance
		{
			get
			{
				if (instance == null)
				{
#if DEBUG
					using (leveledLocker.Enter())
#else
					lock (locker)
#endif
					{
						if (instance == null) instance = new KTSerializer();
					}
				}

				return instance;
			}
		}
		
		#endregion


		#region Constructors.

		/// <summary>
		/// Creates new instance of <see cref="KTSerializer"/>.
		/// </summary>
		public KTSerializer()
			: this(false)
		{ }

		/// <summary>
		/// Creates new instance of <see cref="KTSerializer"/>.
		/// </summary>
		/// <param name="useProcessedTypes">Should already processed types be used in next serialization/deserialization without reprocessing.</param>
		public KTSerializer(bool useProcessedTypes)
		{
			this.KnownTypes = new List<Type>();
			this.KnownTypesMapping = new Dictionary<Type, object>();

			this.UseProcessedTypes = useProcessedTypes;
			this.CommonSerializeProcess = new SerializeProcess();
		}

		#endregion


		#region Public functions.

		#region Serialize().

		/// <summary>
		/// Serializes object.
		/// </summary>
		/// <param name="stream">Stream to write serialized object to.</param>
		/// <param name="value">Object to serialize.</param>
		/// <param name="commonHeaderStream">Possible stream of common header.</param>
		public void Serialize(
			Stream stream,
			object value,
			Stream commonHeaderStream = null
			)
		{
			#region Initial actions.

			// We do nothing for null value.
			if (value == null) return;

			#endregion


			#region Process.

			if (this.UseProcessedTypes)
			{
				// We should have a valid header stream.
				if (commonHeaderStream == null)
					throw new KTSerializeException(Properties.Resources.Error_NoCommonHeaderStreamWithUseProcessedTypes);


				// Prepare common serialization process.
				CommonSerializeProcess.Stream = stream;
				CommonSerializeProcess.CommonHeaderStream = commonHeaderStream;
				CommonSerializeProcess.ProcessedObject = value;
				CommonSerializeProcess.ObjectType = value.GetType();


				// If we should process known types.
				if (this.shouldProcessTypes)
				{
					// Set indicator.
					this.shouldProcessTypes = false;

					CommonSerializeProcess.Serialize(this.KnownTypes);
				}

				// Otherwise, spare time.
				else
				{
					CommonSerializeProcess.Serialize();
				}
			}
			else
			{
				// We should not have a valid header stream.
				if (commonHeaderStream != null)
					throw new KTSerializeException(Properties.Resources.Error_CommonHeaderStreamWithoutUseProcessedTypes);

				new SerializeProcess(stream, value, commonHeaderStream).Serialize(this.KnownTypes);
			}

			#endregion
		}
		
		#endregion		


		#region Deserialize(), Deserialize<T>().

		/// <summary>
		/// Deserializes object.
		/// </summary>
		/// <param name="stream">Stream to read serialized object from.</param>
		/// <param name="valueType">Type to deserialize to.</param>
		/// <param name="commonHeaderStream">Possible stream of common header.</param>
		public object Deserialize(
			Stream stream,
			Type valueType,
			Stream commonHeaderStream = null
			)
		{
			if (this.UseProcessedTypes)
			{
				// We should have a valid header stream.
				if (commonHeaderStream == null)
					throw new KTDeserializeException(Properties.Resources.Error_NoCommonHeaderStreamWithUseProcessedTypes);


				// Prepare common serialization process.
				CommonSerializeProcess.Stream = stream;
				CommonSerializeProcess.CommonHeaderStream = commonHeaderStream;
				CommonSerializeProcess.ProcessedObject = null;
				CommonSerializeProcess.ObjectType = valueType;


				// If we should process known types.
				if (this.shouldProcessTypes)
				{
					// Set indicator.
					this.shouldProcessTypes = false;

					return CommonSerializeProcess.Deserialize(this.KnownTypes);
				}

				// Otherwise, spare time.
				else
				{
					return CommonSerializeProcess.Deserialize();
				}
			}
			else
			{
				// We should not have a valid header stream.
				if (commonHeaderStream != null)
					throw new KTDeserializeException(Properties.Resources.Error_CommonHeaderStreamWithoutUseProcessedTypes);

				return new SerializeProcess(stream, valueType, commonHeaderStream).Deserialize(this.KnownTypes);
			}
		}


		/// <summary>
		/// Deserializes object.
		/// </summary>
		/// <typeparam name="T">Type to deserialize to.</typeparam>
		/// <param name="stream">Stream to read serialized object from.</param>
		/// <param name="valueType"></param>
		/// <param name="commonHeaderStream">Possible stream of common header.</param>
		public T Deserialize<T>(
			Stream stream,
			Stream commonHeaderStream = null
			)
		{
			return (T)this.Deserialize(stream, typeof(T), commonHeaderStream);
		}

		/**/
		#endregion		



		#region Clone().

		/// <summary>
		/// Performs cloning by using serializaion and deserialization on provided object.
		/// </summary>
		/// <param name="value">Object to clone.</param>
		/// <param name="commonHeaderStream">Possible stream of common header.</param>
		/// <returns>Cloned object.</returns>
		public object Clone(
			object value,
			Stream commonHeaderStream = null
			)
		{
			// Initial actions.
			if (value == null) return null;

			// Use standart methods.
			using (MemoryStream stream = new MemoryStream())
			{
				this.Serialize(stream, value, commonHeaderStream);
				stream.Position = 0;
				if (commonHeaderStream != null) commonHeaderStream.Position = 0;
				return this.Deserialize(stream, value.GetType(), commonHeaderStream);
			}
		}
		
		#endregion


		#region Clone<T>().

		/// <summary>
		/// Performs cloning by using serializaion and deserialization on provided object.
		/// </summary>
		/// <typeparam name="T">Type to clone to.</typeparam>
		/// <param name="value">Object to clone.</param>
		/// <param name="commonHeaderStream">Possible stream of common header.</param>
		/// <returns>Cloned object.</returns>
		public T Clone<T>(
			object value,
			Stream commonHeaderStream = null
			)
		{
			// Initial actions.
			if (value == null) return default(T);

			// Use standart methods.
			using (MemoryStream stream = new MemoryStream())
			{
				this.Serialize(stream, value, commonHeaderStream);
				stream.Position = 0;
				if (commonHeaderStream != null) commonHeaderStream.Position = 0;
				return this.Deserialize<T>(stream, commonHeaderStream);
			}
		}

		#endregion



		#region GetCommonKnownTypeNumber().

		/// <summary>
		/// Gets common known type number.
		/// </summary>
		/// <param name="typeID">ID of type to get header for.</param>
		/// <returns>Common known type.</returns>
		internal static uint GetCommonKnownTypeNumber(Type type)
		{
			return CommonKnownTypes[type];
		}

		#endregion



		#region RegisterType(), RegisterTypes().

		/// <summary>
		/// Registers type in the serializer.
		/// </summary>
		/// <param name="type">Type to register.</param>
		public void RegisterType(Type type)
		{
			// Initial check.
			if (type == null) return;

			if (!this.HasRegisteredType(type))
			{
				this.KnownTypes.Add(type);
				this.KnownTypesMapping.Add(type, null);
			}
			this.shouldProcessTypes = true;
		}


		/// <summary>
		/// Registers types in the serializer.
		/// </summary>
		/// <param name="types">Types to register.</param>
		public void RegisterTypes(List<Type> types)
		{
			// Initial check.
			if (types == null) return;

			foreach (Type type in types)
			{
				this.RegisterType(type);
			}
		}

		#endregion


		#region HasRegisteredType().

		/// <summary>
		/// If type has been registered in known types.
		/// </summary>
		/// <param name="type"><see cref="Type"/> to check.</param>
		/// <returns>True if type has been regisered in known types, false oherwise.</returns>
		public bool HasRegisteredType(Type type)
		{
			return (this.KnownTypesMapping.ContainsKey(type));
		}
		
		#endregion


		#region ClearRegisteredTypes().

		/// <summary>
		/// Clears registered types.
		/// </summary>
		public void ClearRegisteredTypes()
		{
			this.KnownTypes.Clear();
			this.KnownTypesMapping.Clear();

			this.shouldProcessTypes = false;
		}
		
		#endregion



		#region CheckSupportedCollection().

		/// <summary>
		/// Check if given type is a supported collection type.
		/// </summary>
		/// <param name="type">Type to check.</param>
		/// <returns>True if type is not null and is a supported collection type, false otherwise.</returns>
		internal static bool CheckSupportedCollection(Type type)
		{
			return (
				// Check for supported collections.	
				type == DictionaryType
				||
				type == ListType
				);
		}

		#endregion
		
		#endregion
	}
}
