using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KT.Common.Classes.Application
{
	// Attributes to use in KT serialization.

	#region KTSerializeAttribute.

	/// <summary>
	/// Attribute, indicating that marked class or structure should be included to serialization/deserialization.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface)]
	public class KTSerializeAttribute : Attribute
	{
		#region Properties.

		#region ClassID.

		/// <summary>
		/// Private. ID of the class this attribute is applied to.
		/// </summary>
		private Guid classID;
		/// <summary>
		/// Private. ID of the class this attribute is applied to.
		/// </summary>
		public Guid ClassID
		{
			get { return classID; }
			set { classID = value; }
		}

		#endregion

		#endregion


		#region Constructors.

		/// <summary>
		/// Creates new instance of <see cref="KTSerializeAttribute"/>.
		/// </summary>
		/// <param name="classIDCode">Class ID code to use in serialization. Code should be a valid <see cref="Guid"/> string representation.</param>
		public KTSerializeAttribute(string classIDCode)
		{
			this.classID = new Guid(classIDCode);
		}

		#endregion
	}

	#endregion


	#region KTSerializeIncludeAttribute.

	/// <summary>
	/// Attribute, indicating that marked field or property should be included to serialization/deserialization.
	/// </summary>
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
	public class KTSerializeIncludeAttribute : Attribute
	{
		#region Properties.

		#region FieldID.

		/// <summary>
		/// Private. ID of the field this attribute is applied to.
		/// </summary>
		private Guid fieldID;
		/// <summary>
		/// Private. ID of the field this attribute is applied to.
		/// </summary>
		public Guid FieldID
		{
			get { return fieldID; }
			set { fieldID = value; }
		}

		#endregion


		#region FieldIDString.

		/// <summary>
		/// Private. ID of the field this attribute is applied to, in string format.
		/// </summary>
		private string fieldIDCode;
		/// <summary>
		/// Private. ID of the field this attribute is applied to, in string format.
		/// </summary>
		public string FieldIDCode
		{
			get { return fieldIDCode; }
			set { fieldIDCode = value; }
		}

		#endregion

		#endregion


		#region Constructor.

		/// <summary>
		/// Creates new instance of <see cref="KTSerializeIncludeAttribute"/>.
		/// </summary>
		/// <param name="fieldIDCode">Field ID code to use in serialization. Code should be a valid <see cref="Guid"/> string representation.</param>
		public KTSerializeIncludeAttribute(string fieldIDCode)
		{
			this.fieldID = new Guid(fieldIDCode);
			this.fieldIDCode = this.fieldID.ToString(KTSerializer.GuidFormat);
		}

		#endregion
	}
	
	#endregion



	#region KTBeforeSerializeAttribute.

	/// <summary>
	/// Attribute, indicating that marked function should be called before serialization.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method)]
	public class KTBeforeSerializeAttribute : Attribute
	{
		#region Constructors.

		/// <summary>
		/// Creates new instance of <see cref="KTBeforeSerializeAttribute"/>.
		/// </summary>
		public KTBeforeSerializeAttribute()
		{ }

		#endregion
	}

	#endregion


	#region KTAfterSerializeAttribute.

	/// <summary>
	/// Attribute, indicating that marked function should be called after serialization.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method)]
	public class KTAfterSerializeAttribute : Attribute
	{
		#region Constructors.

		/// <summary>
		/// Creates new instance of <see cref="KTAfterSerializeAttribute"/>.
		/// </summary>
		public KTAfterSerializeAttribute()
		{ }

		#endregion
	}

	#endregion



	#region KTBeforeDeserializeAttribute.

	/// <summary>
	/// Attribute, indicating that marked function should be called before deserialization.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method)]
	public class KTBeforeDeserializeAttribute : Attribute
	{
		#region Constructors.

		/// <summary>
		/// Creates new instance of <see cref="KTBeforeDeserializeAttribute"/>.
		/// </summary>
		public KTBeforeDeserializeAttribute()
		{ }

		#endregion
	}

	#endregion


	#region KTAfterDeserializeAttribute.

	/// <summary>
	/// Attribute, indicating that marked function should be called after deserialization.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method)]
	public class KTAfterDeserializeAttribute : Attribute
	{
		#region Constructors.

		/// <summary>
		/// Creates new instance of <see cref="KTAfterDeserializeAttribute"/>.
		/// </summary>
		public KTAfterDeserializeAttribute()
		{ }

		#endregion
	}

	#endregion


}
