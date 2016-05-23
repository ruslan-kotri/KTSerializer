using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KT.Common.Classes.Application
{
	/// <summary>
	/// Provides methods and properties that facilitate work with objects.
	/// </summary>
	public static class ObjectHelper
	{
		#region IsEqual().

		/// <summary>
		/// Decides if value is equal to the new value. Can accept null values.
		/// </summary>
		/// <param name="value">Existing value.</param>
		/// <param name="newValue">Value to compare with.</param>
		/// <returns>True if both values are null or equal, false otherwise.</returns>
		public static bool IsEqual(object value, object newValue)
		{
			return (
				(
					value == null
					&&
					newValue == null
					)
				||
				(
					value != null
					&&
					value.Equals(newValue)
					)
				);
		}

		#endregion


		#region CreateCode().

		/// <summary>
		/// Creates code for a collection of values.
		/// </summary>
		/// <param name="values">Collection of values to create code for.</param>
		/// <returns>Created code.</returns>
		public static string CreateCode(List<bool> values)
		{
			// Initial check.
			if (values == null) throw new ArgumentNullException("values");

			string s = new string(' ', values.Count);

			unsafe
			{
				fixed (char* textPointer = s)
				{
					char* pointer = textPointer;

					int i = 0;
					while (*pointer != '\0')
					{
						*pointer = (values[i] ? '1' : '0');

						i++;
						pointer++;
					}
				}
			}

			return s;
		}


		/// <summary>
		/// Creates code for a collection of values.
		/// </summary>
		/// <param name="values">Collection of values to create code for.</param>
		/// <returns>Created code.</returns>
		public static string CreateCode(bool[] values)
		{
			// Initial check.
			if (values == null) throw new ArgumentNullException("values");

			string s = new string(' ', values.Length);

			unsafe
			{
				fixed (bool* valuePointer = values)
				fixed (char* textPointer = s)
				{
					char* pointer = textPointer;
					bool* pointerBool = valuePointer;

					while (*pointer != '\0')
					{
						*pointer = (*pointerBool ? '1' : '0');

						pointer++;
						pointerBool++;
					}
				}
			}

			return s;
		}

		#endregion


		#region GetUnderlyingType().

		/// <summary>
		/// Gets possible underlying type for the given nullable type.
		/// </summary>
		/// <param name="type">Type to get underlying type for.</param>
		/// <returns>Underlying type or null, if such does not exist.</returns>
		public static Type GetUnderlyingType(Type type)
		{
			return Nullable.GetUnderlyingType(type);
		}

		#endregion


		#region GetGenericType().

		/// <summary>
		/// Gets possible generic type for the given type.
		/// </summary>
		/// <param name="type">Type to get generic type for.</param>
		/// <returns>Generic type or null, if such does not exist.</returns>
		public static Type GetGenericType(Type type)
		{
			// Cache works longer than the plain call.
			return (
				type.IsGenericType
				&&
				!type.IsValueType // check for int? or so types
				) ?
				type.GetGenericTypeDefinition() :
				null;
		}

		#endregion


		#region HasGenericType().

		/// <summary>
		/// Is there a possible generic type for the given type.
		/// </summary>
		/// <param name="type">Type to get generic type for.</param>
		/// <param name="genericType">Possible generic type to set.</param>
		/// <returns>True if there is a generic type or false, if such does not exist.</returns>
		public static bool HasGenericType(Type type, out Type genericType)
		{
			// Cache works longer than the plain call.
			genericType = null;

			if (
				type.IsGenericType
				&&
				!type.IsValueType // check for int? or so types
				)
			{
				genericType = type.GetGenericTypeDefinition();
				return true;
			}
			else
			{
				return false;
			}
		}

		#endregion


		#region GetDefaultValue().

		/// <summary>
		/// Get default value of a <see cref="Type"/>.
		/// </summary>
		/// <param name="type"><see cref="Type"/> to get default value for.</param>
		/// <returns>Default value for value types and null for reference types.</returns>
		public static object GetDefaultValue(Type type)
		{
			if (type.IsValueType)
				return Activator.CreateInstance(type); // default(Type)
			else
				return null;
		}
		
		#endregion

	}
}
