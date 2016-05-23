using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace KT.Common.Classes.Application
{
	#region SerializeCollectionEntry.

	/// <summary>
	/// One entry with info about a collection type.
	/// </summary>
	internal abstract class SerializeCollectionEntry
	{
		#region Properties.

		#region Type.

		/// <summary>
		/// Collection type.
		/// </summary>
		protected Type type;
		/// <summary>
		/// Collection type.
		/// </summary>
		public virtual Type Type
		{
			get
			{
				return type;
			}
			set
			{
				type = value;
				createConstructor();
			}
		}

		#endregion


		#region Type derived properties.

		/// <summary>
		/// Is necessary type info entry (or entries) set for the current field/property entry.
		/// </summary>
		public bool IsTypeInfoEntrySet;
		
		#endregion

		#endregion


		#region CreateInstance().

		/// <summary>
		/// Creates new instance of the collection.
		/// </summary>
		/// <param name="i">Collection size.</param>
		/// <returns>Collection object.</returns>
		public object CreateInstance(int i)
		{
			return constructor(i);
		}

		#endregion


		#region Auxiliary functions.

		#region createConstructor().

		/// <summary>
		/// Delegate to create type instance.
		/// </summary>
		private Func<int, object> constructor;

		/// <summary>
		/// Creates constructor delegate.
		/// </summary>
		private void createConstructor()
		{
			// Some end class.
			if (!this.Type.IsAbstract)
			{
				// Get defined constructor.
				ConstructorInfo constructorInfo = this.Type.GetConstructor(
					BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
					null, new Type[1] { ObjectTypes.Int32 }, null
					);

				// NB.! Constructor info should always exist.

				// Create delegate.
				ParameterExpression exValue = Expression.Parameter(ObjectTypes.Int32, "p");
				NewExpression newExpression = Expression.New(constructorInfo, exValue);
				constructor = Expression.Lambda<Func<int, object>>(newExpression, exValue).Compile();
			}

			// We do not need constructors for abstract classes.
			else
			{
				constructor = null;
			}
		}

		#endregion

		#endregion
	}
	
	#endregion


	#region SerializeDictionaryEntry.

	/// <summary>
	/// One entry with info about a IDictionary<,> type.
	/// </summary>
	internal class SerializeDictionaryEntry : SerializeCollectionEntry
	{
		#region Properties.

		#region Type.

		/// <summary>
		/// Collection type.
		/// </summary>
		public override Type Type
		{
			get
			{
				return type;
			}
			set
			{
				base.Type = value;

				// [0] - key type, [1] - value type
				Type[] keyValueTypes = type.GetGenericArguments();

				this.KeyItemEntry.Type = keyValueTypes[0];
				this.ValueItemEntry.Type = keyValueTypes[1];
			}
		}
		
		#endregion


		#region Collection specific.

		/// <summary>
		/// Type of collection key.
		/// </summary>
		public SerializeItemEntry KeyItemEntry = new SerializeItemEntry();

		/// <summary>
		/// Type of collection value.
		/// </summary>
		public SerializeItemEntry ValueItemEntry = new SerializeItemEntry();

		#endregion

		#endregion
	}

	#endregion


	#region SerializeListEntry.

	/// <summary>
	/// One entry with info about a IList type.
	/// </summary>
	internal class SerializeListEntry : SerializeCollectionEntry
	{
		#region Properties.

		#region Type.

		/// <summary>
		/// Collection type.
		/// </summary>
		public override Type Type
		{
			get
			{
				return type;
			}
			set
			{
				base.Type = value;

				// [0] - value type.
				this.ValueItemEntry.Type = type.GetGenericArguments()[0];
			}
		}

		#endregion


		#region Collection specific.

		/// <summary>
		/// Type of collection value.
		/// </summary>
		public SerializeItemEntry ValueItemEntry = new SerializeItemEntry();
		
		#endregion

		#endregion


	}

	#endregion


	#region SerializeArrayEntry.

	/// <summary>
	/// One entry with info about an array type.
	/// </summary>
	internal class SerializeArrayEntry : SerializeCollectionEntry
	{
		#region Properties.

		#region Type.

		/// <summary>
		/// Collection type.
		/// </summary>
		public override Type Type
		{
			get
			{
				return type;
			}
			set
			{
				base.Type = value;

				this.ValueItemEntry.Type = type.GetElementType();


				#region Create setter delegate.

				// This parameter expression represents a variable that will hold the array.
				ParameterExpression arrayExpression = Expression.Parameter(ObjectTypes.Object, "Array");

				// This parameter expression represents an array index.            
				ParameterExpression index = Expression.Parameter(ObjectTypes.Int, "Index");

				// This parameter represents the value that will be added to a corresponding array element.
				ParameterExpression valueExpression = Expression.Parameter(ObjectTypes.Object, "Value");



				this.SetValue = Expression.Lambda<Action<object, int, object>>(
					// This expression represents an array access operation.
					Expression.Assign(
						Expression.ArrayAccess(
							Expression.Convert(arrayExpression, type),
							index
						),
						Expression.Convert(valueExpression, this.ValueItemEntry.Type)
						),
					arrayExpression,
					index,
					valueExpression
					)
					.Compile();

				#endregion


				#region Create getter delegate.

				this.GetValue = Expression.Lambda<Func<object, int, object>>(
					// Convert output to object type.
					Expression.Convert(
						// This expression represents an array access operation.
						Expression.ArrayAccess(
							Expression.Convert(arrayExpression, type),
							index
						), ObjectTypes.Object),
					arrayExpression,
					index
					)
					.Compile();

				#endregion
			}
		}

		#endregion


		#region Collection specific.

		/// <summary>
		/// Type of collection value.
		/// </summary>
		public SerializeItemEntry ValueItemEntry = new SerializeItemEntry();

		/// <summary>
		/// Delegate to get array value. Parameters: array, index, value.
		/// </summary>
		public Func<object, int, object> GetValue;
		/// <summary>
		/// Delegate to set array value. Parameters: array, index.
		/// </summary>
		public Action<object, int, object> SetValue;

		#endregion

		#endregion
	}

	#endregion

}
