using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace KT.Common.Classes.Application
{
	#region SerializeIDEntry.

	/// <summary>
	/// One entry with info about identificators.
	/// </summary>
	internal abstract class SerializeIDEntry : SerializeItemEntry
	{
		#region Properties.

		#region Common.

		/// <summary>
		/// Attribute that describes the include info of the current entry.
		/// </summary>
		public KTSerializeIncludeAttribute IncludeAttribute;

		/// <summary>
		/// Start number of entry in the total collection of gathered type identificators of the parent type.
		/// </summary>
		public int EntryNumber;

		/// <summary>
		/// If get/set delegate should be created when setting entry info.
		/// </summary>
		protected bool shouldCreateGetSetDelegate;
		
		/// <summary>
		/// Is type info entry set for the current field/property entry.
		/// </summary>
		public bool IsTypeInfoEntrySet;
		
		#endregion


		#region Name.

		/// <summary>
		/// Gets the name of stored info of the entry.
		/// </summary>
		/// <returns>Name of the stored info.</returns>
		public abstract string Name
		{
			get;
		}

		#endregion


		#region Get/set delegates.

		/// <summary>
		/// Delegate to get property value.
		/// </summary>
		public Func<object, object> GetValue;
		/// <summary>
		/// Delegate to set property value.
		/// </summary>
		public Action<object, object> SetValue;

		#endregion

		#endregion


		#region Constructors.

		/// <summary>
		/// Creates new instance of <see cref="SerializeIDEntry"/>.
		/// </summary>
		/// <param name="shouldCreateDelegate">Should get/set delegate be created at setting entry info.</param>
		public SerializeIDEntry(bool shouldCreateDelegate)
		{
			this.shouldCreateGetSetDelegate = shouldCreateDelegate;
		}

		/// <summary>
		/// Creates new instance of <see cref="SerializeIDEntry"/> on basis of sent instance.
		/// </summary>
		/// <param name="entry">Entry to create new instance upon.</param>
		public SerializeIDEntry(SerializeIDEntry entry)
			: this(true)
		{ }

		#endregion


		#region ToString().

		/// <summary>
		/// Returns string representation of the current object.
		/// </summary>
		/// <returns>String representation of the current object.</returns>
		public override string ToString()
		{
			return this.Name + " [" + this.Type.FullName + "], " + this.EntryNumber;
		}

		#endregion
	}
	
	#endregion


	#region SerializePropertyIDEntry.

	/// <summary>
	/// One entry with info about identificators in a property.
	/// </summary>
	internal class SerializePropertyIDEntry : SerializeIDEntry
	{
		#region Properties.

		#region PropertyInfo.

		/// <summary>
		/// <see cref="PropertyInfo"/> of the current entry.
		/// </summary>
		private PropertyInfo propertyInfo;
		/// <summary>
		/// <see cref="PropertyInfo"/> of the current entry.
		/// </summary>
		public PropertyInfo PropertyInfo
		{
			get { return propertyInfo; }
			set
			{
				propertyInfo = value;
				this.Type = propertyInfo != null ? propertyInfo.PropertyType : null;

				if (this.shouldCreateGetSetDelegate) createGetSetDelegates();
			}
		}

		#endregion


		#region Name.

		/// <summary>
		/// Gets the name of stored property.
		/// </summary>
		/// <returns>Name of the stored property.</returns>
		public override string Name
		{
			get { return this.propertyInfo.Name; }
		}

		#endregion

		#endregion


		#region Constructors.

		/// <summary>
		/// Creates new instance of <see cref="SerializePropertyIDEntry"/>.
		/// </summary>
		/// <param name="shouldCreateDelegate">Should get/set delegate be created at setting entry info.</param>
		public SerializePropertyIDEntry(bool shouldCreateDelegate)
			: base(shouldCreateDelegate)
		{ }

		/// <summary>
		/// Creates new instance of <see cref="SerializePropertyIDEntry"/> on basis of sent instance.
		/// </summary>
		/// <param name="entry">Entry to create new instance upon.</param>
		public SerializePropertyIDEntry(SerializeIDEntry entry)
			: this(false) // do not recreate delegates at cloning
		{
			SerializePropertyIDEntry propertyEntry = (SerializePropertyIDEntry)entry;

			// Copy field info.
			this.PropertyInfo = propertyEntry.PropertyInfo;
			this.IncludeAttribute = propertyEntry.IncludeAttribute;

			// Copy delegates.
			this.GetValue = propertyEntry.GetValue;
			this.SetValue = propertyEntry.SetValue;

			// For possible future use.
			this.shouldCreateGetSetDelegate = true;
		}

		#endregion


		#region Create Get() and Set() delegates.

		/// <summary>
		/// Creates delegates to get and set property values.
		/// </summary>
		private void createGetSetDelegates()
		{
			#region Structure property.

			if (this.propertyInfo.DeclaringType.IsValueType)
			{
				// Setter.
				SetValue = (object o, object value) => { this.propertyInfo.SetValue(o, value, null); };
				// Getter.
				GetValue = (object o) => { return this.propertyInfo.GetValue(o, null); };
			}

			#endregion


			#region Reference (object) type.

			else
			{
				#region Setter.

				// Create setter delegate.
				ParameterExpression instance = Expression.Parameter(ObjectTypes.Object, "t");
				ParameterExpression argument = Expression.Parameter(ObjectTypes.Object, "p");

				UnaryExpression instanceConvert = Expression.Convert(instance, this.propertyInfo.DeclaringType);

				MethodCallExpression setterCall = Expression.Call(
					instanceConvert,
					this.propertyInfo.GetSetMethod(true),
					Expression.Convert(argument, this.propertyInfo.PropertyType)
					);
				SetValue = Expression.Lambda<Action<object, object>>(setterCall, instance, argument).Compile();
				
				#endregion


				#region Getter.

				// Create getter delegate.
				UnaryExpression getterCall =
					Expression.Convert(
						Expression.Call(
							instanceConvert,
							this.propertyInfo.GetGetMethod(true)
							),
						ObjectTypes.Object
						);
				GetValue = Expression.Lambda<Func<object, object>>(getterCall, instance).Compile();
				
				#endregion
			}

			#endregion
		}
		
		#endregion
	}
	
	#endregion


	#region SerializeFieldIDEntry.

	/// <summary>
	/// One entry with info about identificators in a field.
	/// </summary>
	internal class SerializeFieldIDEntry : SerializeIDEntry
	{
		#region Properties.

		#region FieldInfo.

		/// <summary>
		/// <see cref="FieldInfo"/> of the current entry.
		/// </summary>
		private FieldInfo fieldInfo;
		/// <summary>
		/// <see cref="FieldInfo"/> of the current entry.
		/// </summary>
		public FieldInfo FieldInfo
		{
			get { return fieldInfo; }
			set
			{
				fieldInfo = value;
				this.Type = fieldInfo != null ? fieldInfo.FieldType : null;

				if (this.shouldCreateGetSetDelegate) createGetSetDelegates();
			}
		}

		#endregion


		#region Name.

		/// <summary>
		/// Gets the name of stored field.
		/// </summary>
		/// <returns>Name of the stored field.</returns>
		public override string Name
		{
			get { return this.fieldInfo.Name; }
		}

		#endregion

		#endregion


		#region Constructors.

		/// <summary>
		/// Creates new instance of <see cref="SerializeFieldIDEntry"/>.
		/// </summary>
		/// <param name="shouldCreateDelegate">Should get/set delegate be created at setting entry info.</param>
		public SerializeFieldIDEntry(bool shouldCreateDelegate)
			: base(shouldCreateDelegate)
		{ }

		/// <summary>
		/// Creates new instance of <see cref="SerializeFieldIDEntry"/> on basis of sent instance.
		/// </summary>
		/// <param name="entry">Entry to create new instance upon.</param>
		public SerializeFieldIDEntry(SerializeIDEntry entry)
			: this(false) // do not recreate delegates at cloning
		{
			SerializeFieldIDEntry fieldEntry = (SerializeFieldIDEntry)entry;

			// Copy field info.
			this.FieldInfo = fieldEntry.FieldInfo;
			this.IncludeAttribute = fieldEntry.IncludeAttribute;

			// Copy delegates.
			this.GetValue = fieldEntry.GetValue;
			this.SetValue = fieldEntry.SetValue;

			// For possible future use.
			this.shouldCreateGetSetDelegate = true;
		}

		#endregion


		#region Create Get() and Set() delegates.

		/// <summary>
		/// Creates delegates to get and set property values.
		/// </summary>
		private void createGetSetDelegates()
		{
			#region Structure property.

			if (this.fieldInfo.DeclaringType.IsValueType)
			{
				// Setter.
				SetValue = (object o, object value) => { this.fieldInfo.SetValue(o, value); };
				// Getter.
				GetValue = (object o) => { return this.fieldInfo.GetValue(o); };
			}

			#endregion


			#region Reference (object) type.

			else
			{
				#region Setter.

				// Create setter delegate.
				ParameterExpression instance = Expression.Parameter(ObjectTypes.Object, "t");
				ParameterExpression argument = Expression.Parameter(ObjectTypes.Object, "p");

				UnaryExpression instanceConvert = Expression.Convert(instance, this.fieldInfo.DeclaringType);
				MemberExpression fieldExpression = Expression.Field(instanceConvert, this.fieldInfo);

				BinaryExpression setterCall = Expression.Assign(
					fieldExpression,
					Expression.Convert(argument, this.fieldInfo.FieldType)
					);
				SetValue = Expression.Lambda<Action<object, object>>(setterCall, instance, argument).Compile();

				#endregion


				#region Getter.

				// Create getter delegate.
				UnaryExpression getterCall = Expression.Convert(fieldExpression, ObjectTypes.Object);
				GetValue = Expression.Lambda<Func<object, object>>(getterCall, instance).Compile();

				#endregion
			}

			#endregion
		}

		#endregion
	}
	
	#endregion
}
