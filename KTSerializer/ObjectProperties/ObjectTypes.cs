using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KT.Common.Classes.Application
{
	/// <summary>
	/// Contains different preset types.
	/// </summary>
	/// <remarks>Types are added here when necessary.</remarks>
	public static class ObjectTypes
	{
		#region Int types.

		/// <summary>
		/// Int64 type.
		/// </summary>
		public static readonly Type Int64 = typeof(Int64);
		/// <summary>
		/// Int32 type.
		/// </summary>
		public static readonly Type Int32 = typeof(Int32);
		/// <summary>
		/// Int16 type.
		/// </summary>
		public static readonly Type Int16 = typeof(Int16);
		/// <summary>
		/// Byte type.
		/// </summary>
		public static readonly Type Byte = typeof(Byte);

		/// <summary>
		/// UInt64 type.
		/// </summary>
		public static readonly Type UInt64 = typeof(UInt64);
		/// <summary>
		/// UInt32 type.
		/// </summary>
		public static readonly Type UInt32 = typeof(UInt32);
		/// <summary>
		/// UInt16 type.
		/// </summary>
		public static readonly Type UInt16 = typeof(UInt16);


		/// <summary>
		/// Int type.
		/// </summary>
		public static Type Int
		{
			get { return Int32; }
		}
		/// <summary>
		/// UInt type.
		/// </summary>
		public static Type UInt
		{
			get { return UInt32; }
		}

		#endregion


		#region Nullable int types.

		/// <summary>
		/// Int64? type.
		/// </summary>
		public static readonly Type Int64Nullable = typeof(Int64?);
		/// <summary>
		/// Int32? type.
		/// </summary>
		public static readonly Type Int32Nullable = typeof(Int32?);
		/// <summary>
		/// Int16? type.
		/// </summary>
		public static readonly Type Int16Nullable = typeof(Int16?);
		/// <summary>
		/// Byte? type.
		/// </summary>
		public static readonly Type ByteNullable = typeof(Byte?);

		/// <summary>
		/// UInt64? type.
		/// </summary>
		public static readonly Type UInt64Nullable = typeof(UInt64?);
		/// <summary>
		/// UInt32? type.
		/// </summary>
		public static readonly Type UInt32Nullable = typeof(UInt32?);
		/// <summary>
		/// UInt16? type.
		/// </summary>
		public static readonly Type UInt16Nullable = typeof(UInt16?);


		/// <summary>
		/// Int? type.
		/// </summary>
		public static Type IntNullable
		{
			get { return Int32Nullable; }
		}
		/// <summary>
		/// UInt? type.
		/// </summary>
		public static Type UIntNullable
		{
			get { return UInt32Nullable; }
		}

		#endregion


		#region Double.

		/// <summary>
		/// Double type.
		/// </summary>
		public static readonly Type Double = typeof(double);

		/// <summary>
		/// Double? type.
		/// </summary>
		public static readonly Type DoubleNullable = typeof(double?);

		#endregion


		#region Decimal.

		/// <summary>
		/// Decimal type.
		/// </summary>
		public static readonly Type Decimal = typeof(decimal);

		/// <summary>
		/// Decimal? type.
		/// </summary>
		public static readonly Type DecimalNullable = typeof(decimal?);

		#endregion


		#region Float.

		/// <summary>
		/// Float type.
		/// </summary>
		public static readonly Type Float = typeof(float);

		/// <summary>
		/// Float? type.
		/// </summary>
		public static readonly Type FloatNullable = typeof(float?);

		#endregion


		#region Common.

		/// <summary>
		/// Object type.
		/// </summary>
		public static readonly Type Object = typeof(object);

		/// <summary>
		/// Value type.
		/// </summary>
		public static readonly Type ValueType = typeof(ValueType);


		/// <summary>
		/// String type.
		/// </summary>
		public static readonly Type String = typeof(string);

		/// <summary>
		/// Char type.
		/// </summary>
		public static readonly Type Char = typeof(char);

		/// <summary>
		/// Nullable value (<see cref="Nullable&lt;&gt;"/>) type.
		/// </summary>
		public static readonly Type Nullable = typeof(Nullable<>);
		
		#endregion


		#region Guid.

		/// <summary>
		/// GUID type.
		/// </summary>
		public static readonly Type Guid = typeof(Guid);

		/// <summary>
		/// GUID? type.
		/// </summary>
		public static readonly Type GuidNullable = typeof(Guid?);

		#endregion


		#region DateTime.

		/// <summary>
		/// DateTime type.
		/// </summary>
		public static readonly Type DateTime = typeof(DateTime);

		/// <summary>
		/// DateTime? type.
		/// </summary>
		public static readonly Type DateTimeNullable = typeof(DateTime?);

		#endregion


		#region TimeSpan.

		/// <summary>
		/// TimeSpan type.
		/// </summary>
		public static readonly Type TimeSpan = typeof(TimeSpan);

		/// <summary>
		/// TimeSpan? type.
		/// </summary>
		public static readonly Type TimeSpanNullable = typeof(TimeSpan?);

		#endregion


		#region Boolean.

		/// <summary>
		/// Boolean type.
		/// </summary>
		public static readonly Type Boolean = typeof(Boolean);

		/// <summary>
		/// Boolean? type.
		/// </summary>
		public static readonly Type BooleanNullable = typeof(Boolean?);

		#endregion


		#region Color.

		/// <summary>
		/// Color type.
		/// </summary>
		public static readonly Type Color = typeof(Color);

		/// <summary>
		/// Color? type.
		/// </summary>
		public static readonly Type ColorNullable = typeof(Color?);

		#endregion

	}

}
