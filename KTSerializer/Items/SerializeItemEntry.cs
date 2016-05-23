using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace KT.Common.Classes.Application
{
	/// <summary>
	/// One common item with info about property/field.
	/// </summary>
	internal class SerializeItemEntry
	{
		#region Type.

		/// <summary>
		/// Private. Type of item value.
		/// </summary>
		protected Type type;
		/// <summary>
		/// Type of item value.
		/// </summary>
		public virtual Type Type
		{
			get { return type; }
			set
			{
				this.type = value;

				UnderlyingType = ObjectHelper.GetUnderlyingType(Type);
				HasUnderlyingType = (UnderlyingType != null);

				TypeToProcess = HasUnderlyingType ? UnderlyingType : Type;
				GenericType = ObjectHelper.GetGenericType(TypeToProcess);

				HasGenericType = (GenericType != null);
				HasUnderlyingOrGenericType = (HasUnderlyingType || HasGenericType);

				HasArrayType = TypeToProcess.IsArray;

				if (HasGenericType)
				{
					HasDictionaryType = (GenericType == KTSerializer.DictionaryType);
					HasListType = (GenericType == KTSerializer.ListType);

					HasCollectionType = (HasDictionaryType || HasListType);
				}

				if (HasArrayType) HasCollectionType = true; // always
			}
		}
		
		#endregion


		#region Type-related properties.

		/// <summary>
		/// Possible underlying type of item.
		/// </summary>
		public Type UnderlyingType;
		/// <summary>
		/// Possible generic type of item.
		/// </summary>
		public Type GenericType;
		/// <summary>
		/// Type of item to be processed (main or underlying, if such exists).
		/// </summary>
		public Type TypeToProcess;


		/// <summary>
		/// Does type of item has a generic type.
		/// </summary>
		public bool HasGenericType;
		/// <summary>
		/// Does type of item has an underlying type. It means that type of item is a Nullable&lt;T&gt;.
		/// </summary>
		public bool HasUnderlyingType;
		/// <summary>
		/// Does type of item has an underlying or generic type.
		/// </summary>
		public bool HasUnderlyingOrGenericType;


		/// <summary>
		/// Possible generic entry of the value.
		/// </summary>
		public SerializeCollectionEntry CollectionEntry;

		/// <summary>
		/// Is generic value type a item type.
		/// </summary>
		public bool HasCollectionType;

		/// <summary>
		/// Is generic value type a dictionary type.
		/// </summary>
		public bool HasDictionaryType;
		/// <summary>
		/// Is generic value type a list type.
		/// </summary>
		public bool HasListType;
		/// <summary>
		/// Is value type an array type.
		/// </summary>
		public bool HasArrayType;


		/// <summary>
		/// Value type info entry for the current field/property entry.
		/// </summary>
		public SerializeTypeInfoEntry TypeInfoEntry;
		
		#endregion


		#region Known type wrapper.

		/// <summary>
		/// Is type a some common known type, but not <see cref="object"/>.
		/// </summary>
		public bool HasPrimitiveTypeWrapper = false;


		/// <summary>
		/// Is type an <see cref="Enum"/> or an <see cref="Enum?"/> type.
		/// </summary>
		public bool IsEnumType = false;
		/// <summary>
		/// Is type an <see cref="Enum?"/> type.
		/// </summary>
		public bool IsEnumNullableType = false;

		/// <summary>
		/// Possible common known type wrapper.
		/// </summary>
		public KnownTypeWrapper KnownTypeWrapper;

		#endregion
	}
}
