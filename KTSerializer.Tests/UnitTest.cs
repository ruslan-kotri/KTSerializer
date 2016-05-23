using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Drawing;
using System.Globalization;

using KT.Common.Classes.Application;

namespace KT.Serializer.Tests
{
    [TestClass]
    public class UnitTest
    {
        #region TestKTSerializer().

        /// <summary>
        /// Tests <see cref="KTSerializer"/> class.
        /// </summary>
        [TestMethod]
        public void TestKTSerializer()
        {
            {
                #region Initial actions.

                // Define variables.
                KTSerializer serializer = null;
                Stream headerStream = null;
                bool useProcessedTypes = false;


                long streamLength = -1;
                int cycleLength = 25;


                #region Define plain properties values.

                DateTime dateTime = DateTime.Now;
                DateTime dateTime1 = dateTime.AddMinutes(-10);

                TimeSpan timeSpanValue = new TimeSpan(18, 5, 37, 1658);
                TimeSpan timeSpanValue1 = timeSpanValue.Add(TimeSpan.FromDays(5));

                Guid guidValue = Guid.NewGuid();
                Guid guidValue1 = Guid.NewGuid();


                int intValue = Int32.MaxValue - 1;
                int intValue1 = Int32.MinValue + 1;

                short int16Value = Int16.MaxValue - 1;
                short int16Value1 = Int16.MinValue + 1;

                long int64Value = Int64.MaxValue - 1;
                long int64Value1 = Int64.MinValue + 1;


                uint uintValue = Int32.MaxValue - 1;
                uint uintValue1 = uintValue - 1;

                ushort uint16Value = Int16.MaxValue - 1;
                ushort uint16Value1 = (ushort)(uint16Value - 1);

                ulong uint64Value = Int64.MaxValue - 1;
                ulong uint64Value1 = uint64Value - 1;


                string stringValue = " 	Analysegerät";
                string stringValue1 = "кириллический текст";

                char charValue = 'ä';
                char charValue1 = 'к';

                byte byteValue = Byte.MaxValue - 1;
                byte byteValue1 = (byte)(byteValue - 1);

                float floatValue = 126.1566F;
                float floatValue1 = -58.6657F;

                double doubleValue = 155.052654;
                double doubleValue1 = -44355.0882654;

                decimal decimalValue = 1.24550M;
                decimal decimalValue1 = -4987.564550M;

                Enum21 enumValue = Enum21.Value2;
                Enum21 enumValue1 = Enum21.Value4;

                Color colorValue = ColorTranslator.FromHtml("#f0ffff"); //Color.Azure
                Color colorValue1 = ColorTranslator.FromHtml("#f5f5dc"); //Color.Beige

                #endregion


                #region Define plain properties default values.

                bool defaultBool = default(bool);
                DateTime defaultDateTime = default(DateTime);
                TimeSpan defaultTimeSpanValue = default(TimeSpan);
                Guid defaultGuidValue = default(Guid);

                int defaultIntValue = default(int);
                short defaultInt16Value = default(short);
                long defaultInt64Value = default(long);

                uint defaultUintValue = default(uint);
                ushort defaultUint16Value = default(ushort);
                ulong defaultUint64Value = default(ulong);

                string defaultStringValue = default(string);
                char defaultCharValue = default(char);
                byte defaultByteValue = default(byte);
                float defaultFloatValue = default(float);
                double defaultDoubleValue = default(double);
                decimal defaultDecimalValue = default(decimal);
                Enum21 defaultEnumValue = default(Enum21);

                Color defaultColorValue = Color.FromArgb(255, 0, 0, 0);

                #endregion

                #endregion


                #region Auxiliary functions.

                #region Create plain properties object function.

                Func<bool, APlainProperties> createPlainPropertiesObject = (bool createNullProperties) =>
                {
                    #region Create not null values.

                    APlainProperties _value = new APlainProperties()
                    {
                        boolField = true,
                        boolProperty = true,

                        dateTimeField = dateTime,
                        dateTimeProperty = dateTime1,

                        timeSpanField = timeSpanValue,
                        timeSpanProperty = timeSpanValue1,

                        guidField = guidValue,
                        guidProperty = guidValue1,


                        iField = intValue,
                        iProperty = intValue1,

                        uiField = uintValue,
                        uiProperty = uintValue1,


                        i16Field = int16Value,
                        i16Property = int16Value1,

                        ui16Field = uint16Value,
                        ui16Property = uint16Value1,


                        i64Field = int64Value,
                        i64Property = int64Value1,

                        ui64Field = uint64Value,
                        ui64Property = uint64Value1,


                        stringField = stringValue,
                        stringProperty = stringValue1,

                        charField = charValue,
                        charProperty = charValue1,

                        byteField = byteValue,
                        byteProperty = byteValue1,

                        floatField = floatValue,
                        floatProperty = floatValue1,

                        doubleField = doubleValue,
                        doubleProperty = doubleValue1,

                        decimalField = decimalValue,
                        decimalProperty = decimalValue1,

                        enumField = enumValue,
                        enumProperty = enumValue1,

                        colorField = colorValue,
                        colorProperty = colorValue1,
                    };

                    #endregion


                    #region Possibly create null values.

                    if (createNullProperties)
                    {
                        _value.boolNullField = true;
                        _value.boolNullProperty = true;

                        _value.dateTimeNullField = dateTime;
                        _value.dateTimeNullProperty = dateTime1;

                        _value.timeSpanNullField = timeSpanValue;
                        _value.timeSpanNullProperty = timeSpanValue1;

                        _value.guidNullField = guidValue;
                        _value.guidNullProperty = guidValue1;


                        _value.iNullField = intValue;
                        _value.iNullProperty = intValue1;

                        _value.uiNullField = uintValue;
                        _value.uiNullProperty = uintValue1;


                        _value.i16NullField = int16Value;
                        _value.i16NullProperty = int16Value1;

                        _value.ui16NullField = uint16Value;
                        _value.ui16NullProperty = uint16Value1;


                        _value.i64NullField = int64Value;
                        _value.i64NullProperty = int64Value1;

                        _value.ui64NullField = uint64Value;
                        _value.ui64NullProperty = uint64Value1;


                        _value.byteNullField = byteValue;
                        _value.byteNullProperty = byteValue1;

                        _value.floatNullField = floatValue;
                        _value.floatNullProperty = floatValue1;

                        _value.doubleNullField = doubleValue;
                        _value.doubleNullProperty = doubleValue1;

                        _value.decimalNullField = decimalValue;
                        _value.decimalNullProperty = decimalValue1;

                        _value.enumNullField = enumValue;
                        _value.enumNullProperty = enumValue1;

                        _value.colorNullField = colorValue;
                        _value.colorNullProperty = colorValue1;
                    }

                    #endregion


                    return _value;
                };

                #endregion


                #region Compare plain values objects.

                Action<APlainProperties, APlainProperties, bool> comparePlainValuesObjects = (APlainProperties _value, APlainProperties _clonedValue, bool _compareNull) =>
                {
                    Assert.IsNotNull(_clonedValue);

                    #region Check not null values.

                    Assert.IsTrue(_value.boolField == _clonedValue.boolField);
                    Assert.IsTrue(_value.boolProperty == _clonedValue.boolProperty);

                    Assert.IsTrue(_value.dateTimeField == _clonedValue.dateTimeField);
                    Assert.IsTrue(_value.dateTimeProperty == _clonedValue.dateTimeProperty);

                    Assert.IsTrue(_value.timeSpanField == _clonedValue.timeSpanField);
                    Assert.IsTrue(_value.timeSpanProperty == _clonedValue.timeSpanProperty);

                    Assert.IsTrue(_value.guidField == _clonedValue.guidField);
                    Assert.IsTrue(_value.guidProperty == _clonedValue.guidProperty);


                    Assert.IsTrue(_value.iField == _clonedValue.iField);
                    Assert.IsTrue(_value.iProperty == _clonedValue.iProperty);

                    Assert.IsTrue(_value.uiField == _clonedValue.uiField);
                    Assert.IsTrue(_value.uiProperty == _clonedValue.uiProperty);


                    Assert.IsTrue(_value.i16Field == _clonedValue.i16Field);
                    Assert.IsTrue(_value.i16Property == _clonedValue.i16Property);

                    Assert.IsTrue(_value.ui16Field == _clonedValue.ui16Field);
                    Assert.IsTrue(_value.ui16Property == _clonedValue.ui16Property);


                    Assert.IsTrue(_value.i64Field == _clonedValue.i64Field);
                    Assert.IsTrue(_value.i64Property == _clonedValue.i64Property);

                    Assert.IsTrue(_value.ui64Field == _clonedValue.ui64Field);
                    Assert.IsTrue(_value.ui64Property == _clonedValue.ui64Property);


                    Assert.IsTrue(_value.stringField == _clonedValue.stringField);
                    Assert.IsTrue(_value.stringProperty == _clonedValue.stringProperty);

                    Assert.IsTrue(_value.charField == _clonedValue.charField);
                    Assert.IsTrue(_value.charProperty == _clonedValue.charProperty);

                    Assert.IsTrue(_value.colorField == _clonedValue.colorField);
                    Assert.IsTrue(_value.colorProperty == _clonedValue.colorProperty);

                    Assert.IsTrue(_value.byteField == _clonedValue.byteField);
                    Assert.IsTrue(_value.byteProperty == _clonedValue.byteProperty);

                    Assert.IsTrue(_value.floatField == _clonedValue.floatField);
                    Assert.IsTrue(_value.floatProperty == _clonedValue.floatProperty);

                    Assert.IsTrue(_value.doubleField == _clonedValue.doubleField);
                    Assert.IsTrue(_value.doubleProperty == _clonedValue.doubleProperty);

                    Assert.IsTrue(_value.decimalField == _clonedValue.decimalField);
                    Assert.IsTrue(_value.decimalProperty == _clonedValue.decimalProperty);

                    Assert.IsTrue(_value.enumField == _clonedValue.enumField);
                    Assert.IsTrue(_value.enumProperty == _clonedValue.enumProperty);

                    #endregion


                    #region Possibly compare null values.

                    if (_compareNull)
                    {
                        Assert.IsNotNull(_value.boolNullField);
                        Assert.IsNotNull(_value.boolNullProperty);

                        Assert.IsTrue(_value.boolNullField == _clonedValue.boolNullField);
                        Assert.IsTrue(_value.boolNullProperty == _clonedValue.boolNullProperty);


                        Assert.IsNotNull(_value.dateTimeNullField);
                        Assert.IsNotNull(_value.dateTimeNullProperty);

                        Assert.IsTrue(_value.dateTimeNullField == _clonedValue.dateTimeNullField);
                        Assert.IsTrue(_value.dateTimeNullProperty == _clonedValue.dateTimeNullProperty);


                        Assert.IsNotNull(_value.timeSpanNullField);
                        Assert.IsNotNull(_value.timeSpanNullProperty);

                        Assert.IsTrue(_value.timeSpanNullField == _clonedValue.timeSpanNullField);
                        Assert.IsTrue(_value.timeSpanNullProperty == _clonedValue.timeSpanNullProperty);


                        Assert.IsNotNull(_value.guidNullField);
                        Assert.IsNotNull(_value.guidNullProperty);

                        Assert.IsTrue(_value.guidNullField == _clonedValue.guidNullField);
                        Assert.IsTrue(_value.guidNullProperty == _clonedValue.guidNullProperty);


                        Assert.IsNotNull(_value.iNullField);
                        Assert.IsNotNull(_value.iNullProperty);

                        Assert.IsNotNull(_value.uiNullField);
                        Assert.IsNotNull(_value.uiNullProperty);


                        Assert.IsTrue(_value.iNullField == _clonedValue.iNullField);
                        Assert.IsTrue(_value.iNullProperty == _clonedValue.iNullProperty);

                        Assert.IsTrue(_value.uiNullField == _clonedValue.uiNullField);
                        Assert.IsTrue(_value.uiNullProperty == _clonedValue.uiNullProperty);


                        Assert.IsNotNull(_value.i16NullField);
                        Assert.IsNotNull(_value.i16NullProperty);

                        Assert.IsNotNull(_value.ui16NullField);
                        Assert.IsNotNull(_value.ui16NullProperty);


                        Assert.IsTrue(_value.i16NullField == _clonedValue.i16NullField);
                        Assert.IsTrue(_value.i16NullProperty == _clonedValue.i16NullProperty);

                        Assert.IsTrue(_value.ui16NullField == _clonedValue.ui16NullField);
                        Assert.IsTrue(_value.ui16NullProperty == _clonedValue.ui16NullProperty);


                        Assert.IsNotNull(_value.i64NullField);
                        Assert.IsNotNull(_value.i64NullProperty);

                        Assert.IsNotNull(_value.ui64NullField);
                        Assert.IsNotNull(_value.ui64NullProperty);


                        Assert.IsTrue(_value.i64NullField == _clonedValue.i64NullField);
                        Assert.IsTrue(_value.i64NullProperty == _clonedValue.i64NullProperty);

                        Assert.IsTrue(_value.ui64NullField == _clonedValue.ui64NullField);
                        Assert.IsTrue(_value.ui64NullProperty == _clonedValue.ui64NullProperty);


                        Assert.IsNotNull(_value.byteNullField);
                        Assert.IsNotNull(_value.byteNullProperty);

                        Assert.IsTrue(_value.byteNullField == _clonedValue.byteNullField);
                        Assert.IsTrue(_value.byteNullProperty == _clonedValue.byteNullProperty);


                        Assert.IsNotNull(_value.floatNullField);
                        Assert.IsNotNull(_value.floatNullProperty);

                        Assert.IsTrue(_value.floatNullField == _clonedValue.floatNullField);
                        Assert.IsTrue(_value.floatNullProperty == _clonedValue.floatNullProperty);


                        Assert.IsNotNull(_value.doubleNullField);
                        Assert.IsNotNull(_value.doubleNullProperty);

                        Assert.IsTrue(_value.doubleNullField == _clonedValue.doubleNullField);
                        Assert.IsTrue(_value.doubleNullProperty == _clonedValue.doubleNullProperty);


                        Assert.IsNotNull(_value.decimalNullField);
                        Assert.IsNotNull(_value.decimalNullProperty);

                        Assert.IsTrue(_value.decimalNullField == _clonedValue.decimalNullField);
                        Assert.IsTrue(_value.decimalNullProperty == _clonedValue.decimalNullProperty);

                        Assert.IsTrue(_value.enumNullField == _clonedValue.enumNullField);
                        Assert.IsTrue(_value.enumNullProperty == _clonedValue.enumNullProperty);

                        Assert.IsTrue(_value.colorNullField == _clonedValue.colorNullField);
                        Assert.IsTrue(_value.colorNullProperty == _clonedValue.colorNullProperty);
                    }

                    #endregion


                    #region Otherwise, check that values are null.

                    else
                    {
                        Assert.IsNull(_value.boolNullField);
                        Assert.IsNull(_value.boolNullProperty);

                        Assert.IsNull(_value.dateTimeNullField);
                        Assert.IsNull(_value.dateTimeNullProperty);

                        Assert.IsNull(_value.timeSpanNullField);
                        Assert.IsNull(_value.timeSpanNullProperty);

                        Assert.IsNull(_value.guidNullField);
                        Assert.IsNull(_value.guidNullProperty);


                        Assert.IsNull(_value.iNullField);
                        Assert.IsNull(_value.iNullProperty);

                        Assert.IsNull(_value.uiNullField);
                        Assert.IsNull(_value.uiNullProperty);


                        Assert.IsNull(_value.i16NullField);
                        Assert.IsNull(_value.i16NullProperty);

                        Assert.IsNull(_value.ui16NullField);
                        Assert.IsNull(_value.ui16NullProperty);


                        Assert.IsNull(_value.i64NullField);
                        Assert.IsNull(_value.i64NullProperty);

                        Assert.IsNull(_value.ui64NullField);
                        Assert.IsNull(_value.ui64NullProperty);


                        Assert.IsNull(_value.byteNullField);
                        Assert.IsNull(_value.byteNullProperty);

                        Assert.IsNull(_value.floatNullField);
                        Assert.IsNull(_value.floatNullProperty);

                        Assert.IsNull(_value.doubleNullField);
                        Assert.IsNull(_value.doubleNullProperty);

                        Assert.IsNull(_value.decimalNullField);
                        Assert.IsNull(_value.decimalNullProperty);

                        Assert.IsNull(_value.enumNullField);
                        Assert.IsNull(_value.enumNullProperty);

                        Assert.IsNull(_value.colorNullField);
                        Assert.IsNull(_value.colorNullProperty);
                    }

                    #endregion
                };

                #endregion



                #region Set default plain properties function.

                Action<APlainProperties, bool> setDefaultPlainProperties = (APlainProperties _value, bool createNullProperties) =>
                {
                    #region Create not null values.

                    _value.boolField = defaultBool;
                    _value.boolProperty = defaultBool;

                    _value.dateTimeField = defaultDateTime;
                    _value.dateTimeProperty = defaultDateTime;

                    _value.timeSpanField = defaultTimeSpanValue;
                    _value.timeSpanProperty = defaultTimeSpanValue;

                    _value.guidField = defaultGuidValue;
                    _value.guidProperty = defaultGuidValue;


                    _value.iField = defaultIntValue;
                    _value.iProperty = defaultIntValue;

                    _value.uiField = defaultUintValue;
                    _value.uiProperty = defaultUintValue;


                    _value.i16Field = defaultInt16Value;
                    _value.i16Property = defaultInt16Value;

                    _value.ui16Field = defaultUint16Value;
                    _value.ui16Property = defaultUint16Value;


                    _value.i64Field = defaultInt64Value;
                    _value.i64Property = defaultInt64Value;

                    _value.ui64Field = defaultUint64Value;
                    _value.ui64Property = defaultUint64Value;


                    _value.stringField = defaultStringValue;
                    _value.stringProperty = defaultStringValue;

                    _value.charField = defaultCharValue;
                    _value.charProperty = defaultCharValue;

                    _value.colorField = defaultColorValue;
                    _value.colorProperty = defaultColorValue;

                    _value.byteField = defaultByteValue;
                    _value.byteProperty = defaultByteValue;

                    _value.floatField = defaultFloatValue;
                    _value.floatProperty = defaultFloatValue;

                    _value.doubleField = defaultDoubleValue;
                    _value.doubleProperty = defaultDoubleValue;

                    _value.decimalField = defaultDecimalValue;
                    _value.decimalProperty = defaultDecimalValue;

                    _value.enumField = defaultEnumValue;
                    _value.enumProperty = defaultEnumValue;

                    #endregion


                    #region Possibly create null values.

                    if (createNullProperties)
                    {
                        _value.boolNullField = null;
                        _value.boolNullProperty = null;

                        _value.dateTimeNullField = null;
                        _value.dateTimeNullProperty = null;

                        _value.timeSpanNullField = null;
                        _value.timeSpanNullProperty = null;

                        _value.guidNullField = null;
                        _value.guidNullProperty = null;


                        _value.iNullField = null;
                        _value.iNullProperty = null;

                        _value.uiNullField = null;
                        _value.uiNullProperty = null;


                        _value.i16NullField = null;
                        _value.i16NullProperty = null;

                        _value.ui16NullField = null;
                        _value.ui16NullProperty = null;


                        _value.i64NullField = null;
                        _value.i64NullProperty = null;

                        _value.ui64NullField = null;
                        _value.ui64NullProperty = null;


                        _value.byteNullField = null;
                        _value.byteNullProperty = null;

                        _value.floatNullField = null;
                        _value.floatNullProperty = null;

                        _value.doubleNullField = null;
                        _value.doubleNullProperty = null;

                        _value.decimalNullField = null;
                        _value.decimalNullProperty = null;

                        _value.enumNullField = null;
                        _value.enumNullProperty = null;

                        _value.colorNullField = null;
                        _value.colorNullProperty = null;
                    }

                    #endregion
                };

                #endregion


                #region Checks that default plain properties are set.

                Action<APlainProperties, bool> checkDefaultPlainProperties = (APlainProperties _value, bool checkNullProperties) =>
                {
                    #region Check not null values.

                    Assert.IsTrue(_value.boolField == defaultBool);
                    Assert.IsTrue(_value.boolProperty == defaultBool);

                    Assert.IsTrue(_value.dateTimeField == defaultDateTime);
                    Assert.IsTrue(_value.dateTimeProperty == defaultDateTime);

                    Assert.IsTrue(_value.timeSpanField == defaultTimeSpanValue);
                    Assert.IsTrue(_value.timeSpanProperty == defaultTimeSpanValue);

                    Assert.IsTrue(_value.guidField == defaultGuidValue);
                    Assert.IsTrue(_value.guidProperty == defaultGuidValue);


                    Assert.IsTrue(_value.iField == defaultIntValue);
                    Assert.IsTrue(_value.iProperty == defaultIntValue);

                    Assert.IsTrue(_value.uiField == defaultUintValue);
                    Assert.IsTrue(_value.uiProperty == defaultUintValue);


                    Assert.IsTrue(_value.i16Field == defaultInt16Value);
                    Assert.IsTrue(_value.i16Property == defaultInt16Value);

                    Assert.IsTrue(_value.ui16Field == defaultUint16Value);
                    Assert.IsTrue(_value.ui16Property == defaultUint16Value);


                    Assert.IsTrue(_value.i64Field == defaultInt64Value);
                    Assert.IsTrue(_value.i64Property == defaultInt64Value);

                    Assert.IsTrue(_value.ui64Field == defaultUint64Value);
                    Assert.IsTrue(_value.ui64Property == defaultUint64Value);


                    Assert.IsTrue(_value.stringField == defaultStringValue);
                    Assert.IsTrue(_value.stringProperty == defaultStringValue);

                    Assert.IsTrue(_value.charField == defaultCharValue);
                    Assert.IsTrue(_value.charProperty == defaultCharValue);

                    Assert.IsTrue(_value.colorField == defaultColorValue);
                    Assert.IsTrue(_value.colorProperty == defaultColorValue);

                    Assert.IsTrue(_value.byteField == defaultByteValue);
                    Assert.IsTrue(_value.byteProperty == defaultByteValue);

                    Assert.IsTrue(_value.floatField == defaultFloatValue);
                    Assert.IsTrue(_value.floatProperty == defaultFloatValue);

                    Assert.IsTrue(_value.doubleField == defaultDoubleValue);
                    Assert.IsTrue(_value.doubleProperty == defaultDoubleValue);

                    Assert.IsTrue(_value.decimalField == defaultDecimalValue);
                    Assert.IsTrue(_value.decimalProperty == defaultDecimalValue);

                    Assert.IsTrue(_value.enumField == defaultEnumValue);
                    Assert.IsTrue(_value.enumProperty == defaultEnumValue);

                    #endregion


                    #region Possibly check null values.

                    if (checkNullProperties)
                    {
                        Assert.IsTrue(_value.boolNullField == null);
                        Assert.IsTrue(_value.boolNullProperty == null);

                        Assert.IsTrue(_value.dateTimeNullField == null);
                        Assert.IsTrue(_value.dateTimeNullProperty == null);

                        Assert.IsTrue(_value.timeSpanNullField == null);
                        Assert.IsTrue(_value.timeSpanNullProperty == null);

                        Assert.IsTrue(_value.guidNullField == null);
                        Assert.IsTrue(_value.guidNullProperty == null);


                        Assert.IsTrue(_value.iNullField == null);
                        Assert.IsTrue(_value.iNullProperty == null);

                        Assert.IsTrue(_value.uiNullField == null);
                        Assert.IsTrue(_value.uiNullProperty == null);


                        Assert.IsTrue(_value.i16NullField == null);
                        Assert.IsTrue(_value.i16NullProperty == null);

                        Assert.IsTrue(_value.ui16NullField == null);
                        Assert.IsTrue(_value.ui16NullProperty == null);


                        Assert.IsTrue(_value.i64NullField == null);
                        Assert.IsTrue(_value.i64NullProperty == null);

                        Assert.IsTrue(_value.ui64NullField == null);
                        Assert.IsTrue(_value.ui64NullProperty == null);


                        Assert.IsTrue(_value.byteNullField == null);
                        Assert.IsTrue(_value.byteNullProperty == null);

                        Assert.IsTrue(_value.floatNullField == null);
                        Assert.IsTrue(_value.floatNullProperty == null);

                        Assert.IsTrue(_value.doubleNullField == null);
                        Assert.IsTrue(_value.doubleNullProperty == null);

                        Assert.IsTrue(_value.decimalNullField == null);
                        Assert.IsTrue(_value.decimalNullProperty == null);

                        Assert.IsTrue(_value.enumNullField == null);
                        Assert.IsTrue(_value.enumNullProperty == null);

                        Assert.IsTrue(_value.colorNullField == null);
                        Assert.IsTrue(_value.colorNullProperty == null);
                    }

                    #endregion
                };

                #endregion




                #region Create object plain properties object function.

                Func<bool, AObjectPlainProperties> createObjectPlainPropertiesObject = (bool createNullProperties) =>
                {
                    #region Create not null values.

                    AObjectPlainProperties _value = new AObjectPlainProperties()
                    {
                        boolField = true,
                        boolProperty = true,

                        dateTimeField = dateTime,
                        dateTimeProperty = dateTime1,

                        timeSpanField = timeSpanValue,
                        timeSpanProperty = timeSpanValue1,

                        guidField = guidValue,
                        guidProperty = guidValue1,


                        iField = intValue,
                        iProperty = intValue1,

                        uiField = uintValue,
                        uiProperty = uintValue1,


                        i16Field = int16Value,
                        i16Property = int16Value1,

                        ui16Field = uint16Value,
                        ui16Property = uint16Value1,


                        i64Field = int64Value,
                        i64Property = int64Value1,

                        ui64Field = uint64Value,
                        ui64Property = uint64Value1,


                        stringField = stringValue,
                        stringProperty = stringValue1,

                        charField = charValue,
                        charProperty = charValue1,

                        colorField = colorValue,
                        colorProperty = colorValue1,

                        byteField = byteValue,
                        byteProperty = byteValue1,

                        floatField = floatValue,
                        floatProperty = floatValue1,

                        doubleField = doubleValue,
                        doubleProperty = doubleValue1,

                        decimalField = decimalValue,
                        decimalProperty = decimalValue1,

                        enumField = enumValue,
                        enumProperty = enumValue1,
                    };

                    #endregion


                    #region Possibly create null values.

                    if (createNullProperties)
                    {
                        _value.boolNullField = true;
                        _value.boolNullProperty = true;

                        _value.dateTimeNullField = dateTime;
                        _value.dateTimeNullProperty = dateTime1;

                        _value.timeSpanNullField = timeSpanValue;
                        _value.timeSpanNullProperty = timeSpanValue1;

                        _value.guidNullField = guidValue;
                        _value.guidNullProperty = guidValue1;


                        _value.iNullField = intValue;
                        _value.iNullProperty = intValue1;

                        _value.uiNullField = uintValue;
                        _value.uiNullProperty = uintValue1;


                        _value.i16NullField = int16Value;
                        _value.i16NullProperty = int16Value1;

                        _value.ui16NullField = uint16Value;
                        _value.ui16NullProperty = uint16Value1;


                        _value.i64NullField = int64Value;
                        _value.i64NullProperty = int64Value1;

                        _value.ui64NullField = uint64Value;
                        _value.ui64NullProperty = uint64Value1;


                        _value.byteNullField = byteValue;
                        _value.byteNullProperty = byteValue1;

                        _value.floatNullField = floatValue;
                        _value.floatNullProperty = floatValue1;

                        _value.doubleNullField = doubleValue;
                        _value.doubleNullProperty = doubleValue1;

                        _value.decimalNullField = decimalValue;
                        _value.decimalNullProperty = decimalValue1;

                        _value.enumNullField = enumValue;
                        _value.enumNullProperty = enumValue1;

                        _value.colorNullField = colorValue;
                        _value.colorNullProperty = colorValue1;
                    }

                    #endregion


                    return _value;
                };

                #endregion


                #region Compare object plain values objects (AObjectPlainProperties, APlainProperties).

                Action<AObjectPlainProperties, APlainProperties, bool> compareObjectAndTypedPlainValuesObjects = (AObjectPlainProperties _value, APlainProperties _clonedValue, bool _compareNull) =>
                {
                    Assert.IsNotNull(_clonedValue);

                    #region Check not null values.

                    Assert.IsTrue(_value.boolField.Equals(_clonedValue.boolField));
                    Assert.IsTrue(_value.boolProperty.Equals(_clonedValue.boolProperty));

                    Assert.IsTrue(_value.dateTimeField.Equals(_clonedValue.dateTimeField));
                    Assert.IsTrue(_value.dateTimeProperty.Equals(_clonedValue.dateTimeProperty));

                    Assert.IsTrue(_value.timeSpanField.Equals(_clonedValue.timeSpanField));
                    Assert.IsTrue(_value.timeSpanProperty.Equals(_clonedValue.timeSpanProperty));

                    Assert.IsTrue(_value.guidField.Equals(_clonedValue.guidField));
                    Assert.IsTrue(_value.guidProperty.Equals(_clonedValue.guidProperty));


                    Assert.IsTrue(_value.iField.Equals(_clonedValue.iField));
                    Assert.IsTrue(_value.iProperty.Equals(_clonedValue.iProperty));

                    Assert.IsTrue(_value.uiField.Equals(_clonedValue.uiField));
                    Assert.IsTrue(_value.uiProperty.Equals(_clonedValue.uiProperty));


                    Assert.IsTrue(_value.i16Field.Equals(_clonedValue.i16Field));
                    Assert.IsTrue(_value.i16Property.Equals(_clonedValue.i16Property));

                    Assert.IsTrue(_value.ui16Field.Equals(_clonedValue.ui16Field));
                    Assert.IsTrue(_value.ui16Property.Equals(_clonedValue.ui16Property));


                    Assert.IsTrue(_value.i64Field.Equals(_clonedValue.i64Field));
                    Assert.IsTrue(_value.i64Property.Equals(_clonedValue.i64Property));

                    Assert.IsTrue(_value.ui64Field.Equals(_clonedValue.ui64Field));
                    Assert.IsTrue(_value.ui64Property.Equals(_clonedValue.ui64Property));


                    Assert.IsTrue(_value.stringField.Equals(_clonedValue.stringField));
                    Assert.IsTrue(_value.stringProperty.Equals(_clonedValue.stringProperty));

                    Assert.IsTrue(_value.charField.Equals(_clonedValue.charField));
                    Assert.IsTrue(_value.charProperty.Equals(_clonedValue.charProperty));

                    Assert.IsTrue(_value.colorField.Equals(_clonedValue.colorField));
                    Assert.IsTrue(_value.colorProperty.Equals(_clonedValue.colorProperty));

                    Assert.IsTrue(_value.byteField.Equals(_clonedValue.byteField));
                    Assert.IsTrue(_value.byteProperty.Equals(_clonedValue.byteProperty));

                    Assert.IsTrue(_value.floatField.Equals(_clonedValue.floatField));
                    Assert.IsTrue(_value.floatProperty.Equals(_clonedValue.floatProperty));

                    Assert.IsTrue(_value.doubleField.Equals(_clonedValue.doubleField));
                    Assert.IsTrue(_value.doubleProperty.Equals(_clonedValue.doubleProperty));

                    Assert.IsTrue(_value.decimalField.Equals(_clonedValue.decimalField));
                    Assert.IsTrue(_value.decimalProperty.Equals(_clonedValue.decimalProperty));

                    Assert.IsTrue((Enum21)_value.enumField == (Enum21)_clonedValue.enumField);
                    Assert.IsTrue((Enum21)_value.enumProperty == (Enum21)_clonedValue.enumProperty);

                    #endregion


                    #region Possibly compare null values.

                    if (_compareNull)
                    {
                        Assert.IsNotNull(_value.boolNullField);
                        Assert.IsNotNull(_value.boolNullProperty);

                        Assert.IsTrue(_value.boolNullField.Equals(_clonedValue.boolNullField));
                        Assert.IsTrue(_value.boolNullProperty.Equals(_clonedValue.boolNullProperty));


                        Assert.IsNotNull(_value.dateTimeNullField);
                        Assert.IsNotNull(_value.dateTimeNullProperty);

                        Assert.IsTrue(_value.dateTimeNullField.Equals(_clonedValue.dateTimeNullField));
                        Assert.IsTrue(_value.dateTimeNullProperty.Equals(_clonedValue.dateTimeNullProperty));


                        Assert.IsNotNull(_value.timeSpanNullField);
                        Assert.IsNotNull(_value.timeSpanNullProperty);

                        Assert.IsTrue(_value.timeSpanNullField.Equals(_clonedValue.timeSpanNullField));
                        Assert.IsTrue(_value.timeSpanNullProperty.Equals(_clonedValue.timeSpanNullProperty));


                        Assert.IsNotNull(_value.guidNullField);
                        Assert.IsNotNull(_value.guidNullProperty);

                        Assert.IsTrue(_value.guidNullField.Equals(_clonedValue.guidNullField));
                        Assert.IsTrue(_value.guidNullProperty.Equals(_clonedValue.guidNullProperty));


                        Assert.IsNotNull(_value.iNullField);
                        Assert.IsNotNull(_value.iNullProperty);

                        Assert.IsNotNull(_value.uiNullField);
                        Assert.IsNotNull(_value.uiNullProperty);


                        Assert.IsTrue(_value.iNullField.Equals(_clonedValue.iNullField));
                        Assert.IsTrue(_value.iNullProperty.Equals(_clonedValue.iNullProperty));

                        Assert.IsTrue(_value.uiNullField.Equals(_clonedValue.uiNullField));
                        Assert.IsTrue(_value.uiNullProperty.Equals(_clonedValue.uiNullProperty));


                        Assert.IsNotNull(_value.i16NullField);
                        Assert.IsNotNull(_value.i16NullProperty);

                        Assert.IsNotNull(_value.ui16NullField);
                        Assert.IsNotNull(_value.ui16NullProperty);


                        Assert.IsTrue(_value.i16NullField.Equals(_clonedValue.i16NullField));
                        Assert.IsTrue(_value.i16NullProperty.Equals(_clonedValue.i16NullProperty));

                        Assert.IsTrue(_value.ui16NullField.Equals(_clonedValue.ui16NullField));
                        Assert.IsTrue(_value.ui16NullProperty.Equals(_clonedValue.ui16NullProperty));


                        Assert.IsNotNull(_value.i64NullField);
                        Assert.IsNotNull(_value.i64NullProperty);

                        Assert.IsNotNull(_value.ui64NullField);
                        Assert.IsNotNull(_value.ui64NullProperty);


                        Assert.IsTrue(_value.i64NullField.Equals(_clonedValue.i64NullField));
                        Assert.IsTrue(_value.i64NullProperty.Equals(_clonedValue.i64NullProperty));

                        Assert.IsTrue(_value.ui64NullField.Equals(_clonedValue.ui64NullField));
                        Assert.IsTrue(_value.ui64NullProperty.Equals(_clonedValue.ui64NullProperty));


                        Assert.IsNotNull(_value.byteNullField);
                        Assert.IsNotNull(_value.byteNullProperty);

                        Assert.IsTrue(_value.byteNullField.Equals(_clonedValue.byteNullField));
                        Assert.IsTrue(_value.byteNullProperty.Equals(_clonedValue.byteNullProperty));


                        Assert.IsNotNull(_value.floatNullField);
                        Assert.IsNotNull(_value.floatNullProperty);

                        Assert.IsTrue(_value.floatNullField.Equals(_clonedValue.floatNullField));
                        Assert.IsTrue(_value.floatNullProperty.Equals(_clonedValue.floatNullProperty));


                        Assert.IsNotNull(_value.doubleNullField);
                        Assert.IsNotNull(_value.doubleNullProperty);

                        Assert.IsTrue(_value.doubleNullField.Equals(_clonedValue.doubleNullField));
                        Assert.IsTrue(_value.doubleNullProperty.Equals(_clonedValue.doubleNullProperty));


                        Assert.IsNotNull(_value.decimalNullField);
                        Assert.IsNotNull(_value.decimalNullProperty);

                        Assert.IsTrue(_value.decimalNullField.Equals(_clonedValue.decimalNullField));
                        Assert.IsTrue(_value.decimalNullProperty.Equals(_clonedValue.decimalNullProperty));


                        Assert.IsNotNull(_value.enumNullField);
                        Assert.IsNotNull(_value.enumNullProperty);

                        Assert.IsTrue((Enum21)_value.enumNullField == (Enum21)_clonedValue.enumNullField);
                        Assert.IsTrue((Enum21)_value.enumNullProperty == (Enum21)_clonedValue.enumNullProperty);


                        Assert.IsNotNull(_value.colorNullField);
                        Assert.IsNotNull(_value.colorNullProperty);

                        Assert.IsTrue(_value.colorNullField.Equals(_clonedValue.colorNullField));
                        Assert.IsTrue(_value.colorNullProperty.Equals(_clonedValue.colorNullProperty));
                    }

                    #endregion


                    #region Otherwise, check that values are null.

                    else
                    {
                        Assert.IsNull(_value.boolNullField);
                        Assert.IsNull(_value.boolNullProperty);

                        Assert.IsNull(_value.dateTimeNullField);
                        Assert.IsNull(_value.dateTimeNullProperty);

                        Assert.IsNull(_value.timeSpanNullField);
                        Assert.IsNull(_value.timeSpanNullProperty);

                        Assert.IsNull(_value.guidNullField);
                        Assert.IsNull(_value.guidNullProperty);


                        Assert.IsNull(_value.iNullField);
                        Assert.IsNull(_value.iNullProperty);

                        Assert.IsNull(_value.uiNullField);
                        Assert.IsNull(_value.uiNullProperty);


                        Assert.IsNull(_value.i16NullField);
                        Assert.IsNull(_value.i16NullProperty);

                        Assert.IsNull(_value.ui16NullField);
                        Assert.IsNull(_value.ui16NullProperty);


                        Assert.IsNull(_value.i64NullField);
                        Assert.IsNull(_value.i64NullProperty);

                        Assert.IsNull(_value.ui64NullField);
                        Assert.IsNull(_value.ui64NullProperty);


                        Assert.IsNull(_value.byteNullField);
                        Assert.IsNull(_value.byteNullProperty);

                        Assert.IsNull(_value.floatNullField);
                        Assert.IsNull(_value.floatNullProperty);

                        Assert.IsNull(_value.doubleNullField);
                        Assert.IsNull(_value.doubleNullProperty);

                        Assert.IsNull(_value.decimalNullField);
                        Assert.IsNull(_value.decimalNullProperty);

                        Assert.IsNull(_value.enumNullField);
                        Assert.IsNull(_value.enumNullProperty);

                        Assert.IsNull(_value.colorNullField);
                        Assert.IsNull(_value.colorNullProperty);
                    }

                    #endregion
                };

                #endregion


                #region Compare object plain values objects (AObjectPlainProperties, AObjectPlainProperties).

                Action<AObjectPlainProperties, AObjectPlainProperties, bool> compareObjectPlainValuesObjects = (AObjectPlainProperties _value, AObjectPlainProperties _clonedValue, bool _compareNull) =>
                {
                    Assert.IsNotNull(_clonedValue);

                    #region Check not null values.

                    Assert.IsTrue(_value.boolField.Equals(_clonedValue.boolField));
                    Assert.IsTrue(_value.boolProperty.Equals(_clonedValue.boolProperty));

                    Assert.IsTrue(_value.dateTimeField.Equals(_clonedValue.dateTimeField));
                    Assert.IsTrue(_value.dateTimeProperty.Equals(_clonedValue.dateTimeProperty));

                    Assert.IsTrue(_value.timeSpanField.Equals(_clonedValue.timeSpanField));
                    Assert.IsTrue(_value.timeSpanProperty.Equals(_clonedValue.timeSpanProperty));

                    Assert.IsTrue(_value.guidField.Equals(_clonedValue.guidField));
                    Assert.IsTrue(_value.guidProperty.Equals(_clonedValue.guidProperty));


                    Assert.IsTrue(_value.iField.Equals(_clonedValue.iField));
                    Assert.IsTrue(_value.iProperty.Equals(_clonedValue.iProperty));

                    Assert.IsTrue(_value.uiField.Equals(_clonedValue.uiField));
                    Assert.IsTrue(_value.uiProperty.Equals(_clonedValue.uiProperty));


                    Assert.IsTrue(_value.i16Field.Equals(_clonedValue.i16Field));
                    Assert.IsTrue(_value.i16Property.Equals(_clonedValue.i16Property));

                    Assert.IsTrue(_value.ui16Field.Equals(_clonedValue.ui16Field));
                    Assert.IsTrue(_value.ui16Property.Equals(_clonedValue.ui16Property));


                    Assert.IsTrue(_value.i64Field.Equals(_clonedValue.i64Field));
                    Assert.IsTrue(_value.i64Property.Equals(_clonedValue.i64Property));

                    Assert.IsTrue(_value.ui64Field.Equals(_clonedValue.ui64Field));
                    Assert.IsTrue(_value.ui64Property.Equals(_clonedValue.ui64Property));


                    Assert.IsTrue(_value.stringField.Equals(_clonedValue.stringField));
                    Assert.IsTrue(_value.stringProperty.Equals(_clonedValue.stringProperty));

                    Assert.IsTrue(_value.charField.Equals(_clonedValue.charField));
                    Assert.IsTrue(_value.charProperty.Equals(_clonedValue.charProperty));

                    Assert.IsTrue(_value.colorField.Equals(_clonedValue.colorField));
                    Assert.IsTrue(_value.colorProperty.Equals(_clonedValue.colorProperty));

                    Assert.IsTrue(_value.byteField.Equals(_clonedValue.byteField));
                    Assert.IsTrue(_value.byteProperty.Equals(_clonedValue.byteProperty));

                    Assert.IsTrue(_value.floatField.Equals(_clonedValue.floatField));
                    Assert.IsTrue(_value.floatProperty.Equals(_clonedValue.floatProperty));

                    Assert.IsTrue(_value.doubleField.Equals(_clonedValue.doubleField));
                    Assert.IsTrue(_value.doubleProperty.Equals(_clonedValue.doubleProperty));

                    Assert.IsTrue(_value.decimalField.Equals(_clonedValue.decimalField));
                    Assert.IsTrue(_value.decimalProperty.Equals(_clonedValue.decimalProperty));

                    Assert.IsTrue((Enum21)_value.enumField == (Enum21)_clonedValue.enumField);
                    Assert.IsTrue((Enum21)_value.enumProperty == (Enum21)_clonedValue.enumProperty);

                    #endregion


                    #region Possibly compare null values.

                    if (_compareNull)
                    {
                        Assert.IsNotNull(_value.boolNullField);
                        Assert.IsNotNull(_value.boolNullProperty);

                        Assert.IsTrue(_value.boolNullField.Equals(_clonedValue.boolNullField));
                        Assert.IsTrue(_value.boolNullProperty.Equals(_clonedValue.boolNullProperty));


                        Assert.IsNotNull(_value.dateTimeNullField);
                        Assert.IsNotNull(_value.dateTimeNullProperty);

                        Assert.IsTrue(_value.dateTimeNullField.Equals(_clonedValue.dateTimeNullField));
                        Assert.IsTrue(_value.dateTimeNullProperty.Equals(_clonedValue.dateTimeNullProperty));


                        Assert.IsNotNull(_value.timeSpanNullField);
                        Assert.IsNotNull(_value.timeSpanNullProperty);

                        Assert.IsTrue(_value.timeSpanNullField.Equals(_clonedValue.timeSpanNullField));
                        Assert.IsTrue(_value.timeSpanNullProperty.Equals(_clonedValue.timeSpanNullProperty));


                        Assert.IsNotNull(_value.guidNullField);
                        Assert.IsNotNull(_value.guidNullProperty);

                        Assert.IsTrue(_value.guidNullField.Equals(_clonedValue.guidNullField));
                        Assert.IsTrue(_value.guidNullProperty.Equals(_clonedValue.guidNullProperty));


                        Assert.IsNotNull(_value.iNullField);
                        Assert.IsNotNull(_value.iNullProperty);

                        Assert.IsNotNull(_value.uiNullField);
                        Assert.IsNotNull(_value.uiNullProperty);


                        Assert.IsTrue(_value.iNullField.Equals(_clonedValue.iNullField));
                        Assert.IsTrue(_value.iNullProperty.Equals(_clonedValue.iNullProperty));

                        Assert.IsTrue(_value.uiNullField.Equals(_clonedValue.uiNullField));
                        Assert.IsTrue(_value.uiNullProperty.Equals(_clonedValue.uiNullProperty));


                        Assert.IsNotNull(_value.i16NullField);
                        Assert.IsNotNull(_value.i16NullProperty);

                        Assert.IsNotNull(_value.ui16NullField);
                        Assert.IsNotNull(_value.ui16NullProperty);


                        Assert.IsTrue(_value.i16NullField.Equals(_clonedValue.i16NullField));
                        Assert.IsTrue(_value.i16NullProperty.Equals(_clonedValue.i16NullProperty));

                        Assert.IsTrue(_value.ui16NullField.Equals(_clonedValue.ui16NullField));
                        Assert.IsTrue(_value.ui16NullProperty.Equals(_clonedValue.ui16NullProperty));


                        Assert.IsNotNull(_value.i64NullField);
                        Assert.IsNotNull(_value.i64NullProperty);

                        Assert.IsNotNull(_value.ui64NullField);
                        Assert.IsNotNull(_value.ui64NullProperty);


                        Assert.IsTrue(_value.i64NullField.Equals(_clonedValue.i64NullField));
                        Assert.IsTrue(_value.i64NullProperty.Equals(_clonedValue.i64NullProperty));

                        Assert.IsTrue(_value.ui64NullField.Equals(_clonedValue.ui64NullField));
                        Assert.IsTrue(_value.ui64NullProperty.Equals(_clonedValue.ui64NullProperty));


                        Assert.IsNotNull(_value.byteNullField);
                        Assert.IsNotNull(_value.byteNullProperty);

                        Assert.IsTrue(_value.byteNullField.Equals(_clonedValue.byteNullField));
                        Assert.IsTrue(_value.byteNullProperty.Equals(_clonedValue.byteNullProperty));


                        Assert.IsNotNull(_value.floatNullField);
                        Assert.IsNotNull(_value.floatNullProperty);

                        Assert.IsTrue(_value.floatNullField.Equals(_clonedValue.floatNullField));
                        Assert.IsTrue(_value.floatNullProperty.Equals(_clonedValue.floatNullProperty));


                        Assert.IsNotNull(_value.doubleNullField);
                        Assert.IsNotNull(_value.doubleNullProperty);

                        Assert.IsTrue(_value.doubleNullField.Equals(_clonedValue.doubleNullField));
                        Assert.IsTrue(_value.doubleNullProperty.Equals(_clonedValue.doubleNullProperty));


                        Assert.IsNotNull(_value.decimalNullField);
                        Assert.IsNotNull(_value.decimalNullProperty);

                        Assert.IsTrue(_value.decimalNullField.Equals(_clonedValue.decimalNullField));
                        Assert.IsTrue(_value.decimalNullProperty.Equals(_clonedValue.decimalNullProperty));


                        Assert.IsNotNull(_value.enumNullField);
                        Assert.IsNotNull(_value.enumNullProperty);

                        Assert.IsTrue((Enum21)_value.enumNullField == (Enum21)_clonedValue.enumNullField);
                        Assert.IsTrue((Enum21)_value.enumNullProperty == (Enum21)_clonedValue.enumNullProperty);


                        Assert.IsNotNull(_value.colorNullField);
                        Assert.IsNotNull(_value.colorNullProperty);

                        Assert.IsTrue(_value.colorNullField.Equals(_clonedValue.colorNullField));
                        Assert.IsTrue(_value.colorNullProperty.Equals(_clonedValue.colorNullProperty));
                    }

                    #endregion


                    #region Otherwise, check that values are null.

                    else
                    {
                        Assert.IsNull(_value.boolNullField);
                        Assert.IsNull(_value.boolNullProperty);

                        Assert.IsNull(_value.dateTimeNullField);
                        Assert.IsNull(_value.dateTimeNullProperty);

                        Assert.IsNull(_value.timeSpanNullField);
                        Assert.IsNull(_value.timeSpanNullProperty);

                        Assert.IsNull(_value.guidNullField);
                        Assert.IsNull(_value.guidNullProperty);


                        Assert.IsNull(_value.iNullField);
                        Assert.IsNull(_value.iNullProperty);

                        Assert.IsNull(_value.uiNullField);
                        Assert.IsNull(_value.uiNullProperty);


                        Assert.IsNull(_value.i16NullField);
                        Assert.IsNull(_value.i16NullProperty);

                        Assert.IsNull(_value.ui16NullField);
                        Assert.IsNull(_value.ui16NullProperty);


                        Assert.IsNull(_value.i64NullField);
                        Assert.IsNull(_value.i64NullProperty);

                        Assert.IsNull(_value.ui64NullField);
                        Assert.IsNull(_value.ui64NullProperty);


                        Assert.IsNull(_value.byteNullField);
                        Assert.IsNull(_value.byteNullProperty);

                        Assert.IsNull(_value.floatNullField);
                        Assert.IsNull(_value.floatNullProperty);

                        Assert.IsNull(_value.doubleNullField);
                        Assert.IsNull(_value.doubleNullProperty);

                        Assert.IsNull(_value.decimalNullField);
                        Assert.IsNull(_value.decimalNullProperty);

                        Assert.IsNull(_value.enumNullField);
                        Assert.IsNull(_value.enumNullProperty);

                        Assert.IsNull(_value.colorNullField);
                        Assert.IsNull(_value.colorNullProperty);
                    }

                    #endregion
                };

                #endregion



                #region Create plain properties object function in structures.

                Func<bool, AStructPlainProperties> createPlainPropertiesStruct = (bool createNullProperties) =>
                {
                    #region Create not null values.

                    AStructPlainProperties _value = new AStructPlainProperties()
                    {
                        boolField = true,
                        boolProperty = true,

                        dateTimeField = dateTime,
                        dateTimeProperty = dateTime1,

                        timeSpanField = timeSpanValue,
                        timeSpanProperty = timeSpanValue1,

                        guidField = guidValue,
                        guidProperty = guidValue1,


                        iField = intValue,
                        iProperty = intValue1,

                        uiField = uintValue,
                        uiProperty = uintValue1,


                        i16Field = int16Value,
                        i16Property = int16Value1,

                        ui16Field = uint16Value,
                        ui16Property = uint16Value1,


                        i64Field = int64Value,
                        i64Property = int64Value1,

                        ui64Field = uint64Value,
                        ui64Property = uint64Value1,


                        stringField = stringValue,
                        stringProperty = stringValue1,

                        charField = charValue,
                        charProperty = charValue1,

                        colorField = colorValue,
                        colorProperty = colorValue1,

                        byteField = byteValue,
                        byteProperty = byteValue1,

                        floatField = floatValue,
                        floatProperty = floatValue1,

                        doubleField = doubleValue,
                        doubleProperty = doubleValue1,

                        decimalField = decimalValue,
                        decimalProperty = decimalValue1,

                        enumField = enumValue,
                        enumProperty = enumValue1,
                    };

                    #endregion


                    #region Possibly create null values.

                    if (createNullProperties)
                    {
                        _value.boolNullField = true;
                        _value.boolNullProperty = true;

                        _value.dateTimeNullField = dateTime;
                        _value.dateTimeNullProperty = dateTime1;

                        _value.timeSpanNullField = timeSpanValue;
                        _value.timeSpanNullProperty = timeSpanValue1;

                        _value.guidNullField = guidValue;
                        _value.guidNullProperty = guidValue1;


                        _value.iNullField = intValue;
                        _value.iNullProperty = intValue1;

                        _value.uiNullField = uintValue;
                        _value.uiNullProperty = uintValue1;


                        _value.i16NullField = int16Value;
                        _value.i16NullProperty = int16Value1;

                        _value.ui16NullField = uint16Value;
                        _value.ui16NullProperty = uint16Value1;


                        _value.i64NullField = int64Value;
                        _value.i64NullProperty = int64Value1;

                        _value.ui64NullField = uint64Value;
                        _value.ui64NullProperty = uint64Value1;


                        _value.byteNullField = byteValue;
                        _value.byteNullProperty = byteValue1;

                        _value.floatNullField = floatValue;
                        _value.floatNullProperty = floatValue1;

                        _value.doubleNullField = doubleValue;
                        _value.doubleNullProperty = doubleValue1;

                        _value.decimalNullField = decimalValue;
                        _value.decimalNullProperty = decimalValue1;

                        _value.enumNullField = enumValue;
                        _value.enumNullProperty = enumValue1;

                        _value.colorNullField = colorValue;
                        _value.colorNullProperty = colorValue1;
                    }

                    #endregion


                    return _value;
                };

                #endregion


                #region Compare plain values objects in structures (AStructPlainProperties, AStructPlainProperties).

                Action<AStructPlainProperties, AStructPlainProperties, bool> compareStructPlainValuesObjects = (AStructPlainProperties _value, AStructPlainProperties _clonedValue, bool _compareNull) =>
                {
                    Assert.IsNotNull(_clonedValue);

                    #region Check not null values.

                    Assert.IsTrue(_value.boolField == _clonedValue.boolField);
                    Assert.IsTrue(_value.boolProperty == _clonedValue.boolProperty);

                    Assert.IsTrue(_value.dateTimeField == _clonedValue.dateTimeField);
                    Assert.IsTrue(_value.dateTimeProperty == _clonedValue.dateTimeProperty);

                    Assert.IsTrue(_value.timeSpanField == _clonedValue.timeSpanField);
                    Assert.IsTrue(_value.timeSpanProperty == _clonedValue.timeSpanProperty);

                    Assert.IsTrue(_value.guidField == _clonedValue.guidField);
                    Assert.IsTrue(_value.guidProperty == _clonedValue.guidProperty);


                    Assert.IsTrue(_value.iField == _clonedValue.iField);
                    Assert.IsTrue(_value.iProperty == _clonedValue.iProperty);

                    Assert.IsTrue(_value.uiField == _clonedValue.uiField);
                    Assert.IsTrue(_value.uiProperty == _clonedValue.uiProperty);


                    Assert.IsTrue(_value.i16Field == _clonedValue.i16Field);
                    Assert.IsTrue(_value.i16Property == _clonedValue.i16Property);

                    Assert.IsTrue(_value.ui16Field == _clonedValue.ui16Field);
                    Assert.IsTrue(_value.ui16Property == _clonedValue.ui16Property);


                    Assert.IsTrue(_value.i64Field == _clonedValue.i64Field);
                    Assert.IsTrue(_value.i64Property == _clonedValue.i64Property);

                    Assert.IsTrue(_value.ui64Field == _clonedValue.ui64Field);
                    Assert.IsTrue(_value.ui64Property == _clonedValue.ui64Property);


                    Assert.IsTrue(_value.stringField == _clonedValue.stringField);
                    Assert.IsTrue(_value.stringProperty == _clonedValue.stringProperty);

                    Assert.IsTrue(_value.charField == _clonedValue.charField);
                    Assert.IsTrue(_value.charProperty == _clonedValue.charProperty);

                    Assert.IsTrue(_value.colorField == _clonedValue.colorField);
                    Assert.IsTrue(_value.colorProperty == _clonedValue.colorProperty);

                    Assert.IsTrue(_value.byteField == _clonedValue.byteField);
                    Assert.IsTrue(_value.byteProperty == _clonedValue.byteProperty);

                    Assert.IsTrue(_value.floatField == _clonedValue.floatField);
                    Assert.IsTrue(_value.floatProperty == _clonedValue.floatProperty);

                    Assert.IsTrue(_value.doubleField == _clonedValue.doubleField);
                    Assert.IsTrue(_value.doubleProperty == _clonedValue.doubleProperty);

                    Assert.IsTrue(_value.decimalField == _clonedValue.decimalField);
                    Assert.IsTrue(_value.decimalProperty == _clonedValue.decimalProperty);

                    Assert.IsTrue(_value.enumField == _clonedValue.enumField);
                    Assert.IsTrue(_value.enumProperty == _clonedValue.enumProperty);

                    #endregion


                    #region Possibly compare null values.

                    if (_compareNull)
                    {
                        Assert.IsNotNull(_value.boolNullField);
                        Assert.IsNotNull(_value.boolNullProperty);

                        Assert.IsTrue(_value.boolNullField == _clonedValue.boolNullField);
                        Assert.IsTrue(_value.boolNullProperty == _clonedValue.boolNullProperty);


                        Assert.IsNotNull(_value.dateTimeNullField);
                        Assert.IsNotNull(_value.dateTimeNullProperty);

                        Assert.IsTrue(_value.dateTimeNullField == _clonedValue.dateTimeNullField);
                        Assert.IsTrue(_value.dateTimeNullProperty == _clonedValue.dateTimeNullProperty);


                        Assert.IsNotNull(_value.timeSpanNullField);
                        Assert.IsNotNull(_value.timeSpanNullProperty);

                        Assert.IsTrue(_value.timeSpanNullField == _clonedValue.timeSpanNullField);
                        Assert.IsTrue(_value.timeSpanNullProperty == _clonedValue.timeSpanNullProperty);


                        Assert.IsNotNull(_value.guidNullField);
                        Assert.IsNotNull(_value.guidNullProperty);

                        Assert.IsTrue(_value.guidNullField == _clonedValue.guidNullField);
                        Assert.IsTrue(_value.guidNullProperty == _clonedValue.guidNullProperty);


                        Assert.IsNotNull(_value.iNullField);
                        Assert.IsNotNull(_value.iNullProperty);

                        Assert.IsNotNull(_value.uiNullField);
                        Assert.IsNotNull(_value.uiNullProperty);


                        Assert.IsTrue(_value.iNullField == _clonedValue.iNullField);
                        Assert.IsTrue(_value.iNullProperty == _clonedValue.iNullProperty);

                        Assert.IsTrue(_value.uiNullField == _clonedValue.uiNullField);
                        Assert.IsTrue(_value.uiNullProperty == _clonedValue.uiNullProperty);


                        Assert.IsNotNull(_value.i16NullField);
                        Assert.IsNotNull(_value.i16NullProperty);

                        Assert.IsNotNull(_value.ui16NullField);
                        Assert.IsNotNull(_value.ui16NullProperty);


                        Assert.IsTrue(_value.i16NullField == _clonedValue.i16NullField);
                        Assert.IsTrue(_value.i16NullProperty == _clonedValue.i16NullProperty);

                        Assert.IsTrue(_value.ui16NullField == _clonedValue.ui16NullField);
                        Assert.IsTrue(_value.ui16NullProperty == _clonedValue.ui16NullProperty);


                        Assert.IsNotNull(_value.i64NullField);
                        Assert.IsNotNull(_value.i64NullProperty);

                        Assert.IsNotNull(_value.ui64NullField);
                        Assert.IsNotNull(_value.ui64NullProperty);


                        Assert.IsTrue(_value.i64NullField == _clonedValue.i64NullField);
                        Assert.IsTrue(_value.i64NullProperty == _clonedValue.i64NullProperty);

                        Assert.IsTrue(_value.ui64NullField == _clonedValue.ui64NullField);
                        Assert.IsTrue(_value.ui64NullProperty == _clonedValue.ui64NullProperty);


                        Assert.IsNotNull(_value.byteNullField);
                        Assert.IsNotNull(_value.byteNullProperty);

                        Assert.IsTrue(_value.byteNullField == _clonedValue.byteNullField);
                        Assert.IsTrue(_value.byteNullProperty == _clonedValue.byteNullProperty);


                        Assert.IsNotNull(_value.floatNullField);
                        Assert.IsNotNull(_value.floatNullProperty);

                        Assert.IsTrue(_value.floatNullField == _clonedValue.floatNullField);
                        Assert.IsTrue(_value.floatNullProperty == _clonedValue.floatNullProperty);


                        Assert.IsNotNull(_value.doubleNullField);
                        Assert.IsNotNull(_value.doubleNullProperty);

                        Assert.IsTrue(_value.doubleNullField == _clonedValue.doubleNullField);
                        Assert.IsTrue(_value.doubleNullProperty == _clonedValue.doubleNullProperty);


                        Assert.IsNotNull(_value.decimalNullField);
                        Assert.IsNotNull(_value.decimalNullProperty);

                        Assert.IsTrue(_value.decimalNullField == _clonedValue.decimalNullField);
                        Assert.IsTrue(_value.decimalNullProperty == _clonedValue.decimalNullProperty);


                        Assert.IsNotNull(_value.enumNullField);
                        Assert.IsNotNull(_value.enumNullProperty);

                        Assert.IsTrue(_value.enumNullField == _clonedValue.enumNullField);
                        Assert.IsTrue(_value.enumNullProperty == _clonedValue.enumNullProperty);


                        Assert.IsNotNull(_value.colorNullField);
                        Assert.IsNotNull(_value.colorNullProperty);

                        Assert.IsTrue(_value.colorNullField == _clonedValue.colorNullField);
                        Assert.IsTrue(_value.colorNullProperty == _clonedValue.colorNullProperty);
                    }

                    #endregion


                    #region Otherwise, check that values are null.

                    else
                    {
                        Assert.IsNull(_value.boolNullField);
                        Assert.IsNull(_value.boolNullProperty);

                        Assert.IsNull(_value.dateTimeNullField);
                        Assert.IsNull(_value.dateTimeNullProperty);

                        Assert.IsNull(_value.timeSpanNullField);
                        Assert.IsNull(_value.timeSpanNullProperty);

                        Assert.IsNull(_value.guidNullField);
                        Assert.IsNull(_value.guidNullProperty);


                        Assert.IsNull(_value.iNullField);
                        Assert.IsNull(_value.iNullProperty);

                        Assert.IsNull(_value.uiNullField);
                        Assert.IsNull(_value.uiNullProperty);


                        Assert.IsNull(_value.i16NullField);
                        Assert.IsNull(_value.i16NullProperty);

                        Assert.IsNull(_value.ui16NullField);
                        Assert.IsNull(_value.ui16NullProperty);


                        Assert.IsNull(_value.i64NullField);
                        Assert.IsNull(_value.i64NullProperty);

                        Assert.IsNull(_value.ui64NullField);
                        Assert.IsNull(_value.ui64NullProperty);


                        Assert.IsNull(_value.byteNullField);
                        Assert.IsNull(_value.byteNullProperty);

                        Assert.IsNull(_value.floatNullField);
                        Assert.IsNull(_value.floatNullProperty);

                        Assert.IsNull(_value.doubleNullField);
                        Assert.IsNull(_value.doubleNullProperty);

                        Assert.IsNull(_value.decimalNullField);
                        Assert.IsNull(_value.decimalNullProperty);

                        Assert.IsNull(_value.enumNullField);
                        Assert.IsNull(_value.enumNullProperty);

                        Assert.IsNull(_value.colorNullField);
                        Assert.IsNull(_value.colorNullProperty);
                    }

                    #endregion
                };

                #endregion


                #region Compare plain values objects in structures (AStructPlainProperties, APlainProperties).

                Action<AStructPlainProperties, APlainProperties, bool> compareStructAndTypedPlainValuesObjects = (AStructPlainProperties _value, APlainProperties _clonedValue, bool _compareNull) =>
                {
                    Assert.IsNotNull(_clonedValue);

                    #region Check not null values.

                    Assert.IsTrue(_value.boolField == _clonedValue.boolField);
                    Assert.IsTrue(_value.boolProperty == _clonedValue.boolProperty);

                    Assert.IsTrue(_value.dateTimeField == _clonedValue.dateTimeField);
                    Assert.IsTrue(_value.dateTimeProperty == _clonedValue.dateTimeProperty);

                    Assert.IsTrue(_value.timeSpanField == _clonedValue.timeSpanField);
                    Assert.IsTrue(_value.timeSpanProperty == _clonedValue.timeSpanProperty);

                    Assert.IsTrue(_value.guidField == _clonedValue.guidField);
                    Assert.IsTrue(_value.guidProperty == _clonedValue.guidProperty);


                    Assert.IsTrue(_value.iField == _clonedValue.iField);
                    Assert.IsTrue(_value.iProperty == _clonedValue.iProperty);

                    Assert.IsTrue(_value.uiField == _clonedValue.uiField);
                    Assert.IsTrue(_value.uiProperty == _clonedValue.uiProperty);


                    Assert.IsTrue(_value.i16Field == _clonedValue.i16Field);
                    Assert.IsTrue(_value.i16Property == _clonedValue.i16Property);

                    Assert.IsTrue(_value.ui16Field == _clonedValue.ui16Field);
                    Assert.IsTrue(_value.ui16Property == _clonedValue.ui16Property);


                    Assert.IsTrue(_value.i64Field == _clonedValue.i64Field);
                    Assert.IsTrue(_value.i64Property == _clonedValue.i64Property);

                    Assert.IsTrue(_value.ui64Field == _clonedValue.ui64Field);
                    Assert.IsTrue(_value.ui64Property == _clonedValue.ui64Property);


                    Assert.IsTrue(_value.stringField == _clonedValue.stringField);
                    Assert.IsTrue(_value.stringProperty == _clonedValue.stringProperty);

                    Assert.IsTrue(_value.charField == _clonedValue.charField);
                    Assert.IsTrue(_value.charProperty == _clonedValue.charProperty);

                    Assert.IsTrue(_value.colorField == _clonedValue.colorField);
                    Assert.IsTrue(_value.colorProperty == _clonedValue.colorProperty);

                    Assert.IsTrue(_value.byteField == _clonedValue.byteField);
                    Assert.IsTrue(_value.byteProperty == _clonedValue.byteProperty);

                    Assert.IsTrue(_value.floatField == _clonedValue.floatField);
                    Assert.IsTrue(_value.floatProperty == _clonedValue.floatProperty);

                    Assert.IsTrue(_value.doubleField == _clonedValue.doubleField);
                    Assert.IsTrue(_value.doubleProperty == _clonedValue.doubleProperty);

                    Assert.IsTrue(_value.decimalField == _clonedValue.decimalField);
                    Assert.IsTrue(_value.decimalProperty == _clonedValue.decimalProperty);

                    Assert.IsTrue(_value.enumField == _clonedValue.enumField);
                    Assert.IsTrue(_value.enumProperty == _clonedValue.enumProperty);

                    #endregion


                    #region Possibly compare null values.

                    if (_compareNull)
                    {
                        Assert.IsNotNull(_value.boolNullField);
                        Assert.IsNotNull(_value.boolNullProperty);

                        Assert.IsTrue(_value.boolNullField == _clonedValue.boolNullField);
                        Assert.IsTrue(_value.boolNullProperty == _clonedValue.boolNullProperty);


                        Assert.IsNotNull(_value.dateTimeNullField);
                        Assert.IsNotNull(_value.dateTimeNullProperty);

                        Assert.IsTrue(_value.dateTimeNullField == _clonedValue.dateTimeNullField);
                        Assert.IsTrue(_value.dateTimeNullProperty == _clonedValue.dateTimeNullProperty);


                        Assert.IsNotNull(_value.timeSpanNullField);
                        Assert.IsNotNull(_value.timeSpanNullProperty);

                        Assert.IsTrue(_value.timeSpanNullField == _clonedValue.timeSpanNullField);
                        Assert.IsTrue(_value.timeSpanNullProperty == _clonedValue.timeSpanNullProperty);


                        Assert.IsNotNull(_value.guidNullField);
                        Assert.IsNotNull(_value.guidNullProperty);

                        Assert.IsTrue(_value.guidNullField == _clonedValue.guidNullField);
                        Assert.IsTrue(_value.guidNullProperty == _clonedValue.guidNullProperty);


                        Assert.IsNotNull(_value.iNullField);
                        Assert.IsNotNull(_value.iNullProperty);

                        Assert.IsNotNull(_value.uiNullField);
                        Assert.IsNotNull(_value.uiNullProperty);


                        Assert.IsTrue(_value.iNullField == _clonedValue.iNullField);
                        Assert.IsTrue(_value.iNullProperty == _clonedValue.iNullProperty);

                        Assert.IsTrue(_value.uiNullField == _clonedValue.uiNullField);
                        Assert.IsTrue(_value.uiNullProperty == _clonedValue.uiNullProperty);


                        Assert.IsNotNull(_value.i16NullField);
                        Assert.IsNotNull(_value.i16NullProperty);

                        Assert.IsNotNull(_value.ui16NullField);
                        Assert.IsNotNull(_value.ui16NullProperty);


                        Assert.IsTrue(_value.i16NullField == _clonedValue.i16NullField);
                        Assert.IsTrue(_value.i16NullProperty == _clonedValue.i16NullProperty);

                        Assert.IsTrue(_value.ui16NullField == _clonedValue.ui16NullField);
                        Assert.IsTrue(_value.ui16NullProperty == _clonedValue.ui16NullProperty);


                        Assert.IsNotNull(_value.i64NullField);
                        Assert.IsNotNull(_value.i64NullProperty);

                        Assert.IsNotNull(_value.ui64NullField);
                        Assert.IsNotNull(_value.ui64NullProperty);


                        Assert.IsTrue(_value.i64NullField == _clonedValue.i64NullField);
                        Assert.IsTrue(_value.i64NullProperty == _clonedValue.i64NullProperty);

                        Assert.IsTrue(_value.ui64NullField == _clonedValue.ui64NullField);
                        Assert.IsTrue(_value.ui64NullProperty == _clonedValue.ui64NullProperty);


                        Assert.IsNotNull(_value.byteNullField);
                        Assert.IsNotNull(_value.byteNullProperty);

                        Assert.IsTrue(_value.byteNullField == _clonedValue.byteNullField);
                        Assert.IsTrue(_value.byteNullProperty == _clonedValue.byteNullProperty);


                        Assert.IsNotNull(_value.floatNullField);
                        Assert.IsNotNull(_value.floatNullProperty);

                        Assert.IsTrue(_value.floatNullField == _clonedValue.floatNullField);
                        Assert.IsTrue(_value.floatNullProperty == _clonedValue.floatNullProperty);


                        Assert.IsNotNull(_value.doubleNullField);
                        Assert.IsNotNull(_value.doubleNullProperty);

                        Assert.IsTrue(_value.doubleNullField == _clonedValue.doubleNullField);
                        Assert.IsTrue(_value.doubleNullProperty == _clonedValue.doubleNullProperty);


                        Assert.IsNotNull(_value.decimalNullField);
                        Assert.IsNotNull(_value.decimalNullProperty);

                        Assert.IsTrue(_value.decimalNullField == _clonedValue.decimalNullField);
                        Assert.IsTrue(_value.decimalNullProperty == _clonedValue.decimalNullProperty);


                        Assert.IsNotNull(_value.enumNullField);
                        Assert.IsNotNull(_value.enumNullProperty);

                        Assert.IsTrue(_value.enumNullField == _clonedValue.enumNullField);
                        Assert.IsTrue(_value.enumNullProperty == _clonedValue.enumNullProperty);


                        Assert.IsNotNull(_value.colorNullField);
                        Assert.IsNotNull(_value.colorNullProperty);

                        Assert.IsTrue(_value.colorNullField == _clonedValue.colorNullField);
                        Assert.IsTrue(_value.colorNullProperty == _clonedValue.colorNullProperty);
                    }

                    #endregion


                    #region Otherwise, check that values are null.

                    else
                    {
                        Assert.IsNull(_value.boolNullField);
                        Assert.IsNull(_value.boolNullProperty);

                        Assert.IsNull(_value.dateTimeNullField);
                        Assert.IsNull(_value.dateTimeNullProperty);

                        Assert.IsNull(_value.timeSpanNullField);
                        Assert.IsNull(_value.timeSpanNullProperty);

                        Assert.IsNull(_value.guidNullField);
                        Assert.IsNull(_value.guidNullProperty);


                        Assert.IsNull(_value.iNullField);
                        Assert.IsNull(_value.iNullProperty);

                        Assert.IsNull(_value.uiNullField);
                        Assert.IsNull(_value.uiNullProperty);


                        Assert.IsNull(_value.i16NullField);
                        Assert.IsNull(_value.i16NullProperty);

                        Assert.IsNull(_value.ui16NullField);
                        Assert.IsNull(_value.ui16NullProperty);


                        Assert.IsNull(_value.i64NullField);
                        Assert.IsNull(_value.i64NullProperty);

                        Assert.IsNull(_value.ui64NullField);
                        Assert.IsNull(_value.ui64NullProperty);


                        Assert.IsNull(_value.byteNullField);
                        Assert.IsNull(_value.byteNullProperty);

                        Assert.IsNull(_value.floatNullField);
                        Assert.IsNull(_value.floatNullProperty);

                        Assert.IsNull(_value.doubleNullField);
                        Assert.IsNull(_value.doubleNullProperty);

                        Assert.IsNull(_value.decimalNullField);
                        Assert.IsNull(_value.decimalNullProperty);

                        Assert.IsNull(_value.enumNullField);
                        Assert.IsNull(_value.enumNullProperty);

                        Assert.IsNull(_value.colorNullField);
                        Assert.IsNull(_value.colorNullProperty);
                    }

                    #endregion
                };

                #endregion


                #region Compare object plain values objects (AObjectPlainProperties, AStructPlainProperties).

                Action<AObjectPlainProperties, AStructPlainProperties, bool> compareObjectAndStructPlainValuesObjects = (AObjectPlainProperties _value, AStructPlainProperties _clonedValue, bool _compareNull) =>
                {
                    Assert.IsNotNull(_clonedValue);

                    #region Check not null values.

                    Assert.IsTrue(_value.boolField.Equals(_clonedValue.boolField));
                    Assert.IsTrue(_value.boolProperty.Equals(_clonedValue.boolProperty));

                    Assert.IsTrue(_value.dateTimeField.Equals(_clonedValue.dateTimeField));
                    Assert.IsTrue(_value.dateTimeProperty.Equals(_clonedValue.dateTimeProperty));

                    Assert.IsTrue(_value.timeSpanField.Equals(_clonedValue.timeSpanField));
                    Assert.IsTrue(_value.timeSpanProperty.Equals(_clonedValue.timeSpanProperty));

                    Assert.IsTrue(_value.guidField.Equals(_clonedValue.guidField));
                    Assert.IsTrue(_value.guidProperty.Equals(_clonedValue.guidProperty));


                    Assert.IsTrue(_value.iField.Equals(_clonedValue.iField));
                    Assert.IsTrue(_value.iProperty.Equals(_clonedValue.iProperty));

                    Assert.IsTrue(_value.uiField.Equals(_clonedValue.uiField));
                    Assert.IsTrue(_value.uiProperty.Equals(_clonedValue.uiProperty));


                    Assert.IsTrue(_value.i16Field.Equals(_clonedValue.i16Field));
                    Assert.IsTrue(_value.i16Property.Equals(_clonedValue.i16Property));

                    Assert.IsTrue(_value.ui16Field.Equals(_clonedValue.ui16Field));
                    Assert.IsTrue(_value.ui16Property.Equals(_clonedValue.ui16Property));


                    Assert.IsTrue(_value.i64Field.Equals(_clonedValue.i64Field));
                    Assert.IsTrue(_value.i64Property.Equals(_clonedValue.i64Property));

                    Assert.IsTrue(_value.ui64Field.Equals(_clonedValue.ui64Field));
                    Assert.IsTrue(_value.ui64Property.Equals(_clonedValue.ui64Property));


                    Assert.IsTrue(_value.stringField.Equals(_clonedValue.stringField));
                    Assert.IsTrue(_value.stringProperty.Equals(_clonedValue.stringProperty));

                    Assert.IsTrue(_value.charField.Equals(_clonedValue.charField));
                    Assert.IsTrue(_value.charProperty.Equals(_clonedValue.charProperty));

                    Assert.IsTrue(_value.colorField.Equals(_clonedValue.colorField));
                    Assert.IsTrue(_value.colorProperty.Equals(_clonedValue.colorProperty));

                    Assert.IsTrue(_value.byteField.Equals(_clonedValue.byteField));
                    Assert.IsTrue(_value.byteProperty.Equals(_clonedValue.byteProperty));

                    Assert.IsTrue(_value.floatField.Equals(_clonedValue.floatField));
                    Assert.IsTrue(_value.floatProperty.Equals(_clonedValue.floatProperty));

                    Assert.IsTrue(_value.doubleField.Equals(_clonedValue.doubleField));
                    Assert.IsTrue(_value.doubleProperty.Equals(_clonedValue.doubleProperty));

                    Assert.IsTrue(_value.decimalField.Equals(_clonedValue.decimalField));
                    Assert.IsTrue(_value.decimalProperty.Equals(_clonedValue.decimalProperty));

                    Assert.IsTrue((Enum21)_value.enumField == (Enum21)_clonedValue.enumField);
                    Assert.IsTrue((Enum21)_value.enumProperty == (Enum21)_clonedValue.enumProperty);

                    #endregion


                    #region Possibly compare null values.

                    if (_compareNull)
                    {
                        Assert.IsNotNull(_value.boolNullField);
                        Assert.IsNotNull(_value.boolNullProperty);

                        Assert.IsTrue(_value.boolNullField.Equals(_clonedValue.boolNullField));
                        Assert.IsTrue(_value.boolNullProperty.Equals(_clonedValue.boolNullProperty));


                        Assert.IsNotNull(_value.dateTimeNullField);
                        Assert.IsNotNull(_value.dateTimeNullProperty);

                        Assert.IsTrue(_value.dateTimeNullField.Equals(_clonedValue.dateTimeNullField));
                        Assert.IsTrue(_value.dateTimeNullProperty.Equals(_clonedValue.dateTimeNullProperty));


                        Assert.IsNotNull(_value.timeSpanNullField);
                        Assert.IsNotNull(_value.timeSpanNullProperty);

                        Assert.IsTrue(_value.timeSpanNullField.Equals(_clonedValue.timeSpanNullField));
                        Assert.IsTrue(_value.timeSpanNullProperty.Equals(_clonedValue.timeSpanNullProperty));


                        Assert.IsNotNull(_value.guidNullField);
                        Assert.IsNotNull(_value.guidNullProperty);

                        Assert.IsTrue(_value.guidNullField.Equals(_clonedValue.guidNullField));
                        Assert.IsTrue(_value.guidNullProperty.Equals(_clonedValue.guidNullProperty));


                        Assert.IsNotNull(_value.iNullField);
                        Assert.IsNotNull(_value.iNullProperty);

                        Assert.IsNotNull(_value.uiNullField);
                        Assert.IsNotNull(_value.uiNullProperty);


                        Assert.IsTrue(_value.iNullField.Equals(_clonedValue.iNullField));
                        Assert.IsTrue(_value.iNullProperty.Equals(_clonedValue.iNullProperty));

                        Assert.IsTrue(_value.uiNullField.Equals(_clonedValue.uiNullField));
                        Assert.IsTrue(_value.uiNullProperty.Equals(_clonedValue.uiNullProperty));


                        Assert.IsNotNull(_value.i16NullField);
                        Assert.IsNotNull(_value.i16NullProperty);

                        Assert.IsNotNull(_value.ui16NullField);
                        Assert.IsNotNull(_value.ui16NullProperty);


                        Assert.IsTrue(_value.i16NullField.Equals(_clonedValue.i16NullField));
                        Assert.IsTrue(_value.i16NullProperty.Equals(_clonedValue.i16NullProperty));

                        Assert.IsTrue(_value.ui16NullField.Equals(_clonedValue.ui16NullField));
                        Assert.IsTrue(_value.ui16NullProperty.Equals(_clonedValue.ui16NullProperty));


                        Assert.IsNotNull(_value.i64NullField);
                        Assert.IsNotNull(_value.i64NullProperty);

                        Assert.IsNotNull(_value.ui64NullField);
                        Assert.IsNotNull(_value.ui64NullProperty);


                        Assert.IsTrue(_value.i64NullField.Equals(_clonedValue.i64NullField));
                        Assert.IsTrue(_value.i64NullProperty.Equals(_clonedValue.i64NullProperty));

                        Assert.IsTrue(_value.ui64NullField.Equals(_clonedValue.ui64NullField));
                        Assert.IsTrue(_value.ui64NullProperty.Equals(_clonedValue.ui64NullProperty));


                        Assert.IsNotNull(_value.byteNullField);
                        Assert.IsNotNull(_value.byteNullProperty);

                        Assert.IsTrue(_value.byteNullField.Equals(_clonedValue.byteNullField));
                        Assert.IsTrue(_value.byteNullProperty.Equals(_clonedValue.byteNullProperty));


                        Assert.IsNotNull(_value.floatNullField);
                        Assert.IsNotNull(_value.floatNullProperty);

                        Assert.IsTrue(_value.floatNullField.Equals(_clonedValue.floatNullField));
                        Assert.IsTrue(_value.floatNullProperty.Equals(_clonedValue.floatNullProperty));


                        Assert.IsNotNull(_value.doubleNullField);
                        Assert.IsNotNull(_value.doubleNullProperty);

                        Assert.IsTrue(_value.doubleNullField.Equals(_clonedValue.doubleNullField));
                        Assert.IsTrue(_value.doubleNullProperty.Equals(_clonedValue.doubleNullProperty));


                        Assert.IsNotNull(_value.decimalNullField);
                        Assert.IsNotNull(_value.decimalNullProperty);

                        Assert.IsTrue(_value.decimalNullField.Equals(_clonedValue.decimalNullField));
                        Assert.IsTrue(_value.decimalNullProperty.Equals(_clonedValue.decimalNullProperty));


                        Assert.IsNotNull(_value.enumNullField);
                        Assert.IsNotNull(_value.enumNullProperty);

                        Assert.IsTrue((Enum21)_value.enumNullField == (Enum21)_clonedValue.enumNullField);
                        Assert.IsTrue((Enum21)_value.enumNullProperty == (Enum21)_clonedValue.enumNullProperty);


                        Assert.IsNotNull(_value.colorNullField);
                        Assert.IsNotNull(_value.colorNullProperty);

                        Assert.IsTrue(_value.colorNullField.Equals(_clonedValue.colorNullField));
                        Assert.IsTrue(_value.colorNullProperty.Equals(_clonedValue.colorNullProperty));
                    }

                    #endregion


                    #region Otherwise, check that values are null.

                    else
                    {
                        Assert.IsNull(_value.boolNullField);
                        Assert.IsNull(_value.boolNullProperty);

                        Assert.IsNull(_value.dateTimeNullField);
                        Assert.IsNull(_value.dateTimeNullProperty);

                        Assert.IsNull(_value.timeSpanNullField);
                        Assert.IsNull(_value.timeSpanNullProperty);

                        Assert.IsNull(_value.guidNullField);
                        Assert.IsNull(_value.guidNullProperty);


                        Assert.IsNull(_value.iNullField);
                        Assert.IsNull(_value.iNullProperty);

                        Assert.IsNull(_value.uiNullField);
                        Assert.IsNull(_value.uiNullProperty);


                        Assert.IsNull(_value.i16NullField);
                        Assert.IsNull(_value.i16NullProperty);

                        Assert.IsNull(_value.ui16NullField);
                        Assert.IsNull(_value.ui16NullProperty);


                        Assert.IsNull(_value.i64NullField);
                        Assert.IsNull(_value.i64NullProperty);

                        Assert.IsNull(_value.ui64NullField);
                        Assert.IsNull(_value.ui64NullProperty);


                        Assert.IsNull(_value.byteNullField);
                        Assert.IsNull(_value.byteNullProperty);

                        Assert.IsNull(_value.floatNullField);
                        Assert.IsNull(_value.floatNullProperty);

                        Assert.IsNull(_value.doubleNullField);
                        Assert.IsNull(_value.doubleNullProperty);

                        Assert.IsNull(_value.decimalNullField);
                        Assert.IsNull(_value.decimalNullProperty);

                        Assert.IsNull(_value.enumNullField);
                        Assert.IsNull(_value.enumNullProperty);

                        Assert.IsNull(_value.colorNullField);
                        Assert.IsNull(_value.colorNullProperty);
                    }

                    #endregion
                };

                #endregion

                #endregion


                #region Serialization attributes and common actions.

                for (int x = 0; x < 2; x++)
                {
                    #region Initial actions.

                    useProcessedTypes = (x % 2 == 0);
                    serializer = createSerializer(useProcessedTypes);

                    #endregion


                    try
                    {
                        #region Auxiliary variables.

                        createHeaderStream(useProcessedTypes, ref headerStream);

                        #endregion


                        #region Serialize null object.

                        using (MemoryStream stream = new MemoryStream())
                        {
                            serializer.Serialize(stream, null, headerStream);
                            streamLength = stream.Length;
                        }

                        Assert.IsFalse(streamLength < 0, "KTSerializer.Serialize(null) failed to work with stream.");
                        Assert.IsFalse(streamLength != 0, "KTSerializer.Serialize(null) returned wrong stream lengh:" + streamLength + ".");

                        #endregion


                        #region Test 1. Serialize type which is not marked as serialized.

                        {
                            A1 value = new A1()
                            {
                                iField = 1,
                                iProperty = 1
                            };

                            A1 clonedValue = (A1)serializer.Clone(value, headerStream);
                            Assert.IsNull(clonedValue);
                        }

                        #endregion


                        #region Test 2. Serialize type which is marked as serialized, but properties and fields are not marked as such.

                        {
                            A2.iFieldStatic = 2;
                            A2.iPropertyStatic = 2;

                            A2 value = new A2()
                            {
                                iField = 1,
                                iProperty = 1
                            };

                            A2 clonedValue = (A2)serializer.Clone(value, headerStream);

                            Assert.IsNotNull(clonedValue);
                            Assert.IsFalse(clonedValue.iProperty == 1);
                            Assert.IsFalse(clonedValue.iField == 1);
                        }

                        #endregion


                        #region Check fields and properties that have same GUIDs.

                        #region Test 3.

                        {
                            A3 value = new A3();

                            bool result = false;
                            try
                            {
                                A3 clonedValue = (A3)serializer.Clone(value, headerStream);
                            }
                            catch (KTSerializeAttributeException)
                            {
                                result = true;
                            }

                            Assert.IsTrue(result);
                        }

                        #endregion


                        #region Test 4.

                        {
                            A4 value = new A4();

                            bool result = false;
                            try
                            {
                                A4 clonedValue = (A4)serializer.Clone(value, headerStream);
                            }
                            catch (KTSerializeAttributeException)
                            {
                                result = true;
                            }

                            Assert.IsTrue(result);
                        }

                        #endregion


                        #region Test 5.

                        {
                            A5 value = new A5();

                            bool result = false;
                            try
                            {
                                A5 clonedValue = (A5)serializer.Clone(value, headerStream);
                            }
                            catch (KTSerializeAttributeException)
                            {
                                result = true;
                            }

                            Assert.IsTrue(result);
                        }

                        #endregion


                        #region Test 6. Check that public static properties with the same serialization GUID provide no effect.

                        {
                            A6 value = new A6();
                            A6 clonedValue = (A6)serializer.Clone(value, headerStream);

                            Assert.IsNotNull(clonedValue);
                        }

                        #endregion


                        #region Test 7. Check that private static properties with the same serialization GUID provide no effect.

                        {
                            A7 value = new A7();
                            A7 clonedValue = (A7)serializer.Clone(value, headerStream);

                            Assert.IsNotNull(clonedValue);
                        }

                        #endregion

                        #endregion


                        #region Check serialization between types when one is set as serializable and the other one not.

                        #region Test 8.

                        {
                            A8 value = new A8()
                            {
                                iField = 1,
                                iProperty = 1
                            };

                            B8 clonedValue = serializer.Clone<B8>(value, headerStream);
                            Assert.IsNull(clonedValue);
                        }

                        #endregion


                        #region Test 9.

                        {
                            A9 value = new A9()
                            {
                                iField = 1,
                                iProperty = 1
                            };

                            B9 clonedValue = serializer.Clone<B9>(value, headerStream);

                            Assert.IsNull(clonedValue);
                        }

                        #endregion


                        #region Test 10.

                        {
                            A10 value = new A10()
                            {
                                iField = 1,
                                iProperty = 1
                            };

                            B10 clonedValue = serializer.Clone<B10>(value, headerStream);

                            Assert.IsNull(clonedValue);
                        }

                        #endregion


                        #region Test 11.

                        {
                            A11 value = new A11()
                            {
                                iField = 1,
                                iProperty = 1
                            };

                            B11 clonedValue = serializer.Clone<B11>(value, headerStream);
                            Assert.IsNull(clonedValue);
                        }

                        #endregion

                        #endregion


                        #region Test 12. Try to deserialize with invalid attribute GUID.

                        {
                            A12 value = new A12();

                            bool result = false;

                            try
                            {
                                A12 clonedValue = (A12)serializer.Clone(value, headerStream);
                            }
                            catch (FormatException)
                            {
                                result = true;
                            }

                            Assert.IsTrue(result);
                        }

                        #endregion


                        #region Property is serializable, it's type - not.

                        // Check that value is marked as serializable, but it's type is not marked as such (A.BProperty of type B, BProperty is serializable, B - not).

                        {
                            #region Set value.

                            B17 value = new B17();
                            value.aField = new A17()
                            {
                                iField = 1,
                                iProperty = 2
                            };
                            value.aProperty = new A17()
                            {
                                iField = 1,
                                iProperty = 2
                            };

                            B17 clonedValue = serializer.Clone<B17>(value, headerStream);

                            #endregion


                            #region Check result.

                            Assert.IsNotNull(clonedValue);

                            Assert.IsNull(clonedValue.aField);
                            Assert.IsNull(clonedValue.aProperty);

                            #endregion
                        }

                        #endregion


                        #region Serialization of type, inherited from not serializable type.

                        {
                            A17Inherited value = new A17Inherited();

                            bool result = false;

                            try
                            {
                                A17Inherited clonedValue = (A17Inherited)serializer.Clone(value, headerStream);
                            }
                            catch (KTSerializeException)
                            {
                                result = true;
                            }

                            Assert.IsTrue(result);


                            // Restore state.
                            serializer = createSerializer(useProcessedTypes);
                            createHeaderStream(useProcessedTypes, ref headerStream);
                        }

                        #endregion


                        #region Registration of type, inherited from not serializable type.

                        {
                            serializer.RegisterType(typeof(A17Inherited));
                            A11 value = new A11(); // a serializable type which does not have a collection to the registered type

                            bool result = false;

                            try
                            {
                                A11 clonedValue = (A11)serializer.Clone(value, headerStream);
                            }
                            catch (KTSerializeException)
                            {
                                result = true;
                            }

                            Assert.IsTrue(result);


                            // Restore state.
                            serializer = createSerializer(useProcessedTypes);
                            createHeaderStream(useProcessedTypes, ref headerStream);
                        }

                        #endregion


                        #region Process types with complicated inheritance.

                        {
                            #region Set value.

                            serializer.RegisterType(typeof(A27));

                            C27 value = new C27();
                            value.bField = new B27(1);
                            value.bField.aAttributeList1 = new List<A27>()
                                {
                                    new A27()
                                };
                            value.bField.aAttributeList2 = new List<A27NoClassAttribute>()
                                {
                                    new A27NoClassAttribute()
                                };
                            value.bField.aAttributeList3 = new List<A27InheritedWithoutClassAttribute>()
                                {
                                    new A27InheritedWithoutClassAttribute()
                                };
                            value.bField.aAttributeList4 = new List<A27WithClassAttribute>()
                                {
                                    new A27WithClassAttribute()
                                };
                            value.bField.aAttributeList5 = new List<A27InheritedWithClassAttribute>()
                                {
                                    new A27InheritedWithClassAttribute(2)
                                };

                            C27 clonedValue = serializer.Clone<C27>(value, headerStream);

                            #endregion


                            #region Check result.

                            // Check.
                            Assert.IsTrue(clonedValue.bField.aAttributeList1.Count == 1);
                            Assert.IsTrue(clonedValue.bField.aAttributeList2.Count == 1);
                            Assert.IsTrue(clonedValue.bField.aAttributeList3.Count == 1);
                            Assert.IsTrue(clonedValue.bField.aAttributeList4.Count == 1);
                            Assert.IsTrue(clonedValue.bField.aAttributeList5.Count == 1);

                            Assert.IsNotNull(clonedValue.bField.aAttributeList1[0]);
                            Assert.IsNull(clonedValue.bField.aAttributeList2[0]);
                            Assert.IsNull(clonedValue.bField.aAttributeList3[0]);
                            Assert.IsNotNull(clonedValue.bField.aAttributeList4[0]);
                            Assert.IsNotNull(clonedValue.bField.aAttributeList5[0]);

                            #endregion


                            #region Restore state.

                            serializer = createSerializer(useProcessedTypes);
                            createHeaderStream(useProcessedTypes, ref headerStream);

                            #endregion
                        }

                        #endregion


                        #region Check saving and restoring with different order of known types.

                        if (useProcessedTypes)
                        {
                            #region Set value.

                            serializer = createSerializer(useProcessedTypes);
                            serializer.RegisterType(typeof(APlainProperties));
                            serializer.RegisterType(typeof(APlainPropertiesPreset));
                            serializer.RegisterType(typeof(C25));
                            serializer.RegisterType(typeof(B25));

                            serializer.RegisterType(typeof(A27));


                            // Main value.
                            C27 value = new C27();
                            value.bField = new B27(1);
                            value.bField.aAttributeList1 = new List<A27>()
                                {
                                    new A27()
                                };
                            C27 clonedValue;


                            // Second value.
                            A8_Big secondValue = new A8_Big()
                            {
                                aProperty = new APlainProperties(),
                                iField = 1,
                                iProperty = 1
                            };
                            A8 clonedSecondValue;


                            using (headerStream = new MemoryStream())
                            using (MemoryStream stream = new MemoryStream())
                            using (MemoryStream secondStream = new MemoryStream())
                            {
                                // Save original value to stream and known types to header.
                                serializer.Serialize(stream, value, headerStream);

                                // Save second value.
                                serializer.Serialize(secondStream, secondValue, headerStream);

                                // Recreate serializer with new types order.
                                serializer = createSerializer(useProcessedTypes);

                                serializer.RegisterType(typeof(A9));
                                serializer.RegisterType(typeof(A10));
                                serializer.RegisterType(typeof(B8));
                                serializer.RegisterType(typeof(B9));
                                serializer.RegisterType(typeof(B10));
                                serializer.RegisterType(typeof(A27));
                                serializer.RegisterType(typeof(BPlainProperties));

                                // Some other object has rewritten header stream.
                                // NB.! We imitate case when order of registered types has been changed between compilations, and header stream is restored from the file.
                                using (MemoryStream stream2 = new MemoryStream())
                                {
                                    serializer.Serialize(stream2, value, headerStream);
                                }



                                // Restore second value (reverse order of restore).
                                clonedSecondValue = serializer.Deserialize<A8>(secondStream, headerStream);

                                Assert.IsNotNull(clonedSecondValue);

                                Assert.IsTrue(clonedSecondValue.iField == 1);
                                Assert.IsTrue(clonedSecondValue.iProperty == 1);


                                // Restore main value with new serializer and new header.
                                clonedValue = serializer.Deserialize<C27>(stream, headerStream);
                            }

                            #endregion


                            #region Check value.

                            // Value should be restored, though header may be changed.
                            Assert.IsNotNull(clonedValue);
                            Assert.IsTrue(clonedValue.bField.aAttributeList1.Count == 1);
                            Assert.IsNull(clonedValue.bField.aAttributeList2);
                            Assert.IsNull(clonedValue.bField.aAttributeList3);
                            Assert.IsNull(clonedValue.bField.aAttributeList4);
                            Assert.IsNull(clonedValue.bField.aAttributeList5);

                            #endregion


                            #region Restore state.

                            serializer = createSerializer(useProcessedTypes);
                            createHeaderStream(useProcessedTypes, ref headerStream);

                            #endregion
                        }

                        #endregion


                        #region Check restoring to new set of properties.

                        // NB.! We imitate case when we have taken old value, restore to new type, save this new type and restore it again.

                        {
                            #region Set value.

                            C27 value = new C27();
                            value.bField = new B27(1);
                            value.bField.aAttributeList1 = new List<A27>()
                                {
                                    new A27()
                                };


                            X27 clonedValue;

                            using (MemoryStream stream = new MemoryStream())
                            {
                                // Save original value to stream and known types to header.
                                serializer.Serialize(stream, value, headerStream);

                                // Recreate serializer with new types order.
                                serializer = createSerializer(useProcessedTypes);

                                // Restore new value with new serializer and new header from old stream.
                                clonedValue = serializer.Deserialize<X27>(stream, headerStream);
                            }

                            using (MemoryStream stream = new MemoryStream())
                            {
                                // Save new value to stream and known types to header.
                                serializer.Serialize(stream, clonedValue, headerStream);

                                // Recreate serializer with new types order.
                                serializer = createSerializer(useProcessedTypes);

                                // Restore new value with new serializer and new header from new stored stream.
                                clonedValue = serializer.Deserialize<X27>(stream, headerStream);
                            }

                            #endregion


                            #region Check value.

                            // Value should be restored, though header may be changed.
                            Assert.IsNotNull(clonedValue);
                            Assert.IsNull(clonedValue.bField);

                            Assert.IsTrue(clonedValue.bProperty.aAttributeList1.Count == 1);
                            Assert.IsNull(clonedValue.bProperty.aAttributeList2);
                            Assert.IsNull(clonedValue.bProperty.aAttributeList3);
                            Assert.IsNull(clonedValue.bProperty.aAttributeList4);
                            Assert.IsTrue(clonedValue.bProperty.aAttributeListNew2.Count == 0);

                            #endregion


                            #region Restore state.

                            serializer = createSerializer(useProcessedTypes);
                            createHeaderStream(useProcessedTypes, ref headerStream);

                            #endregion
                        }

                        #endregion


                        #region Check same class GUIDs processing.

                        {
                            serializer.RegisterType(typeof(A28SameGuid));
                            A28 value = new A28(); // a serializable type which has same class GUID as registered type.

                            bool result = false;

                            try
                            {
                                A11 clonedValue = (A11)serializer.Clone(value, headerStream);
                            }
                            catch (KTSerializeAttributeException)
                            {
                                result = true;
                            }

                            Assert.IsTrue(result);


                            // Restore state.
                            serializer = createSerializer(useProcessedTypes);
                            createHeaderStream(useProcessedTypes, ref headerStream);
                        }

                        #endregion


                        #region Check wrong before/after attributes.

                        #region Before serialize.

                        {
                            bool result = false;

                            try
                            {
                                A29BeforeSerializeDuplicate value = new A29BeforeSerializeDuplicate();
                                A29BeforeSerializeDuplicate clonedValue = serializer.Clone<A29BeforeSerializeDuplicate>(value, headerStream);
                            }
                            catch (KTSerializeAttributeException)
                            {
                                result = true;
                            }

                            Assert.IsTrue(result);
                        }

                        #endregion


                        #region After serialize.

                        {
                            bool result = false;

                            try
                            {
                                A29AfterSerializeDuplicate value = new A29AfterSerializeDuplicate();
                                A29AfterSerializeDuplicate clonedValue = serializer.Clone<A29AfterSerializeDuplicate>(value, headerStream);
                            }
                            catch (KTSerializeAttributeException)
                            {
                                result = true;
                            }

                            Assert.IsTrue(result);
                        }

                        #endregion



                        #region Before deserialize.

                        {
                            bool result = false;

                            try
                            {
                                A29BeforeDeserializeDuplicate value = new A29BeforeDeserializeDuplicate();
                                A29BeforeDeserializeDuplicate clonedValue = serializer.Clone<A29BeforeDeserializeDuplicate>(value, headerStream);
                            }
                            catch (KTSerializeAttributeException)
                            {
                                result = true;
                            }

                            Assert.IsTrue(result);
                        }

                        #endregion


                        #region After deserialize.

                        {
                            bool result = false;

                            try
                            {
                                A29AfterDeserializeDuplicate value = new A29AfterDeserializeDuplicate();
                                A29AfterDeserializeDuplicate clonedValue = serializer.Clone<A29AfterDeserializeDuplicate>(value, headerStream);
                            }
                            catch (KTSerializeAttributeException)
                            {
                                result = true;
                            }

                            Assert.IsTrue(result);
                        }

                        #endregion

                        #endregion


                        #region Check correct before/after attributes. Check restore from inherited type to base one (of main object).

                        // NB.! Type to restore from is registered. Type to restore to is some base type of the serialized type.

                        {
                            #region Set value.

                            serializer.RegisterType(typeof(C30InheritedStepTwo));

                            C30InheritedStepTwo value = new C30InheritedStepTwo();
                            value.bField = new B30();
                            value.bField.aObject = 'a';

                            value.bField.a1 = new A30()
                            {
                                iField = 1
                            };
                            value.bField.a2 = new A30Inherited()
                            {
                                sProperty = "2"
                            };
                            value.bField.a3 = new A30Inherited2()
                            {
                                sProperty = "10"
                            };

                            C30 clonedValue = serializer.Clone<C30>(value, headerStream);

                            #endregion


                            #region Check result.

                            #region Serialization.

                            Assert.IsTrue(value.BeforeSerializeBool);
                            Assert.IsTrue(value.AfterSerializeBool);
                            Assert.IsFalse(value.BeforeDeserializeBool);
                            Assert.IsFalse(value.AfterDeserializeBool);

                            Assert.IsTrue(((C30Base)value).BeforeSerializeBool);
                            Assert.IsTrue(((C30Base)value).AfterSerializeBool);
                            Assert.IsFalse(((C30Base)value).BeforeDeserializeBool);
                            Assert.IsFalse(((C30Base)value).AfterDeserializeBool);

                            Assert.IsTrue(((C30PreBase)value).BeforeSerializeBool);
                            Assert.IsTrue(((C30PreBase)value).AfterSerializeBool);
                            Assert.IsFalse(((C30PreBase)value).BeforeDeserializeBool);
                            Assert.IsFalse(((C30PreBase)value).AfterDeserializeBool);


                            Assert.IsTrue(value.bField.a1.BeforeSerializeBool);
                            Assert.IsTrue(value.bField.a1.AfterSerializeBool);
                            Assert.IsFalse(value.bField.a1.BeforeDeserializeBool);
                            Assert.IsFalse(value.bField.a1.AfterDeserializeBool);

                            Assert.IsTrue(value.bField.a1.iField == 4);


                            Assert.IsTrue(value.bField.a2.BeforeSerializeBool);
                            Assert.IsTrue(value.bField.a2.AfterSerializeBool);
                            Assert.IsFalse(value.bField.a2.BeforeDeserializeBool);
                            Assert.IsFalse(value.bField.a2.AfterDeserializeBool);

                            Assert.IsTrue(((A30Inherited)value.bField.a2).BeforeSerializeBool);
                            Assert.IsTrue(((A30Inherited)value.bField.a2).AfterSerializeBool);
                            Assert.IsFalse(((A30Inherited)value.bField.a2).BeforeDeserializeBool);
                            Assert.IsFalse(((A30Inherited)value.bField.a2).AfterDeserializeBool);


                            // Check inheritance from one type by many types and necessary serialization/deserialization events.
                            Assert.IsTrue(value.bField.a3.BeforeSerializeBool);
                            Assert.IsTrue(value.bField.a3.AfterSerializeBool);
                            Assert.IsFalse(value.bField.a3.BeforeDeserializeBool);
                            Assert.IsFalse(value.bField.a3.AfterDeserializeBool);

                            Assert.IsTrue(((A30Inherited2)value.bField.a3).BeforeSerializeBool);
                            Assert.IsTrue(((A30Inherited2)value.bField.a3).AfterSerializeBool);
                            Assert.IsFalse(((A30Inherited2)value.bField.a3).BeforeDeserializeBool);
                            Assert.IsFalse(((A30Inherited2)value.bField.a3).AfterDeserializeBool);

                            #endregion


                            #region Deserialization.

                            Assert.IsFalse(clonedValue.BeforeSerializeBool);
                            Assert.IsFalse(clonedValue.AfterSerializeBool);
                            Assert.IsTrue(clonedValue.BeforeDeserializeBool);
                            Assert.IsTrue(clonedValue.AfterDeserializeBool);

                            Assert.IsFalse(((C30Base)clonedValue).BeforeSerializeBool);
                            Assert.IsFalse(((C30Base)clonedValue).AfterSerializeBool);
                            Assert.IsTrue(((C30Base)clonedValue).BeforeDeserializeBool);
                            Assert.IsTrue(((C30Base)clonedValue).AfterDeserializeBool);

                            Assert.IsFalse(((C30PreBase)clonedValue).BeforeSerializeBool);
                            Assert.IsFalse(((C30PreBase)clonedValue).AfterSerializeBool);
                            Assert.IsTrue(((C30PreBase)clonedValue).BeforeDeserializeBool);
                            Assert.IsTrue(((C30PreBase)clonedValue).AfterDeserializeBool);


                            Assert.IsFalse(clonedValue.bField.a1.BeforeSerializeBool);
                            Assert.IsFalse(clonedValue.bField.a1.AfterSerializeBool);
                            Assert.IsTrue(clonedValue.bField.a1.BeforeDeserializeBool);
                            Assert.IsTrue(clonedValue.bField.a1.AfterDeserializeBool);


                            Assert.IsFalse(clonedValue.bField.a2.BeforeSerializeBool);
                            Assert.IsFalse(clonedValue.bField.a2.AfterSerializeBool);
                            Assert.IsTrue(clonedValue.bField.a2.BeforeDeserializeBool);
                            Assert.IsTrue(clonedValue.bField.a2.AfterDeserializeBool);

                            Assert.IsFalse(((A30)clonedValue.bField.a2).BeforeSerializeBool);
                            Assert.IsFalse(((A30)clonedValue.bField.a2).AfterSerializeBool);
                            Assert.IsTrue(((A30)clonedValue.bField.a2).BeforeDeserializeBool);
                            Assert.IsTrue(((A30)clonedValue.bField.a2).AfterDeserializeBool);

                            Assert.IsTrue(clonedValue.bField.a2.sProperty == "2*");

                            #endregion


                            // NB.! There are additional checks in serialized classes also.

                            #endregion


                            #region Restore state.

                            serializer = createSerializer(useProcessedTypes);
                            createHeaderStream(useProcessedTypes, ref headerStream);

                            #endregion
                        }

                        #endregion
                    }
                    finally
                    {
                        #region Restore state.

                        if (headerStream != null) headerStream.Dispose();

                        #endregion
                    }
                }

                /**/
                #endregion


                #region Check common header.

                #region Check exceptions connected with the common header.

                #region Serialization; type uses processed types, header stream is not set.

                {
                    bool result = false;

                    try
                    {
                        serializer.UseProcessedTypes = true;

                        A30 value = new A30();

                        using (MemoryStream stream = new MemoryStream())
                        {
                            serializer.Serialize(stream, value);
                        }
                    }
                    catch (KTSerializeException)
                    {
                        result = true;
                    }

                    Assert.IsTrue(result);
                }

                #endregion


                #region Serialization; type does not use processed types, header stream is set.

                {
                    bool result = false;

                    try
                    {
                        serializer.UseProcessedTypes = false;

                        A30 value = new A30();

                        using (MemoryStream stream = new MemoryStream())
                        using (headerStream = new MemoryStream())
                        {
                            serializer.Serialize(stream, value, headerStream);
                        }
                    }
                    catch (KTSerializeException)
                    {
                        result = true;
                    }

                    Assert.IsTrue(result);
                }

                #endregion



                #region Deserialization; type uses processed types, header stream is not set.

                {
                    bool result = false;

                    try
                    {
                        A30 value = new A30();

                        using (MemoryStream stream = new MemoryStream())
                        {
                            serializer.Serialize(stream, value);
                            serializer.UseProcessedTypes = true;

                            A30 clonedValue = serializer.Deserialize<A30>(stream);
                        }
                    }
                    catch (KTDeserializeException)
                    {
                        result = true;
                    }

                    Assert.IsTrue(result);
                }

                #endregion


                #region Deserialization; type does not use processed types, header stream is set.

                {
                    bool result = false;
                    serializer.UseProcessedTypes = false;

                    try
                    {
                        A30 value = new A30();

                        using (MemoryStream stream = new MemoryStream())
                        using (headerStream = new MemoryStream())
                        {
                            serializer.Serialize(stream, value);
                            A30 clonedValue = serializer.Deserialize<A30>(stream, headerStream);
                        }
                    }
                    catch (KTDeserializeException)
                    {
                        result = true;
                    }

                    Assert.IsTrue(result);
                }

                #endregion



                #region Restore state.

                serializer = createSerializer(useProcessedTypes);

                #endregion

                #endregion



                #region Clone(), no header stream.

                {
                    #region Set value.

                    APlainProperties value = new APlainProperties()
                    {
                        iField = intValue,
                        stringProperty = stringValue
                    };

                    APlainProperties clonedValue = (APlainProperties)serializer.Clone(value);

                    #endregion


                    #region Check result.

                    Assert.IsNotNull(clonedValue);
                    Assert.IsTrue(clonedValue.iField == intValue);
                    Assert.IsTrue(clonedValue.stringProperty == stringValue);

                    #endregion
                }

                #endregion


                #region Clone<T>(), no header stream.

                {
                    #region Set value.

                    APlainProperties value = new APlainProperties()
                    {
                        iField = intValue,
                        stringProperty = stringValue
                    };

                    APlainProperties clonedValue = serializer.Clone<APlainProperties>(value);

                    #endregion


                    #region Check result.

                    Assert.IsNotNull(clonedValue);
                    Assert.IsTrue(clonedValue.iField == intValue);
                    Assert.IsTrue(clonedValue.stringProperty == stringValue);

                    #endregion
                }

                #endregion



                #region Clone() with header stream, created for every request.

                {
                    #region Inital actions.

                    serializer = createSerializer(true);

                    #endregion


                    for (int i = 0; i < 5; i++)
                    {
                        #region Set value.

                        // Register types.
                        registerTestTypes(serializer, i);


                        APlainProperties value = new APlainProperties()
                        {
                            iField = intValue,
                            stringProperty = stringValue
                        };

                        APlainProperties clonedValue;
                        using (headerStream = new MemoryStream())
                        {
                            clonedValue = (APlainProperties)serializer.Clone(value, headerStream);
                        }

                        #endregion


                        #region Check result.

                        Assert.IsNotNull(clonedValue);
                        Assert.IsTrue(clonedValue.iField == intValue);
                        Assert.IsTrue(clonedValue.stringProperty == stringValue);

                        #endregion
                    }


                    #region Restore state.

                    serializer = createSerializer(useProcessedTypes);

                    #endregion
                }

                #endregion


                #region Clone() with common header stream.

                using (headerStream = new MemoryStream())
                {
                    // Inital actions.
                    serializer = createSerializer(true);


                    for (int i = 0; i < 5; i++)
                    {
                        #region Set value.

                        // Register types.
                        registerTestTypes(serializer, i);


                        APlainProperties value = new APlainProperties()
                        {
                            iField = intValue,
                            stringProperty = stringValue
                        };

                        APlainProperties clonedValue = (APlainProperties)serializer.Clone(value, headerStream);

                        #endregion


                        #region Check result.

                        Assert.IsNotNull(clonedValue);
                        Assert.IsTrue(clonedValue.iField == intValue);
                        Assert.IsTrue(clonedValue.stringProperty == stringValue);

                        #endregion
                    }


                    // Restore state.
                    serializer = createSerializer(useProcessedTypes);
                }

                #endregion



                #region Clone<T>() with header stream, created for every request.

                {
                    #region Inital actions.

                    serializer = createSerializer(true);

                    #endregion


                    for (int i = 0; i < 5; i++)
                    {
                        #region Set value.

                        // Register types.
                        registerTestTypes(serializer, i);


                        APlainProperties value = new APlainProperties()
                        {
                            iField = intValue,
                            stringProperty = stringValue
                        };

                        APlainProperties clonedValue;
                        using (headerStream = new MemoryStream())
                        {
                            clonedValue = serializer.Clone<APlainProperties>(value, headerStream);
                        }

                        #endregion


                        #region Check result.

                        Assert.IsNotNull(clonedValue);
                        Assert.IsTrue(clonedValue.iField == intValue);
                        Assert.IsTrue(clonedValue.stringProperty == stringValue);

                        #endregion
                    }


                    #region Restore state.

                    serializer = createSerializer(useProcessedTypes);

                    #endregion
                }

                #endregion


                #region Clone<T>() with common header stream.

                using (headerStream = new MemoryStream())
                {
                    #region Inital actions.

                    serializer = createSerializer(true);

                    #endregion


                    for (int i = 0; i < 5; i++)
                    {
                        #region Set value.

                        // Register types.
                        registerTestTypes(serializer, i);


                        APlainProperties value = new APlainProperties()
                        {
                            iField = intValue,
                            stringProperty = stringValue
                        };

                        APlainProperties clonedValue = serializer.Clone<APlainProperties>(value, headerStream);

                        #endregion


                        #region Check result.

                        Assert.IsNotNull(clonedValue);
                        Assert.IsTrue(clonedValue.iField == intValue);
                        Assert.IsTrue(clonedValue.stringProperty == stringValue);

                        #endregion
                    }


                    #region Restore state.

                    serializer = createSerializer(useProcessedTypes);

                    #endregion
                }

                #endregion



                #region Check big quantities of data.

                {
                    #region Inital actions.

                    serializer = createSerializer(true);

                    #endregion


                    #region Set value.

                    B15 value = new B15();
                    value.aList = new List<A15>();

                    int counter = 1000000;
                    for (int i = 0; i < counter; i++)
                    {
                        value.aList.Add(
                            new A15()
                            {
                                iField = i,
                                sProperty = i.ToString()
                            });
                    }

                    B15 clonedValue;
                    using (headerStream = new MemoryStream())
                    {
                        clonedValue = serializer.Clone<B15>(value, headerStream);
                    }

                    #endregion


                    #region Check result.

                    Assert.IsTrue(clonedValue.aList.Count == counter);
                    for (int i = 0; i < counter; i++)
                    {
                        A15 tempValue = clonedValue.aList[i];

                        Assert.IsTrue(tempValue.iField == i);
                        Assert.IsTrue(tempValue.sProperty == i.ToString());
                    }

                    #endregion


                    #region Restore state.

                    serializer = createSerializer(useProcessedTypes);

                    #endregion
                }

                #endregion


                #region Restore state.

                useProcessedTypes = false;
                serializer = createSerializer(useProcessedTypes);

                #endregion

                #endregion


                #region Interfaces.

                {
                    #region Set value.

                    B31 value = new B31()
                    {
                        iProperty = new A31()
                        {
                            sProperty = stringValue
                        },
                        iList = new List<IA31>()
                        {
                            new A31()
                            {
                                sProperty = stringValue1
                            }
                        }
                    };

                    B31 clonedValue;
                    KTSerializer.Instance.UseProcessedTypes = true;
                    using (headerStream = new MemoryStream())
                    {
                        clonedValue = KTSerializer.Instance.Clone<B31>(value, headerStream);
                    }

                    #endregion


                    #region Check result.

                    Assert.IsNotNull(clonedValue);

                    Assert.IsTrue(clonedValue.iProperty is A31);
                    Assert.IsTrue(((A31)clonedValue.iProperty).sProperty == stringValue);

                    Assert.IsTrue(clonedValue.iList.Count == 1);
                    Assert.IsTrue(((A31)clonedValue.iList[0]).sProperty == stringValue1);

                    #endregion
                }

                #endregion


                #region Serialize strong typed properties.

                #region Serialize plain and inherited properties in objects.

                #region Serialize class with property of the same class.

                {
                    A13 value = new A13();
                    A13 clonedValue = (A13)serializer.Clone(value);
                }

                #endregion


                #region Serialize class with property of another class.

                {
                    B13 value = new B13();
                    B13 clonedValue = (B13)serializer.Clone(value);
                }

                #endregion


                #region Deserialize primitive type fields, null values stay null.

                {
                    // Set value.
                    APlainProperties value = createPlainPropertiesObject(false);
                    APlainProperties clonedValue = (APlainProperties)serializer.Clone(value);

                    // Check result.
                    comparePlainValuesObjects(value, clonedValue, false);
                }

                #endregion


                #region Deserialize primitive type fields, null values are set.

                {
                    // Set value.
                    APlainProperties value = createPlainPropertiesObject(true);
                    APlainProperties clonedValue = (APlainProperties)serializer.Clone(value);

                    // Check result.
                    comparePlainValuesObjects(value, clonedValue, true);
                }

                #endregion


                #region Deserialize primitive type fields in object as a property, null values are set.

                {
                    #region Set value.

                    BPlainProperties value = new BPlainProperties();
                    value.aField = createPlainPropertiesObject(true);
                    value.aProperty = createPlainPropertiesObject(false);

                    BPlainProperties clonedValue = (BPlainProperties)serializer.Clone(value);

                    #endregion


                    #region Check result.

                    Assert.IsNotNull(clonedValue);
                    comparePlainValuesObjects(value.aField, clonedValue.aField, true);
                    comparePlainValuesObjects(value.aProperty, clonedValue.aProperty, false);

                    #endregion
                }

                #endregion


                #region Serialize properties of the properties, complicated inheritance, simple properties.

                {
                    #region Set value.

                    C14 value = new C14();
                    value.aProperty = new A14()
                    {
                        iField = intValue,
                        iProperty = intValue1
                    };

                    value.dProperty = new D14();
                    value.eField = new E14();

                    value.bProperty = new B14();
                    value.bProperty.aField = new A14()
                    {
                        iField = intValue
                    };

                    C14 clonedValue = (C14)serializer.Clone(value);

                    #endregion


                    #region Check result.

                    Assert.IsNotNull(clonedValue);

                    Assert.IsTrue(clonedValue.aProperty.iField == intValue);
                    Assert.IsTrue(clonedValue.aProperty.iProperty == intValue1);

                    Assert.IsNull(((B14)clonedValue).aProperty); // hidden property

                    Assert.IsNotNull(clonedValue.dProperty);
                    Assert.IsNull(clonedValue.dField);

                    Assert.IsNull(clonedValue.eField);
                    Assert.IsNull(clonedValue.eProperty);

                    Assert.IsTrue(clonedValue.bProperty.aField.iField == intValue);
                    Assert.IsTrue(clonedValue.bProperty.aField.iProperty == 0);

                    Assert.IsNull(clonedValue.aField);
                    Assert.IsNull(clonedValue.bProperty.aProperty);

                    #endregion
                }

                #endregion


                #region Set object with preset values, not equal to the default ones.

                {
                    #region Set value.

                    // Check with plain properties set in class not to default values, but reset to null values at object properties setting.

                    APlainPropertiesPreset value = new APlainPropertiesPreset();
                    setDefaultPlainProperties(value, true);

                    APlainPropertiesPreset clonedValue = serializer.Clone<APlainPropertiesPreset>(value);

                    #endregion


                    #region Check result.

                    Assert.IsNotNull(clonedValue);

                    checkDefaultPlainProperties(clonedValue, true);

                    #endregion
                }

                #endregion


                #region Check hidden properties serialization.

                {
                    #region Set value.

                    C14 value = new C14();
                    value.aProperty = new A14()
                    {
                        iField = intValue,
                        iProperty = intValue1
                    };

                    ((B14)value).aProperty = new A14()
                    {
                        iField = 1,
                        iProperty = 2
                    };

                    C14 clonedValue = (C14)serializer.Clone(value);

                    #endregion


                    #region Check result.

                    Assert.IsNotNull(clonedValue);

                    Assert.IsTrue(clonedValue.aProperty.iField == intValue);
                    Assert.IsTrue(clonedValue.aProperty.iProperty == intValue1);

                    Assert.IsTrue(((B14)clonedValue).aProperty.iField == 1);
                    Assert.IsTrue(((B14)clonedValue).aProperty.iProperty == 2);

                    Assert.IsNull(clonedValue.aField);

                    Assert.IsNull(clonedValue.dProperty);
                    Assert.IsNull(clonedValue.dField);

                    Assert.IsNull(clonedValue.eField);
                    Assert.IsNull(clonedValue.eProperty);

                    Assert.IsNull(clonedValue.bProperty);
                    Assert.IsNull(clonedValue.bField);

                    #endregion
                }

                #endregion


                #region Check overridden properties serialization.

                {
                    #region Set value.

                    C14 value = new C14();
                    value.aProperty2 = new A14()
                    {
                        iField = intValue,
                        iProperty = intValue1
                    };

                    Assert.IsNotNull(((B14)value).aProperty2);

                    Assert.IsNull(((B14)value).aProperty);
                    Assert.IsNull(value.aProperty);

                    C14 clonedValue = (C14)serializer.Clone(value);

                    #endregion


                    #region Check result.

                    Assert.IsNotNull(clonedValue);

                    Assert.IsTrue(clonedValue.aProperty2.iField == intValue);
                    Assert.IsTrue(clonedValue.aProperty2.iProperty == intValue1);

                    Assert.IsTrue(((B14)clonedValue).aProperty2.iField == intValue);
                    Assert.IsTrue(((B14)clonedValue).aProperty2.iProperty == intValue1);

                    Assert.IsNull(((B14)clonedValue).aProperty);
                    Assert.IsNull(clonedValue.aProperty);

                    Assert.IsNull(clonedValue.aField);

                    Assert.IsNull(clonedValue.dProperty);
                    Assert.IsNull(clonedValue.dField);

                    Assert.IsNull(clonedValue.eField);
                    Assert.IsNull(clonedValue.eProperty);

                    Assert.IsNull(clonedValue.bProperty);
                    Assert.IsNull(clonedValue.bField);

                    #endregion
                }

                #endregion


                #region Check overridden and hidden properties serialization.

                {
                    #region Set value.

                    C14 value = new C14();
                    value.aProperty2 = new A14()
                    {
                        iField = intValue,
                        iProperty = intValue1
                    };
                    value.aProperty = new A14()
                    {
                        iField = 1,
                        iProperty = 2
                    };

                    Assert.IsNotNull(((B14)value).aProperty2);

                    Assert.IsNull(((B14)value).aProperty);

                    C14 clonedValue = (C14)serializer.Clone(value);

                    #endregion


                    #region Check result.

                    Assert.IsNotNull(clonedValue);

                    Assert.IsTrue(clonedValue.aProperty2.iField == intValue);
                    Assert.IsTrue(clonedValue.aProperty2.iProperty == intValue1);

                    Assert.IsTrue(((B14)clonedValue).aProperty2.iField == intValue);
                    Assert.IsTrue(((B14)clonedValue).aProperty2.iProperty == intValue1);

                    Assert.IsNull(((B14)clonedValue).aProperty);

                    Assert.IsTrue(clonedValue.aProperty.iField == 1);
                    Assert.IsTrue(clonedValue.aProperty.iProperty == 2);

                    Assert.IsNull(clonedValue.aField);

                    Assert.IsNull(clonedValue.dProperty);
                    Assert.IsNull(clonedValue.dField);

                    Assert.IsNull(clonedValue.eField);
                    Assert.IsNull(clonedValue.eProperty);

                    Assert.IsNull(clonedValue.bProperty);
                    Assert.IsNull(clonedValue.bField);

                    #endregion
                }

                #endregion


                #region Serialize properties of the properties, full properties, rather simple inheritance.

                {
                    #region Set value.

                    CPlainProperties value = new CPlainProperties();
                    value.aProperty = createPlainPropertiesObject(true);

                    value.bProperty = new BPlainProperties();
                    value.bProperty.aField = createPlainPropertiesObject(false);

                    CPlainProperties clonedValue = (CPlainProperties)serializer.Clone(value);

                    #endregion


                    #region Check result.

                    Assert.IsNotNull(clonedValue);
                    comparePlainValuesObjects(value.aProperty, clonedValue.aProperty, true);
                    comparePlainValuesObjects(value.bProperty.aField, clonedValue.bProperty.aField, false);

                    Assert.IsNull(clonedValue.aField);
                    Assert.IsNull(clonedValue.bProperty.aProperty);

                    #endregion
                }

                #endregion


                #region Restore to different type with different class GUIDs.

                // Restore from one type to another, with completely different inheritance chain and order and names of properties, but with the same property types and correct serialization property IDs.

                {
                    #region Set value.

                    C14 value = new C14();
                    value.aProperty2 = new A14()
                    {
                        iField = intValue,
                        iProperty = intValue1
                    };
                    value.aProperty = new A14()
                    {
                        iField = 1,
                        iProperty = 2
                    };

                    Assert.IsNotNull(((B14)value).aProperty2);
                    Assert.IsNull(((B14)value).aProperty);

                    value.dProperty = new D14();

                    X14 clonedValue = serializer.Clone<X14>(value);

                    #endregion


                    #region Check result.

                    Assert.IsNull(clonedValue);

                    #endregion
                }

                #endregion


                #region Restore to different type with same class GUIDs.

                // Restore from one type to another, with completely different inheritance chain and order and names of properties, but with the same property types and correct serialization class and property IDs.

                {
                    #region Set value.

                    C14 value = new C14();
                    value.aProperty2 = new A14()
                    {
                        iField = intValue,
                        iProperty = intValue1
                    };
                    value.aProperty = new A14()
                    {
                        iField = 1,
                        iProperty = 2
                    };

                    Assert.IsNotNull(((B14)value).aProperty2);
                    Assert.IsNull(((B14)value).aProperty);

                    value.dProperty = new D14();

                    X14ID clonedValue = serializer.Clone<X14ID>(value);

                    #endregion


                    #region Check result.

                    Assert.IsNotNull(clonedValue);

                    Assert.IsTrue(clonedValue.aPropertyNew.iFieldNew == 1);
                    Assert.IsTrue(clonedValue.aPropertyNew.iPropertyNew == 2);

                    Assert.IsTrue(clonedValue.aPropertyNew2.iFieldNew == intValue);
                    Assert.IsTrue(clonedValue.aPropertyNew2.iPropertyNew == intValue1);


                    Assert.IsTrue(clonedValue.aPropertyNew.dFromSField == 0);
                    Assert.IsTrue(clonedValue.aPropertyNew.dFromSProperty == 0);

                    Assert.IsTrue(clonedValue.aPropertyNew2.dFromSField == 0);
                    Assert.IsTrue(clonedValue.aPropertyNew2.dFromSProperty == 0);


                    Assert.IsNull(clonedValue.eFieldNew);
                    Assert.IsNull(clonedValue.ePropertyNew);

                    Assert.IsNotNull(clonedValue.dPropertyNew);
                    Assert.IsNull(clonedValue.dFieldNew);

                    #endregion
                }

                #endregion


                #region Check Enum serialization.

                #region Serialize with default values.

                {
                    #region Set value.

                    B21 value = new B21();
                    value.aProperty = new A21();

                    B21 clonedValue = serializer.Clone<B21>(value);

                    #endregion


                    #region Check result.

                    Assert.IsNull(clonedValue.aField);

                    Assert.IsNull(clonedValue.aProperty.eNullProperty);
                    Assert.IsTrue(clonedValue.aProperty.eProperty == 0);
                    Assert.IsTrue(clonedValue.aProperty.iField == 0);

                    #endregion
                }

                #endregion


                #region Serialize with not null property set.

                {
                    #region Set value.

                    B21 value = new B21();
                    value.aProperty = new A21();
                    value.aProperty.eProperty = Enum21.Value1;

                    B21 clonedValue = serializer.Clone<B21>(value);

                    #endregion


                    #region Check result.

                    Assert.IsNull(clonedValue.aField);

                    Assert.IsNull(clonedValue.aProperty.eNullProperty);
                    Assert.IsTrue(clonedValue.aProperty.eProperty == Enum21.Value1);
                    Assert.IsTrue(clonedValue.aProperty.iField == 0);

                    #endregion
                }

                #endregion


                #region Serialize with null property set.

                {
                    #region Set value.

                    B21 value = new B21();
                    value.aProperty = new A21();
                    value.aProperty.eNullProperty = Enum21.Value1;

                    B21 clonedValue = serializer.Clone<B21>(value);

                    #endregion


                    #region Check result.

                    Assert.IsNull(clonedValue.aField);

                    Assert.IsTrue(clonedValue.aProperty.eNullProperty == Enum21.Value1);
                    Assert.IsTrue(clonedValue.aProperty.eProperty == 0);
                    Assert.IsTrue(clonedValue.aProperty.iField == 0);

                    #endregion
                }

                #endregion


                #region Serialize with null and not null properties set.

                {
                    #region Set value.

                    B21 value = new B21();
                    value.aProperty = new A21()
                    {
                        eNullProperty = Enum21.Value2,
                        eProperty = Enum21.Value4,
                        iField = intValue
                    };

                    B21 clonedValue = serializer.Clone<B21>(value);

                    #endregion


                    #region Check result.

                    Assert.IsNull(clonedValue.aField);

                    Assert.IsTrue(clonedValue.aProperty.eNullProperty == Enum21.Value2);
                    Assert.IsTrue(clonedValue.aProperty.eProperty == Enum21.Value4);
                    Assert.IsTrue(clonedValue.aProperty.iField == intValue);

                    #endregion
                }

                #endregion

                #endregion


                #region Serialize to the wrong type.

                #region Plain properties.

                //if (!useProcessedTypes)
                {
                    #region Integer property to string.

                    {
                        A28IntProperty value = new A28IntProperty();
                        A28StringProperty clonedValue = serializer.Clone<A28StringProperty>(value);

                        Assert.IsTrue(clonedValue.iProperty == "0");
                    }

                    #endregion


                    #region Integer field to string.

                    {
                        A28IntField value = new A28IntField();
                        A28StringField clonedValue = serializer.Clone<A28StringField>(value);

                        Assert.IsTrue(clonedValue.iField == "0");
                    }

                    #endregion



                    #region String property to integer.

                    {
                        A28StringProperty value = new A28StringProperty()
                        {
                            iProperty = stringValue
                        };
                        A28IntProperty clonedValue = serializer.Clone<A28IntProperty>(value);

                        Assert.IsTrue(clonedValue.iProperty == 0);
                    }

                    #endregion


                    #region String field to integer.

                    {
                        A28StringField value = new A28StringField()
                        {
                            iField = stringValue
                        };
                        A28IntField clonedValue = serializer.Clone<A28IntField>(value);

                        Assert.IsTrue(clonedValue.iField == 0);
                    }

                    #endregion



                    #region Int32 property to Int64.

                    {
                        A28IntProperty value = new A28IntProperty()
                        {
                            iProperty = intValue
                        };
                        A28Int64Property clonedValue = serializer.Clone<A28Int64Property>(value);

                        Assert.IsTrue(clonedValue.iProperty == intValue);
                        // Conversion from Int32 to Int64 is accepted.
                    }

                    #endregion


                    #region Int32 field to Int64.

                    {
                        A28IntField value = new A28IntField()
                        {
                            iField = intValue1
                        };
                        A28Int64Field clonedValue = serializer.Clone<A28Int64Field>(value);

                        Assert.IsTrue(clonedValue.iField == intValue1);
                        // Conversion from Int32 to Int64 is accepted.
                    }

                    #endregion


                    #region Int32? property to Int64 field.

                    {
                        A28IntNullProperty value = new A28IntNullProperty()
                        {
                            iProperty = intValue1
                        };
                        A28Int64Field clonedValue = serializer.Clone<A28Int64Field>(value);

                        Assert.IsTrue(clonedValue.iField == intValue1);
                    }

                    #endregion



                    #region Int64 property to Int32.

                    {
                        A28Int64Property value = new A28Int64Property();
                        A28IntProperty clonedValue = serializer.Clone<A28IntProperty>(value);

                        Assert.IsTrue(clonedValue.iProperty == 0);
                    }

                    #endregion


                    #region Int64 field to Int32.

                    {
                        A28Int64Field value = new A28Int64Field();
                        A28IntField clonedValue = serializer.Clone<A28IntField>(value);

                        Assert.IsTrue(clonedValue.iField == 0);
                    }

                    #endregion



                    #region Double property to float.

                    {
                        A28DoubleProperty value = new A28DoubleProperty();
                        A28FloatProperty clonedValue = serializer.Clone<A28FloatProperty>(value);

                        Assert.IsTrue(clonedValue.iProperty == 0);
                    }

                    #endregion


                    #region Double field to float.

                    {
                        A28DoubleField value = new A28DoubleField();
                        A28FloatField clonedValue = serializer.Clone<A28FloatField>(value);

                        Assert.IsTrue(clonedValue.iField == 0);
                    }

                    #endregion



                    #region Float property to decimal.

                    {
                        A28FloatProperty value = new A28FloatProperty();
                        A28DecimalProperty clonedValue = serializer.Clone<A28DecimalProperty>(value);

                        Assert.IsTrue(clonedValue.iProperty == 0);
                    }

                    #endregion


                    #region Float field to decimal.

                    {
                        A28FloatField value = new A28FloatField();
                        A28DecimalField clonedValue = serializer.Clone<A28DecimalField>(value);

                        Assert.IsTrue(clonedValue.iField == 0);
                    }

                    #endregion



                    #region Int32 property to Enum.

                    {
                        A28IntProperty value = new A28IntProperty()
                        {
                            iProperty = intValue
                        };
                        A28EnumProperty clonedValue = serializer.Clone<A28EnumProperty>(value);

                        Assert.IsTrue((int)clonedValue.iProperty == intValue);
                        // Restore to not existing enum value is allowed.
                    }

                    #endregion


                    #region Int32 field to Enum.

                    {
                        A28IntField value = new A28IntField()
                        {
                            iField = intValue1
                        };
                        A28EnumField clonedValue = serializer.Clone<A28EnumField>(value);

                        Assert.IsTrue((int)clonedValue.iField == intValue1);
                        // Restore to not existing enum value is allowed.
                    }

                    #endregion



                    #region Enum? property to Int32.

                    {
                        A28EnumNullProperty value = new A28EnumNullProperty();
                        A28IntProperty clonedValue = serializer.Clone<A28IntProperty>(value);

                        Assert.IsNotNull(clonedValue);
                        Assert.IsTrue(clonedValue.iProperty == 0);
                        // NB.! Null value are restored to 0.
                    }

                    #endregion


                    #region  Enum? property to Int32?.

                    {
                        A28EnumNullProperty value = new A28EnumNullProperty();
                        A28IntNullProperty clonedValue = serializer.Clone<A28IntNullProperty>(value);

                        Assert.IsNull(clonedValue.iProperty);
                    }

                    #endregion


                    #region Enum? field to string.

                    {
                        A28EnumNullField value = new A28EnumNullField();
                        A28StringField clonedValue = serializer.Clone<A28StringField>(value);

                        Assert.IsNull(clonedValue.iField);
                    }

                    #endregion



                    #region Enum? property to string, no value.

                    {
                        A28EnumNullProperty value = new A28EnumNullProperty();
                        A28StringProperty clonedValue = serializer.Clone<A28StringProperty>(value);

                        Assert.IsTrue(clonedValue.iProperty == null);
                    }

                    #endregion


                    #region Enum? field to string, no value.

                    {
                        A28EnumNullField value = new A28EnumNullField();
                        A28StringField clonedValue = serializer.Clone<A28StringField>(value);

                        Assert.IsTrue(clonedValue.iField == null);
                    }

                    #endregion



                    #region Enum? property to string, with value.

                    {
                        A28EnumNullProperty value = new A28EnumNullProperty()
                        {
                            iProperty = Enum21.Value2
                        };
                        A28StringProperty clonedValue = serializer.Clone<A28StringProperty>(value);

                        Assert.IsTrue(clonedValue.iProperty == ((int)Enum21.Value2).ToString());
                    }

                    #endregion


                    #region Enum? field to string, no value.

                    {
                        A28EnumNullField value = new A28EnumNullField()
                        {
                            iField = Enum21.Value2
                        };
                        A28StringField clonedValue = serializer.Clone<A28StringField>(value);

                        Assert.IsTrue(clonedValue.iField == ((int)Enum21.Value2).ToString());
                    }

                    #endregion
                }

                #endregion


                #region Lists.

                #region List<Int32> to List<Int64>.

                {
                    #region Set value.

                    A28Int32List value = new A28Int32List();
                    value.list = new List<int>();

                    for (int i = 0; i < cycleLength; i++)
                    {
                        value.list.Add(i);
                    }

                    A28Int64List clonedValue = serializer.Clone<A28Int64List>(value);

                    #endregion


                    #region Check result.

                    Assert.IsNotNull(clonedValue);
                    Assert.IsTrue(clonedValue.list.Count == cycleLength);

                    for (int i = 0; i < cycleLength; i++)
                    {
                        Assert.IsTrue(clonedValue.list[i] == i);
                    }

                    #endregion
                }

                #endregion


                #region List<Int32> to List<Int32?>.

                {
                    #region Set value.

                    A28Int32List value = new A28Int32List();
                    value.list = new List<int>();

                    for (int i = 0; i < cycleLength; i++)
                    {
                        value.list.Add(i);
                    }

                    A28IntNullableList clonedValue = serializer.Clone<A28IntNullableList>(value);

                    #endregion


                    #region Check result.

                    Assert.IsNotNull(clonedValue);
                    Assert.IsTrue(clonedValue.list.Count == cycleLength);

                    for (int i = 0; i < cycleLength; i++)
                    {
                        Assert.IsTrue(clonedValue.list[i] == i);
                    }

                    #endregion
                }

                #endregion


                #region List<string> to List<Int32>.

                {
                    #region Set value.

                    A28StringList value = new A28StringList();
                    value.list = new List<string>();

                    for (int i = 0; i < cycleLength; i++)
                    {
                        value.list.Add(i.ToString());
                    }

                    A28Int32List clonedValue = serializer.Clone<A28Int32List>(value);

                    #endregion


                    #region Check result.

                    Assert.IsNotNull(clonedValue);
                    Assert.IsTrue(clonedValue.list.Count == cycleLength);

                    for (int i = 0; i < cycleLength; i++)
                    {
                        Assert.IsTrue(clonedValue.list[i] == i);
                    }

                    #endregion
                }

                #endregion

                #endregion


                #region Dictionaries.

                #region Dictionary<Int32, Int32> to Dictionary<Int64, Int64>.

                {
                    #region Set value.

                    A28Int32Dictionary value = new A28Int32Dictionary();
                    value.dictionary = new Dictionary<int, int>();

                    for (int i = 0; i < cycleLength; i++)
                    {
                        value.dictionary.Add(i, i);
                    }

                    A28Int64Dictionary clonedValue = serializer.Clone<A28Int64Dictionary>(value);

                    #endregion


                    #region Check result.

                    Assert.IsNotNull(clonedValue);
                    Assert.IsTrue(clonedValue.dictionary.Count == cycleLength);

                    for (int i = 0; i < cycleLength; i++)
                    {
                        Assert.IsTrue(clonedValue.dictionary[i] == i);
                    }

                    #endregion
                }

                #endregion


                #region Dictionary<Int32, Int32> to Dictionary<Int32?, Int32?>.

                {
                    #region Set value.

                    A28Int32Dictionary value = new A28Int32Dictionary();
                    value.dictionary = new Dictionary<int, int>();

                    for (int i = 0; i < cycleLength; i++)
                    {
                        value.dictionary.Add(i, i);
                    }

                    A28IntNullableDictionary clonedValue = serializer.Clone<A28IntNullableDictionary>(value);

                    #endregion


                    #region Check result.

                    Assert.IsNotNull(clonedValue);
                    Assert.IsTrue(clonedValue.dictionary.Count == cycleLength);

                    for (int i = 0; i < cycleLength; i++)
                    {
                        Assert.IsTrue(clonedValue.dictionary[i] == i);
                    }

                    #endregion
                }

                #endregion


                #region Dictionary<string> to Dictionary<Int32>.

                {
                    #region Set value.

                    A28StringDictionary value = new A28StringDictionary();
                    value.dictionary = new Dictionary<string, string>();

                    for (int i = 0; i < cycleLength; i++)
                    {
                        value.dictionary.Add(i.ToString(), i.ToString());
                    }

                    A28Int32Dictionary clonedValue = serializer.Clone<A28Int32Dictionary>(value);

                    #endregion


                    #region Check result.

                    Assert.IsNotNull(clonedValue);
                    Assert.IsTrue(clonedValue.dictionary.Count == cycleLength);

                    for (int i = 0; i < cycleLength; i++)
                    {
                        Assert.IsTrue(clonedValue.dictionary[i] == i);
                    }

                    #endregion
                }

                #endregion

                #endregion

                #endregion

                #endregion


                #region Serialize structures.

                #region Plain properties.

                //if (!useProcessedTypes)
                {
                    #region Restore strong typed plain property to object one.

                    #region Deserialize primitive type fields, null values stay null.

                    {
                        // Set value.
                        APlainProperties value = createPlainPropertiesObject(false);
                        AStructPlainProperties clonedValue = serializer.Clone<AStructPlainProperties>(value);

                        // Check result.
                        compareStructAndTypedPlainValuesObjects(clonedValue, value, false);
                    }

                    #endregion


                    #region Deserialize primitive type fields, null values are set.

                    {
                        // Set value.
                        APlainProperties value = createPlainPropertiesObject(true);
                        AStructPlainProperties clonedValue = serializer.Clone<AStructPlainProperties>(value);

                        // Check result.
                        compareStructAndTypedPlainValuesObjects(clonedValue, value, true);
                    }

                    #endregion


                    #region Deserialize primitive type fields in object as a property, null values are set.

                    {
                        #region Set value.

                        BPlainProperties value = new BPlainProperties();
                        value.aField = createPlainPropertiesObject(true);
                        value.aProperty = createPlainPropertiesObject(false);

                        BStructPlainProperties clonedValue = serializer.Clone<BStructPlainProperties>(value);

                        #endregion


                        #region Check result.

                        Assert.IsNotNull(clonedValue);
                        compareStructAndTypedPlainValuesObjects(clonedValue.aField, value.aField, true);
                        compareStructAndTypedPlainValuesObjects((AStructPlainProperties)clonedValue.aProperty, value.aProperty, false);

                        #endregion
                    }

                    #endregion

                    #endregion


                    #region Restore object type plain property to strong typed one.

                    #region Deserialize primitive type fields, null values stay null.

                    {
                        // Set value.
                        AStructPlainProperties value = createPlainPropertiesStruct(false);
                        APlainProperties clonedValue = serializer.Clone<APlainProperties>(value);

                        // Check result.
                        compareStructAndTypedPlainValuesObjects(value, clonedValue, false);
                    }

                    #endregion


                    #region Deserialize primitive type fields, null values are set.

                    {
                        // Set value.
                        AStructPlainProperties value = createPlainPropertiesStruct(true);
                        APlainProperties clonedValue = serializer.Clone<APlainProperties>(value);

                        // Check result.
                        compareStructAndTypedPlainValuesObjects(value, clonedValue, true);
                    }

                    #endregion


                    #region Deserialize primitive type fields in object as a property, null values are set.

                    {
                        #region Set value.

                        BStructPlainProperties value = new BStructPlainProperties();
                        value.aField = createPlainPropertiesStruct(true);
                        value.aProperty = createPlainPropertiesStruct(false);

                        BPlainProperties clonedValue = serializer.Clone<BPlainProperties>(value);

                        #endregion


                        #region Check result.

                        Assert.IsNotNull(clonedValue);
                        compareStructAndTypedPlainValuesObjects(value.aField, clonedValue.aField, true);
                        compareStructAndTypedPlainValuesObjects((AStructPlainProperties)value.aProperty, clonedValue.aProperty, false);

                        #endregion
                    }

                    #endregion

                    #endregion


                    #region Restore object type plain property to object typed one.

                    #region Deserialize primitive type fields, null values stay null.

                    {
                        // Set value.
                        AStructPlainProperties value = createPlainPropertiesStruct(false);
                        AStructPlainProperties clonedValue = serializer.Clone<AStructPlainProperties>(value);

                        // Check result.
                        compareStructPlainValuesObjects(value, clonedValue, false);
                    }

                    #endregion


                    #region Deserialize primitive type fields, null values are set.

                    {
                        // Set value.
                        AStructPlainProperties value = createPlainPropertiesStruct(true);
                        AStructPlainProperties clonedValue = serializer.Clone<AStructPlainProperties>(value);

                        // Check result.
                        compareStructPlainValuesObjects(value, clonedValue, true);
                    }

                    #endregion


                    #region Deserialize primitive type fields in object as a property, nullable property is not set, null values are set.

                    {
                        #region Set value.

                        BStructPlainProperties value = new BStructPlainProperties();
                        value.aField = createPlainPropertiesStruct(true);

                        BStructPlainProperties clonedValue = serializer.Clone<BStructPlainProperties>(value);

                        #endregion


                        #region Check result.

                        Assert.IsNotNull(clonedValue);
                        compareStructPlainValuesObjects(value.aField, clonedValue.aField, true);
                        Assert.IsNull(value.aProperty);
                        Assert.IsNull(clonedValue.aProperty);

                        #endregion
                    }

                    #endregion


                    #region Deserialize primitive type fields in object as a property, null values are set.

                    {
                        #region Set value.

                        BStructPlainProperties value = new BStructPlainProperties();
                        value.aField = createPlainPropertiesStruct(true);
                        value.aProperty = createPlainPropertiesStruct(false);

                        BStructPlainProperties clonedValue = serializer.Clone<BStructPlainProperties>(value);

                        #endregion


                        #region Check result.

                        Assert.IsNotNull(clonedValue);
                        compareStructPlainValuesObjects(value.aField, clonedValue.aField, true);
                        compareStructPlainValuesObjects((AStructPlainProperties)value.aProperty, (AStructPlainProperties)clonedValue.aProperty, false);

                        #endregion
                    }

                    #endregion

                    #endregion
                }

                #endregion


                #region Serialize List<struct?>.

                #region Serialize List<struct?>, list is not created.

                {
                    #region Set value.

                    C23 value = new C23();
                    value.bField = new B23();

                    Assert.IsNull(value.bField.aList);

                    C23 clonedValue = (C23)serializer.Clone(value);

                    #endregion


                    #region Check result.

                    Assert.IsNull(clonedValue.bField.aList);
                    Assert.IsNull(clonedValue.bProperty);

                    #endregion
                }

                #endregion


                #region Serialize List<struct?>, empty list is created.

                {
                    #region Set value.

                    C23 value = new C23();
                    value.bField = new B23();
                    value.bField.aList = new List<A23?>();

                    Assert.IsNotNull(value.bField.aList);

                    C23 clonedValue = (C23)serializer.Clone(value);

                    #endregion


                    #region Check result.

                    Assert.IsNotNull(clonedValue.bField.aList);
                    Assert.IsTrue(clonedValue.bField.aList.Count == 0);
                    Assert.IsNull(clonedValue.bProperty);

                    #endregion
                }

                #endregion


                #region Serialize List<struct?> with values.

                {
                    #region Set value.

                    C23 value = new C23();
                    value.bField = new B23();
                    value.bField.aList = new List<A23?>();

                    for (int i = 0; i < cycleLength; i++)
                    {
                        value.bField.aList.Add(new A23()
                        {
                            iField = i,
                            sProperty = i.ToString()
                        });
                    }

                    C23 clonedValue = (C23)serializer.Clone(value);

                    #endregion


                    #region Check values.

                    for (int i = 0; i < cycleLength; i++)
                    {
                        A23 aValue = (A23)clonedValue.bField.aList[i];

                        Assert.IsTrue(aValue.iField == i);
                        Assert.IsTrue(aValue.sProperty == i.ToString());
                    }

                    #endregion
                }

                #endregion


                #region Serialize List<struct?> with null and not null values.

                {
                    #region Set value.

                    C23 value = new C23();
                    value.bField = new B23();
                    value.bField.aList = new List<A23?>();

                    for (int i = 0; i < cycleLength; i++)
                    {
                        // Null value.
                        if (i % 2 == 0)
                        {
                            value.bField.aList.Add(null);
                        }

                        // Not null value.
                        else
                        {
                            value.bField.aList.Add(new A23()
                            {
                                iField = i,
                                sProperty = i.ToString()
                            });
                        }
                    }

                    C23 clonedValue = (C23)serializer.Clone(value);

                    #endregion


                    #region Check values.

                    for (int i = 0; i < cycleLength; i++)
                    {
                        if (i % 2 == 0)
                        {
                            Assert.IsNull(clonedValue.bField.aList[i]);
                        }
                        else
                        {
                            A23 aValue = (A23)clonedValue.bField.aList[i];

                            Assert.IsTrue(aValue.iField == i);
                            Assert.IsTrue(aValue.sProperty == i.ToString());
                        }
                    }

                    #endregion
                }

                #endregion

                #endregion


                #region Serialize Dictionary<, struct?>.

                #region Serialize Dictionary<, struct?>, collection is not created.

                {
                    #region Set value.

                    C24 value = new C24();
                    value.bField = new B24();

                    Assert.IsNull(value.bField.aCollection);

                    C24 clonedValue = (C24)serializer.Clone(value);

                    #endregion


                    #region Check result.

                    Assert.IsNull(clonedValue.bField.aCollection);
                    Assert.IsNull(clonedValue.bProperty);

                    #endregion
                }

                #endregion


                #region Serialize Dictionary<, struct?>, empty collection is created.

                {
                    #region Set value.

                    C24 value = new C24();
                    value.bField = new B24();
                    value.bField.aCollection = new Dictionary<int, A24?>();

                    Assert.IsNotNull(value.bField.aCollection);

                    C24 clonedValue = (C24)serializer.Clone(value);

                    #endregion


                    #region Check result.

                    Assert.IsNotNull(clonedValue.bField.aCollection);
                    Assert.IsNull(clonedValue.bProperty);

                    #endregion
                }

                #endregion


                #region Serialize Dictionary<, struct?> with values.

                {
                    #region Set value.

                    C24 value = new C24();
                    value.bField = new B24();
                    value.bField.aCollection = new Dictionary<int, A24?>();

                    for (int i = 0; i < cycleLength; i++)
                    {
                        value.bField.aCollection.Add(
                            i,
                            new A24()
                            {
                                iField = i,
                                sProperty = i.ToString()
                            });
                    }

                    C24 clonedValue = (C24)serializer.Clone(value);

                    #endregion


                    #region Check result.

                    Assert.IsNotNull(clonedValue.bField.aCollection);
                    Assert.IsNull(clonedValue.bProperty);

                    for (int i = 0; i < cycleLength; i++)
                    {
                        A24 aValue = (A24)clonedValue.bField.aCollection[i];

                        Assert.IsTrue(aValue.iField == i);
                        Assert.IsTrue(aValue.sProperty == i.ToString());
                    }

                    #endregion
                }

                #endregion


                #region Serialize Dictionary<, struct?> with null and not null values.

                {
                    #region Set value.

                    C24 value = new C24();
                    value.bField = new B24();
                    value.bField.aCollection = new Dictionary<int, A24?>();

                    for (int i = 0; i < cycleLength; i++)
                    {
                        // Null value.
                        if (i % 2 == 0)
                        {
                            value.bField.aCollection.Add(i, null);
                        }

                        else
                        {
                            // Not null value.
                            value.bField.aCollection.Add(
                                i,
                                new A24()
                                {
                                    iField = i,
                                    sProperty = i.ToString()
                                });
                        }
                    }

                    C24 clonedValue = (C24)serializer.Clone(value);

                    #endregion


                    #region Check result.

                    Assert.IsNotNull(clonedValue.bField.aCollection);
                    Assert.IsNull(clonedValue.bProperty);

                    for (int i = 0; i < cycleLength; i++)
                    {
                        // Null value.
                        if (i % 2 == 0)
                        {
                            Assert.IsNull(clonedValue.bField.aCollection[i]);
                        }

                        // Not null value.
                        else
                        {
                            A24 aValue = (A24)clonedValue.bField.aCollection[i];

                            Assert.IsTrue(aValue.iField == i);
                            Assert.IsTrue(aValue.sProperty == i.ToString());
                        }
                    }

                    #endregion
                }

                #endregion

                #endregion

                #endregion


                #region Serialize collections.

                #region Serialize List<T>.

                #region Serialize List<T>, list is not created.

                {
                    #region Set value.

                    C15 value = new C15();
                    value.bField = new B15();

                    Assert.IsNull(value.bField.aList);

                    C15 clonedValue = (C15)serializer.Clone(value);

                    #endregion


                    #region Check result.

                    Assert.IsNull(clonedValue.bField.aList);
                    Assert.IsNotNull(clonedValue.bField.aListPreset);
                    Assert.IsNull(clonedValue.bProperty);

                    #endregion
                }

                #endregion


                #region Serialize List<T>, empty list is created, preset list is set to null.

                {
                    #region Set value.

                    C15 value = new C15();
                    value.bField = new B15();
                    value.bField.aList = new List<A15>();
                    value.bField.aListPreset = null;

                    Assert.IsNotNull(value.bField.aList);

                    C15 clonedValue = (C15)serializer.Clone(value);

                    #endregion


                    #region Check result.

                    Assert.IsNotNull(clonedValue.bField.aList);
                    Assert.IsTrue(clonedValue.bField.aList.Count == 0);

                    Assert.IsNull(clonedValue.bField.aListPreset);
                    Assert.IsNull(clonedValue.bProperty);

                    #endregion
                }

                #endregion


                #region Serialize List<T> with values.

                {
                    #region Set value.

                    C15 value = new C15();
                    value.bField = new B15();
                    value.bField.aList = new List<A15>();

                    for (int i = 0; i < cycleLength; i++)
                    {
                        value.bField.aList.Add(new A15()
                        {
                            iField = i,
                            sProperty = i.ToString()
                        });
                    }

                    C15 clonedValue = (C15)serializer.Clone(value);

                    #endregion


                    #region Check values.

                    for (int i = 0; i < cycleLength; i++)
                    {
                        A15 aValue = clonedValue.bField.aList[i];

                        Assert.IsTrue(aValue.iField == i);
                        Assert.IsTrue(aValue.sProperty == i.ToString());
                    }

                    #endregion
                }

                #endregion


                #region Serialize List<T> with null and not null values.

                {
                    #region Set value.

                    C15 value = new C15();
                    value.bField = new B15();
                    value.bField.aList = new List<A15>();

                    for (int i = 0; i < cycleLength; i++)
                    {
                        // Null value.
                        if (i % 2 == 0)
                        {
                            value.bField.aList.Add(null);
                        }

                        // Not null value.
                        else
                        {
                            value.bField.aList.Add(new A15()
                            {
                                iField = i,
                                sProperty = i.ToString()
                            });
                        }
                    }

                    C15 clonedValue = (C15)serializer.Clone(value);

                    #endregion


                    #region Check values.

                    for (int i = 0; i < cycleLength; i++)
                    {
                        A15 aValue = clonedValue.bField.aList[i];

                        if (i % 2 == 0)
                        {
                            Assert.IsNull(aValue);
                        }
                        else
                        {
                            Assert.IsTrue(aValue.iField == i);
                            Assert.IsTrue(aValue.sProperty == i.ToString());
                        }
                    }

                    #endregion
                }

                #endregion

                #endregion


                #region Serialize Dictionary<>.

                #region Serialize Dictionary<>, collection is not created.

                {
                    #region Set value.

                    C16 value = new C16();
                    value.bField = new B16();

                    Assert.IsNull(value.bField.aCollection);

                    C16 clonedValue = (C16)serializer.Clone(value);

                    #endregion


                    #region Check result.

                    Assert.IsNull(clonedValue.bField.aCollection);
                    Assert.IsNotNull(clonedValue.bField.aCollectionPreset);
                    Assert.IsNull(clonedValue.bProperty);

                    #endregion
                }

                #endregion


                #region Serialize Dictionary<>, empty collection is created, preset collection is set to null.

                {
                    #region Set value.

                    C16 value = new C16();
                    value.bField = new B16();
                    value.bField.aCollection = new Dictionary<int, A16>();
                    value.bField.aCollectionPreset = null;

                    Assert.IsNotNull(value.bField.aCollection);

                    C16 clonedValue = (C16)serializer.Clone(value);

                    #endregion


                    #region Check result.

                    Assert.IsNotNull(clonedValue.bField.aCollection);
                    Assert.IsNull(clonedValue.bField.aCollectionPreset);
                    Assert.IsNull(clonedValue.bProperty);

                    #endregion
                }

                #endregion


                #region Serialize Dictionary<> with values.

                {
                    #region Set value.

                    C16 value = new C16();
                    value.bField = new B16();
                    value.bField.aCollection = new Dictionary<int, A16>();

                    for (int i = 0; i < cycleLength; i++)
                    {
                        value.bField.aCollection.Add(
                            i,
                            new A16()
                            {
                                iField = i,
                                sProperty = i.ToString()
                            });
                    }

                    C16 clonedValue = (C16)serializer.Clone(value);

                    #endregion


                    #region Check result.

                    Assert.IsNotNull(clonedValue.bField.aCollection);
                    Assert.IsNull(clonedValue.bProperty);

                    for (int i = 0; i < cycleLength; i++)
                    {
                        A16 aValue = clonedValue.bField.aCollection[i];

                        Assert.IsTrue(aValue.iField == i);
                        Assert.IsTrue(aValue.sProperty == i.ToString());
                    }

                    #endregion
                }

                #endregion


                #region Serialize Dictionary<> with null and not null values.

                {
                    #region Set value.

                    C16 value = new C16();
                    value.bField = new B16();
                    value.bField.aCollection = new Dictionary<int, A16>();

                    for (int i = 0; i < cycleLength; i++)
                    {
                        // Null value.
                        if (i % 2 == 0)
                        {
                            value.bField.aCollection.Add(i, null);
                        }

                        else
                        {
                            // Not null value.
                            value.bField.aCollection.Add(
                                i,
                                new A16()
                                {
                                    iField = i,
                                    sProperty = i.ToString()
                                });
                        }
                    }

                    C16 clonedValue = (C16)serializer.Clone(value);

                    #endregion


                    #region Check result.

                    Assert.IsNotNull(clonedValue.bField.aCollection);
                    Assert.IsNull(clonedValue.bProperty);

                    for (int i = 0; i < cycleLength; i++)
                    {
                        // Null value.
                        if (i % 2 == 0)
                        {
                            Assert.IsNull(clonedValue.bField.aCollection[i]);
                        }

                        // Not null value.
                        else
                        {
                            A16 aValue = clonedValue.bField.aCollection[i];

                            Assert.IsTrue(aValue.iField == i);
                            Assert.IsTrue(aValue.sProperty == i.ToString());
                        }
                    }

                    #endregion
                }

                #endregion

                #endregion


                #region Serialize T[] and int?[].

                #region Serialize T[], array is not created.

                {
                    #region Set value.

                    C32 value = new C32();
                    value.bField = new B32();

                    Assert.IsNull(value.bField.aArray);
                    Assert.IsNull(value.bField.aArrayNullable);

                    C32 clonedValue = (C32)serializer.Clone(value);

                    #endregion


                    #region Check result.

                    Assert.IsNull(clonedValue.bField.aArray);
                    Assert.IsNotNull(clonedValue.bField.aArrayPreset);
                    Assert.IsNull(clonedValue.bField.aArrayNullable);
                    Assert.IsNull(clonedValue.bProperty);

                    #endregion
                }

                #endregion


                #region Serialize T[], empty array is created, preset array is set to null.

                {
                    #region Set value.

                    C32 value = new C32();
                    value.bField = new B32();
                    value.bField.aArray = new A32[0];
                    value.bField.aArrayPreset = null;
                    value.bField.aArrayNullable = new int?[0];

                    Assert.IsNotNull(value.bField.aArray);

                    C32 clonedValue = (C32)serializer.Clone(value);

                    #endregion


                    #region Check result.

                    Assert.IsNotNull(clonedValue.bField.aArray);
                    Assert.IsTrue(clonedValue.bField.aArray.Length == 0);

                    Assert.IsNull(clonedValue.bField.aArrayPreset);
                    Assert.IsNull(clonedValue.bProperty);

                    Assert.IsNotNull(clonedValue.bField.aArrayNullable);
                    Assert.IsTrue(clonedValue.bField.aArrayNullable.Length == 0);

                    #endregion
                }

                #endregion


                #region Serialize T[] with values.

                {
                    #region Set value.

                    C32 value = new C32();
                    value.bField = new B32();
                    value.bField.aArray = new A32[cycleLength];
                    value.bField.aArrayNullable = new int?[cycleLength];

                    for (int i = 0; i < cycleLength; i++)
                    {
                        value.bField.aArray[i] = new A32()
                        {
                            iField = i,
                            sProperty = i.ToString()
                        };

                        value.bField.aArrayNullable[i] = i;
                    }

                    C32 clonedValue = (C32)serializer.Clone(value);

                    #endregion


                    #region Check values.

                    for (int i = 0; i < cycleLength; i++)
                    {
                        A32 aValue = clonedValue.bField.aArray[i];

                        Assert.IsTrue(aValue.iField == i);
                        Assert.IsTrue(aValue.sProperty == i.ToString());


                        Assert.IsTrue(clonedValue.bField.aArrayNullable[i] == i);
                    }

                    #endregion
                }

                #endregion


                #region Serialize T[] with null and not null values.

                {
                    #region Set value.

                    C32 value = new C32();
                    value.bField = new B32();
                    value.bField.aArray = new A32[cycleLength];
                    value.bField.aArrayNullable = new int?[cycleLength];

                    for (int i = 0; i < cycleLength; i++)
                    {
                        // Null value.
                        if (i % 2 == 0)
                        {
                            value.bField.aArray[i] = null;
                            value.bField.aArrayNullable[i] = null;
                        }

                        // Not null value.
                        else
                        {
                            value.bField.aArray[i] = new A32()
                            {
                                iField = i,
                                sProperty = i.ToString()
                            };

                            value.bField.aArrayNullable[i] = i;
                        }
                    }

                    C32 clonedValue = (C32)serializer.Clone(value);

                    #endregion


                    #region Check values.

                    for (int i = 0; i < cycleLength; i++)
                    {
                        A32 aValue = clonedValue.bField.aArray[i];

                        if (i % 2 == 0)
                        {
                            Assert.IsNull(aValue);
                            Assert.IsNull(clonedValue.bField.aArrayNullable[i]);
                        }
                        else
                        {
                            Assert.IsTrue(aValue.iField == i);
                            Assert.IsTrue(aValue.sProperty == i.ToString());

                            Assert.IsTrue(clonedValue.bField.aArrayNullable[i] == i);
                        }
                    }

                    #endregion
                }

                #endregion

                #endregion



                #region Serialize List<List<T>>.

                #region Serialize List<List<T>>, list is not created.

                {
                    #region Set value.

                    C18 value = new C18();
                    value.bField = new B18();

                    Assert.IsNull(value.bField.aList);

                    C18 clonedValue = (C18)serializer.Clone(value);

                    #endregion


                    #region Check result.

                    Assert.IsNull(clonedValue.bField.aList);
                    Assert.IsNull(clonedValue.bProperty);

                    #endregion
                }

                #endregion


                #region Serialize List<T>, empty list is created.

                {
                    #region Set value.

                    C18 value = new C18();
                    value.bField = new B18();
                    value.bField.aList = new List<List<A18>>();

                    Assert.IsNotNull(value.bField.aList);

                    C18 clonedValue = (C18)serializer.Clone(value);

                    #endregion


                    #region Check result.

                    Assert.IsNotNull(clonedValue.bField.aList);
                    Assert.IsTrue(clonedValue.bField.aList.Count == 0);
                    Assert.IsNull(clonedValue.bProperty);

                    #endregion
                }

                #endregion


                #region Serialize List<T> with values.

                {
                    #region Set value.

                    C18 value = new C18();
                    value.bField = new B18();
                    value.bField.aList = new List<List<A18>>();

                    for (int i = 0; i < cycleLength; i++)
                    {
                        List<A18> currentList = new List<A18>();
                        value.bField.aList.Add(currentList);

                        for (int j = 0; j < cycleLength; j++)
                        {
                            currentList.Add(new A18()
                            {
                                iField = j,
                                sProperty = j.ToString()
                            });
                        }
                    }

                    C18 clonedValue = (C18)serializer.Clone(value);

                    #endregion


                    #region Check values.

                    for (int i = 0; i < cycleLength; i++)
                    {
                        List<A18> currentList = clonedValue.bField.aList[i];

                        for (int j = 0; j < cycleLength; j++)
                        {
                            A18 aValue = currentList[j];

                            Assert.IsTrue(aValue.iField == j);
                            Assert.IsTrue(aValue.sProperty == j.ToString());
                        }
                    }

                    #endregion
                }

                #endregion


                #region Serialize List<T> with null and not null values.

                {
                    #region Set value.

                    C18 value = new C18();
                    value.bField = new B18();
                    value.bField.aList = new List<List<A18>>();

                    for (int i = 0; i < cycleLength; i++)
                    {
                        // Null value.
                        if (i % 2 == 0)
                        {
                            value.bField.aList.Add(null);
                        }

                        // Not null value.
                        else
                        {
                            List<A18> currentList = new List<A18>();
                            value.bField.aList.Add(currentList);

                            for (int j = 0; j < cycleLength; j++)
                            {
                                currentList.Add(new A18()
                                {
                                    iField = j,
                                    sProperty = j.ToString()
                                });
                            }
                        }
                    }

                    C18 clonedValue = (C18)serializer.Clone(value);

                    #endregion


                    #region Check values.

                    for (int i = 0; i < cycleLength; i++)
                    {
                        // Null value.
                        if (i % 2 == 0)
                        {
                            Assert.IsNull(clonedValue.bField.aList[i]);
                        }

                        // Not null value.
                        else
                        {
                            List<A18> currentList = clonedValue.bField.aList[i];

                            for (int j = 0; j < cycleLength; j++)
                            {
                                A18 aValue = currentList[j];

                                Assert.IsTrue(aValue.iField == j);
                                Assert.IsTrue(aValue.sProperty == j.ToString());
                            }
                        }
                    }

                    #endregion
                }

                #endregion

                #endregion



                #region Serialize Dictionary<,Dictionary<>>.

                #region Serialize Dictionary<,Dictionary<>>, collection is not created.

                {
                    #region Set value.

                    C19 value = new C19();
                    value.bField = new B19();

                    Assert.IsNull(value.bField.aCollection);

                    C19 clonedValue = (C19)serializer.Clone(value);

                    #endregion


                    #region Check result.

                    Assert.IsNull(clonedValue.bField.aCollection);
                    Assert.IsNull(clonedValue.bProperty);

                    #endregion
                }

                #endregion


                #region Serialize Dictionary<,Dictionary<>>, empty collection is created.

                {
                    #region Set value.

                    C19 value = new C19();
                    value.bField = new B19();
                    value.bField.aCollection = new Dictionary<int, Dictionary<int, A19>>();

                    Assert.IsNotNull(value.bField.aCollection);

                    C19 clonedValue = (C19)serializer.Clone(value);

                    #endregion


                    #region Check result.

                    Assert.IsNotNull(clonedValue.bField.aCollection);
                    Assert.IsNull(clonedValue.bProperty);

                    #endregion
                }

                #endregion


                #region Serialize Dictionary<,Dictionary<>> with values.

                {
                    #region Set value.

                    C19 value = new C19();
                    value.bField = new B19();
                    value.bField.aCollection = new Dictionary<int, Dictionary<int, A19>>();

                    for (int i = 0; i < cycleLength; i++)
                    {
                        Dictionary<int, A19> currentCollection = new Dictionary<int, A19>();
                        value.bField.aCollection.Add(i, currentCollection);

                        for (int j = 0; j < cycleLength; j++)
                        {
                            currentCollection.Add(
                                j,
                                new A19()
                                {
                                    iField = j,
                                    sProperty = j.ToString()
                                });
                        }
                    }

                    C19 clonedValue = (C19)serializer.Clone(value);

                    #endregion


                    #region Check result.

                    for (int i = 0; i < cycleLength; i++)
                    {
                        Dictionary<int, A19> currentList = clonedValue.bField.aCollection[i];

                        for (int j = 0; j < cycleLength; j++)
                        {
                            A19 aValue = currentList[j];

                            Assert.IsTrue(aValue.iField == j);
                            Assert.IsTrue(aValue.sProperty == j.ToString());
                        }
                    }


                    Assert.IsNull(clonedValue.bProperty);

                    #endregion
                }

                #endregion


                #region Serialize Dictionary<,Dictionary<>> with null and not null values.

                {
                    #region Set value.

                    C19 value = new C19();
                    value.bField = new B19();
                    value.bField.aCollection = new Dictionary<int, Dictionary<int, A19>>();

                    for (int i = 0; i < cycleLength; i++)
                    {
                        // Null value.
                        if (i % 2 == 0)
                        {
                            value.bField.aCollection.Add(i, null);
                        }

                        // Not null value.
                        else
                        {
                            Dictionary<int, A19> currentCollection = new Dictionary<int, A19>();
                            value.bField.aCollection.Add(i, currentCollection);

                            for (int j = 0; j < cycleLength; j++)
                            {
                                currentCollection.Add(
                                    j,
                                    new A19()
                                    {
                                        iField = j,
                                        sProperty = j.ToString()
                                    });
                            }
                        }
                    }

                    C19 clonedValue = (C19)serializer.Clone(value);

                    #endregion


                    #region Check result.

                    for (int i = 0; i < cycleLength; i++)
                    {
                        // Null value.
                        if (i % 2 == 0)
                        {
                            Assert.IsNull(clonedValue.bField.aCollection[i]);
                        }

                        // Not null value.
                        else
                        {
                            Dictionary<int, A19> currentList = clonedValue.bField.aCollection[i];

                            for (int j = 0; j < cycleLength; j++)
                            {
                                A19 aValue = currentList[j];

                                Assert.IsTrue(aValue.iField == j);
                                Assert.IsTrue(aValue.sProperty == j.ToString());
                            }
                        }
                    }


                    Assert.IsNull(clonedValue.bProperty);

                    #endregion
                }

                #endregion

                #endregion


                #region Serialize Dictionary<Dictionary<>,>.

                // NB.! Check includes usage of int? type.

                #region Serialize Dictionary<Dictionary<>,>, collection is not created.

                {
                    #region Set value.

                    C20 value = new C20();
                    value.bField = new B20();

                    Assert.IsNull(value.bField.aCollection);

                    C20 clonedValue = (C20)serializer.Clone(value);

                    #endregion


                    #region Check result.

                    Assert.IsNull(clonedValue.bField.aCollection);
                    Assert.IsNull(clonedValue.bProperty);

                    #endregion
                }

                #endregion


                #region Serialize Dictionary<Dictionary<>,>, empty collection is created.

                {
                    #region Set value.

                    C20 value = new C20();
                    value.bField = new B20();
                    value.bField.aCollection = new Dictionary<Dictionary<A20, int?>, int?>();

                    Assert.IsNotNull(value.bField.aCollection);

                    C20 clonedValue = (C20)serializer.Clone(value);

                    #endregion


                    #region Check result.

                    Assert.IsNotNull(clonedValue.bField.aCollection);
                    Assert.IsNull(clonedValue.bProperty);

                    #endregion
                }

                #endregion


                #region Serialize Dictionary<Dictionary<>,> with values.

                {
                    #region Set value.

                    C20 value = new C20();
                    value.bField = new B20();
                    value.bField.aCollection = new Dictionary<Dictionary<A20, int?>, int?>();

                    for (int i = 0; i < cycleLength; i++)
                    {
                        Dictionary<A20, int?> currentCollection = new Dictionary<A20, int?>();
                        value.bField.aCollection.Add(currentCollection, i);

                        for (int j = 0; j < cycleLength; j++)
                        {
                            currentCollection.Add(
                                new A20()
                                {
                                    iField = j,
                                    sProperty = j.ToString()
                                },
                                j);
                        }
                    }

                    C20 clonedValue = (C20)serializer.Clone(value);

                    #endregion


                    #region Check result.

                    Assert.IsTrue(clonedValue.bField.aCollection.Count == cycleLength);

                    int k = 0;
                    foreach (KeyValuePair<Dictionary<A20, int?>, int?> pair in clonedValue.bField.aCollection)
                    {
                        Dictionary<A20, int?> currentList = pair.Key;
                        Assert.IsTrue(currentList.Count == cycleLength);
                        Assert.IsTrue(pair.Value == k);

                        int m = 0;
                        foreach (KeyValuePair<A20, int?> subPair in currentList)
                        {
                            A20 aValue = subPair.Key;
                            Assert.IsTrue(subPair.Value == m);

                            Assert.IsTrue(aValue.iField == m);
                            Assert.IsTrue(aValue.sProperty == m.ToString());

                            m++;
                        }

                        k++;
                    }


                    Assert.IsNull(clonedValue.bProperty);

                    #endregion
                }

                #endregion


                #region Serialize Dictionary<Dictionary<>,> with null and not null values.

                {
                    #region Set value.

                    C20 value = new C20();
                    value.bField = new B20();
                    value.bField.aCollection = new Dictionary<Dictionary<A20, int?>, int?>();

                    for (int i = 0; i < cycleLength; i++)
                    {
                        Dictionary<A20, int?> currentCollection = new Dictionary<A20, int?>();

                        for (int j = 0; j < cycleLength; j++)
                        {
                            currentCollection.Add(
                                new A20()
                                {
                                    iField = j,
                                    sProperty = j.ToString()
                                },
                                j);
                        }

                        // Null value.
                        if (i % 2 == 0)
                        {
                            value.bField.aCollection.Add(currentCollection, i);
                        }

                        // Not null value.
                        else
                        {
                            value.bField.aCollection.Add(currentCollection, null);
                        }
                    }

                    C20 clonedValue = (C20)serializer.Clone(value);

                    #endregion


                    #region Check result.

                    Assert.IsTrue(clonedValue.bField.aCollection.Count == cycleLength);

                    int k = 0;
                    foreach (KeyValuePair<Dictionary<A20, int?>, int?> pair in clonedValue.bField.aCollection)
                    {
                        // Null value.
                        if (k % 2 == 0)
                        {
                            Assert.IsTrue(pair.Value == k);
                        }

                        // Not null value.
                        else
                        {
                            Assert.IsNull(pair.Value);
                        }


                        Dictionary<A20, int?> currentList = pair.Key;
                        Assert.IsTrue(currentList.Count == cycleLength);

                        int m = 0;
                        foreach (KeyValuePair<A20, int?> subPair in currentList)
                        {
                            A20 aValue = subPair.Key;
                            Assert.IsTrue(subPair.Value == m);

                            Assert.IsTrue(aValue.iField == m);
                            Assert.IsTrue(aValue.sProperty == m.ToString());

                            m++;
                        }

                        k++;
                    }


                    Assert.IsNull(clonedValue.bProperty);

                    #endregion
                }

                #endregion

                #endregion



                #region Serialize T[][] and int?[][].

                #region Serialize T[][], array is not created.

                {
                    #region Set value.

                    C33 value = new C33();
                    value.bField = new B33();

                    Assert.IsNull(value.bField.aArray);

                    C33 clonedValue = (C33)serializer.Clone(value);

                    #endregion


                    #region Check result.

                    Assert.IsNull(clonedValue.bField.aArray);
                    Assert.IsNull(clonedValue.bField.aArrayNullable);
                    Assert.IsNull(clonedValue.bProperty);

                    #endregion
                }

                #endregion


                #region Serialize T[][], empty array is created.

                {
                    #region Set value.

                    C33 value = new C33();
                    value.bField = new B33();
                    value.bField.aArray = new A33[0][];
                    value.bField.aArrayNullable = new int?[0][];

                    Assert.IsNotNull(value.bField.aArray);
                    Assert.IsNotNull(value.bField.aArrayNullable);

                    C33 clonedValue = (C33)serializer.Clone(value);

                    #endregion


                    #region Check result.

                    Assert.IsNotNull(clonedValue.bField.aArray);
                    Assert.IsTrue(clonedValue.bField.aArray.Length == 0);

                    Assert.IsNotNull(clonedValue.bField.aArrayNullable);
                    Assert.IsTrue(clonedValue.bField.aArrayNullable.Length == 0);

                    Assert.IsNull(clonedValue.bProperty);

                    #endregion
                }

                #endregion


                #region Serialize T[][] with values.

                {
                    #region Set value.

                    C33 value = new C33();
                    value.bField = new B33();

                    value.bField.aArray = new A33[cycleLength][];
                    value.bField.aArrayNullable = new int?[cycleLength][];

                    for (int i = 0; i < cycleLength; i++)
                    {
                        A33[] currentArray = new A33[cycleLength];
                        value.bField.aArray[i] = currentArray;

                        int?[] currentArrayNullable = new int?[cycleLength];
                        value.bField.aArrayNullable[i] = currentArrayNullable;

                        for (int j = 0; j < cycleLength; j++)
                        {
                            currentArray[j] = new A33()
                            {
                                iField = j,
                                sProperty = j.ToString()
                            };

                            currentArrayNullable[j] = j;
                        }
                    }

                    C33 clonedValue = (C33)serializer.Clone(value);

                    #endregion


                    #region Check values.

                    for (int i = 0; i < cycleLength; i++)
                    {
                        A33[] currentArray = clonedValue.bField.aArray[i];
                        int?[] currentArrayNullable = clonedValue.bField.aArrayNullable[i];

                        for (int j = 0; j < cycleLength; j++)
                        {
                            A33 aValue = currentArray[j];

                            Assert.IsTrue(aValue.iField == j);
                            Assert.IsTrue(aValue.sProperty == j.ToString());


                            Assert.IsTrue(currentArrayNullable[j] == j);
                        }
                    }

                    #endregion
                }

                #endregion


                #region Serialize T[][] with null and not null values.

                {
                    #region Set value.

                    C33 value = new C33();
                    value.bField = new B33();

                    value.bField.aArray = new A33[cycleLength][];
                    value.bField.aArrayNullable = new int?[cycleLength][];

                    for (int i = 0; i < cycleLength; i++)
                    {
                        // Null value.
                        if (i % 2 == 0)
                        {
                            value.bField.aArray[i] = null;
                            value.bField.aArrayNullable[i] = null;
                        }

                        // Not null value.
                        else
                        {
                            A33[] currentArray = new A33[cycleLength];
                            value.bField.aArray[i] = currentArray;

                            int?[] currentArrayNullable = new int?[cycleLength];
                            value.bField.aArrayNullable[i] = currentArrayNullable;

                            for (int j = 0; j < cycleLength; j++)
                            {
                                currentArray[j] = new A33()
                                {
                                    iField = j,
                                    sProperty = j.ToString()
                                };

                                currentArrayNullable[j] = j;
                            }
                        }
                    }

                    C33 clonedValue = (C33)serializer.Clone(value);

                    #endregion


                    #region Check values.

                    for (int i = 0; i < cycleLength; i++)
                    {
                        // Null value.
                        if (i % 2 == 0)
                        {
                            Assert.IsNull(clonedValue.bField.aArray[i]);
                            Assert.IsNull(clonedValue.bField.aArrayNullable[i]);
                        }

                        // Not null value.
                        else
                        {
                            A33[] currentArray = clonedValue.bField.aArray[i];
                            int?[] currentArrayNullable = clonedValue.bField.aArrayNullable[i];

                            for (int j = 0; j < cycleLength; j++)
                            {
                                A33 aValue = currentArray[j];

                                Assert.IsTrue(aValue.iField == j);
                                Assert.IsTrue(aValue.sProperty == j.ToString());


                                Assert.IsTrue(currentArrayNullable[j] == j);
                            }
                        }
                    }

                    #endregion
                }

                #endregion

                #endregion

                #endregion


                #region Check generic inherited classes.

                #region Simple types.

                #region Null property.

                {
                    // Set value.
                    B22<int, string> value = new B22<int, string>();
                    B22<int, string> clonedValue = serializer.Clone<B22<int, string>>(value);

                    // Check result.
                    Assert.IsNotNull(clonedValue);
                    Assert.IsNull(clonedValue.collection);
                    Assert.IsNull(clonedValue.aCollection);
                    Assert.IsNull(clonedValue.aReverseCollection);
                }

                #endregion


                #region Empty collection property.

                {
                    // Set value.
                    B22<int, string> value = new B22<int, string>();
                    value.collection = new Dictionary<int, string>();

                    B22<int, string> clonedValue = serializer.Clone<B22<int, string>>(value);

                    // Check result.
                    Assert.IsNotNull(clonedValue);
                    Assert.IsTrue(clonedValue.collection.Count == 0);
                    Assert.IsNull(clonedValue.aCollection);
                    Assert.IsNull(clonedValue.aReverseCollection);
                }

                #endregion


                #region Filled collection property.

                {
                    #region Set value.

                    B22<int, string> value = new B22<int, string>();
                    value.collection = new Dictionary<int, string>();

                    for (int i = 0; i < cycleLength; i++)
                    {
                        value.collection.Add(i, i.ToString());
                    }

                    B22<int, string> clonedValue = serializer.Clone<B22<int, string>>(value);

                    #endregion


                    #region Check result.

                    Assert.IsNotNull(clonedValue);
                    Assert.IsNull(clonedValue.aCollection);
                    Assert.IsNull(clonedValue.aReverseCollection);
                    Assert.IsTrue(clonedValue.collection.Count == cycleLength);

                    for (int i = 0; i < cycleLength; i++)
                    {
                        Assert.IsTrue(clonedValue.collection[i] == i.ToString());
                    }

                    #endregion
                }

                #endregion



                #region Restore generic type to itself.

                {
                    #region Set value.

                    serializer.RegisterType(typeof(A22New<int, string>));

                    C22<int, string> value = new C22<int, string>()
                    {
                        aValue = new A22New<int, string>()
                        {
                            sProperty = stringValue,
                            tField = intValue
                        }
                    };

                    C22<int, string> clonedValue = serializer.Clone<C22<int, string>>(value);

                    #endregion


                    #region Check result.

                    Assert.IsNotNull(clonedValue.aValue);
                    Assert.IsTrue(clonedValue.aValue is A22New<int, string>);

                    Assert.IsTrue(clonedValue.aValue.sProperty == stringValue);
                    Assert.IsTrue(clonedValue.aValue.tField == intValue);

                    #endregion
                }

                #endregion


                #region Restore inherited non-generic type to parent generic.

                {
                    #region Set value.

                    serializer.RegisterType(typeof(A22NewInherited));

                    C22<int, string> value = new C22<int, string>()
                    {
                        aValue = new A22NewInherited()
                        {
                            sProperty = stringValue,
                            tField = intValue
                        }
                    };

                    C22<int, string> clonedValue = serializer.Clone<C22<int, string>>(value);

                    #endregion


                    #region Check result.

                    Assert.IsNotNull(clonedValue.aValue);
                    Assert.IsTrue(clonedValue.aValue is A22NewInherited);

                    Assert.IsTrue(clonedValue.aValue.sProperty == stringValue);
                    Assert.IsTrue(clonedValue.aValue.tField == intValue);

                    #endregion
                }

                #endregion


                #region Restore generic type to inherited non-generic in another class.

                {
                    bool result = false;

                    try
                    {
                        C22<int, string> value = new C22<int, string>()
                        {
                            aValue = new A22New<int, string>()
                            {
                                sProperty = stringValue,
                                tField = intValue
                            }
                        };

                        X22 clonedValue = serializer.Clone<X22>(value);
                    }
                    catch (InvalidCastException)
                    {
                        // We cannot restore parent type to inherited one.
                        result = true;
                    }


                    Assert.IsTrue(result);
                }

                #endregion


                #region Restore inherited non-generic type, stored as parent gerenic one, to itself in another class.

                {
                    #region Set value.

                    C22<int, string> value = new C22<int, string>()
                    {
                        aValue = new A22NewInherited()
                        {
                            sProperty = stringValue,
                            tField = intValue
                        }
                    };

                    X22 clonedValue = serializer.Clone<X22>(value);

                    #endregion


                    #region Check result.

                    Assert.IsNotNull(clonedValue.aValue);
                    Assert.IsTrue(clonedValue.aValue is A22NewInherited);

                    Assert.IsTrue(clonedValue.aValue.sProperty == stringValue);
                    Assert.IsTrue(clonedValue.aValue.tField == intValue);

                    #endregion
                }

                #endregion



                #region Restore state.

                serializer = createSerializer(useProcessedTypes);

                #endregion

                #endregion


                #region Collections of generic class items.

                // NB! Null property has been already checked above.

                #region Empty collection property.

                {
                    #region Set value.

                    B22<int, string> value = new B22<int, string>();
                    value.aCollection = new Dictionary<int, A22<int, string>>();
                    value.aReverseCollection = new Dictionary<A22New<int, int>, int>();

                    B22<int, string> clonedValue = serializer.Clone<B22<int, string>>(value);

                    #endregion


                    #region Check result.

                    Assert.IsNotNull(clonedValue);
                    Assert.IsTrue(clonedValue.aCollection.Count == 0);
                    Assert.IsTrue(clonedValue.aReverseCollection.Count == 0);
                    Assert.IsNull(clonedValue.collection);

                    #endregion
                }

                #endregion


                #region Filled collection property.

                {
                    #region Set value.

                    B22<int, string> value = new B22<int, string>();
                    value.aCollection = new Dictionary<int, A22<int, string>>();
                    value.aReverseCollection = new Dictionary<A22New<int, int>, int>();

                    for (int i = 0; i < cycleLength; i++)
                    {
                        // Common collection.
                        value.aCollection.Add(i, new A22<int, string>()
                        {
                            tField = i,
                            sProperty = i.ToString()
                        });

                        // Reverse collection.
                        value.aReverseCollection.Add(
                            new A22New<int, int>()
                            {
                                tField = i,
                                sProperty = i
                            },
                            i);
                    }

                    B22<int, string> clonedValue = serializer.Clone<B22<int, string>>(value);

                    #endregion


                    #region Check result.

                    Assert.IsNotNull(clonedValue);
                    Assert.IsNull(clonedValue.collection);
                    Assert.IsTrue(clonedValue.aCollection.Count == cycleLength);
                    Assert.IsTrue(clonedValue.aReverseCollection.Count == cycleLength);

                    // Common collection.
                    for (int i = 0; i < cycleLength; i++)
                    {
                        A22<int, string> item = clonedValue.aCollection[i];

                        Assert.IsTrue(item.tField == i);
                        Assert.IsTrue(item.sProperty == i.ToString());
                    }


                    // Reverse collection.
                    int j = 0;
                    foreach (KeyValuePair<A22New<int, int>, int> pair in clonedValue.aReverseCollection)
                    {
                        Assert.IsTrue(pair.Key.tField == j);
                        Assert.IsTrue(pair.Key.sProperty == j);

                        Assert.IsTrue(pair.Value == j);

                        j++;
                    }

                    #endregion
                }

                #endregion

                #endregion

                #endregion

                #endregion


                #region Serialize object type properties.

                //if (!useProcessedTypes)
                {
                    #region Plain type properties in classes.

                    #region Restore strong typed plain property to object one.

                    #region Deserialize primitive type fields, null values stay null.

                    {
                        // Set value.
                        APlainProperties value = createPlainPropertiesObject(false);
                        AObjectPlainProperties clonedValue = serializer.Clone<AObjectPlainProperties>(value);

                        // Check result.
                        compareObjectAndTypedPlainValuesObjects(clonedValue, value, false);
                    }

                    #endregion


                    #region Deserialize primitive type fields, null values are set.

                    {
                        // Set value.
                        APlainProperties value = createPlainPropertiesObject(true);
                        AObjectPlainProperties clonedValue = serializer.Clone<AObjectPlainProperties>(value);

                        // Check result.
                        compareObjectAndTypedPlainValuesObjects(clonedValue, value, true);
                    }

                    #endregion


                    #region Deserialize primitive type fields in object as a property, null values are set.

                    {
                        #region Set value.

                        BPlainProperties value = new BPlainProperties();
                        value.aField = createPlainPropertiesObject(true);
                        value.aProperty = createPlainPropertiesObject(false);

                        BObjectPlainProperties clonedValue = serializer.Clone<BObjectPlainProperties>(value);

                        #endregion


                        #region Check result.

                        Assert.IsNotNull(clonedValue);
                        compareObjectAndTypedPlainValuesObjects(clonedValue.aField, value.aField, true);
                        compareObjectAndTypedPlainValuesObjects(clonedValue.aProperty, value.aProperty, false);

                        #endregion
                    }

                    #endregion

                    #endregion


                    #region Restore object type plain property to strong typed one.

                    #region Deserialize primitive type fields, null values stay null.

                    {
                        // Set value.
                        AObjectPlainProperties value = createObjectPlainPropertiesObject(false);
                        APlainProperties clonedValue = serializer.Clone<APlainProperties>(value);

                        // Check result.
                        compareObjectAndTypedPlainValuesObjects(value, clonedValue, false);
                    }

                    #endregion


                    #region Deserialize primitive type fields, null values are set.

                    {
                        // Set value.
                        AObjectPlainProperties value = createObjectPlainPropertiesObject(true);
                        APlainProperties clonedValue = serializer.Clone<APlainProperties>(value);

                        // Check result.
                        compareObjectAndTypedPlainValuesObjects(value, clonedValue, true);
                    }

                    #endregion


                    #region Deserialize primitive type fields in object as a property, null values are set.

                    {
                        #region Set value.

                        BObjectPlainProperties value = new BObjectPlainProperties();
                        value.aField = createObjectPlainPropertiesObject(true);
                        value.aProperty = createObjectPlainPropertiesObject(false);

                        BPlainProperties clonedValue = serializer.Clone<BPlainProperties>(value);

                        #endregion


                        #region Check result.

                        Assert.IsNotNull(clonedValue);
                        compareObjectAndTypedPlainValuesObjects(value.aField, clonedValue.aField, true);
                        compareObjectAndTypedPlainValuesObjects(value.aProperty, clonedValue.aProperty, false);

                        #endregion
                    }

                    #endregion

                    #endregion


                    #region Restore object type plain property to object typed one.

                    #region Deserialize primitive type fields, null values stay null.

                    {
                        // Set value.
                        AObjectPlainProperties value = createObjectPlainPropertiesObject(false);
                        AObjectPlainProperties clonedValue = serializer.Clone<AObjectPlainProperties>(value);

                        // Check result.
                        compareObjectPlainValuesObjects(value, clonedValue, false);
                    }

                    #endregion


                    #region Deserialize primitive type fields, null values are set.

                    {
                        // Set value.
                        AObjectPlainProperties value = createObjectPlainPropertiesObject(true);
                        AObjectPlainProperties clonedValue = serializer.Clone<AObjectPlainProperties>(value);

                        // Check result.
                        compareObjectPlainValuesObjects(value, clonedValue, true);
                    }

                    #endregion


                    #region Deserialize primitive type fields in object as a property, null values are set.

                    {
                        #region Set value.

                        BObjectPlainProperties value = new BObjectPlainProperties();
                        value.aField = createObjectPlainPropertiesObject(true);
                        value.aProperty = createObjectPlainPropertiesObject(false);

                        BObjectPlainProperties clonedValue = serializer.Clone<BObjectPlainProperties>(value);

                        #endregion


                        #region Check result.

                        Assert.IsNotNull(clonedValue);
                        compareObjectPlainValuesObjects(value.aField, clonedValue.aField, true);
                        compareObjectPlainValuesObjects(value.aProperty, clonedValue.aProperty, false);

                        #endregion
                    }

                    #endregion

                    #endregion

                    #endregion


                    #region Plain type properties in structures.

                    #region Restore strong typed plain property to object one.

                    #region Deserialize primitive type fields, null values stay null.

                    {
                        // Set value.
                        AStructPlainProperties value = createPlainPropertiesStruct(false);
                        AObjectPlainProperties clonedValue = serializer.Clone<AObjectPlainProperties>(value);

                        // Check result.
                        compareObjectAndStructPlainValuesObjects(clonedValue, value, false);
                    }

                    #endregion


                    #region Deserialize primitive type fields, null values are set.

                    {
                        // Set value.
                        AStructPlainProperties value = createPlainPropertiesStruct(true);
                        AObjectPlainProperties clonedValue = serializer.Clone<AObjectPlainProperties>(value);

                        // Check result.
                        compareObjectAndStructPlainValuesObjects(clonedValue, value, true);
                    }

                    #endregion


                    #region Deserialize primitive type fields in object as a property, null values are set.

                    {
                        #region Set value.

                        BPlainProperties value = new BPlainProperties();
                        value.aField = createPlainPropertiesObject(true);
                        value.aProperty = createPlainPropertiesObject(false);

                        BObjectPlainProperties clonedValue = serializer.Clone<BObjectPlainProperties>(value);

                        #endregion


                        #region Check result.

                        Assert.IsNotNull(clonedValue);
                        compareObjectAndTypedPlainValuesObjects(clonedValue.aField, value.aField, true);
                        compareObjectAndTypedPlainValuesObjects(clonedValue.aProperty, value.aProperty, false);

                        #endregion
                    }

                    #endregion

                    #endregion


                    #region Restore object type plain property to strong typed one.

                    #region Deserialize primitive type fields, null values stay null.

                    {
                        // Set value.
                        AObjectPlainProperties value = createObjectPlainPropertiesObject(false);
                        AStructPlainProperties clonedValue = serializer.Clone<AStructPlainProperties>(value);

                        // Check result.
                        compareObjectAndStructPlainValuesObjects(value, clonedValue, false);
                    }

                    #endregion


                    #region Deserialize primitive type fields, null values are set.

                    {
                        // Set value.
                        AObjectPlainProperties value = createObjectPlainPropertiesObject(true);
                        AStructPlainProperties clonedValue = serializer.Clone<AStructPlainProperties>(value);

                        // Check result.
                        compareObjectAndStructPlainValuesObjects(value, clonedValue, true);
                    }

                    #endregion


                    #region Deserialize primitive type fields in object as a property, null values are set.

                    {
                        #region Set value.

                        BObjectPlainProperties value = new BObjectPlainProperties();
                        value.aField = createObjectPlainPropertiesObject(true);
                        value.aProperty = createObjectPlainPropertiesObject(false);

                        BPlainProperties clonedValue = serializer.Clone<BPlainProperties>(value);

                        #endregion


                        #region Check result.

                        Assert.IsNotNull(clonedValue);
                        compareObjectAndTypedPlainValuesObjects(value.aField, clonedValue.aField, true);
                        compareObjectAndTypedPlainValuesObjects(value.aProperty, clonedValue.aProperty, false);

                        #endregion
                    }

                    #endregion

                    #endregion

                    #endregion
                }


                #region List<>.

                #region Serialize List<object> of one type values to List<object>.

                #region Serialize List<object>, list is not created.

                {
                    serializer = createSerializer(useProcessedTypes);

                    #region Set value.

                    C25 value = new C25();
                    value.bField = new B25();

                    Assert.IsNull(value.bField.aList);

                    C25 clonedValue = (C25)serializer.Clone(value);

                    #endregion


                    #region Check result.

                    Assert.IsNull(clonedValue.bField.aList);
                    Assert.IsNull(clonedValue.bProperty);

                    #endregion
                }

                #endregion


                #region Serialize List<object>, empty list is created.

                {
                    #region Set value.

                    C25 value = new C25();
                    value.bField = new B25();
                    value.bField.aList = new List<object>();

                    Assert.IsNotNull(value.bField.aList);

                    C25 clonedValue = (C25)serializer.Clone(value);

                    #endregion


                    #region Check result.

                    Assert.IsNotNull(clonedValue.bField.aList);
                    Assert.IsTrue(clonedValue.bField.aList.Count == 0);
                    Assert.IsNull(clonedValue.bProperty);

                    #endregion
                }

                #endregion


                #region Serialize List<object> with values, no types are registered.

                //if (!useProcessedTypes)
                {
                    #region Set value.

                    C25 value = new C25();
                    value.bField = new B25();
                    value.bField.aList = new List<object>();

                    for (int i = 0; i < cycleLength; i++)
                    {
                        value.bField.aList.Add(new A25()
                        {
                            iField = i,
                            sProperty = i.ToString()
                        });
                    }

                    C25 clonedValue = (C25)serializer.Clone(value);

                    #endregion


                    #region Check values.

                    for (int i = 0; i < cycleLength; i++)
                    {
                        Assert.IsNull(clonedValue.bField.aList[i]);
                    }

                    #endregion
                }

                #endregion


                #region Serialize List<object> with plain type values, no types are registered.

                {
                    #region Set value.

                    C25 value = new C25();
                    value.bField = new B25();
                    value.bField.aList = new List<object>()
                    {
                        dateTime,
                        dateTime1,

                        timeSpanValue,
                        timeSpanValue1,

                        guidValue,
                        guidValue1,


                        intValue,
                        intValue1,

                        int16Value,
                        int16Value1,

                        int64Value,
                        int64Value1,


                        uintValue,
                        uintValue1,

                        uint16Value,
                        uint16Value1,

                        uint64Value,
                        uint64Value1,


                        byteValue,
                        byteValue1,

                        floatValue,
                        floatValue1,

                        doubleValue,
                        doubleValue1,

                        decimalValue,
                        decimalValue1,
                    };


                    C25 clonedValue = (C25)serializer.Clone(value);

                    #endregion


                    #region Check values.

                    Assert.IsTrue(clonedValue.bField.aList[0].Equals(dateTime));
                    Assert.IsTrue(clonedValue.bField.aList[1].Equals(dateTime1));

                    Assert.IsTrue(clonedValue.bField.aList[2].Equals(timeSpanValue));
                    Assert.IsTrue(clonedValue.bField.aList[3].Equals(timeSpanValue1));

                    Assert.IsTrue(clonedValue.bField.aList[4].Equals(guidValue));
                    Assert.IsTrue(clonedValue.bField.aList[5].Equals(guidValue1));

                    Assert.IsTrue(clonedValue.bField.aList[6].Equals(intValue));
                    Assert.IsTrue(clonedValue.bField.aList[7].Equals(intValue1));

                    Assert.IsTrue(clonedValue.bField.aList[8].Equals(int16Value));
                    Assert.IsTrue(clonedValue.bField.aList[9].Equals(int16Value1));

                    Assert.IsTrue(clonedValue.bField.aList[10].Equals(int64Value));
                    Assert.IsTrue(clonedValue.bField.aList[11].Equals(int64Value1));

                    Assert.IsTrue(clonedValue.bField.aList[12].Equals(uintValue));
                    Assert.IsTrue(clonedValue.bField.aList[13].Equals(uintValue1));

                    Assert.IsTrue(clonedValue.bField.aList[14].Equals(uint16Value));
                    Assert.IsTrue(clonedValue.bField.aList[15].Equals(uint16Value1));

                    Assert.IsTrue(clonedValue.bField.aList[16].Equals(uint64Value));
                    Assert.IsTrue(clonedValue.bField.aList[17].Equals(uint64Value1));

                    Assert.IsTrue(clonedValue.bField.aList[18].Equals(byteValue));
                    Assert.IsTrue(clonedValue.bField.aList[19].Equals(byteValue1));

                    Assert.IsTrue(clonedValue.bField.aList[20].Equals(floatValue));
                    Assert.IsTrue(clonedValue.bField.aList[21].Equals(floatValue1));

                    Assert.IsTrue(clonedValue.bField.aList[22].Equals(doubleValue));
                    Assert.IsTrue(clonedValue.bField.aList[23].Equals(doubleValue1));

                    Assert.IsTrue(clonedValue.bField.aList[24].Equals(decimalValue));
                    Assert.IsTrue(clonedValue.bField.aList[25].Equals(decimalValue1));

                    #endregion
                }

                #endregion



                #region Serialize List<object> with values, types is registered at serialization and is unknown at deserialization.

                {
                    #region Set value.

                    serializer.RegisterType(typeof(A25));

                    C25 value = new C25();
                    value.bField = new B25();
                    value.bField.aList = new List<object>();

                    for (int i = 0; i < cycleLength; i++)
                    {
                        value.bField.aList.Add(new A25()
                        {
                            iField = i,
                            sProperty = i.ToString()
                        });
                    }

                    C25 clonedValue;
                    using (MemoryStream stream = new MemoryStream())
                    {
                        serializer.Serialize(stream, value);
                        serializer = createSerializer(useProcessedTypes);

                        clonedValue = serializer.Deserialize<C25>(stream);
                    }

                    #endregion


                    #region Check values.

                    for (int i = 0; i < cycleLength; i++)
                    {
                        Assert.IsNull(clonedValue.bField.aList[i]);
                    }

                    #endregion


                    #region Restore state.

                    serializer = createSerializer(useProcessedTypes);

                    #endregion
                }

                #endregion


                #region Serialize List<object> with values, type without serialization attribute is registered.

                {
                    #region Set value.

                    serializer.RegisterType(typeof(A25NoClassAttribute));

                    C25 value = new C25();
                    value.bField = new B25();
                    value.bField.aList = new List<object>();

                    for (int i = 0; i < cycleLength; i++)
                    {
                        value.bField.aList.Add(new A25NoClassAttribute()
                        {
                            iField = i,
                            sProperty = i.ToString()
                        });
                    }

                    C25 clonedValue = (C25)serializer.Clone(value);

                    #endregion


                    #region Check values.

                    for (int i = 0; i < cycleLength; i++)
                    {
                        Assert.IsNull(clonedValue.bField.aList[i]);
                    }

                    #endregion


                    #region Restore state.

                    serializer = createSerializer(useProcessedTypes);

                    #endregion
                }

                #endregion


                #region Serialize List<object> with values, type with GUID is registered, no inheritance.

                {
                    #region Set value.

                    serializer.RegisterType(typeof(A25));

                    C25 value = new C25();
                    value.bField = new B25();
                    value.bField.aList = new List<object>();

                    for (int i = 0; i < cycleLength; i++)
                    {
                        value.bField.aList.Add(new A25()
                        {
                            iField = i,
                            sProperty = i.ToString()
                        });
                    }

                    C25 clonedValue = (C25)serializer.Clone(value);

                    #endregion


                    #region Check values.

                    for (int i = 0; i < cycleLength; i++)
                    {
                        A25 aValue = (A25)clonedValue.bField.aList[i];

                        Assert.IsTrue(aValue.iField == i);
                        Assert.IsTrue(aValue.sProperty == i.ToString());
                    }

                    #endregion


                    #region Restore state.

                    serializer = createSerializer(useProcessedTypes);

                    #endregion
                }

                #endregion


                #region Serialize List<object> with values, type with GUID is registered, no inheritance (v. 2).

                {
                    #region Set value.

                    serializer.RegisterType(typeof(A25WithClassAttribute));

                    C25 value = new C25();
                    value.bField = new B25();
                    value.bField.aList = new List<object>();

                    for (int i = 0; i < cycleLength; i++)
                    {
                        value.bField.aList.Add(new A25WithClassAttribute()
                        {
                            iField = i,
                            sProperty = i.ToString()
                        });
                    }

                    C25 clonedValue = (C25)serializer.Clone(value);

                    #endregion


                    #region Check values.

                    for (int i = 0; i < cycleLength; i++)
                    {
                        A25WithClassAttribute aValue = (A25WithClassAttribute)clonedValue.bField.aList[i];

                        Assert.IsTrue(aValue.iField == i);
                        Assert.IsTrue(aValue.sProperty == i.ToString());
                    }

                    #endregion


                    #region Restore state.

                    serializer = createSerializer(useProcessedTypes);

                    #endregion
                }

                #endregion


                #region Serialize List<object> with values, type with GUID is registered, with inheritance.

                {
                    #region Set value.

                    serializer.RegisterType(typeof(A25InheritedWithClassAttribute));

                    C25 value = new C25();
                    value.bField = new B25();
                    value.bField.aList = new List<object>();

                    for (int i = 0; i < cycleLength; i++)
                    {
                        value.bField.aList.Add(new A25InheritedWithClassAttribute()
                        {
                            iField = i,
                            sProperty = i.ToString()
                        });
                    }

                    C25 clonedValue = (C25)serializer.Clone(value);

                    #endregion


                    #region Check values.

                    for (int i = 0; i < cycleLength; i++)
                    {
                        A25InheritedWithClassAttribute aInheritedValue = (A25InheritedWithClassAttribute)clonedValue.bField.aList[i];
                        A25WithClassAttribute aValue = (A25WithClassAttribute)clonedValue.bField.aList[i];

                        Assert.IsTrue(aInheritedValue.iField == i);
                        Assert.IsTrue(aInheritedValue.sProperty == i.ToString());

                        // Fields are hidden.
                        Assert.IsTrue(aValue.iField == 0);
                        Assert.IsTrue(aValue.sProperty == null);
                    }

                    #endregion


                    #region Restore state.

                    serializer = createSerializer(useProcessedTypes);

                    #endregion
                }

                #endregion


                #region Serialize List<object> with values, type without GUID (inherited from type with GUID) is registered, with inheritance.

                {
                    #region Set value.

                    serializer.RegisterType(typeof(A25InheritedWithoutClassAttribute));

                    C25 value = new C25();
                    value.bField = new B25();
                    value.bField.aList = new List<object>();

                    for (int i = 0; i < cycleLength; i++)
                    {
                        value.bField.aList.Add(new A25InheritedWithoutClassAttribute()
                        {
                            iField = i,
                            sProperty = i.ToString()
                        });
                    }

                    C25 clonedValue = (C25)serializer.Clone(value);

                    #endregion


                    #region Check values.

                    for (int i = 0; i < cycleLength; i++)
                    {
                        Assert.IsNull(clonedValue.bField.aList[i]);
                    }

                    #endregion


                    #region Restore state.

                    serializer = createSerializer(useProcessedTypes);

                    #endregion
                }

                #endregion

                #endregion


                //if (!useProcessedTypes)
                {
                    #region Serialize List<T> to List<object>.

                    #region Serialize List<T>, list is not created.

                    {
                        #region Set value.

                        C26TypedList value = new C26TypedList();
                        value.bField = new B26TypedList();

                        Assert.IsNull(value.bField.aList);

                        C26ObjectList clonedValue = serializer.Clone<C26ObjectList>(value);

                        #endregion


                        #region Check result.

                        Assert.IsNull(clonedValue.bField.aList);
                        Assert.IsNull(clonedValue.bProperty);

                        #endregion
                    }

                    #endregion


                    #region Serialize List<T>, empty list is created.

                    {
                        #region Set value.

                        C26TypedList value = new C26TypedList();
                        value.bField = new B26TypedList();
                        value.bField.aList = new List<A26>();

                        Assert.IsNotNull(value.bField.aList);

                        C26ObjectList clonedValue = serializer.Clone<C26ObjectList>(value);

                        #endregion


                        #region Check result.

                        Assert.IsNotNull(clonedValue.bField.aList);
                        Assert.IsTrue(clonedValue.bField.aList.Count == 0);
                        Assert.IsNull(clonedValue.bProperty);

                        #endregion
                    }

                    #endregion


                    #region Serialize List<T> with values, no types are registered.

                    {
                        #region Set value.

                        C26TypedList value = new C26TypedList();
                        value.bField = new B26TypedList();
                        value.bField.aList = new List<A26>();

                        for (int i = 0; i < cycleLength; i++)
                        {
                            value.bField.aList.Add(new A26()
                            {
                                iField = i,
                                sProperty = i.ToString()
                            });
                        }

                        C26ObjectList clonedValue = serializer.Clone<C26ObjectList>(value);

                        #endregion


                        #region Check values.

                        for (int i = 0; i < cycleLength; i++)
                        {
                            // Values are empty, because type has not been registered for restore and could not be found in any types chain.
                            Assert.IsNull(clonedValue.bField.aList[i]);
                        }

                        #endregion
                    }

                    #endregion



                    #region Serialize List<T> with values, type without serialization attribute is registered.

                    {
                        #region Set value.

                        serializer.RegisterType(typeof(A26NoClassAttribute));

                        C26TypedList value = new C26TypedList();
                        value.bField = new B26TypedList();
                        value.bField.aList = new List<A26>();

                        for (int i = 0; i < cycleLength; i++)
                        {
                            value.bField.aList.Add(new A26NoClassAttribute()
                            {
                                iField = i,
                                sProperty = i.ToString()
                            });
                        }

                        C26ObjectList clonedValue = serializer.Clone<C26ObjectList>(value);

                        #endregion


                        #region Check values.

                        Assert.IsNull(clonedValue.bField.aAttributeList);

                        for (int i = 0; i < cycleLength; i++)
                        {
                            Assert.IsNull(clonedValue.bField.aList[i]);
                        }

                        #endregion


                        #region Restore state.

                        serializer = createSerializer(useProcessedTypes);

                        #endregion
                    }

                    #endregion


                    #region Serialize List<T> with values, type with GUID is registered, no inheritance.

                    {
                        #region Set value.

                        serializer.RegisterType(typeof(A26));

                        C26TypedList value = new C26TypedList();
                        value.bField = new B26TypedList();
                        value.bField.aList = new List<A26>();

                        for (int i = 0; i < cycleLength; i++)
                        {
                            value.bField.aList.Add(new A26()
                            {
                                iField = i,
                                sProperty = i.ToString()
                            });
                        }

                        C26ObjectList clonedValue = serializer.Clone<C26ObjectList>(value);

                        #endregion


                        #region Check values.

                        Assert.IsNull(clonedValue.bField.aAttributeList);

                        for (int i = 0; i < cycleLength; i++)
                        {
                            A26 aValue = (A26)clonedValue.bField.aList[i];

                            Assert.IsTrue(aValue.iField == i);
                            Assert.IsTrue(aValue.sProperty == i.ToString());
                        }

                        #endregion


                        #region Restore state.

                        serializer = createSerializer(useProcessedTypes);

                        #endregion
                    }

                    #endregion


                    #region Serialize List<T> with values, type with GUID is registered, collection is of type without GUID.

                    {
                        #region Set value.

                        serializer.RegisterType(typeof(A26WithClassAttribute));

                        C26TypedList value = new C26TypedList();
                        value.bField = new B26TypedList();
                        value.bField.aList = new List<A26>();

                        for (int i = 0; i < cycleLength; i++)
                        {
                            value.bField.aList.Add(new A26WithClassAttribute()
                            {
                                iField = i,
                                sProperty = i.ToString()
                            });
                        }

                        C26ObjectList clonedValue = serializer.Clone<C26ObjectList>(value);

                        #endregion


                        #region Check values.

                        Assert.IsNull(clonedValue.bField.aAttributeList);

                        for (int i = 0; i < cycleLength; i++)
                        {
                            A26WithClassAttribute aInheritedValue = (A26WithClassAttribute)clonedValue.bField.aList[i];
                            A26 aValue = (A26)clonedValue.bField.aList[i];

                            Assert.IsTrue(aInheritedValue.iField == i);
                            Assert.IsTrue(aInheritedValue.sProperty == i.ToString());

                            // Fields are hidden.
                            Assert.IsTrue(aValue.iField == 0);
                            Assert.IsTrue(aValue.sProperty == null);
                        }

                        #endregion


                        #region Restore state.

                        serializer = createSerializer(useProcessedTypes);

                        #endregion
                    }

                    #endregion


                    #region Serialize List<T> with values, type with GUID is registered, collection is of type with GUID.

                    {
                        #region Set value.

                        serializer.RegisterType(typeof(A26WithClassAttribute));

                        C26TypedList value = new C26TypedList();
                        value.bField = new B26TypedList();
                        value.bField.aAttributeList = new List<A26WithClassAttribute>();

                        for (int i = 0; i < cycleLength; i++)
                        {
                            value.bField.aAttributeList.Add(new A26WithClassAttribute()
                            {
                                iField = i,
                                sProperty = i.ToString()
                            });
                        }

                        C26ObjectList clonedValue = serializer.Clone<C26ObjectList>(value);

                        #endregion


                        #region Check values.

                        Assert.IsNull(clonedValue.bField.aList);

                        for (int i = 0; i < cycleLength; i++)
                        {
                            A26WithClassAttribute aInheritedValue = (A26WithClassAttribute)clonedValue.bField.aAttributeList[i];
                            A26 aValue = (A26)clonedValue.bField.aAttributeList[i];

                            Assert.IsTrue(aInheritedValue.iField == i);
                            Assert.IsTrue(aInheritedValue.sProperty == i.ToString());

                            // Fields are hidden.
                            Assert.IsTrue(aValue.iField == 0);
                            Assert.IsTrue(aValue.sProperty == null);
                        }

                        #endregion


                        #region Restore state.

                        serializer = createSerializer(useProcessedTypes);

                        #endregion
                    }

                    #endregion


                    #region Serialize List<T> with values, inherited type with GUID is registered, collection is of type with GUID.

                    {
                        #region Set value.

                        serializer.RegisterType(typeof(A26WithClassAttribute)); // register type which is higher in inheritance chain
                        serializer.RegisterType(typeof(A26InheritedWithClassAttribute));
                        serializer.RegisterType(typeof(A26InheritedWithClassAttribute)); // register twice - by an oversight

                        C26TypedList value = new C26TypedList();
                        value.bField = new B26TypedList();
                        value.bField.aAttributeList = new List<A26WithClassAttribute>();

                        for (int i = 0; i < cycleLength; i++)
                        {
                            value.bField.aAttributeList.Add(new A26InheritedWithClassAttribute()
                            {
                                iField = i,
                                sProperty = i.ToString()
                            });
                        }

                        C26ObjectList clonedValue = serializer.Clone<C26ObjectList>(value);

                        #endregion


                        #region Check values.

                        Assert.IsNull(clonedValue.bField.aList);

                        for (int i = 0; i < cycleLength; i++)
                        {
                            // NB.! Field values are not restored properly, because collection values set their type to A26InheritedWithClassAttribute, and it hides base class field. Values are restored to the declared type of the collection, not to the real type.

                            A26InheritedWithClassAttribute aInheritedValue = (A26InheritedWithClassAttribute)clonedValue.bField.aAttributeList[i];
                            A26WithClassAttribute aClassValue = (A26WithClassAttribute)clonedValue.bField.aAttributeList[i];
                            A26 aValue = (A26)clonedValue.bField.aAttributeList[i];

                            Assert.IsTrue(aInheritedValue.iField == i);
                            Assert.IsTrue(aInheritedValue.sProperty == i.ToString());


                            // These fields are hidden.
                            Assert.IsTrue(aClassValue.iField == i);
                            Assert.IsTrue(aClassValue.sProperty == i.ToString());

                            Assert.IsTrue(aValue.iField == 0);
                            Assert.IsTrue(aValue.sProperty == null);
                        }

                        #endregion


                        #region Restore state.

                        serializer = createSerializer(useProcessedTypes);

                        #endregion
                    }

                    #endregion

                    #endregion


                    #region Serialize List<object> of one type values to List<T>.

                    #region Serialize List<object>, list is not created.

                    {
                        #region Set value.

                        C26ObjectList value = new C26ObjectList();
                        value.bField = new B26ObjectList();

                        Assert.IsNull(value.bField.aList);

                        C26TypedList clonedValue = serializer.Clone<C26TypedList>(value);

                        #endregion


                        #region Check result.

                        Assert.IsNull(clonedValue.bField.aList);
                        Assert.IsNull(clonedValue.bProperty);

                        #endregion
                    }

                    #endregion


                    #region Serialize List<object>, empty list is created.

                    {
                        #region Set value.

                        C26ObjectList value = new C26ObjectList();
                        value.bField = new B26ObjectList();
                        value.bField.aList = new List<object>();

                        Assert.IsNotNull(value.bField.aList);

                        C26TypedList clonedValue = serializer.Clone<C26TypedList>(value);

                        #endregion


                        #region Check result.

                        Assert.IsNotNull(clonedValue.bField.aList);
                        Assert.IsTrue(clonedValue.bField.aList.Count == 0);
                        Assert.IsNull(clonedValue.bProperty);

                        #endregion
                    }

                    #endregion


                    #region Serialize List<object> with values, no types are registered.

                    {
                        #region Set value.

                        C26ObjectList value = new C26ObjectList();
                        value.bField = new B26ObjectList();
                        value.bField.aList = new List<object>();

                        for (int i = 0; i < cycleLength; i++)
                        {
                            value.bField.aList.Add(new A26()
                            {
                                iField = i,
                                sProperty = i.ToString()
                            });
                        }

                        C26TypedList clonedValue = serializer.Clone<C26TypedList>(value);

                        #endregion


                        #region Check values.

                        Assert.IsNull(clonedValue.bField.aAttributeList);

                        for (int i = 0; i < cycleLength; i++)
                        {
                            // NB.! Values are set, because A26 type is known at deserialization time.
                            A26 aValue = clonedValue.bField.aList[i];

                            Assert.IsTrue(aValue.iField == i);
                            Assert.IsTrue(aValue.sProperty == i.ToString());
                        }

                        #endregion
                    }

                    #endregion


                    #region Serialize List<object> with values, types are registered at serialization and not registered at deserialization.

                    {
                        #region Set value.

                        serializer.RegisterType(typeof(A26InheritedWithClassAttribute));

                        C26ObjectList value = new C26ObjectList();
                        value.bField = new B26ObjectList();
                        value.bField.aList = new List<object>();

                        for (int i = 0; i < cycleLength; i++)
                        {
                            value.bField.aList.Add(new A26InheritedWithClassAttribute()
                            {
                                iField = i,
                                sProperty = i.ToString()
                            });
                        }

                        C26TypedList clonedValue;
                        using (MemoryStream stream = new MemoryStream())
                        {
                            serializer.Serialize(stream, value);
                            serializer = createSerializer(useProcessedTypes);

                            clonedValue = serializer.Deserialize<C26TypedList>(stream);
                        }

                        #endregion


                        #region Check values.

                        Assert.IsNull(clonedValue.bField.aAttributeList);

                        for (int i = 0; i < cycleLength; i++)
                        {
                            // NB.! Values are not set, because A26InheritedWithClassAttribute type is not known at deserialization time.

                            Assert.IsNull(clonedValue.bField.aList[i]);
                        }

                        #endregion


                        #region Restore state.

                        serializer = createSerializer(useProcessedTypes);

                        #endregion
                    }

                    #endregion


                    #region Serialize List<object> with values, type is registered at serialization and is known at deserialization.

                    {
                        #region Set value.

                        serializer.RegisterType(typeof(A26));

                        C26ObjectList value = new C26ObjectList();
                        value.bField = new B26ObjectList();
                        value.bField.aList = new List<object>();

                        for (int i = 0; i < cycleLength; i++)
                        {
                            value.bField.aList.Add(new A26()
                            {
                                iField = i,
                                sProperty = i.ToString()
                            });
                        }

                        C26TypedList clonedValue;
                        using (MemoryStream stream = new MemoryStream())
                        {
                            serializer.Serialize(stream, value);
                            serializer = createSerializer(useProcessedTypes);

                            clonedValue = serializer.Deserialize<C26TypedList>(stream);
                        }

                        #endregion


                        #region Check values.

                        Assert.IsNull(clonedValue.bField.aAttributeList);

                        for (int i = 0; i < cycleLength; i++)
                        {
                            // NB.! Values are not null, because A26 type is known at deserialization time through property type of B26TypedList.
                            A26 aValue = (A26)clonedValue.bField.aList[i];

                            Assert.IsTrue(aValue.iField == i);
                            Assert.IsTrue(aValue.sProperty == i.ToString());
                        }

                        #endregion


                        #region Restore state.

                        serializer = createSerializer(useProcessedTypes);

                        #endregion
                    }

                    #endregion



                    #region Serialize List<object> with values, type without serialization attribute is registered.

                    {
                        #region Set value.

                        serializer.RegisterType(typeof(A26NoClassAttribute));

                        C26ObjectList value = new C26ObjectList();
                        value.bField = new B26ObjectList();
                        value.bField.aList = new List<object>();

                        for (int i = 0; i < cycleLength; i++)
                        {
                            value.bField.aList.Add(new A26NoClassAttribute()
                            {
                                iField = i,
                                sProperty = i.ToString()
                            });
                        }

                        C26TypedList clonedValue = serializer.Clone<C26TypedList>(value);

                        #endregion


                        #region Check values.

                        Assert.IsNull(clonedValue.bField.aAttributeList);

                        for (int i = 0; i < cycleLength; i++)
                        {
                            Assert.IsNull(clonedValue.bField.aList[i]);
                        }

                        #endregion


                        #region Restore state.

                        serializer = createSerializer(useProcessedTypes);

                        #endregion
                    }

                    #endregion


                    #region Serialize List<object> with values, type with GUID is registered, no inheritance.

                    {
                        #region Set value.

                        serializer.RegisterType(typeof(A26));

                        C26ObjectList value = new C26ObjectList();
                        value.bField = new B26ObjectList();
                        value.bField.aList = new List<object>();

                        for (int i = 0; i < cycleLength; i++)
                        {
                            value.bField.aList.Add(new A26()
                            {
                                iField = i,
                                sProperty = i.ToString()
                            });
                        }

                        C26TypedList clonedValue = serializer.Clone<C26TypedList>(value);

                        #endregion


                        #region Check values.

                        Assert.IsNull(clonedValue.bField.aAttributeList);

                        for (int i = 0; i < cycleLength; i++)
                        {
                            A26 aValue = (A26)clonedValue.bField.aList[i];

                            Assert.IsTrue(aValue.iField == i);
                            Assert.IsTrue(aValue.sProperty == i.ToString());
                        }

                        #endregion


                        #region Restore state.

                        serializer = createSerializer(useProcessedTypes);

                        #endregion
                    }

                    #endregion


                    #region Serialize List<object> with values, type with GUID is registered, collection is of type without GUID.

                    {
                        #region Set value.

                        serializer.RegisterType(typeof(A26WithClassAttribute));

                        C26ObjectList value = new C26ObjectList();
                        value.bField = new B26ObjectList();
                        value.bField.aList = new List<object>();

                        for (int i = 0; i < cycleLength; i++)
                        {
                            value.bField.aList.Add(new A26WithClassAttribute()
                            {
                                iField = i,
                                sProperty = i.ToString()
                            });
                        }

                        C26TypedList clonedValue = serializer.Clone<C26TypedList>(value);

                        #endregion


                        #region Check values.

                        Assert.IsNull(clonedValue.bField.aAttributeList);

                        for (int i = 0; i < cycleLength; i++)
                        {
                            A26WithClassAttribute aInheritedValue = (A26WithClassAttribute)clonedValue.bField.aList[i];
                            A26 aValue = (A26)clonedValue.bField.aList[i];

                            Assert.IsTrue(aInheritedValue.iField == i);
                            Assert.IsTrue(aInheritedValue.sProperty == i.ToString());

                            // Fields are hidden.
                            Assert.IsTrue(aValue.iField == 0);
                            Assert.IsTrue(aValue.sProperty == null);
                        }

                        #endregion


                        #region Restore state.

                        serializer = createSerializer(useProcessedTypes);

                        #endregion
                    }

                    #endregion


                    #region Serialize List<object> with values, type with GUID is registered, collection is of type with GUID.

                    {
                        #region Set value.

                        serializer.RegisterType(typeof(A26WithClassAttribute));

                        C26ObjectList value = new C26ObjectList();
                        value.bField = new B26ObjectList();
                        value.bField.aList = new List<object>();

                        for (int i = 0; i < cycleLength; i++)
                        {
                            value.bField.aList.Add(new A26WithClassAttribute()
                            {
                                iField = i,
                                sProperty = i.ToString()
                            });
                        }

                        C26TypedList clonedValue = serializer.Clone<C26TypedList>(value);

                        #endregion


                        #region Check values.

                        Assert.IsNull(clonedValue.bField.aAttributeList);

                        for (int i = 0; i < cycleLength; i++)
                        {
                            A26WithClassAttribute aInheritedValue = (A26WithClassAttribute)clonedValue.bField.aList[i];
                            A26 aValue = (A26)clonedValue.bField.aList[i];

                            Assert.IsTrue(aInheritedValue.iField == i);
                            Assert.IsTrue(aInheritedValue.sProperty == i.ToString());

                            // Fields are hidden.
                            Assert.IsTrue(aValue.iField == 0);
                            Assert.IsTrue(aValue.sProperty == null);
                        }

                        #endregion


                        #region Restore state.

                        serializer = createSerializer(useProcessedTypes);

                        #endregion
                    }

                    #endregion


                    #region Serialize List<object> with values, inherited type with GUID is registered, collection is of type with GUID.

                    {
                        #region Set value.

                        serializer.RegisterType(typeof(A26InheritedWithClassAttribute));

                        C26ObjectList value = new C26ObjectList();
                        value.bField = new B26ObjectList();
                        value.bField.aList = new List<object>();

                        for (int i = 0; i < cycleLength; i++)
                        {
                            value.bField.aList.Add(new A26InheritedWithClassAttribute()
                            {
                                iField = i,
                                sProperty = i.ToString()
                            });
                        }

                        C26TypedList clonedValue = serializer.Clone<C26TypedList>(value);

                        #endregion


                        #region Check values.

                        Assert.IsNull(clonedValue.bField.aAttributeList);

                        for (int i = 0; i < cycleLength; i++)
                        {
                            // NB.! Field values are not restored properly, because collection values set their type to A26InheritedWithClassAttribute, and it hides base class field. Values are restored to the declared type of the collection, not to the real type.

                            A26InheritedWithClassAttribute aInheritedValue = (A26InheritedWithClassAttribute)clonedValue.bField.aList[i];
                            A26WithClassAttribute aClassValue = (A26WithClassAttribute)clonedValue.bField.aList[i];
                            A26 aValue = clonedValue.bField.aList[i];

                            Assert.IsTrue(aInheritedValue.iField == i);
                            Assert.IsTrue(aInheritedValue.sProperty == i.ToString());


                            // These fields are hidden.
                            Assert.IsTrue(aClassValue.iField == i);
                            Assert.IsTrue(aClassValue.sProperty == i.ToString());

                            Assert.IsTrue(aValue.iField == 0);
                            Assert.IsTrue(aValue.sProperty == null);
                        }

                        #endregion


                        #region Restore state.

                        serializer = createSerializer(useProcessedTypes);

                        #endregion
                    }

                    #endregion

                    #endregion
                }

                #endregion


                #region Dictionary<>, object type property and field.

                // NB.! Check includes usage of property of object and strong type.

                #region Serialize Dictionary<object, object> of one type values to Dictionary<object, object>.

                #region Serialize Dictionary<object, object>, collection is not created.

                {
                    #region Set value.

                    C25 value = new C25();
                    value.bField = new B25();

                    Assert.IsNull(value.bField.aDictionary);

                    C25 clonedValue = (C25)serializer.Clone(value);

                    #endregion


                    #region Check result.

                    Assert.IsNull(clonedValue.bField.aDictionary);
                    Assert.IsNull(clonedValue.bField.aObject);
                    Assert.IsNull(clonedValue.bProperty);

                    #endregion
                }

                #endregion


                #region Serialize Dictionary<object, object>, empty collection is created. Object property set to object.

                {
                    #region Set value.

                    C25 value = new C25();
                    value.bField = new B25();
                    value.bField.aDictionary = new Dictionary<object, object>();

                    Assert.IsNotNull(value.bField.aDictionary);

                    value.bField.aObject = new object();

                    C25 clonedValue = (C25)serializer.Clone(value);

                    #endregion


                    #region Check result.

                    Assert.IsNotNull(clonedValue.bField.aDictionary);
                    Assert.IsTrue(clonedValue.bField.aDictionary.Count == 0);

                    Assert.IsTrue(clonedValue.bField.aObject.GetType() == ObjectTypes.Object);

                    Assert.IsNull(clonedValue.bProperty);

                    #endregion
                }

                #endregion



                #region Serialize Dictionary<object, object> with values, no types are registered.

                //if (!useProcessedTypes)
                {
                    #region Set value.

                    C25 value = new C25();
                    value.bField = new B25();
                    value.bField.aDictionary = new Dictionary<object, object>();

                    for (int i = 0; i < cycleLength; i++)
                    {
                        value.bField.aDictionary.Add(
                            new A25()
                            {
                                iField = i * 2,
                                sProperty = (i * 2).ToString()
                            },
                            new A25()
                            {
                                iField = i,
                                sProperty = i.ToString()
                            });
                    }

                    C25 clonedValue = (C25)serializer.Clone(value);

                    #endregion


                    #region Check values.

                    // Null keys are not restored.
                    Assert.IsTrue(clonedValue.bField.aDictionary.Count == 0);

                    #endregion
                }

                #endregion


                #region Serialize Dictionary<object, object> with plain type values, no types are registered.

                {
                    #region Set value.

                    C25 value = new C25();
                    value.bField = new B25();
                    value.bField.aDictionary = new Dictionary<object, object>();

                    value.bField.aDictionary.Add(dateTime, dateTime1);
                    value.bField.aDictionary.Add(timeSpanValue, timeSpanValue1);
                    value.bField.aDictionary.Add(guidValue, guidValue1);
                    value.bField.aDictionary.Add(intValue, intValue1);
                    value.bField.aDictionary.Add(int16Value, int16Value1);
                    value.bField.aDictionary.Add(int64Value, int64Value1);
                    value.bField.aDictionary.Add(uintValue, uintValue1);
                    value.bField.aDictionary.Add(uint16Value, uint16Value1);
                    value.bField.aDictionary.Add(uint64Value, uint64Value1);
                    value.bField.aDictionary.Add(byteValue, byteValue1);
                    value.bField.aDictionary.Add(floatValue, floatValue1);
                    value.bField.aDictionary.Add(doubleValue, doubleValue1);
                    value.bField.aDictionary.Add(decimalValue, decimalValue1);


                    C25 clonedValue = (C25)serializer.Clone(value);

                    #endregion


                    #region Check values.

                    Assert.IsTrue(clonedValue.bField.aDictionary[dateTime].Equals(dateTime1));
                    Assert.IsTrue(clonedValue.bField.aDictionary[timeSpanValue].Equals(timeSpanValue1));
                    Assert.IsTrue(clonedValue.bField.aDictionary[guidValue].Equals(guidValue1));
                    Assert.IsTrue(clonedValue.bField.aDictionary[intValue].Equals(intValue1));
                    Assert.IsTrue(clonedValue.bField.aDictionary[int16Value].Equals(int16Value1));
                    Assert.IsTrue(clonedValue.bField.aDictionary[int64Value].Equals(int64Value1));
                    Assert.IsTrue(clonedValue.bField.aDictionary[uintValue].Equals(uintValue1));
                    Assert.IsTrue(clonedValue.bField.aDictionary[uint16Value].Equals(uint16Value1));
                    Assert.IsTrue(clonedValue.bField.aDictionary[uint64Value].Equals(uint64Value1));
                    Assert.IsTrue(clonedValue.bField.aDictionary[byteValue].Equals(byteValue1));
                    Assert.IsTrue(clonedValue.bField.aDictionary[floatValue].Equals(floatValue1));
                    Assert.IsTrue(clonedValue.bField.aDictionary[doubleValue].Equals(doubleValue1));
                    Assert.IsTrue(clonedValue.bField.aDictionary[decimalValue].Equals(decimalValue1));

                    #endregion
                }

                #endregion



                #region Object property set to type without serialization attribute.

                {
                    #region Set value.

                    C25 value = new C25();
                    value.bField = new B25();

                    value.bField.aObject = new A25NoClassAttribute();

                    C25 clonedValue = (C25)serializer.Clone(value);

                    #endregion


                    #region Check result.

                    Assert.IsNull(clonedValue.bField.aObject);

                    Assert.IsNull(clonedValue.bField.aDictionary);
                    Assert.IsNull(clonedValue.bProperty);

                    #endregion
                }

                #endregion


                #region Object property set to type with serialization attribute, type is not registered.

                //if (!useProcessedTypes)
                {
                    #region Set value.

                    C25 value = new C25();
                    value.bField = new B25();

                    value.bField.aObject = new A25WithClassAttribute();

                    C25 clonedValue = (C25)serializer.Clone(value);

                    #endregion


                    #region Check result.

                    Assert.IsNull(clonedValue.bField.aObject);

                    Assert.IsNull(clonedValue.bField.aDictionary);
                    Assert.IsNull(clonedValue.bProperty);

                    #endregion
                }

                #endregion


                #region Object property set to type with serialization attribute.

                {
                    #region Set value.

                    serializer.RegisterType(typeof(A25WithClassAttribute));

                    C25 value = new C25();
                    value.bField = new B25();

                    value.bField.aObject = new A25WithClassAttribute();

                    C25 clonedValue = (C25)serializer.Clone(value);

                    #endregion


                    #region Check result.

                    Assert.IsTrue(clonedValue.bField.aObject.GetType() == typeof(A25WithClassAttribute));

                    Assert.IsNull(clonedValue.bField.aDictionary);
                    Assert.IsNull(clonedValue.bProperty);

                    #endregion


                    #region Restore state.

                    serializer = createSerializer(useProcessedTypes);

                    #endregion
                }

                #endregion



                #region Serialize Dictionary<object, object> with values, set property; type is registered at serialization and is unknown at deserialization.

                {
                    #region Set value.

                    serializer.RegisterType(typeof(A25));

                    C25 value = new C25();
                    value.bField = new B25();
                    value.bField.aDictionary = new Dictionary<object, object>();

                    for (int i = 0; i < cycleLength; i++)
                    {
                        value.bField.aDictionary.Add(
                            new A25()
                            {
                                iField = i * 2,
                                sProperty = (i * 2).ToString()
                            },
                            new A25()
                            {
                                iField = i,
                                sProperty = i.ToString()
                            });
                    }

                    // Set property object.
                    value.bField.aObject = new A25();


                    C25 clonedValue;
                    using (MemoryStream stream = new MemoryStream())
                    {
                        serializer.Serialize(stream, value);
                        serializer = createSerializer(useProcessedTypes);

                        clonedValue = serializer.Deserialize<C25>(stream);
                    }

                    #endregion


                    #region Check values.

                    Assert.IsTrue(clonedValue.bField.aDictionary.Count == 0);
                    Assert.IsNull(clonedValue.bField.aObject);

                    #endregion


                    #region Restore state.

                    serializer = createSerializer(useProcessedTypes);

                    #endregion
                }

                #endregion


                #region Serialize Dictionary<object, object> with values, set property; type without serialization attribute is registered.

                {
                    #region Set value.

                    serializer.RegisterType(typeof(A25NoClassAttribute));

                    C25 value = new C25();
                    value.bField = new B25();
                    value.bField.aDictionary = new Dictionary<object, object>();

                    for (int i = 0; i < cycleLength; i++)
                    {
                        value.bField.aDictionary.Add(
                            new A25NoClassAttribute()
                            {
                                iField = i * 2,
                                sProperty = (i * 2).ToString()
                            },
                            new A25NoClassAttribute()
                            {
                                iField = i,
                                sProperty = i.ToString()
                            });
                    }

                    // Set property object.
                    value.bField.aObject = new A25NoClassAttribute();

                    C25 clonedValue = (C25)serializer.Clone(value);

                    #endregion


                    #region Check values.

                    Assert.IsTrue(clonedValue.bField.aDictionary.Count == 0);
                    Assert.IsNull(clonedValue.bField.aObject);

                    #endregion


                    #region Restore state.

                    serializer = createSerializer(useProcessedTypes);

                    #endregion
                }

                #endregion


                #region Serialize Dictionary<object, object> with values, set property; type with GUID is registered, no inheritance.

                {
                    #region Set value.

                    serializer.RegisterType(typeof(A25));

                    C25 value = new C25();
                    value.bField = new B25();
                    value.bField.aDictionary = new Dictionary<object, object>();

                    for (int i = 0; i < cycleLength; i++)
                    {
                        value.bField.aDictionary.Add(
                            new A25()
                            {
                                iField = i * 2,
                                sProperty = (i * 2).ToString()
                            },
                            new A25()
                            {
                                iField = i,
                                sProperty = i.ToString()
                            });
                    }

                    // Set property object.
                    value.bField.aObject = new A25()
                    {
                        iField = intValue,
                        sProperty = stringValue
                    };

                    C25 clonedValue = (C25)serializer.Clone(value);

                    #endregion


                    #region Check values.

                    {
                        int i = 0;
                        foreach (KeyValuePair<object, object> pair in clonedValue.bField.aDictionary)
                        {
                            A25 aKeyValue = (A25)pair.Key;
                            A25 aValue = (A25)pair.Value;

                            Assert.IsTrue(aKeyValue.iField == i * 2);
                            Assert.IsTrue(aKeyValue.sProperty == (i * 2).ToString());

                            Assert.IsTrue(aValue.iField == i);
                            Assert.IsTrue(aValue.sProperty == i.ToString());

                            i++;
                        }

                        // Check property object.
                        Assert.IsTrue(clonedValue.bField.aObject.GetType() == typeof(A25));
                        A25 aClonedObject = (A25)clonedValue.bField.aObject;

                        Assert.IsTrue(aClonedObject.iField == intValue);
                        Assert.IsTrue(aClonedObject.sProperty == stringValue);
                    }

                    #endregion


                    #region Restore state.

                    serializer = createSerializer(useProcessedTypes);

                    #endregion
                }

                #endregion


                #region Serialize Dictionary<object, object> with values, set property; type with GUID is registered, no inheritance (v. 2).

                {
                    #region Set value.

                    serializer.RegisterType(typeof(A25WithClassAttribute));

                    C25 value = new C25();
                    value.bField = new B25();
                    value.bField.aDictionary = new Dictionary<object, object>();

                    for (int i = 0; i < cycleLength; i++)
                    {
                        value.bField.aDictionary.Add(
                            new A25WithClassAttribute()
                            {
                                iField = i * 2,
                                sProperty = (i * 2).ToString()
                            },
                            new A25WithClassAttribute()
                            {
                                iField = i,
                                sProperty = i.ToString()
                            });
                    }

                    // Set property object.
                    value.bField.aObject = new A25WithClassAttribute()
                    {
                        iField = intValue,
                        sProperty = stringValue
                    };

                    C25 clonedValue = (C25)serializer.Clone(value);

                    #endregion


                    #region Check values.

                    {
                        int i = 0;
                        foreach (KeyValuePair<object, object> pair in clonedValue.bField.aDictionary)
                        {
                            A25WithClassAttribute aKeyValue = (A25WithClassAttribute)pair.Key;
                            A25WithClassAttribute aValue = (A25WithClassAttribute)pair.Value;

                            Assert.IsTrue(aKeyValue.iField == i * 2);
                            Assert.IsTrue(aKeyValue.sProperty == (i * 2).ToString());

                            Assert.IsTrue(aValue.iField == i);
                            Assert.IsTrue(aValue.sProperty == i.ToString());

                            i++;
                        }

                        // Check property object.
                        Assert.IsTrue(clonedValue.bField.aObject.GetType() == typeof(A25WithClassAttribute));
                        A25WithClassAttribute aClonedObject = (A25WithClassAttribute)clonedValue.bField.aObject;

                        Assert.IsTrue(aClonedObject.iField == intValue);
                        Assert.IsTrue(aClonedObject.sProperty == stringValue);
                    }

                    #endregion


                    #region Restore state.

                    serializer = createSerializer(useProcessedTypes);

                    #endregion
                }

                #endregion


                #region Serialize Dictionary<object, object> with values, set property; type with GUID is registered, with inheritance.

                {
                    #region Set value.

                    serializer.RegisterType(typeof(A25InheritedWithClassAttribute));

                    C25 value = new C25();
                    value.bField = new B25();
                    value.bField.aDictionary = new Dictionary<object, object>();

                    for (int i = 0; i < cycleLength; i++)
                    {
                        value.bField.aDictionary.Add(
                            new A25InheritedWithClassAttribute()
                            {
                                iField = i * 2,
                                sProperty = (i * 2).ToString()
                            },
                            new A25InheritedWithClassAttribute()
                            {
                                iField = i,
                                sProperty = i.ToString()
                            });
                    }

                    // Set property object.
                    value.bField.aObject = new A25InheritedWithClassAttribute()
                    {
                        iField = intValue,
                        sProperty = stringValue
                    };

                    C25 clonedValue = (C25)serializer.Clone(value);

                    #endregion


                    #region Check values.

                    {
                        int i = 0;
                        foreach (KeyValuePair<object, object> pair in clonedValue.bField.aDictionary)
                        {
                            A25InheritedWithClassAttribute aKeyInheritedValue = (A25InheritedWithClassAttribute)pair.Key;
                            A25WithClassAttribute aKeyValue = (A25WithClassAttribute)pair.Key;

                            A25InheritedWithClassAttribute aInheritedValue = (A25InheritedWithClassAttribute)pair.Value;
                            A25WithClassAttribute aValue = (A25WithClassAttribute)pair.Value;


                            Assert.IsTrue(aKeyInheritedValue.iField == i * 2);
                            Assert.IsTrue(aKeyInheritedValue.sProperty == (i * 2).ToString());

                            // Fields are hidden.
                            Assert.IsTrue(aKeyValue.iField == 0);
                            Assert.IsTrue(aKeyValue.sProperty == null);


                            Assert.IsTrue(aInheritedValue.iField == i);
                            Assert.IsTrue(aInheritedValue.sProperty == i.ToString());

                            // Fields are hidden.
                            Assert.IsTrue(aValue.iField == 0);
                            Assert.IsTrue(aValue.sProperty == null);

                            i++;
                        }


                        // Check property object.
                        Assert.IsTrue(clonedValue.bField.aObject.GetType() == typeof(A25InheritedWithClassAttribute));

                        A25InheritedWithClassAttribute aClonedInheritedObject = (A25InheritedWithClassAttribute)clonedValue.bField.aObject;
                        A25WithClassAttribute aClonedObject = (A25WithClassAttribute)clonedValue.bField.aObject;

                        Assert.IsTrue(aClonedInheritedObject.iField == intValue);
                        Assert.IsTrue(aClonedInheritedObject.sProperty == stringValue);

                        // Fields are hidden.
                        Assert.IsTrue(aClonedObject.iField == 0);
                        Assert.IsTrue(aClonedObject.sProperty == null);
                    }

                    #endregion


                    #region Restore state.

                    serializer = createSerializer(useProcessedTypes);

                    #endregion
                }

                #endregion


                #region Serialize Dictionary<object, object> with values, set property.

                // Type without GUID (inherited from type with GUID) is registered, with inheritance.

                {
                    #region Set value.

                    serializer.RegisterType(typeof(A25InheritedWithoutClassAttribute));

                    C25 value = new C25();
                    value.bField = new B25();
                    value.bField.aDictionary = new Dictionary<object, object>();

                    for (int i = 0; i < cycleLength; i++)
                    {
                        value.bField.aDictionary.Add(
                            new A25InheritedWithoutClassAttribute()
                            {
                                iField = i * 2,
                                sProperty = (i * 2).ToString()
                            },
                            new A25InheritedWithoutClassAttribute()
                            {
                                iField = i,
                                sProperty = i.ToString()
                            });
                    }

                    // Set property object.
                    value.bField.aObject = new A25InheritedWithoutClassAttribute()
                    {
                        iField = intValue,
                        sProperty = stringValue
                    };

                    C25 clonedValue = (C25)serializer.Clone(value);

                    #endregion


                    #region Check values.

                    Assert.IsTrue(clonedValue.bField.aDictionary.Count == 0);
                    Assert.IsNull(clonedValue.bField.aObject);

                    #endregion


                    #region Restore state.

                    serializer = createSerializer(useProcessedTypes);

                    #endregion
                }

                #endregion

                #endregion


                //if (!useProcessedTypes)
                {
                    #region Serialize Dictionary<T, T> to Dictionary<object, object>.

                    #region Serialize Dictionary<T, T>, collection is not created.

                    {
                        #region Set value.

                        C26TypedList value = new C26TypedList();
                        value.bField = new B26TypedList();

                        Assert.IsNull(value.bField.aDictionary);

                        C26ObjectList clonedValue = serializer.Clone<C26ObjectList>(value);

                        #endregion


                        #region Check result.

                        Assert.IsNull(clonedValue.bField.aDictionary);
                        Assert.IsNull(clonedValue.bField.aObject);
                        Assert.IsNull(clonedValue.bProperty);

                        #endregion
                    }

                    #endregion


                    #region Serialize Dictionary<T, T>, empty collection is created.

                    {
                        #region Set value.

                        C26TypedList value = new C26TypedList();
                        value.bField = new B26TypedList();
                        value.bField.aDictionary = new Dictionary<A26, A26>();

                        Assert.IsNotNull(value.bField.aDictionary);

                        C26ObjectList clonedValue = serializer.Clone<C26ObjectList>(value);

                        #endregion


                        #region Check result.

                        Assert.IsNotNull(clonedValue.bField.aDictionary);
                        Assert.IsTrue(clonedValue.bField.aDictionary.Count == 0);
                        Assert.IsNull(clonedValue.bProperty);

                        #endregion
                    }

                    #endregion


                    #region Serialize Dictionary<T, T> with values, no types are registered.

                    {
                        #region Set value.

                        C26TypedList value = new C26TypedList();
                        value.bField = new B26TypedList();
                        value.bField.aDictionary = new Dictionary<A26, A26>();

                        for (int i = 0; i < cycleLength; i++)
                        {
                            value.bField.aDictionary.Add(
                                new A26()
                                {
                                    iField = i * 2,
                                    sProperty = (i * 2).ToString()
                                },
                                new A26()
                                {
                                    iField = i,
                                    sProperty = i.ToString()
                                });
                        }

                        C26ObjectList clonedValue = serializer.Clone<C26ObjectList>(value);

                        #endregion


                        #region Check values.

                        Assert.IsNull(clonedValue.bField.aAttributeDictionary);

                        // Keys are empty, because type has not been registered for restore and could not be found in any types chain.
                        Assert.IsTrue(clonedValue.bField.aDictionary.Count == 0);

                        #endregion
                    }

                    #endregion



                    #region Object property set to type without serialization attribute.

                    {
                        #region Set value.

                        C26TypedList value = new C26TypedList();
                        value.bField = new B26TypedList();

                        value.bField.aObject = new A26NoClassAttribute();

                        C26ObjectList clonedValue = serializer.Clone<C26ObjectList>(value);

                        #endregion


                        #region Check result.

                        Assert.IsNull(clonedValue.bField.aObject);

                        Assert.IsNull(clonedValue.bField.aDictionary);
                        Assert.IsNull(clonedValue.bProperty);

                        #endregion
                    }

                    #endregion


                    #region Object property set to type with serialization attribute, type is not registered.

                    {
                        #region Set value.

                        C26TypedList value = new C26TypedList();
                        value.bField = new B26TypedList();

                        value.bField.aObject = new A26WithClassAttribute();

                        C26ObjectList clonedValue = serializer.Clone<C26ObjectList>(value);

                        #endregion


                        #region Check result.

                        Assert.IsNull(clonedValue.bField.aObject); // type is not known at deserialization

                        Assert.IsNull(clonedValue.bField.aDictionary);
                        Assert.IsNull(clonedValue.bProperty);

                        #endregion
                    }

                    #endregion


                    #region Object property set to type with serialization attribute.

                    {
                        #region Set value.

                        serializer.RegisterType(typeof(A26WithClassAttribute));

                        C26TypedList value = new C26TypedList();
                        value.bField = new B26TypedList();

                        value.bField.aObject = new A26WithClassAttribute();

                        C26ObjectList clonedValue = serializer.Clone<C26ObjectList>(value);

                        #endregion


                        #region Check result.

                        Assert.IsTrue(clonedValue.bField.aObject.GetType() == typeof(A26WithClassAttribute));

                        Assert.IsNull(clonedValue.bField.aDictionary);
                        Assert.IsNull(clonedValue.bProperty);

                        #endregion


                        #region Restore state.

                        serializer = createSerializer(useProcessedTypes);

                        #endregion
                    }

                    #endregion



                    #region Serialize Dictionary<T, T> with values, set property; type without serialization attribute is registered.

                    {
                        #region Set value.

                        serializer.RegisterType(typeof(A26NoClassAttribute));

                        C26TypedList value = new C26TypedList();
                        value.bField = new B26TypedList();
                        value.bField.aDictionary = new Dictionary<A26, A26>();

                        for (int i = 0; i < cycleLength; i++)
                        {
                            value.bField.aDictionary.Add(
                                new A26NoClassAttribute()
                                {
                                    iField = i * 2,
                                    sProperty = (i * 2).ToString()
                                },
                                new A26NoClassAttribute()
                                {
                                    iField = i,
                                    sProperty = i.ToString()
                                });
                        }

                        value.bField.aObject = new A26NoClassAttribute()
                        {
                            iField = intValue,
                            sProperty = stringValue
                        };

                        C26ObjectList clonedValue = serializer.Clone<C26ObjectList>(value);

                        #endregion


                        #region Check values.

                        Assert.IsNull(clonedValue.bField.aAttributeDictionary);
                        Assert.IsTrue(clonedValue.bField.aDictionary.Count == 0);
                        Assert.IsNull(clonedValue.bField.aObject);

                        #endregion


                        #region Restore state.

                        serializer = createSerializer(useProcessedTypes);

                        #endregion
                    }

                    #endregion


                    #region Serialize Dictionary<T, T> with values, set property; type with GUID is registered, no inheritance.

                    {
                        #region Set value.

                        serializer.RegisterType(typeof(A26));

                        C26TypedList value = new C26TypedList();
                        value.bField = new B26TypedList();
                        value.bField.aDictionary = new Dictionary<A26, A26>();

                        for (int i = 0; i < cycleLength; i++)
                        {
                            value.bField.aDictionary.Add(
                                new A26()
                                {
                                    iField = i * 2,
                                    sProperty = (i * 2).ToString()
                                },
                                new A26()
                                {
                                    iField = i,
                                    sProperty = i.ToString()
                                });
                        }

                        value.bField.aObject = new A26()
                        {
                            iField = intValue,
                            sProperty = stringValue
                        };

                        C26ObjectList clonedValue = serializer.Clone<C26ObjectList>(value);

                        #endregion


                        #region Check values.

                        Assert.IsNull(clonedValue.bField.aAttributeDictionary);

                        {
                            int i = 0;
                            foreach (KeyValuePair<object, object> pair in clonedValue.bField.aDictionary)
                            {
                                A26 aKeyValue = (A26)pair.Key;
                                A26 aValue = (A26)pair.Value;

                                Assert.IsTrue(aKeyValue.iField == i * 2);
                                Assert.IsTrue(aKeyValue.sProperty == (i * 2).ToString());

                                Assert.IsTrue(aValue.iField == i);
                                Assert.IsTrue(aValue.sProperty == i.ToString());

                                i++;
                            }

                            // Check property object.
                            Assert.IsTrue(clonedValue.bField.aObject.GetType() == typeof(A26));
                            A26 aClonedObject = (A26)clonedValue.bField.aObject;

                            Assert.IsTrue(aClonedObject.iField == intValue);
                            Assert.IsTrue(aClonedObject.sProperty == stringValue);
                        }

                        #endregion


                        #region Restore state.

                        serializer = createSerializer(useProcessedTypes);

                        #endregion
                    }

                    #endregion


                    #region Serialize Dictionary<T, T> with values, set property; type with GUID is registered, collection is of type without GUID.

                    {
                        #region Set value.

                        serializer.RegisterType(typeof(A26WithClassAttribute));

                        C26TypedList value = new C26TypedList();
                        value.bField = new B26TypedList();
                        value.bField.aDictionary = new Dictionary<A26, A26>();

                        for (int i = 0; i < cycleLength; i++)
                        {
                            value.bField.aDictionary.Add(
                                new A26WithClassAttribute()
                                {
                                    iField = i * 2,
                                    sProperty = (i * 2).ToString()
                                },
                                new A26WithClassAttribute()
                                {
                                    iField = i,
                                    sProperty = i.ToString()
                                });
                        }

                        value.bField.aObject = new A26WithClassAttribute()
                        {
                            iField = intValue,
                            sProperty = stringValue
                        };

                        C26ObjectList clonedValue = serializer.Clone<C26ObjectList>(value);

                        #endregion


                        #region Check values.

                        Assert.IsNull(clonedValue.bField.aAttributeDictionary);

                        {
                            int i = 0;
                            foreach (KeyValuePair<object, object> pair in clonedValue.bField.aDictionary)
                            {
                                A26WithClassAttribute aKeyInheritedValue = (A26WithClassAttribute)pair.Key;
                                A26WithClassAttribute aInheritedValue = (A26WithClassAttribute)pair.Value;

                                A26 aKeyValue = (A26)pair.Key;
                                A26 aValue = (A26)pair.Value;


                                Assert.IsTrue(aKeyInheritedValue.iField == i * 2);
                                Assert.IsTrue(aKeyInheritedValue.sProperty == (i * 2).ToString());

                                Assert.IsTrue(aInheritedValue.iField == i);
                                Assert.IsTrue(aInheritedValue.sProperty == i.ToString());

                                // Fields are hidden.
                                Assert.IsTrue(aKeyValue.iField == 0);
                                Assert.IsTrue(aKeyValue.sProperty == null);

                                Assert.IsTrue(aValue.iField == 0);
                                Assert.IsTrue(aValue.sProperty == null);

                                i++;
                            }


                            // Check property object.
                            Assert.IsTrue(clonedValue.bField.aObject.GetType() == typeof(A26WithClassAttribute));
                            A26WithClassAttribute aClonedObject = (A26WithClassAttribute)clonedValue.bField.aObject;

                            Assert.IsTrue(aClonedObject.iField == intValue);
                            Assert.IsTrue(aClonedObject.sProperty == stringValue);
                        }

                        #endregion


                        #region Restore state.

                        serializer = createSerializer(useProcessedTypes);

                        #endregion
                    }

                    #endregion


                    #region Serialize Dictionary<T, T> with values, set property; type with GUID is registered, collection is of type with GUID.

                    {
                        #region Set value.

                        serializer.RegisterType(typeof(A26WithClassAttribute));

                        C26TypedList value = new C26TypedList();
                        value.bField = new B26TypedList();
                        value.bField.aAttributeDictionary = new Dictionary<A26WithClassAttribute, A26WithClassAttribute>();

                        for (int i = 0; i < cycleLength; i++)
                        {
                            value.bField.aAttributeDictionary.Add(
                                new A26WithClassAttribute()
                                {
                                    iField = i * 2,
                                    sProperty = (i * 2).ToString()
                                },
                                new A26WithClassAttribute()
                                {
                                    iField = i,
                                    sProperty = i.ToString()
                                });
                        }

                        value.bField.aObject = new A26WithClassAttribute()
                        {
                            iField = intValue,
                            sProperty = stringValue
                        };

                        C26ObjectList clonedValue = serializer.Clone<C26ObjectList>(value);

                        #endregion


                        #region Check values.

                        Assert.IsNull(clonedValue.bField.aDictionary);

                        {
                            int i = 0;
                            foreach (KeyValuePair<object, object> pair in clonedValue.bField.aAttributeDictionary)
                            {
                                A26WithClassAttribute aKeyInheritedValue = (A26WithClassAttribute)pair.Key;
                                A26WithClassAttribute aInheritedValue = (A26WithClassAttribute)pair.Value;

                                A26 aKeyValue = (A26)pair.Key;
                                A26 aValue = (A26)pair.Value;


                                Assert.IsTrue(aKeyInheritedValue.iField == i * 2);
                                Assert.IsTrue(aKeyInheritedValue.sProperty == (i * 2).ToString());

                                Assert.IsTrue(aInheritedValue.iField == i);
                                Assert.IsTrue(aInheritedValue.sProperty == i.ToString());

                                // Fields are hidden.
                                Assert.IsTrue(aKeyValue.iField == 0);
                                Assert.IsTrue(aKeyValue.sProperty == null);

                                Assert.IsTrue(aValue.iField == 0);
                                Assert.IsTrue(aValue.sProperty == null);

                                i++;
                            }


                            // Check property object.
                            Assert.IsTrue(clonedValue.bField.aObject.GetType() == typeof(A26WithClassAttribute));
                            A26WithClassAttribute aClonedObject = (A26WithClassAttribute)clonedValue.bField.aObject;

                            Assert.IsTrue(aClonedObject.iField == intValue);
                            Assert.IsTrue(aClonedObject.sProperty == stringValue);
                        }

                        #endregion


                        #region Restore state.

                        serializer = createSerializer(useProcessedTypes);

                        #endregion
                    }

                    #endregion


                    #region Serialize Dictionary<T, T> with values, set property; inherited type with GUID is registered, collection is of type with GUID.

                    {
                        #region Set value.

                        serializer.RegisterType(typeof(A26InheritedWithClassAttribute));

                        C26TypedList value = new C26TypedList();
                        value.bField = new B26TypedList();
                        value.bField.aAttributeDictionary = new Dictionary<A26WithClassAttribute, A26WithClassAttribute>();

                        for (int i = 0; i < cycleLength; i++)
                        {
                            value.bField.aAttributeDictionary.Add(
                                new A26InheritedWithClassAttribute()
                                {
                                    iField = i * 2,
                                    sProperty = (i * 2).ToString()
                                },
                                new A26InheritedWithClassAttribute()
                                {
                                    iField = i,
                                    sProperty = i.ToString()
                                });
                        }

                        // Set property object.
                        value.bField.aObject = new A26InheritedWithClassAttribute()
                        {
                            iField = intValue,
                            sProperty = stringValue
                        };

                        C26ObjectList clonedValue = serializer.Clone<C26ObjectList>(value);

                        #endregion


                        #region Check values.

                        Assert.IsNull(clonedValue.bField.aDictionary);

                        {
                            int i = 0;
                            foreach (KeyValuePair<object, object> pair in clonedValue.bField.aAttributeDictionary)
                            {
                                A26InheritedWithClassAttribute aKeyInheritedValue = (A26InheritedWithClassAttribute)pair.Key;
                                A26WithClassAttribute aKeyClassValue = (A26WithClassAttribute)pair.Key;
                                A26 aKeyValue = (A26)pair.Key;

                                A26InheritedWithClassAttribute aInheritedValue = (A26InheritedWithClassAttribute)pair.Value;
                                A26WithClassAttribute aClassValue = (A26WithClassAttribute)pair.Value;
                                A26 aValue = (A26)pair.Value;


                                Assert.IsTrue(aKeyInheritedValue.iField == i * 2);
                                Assert.IsTrue(aKeyInheritedValue.sProperty == (i * 2).ToString());

                                Assert.IsTrue(aInheritedValue.iField == i);
                                Assert.IsTrue(aInheritedValue.sProperty == i.ToString());


                                // Fields are hidden, properties are overridden.
                                Assert.IsTrue(aKeyClassValue.iField == i * 2);
                                Assert.IsTrue(aKeyClassValue.sProperty == (i * 2).ToString());

                                Assert.IsTrue(aClassValue.iField == i);
                                Assert.IsTrue(aClassValue.sProperty == i.ToString());


                                // Fields are hidden.
                                Assert.IsTrue(aKeyValue.iField == 0);
                                Assert.IsTrue(aKeyValue.sProperty == null);

                                Assert.IsTrue(aValue.iField == 0);
                                Assert.IsTrue(aValue.sProperty == null);

                                i++;
                            }


                            // Check property object.
                            Assert.IsTrue(clonedValue.bField.aObject.GetType() == typeof(A26InheritedWithClassAttribute));

                            A26InheritedWithClassAttribute aClonedInheritedObject = (A26InheritedWithClassAttribute)clonedValue.bField.aObject;
                            A26WithClassAttribute aClonedObject = (A26WithClassAttribute)clonedValue.bField.aObject;

                            Assert.IsTrue(aClonedInheritedObject.iField == intValue);
                            Assert.IsTrue(aClonedInheritedObject.sProperty == stringValue);

                            // Fields are hidden.
                            Assert.IsTrue(aClonedObject.iField == intValue);
                            Assert.IsTrue(aClonedObject.sProperty == stringValue); // property is overridden
                        }

                        #endregion


                        #region Restore state.

                        serializer = createSerializer(useProcessedTypes);

                        #endregion
                    }

                    #endregion

                    #endregion


                    #region Serialize Dictionary<object, object> of one type values to Dictionary<T, T>.

                    #region Serialize Dictionary<object, object>, collection is not created.

                    {
                        #region Set value.

                        C26ObjectList value = new C26ObjectList();
                        value.bField = new B26ObjectList();

                        Assert.IsNull(value.bField.aDictionary);

                        C26TypedList clonedValue = serializer.Clone<C26TypedList>(value);

                        #endregion


                        #region Check result.

                        Assert.IsNull(clonedValue.bField.aDictionary);
                        Assert.IsNull(clonedValue.bField.aObject);
                        Assert.IsNull(clonedValue.bProperty);

                        #endregion
                    }

                    #endregion


                    #region Serialize Dictionary<object, object>, empty collection is created. Property set to object.

                    {
                        #region Set value.

                        C26ObjectList value = new C26ObjectList();
                        value.bField = new B26ObjectList();
                        value.bField.aDictionary = new Dictionary<object, object>();

                        Assert.IsNotNull(value.bField.aDictionary);

                        C26TypedList clonedValue = serializer.Clone<C26TypedList>(value);

                        #endregion


                        #region Check result.

                        Assert.IsNotNull(clonedValue.bField.aDictionary);
                        Assert.IsTrue(clonedValue.bField.aDictionary.Count == 0);
                        Assert.IsNull(clonedValue.bProperty);

                        Assert.IsNull(clonedValue.bField.aObject);

                        #endregion
                    }

                    #endregion


                    #region Serialize Dictionary<object, object> with values, property set; no types are registered.

                    {
                        #region Set value.

                        C26ObjectList value = new C26ObjectList();
                        value.bField = new B26ObjectList();
                        value.bField.aDictionary = new Dictionary<object, object>();

                        for (int i = 0; i < cycleLength; i++)
                        {
                            value.bField.aDictionary.Add(
                                new A26()
                                {
                                    iField = i * 2,
                                    sProperty = (i * 2).ToString()
                                },
                                new A26()
                                {
                                    iField = i,
                                    sProperty = i.ToString()
                                });
                        }

                        // Set object property.
                        value.bField.aObject = new A26()
                        {
                            iField = intValue,
                            sProperty = stringValue
                        };


                        C26TypedList clonedValue = serializer.Clone<C26TypedList>(value);

                        #endregion


                        #region Check values.

                        Assert.IsNull(clonedValue.bField.aAttributeDictionary);

                        {
                            int i = 0;
                            foreach (KeyValuePair<A26, A26> pair in clonedValue.bField.aDictionary)
                            {
                                // NB.! Values are not null, because A26 type is known at deserialization time through property type of B26TypedList.
                                A26 aKey = pair.Key;
                                A26 aValue = pair.Value;

                                Assert.IsTrue(aKey.iField == i * 2);
                                Assert.IsTrue(aKey.sProperty == (i * 2).ToString());

                                Assert.IsTrue(aValue.iField == i);
                                Assert.IsTrue(aValue.sProperty == i.ToString());

                                i++;
                            }


                            // Check property object.
                            Assert.IsTrue(clonedValue.bField.aObject.GetType() == typeof(A26));
                            Assert.IsTrue(clonedValue.bField.aObject.iField == intValue);
                            Assert.IsTrue(clonedValue.bField.aObject.sProperty == stringValue);

                            Assert.IsTrue(clonedValue.bField.aObject.iField == intValue);
                            Assert.IsTrue(clonedValue.bField.aObject.sProperty == stringValue);
                        }

                        #endregion
                    }

                    #endregion


                    #region Serialize Dictionary<object, object> with values, types are registered at serialization and not registered at deserialization.

                    {
                        #region Set value.

                        serializer.RegisterType(typeof(A26InheritedWithClassAttribute));

                        C26ObjectList value = new C26ObjectList();
                        value.bField = new B26ObjectList();
                        value.bField.aDictionary = new Dictionary<object, object>();

                        for (int i = 0; i < cycleLength; i++)
                        {
                            value.bField.aDictionary.Add(
                                new A26InheritedWithClassAttribute()
                                {
                                    iField = i * 2,
                                    sProperty = (i * 2).ToString()
                                },
                                new A26InheritedWithClassAttribute()
                                {
                                    iField = i,
                                    sProperty = i.ToString()
                                });
                        }

                        // Set object property.
                        value.bField.aObject = new A26InheritedWithClassAttribute()
                        {
                            iField = intValue,
                            sProperty = stringValue
                        };


                        C26TypedList clonedValue;
                        using (MemoryStream stream = new MemoryStream())
                        {
                            serializer.Serialize(stream, value);
                            serializer = createSerializer(useProcessedTypes);

                            clonedValue = serializer.Deserialize<C26TypedList>(stream);
                        }

                        #endregion


                        #region Check values.

                        Assert.IsNull(clonedValue.bField.aAttributeDictionary);
                        Assert.IsTrue(clonedValue.bField.aDictionary.Count == 0);
                        Assert.IsNull(clonedValue.bField.aObject);
                        // NB.! Values are not set, because A26InheritedWithClassAttribute type is not known at deserialization time.

                        #endregion


                        #region Restore state.

                        serializer = createSerializer(useProcessedTypes);

                        #endregion
                    }

                    #endregion


                    #region Serialize Dictionary<object, object> with values, type is registered at serialization and is known at deserialization.

                    {
                        #region Set value.

                        serializer.RegisterType(typeof(A26));

                        C26ObjectList value = new C26ObjectList();
                        value.bField = new B26ObjectList();
                        value.bField.aDictionary = new Dictionary<object, object>();

                        for (int i = 0; i < cycleLength; i++)
                        {
                            value.bField.aDictionary.Add(
                                new A26()
                                {
                                    iField = i * 2,
                                    sProperty = (i * 2).ToString()
                                },
                                new A26()
                                {
                                    iField = i,
                                    sProperty = i.ToString()
                                });
                        }

                        // Set object property.
                        value.bField.aObject = new A26()
                        {
                            iField = intValue,
                            sProperty = stringValue
                        };

                        C26TypedList clonedValue;
                        using (MemoryStream stream = new MemoryStream())
                        {
                            serializer.Serialize(stream, value);
                            serializer = createSerializer(useProcessedTypes);

                            clonedValue = serializer.Deserialize<C26TypedList>(stream);
                        }

                        #endregion


                        #region Check values.

                        Assert.IsNull(clonedValue.bField.aAttributeDictionary);

                        {
                            int i = 0;
                            foreach (KeyValuePair<A26, A26> pair in clonedValue.bField.aDictionary)
                            {
                                // NB.! Values are not null, because A26 type is known at deserialization time through property type of B26TypedList.
                                A26 aKey = pair.Key;
                                A26 aValue = pair.Value;

                                Assert.IsTrue(aKey.iField == i * 2);
                                Assert.IsTrue(aKey.sProperty == (i * 2).ToString());

                                Assert.IsTrue(aValue.iField == i);
                                Assert.IsTrue(aValue.sProperty == i.ToString());

                                i++;
                            }


                            // Check property object.
                            Assert.IsTrue(clonedValue.bField.aObject.GetType() == typeof(A26));

                            Assert.IsTrue(clonedValue.bField.aObject.iField == intValue);
                            Assert.IsTrue(clonedValue.bField.aObject.sProperty == stringValue);
                        }

                        #endregion


                        #region Restore state.

                        serializer = createSerializer(useProcessedTypes);

                        #endregion
                    }

                    #endregion




                    #region Property set to object, restore to typed property.

                    {
                        bool result = false;

                        try
                        {
                            #region Set value.

                            C26ObjectList value = new C26ObjectList();
                            value.bField = new B26ObjectList();

                            value.bField.aObject = new object();

                            C26TypedList clonedValue = serializer.Clone<C26TypedList>(value);

                            #endregion
                        }

                        catch (InvalidCastException)
                        {
                            result = true;
                        }

                        catch (ArgumentException)
                        {
                            result = true;
                        }

                        // We cannot restore simple object to type.
                        Assert.IsTrue(result);
                    }

                    #endregion


                    #region Object property set to type without serialization attribute.

                    {
                        #region Set value.

                        C26ObjectList value = new C26ObjectList();
                        value.bField = new B26ObjectList();

                        value.bField.aObject = new A26NoClassAttribute();

                        C26TypedList clonedValue = serializer.Clone<C26TypedList>(value);

                        #endregion


                        #region Check result.

                        Assert.IsNull(clonedValue.bField.aObject);

                        Assert.IsNull(clonedValue.bField.aDictionary);
                        Assert.IsNull(clonedValue.bProperty);

                        #endregion
                    }

                    #endregion


                    #region Object property set to type with serialization attribute, type is not registered.

                    {
                        #region Set value.

                        C26ObjectList value = new C26ObjectList();
                        value.bField = new B26ObjectList();

                        value.bField.aObject = new A26WithClassAttribute();

                        C26TypedList clonedValue = serializer.Clone<C26TypedList>(value);

                        #endregion


                        #region Check result.

                        // Type is known at deserialization.
                        Assert.IsTrue(clonedValue.bField.aObject.GetType() == typeof(A26WithClassAttribute));

                        Assert.IsNull(clonedValue.bField.aDictionary);
                        Assert.IsNull(clonedValue.bProperty);

                        #endregion
                    }

                    #endregion


                    #region Object property set to type with serialization attribute.

                    {
                        #region Set value.

                        serializer.RegisterType(typeof(A26WithClassAttribute));

                        C26ObjectList value = new C26ObjectList();
                        value.bField = new B26ObjectList();

                        value.bField.aObject = new A26WithClassAttribute();

                        C26TypedList clonedValue = serializer.Clone<C26TypedList>(value);

                        #endregion


                        #region Check result.

                        Assert.IsTrue(clonedValue.bField.aObject.GetType() == typeof(A26WithClassAttribute));

                        Assert.IsNull(clonedValue.bField.aDictionary);
                        Assert.IsNull(clonedValue.bProperty);

                        #endregion


                        #region Restore state.

                        serializer = createSerializer(useProcessedTypes);

                        #endregion
                    }

                    #endregion



                    #region Serialize Dictionary<object, object> with values, type without serialization attribute is registered.

                    {
                        #region Set value.

                        serializer.RegisterType(typeof(A26NoClassAttribute));

                        C26ObjectList value = new C26ObjectList();
                        value.bField = new B26ObjectList();
                        value.bField.aDictionary = new Dictionary<object, object>();

                        for (int i = 0; i < cycleLength; i++)
                        {
                            value.bField.aDictionary.Add(
                                new A26NoClassAttribute()
                                {
                                    iField = i * 2,
                                    sProperty = (i * 2).ToString()
                                },
                                new A26NoClassAttribute()
                                {
                                    iField = i,
                                    sProperty = i.ToString()
                                });
                        }

                        value.bField.aObject = new A26NoClassAttribute();

                        C26TypedList clonedValue = serializer.Clone<C26TypedList>(value);

                        #endregion


                        #region Check values.

                        Assert.IsNull(clonedValue.bField.aAttributeDictionary);
                        Assert.IsTrue(clonedValue.bField.aDictionary.Count == 0);
                        Assert.IsNull(clonedValue.bField.aObject);

                        #endregion


                        #region Restore state.

                        serializer = createSerializer(useProcessedTypes);

                        #endregion
                    }

                    #endregion


                    #region Serialize Dictionary<object, object> with values, type with GUID is registered, no inheritance.

                    {
                        #region Set value.

                        serializer.RegisterType(typeof(A26));

                        C26ObjectList value = new C26ObjectList();
                        value.bField = new B26ObjectList();
                        value.bField.aDictionary = new Dictionary<object, object>();

                        for (int i = 0; i < cycleLength; i++)
                        {
                            value.bField.aDictionary.Add(
                                new A26()
                                {
                                    iField = i * 2,
                                    sProperty = (i * 2).ToString()
                                },
                                new A26()
                                {
                                    iField = i,
                                    sProperty = i.ToString()
                                });
                        }

                        // Set object property.
                        value.bField.aObject = new A26()
                        {
                            iField = intValue,
                            sProperty = stringValue
                        };

                        C26TypedList clonedValue = serializer.Clone<C26TypedList>(value);

                        #endregion


                        #region Check values.

                        Assert.IsNull(clonedValue.bField.aAttributeDictionary);

                        {
                            int i = 0;
                            foreach (KeyValuePair<A26, A26> pair in clonedValue.bField.aDictionary)
                            {
                                A26 aKey = pair.Key;
                                A26 aValue = pair.Value;

                                Assert.IsTrue(aKey.iField == i * 2);
                                Assert.IsTrue(aKey.sProperty == (i * 2).ToString());

                                Assert.IsTrue(aValue.iField == i);
                                Assert.IsTrue(aValue.sProperty == i.ToString());

                                i++;
                            }

                            // Check property.
                            Assert.IsTrue(clonedValue.bField.aObject.GetType() == typeof(A26));
                            Assert.IsTrue(clonedValue.bField.aObject.iField == intValue);
                            Assert.IsTrue(clonedValue.bField.aObject.sProperty == stringValue);
                        }

                        #endregion


                        #region Restore state.

                        serializer = createSerializer(useProcessedTypes);

                        #endregion
                    }

                    #endregion


                    #region Serialize Dictionary<object, object> with values, type with GUID is registered, collection is of type without GUID.

                    {
                        #region Set value.

                        serializer.RegisterType(typeof(A26WithClassAttribute));

                        C26ObjectList value = new C26ObjectList();
                        value.bField = new B26ObjectList();
                        value.bField.aDictionary = new Dictionary<object, object>();

                        for (int i = 0; i < cycleLength; i++)
                        {
                            value.bField.aDictionary.Add(
                                new A26WithClassAttribute()
                                {
                                    iField = i * 2,
                                    sProperty = (i * 2).ToString()
                                },
                                new A26WithClassAttribute()
                                {
                                    iField = i,
                                    sProperty = i.ToString()
                                });
                        }

                        // Set object property.
                        value.bField.aObject = new A26WithClassAttribute()
                        {
                            iField = intValue,
                            sProperty = stringValue
                        };

                        C26TypedList clonedValue = serializer.Clone<C26TypedList>(value);

                        #endregion


                        #region Check values.

                        Assert.IsNull(clonedValue.bField.aAttributeDictionary);

                        {
                            int i = 0;
                            foreach (KeyValuePair<A26, A26> pair in clonedValue.bField.aDictionary)
                            {
                                A26WithClassAttribute aInheritedKey = (A26WithClassAttribute)pair.Key;
                                A26WithClassAttribute aInheritedValue = (A26WithClassAttribute)pair.Value;

                                A26 aKey = pair.Key;
                                A26 aValue = pair.Value;

                                Assert.IsTrue(aInheritedKey.iField == i * 2);
                                Assert.IsTrue(aInheritedKey.sProperty == (i * 2).ToString());

                                Assert.IsTrue(aInheritedValue.iField == i);
                                Assert.IsTrue(aInheritedValue.sProperty == i.ToString());

                                // Fields are hidden.
                                Assert.IsTrue(aKey.iField == 0);
                                Assert.IsTrue(aKey.sProperty == null);

                                Assert.IsTrue(aValue.iField == 0);
                                Assert.IsTrue(aValue.sProperty == null);

                                i++;
                            }


                            // Check property.
                            Assert.IsTrue(clonedValue.bField.aObject.GetType() == typeof(A26WithClassAttribute));
                            A26WithClassAttribute clonedAObject = (A26WithClassAttribute)clonedValue.bField.aObject;

                            Assert.IsTrue(clonedAObject.iField == intValue);
                            Assert.IsTrue(clonedAObject.sProperty == stringValue);

                            // A26 type object is checked.
                            Assert.IsTrue(clonedValue.bField.aObject.iField == 0);
                            Assert.IsNull(clonedValue.bField.aObject.sProperty);
                        }

                        #endregion


                        #region Restore state.

                        serializer = createSerializer(useProcessedTypes);

                        #endregion
                    }

                    #endregion


                    #region Serialize Dictionary<object, object> with values, type with GUID is registered, collection is of type with GUID.

                    {
                        #region Set value.

                        serializer.RegisterType(typeof(A26WithClassAttribute));

                        C26ObjectList value = new C26ObjectList();
                        value.bField = new B26ObjectList();
                        value.bField.aDictionary = new Dictionary<object, object>();

                        for (int i = 0; i < cycleLength; i++)
                        {
                            value.bField.aDictionary.Add(
                                new A26WithClassAttribute()
                                {
                                    iField = i * 2,
                                    sProperty = (i * 2).ToString()
                                },
                                new A26WithClassAttribute()
                                {
                                    iField = i,
                                    sProperty = i.ToString()
                                });
                        }

                        // Set object property.
                        value.bField.aObject = new A26WithClassAttribute()
                        {
                            iField = intValue,
                            sProperty = stringValue
                        };

                        C26TypedList clonedValue = serializer.Clone<C26TypedList>(value);

                        #endregion


                        #region Check values.

                        Assert.IsNull(clonedValue.bField.aAttributeDictionary);

                        {
                            int i = 0;
                            foreach (KeyValuePair<A26, A26> pair in clonedValue.bField.aDictionary)
                            {
                                A26WithClassAttribute aInheritedKey = (A26WithClassAttribute)pair.Key;
                                A26WithClassAttribute aInheritedValue = (A26WithClassAttribute)pair.Value;

                                A26 aKey = pair.Key;
                                A26 aValue = pair.Value;

                                Assert.IsTrue(aInheritedKey.iField == i * 2);
                                Assert.IsTrue(aInheritedKey.sProperty == (i * 2).ToString());

                                Assert.IsTrue(aInheritedValue.iField == i);
                                Assert.IsTrue(aInheritedValue.sProperty == i.ToString());

                                // Fields are hidden.
                                Assert.IsTrue(aKey.iField == 0);
                                Assert.IsTrue(aKey.sProperty == null);

                                Assert.IsTrue(aValue.iField == 0);
                                Assert.IsTrue(aValue.sProperty == null);

                                i++;
                            }


                            // Check property.
                            Assert.IsTrue(clonedValue.bField.aObject.GetType() == typeof(A26WithClassAttribute));
                            A26WithClassAttribute clonedAObject = (A26WithClassAttribute)clonedValue.bField.aObject;

                            Assert.IsTrue(clonedAObject.iField == intValue);
                            Assert.IsTrue(clonedAObject.sProperty == stringValue);

                            // A26 type object is checked.
                            Assert.IsTrue(clonedValue.bField.aObject.iField == 0);
                            Assert.IsNull(clonedValue.bField.aObject.sProperty);
                        }

                        #endregion


                        #region Restore state.

                        serializer = createSerializer(useProcessedTypes);

                        #endregion
                    }

                    #endregion


                    #region Serialize Dictionary<object, object> with values, inherited type with GUID is registered, collection is of type with GUID.

                    {
                        #region Set value.

                        serializer.RegisterType(typeof(A26InheritedWithClassAttribute));

                        C26ObjectList value = new C26ObjectList();
                        value.bField = new B26ObjectList();
                        value.bField.aDictionary = new Dictionary<object, object>();

                        for (int i = 0; i < cycleLength; i++)
                        {
                            value.bField.aDictionary.Add(
                                new A26InheritedWithClassAttribute()
                                {
                                    iField = i * 2,
                                    sProperty = (i * 2).ToString()
                                },
                                new A26InheritedWithClassAttribute()
                                {
                                    iField = i,
                                    sProperty = i.ToString()
                                });
                        }

                        // Set object property.
                        value.bField.aObject = new A26InheritedWithClassAttribute()
                        {
                            iField = intValue,
                            sProperty = stringValue
                        };

                        C26TypedList clonedValue = serializer.Clone<C26TypedList>(value);

                        #endregion


                        #region Check values.

                        Assert.IsNull(clonedValue.bField.aAttributeDictionary);

                        {
                            int i = 0;
                            foreach (KeyValuePair<A26, A26> pair in clonedValue.bField.aDictionary)
                            {
                                A26InheritedWithClassAttribute aKeyInheritedValue = (A26InheritedWithClassAttribute)pair.Key;
                                A26WithClassAttribute aKeyClassValue = (A26WithClassAttribute)pair.Key;
                                A26 aKeyValue = pair.Key;

                                A26InheritedWithClassAttribute aInheritedValue = (A26InheritedWithClassAttribute)pair.Value;
                                A26WithClassAttribute aClassValue = (A26WithClassAttribute)pair.Value;
                                A26 aValue = (A26)pair.Value;


                                Assert.IsTrue(aKeyInheritedValue.iField == i * 2);
                                Assert.IsTrue(aKeyInheritedValue.sProperty == (i * 2).ToString());

                                Assert.IsTrue(aInheritedValue.iField == i);
                                Assert.IsTrue(aInheritedValue.sProperty == i.ToString());


                                // Fields are hidden, properties are overridden.
                                Assert.IsTrue(aKeyClassValue.iField == i * 2);
                                Assert.IsTrue(aKeyClassValue.sProperty == (i * 2).ToString());

                                Assert.IsTrue(aClassValue.iField == i);
                                Assert.IsTrue(aClassValue.sProperty == i.ToString());


                                // Fields are hidden.
                                Assert.IsTrue(aKeyValue.iField == 0);
                                Assert.IsTrue(aKeyValue.sProperty == null);

                                Assert.IsTrue(aValue.iField == 0);
                                Assert.IsTrue(aValue.sProperty == null);

                                i++;
                            }


                            // Check property.
                            Assert.IsTrue(clonedValue.bField.aObject.GetType() == typeof(A26InheritedWithClassAttribute));
                            A26InheritedWithClassAttribute clonedAObject = (A26InheritedWithClassAttribute)clonedValue.bField.aObject;

                            Assert.IsTrue(clonedAObject.iField == intValue);
                            Assert.IsTrue(clonedAObject.sProperty == stringValue);

                            // A26 type object is checked.
                            Assert.IsTrue(clonedValue.bField.aObject.iField == 0);
                            Assert.IsNull(clonedValue.bField.aObject.sProperty);
                        }

                        #endregion


                        #region Restore state.

                        serializer = createSerializer(useProcessedTypes);

                        #endregion
                    }

                    #endregion

                    #endregion
                }

                #endregion


                #region Array.

                #region Serialize object[] of one type values to object[].

                #region Serialize object[], array is not created.

                {
                    serializer = createSerializer(useProcessedTypes);

                    #region Set value.

                    C25 value = new C25();
                    value.bField = new B25();

                    Assert.IsNull(value.bField.aArray);

                    C25 clonedValue = (C25)serializer.Clone(value);

                    #endregion


                    #region Check result.

                    Assert.IsNull(clonedValue.bField.aArray);
                    Assert.IsNull(clonedValue.bProperty);

                    #endregion
                }

                #endregion


                #region Serialize object[], empty array is created.

                {
                    #region Set value.

                    C25 value = new C25();
                    value.bField = new B25();
                    value.bField.aArray = new object[0];

                    Assert.IsNotNull(value.bField.aArray);

                    C25 clonedValue = (C25)serializer.Clone(value);

                    #endregion


                    #region Check result.

                    Assert.IsNotNull(clonedValue.bField.aArray);
                    Assert.IsTrue(clonedValue.bField.aArray.Length == 0);
                    Assert.IsNull(clonedValue.bProperty);

                    #endregion
                }

                #endregion


                #region Serialize object[] with values, no types are registered.

                {
                    #region Set value.

                    C25 value = new C25();
                    value.bField = new B25();
                    value.bField.aArray = new object[cycleLength];

                    for (int i = 0; i < cycleLength; i++)
                    {
                        value.bField.aArray[i] = new A25()
                        {
                            iField = i,
                            sProperty = i.ToString()
                        };
                    }

                    C25 clonedValue = (C25)serializer.Clone(value);

                    #endregion


                    #region Check values.

                    for (int i = 0; i < cycleLength; i++)
                    {
                        Assert.IsNull(clonedValue.bField.aArray[i]);
                    }

                    #endregion
                }

                #endregion


                #region Serialize object[] with plain type values, no types are registered.

                {
                    #region Set value.

                    C25 value = new C25();
                    value.bField = new B25();
                    value.bField.aArray = new object[]
                    {
                        dateTime,
                        dateTime1,

                        timeSpanValue,
                        timeSpanValue1,

                        guidValue,
                        guidValue1,


                        intValue,
                        intValue1,

                        int16Value,
                        int16Value1,

                        int64Value,
                        int64Value1,


                        uintValue,
                        uintValue1,

                        uint16Value,
                        uint16Value1,

                        uint64Value,
                        uint64Value1,


                        byteValue,
                        byteValue1,

                        floatValue,
                        floatValue1,

                        doubleValue,
                        doubleValue1,

                        decimalValue,
                        decimalValue1,
                    };


                    C25 clonedValue = (C25)serializer.Clone(value);

                    #endregion


                    #region Check values.

                    Assert.IsTrue(clonedValue.bField.aArray[0].Equals(dateTime));
                    Assert.IsTrue(clonedValue.bField.aArray[1].Equals(dateTime1));

                    Assert.IsTrue(clonedValue.bField.aArray[2].Equals(timeSpanValue));
                    Assert.IsTrue(clonedValue.bField.aArray[3].Equals(timeSpanValue1));

                    Assert.IsTrue(clonedValue.bField.aArray[4].Equals(guidValue));
                    Assert.IsTrue(clonedValue.bField.aArray[5].Equals(guidValue1));

                    Assert.IsTrue(clonedValue.bField.aArray[6].Equals(intValue));
                    Assert.IsTrue(clonedValue.bField.aArray[7].Equals(intValue1));

                    Assert.IsTrue(clonedValue.bField.aArray[8].Equals(int16Value));
                    Assert.IsTrue(clonedValue.bField.aArray[9].Equals(int16Value1));

                    Assert.IsTrue(clonedValue.bField.aArray[10].Equals(int64Value));
                    Assert.IsTrue(clonedValue.bField.aArray[11].Equals(int64Value1));

                    Assert.IsTrue(clonedValue.bField.aArray[12].Equals(uintValue));
                    Assert.IsTrue(clonedValue.bField.aArray[13].Equals(uintValue1));

                    Assert.IsTrue(clonedValue.bField.aArray[14].Equals(uint16Value));
                    Assert.IsTrue(clonedValue.bField.aArray[15].Equals(uint16Value1));

                    Assert.IsTrue(clonedValue.bField.aArray[16].Equals(uint64Value));
                    Assert.IsTrue(clonedValue.bField.aArray[17].Equals(uint64Value1));

                    Assert.IsTrue(clonedValue.bField.aArray[18].Equals(byteValue));
                    Assert.IsTrue(clonedValue.bField.aArray[19].Equals(byteValue1));

                    Assert.IsTrue(clonedValue.bField.aArray[20].Equals(floatValue));
                    Assert.IsTrue(clonedValue.bField.aArray[21].Equals(floatValue1));

                    Assert.IsTrue(clonedValue.bField.aArray[22].Equals(doubleValue));
                    Assert.IsTrue(clonedValue.bField.aArray[23].Equals(doubleValue1));

                    Assert.IsTrue(clonedValue.bField.aArray[24].Equals(decimalValue));
                    Assert.IsTrue(clonedValue.bField.aArray[25].Equals(decimalValue1));

                    #endregion
                }

                #endregion



                #region Serialize object[] with values, types is registered at serialization and is unknown at deserialization.

                {
                    #region Set value.

                    serializer.RegisterType(typeof(A25));

                    C25 value = new C25();
                    value.bField = new B25();
                    value.bField.aArray = new object[cycleLength];

                    for (int i = 0; i < cycleLength; i++)
                    {
                        value.bField.aArray[i] = new A25()
                        {
                            iField = i,
                            sProperty = i.ToString()
                        };
                    }

                    C25 clonedValue;
                    using (MemoryStream stream = new MemoryStream())
                    {
                        serializer.Serialize(stream, value);
                        serializer = createSerializer(useProcessedTypes);

                        clonedValue = serializer.Deserialize<C25>(stream);
                    }

                    #endregion


                    #region Check values.

                    for (int i = 0; i < cycleLength; i++)
                    {
                        Assert.IsNull(clonedValue.bField.aArray[i]);
                    }

                    #endregion


                    #region Restore state.

                    serializer = createSerializer(useProcessedTypes);

                    #endregion
                }

                #endregion


                #region Serialize object[] with values, type without serialization attribute is registered.

                {
                    #region Set value.

                    serializer.RegisterType(typeof(A25NoClassAttribute));

                    C25 value = new C25();
                    value.bField = new B25();
                    value.bField.aArray = new object[cycleLength];

                    for (int i = 0; i < cycleLength; i++)
                    {
                        value.bField.aArray[i] = new A25NoClassAttribute()
                        {
                            iField = i,
                            sProperty = i.ToString()
                        };
                    }

                    C25 clonedValue = (C25)serializer.Clone(value);

                    #endregion


                    #region Check values.

                    for (int i = 0; i < cycleLength; i++)
                    {
                        Assert.IsNull(clonedValue.bField.aArray[i]);
                    }

                    #endregion


                    #region Restore state.

                    serializer = createSerializer(useProcessedTypes);

                    #endregion
                }

                #endregion


                #region Serialize object[] with values, type with GUID is registered, no inheritance.

                {
                    #region Set value.

                    serializer.RegisterType(typeof(A25));

                    C25 value = new C25();
                    value.bField = new B25();
                    value.bField.aArray = new object[cycleLength];

                    for (int i = 0; i < cycleLength; i++)
                    {
                        value.bField.aArray[i] = new A25()
                        {
                            iField = i,
                            sProperty = i.ToString()
                        };
                    }

                    C25 clonedValue = (C25)serializer.Clone(value);

                    #endregion


                    #region Check values.

                    for (int i = 0; i < cycleLength; i++)
                    {
                        A25 aValue = (A25)clonedValue.bField.aArray[i];

                        Assert.IsTrue(aValue.iField == i);
                        Assert.IsTrue(aValue.sProperty == i.ToString());
                    }

                    #endregion


                    #region Restore state.

                    serializer = createSerializer(useProcessedTypes);

                    #endregion
                }

                #endregion


                #region Serialize object[] with values, type with GUID is registered, no inheritance (v. 2).

                {
                    #region Set value.

                    serializer.RegisterType(typeof(A25WithClassAttribute));

                    C25 value = new C25();
                    value.bField = new B25();
                    value.bField.aArray = new object[cycleLength];

                    for (int i = 0; i < cycleLength; i++)
                    {
                        value.bField.aArray[i] = new A25WithClassAttribute()
                        {
                            iField = i,
                            sProperty = i.ToString()
                        };
                    }

                    C25 clonedValue = (C25)serializer.Clone(value);

                    #endregion


                    #region Check values.

                    for (int i = 0; i < cycleLength; i++)
                    {
                        A25WithClassAttribute aValue = (A25WithClassAttribute)clonedValue.bField.aArray[i];

                        Assert.IsTrue(aValue.iField == i);
                        Assert.IsTrue(aValue.sProperty == i.ToString());
                    }

                    #endregion


                    #region Restore state.

                    serializer = createSerializer(useProcessedTypes);

                    #endregion
                }

                #endregion


                #region Serialize object[] with values, type with GUID is registered, with inheritance.

                {
                    #region Set value.

                    serializer.RegisterType(typeof(A25InheritedWithClassAttribute));

                    C25 value = new C25();
                    value.bField = new B25();
                    value.bField.aArray = new object[cycleLength];

                    for (int i = 0; i < cycleLength; i++)
                    {
                        value.bField.aArray[i] = new A25InheritedWithClassAttribute()
                        {
                            iField = i,
                            sProperty = i.ToString()
                        };
                    }

                    C25 clonedValue = (C25)serializer.Clone(value);

                    #endregion


                    #region Check values.

                    for (int i = 0; i < cycleLength; i++)
                    {
                        A25InheritedWithClassAttribute aInheritedValue = (A25InheritedWithClassAttribute)clonedValue.bField.aArray[i];
                        A25WithClassAttribute aValue = (A25WithClassAttribute)clonedValue.bField.aArray[i];

                        Assert.IsTrue(aInheritedValue.iField == i);
                        Assert.IsTrue(aInheritedValue.sProperty == i.ToString());

                        // Fields are hidden.
                        Assert.IsTrue(aValue.iField == 0);
                        Assert.IsTrue(aValue.sProperty == null);
                    }

                    #endregion


                    #region Restore state.

                    serializer = createSerializer(useProcessedTypes);

                    #endregion
                }

                #endregion


                #region Serialize object[] with values, type without GUID (inherited from type with GUID) is registered, with inheritance.

                {
                    #region Set value.

                    serializer.RegisterType(typeof(A25InheritedWithoutClassAttribute));

                    C25 value = new C25();
                    value.bField = new B25();
                    value.bField.aArray = new object[cycleLength];

                    for (int i = 0; i < cycleLength; i++)
                    {
                        value.bField.aArray[i] = new A25InheritedWithoutClassAttribute()
                        {
                            iField = i,
                            sProperty = i.ToString()
                        };
                    }

                    C25 clonedValue = (C25)serializer.Clone(value);

                    #endregion


                    #region Check values.

                    for (int i = 0; i < cycleLength; i++)
                    {
                        Assert.IsNull(clonedValue.bField.aArray[i]);
                    }

                    #endregion


                    #region Restore state.

                    serializer = createSerializer(useProcessedTypes);

                    #endregion
                }

                #endregion

                #endregion


                #region Serialize T[] to object[].

                #region Serialize T[], array is not created.

                {
                    #region Set value.

                    C26TypedList value = new C26TypedList();
                    value.bField = new B26TypedList();

                    Assert.IsNull(value.bField.aArray);

                    C26ObjectList clonedValue = serializer.Clone<C26ObjectList>(value);

                    #endregion


                    #region Check result.

                    Assert.IsNull(clonedValue.bField.aArray);
                    Assert.IsNull(clonedValue.bProperty);

                    #endregion
                }

                #endregion


                #region Serialize T[], empty array is created.

                {
                    #region Set value.

                    C26TypedList value = new C26TypedList();
                    value.bField = new B26TypedList();
                    value.bField.aArray = new A26[0];

                    Assert.IsNotNull(value.bField.aArray);

                    C26ObjectList clonedValue = serializer.Clone<C26ObjectList>(value);

                    #endregion


                    #region Check result.

                    Assert.IsNotNull(clonedValue.bField.aArray);
                    Assert.IsTrue(clonedValue.bField.aArray.Length == 0);
                    Assert.IsNull(clonedValue.bProperty);

                    #endregion
                }

                #endregion


                #region Serialize T[] with values, no types are registered.

                {
                    #region Set value.

                    C26TypedList value = new C26TypedList();
                    value.bField = new B26TypedList();
                    value.bField.aArray = new A26[cycleLength];

                    for (int i = 0; i < cycleLength; i++)
                    {
                        value.bField.aArray[i] = new A26()
                        {
                            iField = i,
                            sProperty = i.ToString()
                        };
                    }

                    C26ObjectList clonedValue = serializer.Clone<C26ObjectList>(value);

                    #endregion


                    #region Check values.

                    for (int i = 0; i < cycleLength; i++)
                    {
                        // Values are empty, because type has not been registered for restore and could not be found in any types chain.
                        Assert.IsNull(clonedValue.bField.aArray[i]);
                    }

                    #endregion
                }

                #endregion



                #region Serialize T[] with values, type without serialization attribute is registered.

                {
                    #region Set value.

                    serializer.RegisterType(typeof(A26NoClassAttribute));

                    C26TypedList value = new C26TypedList();
                    value.bField = new B26TypedList();
                    value.bField.aArray = new A26[cycleLength];

                    for (int i = 0; i < cycleLength; i++)
                    {
                        value.bField.aArray[i] = new A26NoClassAttribute()
                        {
                            iField = i,
                            sProperty = i.ToString()
                        };
                    }

                    C26ObjectList clonedValue = serializer.Clone<C26ObjectList>(value);

                    #endregion


                    #region Check values.

                    Assert.IsNull(clonedValue.bField.aAttributeArray);

                    for (int i = 0; i < cycleLength; i++)
                    {
                        Assert.IsNull(clonedValue.bField.aArray[i]);
                    }

                    #endregion


                    #region Restore state.

                    serializer = createSerializer(useProcessedTypes);

                    #endregion
                }

                #endregion


                #region Serialize T[] with values, type with GUID is registered, no inheritance.

                {
                    #region Set value.

                    serializer.RegisterType(typeof(A26));

                    C26TypedList value = new C26TypedList();
                    value.bField = new B26TypedList();
                    value.bField.aArray = new A26[cycleLength];

                    for (int i = 0; i < cycleLength; i++)
                    {
                        value.bField.aArray[i] = new A26()
                        {
                            iField = i,
                            sProperty = i.ToString()
                        };
                    }

                    C26ObjectList clonedValue = serializer.Clone<C26ObjectList>(value);

                    #endregion


                    #region Check values.

                    Assert.IsNull(clonedValue.bField.aAttributeArray);

                    for (int i = 0; i < cycleLength; i++)
                    {
                        A26 aValue = (A26)clonedValue.bField.aArray[i];

                        Assert.IsTrue(aValue.iField == i);
                        Assert.IsTrue(aValue.sProperty == i.ToString());
                    }

                    #endregion


                    #region Restore state.

                    serializer = createSerializer(useProcessedTypes);

                    #endregion
                }

                #endregion


                #region Serialize T[] with values, type with GUID is registered, collection is of type without GUID.

                {
                    #region Set value.

                    serializer.RegisterType(typeof(A26WithClassAttribute));

                    C26TypedList value = new C26TypedList();
                    value.bField = new B26TypedList();
                    value.bField.aArray = new A26[cycleLength];

                    for (int i = 0; i < cycleLength; i++)
                    {
                        value.bField.aArray[i] = new A26WithClassAttribute()
                        {
                            iField = i,
                            sProperty = i.ToString()
                        };
                    }

                    C26ObjectList clonedValue = serializer.Clone<C26ObjectList>(value);

                    #endregion


                    #region Check values.

                    Assert.IsNull(clonedValue.bField.aAttributeArray);

                    for (int i = 0; i < cycleLength; i++)
                    {
                        A26WithClassAttribute aInheritedValue = (A26WithClassAttribute)clonedValue.bField.aArray[i];
                        A26 aValue = (A26)clonedValue.bField.aArray[i];

                        Assert.IsTrue(aInheritedValue.iField == i);
                        Assert.IsTrue(aInheritedValue.sProperty == i.ToString());

                        // Fields are hidden.
                        Assert.IsTrue(aValue.iField == 0);
                        Assert.IsTrue(aValue.sProperty == null);
                    }

                    #endregion


                    #region Restore state.

                    serializer = createSerializer(useProcessedTypes);

                    #endregion
                }

                #endregion


                #region Serialize T[] with values, type with GUID is registered, collection is of type with GUID.

                {
                    #region Set value.

                    serializer.RegisterType(typeof(A26WithClassAttribute));

                    C26TypedList value = new C26TypedList();
                    value.bField = new B26TypedList();
                    value.bField.aAttributeArray = new A26WithClassAttribute[cycleLength];

                    for (int i = 0; i < cycleLength; i++)
                    {
                        value.bField.aAttributeArray[i] = new A26WithClassAttribute()
                        {
                            iField = i,
                            sProperty = i.ToString()
                        };
                    }

                    C26ObjectList clonedValue = serializer.Clone<C26ObjectList>(value);

                    #endregion


                    #region Check values.

                    Assert.IsNull(clonedValue.bField.aArray);

                    for (int i = 0; i < cycleLength; i++)
                    {
                        A26WithClassAttribute aInheritedValue = (A26WithClassAttribute)clonedValue.bField.aAttributeArray[i];
                        A26 aValue = (A26)clonedValue.bField.aAttributeArray[i];

                        Assert.IsTrue(aInheritedValue.iField == i);
                        Assert.IsTrue(aInheritedValue.sProperty == i.ToString());

                        // Fields are hidden.
                        Assert.IsTrue(aValue.iField == 0);
                        Assert.IsTrue(aValue.sProperty == null);
                    }

                    #endregion


                    #region Restore state.

                    serializer = createSerializer(useProcessedTypes);

                    #endregion
                }

                #endregion


                #region Serialize T[] with values, inherited type with GUID is registered, collection is of type with GUID.

                {
                    #region Set value.

                    serializer.RegisterType(typeof(A26WithClassAttribute)); // register type which is higher in inheritance chain
                    serializer.RegisterType(typeof(A26InheritedWithClassAttribute));
                    serializer.RegisterType(typeof(A26InheritedWithClassAttribute)); // register twice - by an oversight

                    C26TypedList value = new C26TypedList();
                    value.bField = new B26TypedList();
                    value.bField.aAttributeArray = new A26WithClassAttribute[cycleLength];

                    for (int i = 0; i < cycleLength; i++)
                    {
                        value.bField.aAttributeArray[i] = new A26InheritedWithClassAttribute()
                        {
                            iField = i,
                            sProperty = i.ToString()
                        };
                    }

                    C26ObjectList clonedValue = serializer.Clone<C26ObjectList>(value);

                    #endregion


                    #region Check values.

                    Assert.IsNull(clonedValue.bField.aArray);

                    for (int i = 0; i < cycleLength; i++)
                    {
                        // NB.! Field values are not restored properly, because collection values set their type to A26InheritedWithClassAttribute, and it hides base class field. Values are restored to the declared type of the collection, not to the real type.

                        A26InheritedWithClassAttribute aInheritedValue = (A26InheritedWithClassAttribute)clonedValue.bField.aAttributeArray[i];
                        A26WithClassAttribute aClassValue = (A26WithClassAttribute)clonedValue.bField.aAttributeArray[i];
                        A26 aValue = (A26)clonedValue.bField.aAttributeArray[i];

                        Assert.IsTrue(aInheritedValue.iField == i);
                        Assert.IsTrue(aInheritedValue.sProperty == i.ToString());


                        // These fields are hidden.
                        Assert.IsTrue(aClassValue.iField == i);
                        Assert.IsTrue(aClassValue.sProperty == i.ToString());

                        Assert.IsTrue(aValue.iField == 0);
                        Assert.IsTrue(aValue.sProperty == null);
                    }

                    #endregion


                    #region Restore state.

                    serializer = createSerializer(useProcessedTypes);

                    #endregion
                }

                #endregion

                #endregion


                #region Serialize object[] of one type values to T[].

                #region Serialize object[], array is not created.

                {
                    #region Set value.

                    C26ObjectList value = new C26ObjectList();
                    value.bField = new B26ObjectList();

                    Assert.IsNull(value.bField.aArray);

                    C26TypedList clonedValue = serializer.Clone<C26TypedList>(value);

                    #endregion


                    #region Check result.

                    Assert.IsNull(clonedValue.bField.aArray);
                    Assert.IsNull(clonedValue.bProperty);

                    #endregion
                }

                #endregion


                #region Serialize object[], empty array is created.

                {
                    #region Set value.

                    C26ObjectList value = new C26ObjectList();
                    value.bField = new B26ObjectList();
                    value.bField.aArray = new object[0];

                    Assert.IsNotNull(value.bField.aArray);

                    C26TypedList clonedValue = serializer.Clone<C26TypedList>(value);

                    #endregion


                    #region Check result.

                    Assert.IsNotNull(clonedValue.bField.aArray);
                    Assert.IsTrue(clonedValue.bField.aArray.Length == 0);
                    Assert.IsNull(clonedValue.bProperty);

                    #endregion
                }

                #endregion


                #region Serialize object[] with values, no types are registered.

                {
                    #region Set value.

                    C26ObjectList value = new C26ObjectList();
                    value.bField = new B26ObjectList();
                    value.bField.aArray = new object[cycleLength];

                    for (int i = 0; i < cycleLength; i++)
                    {
                        value.bField.aArray[i] = new A26()
                        {
                            iField = i,
                            sProperty = i.ToString()
                        };
                    }

                    C26TypedList clonedValue = serializer.Clone<C26TypedList>(value);

                    #endregion


                    #region Check values.

                    Assert.IsNull(clonedValue.bField.aAttributeArray);

                    for (int i = 0; i < cycleLength; i++)
                    {
                        // NB.! Values are set, because A26 type is known at deserialization time.
                        A26 aValue = clonedValue.bField.aArray[i];

                        Assert.IsTrue(aValue.iField == i);
                        Assert.IsTrue(aValue.sProperty == i.ToString());
                    }

                    #endregion
                }

                #endregion


                #region Serialize object[] with values, types are registered at serialization and not registered at deserialization.

                {
                    #region Set value.

                    serializer.RegisterType(typeof(A26InheritedWithClassAttribute));

                    C26ObjectList value = new C26ObjectList();
                    value.bField = new B26ObjectList();
                    value.bField.aArray = new object[cycleLength];

                    for (int i = 0; i < cycleLength; i++)
                    {
                        value.bField.aArray[i] = new A26InheritedWithClassAttribute()
                        {
                            iField = i,
                            sProperty = i.ToString()
                        };
                    }

                    C26TypedList clonedValue;
                    using (MemoryStream stream = new MemoryStream())
                    {
                        serializer.Serialize(stream, value);
                        serializer = createSerializer(useProcessedTypes);

                        clonedValue = serializer.Deserialize<C26TypedList>(stream);
                    }

                    #endregion


                    #region Check values.

                    Assert.IsNull(clonedValue.bField.aAttributeArray);

                    for (int i = 0; i < cycleLength; i++)
                    {
                        // NB.! Values are not set, because A26InheritedWithClassAttribute type is not known at deserialization time.

                        Assert.IsNull(clonedValue.bField.aArray[i]);
                    }

                    #endregion


                    #region Restore state.

                    serializer = createSerializer(useProcessedTypes);

                    #endregion
                }

                #endregion


                #region Serialize object[] with values, type is registered at serialization and is known at deserialization.

                {
                    #region Set value.

                    serializer.RegisterType(typeof(A26));

                    C26ObjectList value = new C26ObjectList();
                    value.bField = new B26ObjectList();
                    value.bField.aArray = new object[cycleLength];

                    for (int i = 0; i < cycleLength; i++)
                    {
                        value.bField.aArray[i] = new A26()
                        {
                            iField = i,
                            sProperty = i.ToString()
                        };
                    }

                    C26TypedList clonedValue;
                    using (MemoryStream stream = new MemoryStream())
                    {
                        serializer.Serialize(stream, value);
                        serializer = createSerializer(useProcessedTypes);

                        clonedValue = serializer.Deserialize<C26TypedList>(stream);
                    }

                    #endregion


                    #region Check values.

                    Assert.IsNull(clonedValue.bField.aAttributeArray);

                    for (int i = 0; i < cycleLength; i++)
                    {
                        // NB.! Values are not null, because A26 type is known at deserialization time through property type of B26TypedList.
                        A26 aValue = (A26)clonedValue.bField.aArray[i];

                        Assert.IsTrue(aValue.iField == i);
                        Assert.IsTrue(aValue.sProperty == i.ToString());
                    }

                    #endregion


                    #region Restore state.

                    serializer = createSerializer(useProcessedTypes);

                    #endregion
                }

                #endregion



                #region Serialize object[] with values, type without serialization attribute is registered.

                {
                    #region Set value.

                    serializer.RegisterType(typeof(A26NoClassAttribute));

                    C26ObjectList value = new C26ObjectList();
                    value.bField = new B26ObjectList();
                    value.bField.aArray = new object[cycleLength];

                    for (int i = 0; i < cycleLength; i++)
                    {
                        value.bField.aArray[i] = new A26NoClassAttribute()
                        {
                            iField = i,
                            sProperty = i.ToString()
                        };
                    }

                    C26TypedList clonedValue = serializer.Clone<C26TypedList>(value);

                    #endregion


                    #region Check values.

                    Assert.IsNull(clonedValue.bField.aAttributeArray);

                    for (int i = 0; i < cycleLength; i++)
                    {
                        Assert.IsNull(clonedValue.bField.aArray[i]);
                    }

                    #endregion


                    #region Restore state.

                    serializer = createSerializer(useProcessedTypes);

                    #endregion
                }

                #endregion


                #region Serialize object[] with values, type with GUID is registered, no inheritance.

                {
                    #region Set value.

                    serializer.RegisterType(typeof(A26));

                    C26ObjectList value = new C26ObjectList();
                    value.bField = new B26ObjectList();
                    value.bField.aArray = new object[cycleLength];

                    for (int i = 0; i < cycleLength; i++)
                    {
                        value.bField.aArray[i] = new A26()
                        {
                            iField = i,
                            sProperty = i.ToString()
                        };
                    }

                    C26TypedList clonedValue = serializer.Clone<C26TypedList>(value);

                    #endregion


                    #region Check values.

                    Assert.IsNull(clonedValue.bField.aAttributeArray);

                    for (int i = 0; i < cycleLength; i++)
                    {
                        A26 aValue = (A26)clonedValue.bField.aArray[i];

                        Assert.IsTrue(aValue.iField == i);
                        Assert.IsTrue(aValue.sProperty == i.ToString());
                    }

                    #endregion


                    #region Restore state.

                    serializer = createSerializer(useProcessedTypes);

                    #endregion
                }

                #endregion


                #region Serialize object[] with values, type with GUID is registered, collection is of type without GUID.

                {
                    #region Set value.

                    serializer.RegisterType(typeof(A26WithClassAttribute));

                    C26ObjectList value = new C26ObjectList();
                    value.bField = new B26ObjectList();
                    value.bField.aArray = new object[cycleLength];

                    for (int i = 0; i < cycleLength; i++)
                    {
                        value.bField.aArray[i] = new A26WithClassAttribute()
                        {
                            iField = i,
                            sProperty = i.ToString()
                        };
                    }

                    C26TypedList clonedValue = serializer.Clone<C26TypedList>(value);

                    #endregion


                    #region Check values.

                    Assert.IsNull(clonedValue.bField.aAttributeArray);

                    for (int i = 0; i < cycleLength; i++)
                    {
                        A26WithClassAttribute aInheritedValue = (A26WithClassAttribute)clonedValue.bField.aArray[i];
                        A26 aValue = (A26)clonedValue.bField.aArray[i];

                        Assert.IsTrue(aInheritedValue.iField == i);
                        Assert.IsTrue(aInheritedValue.sProperty == i.ToString());

                        // Fields are hidden.
                        Assert.IsTrue(aValue.iField == 0);
                        Assert.IsTrue(aValue.sProperty == null);
                    }

                    #endregion


                    #region Restore state.

                    serializer = createSerializer(useProcessedTypes);

                    #endregion
                }

                #endregion


                #region Serialize object[] with values, type with GUID is registered, collection is of type with GUID.

                {
                    #region Set value.

                    serializer.RegisterType(typeof(A26WithClassAttribute));

                    C26ObjectList value = new C26ObjectList();
                    value.bField = new B26ObjectList();
                    value.bField.aArray = new object[cycleLength];

                    for (int i = 0; i < cycleLength; i++)
                    {
                        value.bField.aArray[i] = new A26WithClassAttribute()
                        {
                            iField = i,
                            sProperty = i.ToString()
                        };
                    }

                    C26TypedList clonedValue = serializer.Clone<C26TypedList>(value);

                    #endregion


                    #region Check values.

                    Assert.IsNull(clonedValue.bField.aAttributeArray);

                    for (int i = 0; i < cycleLength; i++)
                    {
                        A26WithClassAttribute aInheritedValue = (A26WithClassAttribute)clonedValue.bField.aArray[i];
                        A26 aValue = (A26)clonedValue.bField.aArray[i];

                        Assert.IsTrue(aInheritedValue.iField == i);
                        Assert.IsTrue(aInheritedValue.sProperty == i.ToString());

                        // Fields are hidden.
                        Assert.IsTrue(aValue.iField == 0);
                        Assert.IsTrue(aValue.sProperty == null);
                    }

                    #endregion


                    #region Restore state.

                    serializer = createSerializer(useProcessedTypes);

                    #endregion
                }

                #endregion


                #region Serialize object[] with values, inherited type with GUID is registered, collection is of type with GUID.

                {
                    #region Set value.

                    serializer.RegisterType(typeof(A26InheritedWithClassAttribute));

                    C26ObjectList value = new C26ObjectList();
                    value.bField = new B26ObjectList();
                    value.bField.aArray = new object[cycleLength];

                    for (int i = 0; i < cycleLength; i++)
                    {
                        value.bField.aArray[i] = new A26InheritedWithClassAttribute()
                        {
                            iField = i,
                            sProperty = i.ToString()
                        };
                    }

                    C26TypedList clonedValue = serializer.Clone<C26TypedList>(value);

                    #endregion


                    #region Check values.

                    Assert.IsNull(clonedValue.bField.aAttributeArray);

                    for (int i = 0; i < cycleLength; i++)
                    {
                        // NB.! Field values are not restored properly, because collection values set their type to A26InheritedWithClassAttribute, and it hides base class field. Values are restored to the declared type of the collection, not to the real type.

                        A26InheritedWithClassAttribute aInheritedValue = (A26InheritedWithClassAttribute)clonedValue.bField.aArray[i];
                        A26WithClassAttribute aClassValue = (A26WithClassAttribute)clonedValue.bField.aArray[i];
                        A26 aValue = clonedValue.bField.aArray[i];

                        Assert.IsTrue(aInheritedValue.iField == i);
                        Assert.IsTrue(aInheritedValue.sProperty == i.ToString());


                        // These fields are hidden.
                        Assert.IsTrue(aClassValue.iField == i);
                        Assert.IsTrue(aClassValue.sProperty == i.ToString());

                        Assert.IsTrue(aValue.iField == 0);
                        Assert.IsTrue(aValue.sProperty == null);
                    }

                    #endregion


                    #region Restore state.

                    serializer = createSerializer(useProcessedTypes);

                    #endregion
                }

                #endregion

                #endregion

                #endregion

                #endregion

            }
        }

        #endregion


        #region Auxiliary functions.

        #region createSerializer().

        /// <summary>
        /// Creates new instance of <see cref="KTSerializer"/> with all necessary properties set.
        /// </summary>
        /// <param name="useProcessedTypes">Should serializer use processed types.</param>
        /// <returns>Created instance of <see cref="KTSerializer"/>.</returns>
        private static KTSerializer createSerializer(bool useProcessedTypes)
        {
            KTSerializer result = new KTSerializer(useProcessedTypes);
            return result;
        }

        #endregion


        #region createHeaderStream().

        /// <summary>
        /// (Re)Creates new <see cref="Stream/> object, when it's appropriate.
        /// </summary>
        /// <param name="useProcessedTypes">Should serializer use processed types.</param>
        /// <param name="stream"><see cref="Stream"/> object to set.</param>
        private static void createHeaderStream(bool useProcessedTypes, ref Stream stream)
        {
            if (stream != null) stream.Dispose();
            stream = useProcessedTypes ? new MemoryStream() : null;
        }

        #endregion


        #region registerTestTypes().

        /// <summary>
        /// Registers necessary types in serializer.
        /// </summary>
        /// <param name="serializer">Serializer to register types in.</param>
        /// <param name="i">Cycle counter.</param>
        private static void registerTestTypes(KTSerializer serializer, int i)
        {
            if (i == 0)
            {
                serializer.RegisterType(typeof(ARegister009));
            }
            else if (i == 1)
            {
                serializer.RegisterType(typeof(ARegister019));
            }
            else if (i == 2)
            {
                serializer.RegisterType(typeof(ARegister029));
            }
            else if (i == 3)
            {
                serializer.RegisterType(typeof(ARegister039));
            }
            else if (i == 4)
            {
                serializer.RegisterType(typeof(ARegister049));
            }
        }

        #endregion

        #endregion


        #region Auxiliary classes.

        #region Used.

        #region Test 1.

        class A1
        {
            #region Properties and fields.

            [KTSerializeInclude("c13d1ce1-5aec-42e8-9a1b-ba6ead1434d2")]
            public int iField = 0;

            [KTSerializeInclude("650afe40-24ad-4f44-b7cb-e4a8fe0461da")]
            public int iProperty
            {
                get;
                set;
            }


            [KTSerializeInclude("addc9270-27e9-4315-9b92-ee5dc83f0a2e")]
            protected int iFieldProtected = 0;

            [KTSerializeInclude("00379857-c3ce-4e54-9f55-8b1fa3e0f608")]
            protected int iPropertyProtected
            {
                get;
                set;
            }


            [KTSerializeInclude("88743a85-d197-49cb-b0c5-e4c7513d2d0f")]
            private int iFieldPrivate = 0;

            [KTSerializeInclude("519128fa-d616-45c1-b3e0-4b9e5187baf1")]
            private int iPropertyPrivate
            {
                get;
                set;
            }


            [KTSerializeInclude("3d08627e-4de1-4fe2-8eea-4202f7c749e7")]
            public static int iFieldStatic = 0;

            [KTSerializeInclude("8b7d9dad-ad9d-4817-967c-3d3c904a686b")]
            public static int iPropertyStatic
            {
                get;
                set;
            }


            [KTSerializeInclude("88d6753e-f6e7-48aa-a708-baae7b702d0a")]
            private static int iFieldStaticPrivate = 0;

            [KTSerializeInclude("c886a30e-a4f2-4528-b4c5-fa78ce524800")]
            private static int iPropertyStaticPrivate
            {
                get;
                set;
            }

            #endregion


            #region Constructors.

            public A1()
            { }

            #endregion
        }

        #endregion


        #region Test 2.

        [KTSerialize("ccd81bf1-30fd-451e-bc5f-851d0bbdf430")]
        class A2
        {
            #region Fields and properties.

            public int iField = 0;

            public int iProperty
            {
                get;
                set;
            }


            protected int iFieldProtected = 0;

            protected int iPropertyProtected
            {
                get;
                set;
            }


            private int iFieldPrivate = 0;

            private int iPropertyPrivate
            {
                get;
                set;
            }


            public static int iFieldStatic = 0;

            public static int iPropertyStatic
            {
                get;
                set;
            }


            private static int iFieldStaticPrivate = 0;

            private static int iPropertyStaticPrivate
            {
                get;
                set;
            }

            #endregion
        }

        #endregion



        #region Test 3.

        [KTSerialize("bc15c658-c411-45ce-a206-bba3edff2677")]
        class A3
        {
            #region Fields and properties.

            [KTSerializeInclude("cc2a1910-298f-4044-a4e4-27adc24e123a")]
            public int iField = 0;

            [KTSerializeInclude("cc2a1910-298f-4044-a4e4-27adc24e123a")]
            public int iProperty
            {
                get;
                set;
            }

            #endregion
        }

        #endregion


        #region Test 4.

        [KTSerialize("f5c0c48b-8930-478f-a114-4df5bd4beba4")]
        class A4
        {
            #region Fields and properties.

            [KTSerializeInclude("cc2a1910-298f-4044-a4e4-27adc24e123a")]
            protected int iFieldProtected = 0;

            [KTSerializeInclude("cc2a1910-298f-4044-a4e4-27adc24e123a")]
            protected int iPropertyProtected
            {
                get;
                set;
            }

            #endregion
        }

        #endregion


        #region Test 5.

        [KTSerialize("bb8a97ca-10d9-46b3-a8b6-db1ef8f5a8b3")]
        class A5
        {
            #region Fields and properties.

            [KTSerializeInclude("cc2a1910-298f-4044-a4e4-27adc24e123a")]
            private int iFieldPrivate = 0;

            [KTSerializeInclude("cc2a1910-298f-4044-a4e4-27adc24e123a")]
            private int iPropertyPrivate
            {
                get;
                set;
            }

            #endregion
        }

        #endregion


        #region Test 6.

        [KTSerialize("719ef515-a8a0-45c1-93b8-05c2b75c3611")]
        class A6
        {
            #region Fields and properties.

            [KTSerializeInclude("cc2a1910-298f-4044-a4e4-27adc24e123a")]
            public static int iFieldStatic = 0;

            [KTSerializeInclude("cc2a1910-298f-4044-a4e4-27adc24e123a")]
            public static int iPropertyStatic
            {
                get;
                set;
            }

            #endregion
        }

        #endregion


        #region Test 7.

        [KTSerialize("ae38057c-750c-421d-9c80-cb22eda20493")]
        class A7
        {
            #region Fields and properties.

            [KTSerializeInclude("cc2a1910-298f-4044-a4e4-27adc24e123a")]
            private static int iFieldStaticPrivate = 0;

            [KTSerializeInclude("cc2a1910-298f-4044-a4e4-27adc24e123a")]
            private static int iPropertyStaticPrivate
            {
                get;
                set;
            }

            #endregion
        }

        #endregion



        #region Test 8.

        #region A.

        [KTSerialize("c741dc04-675a-4a29-b6cd-45cc1bc21e16")]
        class A8
        {
            #region Properties and fields.

            [KTSerializeInclude("c13d1ce1-5aec-42e8-9a1b-ba6ead1434d2")]
            public int iField = 0;

            [KTSerializeInclude("650afe40-24ad-4f44-b7cb-e4a8fe0461da")]
            public int iProperty
            {
                get;
                set;
            }

            #endregion
        }


        [KTSerialize("c741dc04-675a-4a29-b6cd-45cc1bc21e16")]
        class A8_Big
        {
            #region Properties and fields.

            [KTSerializeInclude("263d5b67-33e6-4272-ae5c-0753da63dc56")]
            public APlainProperties aField;

            [KTSerializeInclude("afcdc5d3-0048-4649-a361-442fa9ff37fc")]
            public APlainProperties aProperty
            {
                get;
                set;
            }


            [KTSerializeInclude("650afe40-24ad-4f44-b7cb-e4a8fe0461da")]
            public int iProperty
            {
                get;
                set;
            }

            [KTSerializeInclude("c13d1ce1-5aec-42e8-9a1b-ba6ead1434d2")]
            public int iField = 0;

            #endregion
        }

        #endregion


        #region B.

        class B8
        {
            #region Properties and fields.

            public int iField = 0;

            public int iProperty
            {
                get;
                set;
            }

            #endregion
        }

        #endregion

        #endregion


        #region Test 9.

        #region A.

        class A9
        {
            #region Properties and fields.

            public int iField = 0;

            public int iProperty
            {
                get;
                set;
            }

            #endregion
        }

        #endregion


        #region B.

        [KTSerialize("b92328cd-d8ea-4193-8443-b00ce2e45d58")]
        class B9
        {
            #region Properties and fields.

            [KTSerializeInclude("c13d1ce1-5aec-42e9-9a1b-ba6ead1434d2")]
            public int iField = 0;

            [KTSerializeInclude("650afe40-24ad-4f44-b7cb-e4a9fe0461da")]
            public int iProperty
            {
                get;
                set;
            }

            #endregion
        }

        #endregion

        #endregion


        #region Test 10.

        #region A.

        class A10
        {
            #region Properties and fields.

            [KTSerializeInclude("c13d1ce1-5aec-42e10-9a1b-ba6ead1434d2")]
            public int iField = 0;

            [KTSerializeInclude("650afe40-24ad-4f44-b7cb-e4a8fe0461da")]
            public int iProperty
            {
                get;
                set;
            }

            #endregion
        }

        #endregion


        #region B.

        [KTSerialize("868f7f51-9aa3-4cbe-b459-7835b487bc1e")]
        class B10
        {
            #region Properties and fields.

            public int iField = 0;

            public int iProperty
            {
                get;
                set;
            }

            #endregion
        }

        #endregion

        #endregion


        #region Test 11.

        #region A.

        [KTSerialize("fad23339-be97-4cc2-8025-fc94e5558a84")]
        class A11
        {
            #region Properties and fields.

            public int iField = 0;

            public int iProperty
            {
                get;
                set;
            }

            #endregion
        }

        #endregion


        #region B.

        class B11
        {
            #region Properties and fields.

            [KTSerializeInclude("c13d1ce1-5aec-42e8-9a1b-ba6ead1434d2")]
            public int iField = 0;

            [KTSerializeInclude("650afe40-24ad-4f44-b7cb-e4a11fe0461da")]
            public int iProperty
            {
                get;
                set;
            }

            #endregion
        }

        #endregion

        #endregion


        #region Test 12.

        [KTSerialize("a5939fd0-79f9-43e6-8a8b-3d03b6f78c56")]
        class A12
        {
            #region Properties and fields.

            [KTSerializeInclude("false GUID")]
            public int iField = 0;

            [KTSerializeInclude("650afe40-24ad-4f44-b7cb-e4a8fe0461da")]
            public int iProperty
            {
                get;
                set;
            }

            #endregion
        }

        #endregion


        #region Test 13.

        #region A.

        [KTSerialize("544b3e76-4e77-42c0-80ba-d5e58cb11138")]
        class A13
        {
            #region Properties and fields.

            [KTSerializeInclude("46d953bc-91e3-4fd7-966c-ca309a27fbf2")]
            private int iField = 0;

            [KTSerializeInclude("6d3ace81-2c80-49de-9c4a-03309ab63719")]
            public int iProperty
            {
                get;
                set;
            }


            [KTSerializeInclude("1f25643d-2535-4f29-aece-77bff93905cc")]
            private A13 aField;

            [KTSerializeInclude("d478431c-dcac-475b-b545-c93b582a6fc2")]
            public A13 aProperty
            {
                get;
                set;
            }

            #endregion
        }

        #endregion


        #region B.

        [KTSerialize("1befc450-4802-44e4-96d6-b715396ce74f")]
        class B13
        {
            #region Properties and fields.

            [KTSerializeInclude("b771b8fb-3741-482d-aafd-f6aacc480306")]
            private C13 cField;

            [KTSerializeInclude("884334b5-58e6-46be-af1a-1d9922d145e1")]
            public C13 cProperty
            {
                get;
                set;
            }


            [KTSerializeInclude("455c5cba-a5c8-4f74-901e-ba570511a139")]
            private A13 aField;

            [KTSerializeInclude("0e535c6e-2e74-4eea-8937-2f062ed9d7df")]
            public A13 aProperty
            {
                get;
                set;
            }

            #endregion
        }

        #endregion


        #region C.

        [KTSerialize("c5617d8f-86d7-42d1-95dc-30d1b035f498")]
        class C13 : A13
        {
            #region Properties and fields.

            [KTSerializeInclude("3a5a1af8-4941-49e8-9e13-f4c75fafe3a5")]
            private D13 dField;

            [KTSerializeInclude("15dc7429-d001-4d30-9d92-de3f61252be6")]
            public D13 dProperty
            {
                get;
                set;
            }


            [KTSerializeInclude("7cfa47f5-0e6a-4938-a278-05c99b5e250f")]
            private A13 aField;

            [KTSerializeInclude("a15ad2a8-0d3e-4f0e-b003-38615bcd92ed")]
            public A13 aProperty
            {
                get;
                set;
            }


            [KTSerializeInclude("20092b7a-1506-444c-9fd1-f8ef7509883d")]
            private B13 bField;

            [KTSerializeInclude("18a1b27d-feed-45dd-9e3f-5c761290ba3d")]
            public B13 bProperty
            {
                get;
                set;
            }

            #endregion
        }

        #endregion


        #region D.

        [KTSerialize("b4a46972-25fe-42e1-b67e-a0029f78e43e")]
        class D13 : C13
        {
            #region Properties and fields.

            [KTSerializeInclude("202d7be5-c444-4182-b08b-cceca4d65268")]
            private A13 aField;

            [KTSerializeInclude("e0943237-a9c5-447a-b57f-bfb5945afab8")]
            public A13 aProperty
            {
                get;
                set;
            }

            #endregion
        }

        #endregion

        #endregion


        #region Test with plain properties.

        #region A.

        [KTSerialize("69e70d39-02b2-4525-a554-065728a1b0f6")]
        class APlainProperties
        {
            #region Properties and fields.

            #region Int, Uint.

            [KTSerializeInclude("c13d1ce1-5aec-42e8-9a1b-ba6ead1434d2")]
            public int iField = 0;

            [KTSerializeInclude("650afe40-24ad-4f44-b7cb-e4a8fe0461da")]
            public int iProperty
            {
                get;
                set;
            }


            [KTSerializeInclude("5499430b-cce8-43de-b1cb-12e1b69b027b")]
            public uint uiField = 0;

            [KTSerializeInclude("ff77d98f-ca7f-41fe-809f-535afd15f2c2")]
            public uint uiProperty
            {
                get;
                set;
            }

            #endregion


            #region Int16, UInt16.

            [KTSerializeInclude("9833bb81-8c23-45a9-9a63-1fca694e6484")]
            public Int16 i16Field = 0;

            [KTSerializeInclude("b660ee3d-a1f3-46fa-825c-e3654a665cc6")]
            public Int16 i16Property
            {
                get;
                set;
            }


            [KTSerializeInclude("af8fe343-5333-4821-8135-742f592cd846")]
            public UInt16 ui16Field = 0;

            [KTSerializeInclude("ace6d97e-67dd-4598-a3fb-33edee768ce0")]
            public UInt16 ui16Property
            {
                get;
                set;
            }

            #endregion


            #region Int64, UInt64.

            [KTSerializeInclude("af85b01e-724f-4bd8-a821-f3a39e952b37")]
            public Int64 i64Field = 0;

            [KTSerializeInclude("17791cd1-38e5-4725-960a-a9c706688fcc")]
            public Int64 i64Property
            {
                get;
                set;
            }


            [KTSerializeInclude("06783d94-4688-47e1-b99d-68e2a5902dd0")]
            public UInt64 ui64Field = 0;

            [KTSerializeInclude("b9a24969-0a74-418c-98db-57fbab02de3e")]
            public UInt64 ui64Property
            {
                get;
                set;
            }

            #endregion



            #region IntNull, Uint?Null.

            [KTSerializeInclude("d8d1b0f1-955c-4426-b40e-a4f3b061cf08")]
            public int? iNullField = null;

            [KTSerializeInclude("2fc53941-fc34-4e8f-808a-710cc2980263")]
            public int? iNullProperty
            {
                get;
                set;
            }


            [KTSerializeInclude("1ab768c5-c09b-46c9-9969-67a5139e7002")]
            public uint? uiNullField = null;

            [KTSerializeInclude("337f81bc-e129-48c5-9b4a-a4e0a96946b3")]
            public uint? uiNullProperty
            {
                get;
                set;
            }

            #endregion


            #region Int16?, UInt16?.

            [KTSerializeInclude("6d0d0fc5-4ecc-445d-b161-2f2f07479441")]
            public Int16? i16NullField = null;

            [KTSerializeInclude("cb99f029-91d0-4747-9666-206552aaf027")]
            public Int16? i16NullProperty
            {
                get;
                set;
            }


            [KTSerializeInclude("616916b5-9ba8-4c02-8381-0193f3021c95")]
            public UInt16? ui16NullField = null;

            [KTSerializeInclude("830e72c6-f945-454e-b946-6ab40970bd4f")]
            public UInt16? ui16NullProperty
            {
                get;
                set;
            }

            #endregion


            #region Int64?, UInt64?.

            [KTSerializeInclude("b9430bff-4f10-42d2-b3fd-1ade45e05f15")]
            public Int64? i64NullField = null;

            [KTSerializeInclude("c919d308-e9d0-49db-9457-1247921429c5")]
            public Int64? i64NullProperty
            {
                get;
                set;
            }


            [KTSerializeInclude("3f26197e-f71d-4b2f-b0a4-b0714af76188")]
            public UInt64? ui64NullField = null;

            [KTSerializeInclude("42289ec3-ad9d-4c5c-a103-3a062781435a")]
            public UInt64? ui64NullProperty
            {
                get;
                set;
            }

            #endregion



            #region String.

            [KTSerializeInclude("5b2ea5ff-e6d8-402b-b518-e12d314b7035")]
            public string stringField;

            [KTSerializeInclude("fee17aef-12d5-4206-8861-3640efc724cc")]
            public string stringProperty
            {
                get;
                set;
            }

            #endregion


            #region Char.

            [KTSerializeInclude("b6157981-5995-4b66-9502-55d707624992")]
            public char charField;

            [KTSerializeInclude("713f3196-0028-41d3-a490-69bad5dc03bd")]
            public char charProperty
            {
                get;
                set;
            }

            #endregion


            #region Byte.

            [KTSerializeInclude("a1d666e8-33b5-49be-8b2f-5485a71664e6")]
            public byte byteField = 0;

            [KTSerializeInclude("fb19feb4-9e0e-4781-a911-b10c78fa2ad7")]
            public byte byteProperty
            {
                get;
                set;
            }


            [KTSerializeInclude("8dd61d28-5f64-4f98-9d1f-8f79a9d11ae7")]
            public byte? byteNullField = null;

            [KTSerializeInclude("bec679c6-c6bf-4d08-9f1b-18a2a45ed176")]
            public byte? byteNullProperty
            {
                get;
                set;
            }

            #endregion


            #region Color.

            [KTSerializeInclude("269e4e81-2b6a-4efd-b96a-7239edd0917e")]
            public Color colorField;

            [KTSerializeInclude("760006c3-f532-4529-8be8-54263387e062")]
            public Color colorProperty
            {
                get;
                set;
            }


            [KTSerializeInclude("3439cc31-abcb-4472-ae0b-e9cc92d45e1f")]
            public Color? colorNullField;

            [KTSerializeInclude("078855af-a766-447a-bdc5-bc0896a2885b")]
            public Color? colorNullProperty
            {
                get;
                set;
            }

            #endregion



            #region Float.

            [KTSerializeInclude("5493f028-cb12-4674-8886-0a7cb9628ee5")]
            public float floatField = 0;

            [KTSerializeInclude("c3a9ec2e-6ae7-4e67-9c4b-78dec9ba1e09")]
            public float floatProperty
            {
                get;
                set;
            }


            [KTSerializeInclude("5f951e65-b99b-4393-80da-fe61fbe47f90")]
            public float? floatNullField = null;

            [KTSerializeInclude("2b5b10cc-0e28-4de7-8cac-d0c02b3d8758")]
            public float? floatNullProperty
            {
                get;
                set;
            }

            #endregion


            #region Double.

            [KTSerializeInclude("eea1f963-6443-47af-b05b-7c2dc4ce1d29")]
            public double doubleField = 0;

            [KTSerializeInclude("5a37a1a3-7066-4a4d-a8d7-833b4f21b8e1")]
            public double doubleProperty
            {
                get;
                set;
            }


            [KTSerializeInclude("d2dbc189-f5f4-416c-b982-436d954a95e5")]
            public double? doubleNullField = null;

            [KTSerializeInclude("822fa5ad-3990-468a-89b6-f4260ed4cbfa")]
            public double? doubleNullProperty
            {
                get;
                set;
            }

            #endregion


            #region Decimal.

            [KTSerializeInclude("4ff877f8-3bbe-4be8-ab83-900df65bd482")]
            public decimal decimalField = 0;

            [KTSerializeInclude("76e335b9-77c3-46e6-818e-bf910acadabe")]
            public decimal decimalProperty
            {
                get;
                set;
            }


            [KTSerializeInclude("e70a9c97-f249-4977-84db-4abc8c1a37bb")]
            public decimal? decimalNullField = null;

            [KTSerializeInclude("43d67c29-7f01-4f43-ab49-5ee68be9fae3")]
            public decimal? decimalNullProperty
            {
                get;
                set;
            }

            #endregion



            #region Guid.

            [KTSerializeInclude("d9c0aca7-0cb3-40e1-8304-9da33f240748")]
            public Guid guidField;

            [KTSerializeInclude("bdf0fbf3-7568-47bd-9441-3e12597c6eda")]
            public Guid guidProperty
            {
                get;
                set;
            }


            [KTSerializeInclude("1d4454ec-5ae8-4991-bad6-be27dba5982e")]
            public Guid? guidNullField;

            [KTSerializeInclude("28cd1e84-31e8-4ba6-acba-b13c830924a8")]
            public Guid? guidNullProperty
            {
                get;
                set;
            }

            #endregion


            #region Bool.

            [KTSerializeInclude("ea824fd7-5f90-48b7-9340-3e65764c51d4")]
            public bool boolField = false;

            [KTSerializeInclude("eea1de6a-c181-486a-bb42-0ad53a13acb7")]
            public bool boolProperty
            {
                get;
                set;
            }


            [KTSerializeInclude("ab50fbdc-2a83-4499-b942-58c2ae3c6d01")]
            public bool? boolNullField = null;

            [KTSerializeInclude("28d22899-df51-4668-a6b1-37cba1f4c814")]
            public bool? boolNullProperty
            {
                get;
                set;
            }

            #endregion


            #region DateTime.

            [KTSerializeInclude("a52052da-dc2e-4fe7-a5da-234ab9472660")]
            public DateTime dateTimeField;

            [KTSerializeInclude("7f8f5a2c-fa3a-41e1-a638-cc6535607215")]
            public DateTime dateTimeProperty
            {
                get;
                set;
            }


            [KTSerializeInclude("54072844-a55b-4e23-bcf1-99b90c159f97")]
            public DateTime? dateTimeNullField;

            [KTSerializeInclude("4e5480f5-37e7-4e0f-8d57-b4098f34b66d")]
            public DateTime? dateTimeNullProperty
            {
                get;
                set;
            }

            #endregion


            #region TimeSpan.

            [KTSerializeInclude("1d3114e5-4850-4e5c-b233-050d4f523f91")]
            public TimeSpan timeSpanField;

            [KTSerializeInclude("7394f5f2-1800-4251-b40c-658ce1e796f8")]
            public TimeSpan timeSpanProperty
            {
                get;
                set;
            }


            [KTSerializeInclude("0478fd81-59fc-4f47-a357-53b24f571518")]
            public TimeSpan? timeSpanNullField;

            [KTSerializeInclude("d2a893c7-b52f-4153-b155-b740baf9ab9f")]
            public TimeSpan? timeSpanNullProperty
            {
                get;
                set;
            }

            #endregion


            #region Enum.

            [KTSerializeInclude("0bdb91e7-6c8c-4f28-a01a-907e57dfc592")]
            public Enum21 enumField;

            [KTSerializeInclude("38cac7c4-a02f-4d30-b831-b50695f2cd74")]
            public Enum21 enumProperty
            {
                get;
                set;
            }


            [KTSerializeInclude("6c6cfa97-b529-4db8-ab21-d4316e8d1ad5")]
            public Enum21? enumNullField;

            [KTSerializeInclude("93ae8d42-66b6-48b2-af2e-0962e07dbe8e")]
            public Enum21? enumNullProperty
            {
                get;
                set;
            }

            #endregion

            #endregion
        }

        #endregion


        #region B.

        [KTSerialize("23ff4f30-3753-4376-9c7d-e63bd9b9828c")]
        class BPlainProperties
        {
            #region Properties and fields.

            [KTSerializeInclude("307a91bb-da63-444b-9a20-c837d0c8c435")]
            public int iField = 0;

            [KTSerializeInclude("314ab790-6136-4fc2-802c-03d6728efe20")]
            public int iProperty
            {
                get;
                set;
            }


            [KTSerializeInclude("455c5cba-a5c8-4f74-901e-ba570511a139")]
            public APlainProperties aField;

            [KTSerializeInclude("0e535c6e-2e74-4eea-8937-2f062ed9d7df")]
            public APlainProperties aProperty
            {
                get;
                set;
            }

            #endregion
        }

        #endregion


        #region C.

        [KTSerialize("811f4959-8fc8-45b6-8537-4b36680b7a2e")]
        class CPlainProperties : BPlainProperties
        {
            #region Properties and fields.

            [KTSerializeInclude("3ca1b592-3e07-4397-a07c-d80d0b405ba4")]
            public APlainProperties aField;

            [KTSerializeInclude("d813fb7b-44ee-4277-b375-ae4a30ed8621")]
            public APlainProperties aProperty
            {
                get;
                set;
            }


            [KTSerializeInclude("89cb6d4b-4522-4a1e-95a8-ca2d76109a9d")]
            public BPlainProperties bField;

            [KTSerializeInclude("6c9a11a4-c7e3-4441-ba11-354ce29fc73f")]
            public BPlainProperties bProperty
            {
                get;
                set;
            }

            #endregion
        }

        #endregion




        #region AStruct.

        [KTSerialize("69e70d39-02b2-4525-a554-065728a1b0f6")]
        struct AStructPlainProperties
        {
            #region Properties and fields.

            #region Int, Uint.

            [KTSerializeInclude("c13d1ce1-5aec-42e8-9a1b-ba6ead1434d2")]
            public int iField;

            [KTSerializeInclude("650afe40-24ad-4f44-b7cb-e4a8fe0461da")]
            public int iProperty
            {
                get;
                set;
            }


            [KTSerializeInclude("5499430b-cce8-43de-b1cb-12e1b69b027b")]
            public uint uiField;

            [KTSerializeInclude("ff77d98f-ca7f-41fe-809f-535afd15f2c2")]
            public uint uiProperty
            {
                get;
                set;
            }

            #endregion


            #region Int16, UInt16.

            [KTSerializeInclude("9833bb81-8c23-45a9-9a63-1fca694e6484")]
            public Int16 i16Field;

            [KTSerializeInclude("b660ee3d-a1f3-46fa-825c-e3654a665cc6")]
            public Int16 i16Property
            {
                get;
                set;
            }


            [KTSerializeInclude("af8fe343-5333-4821-8135-742f592cd846")]
            public UInt16 ui16Field;

            [KTSerializeInclude("ace6d97e-67dd-4598-a3fb-33edee768ce0")]
            public UInt16 ui16Property
            {
                get;
                set;
            }

            #endregion


            #region Int64, UInt64.

            [KTSerializeInclude("af85b01e-724f-4bd8-a821-f3a39e952b37")]
            public Int64 i64Field;

            [KTSerializeInclude("17791cd1-38e5-4725-960a-a9c706688fcc")]
            public Int64 i64Property
            {
                get;
                set;
            }


            [KTSerializeInclude("06783d94-4688-47e1-b99d-68e2a5902dd0")]
            public UInt64 ui64Field;

            [KTSerializeInclude("b9a24969-0a74-418c-98db-57fbab02de3e")]
            public UInt64 ui64Property
            {
                get;
                set;
            }

            #endregion



            #region IntNull, Uint?Null.

            [KTSerializeInclude("d8d1b0f1-955c-4426-b40e-a4f3b061cf08")]
            public int? iNullField;

            [KTSerializeInclude("2fc53941-fc34-4e8f-808a-710cc2980263")]
            public int? iNullProperty
            {
                get;
                set;
            }


            [KTSerializeInclude("1ab768c5-c09b-46c9-9969-67a5139e7002")]
            public uint? uiNullField;

            [KTSerializeInclude("337f81bc-e129-48c5-9b4a-a4e0a96946b3")]
            public uint? uiNullProperty
            {
                get;
                set;
            }

            #endregion


            #region Int16?, UInt16?.

            [KTSerializeInclude("6d0d0fc5-4ecc-445d-b161-2f2f07479441")]
            public Int16? i16NullField;

            [KTSerializeInclude("cb99f029-91d0-4747-9666-206552aaf027")]
            public Int16? i16NullProperty
            {
                get;
                set;
            }


            [KTSerializeInclude("616916b5-9ba8-4c02-8381-0193f3021c95")]
            public UInt16? ui16NullField;

            [KTSerializeInclude("830e72c6-f945-454e-b946-6ab40970bd4f")]
            public UInt16? ui16NullProperty
            {
                get;
                set;
            }

            #endregion


            #region Int64?, UInt64?.

            [KTSerializeInclude("b9430bff-4f10-42d2-b3fd-1ade45e05f15")]
            public Int64? i64NullField;

            [KTSerializeInclude("c919d308-e9d0-49db-9457-1247921429c5")]
            public Int64? i64NullProperty
            {
                get;
                set;
            }


            [KTSerializeInclude("3f26197e-f71d-4b2f-b0a4-b0714af76188")]
            public UInt64? ui64NullField;

            [KTSerializeInclude("42289ec3-ad9d-4c5c-a103-3a062781435a")]
            public UInt64? ui64NullProperty
            {
                get;
                set;
            }

            #endregion



            #region String.

            [KTSerializeInclude("5b2ea5ff-e6d8-402b-b518-e12d314b7035")]
            public string stringField;

            [KTSerializeInclude("fee17aef-12d5-4206-8861-3640efc724cc")]
            public string stringProperty
            {
                get;
                set;
            }

            #endregion


            #region Char.

            [KTSerializeInclude("b6157981-5995-4b66-9502-55d707624992")]
            public char charField;

            [KTSerializeInclude("713f3196-0028-41d3-a490-69bad5dc03bd")]
            public char charProperty
            {
                get;
                set;
            }

            #endregion


            #region Byte.

            [KTSerializeInclude("a1d666e8-33b5-49be-8b2f-5485a71664e6")]
            public byte byteField;

            [KTSerializeInclude("fb19feb4-9e0e-4781-a911-b10c78fa2ad7")]
            public byte byteProperty
            {
                get;
                set;
            }


            [KTSerializeInclude("8dd61d28-5f64-4f98-9d1f-8f79a9d11ae7")]
            public byte? byteNullField;

            [KTSerializeInclude("bec679c6-c6bf-4d08-9f1b-18a2a45ed176")]
            public byte? byteNullProperty
            {
                get;
                set;
            }

            #endregion


            #region Color.

            [KTSerializeInclude("269e4e81-2b6a-4efd-b96a-7239edd0917e")]
            public Color colorField;

            [KTSerializeInclude("760006c3-f532-4529-8be8-54263387e062")]
            public Color colorProperty
            {
                get;
                set;
            }


            [KTSerializeInclude("3439cc31-abcb-4472-ae0b-e9cc92d45e1f")]
            public Color? colorNullField;

            [KTSerializeInclude("078855af-a766-447a-bdc5-bc0896a2885b")]
            public Color? colorNullProperty
            {
                get;
                set;
            }

            #endregion



            #region Float.

            [KTSerializeInclude("5493f028-cb12-4674-8886-0a7cb9628ee5")]
            public float floatField;

            [KTSerializeInclude("c3a9ec2e-6ae7-4e67-9c4b-78dec9ba1e09")]
            public float floatProperty
            {
                get;
                set;
            }


            [KTSerializeInclude("5f951e65-b99b-4393-80da-fe61fbe47f90")]
            public float? floatNullField;

            [KTSerializeInclude("2b5b10cc-0e28-4de7-8cac-d0c02b3d8758")]
            public float? floatNullProperty
            {
                get;
                set;
            }

            #endregion


            #region Double.

            [KTSerializeInclude("eea1f963-6443-47af-b05b-7c2dc4ce1d29")]
            public double doubleField;

            [KTSerializeInclude("5a37a1a3-7066-4a4d-a8d7-833b4f21b8e1")]
            public double doubleProperty
            {
                get;
                set;
            }


            [KTSerializeInclude("d2dbc189-f5f4-416c-b982-436d954a95e5")]
            public double? doubleNullField;

            [KTSerializeInclude("822fa5ad-3990-468a-89b6-f4260ed4cbfa")]
            public double? doubleNullProperty
            {
                get;
                set;
            }

            #endregion


            #region Decimal.

            [KTSerializeInclude("4ff877f8-3bbe-4be8-ab83-900df65bd482")]
            public decimal decimalField;

            [KTSerializeInclude("76e335b9-77c3-46e6-818e-bf910acadabe")]
            public decimal decimalProperty
            {
                get;
                set;
            }


            [KTSerializeInclude("e70a9c97-f249-4977-84db-4abc8c1a37bb")]
            public decimal? decimalNullField;

            [KTSerializeInclude("43d67c29-7f01-4f43-ab49-5ee68be9fae3")]
            public decimal? decimalNullProperty
            {
                get;
                set;
            }

            #endregion



            #region Guid.

            [KTSerializeInclude("d9c0aca7-0cb3-40e1-8304-9da33f240748")]
            public Guid guidField;

            [KTSerializeInclude("bdf0fbf3-7568-47bd-9441-3e12597c6eda")]
            public Guid guidProperty
            {
                get;
                set;
            }


            [KTSerializeInclude("1d4454ec-5ae8-4991-bad6-be27dba5982e")]
            public Guid? guidNullField;

            [KTSerializeInclude("28cd1e84-31e8-4ba6-acba-b13c830924a8")]
            public Guid? guidNullProperty
            {
                get;
                set;
            }

            #endregion


            #region Bool.

            [KTSerializeInclude("ea824fd7-5f90-48b7-9340-3e65764c51d4")]
            public bool boolField;

            [KTSerializeInclude("eea1de6a-c181-486a-bb42-0ad53a13acb7")]
            public bool boolProperty
            {
                get;
                set;
            }


            [KTSerializeInclude("ab50fbdc-2a83-4499-b942-58c2ae3c6d01")]
            public bool? boolNullField;

            [KTSerializeInclude("28d22899-df51-4668-a6b1-37cba1f4c814")]
            public bool? boolNullProperty
            {
                get;
                set;
            }

            #endregion


            #region DateTime.

            [KTSerializeInclude("a52052da-dc2e-4fe7-a5da-234ab9472660")]
            public DateTime dateTimeField;

            [KTSerializeInclude("7f8f5a2c-fa3a-41e1-a638-cc6535607215")]
            public DateTime dateTimeProperty
            {
                get;
                set;
            }


            [KTSerializeInclude("54072844-a55b-4e23-bcf1-99b90c159f97")]
            public DateTime? dateTimeNullField;

            [KTSerializeInclude("4e5480f5-37e7-4e0f-8d57-b4098f34b66d")]
            public DateTime? dateTimeNullProperty
            {
                get;
                set;
            }

            #endregion


            #region TimeSpan.

            [KTSerializeInclude("1d3114e5-4850-4e5c-b233-050d4f523f91")]
            public TimeSpan timeSpanField;

            [KTSerializeInclude("7394f5f2-1800-4251-b40c-658ce1e796f8")]
            public TimeSpan timeSpanProperty
            {
                get;
                set;
            }


            [KTSerializeInclude("0478fd81-59fc-4f47-a357-53b24f571518")]
            public TimeSpan? timeSpanNullField;

            [KTSerializeInclude("d2a893c7-b52f-4153-b155-b740baf9ab9f")]
            public TimeSpan? timeSpanNullProperty
            {
                get;
                set;
            }

            #endregion


            #region Enum.

            [KTSerializeInclude("0bdb91e7-6c8c-4f28-a01a-907e57dfc592")]
            public Enum21 enumField;

            [KTSerializeInclude("38cac7c4-a02f-4d30-b831-b50695f2cd74")]
            public Enum21 enumProperty
            {
                get;
                set;
            }


            [KTSerializeInclude("6c6cfa97-b529-4db8-ab21-d4316e8d1ad5")]
            public Enum21? enumNullField;

            [KTSerializeInclude("93ae8d42-66b6-48b2-af2e-0962e07dbe8e")]
            public Enum21? enumNullProperty
            {
                get;
                set;
            }

            #endregion

            #endregion
        }

        #endregion


        #region BStruct.

        [KTSerialize("23ff4f30-3753-4376-9c7d-e63bd9b9828c")]
        struct BStructPlainProperties
        {
            #region Properties and fields.

            [KTSerializeInclude("307a91bb-da63-444b-9a20-c837d0c8c435")]
            public int iField;

            [KTSerializeInclude("314ab790-6136-4fc2-802c-03d6728efe20")]
            public int iProperty
            {
                get;
                set;
            }


            [KTSerializeInclude("455c5cba-a5c8-4f74-901e-ba570511a139")]
            public AStructPlainProperties aField;

            [KTSerializeInclude("0e535c6e-2e74-4eea-8937-2f062ed9d7df")]
            public AStructPlainProperties? aProperty
            {
                get;
                set;
            }

            #endregion
        }

        #endregion



        #region A with preset values.

        [KTSerialize("24d7a996-e66a-4445-ae3d-44f3d32d9e51")]
        class APlainPropertiesPreset : APlainProperties
        {
            #region Constructors.

            public APlainPropertiesPreset()
            {
                // Define variables.
                DateTime dateTime = DateTime.Now;
                TimeSpan timeSpanValue = new TimeSpan(18, 5, 37, 1658);
                Guid guidValue = Guid.NewGuid();
                string stringValue = "1";
                char charValue = 'a';
                Color colorValue = ColorTranslator.FromHtml("#f0ffff"); //Color.Azure

                int intValue = 1;
                uint uintValue = 1;

                short int16Value = 1;
                ushort uint16Value = 1;

                long int64Value = 1;
                ulong uint64Value = 1;

                byte byteValue = 1;
                float floatValue = 1F;
                double doubleValue = 1;
                decimal decimalValue = 1M;
                Enum21 enumValue = Enum21.Value1;


                // Not null values.
                boolField = true;
                boolProperty = true;

                dateTimeField = dateTime;
                dateTimeProperty = dateTime;

                timeSpanField = timeSpanValue;
                timeSpanProperty = timeSpanValue;

                guidField = guidValue;
                guidProperty = guidValue;


                iField = intValue;
                iProperty = intValue;

                uiField = uintValue;
                uiProperty = uintValue;


                i16Field = int16Value;
                i16Property = int16Value;

                ui16Field = uint16Value;
                ui16Property = uint16Value;


                i64Field = int64Value;
                i64Property = int64Value;

                ui64Field = uint64Value;
                ui64Property = uint64Value;


                stringField = stringValue;
                stringProperty = stringValue;

                charField = charValue;
                charProperty = charValue;

                colorField = colorValue;
                colorProperty = colorValue;

                byteField = byteValue;
                byteProperty = byteValue;

                floatField = floatValue;
                floatProperty = floatValue;

                doubleField = doubleValue;
                doubleProperty = doubleValue;

                decimalField = decimalValue;
                decimalProperty = decimalValue;

                this.enumField = enumValue;
                this.enumProperty = enumValue;


                // Null values.
                this.boolNullField = true;
                this.boolNullProperty = true;

                this.dateTimeNullField = dateTime;
                this.dateTimeNullProperty = dateTime;

                this.timeSpanNullField = timeSpanValue;
                this.timeSpanNullProperty = timeSpanValue;

                this.guidNullField = guidValue;
                this.guidNullProperty = guidValue;


                this.iNullField = intValue;
                this.iNullProperty = intValue;

                this.uiNullField = uintValue;
                this.uiNullProperty = uintValue;


                this.i16NullField = int16Value;
                this.i16NullProperty = int16Value;

                this.ui16NullField = uint16Value;
                this.ui16NullProperty = uint16Value;


                this.i64NullField = int64Value;
                this.i64NullProperty = int64Value;

                this.ui64NullField = uint64Value;
                this.ui64NullProperty = uint64Value;


                this.colorNullField = colorValue;
                this.colorNullProperty = colorValue;

                this.byteNullField = byteValue;
                this.byteNullProperty = byteValue;

                this.floatNullField = floatValue;
                this.floatNullProperty = floatValue;

                this.doubleNullField = doubleValue;
                this.doubleNullProperty = doubleValue;

                this.decimalNullField = decimalValue;
                this.decimalNullProperty = decimalValue;

                this.enumNullField = enumValue;
                this.enumNullProperty = enumValue;

            }

            #endregion
        }

        #endregion



        #region A, object type.

        [KTSerialize("69e70d39-02b2-4525-a554-065728a1b0f6")]
        class AObjectPlainProperties
        {
            #region Properties and fields.

            #region Int, Uint.

            [KTSerializeInclude("c13d1ce1-5aec-42e8-9a1b-ba6ead1434d2")]
            public object iField = 0;

            [KTSerializeInclude("650afe40-24ad-4f44-b7cb-e4a8fe0461da")]
            public object iProperty
            {
                get;
                set;
            }


            [KTSerializeInclude("5499430b-cce8-43de-b1cb-12e1b69b027b")]
            public object uiField = 0;

            [KTSerializeInclude("ff77d98f-ca7f-41fe-809f-535afd15f2c2")]
            public object uiProperty
            {
                get;
                set;
            }

            #endregion


            #region Int16, UInt16.

            [KTSerializeInclude("9833bb81-8c23-45a9-9a63-1fca694e6484")]
            public object i16Field;

            [KTSerializeInclude("b660ee3d-a1f3-46fa-825c-e3654a665cc6")]
            public object i16Property
            {
                get;
                set;
            }


            [KTSerializeInclude("af8fe343-5333-4821-8135-742f592cd846")]
            public object ui16Field;

            [KTSerializeInclude("ace6d97e-67dd-4598-a3fb-33edee768ce0")]
            public object ui16Property
            {
                get;
                set;
            }

            #endregion


            #region Int64, UInt64.

            [KTSerializeInclude("af85b01e-724f-4bd8-a821-f3a39e952b37")]
            public object i64Field;

            [KTSerializeInclude("17791cd1-38e5-4725-960a-a9c706688fcc")]
            public object i64Property
            {
                get;
                set;
            }


            [KTSerializeInclude("06783d94-4688-47e1-b99d-68e2a5902dd0")]
            public object ui64Field;

            [KTSerializeInclude("b9a24969-0a74-418c-98db-57fbab02de3e")]
            public object ui64Property
            {
                get;
                set;
            }

            #endregion



            #region IntNull, Uint?Null.

            [KTSerializeInclude("d8d1b0f1-955c-4426-b40e-a4f3b061cf08")]
            public object iNullField;

            [KTSerializeInclude("2fc53941-fc34-4e8f-808a-710cc2980263")]
            public object iNullProperty
            {
                get;
                set;
            }


            [KTSerializeInclude("1ab768c5-c09b-46c9-9969-67a5139e7002")]
            public object uiNullField;

            [KTSerializeInclude("337f81bc-e129-48c5-9b4a-a4e0a96946b3")]
            public object uiNullProperty
            {
                get;
                set;
            }

            #endregion


            #region Int16?, UInt16?.

            [KTSerializeInclude("6d0d0fc5-4ecc-445d-b161-2f2f07479441")]
            public object i16NullField = null;

            [KTSerializeInclude("cb99f029-91d0-4747-9666-206552aaf027")]
            public object i16NullProperty
            {
                get;
                set;
            }


            [KTSerializeInclude("616916b5-9ba8-4c02-8381-0193f3021c95")]
            public object ui16NullField = null;

            [KTSerializeInclude("830e72c6-f945-454e-b946-6ab40970bd4f")]
            public object ui16NullProperty
            {
                get;
                set;
            }

            #endregion


            #region Int64?, UInt64?.

            [KTSerializeInclude("b9430bff-4f10-42d2-b3fd-1ade45e05f15")]
            public object i64NullField = null;

            [KTSerializeInclude("c919d308-e9d0-49db-9457-1247921429c5")]
            public object i64NullProperty
            {
                get;
                set;
            }


            [KTSerializeInclude("3f26197e-f71d-4b2f-b0a4-b0714af76188")]
            public object ui64NullField = null;

            [KTSerializeInclude("42289ec3-ad9d-4c5c-a103-3a062781435a")]
            public object ui64NullProperty
            {
                get;
                set;
            }

            #endregion



            #region String.

            [KTSerializeInclude("5b2ea5ff-e6d8-402b-b518-e12d314b7035")]
            public object stringField;

            [KTSerializeInclude("fee17aef-12d5-4206-8861-3640efc724cc")]
            public object stringProperty
            {
                get;
                set;
            }

            #endregion


            #region Char.

            [KTSerializeInclude("b6157981-5995-4b66-9502-55d707624992")]
            public object charField;

            [KTSerializeInclude("713f3196-0028-41d3-a490-69bad5dc03bd")]
            public object charProperty
            {
                get;
                set;
            }

            #endregion


            #region Byte.

            [KTSerializeInclude("a1d666e8-33b5-49be-8b2f-5485a71664e6")]
            public object byteField = 0;

            [KTSerializeInclude("fb19feb4-9e0e-4781-a911-b10c78fa2ad7")]
            public object byteProperty
            {
                get;
                set;
            }


            [KTSerializeInclude("8dd61d28-5f64-4f98-9d1f-8f79a9d11ae7")]
            public object byteNullField = null;

            [KTSerializeInclude("bec679c6-c6bf-4d08-9f1b-18a2a45ed176")]
            public object byteNullProperty
            {
                get;
                set;
            }

            #endregion


            #region Color.

            [KTSerializeInclude("269e4e81-2b6a-4efd-b96a-7239edd0917e")]
            public object colorField = null;

            [KTSerializeInclude("760006c3-f532-4529-8be8-54263387e062")]
            public object colorProperty
            {
                get;
                set;
            }


            [KTSerializeInclude("3439cc31-abcb-4472-ae0b-e9cc92d45e1f")]
            public object colorNullField = null;

            [KTSerializeInclude("078855af-a766-447a-bdc5-bc0896a2885b")]
            public object colorNullProperty
            {
                get;
                set;
            }

            #endregion



            #region Float.

            [KTSerializeInclude("5493f028-cb12-4674-8886-0a7cb9628ee5")]
            public object floatField = 0;

            [KTSerializeInclude("c3a9ec2e-6ae7-4e67-9c4b-78dec9ba1e09")]
            public object floatProperty
            {
                get;
                set;
            }


            [KTSerializeInclude("5f951e65-b99b-4393-80da-fe61fbe47f90")]
            public object floatNullField = null;

            [KTSerializeInclude("2b5b10cc-0e28-4de7-8cac-d0c02b3d8758")]
            public object floatNullProperty
            {
                get;
                set;
            }

            #endregion


            #region Double.

            [KTSerializeInclude("eea1f963-6443-47af-b05b-7c2dc4ce1d29")]
            public object doubleField = 0;

            [KTSerializeInclude("5a37a1a3-7066-4a4d-a8d7-833b4f21b8e1")]
            public object doubleProperty
            {
                get;
                set;
            }


            [KTSerializeInclude("d2dbc189-f5f4-416c-b982-436d954a95e5")]
            public object doubleNullField = null;

            [KTSerializeInclude("822fa5ad-3990-468a-89b6-f4260ed4cbfa")]
            public object doubleNullProperty
            {
                get;
                set;
            }

            #endregion


            #region Decimal.

            [KTSerializeInclude("4ff877f8-3bbe-4be8-ab83-900df65bd482")]
            public object decimalField = 0;

            [KTSerializeInclude("76e335b9-77c3-46e6-818e-bf910acadabe")]
            public object decimalProperty
            {
                get;
                set;
            }


            [KTSerializeInclude("e70a9c97-f249-4977-84db-4abc8c1a37bb")]
            public object decimalNullField = null;

            [KTSerializeInclude("43d67c29-7f01-4f43-ab49-5ee68be9fae3")]
            public object decimalNullProperty
            {
                get;
                set;
            }

            #endregion



            #region Guid.

            [KTSerializeInclude("d9c0aca7-0cb3-40e1-8304-9da33f240748")]
            public object guidField;

            [KTSerializeInclude("bdf0fbf3-7568-47bd-9441-3e12597c6eda")]
            public object guidProperty
            {
                get;
                set;
            }


            [KTSerializeInclude("1d4454ec-5ae8-4991-bad6-be27dba5982e")]
            public object guidNullField;

            [KTSerializeInclude("28cd1e84-31e8-4ba6-acba-b13c830924a8")]
            public object guidNullProperty
            {
                get;
                set;
            }

            #endregion


            #region Bool.

            [KTSerializeInclude("ea824fd7-5f90-48b7-9340-3e65764c51d4")]
            public object boolField = false;

            [KTSerializeInclude("eea1de6a-c181-486a-bb42-0ad53a13acb7")]
            public object boolProperty
            {
                get;
                set;
            }


            [KTSerializeInclude("ab50fbdc-2a83-4499-b942-58c2ae3c6d01")]
            public object boolNullField = null;

            [KTSerializeInclude("28d22899-df51-4668-a6b1-37cba1f4c814")]
            public object boolNullProperty
            {
                get;
                set;
            }

            #endregion


            #region DateTime.

            [KTSerializeInclude("a52052da-dc2e-4fe7-a5da-234ab9472660")]
            public object dateTimeField;

            [KTSerializeInclude("7f8f5a2c-fa3a-41e1-a638-cc6535607215")]
            public object dateTimeProperty
            {
                get;
                set;
            }


            [KTSerializeInclude("54072844-a55b-4e23-bcf1-99b90c159f97")]
            public object dateTimeNullField;

            [KTSerializeInclude("4e5480f5-37e7-4e0f-8d57-b4098f34b66d")]
            public object dateTimeNullProperty
            {
                get;
                set;
            }

            #endregion


            #region TimeSpan.

            [KTSerializeInclude("1d3114e5-4850-4e5c-b233-050d4f523f91")]
            public object timeSpanField;

            [KTSerializeInclude("7394f5f2-1800-4251-b40c-658ce1e796f8")]
            public object timeSpanProperty
            {
                get;
                set;
            }


            [KTSerializeInclude("0478fd81-59fc-4f47-a357-53b24f571518")]
            public object timeSpanNullField;

            [KTSerializeInclude("d2a893c7-b52f-4153-b155-b740baf9ab9f")]
            public object timeSpanNullProperty
            {
                get;
                set;
            }

            #endregion


            #region Enum.

            [KTSerializeInclude("0bdb91e7-6c8c-4f28-a01a-907e57dfc592")]
            public object enumField;

            [KTSerializeInclude("38cac7c4-a02f-4d30-b831-b50695f2cd74")]
            public object enumProperty
            {
                get;
                set;
            }


            [KTSerializeInclude("6c6cfa97-b529-4db8-ab21-d4316e8d1ad5")]
            public object enumNullField;

            [KTSerializeInclude("93ae8d42-66b6-48b2-af2e-0962e07dbe8e")]
            public object enumNullProperty
            {
                get;
                set;
            }

            #endregion

            #endregion
        }

        #endregion


        #region B, object type.

        [KTSerialize("23ff4f30-3753-4376-9c7d-e63bd9b9828c")]
        class BObjectPlainProperties
        {
            #region Properties and fields.

            [KTSerializeInclude("307a91bb-da63-444b-9a20-c837d0c8c435")]
            public int iField = 0;

            [KTSerializeInclude("314ab790-6136-4fc2-802c-03d6728efe20")]
            public int iProperty
            {
                get;
                set;
            }


            [KTSerializeInclude("455c5cba-a5c8-4f74-901e-ba570511a139")]
            public AObjectPlainProperties aField;

            [KTSerializeInclude("0e535c6e-2e74-4eea-8937-2f062ed9d7df")]
            public AObjectPlainProperties aProperty
            {
                get;
                set;
            }

            #endregion
        }

        #endregion

        #endregion


        #region Test inheritance.

        #region A.

        [KTSerialize("b3e8367f-3cf5-401f-966e-e5f149232800")]
        class A14
        {
            #region Properties and fields.

            [KTSerializeInclude("c13d1ce1-5aec-42e8-9a1b-ba6ead1434d2")]
            public int iField = 0;

            [KTSerializeInclude("650afe40-24ad-4f44-b7cb-e4a8fe0461da")]
            public int iProperty
            {
                get;
                set;
            }


            [KTSerializeInclude("c3d3c6f6-713f-4c0e-b48c-7da5f64938c5")]
            public string sField;

            [KTSerializeInclude("362cf5b9-c456-4063-bd57-e788dc6e87f8")]
            public string sProperty
            {
                get;
                set;
            }

            #endregion
        }

        #endregion


        #region B.

        [KTSerialize("2ee0ef7e-c486-471b-8c11-4d677e6851c7")]
        class B14
        {
            #region Properties and fields.

            [KTSerializeInclude("307a91bb-da63-444b-9a20-c837d0c8c435")]
            public int iField = 0;

            [KTSerializeInclude("314ab790-6136-4fc2-802c-03d6728efe20")]
            public int iProperty
            {
                get;
                set;
            }


            [KTSerializeInclude("455c5cba-a5c8-4f74-901e-ba570511a139")]
            public A14 aField;

            [KTSerializeInclude("0e535c6e-2e74-4eea-8937-2f062ed9d7df")]
            public A14 aProperty
            {
                get;
                set;
            }


            [KTSerializeInclude("edb2df99-bcc1-474f-98b3-db66d274bac4")]
            public virtual A14 aProperty2
            {
                get;
                set;
            }

            #endregion
        }

        #endregion


        #region C.

        [KTSerialize("86f25d4a-1c9b-4a92-a4c8-50258ed5e767")]
        class C14 : B14
        {
            #region Properties and fields.

            [KTSerializeInclude("3ca1b592-3e07-4397-a07c-d80d0b405ba4")]
            public A14 aField;

            [KTSerializeInclude("d813fb7b-44ee-4277-b375-ae4a30ed8621")]
            public A14 aProperty
            {
                get;
                set;
            }


            [KTSerializeInclude("350f9ce7-76da-481e-840e-89e188b8360d")]
            public override A14 aProperty2
            {
                get;
                set;
            }


            [KTSerializeInclude("89cb6d4b-4522-4a1e-95a8-ca2d76109a9d")]
            public B14 bField;

            [KTSerializeInclude("6c9a11a4-c7e3-4441-ba11-354ce29fc73f")]
            public B14 bProperty
            {
                get;
                set;
            }


            [KTSerializeInclude("77d4c514-6e49-45aa-bac4-69d4f68837f1")]
            public D14 dField;

            [KTSerializeInclude("0abfcfa5-e1f1-4a77-b33b-494b69bc6605")]
            public D14 dProperty
            {
                get;
                set;
            }


            [KTSerializeInclude("d6832756-a584-404a-be94-db6157ad4b72")]
            public E14 eField;

            [KTSerializeInclude("db1ae86f-adce-400e-8025-23f9ca9068eb")]
            public E14 eProperty
            {
                get;
                set;
            }

            #endregion
        }

        #endregion


        #region D.

        [KTSerialize("2df79de7-b5bc-4399-9597-d79b36b0a0d4")]
        class D14
        {
        }

        #endregion


        #region E.

        // Is not serializable.
        class E14
        {
            #region Constructors.

            public E14()
            { }

            #endregion
        }

        #endregion



        #region X (analog of C14).

        [KTSerialize("04eae475-4076-4f81-b280-c777a92471d3")]
        class X14
        {
            [KTSerializeInclude("d813fb7b-44ee-4277-b375-ae4a30ed8621")]
            public Y14 aPropertyNew
            {
                get;
                set;
            }

            [KTSerializeInclude("d6832756-a584-404a-be94-db6157ad4b72")]
            public E14 eFieldNew;

            [KTSerializeInclude("3ca1b592-3e07-4397-a07c-d80d0b405ba4")]
            public Y14 aFieldNew;

            [KTSerializeInclude("db1ae86f-adce-400e-8025-23f9ca9068eb")]
            public E14 ePropertyNew
            {
                get;
                set;
            }


            [KTSerializeInclude("350f9ce7-76da-481e-840e-89e188b8360d")]
            public virtual Y14 aPropertyNew2
            {
                get;
                set;
            }

            [KTSerializeInclude("77d4c514-6e49-45aa-bac4-69d4f68837f1")]
            public D14 dFieldNew;

            [KTSerializeInclude("0abfcfa5-e1f1-4a77-b33b-494b69bc6605")]
            public D14 dPropertyNew
            {
                get;
                set;
            }
        }

        #endregion


        #region Y (analog of A14).

        [KTSerialize("b8fa91a4-592f-49a6-8f26-c80fc2a746c3")]
        class Y14
        {
            [KTSerializeInclude("650afe40-24ad-4f44-b7cb-e4a8fe0461da")]
            public int iPropertyNew
            {
                get;
                set;
            }

            [KTSerializeInclude("c13d1ce1-5aec-42e8-9a1b-ba6ead1434d2")]
            public int iFieldNew = 0;


            [KTSerializeInclude("c3d3c6f6-713f-4c0e-b48c-7da5f64938c5")]
            public decimal dFromSField;

            [KTSerializeInclude("362cf5b9-c456-4063-bd57-e788dc6e87f8")]
            public decimal dFromSProperty
            {
                get;
                set;
            }
        }

        #endregion



        #region X (analog of C14 with class GUID).

        [KTSerialize("86f25d4a-1c9b-4a92-a4c8-50258ed5e767")]
        class X14ID
        {
            [KTSerializeInclude("d813fb7b-44ee-4277-b375-ae4a30ed8621")]
            public Y14ID aPropertyNew
            {
                get;
                set;
            }

            [KTSerializeInclude("d6832756-a584-404a-be94-db6157ad4b72")]
            public E14 eFieldNew;

            [KTSerializeInclude("3ca1b592-3e07-4397-a07c-d80d0b405ba4")]
            public Y14ID aFieldNew;

            [KTSerializeInclude("db1ae86f-adce-400e-8025-23f9ca9068eb")]
            public E14 ePropertyNew
            {
                get;
                set;
            }


            [KTSerializeInclude("350f9ce7-76da-481e-840e-89e188b8360d")]
            public virtual Y14ID aPropertyNew2
            {
                get;
                set;
            }

            [KTSerializeInclude("77d4c514-6e49-45aa-bac4-69d4f68837f1")]
            public D14 dFieldNew;

            [KTSerializeInclude("0abfcfa5-e1f1-4a77-b33b-494b69bc6605")]
            public D14 dPropertyNew
            {
                get;
                set;
            }
        }

        #endregion


        #region Y (analog of A14 with class GUID).

        [KTSerialize("b3e8367f-3cf5-401f-966e-e5f149232800")]
        class Y14ID
        {
            #region Properties and fields.

            [KTSerializeInclude("650afe40-24ad-4f44-b7cb-e4a8fe0461da")]
            public int iPropertyNew
            {
                get;
                set;
            }

            [KTSerializeInclude("c13d1ce1-5aec-42e8-9a1b-ba6ead1434d2")]
            public int iFieldNew = 0;


            [KTSerializeInclude("c3d3c6f6-713f-4c0e-b48c-7da5f64938c5")]
            public decimal dFromSField;

            [KTSerializeInclude("362cf5b9-c456-4063-bd57-e788dc6e87f8")]
            public decimal dFromSProperty
            {
                get;
                set;
            }

            #endregion
        }

        #endregion

        #endregion


        #region Test List<T>.

        #region A.

        [KTSerialize("b6384e30-aab5-4003-9b3d-55beef784c83")]
        class A15
        {
            #region Properties and fields.

            [KTSerializeInclude("c13d1ce1-5aec-42e8-9a1b-ba6ead1534d2")]
            public int iField = 0;

            [KTSerializeInclude("650afe40-24ad-4f44-b7cb-e4a8fe0461da")]
            public string sProperty
            {
                get;
                set;
            }

            #endregion
        }

        #endregion


        #region B.

        [KTSerialize("389d55d9-8695-4bbf-ac7b-28615f51aa63")]
        class B15
        {
            [KTSerializeInclude("307a91bb-da63-444b-9a20-c837d0c8c435")]
            public List<A15> aList;

            [KTSerializeInclude("3196f795-4645-4c22-b188-c4d51f862456")]
            public List<A15> aListPreset = new List<A15>();
        }

        #endregion


        #region C.

        [KTSerialize("23d8dd65-da50-48ae-a78b-3134a776ce83")]
        class C15
        {
            [KTSerializeInclude("89cb6d4b-4522-4a1e-95a8-ca2d76109a9d")]
            public B15 bField;

            [KTSerializeInclude("6c9a11a4-c7e3-4441-ba11-354ce29fc73f")]
            public B15 bProperty
            {
                get;
                set;
            }
        }

        #endregion

        #endregion


        #region Test Dictionary<>.

        #region A.

        [KTSerialize("6faea60f-16c3-4bd4-9c47-6637b32c0782")]
        class A16
        {
            #region Properties and fields.

            [KTSerializeInclude("c13d1ce1-5aec-42e8-9a1b-ba6ead1634d2")]
            public int iField = 0;

            [KTSerializeInclude("650afe40-24ad-4f44-b7cb-e4a8fe0461da")]
            public string sProperty
            {
                get;
                set;
            }

            #endregion
        }

        #endregion


        #region B.

        [KTSerialize("da324dc5-c045-432e-94b8-80eee3c1a551")]
        class B16
        {
            [KTSerializeInclude("307a91bb-da63-444b-9a20-c837d0c8c435")]
            public Dictionary<int, A16> aCollection;

            [KTSerializeInclude("3196f795-4645-4c22-b188-c4d51f862456")]
            public Dictionary<int, A16> aCollectionPreset = new Dictionary<int, A16>();
        }

        #endregion


        #region C.

        [KTSerialize("9980e80d-a4cc-498c-ae84-61163528853a")]
        class C16
        {
            #region Properties and fields.

            [KTSerializeInclude("89cb6d4b-4522-4a1e-95a8-ca2d76109a9d")]
            public B16 bField;

            [KTSerializeInclude("6c9a11a4-c7e3-4441-ba11-354ce29fc73f")]
            public B16 bProperty
            {
                get;
                set;
            }

            #endregion
        }

        #endregion

        #endregion


        #region Test 17.

        #region A.

        class A17
        {
            #region Properties and fields.

            [KTSerializeInclude("ec6d7908-779d-4b6f-a52b-c7d85fd9bd61")]
            public int iField = 0;

            [KTSerializeInclude("015af692-738f-4e49-85a8-f019cd59b281")]
            public int iProperty
            {
                get;
                set;
            }

            #endregion


            #region Constructors.

            public A17()
            { }

            #endregion
        }

        #endregion


        #region B.

        [KTSerialize("c1272cfa-334d-4c75-be8d-99ae546a52e4")]
        class B17
        {
            #region Properties and fields.

            [KTSerializeInclude("c13d1ce1-5aec-42e8-9a1b-ba6ead1434d2")]
            public A17 aField;

            [KTSerializeInclude("650afe40-24ad-4f44-b7cb-e4a8fe0461da")]
            public A17 aProperty
            {
                get;
                set;
            }

            #endregion
        }

        #endregion



        #region A17Inherited.

        [KTSerialize("7e12bc70-2469-4276-81b0-ef8f35d0a5ef")]
        class A17Inherited : A17
        { }

        #endregion

        #endregion


        #region Test List<List<T>>.

        #region A.

        [KTSerialize("93e72587-5122-44bc-8b55-481e5844ddee")]
        class A18
        {
            #region Properties and fields.

            [KTSerializeInclude("c13d1ce1-5aec-42e8-9a1b-ba6ead1834d2")]
            public int iField = 0;

            [KTSerializeInclude("650afe40-24ad-4f44-b7cb-e4a8fe0461da")]
            public string sProperty
            {
                get;
                set;
            }

            #endregion
        }

        #endregion


        #region B.

        [KTSerialize("070522e4-017f-4b0b-bfe4-2e0d119f97d8")]
        class B18
        {
            #region Properties and fields.

            [KTSerializeInclude("307a91bb-da63-444b-9a20-c837d0c8c435")]
            public List<List<A18>> aList;

            #endregion
        }

        #endregion


        #region C.

        [KTSerialize("eb84bd0d-304c-4cb0-afbc-a702f1417e3b")]
        class C18
        {
            #region Properties and fields.

            [KTSerializeInclude("89cb6d4b-4522-4a1e-95a8-ca2d76109a9d")]
            public B18 bField;

            [KTSerializeInclude("6c9a11a4-c7e3-4441-ba11-354ce29fc73f")]
            public B18 bProperty
            {
                get;
                set;
            }

            #endregion
        }

        #endregion

        #endregion


        #region Test Dictionary<,Dictionary<>>.

        #region A.

        [KTSerialize("72e521eb-0f32-42b9-ab0c-dfda4256aad4")]
        class A19
        {
            #region Properties and fields.

            [KTSerializeInclude("c13d1ce1-5aec-42e8-9a1b-ba6ead1934d2")]
            public int iField = 0;

            [KTSerializeInclude("650afe40-24ad-4f44-b7cb-e4a8fe0461da")]
            public string sProperty
            {
                get;
                set;
            }

            #endregion
        }

        #endregion


        #region B.

        [KTSerialize("01a47717-dab9-49c2-89c3-1101f60c4020")]
        class B19
        {
            #region Properties and fields.

            [KTSerializeInclude("307a91bb-da63-444b-9a20-c837d0c8c435")]
            public Dictionary<int, Dictionary<int, A19>> aCollection;

            #endregion
        }

        #endregion


        #region C.

        [KTSerialize("9cc7f6bf-8a2c-4266-99ec-7bf6ca291b48")]
        class C19
        {
            #region Properties and fields.

            [KTSerializeInclude("89cb6d4b-4522-4a1e-95a8-ca2d76109a9d")]
            public B19 bField;

            [KTSerializeInclude("6c9a11a4-c7e3-4441-ba11-354ce29fc73f")]
            public B19 bProperty
            {
                get;
                set;
            }

            #endregion
        }

        #endregion

        #endregion


        #region Test Dictionary<Dictionary<>,>.

        #region A.

        [KTSerialize("fdd6cb89-8ce4-4225-aa87-732f6fef06ea")]
        class A20
        {
            [KTSerializeInclude("c13d1ce1-5aec-42e8-9a1b-ba6ead2034d2")]
            public int iField = 0;

            [KTSerializeInclude("650afe40-24ad-4f44-b7cb-e4a8fe0461da")]
            public string sProperty
            {
                get;
                set;
            }
        }

        #endregion


        #region B.

        [KTSerialize("b4231f19-b9fb-44b1-91d7-c7ac18be0aa7")]
        class B20
        {
            [KTSerializeInclude("307a91bb-da63-444b-9a20-c837d0c8c435")]
            public Dictionary<Dictionary<A20, int?>, int?> aCollection;
        }

        #endregion


        #region C.

        [KTSerialize("52595658-eb54-4832-ae12-04eab90d44b5")]
        class C20
        {
            [KTSerializeInclude("89cb6d4b-4522-4a1e-95a8-ca2d76109a9d")]
            public B20 bField;

            [KTSerializeInclude("6c9a11a4-c7e3-4441-ba11-354ce29fc73f")]
            public B20 bProperty
            {
                get;
                set;
            }
        }

        #endregion

        #endregion


        #region Test enumerators.

        #region A.

        [KTSerialize("d8c98f0d-cb12-40f2-af17-bce51ac468a7")]
        class A21
        {
            #region Properties and fields.

            [KTSerializeInclude("c13d1ce1-5aec-42e8-9a1b-ba6ead2134d2")]
            public int iField = 0;

            [KTSerializeInclude("650afe40-24ad-4f44-b7cb-e4a8fe0461da")]
            public Enum21? eNullProperty
            {
                get;
                set;
            }

            [KTSerializeInclude("fed21ef9-4d87-40ea-8aad-e56e93d8ad0b")]
            public Enum21 eProperty
            {
                get;
                set;
            }

            #endregion
        }

        #endregion


        #region B.

        [KTSerialize("0dd907de-be2d-4554-8449-3fe000614645")]
        class B21
        {
            #region Properties and fields.

            [KTSerializeInclude("8ab1e5e0-581b-4485-8cc0-3451a2da78c1")]
            public A21 aField;

            [KTSerializeInclude("9851dd40-4df5-46a7-bb3c-c6609c31a70d")]
            public A21 aProperty
            {
                get;
                set;
            }

            #endregion
        }

        #endregion


        #region Enum21.

        enum Enum21
        {
            Value1 = 5,
            Value2 = 7,
            Value3 = 9,
            Value4 = 10
        }

        #endregion

        #endregion


        #region Test generic classes.

        #region APre.

        [KTSerialize("cbc98b7e-1d45-4242-9962-c20315edac84")]
        class A22Pre<T, S>
        {
            #region Properties and fields.

            [KTSerializeInclude("c13d1ce1-5aec-42e9-9a1b-ba6ead1434d2")]
            public T tField;

            [KTSerializeInclude("650afe40-24ad-4f44-b7cb-e4a9fe0461da")]
            public S sProperty
            {
                get;
                set;
            }

            #endregion
        }

        #endregion


        #region A.

        [KTSerialize("09ee2ed0-6ec9-4fcf-8466-7f7c13f35a01")]
        class A22<T, S> : A22Pre<T, S>
        { }

        #endregion


        #region ANew.

        [KTSerialize("3180c106-b112-4750-bbeb-3d0232a97acc")]
        class A22New<T, S> : A22Pre<T, S>
        { }

        #endregion


        #region ANewInherited.

        [KTSerialize("1d73ee7c-ce60-4c5a-9be9-0309791ecebe")]
        class A22NewInherited : A22New<int, string>
        { }

        #endregion


        #region B.

        [KTSerialize("d7c9bcd4-aaf9-48fc-8b11-b3356cde9229")]
        class B22<T, S> : A22<T, S>
        {
            #region Properties and fields.

            [KTSerializeInclude("b9db6b52-a317-4aca-a1da-a79f1bd37343")]
            public Dictionary<T, S> collection;

            [KTSerializeInclude("3f0481a1-2660-4bf9-94d2-12341e7c8a6e")]
            public Dictionary<T, A22<T, S>> aCollection;

            [KTSerializeInclude("43a8341e-8687-420e-b424-b76cab0d8be2")]
            public Dictionary<A22New<int, int>, T> aReverseCollection;

            #endregion
        }

        #endregion


        #region C.

        [KTSerialize("2f8bd41a-4c5a-446b-a93b-dd4dfbed5732")]
        class C22<T, S>
        {
            #region Properties and fields.

            [KTSerializeInclude("c5866ee8-11ce-42f5-8b9b-db9792eb4765")]
            public A22New<T, S> aValue;

            #endregion
        }

        #endregion



        #region X (analog of C<int, string>).

        [KTSerialize("2f8bd41a-4c5a-446b-a93b-dd4dfbed5732")]
        class X22
        {
            #region Properties and fields.

            [KTSerializeInclude("c5866ee8-11ce-42f5-8b9b-db9792eb4765")]
            public A22NewInherited aValue;

            #endregion
        }

        #endregion

        #endregion


        #region Test List<struct?>.

        #region A.

        [KTSerialize("75483415-da84-4539-bfd6-48c3cd9f0fc9")]
        struct A23
        {
            #region Properties and fields.

            [KTSerializeInclude("c13d1ce1-5aec-42e8-9a1b-ba6ead2334d2")]
            public int iField;

            [KTSerializeInclude("650afe40-24ad-4f44-b7cb-e4a8fe0461da")]
            public string sProperty
            {
                get;
                set;
            }

            #endregion
        }

        #endregion


        #region B.

        [KTSerialize("dc56c9ea-240a-46d3-bf94-7761e9c4e5ff")]
        struct B23
        {
            #region Properties and fields.

            [KTSerializeInclude("307a91bb-da63-444b-9a20-c837d0c8c435")]
            public List<A23?> aList;

            #endregion
        }

        #endregion


        #region C.

        [KTSerialize("149e02fa-4eb0-436f-88d9-ae76632d49dc")]
        class C23
        {
            #region Properties and fields.

            [KTSerializeInclude("89cb6d4b-4522-4a1e-95a8-ca2d76109a9d")]
            public B23 bField;

            [KTSerializeInclude("6c9a11a4-c7e3-4441-ba11-354ce29fc73f")]
            public B23? bProperty
            {
                get;
                set;
            }

            #endregion
        }

        #endregion

        #endregion


        #region Test Dictionary<int, struct?>.

        #region A.

        [KTSerialize("92637dac-84ab-486e-b7d7-957947a1ac24")]
        struct A24
        {
            #region Properties and fields.

            [KTSerializeInclude("c13d1ce1-5aec-42e8-9a1b-ba6ead2434d2")]
            public int iField;

            [KTSerializeInclude("650afe40-24ad-4f44-b7cb-e4a8fe0461da")]
            public string sProperty
            {
                get;
                set;
            }

            #endregion
        }

        #endregion


        #region B.

        [KTSerialize("9b554b7b-2a5e-4944-8dfa-80486fab9495")]
        struct B24
        {
            #region Properties and fields.

            [KTSerializeInclude("307a91bb-da63-444b-9a20-c837d0c8c435")]
            public Dictionary<int, A24?> aCollection;

            #endregion
        }

        #endregion


        #region C.

        [KTSerialize("4d14eb88-1298-42ad-a2fd-9b43b63b45bc")]
        class C24
        {
            #region Properties and fields.

            [KTSerializeInclude("89cb6d4b-4522-4a1e-95a8-ca2d76109a9d")]
            public B24 bField;

            [KTSerializeInclude("6c9a11a4-c7e3-4441-ba11-354ce29fc73f")]
            public B24? bProperty
            {
                get;
                set;
            }

            #endregion
        }

        #endregion

        #endregion



        #region Test serialization of List<object> to List<object>, Dictionary<object, object> to Dictionary<object, object> and object[] to object[].

        #region A.

        [KTSerialize("8f0e174e-bd5e-43ea-a4b2-636c4ecab479")]
        class A25
        {
            [KTSerializeInclude("c13d1ce1-5aec-42e8-9a1b-ba6ead2534d2")]
            public int iField;

            [KTSerializeInclude("650afe40-24ad-4f44-b7cb-e4a8fe0461da")]
            public string sProperty
            {
                get;
                set;
            }
        }

        #endregion


        #region B.

        [KTSerialize("a6ac4f43-79dd-40d5-816e-89eff1f9a716")]
        class B25
        {
            [KTSerializeInclude("307a91bb-da63-444b-9a20-c837d0c8c435")]
            public List<object> aList;

            [KTSerializeInclude("8e2a4613-832b-4394-beaf-4c39444599d7")]
            public Dictionary<object, object> aDictionary;

            [KTSerializeInclude("bae0c557-848f-432d-8c98-161bf375edab")]
            public object[] aArray;

            [KTSerializeInclude("f5bee2be-9d48-49fd-9c0c-b1db679360dc")]
            public object aObject;
        }

        #endregion


        #region C.

        [KTSerialize("5ef959f8-3207-4800-8b6b-ed74b119aaf3")]
        class C25
        {
            [KTSerializeInclude("89cb6d4b-4522-4a1e-95a8-ca2d76109a9d")]
            public B25 bField;

            [KTSerializeInclude("6c9a11a4-c7e3-4441-ba11-354ce29fc73f")]
            public B25 bProperty
            {
                get;
                set;
            }
        }

        #endregion



        #region A25NoClassAttribute.

        class A25NoClassAttribute
        {
            [KTSerializeInclude("c13d1ce1-5aec-42e8-9a1b-ba6ead2534d2")]
            public int iField;

            [KTSerializeInclude("650afe40-24ad-4f44-b7cb-e4a8fe0461da")]
            public string sProperty
            {
                get;
                set;
            }
        }

        #endregion


        #region A25WithClassAttribute.

        [KTSerialize("224c565f-a564-41f6-a731-7436f347e20a")]
        class A25WithClassAttribute
        {
            [KTSerializeInclude("c13d1ce1-5aec-42e8-9a1b-ba6ead2534d2")]
            public int iField;

            [KTSerializeInclude("650afe40-24ad-4f44-b7cb-e4a8fe0461da")]
            public string sProperty
            {
                get;
                set;
            }
        }

        #endregion


        #region A25InheritedWithClassAttribute.

        [KTSerialize("4bf3c2e4-892c-4807-b8ee-38279438be92")]
        class A25InheritedWithClassAttribute : A25WithClassAttribute
        {
            [KTSerializeInclude("8fcab990-f750-4291-b96f-344bfe163663")]
            public int iField;

            [KTSerializeInclude("42be573e-ce2d-48d4-aefa-92791daf7db2")]
            public string sProperty
            {
                get;
                set;
            }
        }

        #endregion


        #region A25InheritedWithoutClassAttribute.

        class A25InheritedWithoutClassAttribute : A25WithClassAttribute
        {
            [KTSerializeInclude("8fcab990-f750-4291-b96f-344bfe163663")]
            public int iField;

            [KTSerializeInclude("42be573e-ce2d-48d4-aefa-92791daf7db2")]
            public string sProperty
            {
                get;
                set;
            }
        }

        #endregion

        #endregion


        #region Test List<T> serialization to List<object> and vice versa, no inheritance.

        #region A.

        [KTSerialize("0c95873f-d720-456a-81f4-807e58adf397")]
        class A26
        {
            [KTSerializeInclude("c13d1ce1-5aec-42e8-9a1b-ba6ead2534d2")]
            public int iField;

            [KTSerializeInclude("650afe40-24ad-4f44-b7cb-e4a8fe0461da")]
            public string sProperty
            {
                get;
                set;
            }
        }

        #endregion



        #region BObjectList.

        [KTSerialize("9259c3db-bef5-45a0-bea4-435f49c706b7")]
        class B26ObjectList
        {
            [KTSerializeInclude("307a91bb-da63-444b-9a20-c837d0c8c435")]
            public List<object> aList;

            [KTSerializeInclude("32187084-c7bd-4ee1-b0e7-53e72ad138e4")]
            public List<object> aAttributeList;


            [KTSerializeInclude("8e2a4613-832b-4394-beaf-4c39444599d7")]
            public Dictionary<object, object> aDictionary;

            [KTSerializeInclude("1105e14c-8546-4fd0-9c55-2623d714ba88")]
            public Dictionary<object, object> aAttributeDictionary;


            [KTSerializeInclude("f8c8fa93-1023-47d3-b54d-f564fdb94cea")]
            public object[] aArray;

            [KTSerializeInclude("065d6314-a540-4a89-af1f-9be1693a10a6")]
            public object[] aAttributeArray;


            [KTSerializeInclude("f5bee2be-9d48-49fd-9c0c-b1db679360dc")]
            public object aObject;

            [KTSerializeInclude("0f4f7e9e-57f3-452e-9083-a0c65135d31c")]
            public object aAttributeObject;
        }

        #endregion


        #region BTypedList.

        [KTSerialize("9259c3db-bef5-45a0-bea4-435f49c706b7")]
        class B26TypedList
        {
            [KTSerializeInclude("307a91bb-da63-444b-9a20-c837d0c8c435")]
            public List<A26> aList;

            [KTSerializeInclude("32187084-c7bd-4ee1-b0e7-53e72ad138e4")]
            public List<A26WithClassAttribute> aAttributeList;


            [KTSerializeInclude("8e2a4613-832b-4394-beaf-4c39444599d7")]
            public Dictionary<A26, A26> aDictionary;

            [KTSerializeInclude("1105e14c-8546-4fd0-9c55-2623d714ba88")]
            public Dictionary<A26WithClassAttribute, A26WithClassAttribute> aAttributeDictionary;


            [KTSerializeInclude("f8c8fa93-1023-47d3-b54d-f564fdb94cea")]
            public A26[] aArray;

            [KTSerializeInclude("065d6314-a540-4a89-af1f-9be1693a10a6")]
            public A26WithClassAttribute[] aAttributeArray;


            [KTSerializeInclude("f5bee2be-9d48-49fd-9c0c-b1db679360dc")]
            public A26 aObject;

            [KTSerializeInclude("0f4f7e9e-57f3-452e-9083-a0c65135d31c")]
            public A26WithClassAttribute aAttributeObject;
        }

        #endregion



        #region CObjectList.

        [KTSerialize("76cc1195-ecce-4cd9-a292-4b57d225661a")]
        class C26ObjectList
        {
            [KTSerializeInclude("89cb6d4b-4522-4a1e-95a8-ca2d76109a9d")]
            public B26ObjectList bField;

            [KTSerializeInclude("6c9a11a4-c7e3-4441-ba11-354ce29fc73f")]
            public B26ObjectList bProperty
            {
                get;
                set;
            }
        }

        #endregion


        #region CTypedList.

        [KTSerialize("76cc1195-ecce-4cd9-a292-4b57d225661a")]
        class C26TypedList
        {
            [KTSerializeInclude("89cb6d4b-4522-4a1e-95a8-ca2d76109a9d")]
            public B26TypedList bField;

            [KTSerializeInclude("6c9a11a4-c7e3-4441-ba11-354ce29fc73f")]
            public B26TypedList bProperty
            {
                get;
                set;
            }
        }

        #endregion



        #region A26NoClassAttribute.

        class A26NoClassAttribute : A26
        {
            [KTSerializeInclude("0599fa59-20ae-4737-a41d-b7bba46b46e0")]
            public int iField;

            [KTSerializeInclude("b725c75a-044d-4ce3-997c-1727f2775095")]
            public string sProperty
            {
                get;
                set;
            }
        }

        #endregion


        #region A26WithClassAttribute.

        [KTSerialize("224c565f-a564-41f6-a731-7436f347e20a")]
        class A26WithClassAttribute : A26
        {
            [KTSerializeInclude("d61d733b-b8ce-4621-8a6d-5b821abf48a8")]
            public int iField;

            [KTSerializeInclude("dde00848-8b38-439f-b493-cf5eb9baa37a")]
            public virtual string sProperty
            {
                get;
                set;
            }
        }

        #endregion


        #region A26InheritedWithClassAttribute.

        [KTSerialize("4bf3c2e4-892c-4807-b8ee-38279438be92")]
        class A26InheritedWithClassAttribute : A26WithClassAttribute
        {
            //[KTSerializeInclude("62cf6c11-0d53-4aa0-a29d-8866eea85eba")]
            //public int iField;

            [KTSerializeInclude("866f375a-8939-459d-92b5-4325178884af")]
            public override string sProperty
            {
                get;
                set;
            }
        }

        #endregion


        #region A26InheritedWithoutClassAttribute.

        class A26InheritedWithoutClassAttribute : A26WithClassAttribute
        {
            [KTSerializeInclude("d79bfc5f-00e2-47a2-9f8f-cab977c4ece5")]
            public int iField;

            [KTSerializeInclude("9920b621-6972-4e84-8632-109e906d10f3")]
            public string sProperty
            {
                get;
                set;
            }
        }

        #endregion

        #endregion



        #region Test types processing with complicated inheritance.

        #region A.

        [KTSerialize("865d49ea-17ed-4cdd-979f-c772929dfc5e")]
        class A27
        {
            [KTSerializeInclude("c13d1ce1-5aec-42e8-9a1b-ba6ead2534d2")]
            public int iField;

            [KTSerializeInclude("650afe40-24ad-4f44-b7cb-e4a8fe0461da")]
            public string sProperty
            {
                get;
                set;
            }
        }

        #endregion


        #region B.

        [KTSerialize("4d5efb37-02a7-4350-a85f-d23037461354")]
        class B27
        {
            #region Properties and fields.

            [KTSerializeInclude("32187084-c7bd-4ee1-b0e7-53e72ad138e4")]
            public List<A27> aAttributeList1;

            [KTSerializeInclude("bc7020aa-1faa-402c-a88b-b03b30e18122")]
            public List<A27NoClassAttribute> aAttributeList2;

            [KTSerializeInclude("dd21797b-d5b4-4c49-ba82-5f90cad2cd7b")]
            public List<A27InheritedWithoutClassAttribute> aAttributeList3;

            [KTSerializeInclude("8089757b-f377-41ba-a311-e14c7b2010a0")]
            public List<A27WithClassAttribute> aAttributeList4;

            [KTSerializeInclude("83fbc30a-1d59-43a5-aaad-c1047c700a4a")]
            public List<A27InheritedWithClassAttribute> aAttributeList5;

            #endregion


            #region Constructors.

            private B27() { }
            public B27(int i) { }

            #endregion
        }

        #endregion


        #region C.

        [KTSerialize("ca81b654-768a-4c5c-b34c-14993cf725d7")]
        class C27
        {
            [KTSerializeInclude("89cb6d4b-4522-4a1e-95a8-ca2d76109a9d")]
            public B27 bField;

            [KTSerializeInclude("6c9a11a4-c7e3-4441-ba11-354ce29fc73f")]
            public B27 bProperty
            {
                get;
                set;
            }
        }

        #endregion



        #region A27NoClassAttribute.

        class A27NoClassAttribute : A27
        {
            #region Properties and fields.

            [KTSerializeInclude("0599fa59-20ae-4737-a41d-b7bba46b46e0")]
            public int iField;

            [KTSerializeInclude("b725c75a-044d-4ce3-997c-1727f2775095")]
            public string sProperty
            {
                get;
                set;
            }

            #endregion
        }

        #endregion


        #region A27WithClassAttribute.

        [KTSerialize("224c565f-a564-41f6-a731-7436f347e20a")]
        class A27WithClassAttribute : A27
        {
            #region Properties and fields.

            [KTSerializeInclude("d61d733b-b8ce-4621-8a6d-5b821abf48a8")]
            public int iField;

            [KTSerializeInclude("dde00848-8b38-439f-b493-cf5eb9baa37a")]
            public string sProperty
            {
                get;
                set;
            }

            #endregion
        }

        #endregion


        #region A27InheritedWithClassAttribute.

        [KTSerialize("4bf3c2e4-892c-4807-b8ee-38279438be92")]
        class A27InheritedWithClassAttribute : A27WithClassAttribute
        {
            #region Properties and fields.

            [KTSerializeInclude("62cf6c11-0d53-4aa0-a29d-8866eea85eba")]
            public int iField;

            [KTSerializeInclude("866f375a-8939-459d-92b5-4325178884af")]
            public string sProperty
            {
                get;
                set;
            }

            #endregion


            #region Constructors.

            protected A27InheritedWithClassAttribute() { }
            public A27InheritedWithClassAttribute(int i) { }

            #endregion
        }

        #endregion


        #region A27InheritedWithoutClassAttribute.

        class A27InheritedWithoutClassAttribute : A27WithClassAttribute
        {
            #region Properties and fields.

            [KTSerializeInclude("d79bfc5f-00e2-47a2-9f8f-cab977c4ece5")]
            public int iField;

            [KTSerializeInclude("9920b621-6972-4e84-8632-109e906d10f3")]
            public string sProperty
            {
                get;
                set;
            }

            #endregion
        }

        #endregion



        #region X (same as C).

        [KTSerialize("ca81b654-768a-4c5c-b34c-14993cf725d7")]
        class X27
        {
            [KTSerializeInclude("6c9a11a4-c7e3-4441-ba11-354ce29fc73f")]
            public Y27 bField;

            [KTSerializeInclude("89cb6d4b-4522-4a1e-95a8-ca2d76109a9d")]
            public Y27 bProperty
            {
                get;
                set;
            }
        }

        #endregion


        #region Y (same as B).

        [KTSerialize("4d5efb37-02a7-4350-a85f-d23037461354")]
        class Y27
        {
            #region Properties and fields.

            [KTSerializeInclude("bc7020aa-1faa-402c-a88b-b03b30e18122")]
            public List<A27NoClassAttribute> aAttributeList2;

            [KTSerializeInclude("8089757b-f377-41ba-a311-e14c7b2010a0")]
            public List<A27WithClassAttribute> aAttributeList4;

            [KTSerializeInclude("dd21797b-d5b4-4c49-ba82-5f90cad2cd7b")]
            public List<A27InheritedWithoutClassAttribute> aAttributeList3;

            [KTSerializeInclude("32187084-c7bd-4ee1-b0e7-53e72ad138e4")]
            public List<A27> aAttributeList1;

            [KTSerializeInclude("1b9d9032-0ab9-4854-8177-6d7ffe15f63c")]
            public List<A27> aAttributeListNew2 = new List<A27>();

            #endregion


            #region Constructors.

            private Y27() { }
            public Y27(int i) { }

            #endregion
        }

        #endregion

        #endregion


        #region Test same class GUID.

        #region A.

        [KTSerialize("b7ea9c19-8784-4a50-a425-f053b1a268fa")]
        class A28
        { }

        #endregion


        #region ASameGuid.

        [KTSerialize("b7ea9c19-8784-4a50-a425-f053b1a268fa")]
        class A28SameGuid
        { }

        #endregion

        #endregion


        #region Serialize plain property to the wrong type.

        #region AIntProperty.

        [KTSerialize("b7ea9c19-8784-4a50-a425-f053b1a268fa")]
        class A28IntProperty
        {
            [KTSerializeInclude("ec6d7908-779d-4b6f-a52b-c7d85fd9bd61")]
            public int iProperty
            {
                get;
                set;
            }
        }

        #endregion


        #region AIntField.

        [KTSerialize("b7ea9c19-8784-4a50-a425-f053b1a268fa")]
        class A28IntField
        {
            [KTSerializeInclude("ec6d7908-779d-4b6f-a52b-c7d85fd9bd61")]
            public int iField = 0;
        }

        #endregion



        #region AIntNullProperty.

        [KTSerialize("b7ea9c19-8784-4a50-a425-f053b1a268fa")]
        class A28IntNullProperty
        {
            [KTSerializeInclude("ec6d7908-779d-4b6f-a52b-c7d85fd9bd61")]
            public int? iProperty
            {
                get;
                set;
            }
        }

        #endregion



        #region AInt64Property.

        [KTSerialize("b7ea9c19-8784-4a50-a425-f053b1a268fa")]
        class A28Int64Property
        {
            [KTSerializeInclude("ec6d7908-779d-4b6f-a52b-c7d85fd9bd61")]
            public Int64 iProperty
            {
                get;
                set;
            }
        }

        #endregion


        #region AInt64Field.

        [KTSerialize("b7ea9c19-8784-4a50-a425-f053b1a268fa")]
        class A28Int64Field
        {
            [KTSerializeInclude("ec6d7908-779d-4b6f-a52b-c7d85fd9bd61")]
            public Int64 iField = 0;
        }

        #endregion



        #region AStringProperty.

        [KTSerialize("b7ea9c19-8784-4a50-a425-f053b1a268fa")]
        class A28StringProperty
        {
            [KTSerializeInclude("ec6d7908-779d-4b6f-a52b-c7d85fd9bd61")]
            public string iProperty
            {
                get;
                set;
            }
        }

        #endregion


        #region AStringField.

        [KTSerialize("b7ea9c19-8784-4a50-a425-f053b1a268fa")]
        class A28StringField
        {
            [KTSerializeInclude("ec6d7908-779d-4b6f-a52b-c7d85fd9bd61")]
            public string iField;
        }

        #endregion



        #region AFloatProperty.

        [KTSerialize("b7ea9c19-8784-4a50-a425-f053b1a268fa")]
        class A28FloatProperty
        {
            [KTSerializeInclude("ec6d7908-779d-4b6f-a52b-c7d85fd9bd61")]
            public float iProperty
            {
                get;
                set;
            }
        }

        #endregion


        #region AFloatField.

        [KTSerialize("b7ea9c19-8784-4a50-a425-f053b1a268fa")]
        class A28FloatField
        {
            [KTSerializeInclude("ec6d7908-779d-4b6f-a52b-c7d85fd9bd61")]
            public float iField;
        }

        #endregion



        #region ADoubleProperty.

        [KTSerialize("b7ea9c19-8784-4a50-a425-f053b1a268fa")]
        class A28DoubleProperty
        {
            [KTSerializeInclude("ec6d7908-779d-4b6f-a52b-c7d85fd9bd61")]
            public double iProperty
            {
                get;
                set;
            }
        }

        #endregion


        #region ADoubleField.

        [KTSerialize("b7ea9c19-8784-4a50-a425-f053b1a268fa")]
        class A28DoubleField
        {
            [KTSerializeInclude("ec6d7908-779d-4b6f-a52b-c7d85fd9bd61")]
            public double iField;
        }

        #endregion



        #region ADecimalProperty.

        [KTSerialize("b7ea9c19-8784-4a50-a425-f053b1a268fa")]
        class A28DecimalProperty
        {
            [KTSerializeInclude("ec6d7908-779d-4b6f-a52b-c7d85fd9bd61")]
            public decimal iProperty
            {
                get;
                set;
            }
        }

        #endregion


        #region ADecimalField.

        [KTSerialize("b7ea9c19-8784-4a50-a425-f053b1a268fa")]
        class A28DecimalField
        {
            [KTSerializeInclude("ec6d7908-779d-4b6f-a52b-c7d85fd9bd61")]
            public decimal iField;
        }

        #endregion



        #region AEnumProperty.

        [KTSerialize("b7ea9c19-8784-4a50-a425-f053b1a268fa")]
        class A28EnumProperty
        {
            [KTSerializeInclude("ec6d7908-779d-4b6f-a52b-c7d85fd9bd61")]
            public Enum21 iProperty
            {
                get;
                set;
            }
        }

        #endregion


        #region AEnumField.

        [KTSerialize("b7ea9c19-8784-4a50-a425-f053b1a268fa")]
        class A28EnumField
        {
            [KTSerializeInclude("ec6d7908-779d-4b6f-a52b-c7d85fd9bd61")]
            public Enum21 iField = 0;
        }

        #endregion



        #region AEnumNullProperty.

        [KTSerialize("b7ea9c19-8784-4a50-a425-f053b1a268fa")]
        class A28EnumNullProperty
        {
            [KTSerializeInclude("ec6d7908-779d-4b6f-a52b-c7d85fd9bd61")]
            public Enum21? iProperty
            {
                get;
                set;
            }
        }

        #endregion


        #region AEnumNullField.

        [KTSerialize("b7ea9c19-8784-4a50-a425-f053b1a268fa")]
        class A28EnumNullField
        {
            [KTSerializeInclude("ec6d7908-779d-4b6f-a52b-c7d85fd9bd61")]
            public Enum21? iField;
        }

        #endregion

        #endregion


        #region Serialize lists to the wrong type.

        #region A28Int32List.

        [KTSerialize("befd557f-2625-424a-a3a6-409c751ff425")]
        class A28Int32List
        {
            [KTSerializeInclude("d0421175-997e-4501-bada-f23e01c7b1fb")]
            public List<int> list
            {
                get;
                set;
            }
        }

        #endregion


        #region A28Int64List.

        [KTSerialize("befd557f-2625-424a-a3a6-409c751ff425")]
        class A28Int64List
        {
            [KTSerializeInclude("d0421175-997e-4501-bada-f23e01c7b1fb")]
            public List<Int64> list;
        }

        #endregion



        #region AIntNullPropertyList.

        [KTSerialize("befd557f-2625-424a-a3a6-409c751ff425")]
        class A28IntNullableList
        {
            [KTSerializeInclude("d0421175-997e-4501-bada-f23e01c7b1fb")]
            public List<int?> list
            {
                get;
                set;
            }
        }

        #endregion


        #region A28StringList.

        [KTSerialize("befd557f-2625-424a-a3a6-409c751ff425")]
        class A28StringList
        {
            [KTSerializeInclude("d0421175-997e-4501-bada-f23e01c7b1fb")]
            public List<string> list;
        }

        #endregion

        #endregion


        #region Serialize dictionaries to the wrong type.

        #region A28Int32Dictionary.

        [KTSerialize("befd557f-2625-424a-a3a6-409c751ff425")]
        class A28Int32Dictionary
        {
            [KTSerializeInclude("d0421175-997e-4501-bada-f23e01c7b1fb")]
            public Dictionary<int, int> dictionary
            {
                get;
                set;
            }
        }

        #endregion


        #region A28Int64Dictionary.

        [KTSerialize("befd557f-2625-424a-a3a6-409c751ff425")]
        class A28Int64Dictionary
        {
            [KTSerializeInclude("d0421175-997e-4501-bada-f23e01c7b1fb")]
            public Dictionary<long, long> dictionary;
        }

        #endregion



        #region AIntNullPropertyDictionary.

        [KTSerialize("befd557f-2625-424a-a3a6-409c751ff425")]
        class A28IntNullableDictionary
        {
            [KTSerializeInclude("d0421175-997e-4501-bada-f23e01c7b1fb")]
            public Dictionary<int?, int?> dictionary
            {
                get;
                set;
            }
        }

        #endregion


        #region A28StringDictionary.

        [KTSerialize("befd557f-2625-424a-a3a6-409c751ff425")]
        class A28StringDictionary
        {
            [KTSerializeInclude("d0421175-997e-4501-bada-f23e01c7b1fb")]
            public Dictionary<string, string> dictionary;
        }

        #endregion

        #endregion


        #region Test wrong before/after attributes.

        #region BeforeSerializeDuplicate.

        [KTSerialize("7e12bc70-2469-4276-81b0-ef8f35d0a5ef")]
        class A29BeforeSerializeDuplicate
        {
            [KTBeforeSerialize]
            public void Function1() { }

            [KTBeforeSerialize]
            public void Function2() { }
        }

        #endregion


        #region AfterSerializeDuplicate.

        [KTSerialize("7e12bc70-2469-4276-81b0-ef8f35d0a5ef")]
        class A29AfterSerializeDuplicate
        {
            [KTAfterSerialize]
            public void Function1() { }

            [KTAfterSerialize]
            public void Function2() { }
        }

        #endregion


        #region BeforeDeserializeDuplicate.

        [KTSerialize("7e12bc70-2469-4276-81b0-ef8f35d0a5ef")]
        class A29BeforeDeserializeDuplicate
        {
            [KTBeforeDeserialize]
            public void Function1() { }

            [KTBeforeDeserialize]
            public void Function2() { }
        }

        #endregion


        #region AfterDeserializeDuplicate.

        [KTSerialize("7e12bc70-2469-4276-81b0-ef8f35d0a5ef")]
        class A29AfterDeserializeDuplicate
        {
            [KTAfterDeserialize]
            public void Function1() { }

            [KTAfterDeserialize]
            public void Function2() { }
        }

        #endregion

        #endregion


        #region Test before/after serialization and deserialization with inheritance.

        #region A.

        [KTSerialize("8f0e174e-bd5e-43ea-a4b2-636c4ecab479")]
        class A30
        {
            #region Properties and fields.

            [KTSerializeInclude("c13d1ce1-5aec-42e8-9a1b-ba6ead3034d2")]
            public int iField;

            [KTSerializeInclude("650afe40-24ad-4f44-b7cb-e4a8fe0461da")]
            public string sProperty
            {
                get;
                set;
            }

            #endregion


            public bool BeforeSerializeBool;
            public bool AfterSerializeBool;
            public bool BeforeDeserializeBool;
            public bool AfterDeserializeBool;


            [KTBeforeSerialize]
            public void BeforeSerializeABase()
            {
                Assert.IsFalse(BeforeSerializeBool);
                BeforeSerializeBool = true;

                // Modify value.
                this.iField += 3;
            }


            [KTAfterSerialize]
            public void AfterSerializeABase()
            {
                Assert.IsTrue(BeforeSerializeBool);
                Assert.IsFalse(AfterSerializeBool);
                AfterSerializeBool = true;
            }


            [KTBeforeDeserialize]
            public void BeforeDeserializeABase()
            {
                Assert.IsFalse(BeforeDeserializeBool);
                BeforeDeserializeBool = true;
            }


            [KTAfterDeserialize]
            public void AfterDeserializeABase()
            {
                Assert.IsTrue(BeforeDeserializeBool);
                Assert.IsFalse(AfterDeserializeBool);
                AfterDeserializeBool = true;
            }
        }

        #endregion


        #region AInherited.

        [KTSerialize("ec5683e4-e855-461c-871f-a8a1ec194221")]
        class A30Inherited : A30
        {
            public bool BeforeSerializeBool;
            public bool AfterSerializeBool;
            public bool BeforeDeserializeBool;
            public bool AfterDeserializeBool;


            [KTBeforeSerialize]
            public void BeforeSerializeA()
            {
                // Base.
                Assert.IsTrue(((A30)this).BeforeSerializeBool);

                // Self.
                Assert.IsFalse(BeforeSerializeBool);
                BeforeSerializeBool = true;
            }


            [KTAfterSerialize]
            public void AfterSerializeA()
            {
                // Base.
                Assert.IsTrue(((A30)this).AfterSerializeBool);

                // Self.
                Assert.IsTrue(BeforeSerializeBool);
                Assert.IsFalse(AfterSerializeBool);
                AfterSerializeBool = true;
            }


            [KTBeforeDeserialize]
            public void BeforeDeserializeA()
            {
                // Base.
                Assert.IsTrue(((A30)this).BeforeDeserializeBool);

                // Self.
                Assert.IsFalse(BeforeDeserializeBool);
                BeforeDeserializeBool = true;
            }


            [KTAfterDeserialize]
            public void AfterDeserializeA()
            {
                // Base.
                Assert.IsTrue(((A30)this).AfterDeserializeBool);

                // Self.
                Assert.IsTrue(BeforeDeserializeBool);
                Assert.IsFalse(AfterDeserializeBool);
                AfterDeserializeBool = true;

                // Modify value.
                this.sProperty += "*";
            }
        }

        #endregion


        #region AInherited2 (witn same actions as A30Inherited).

        [KTSerialize("660896d1-830e-4a64-aa87-9aac1edfa9f8")]
        class A30Inherited2 : A30
        {
            public bool BeforeSerializeBool;
            public bool AfterSerializeBool;
            public bool BeforeDeserializeBool;
            public bool AfterDeserializeBool;


            [KTBeforeSerialize]
            public void BeforeSerializeA()
            {
                // Base.
                Assert.IsTrue(((A30)this).BeforeSerializeBool);

                // Self.
                Assert.IsFalse(BeforeSerializeBool);
                BeforeSerializeBool = true;
            }


            [KTAfterSerialize]
            public void AfterSerializeA()
            {
                // Base.
                Assert.IsTrue(((A30)this).AfterSerializeBool);

                // Self.
                Assert.IsTrue(BeforeSerializeBool);
                Assert.IsFalse(AfterSerializeBool);
                AfterSerializeBool = true;
            }


            [KTBeforeDeserialize]
            public void BeforeDeserializeA()
            {
                // Base.
                Assert.IsTrue(((A30)this).BeforeDeserializeBool);

                // Self.
                Assert.IsFalse(BeforeDeserializeBool);
                BeforeDeserializeBool = true;
            }


            [KTAfterDeserialize]
            public void AfterDeserializeA()
            {
                // Base.
                Assert.IsTrue(((A30)this).AfterDeserializeBool);

                // Self.
                Assert.IsTrue(BeforeDeserializeBool);
                Assert.IsFalse(AfterDeserializeBool);
                AfterDeserializeBool = true;

                // Modify value.
                this.sProperty += "*";
            }
        }

        #endregion



        #region B.

        [KTSerialize("a6ac4f43-79dd-40d5-816e-89eff1f9a716")]
        class B30
        {
            [KTSerializeInclude("f5bee2be-9d48-49fd-9c0c-b1db679360dc")]
            public object aObject;

            [KTSerializeInclude("6e9c0306-a576-47ed-9768-3c503c0c6f15")]
            public A30 a1;

            [KTSerializeInclude("3585352a-7d4b-4c44-aa29-ccc52d8229f6")]
            public A30Inherited a2;

            [KTSerializeInclude("46168e8c-6370-4072-ab11-0feab377f05b")]
            public A30Inherited2 a3;
        }

        #endregion


        #region CPreBase.

        [KTSerialize("67555af6-2b40-485c-92e1-fbf6e0f0992b")]
        class C30PreBase
        {
            public bool BeforeSerializeBool;
            public bool AfterSerializeBool;
            public bool BeforeDeserializeBool;
            public bool AfterDeserializeBool;


            [KTBeforeSerialize]
            public void BeforeSerializePre()
            {
                Assert.IsFalse(BeforeSerializeBool);
                BeforeSerializeBool = true;
            }

            [KTAfterSerialize]
            public void AfterSerializePre()
            {
                Assert.IsTrue(BeforeSerializeBool);
                Assert.IsFalse(AfterSerializeBool);
                AfterSerializeBool = true;
            }

            [KTBeforeDeserialize]
            public void BeforeDeserializePre()
            {
                Assert.IsFalse(BeforeDeserializeBool);
                BeforeDeserializeBool = true;
            }

            [KTAfterDeserialize]
            public void AfterDeserializePre()
            {
                Assert.IsTrue(BeforeDeserializeBool);
                Assert.IsFalse(AfterDeserializeBool);
                AfterDeserializeBool = true;
            }
        }

        #endregion


        #region CBase.

        [KTSerialize("7c5e23ef-d846-4690-9a5e-9b18eab4cca0")]
        class C30Base : C30PreBase
        {
            public bool BeforeSerializeBool;
            public bool AfterSerializeBool;
            public bool BeforeDeserializeBool;
            public bool AfterDeserializeBool;


            [KTBeforeSerialize]
            public void BeforeSerializeBase()
            {
                // Base.
                Assert.IsTrue(((C30PreBase)this).BeforeSerializeBool);

                // Self.
                Assert.IsFalse(BeforeSerializeBool);
                BeforeSerializeBool = true;
            }


            [KTAfterSerialize]
            public void AfterSerializeBase()
            {
                // Base.
                Assert.IsTrue(((C30PreBase)this).AfterSerializeBool);

                // Self.
                Assert.IsTrue(BeforeSerializeBool);
                Assert.IsFalse(AfterSerializeBool);
                AfterSerializeBool = true;
            }


            [KTBeforeDeserialize]
            public void BeforeDeserializeBase()
            {
                // Base.
                Assert.IsTrue(((C30PreBase)this).BeforeDeserializeBool);

                // Self.
                Assert.IsFalse(BeforeDeserializeBool);
                BeforeDeserializeBool = true;
            }


            [KTAfterDeserialize]
            public void AfterDeserializeBase()
            {
                // Base.
                Assert.IsTrue(((C30PreBase)this).AfterDeserializeBool);

                // Self.
                Assert.IsTrue(BeforeDeserializeBool);
                Assert.IsFalse(AfterDeserializeBool);
                AfterDeserializeBool = true;
            }
        }

        #endregion


        #region C.

        [KTSerialize("5ef959f8-3207-4800-8b6b-ed74b119aaf3")]
        class C30 : C30Base
        {
            #region Properties and fields.

            [KTSerializeInclude("89cb6d4b-4522-4a1e-95a8-ca2d76109a9d")]
            public B30 bField;


            public bool BeforeSerializeBool;
            public bool AfterSerializeBool;
            public bool BeforeDeserializeBool;
            public bool AfterDeserializeBool;


            [KTBeforeSerialize]
            public void BeforeSerialize()
            {
                // Base.
                Assert.IsTrue(((C30PreBase)this).BeforeSerializeBool);
                Assert.IsTrue(((C30Base)this).BeforeSerializeBool);

                // Self.
                Assert.IsFalse(BeforeSerializeBool);
                BeforeSerializeBool = true;
            }


            [KTAfterSerialize]
            public void AfterSerialize()
            {
                // Base.
                Assert.IsTrue(((C30PreBase)this).AfterSerializeBool);
                Assert.IsTrue(((C30Base)this).AfterSerializeBool);

                // Self.
                Assert.IsTrue(BeforeSerializeBool);
                Assert.IsFalse(AfterSerializeBool);
                AfterSerializeBool = true;
            }


            [KTBeforeDeserialize]
            public void BeforeDeserialize()
            {
                // Base.
                Assert.IsTrue(((C30PreBase)this).BeforeDeserializeBool);
                Assert.IsTrue(((C30Base)this).BeforeDeserializeBool);

                // Self.
                Assert.IsFalse(BeforeDeserializeBool);
                BeforeDeserializeBool = true;
            }


            [KTAfterDeserialize]
            public void AfterDeserialize()
            {
                // Base.
                Assert.IsTrue(((C30PreBase)this).AfterDeserializeBool);
                Assert.IsTrue(((C30Base)this).AfterDeserializeBool);

                // Self.
                Assert.IsTrue(BeforeDeserializeBool);
                Assert.IsFalse(AfterDeserializeBool);
                AfterDeserializeBool = true;
            }

            #endregion
        }

        #endregion


        #region CInherited.

        [KTSerialize("a6b8321e-7109-4b5c-899c-7c8d672ecad9")]
        class C30Inherited : C30
        {

        }

        #endregion


        #region C30InheritedStepTwo.

        [KTSerialize("13bdf108-a086-45a0-9493-a4acc0c32745")]
        class C30InheritedStepTwo : C30Inherited
        {

        }

        #endregion

        #endregion


        #region Test register types.

        #region Series 1.

        [KTSerialize("2593d50f-0cd7-49d1-8745-0e36b6fae4af")]
        class ARegister000
        {
            [KTSerializeInclude("b2e3d586-67f8-449b-90ac-fe457aaf463a")]
            protected string sField0;

            [KTSerializeInclude("6169930e-1364-4279-859c-40959cd82951")]
            protected string sField1;

            [KTSerializeInclude("6c5417b0-3edb-47fa-b673-20fab6e00546")]
            protected string sField2;

            [KTSerializeInclude("18c39465-7828-4125-8596-facff2f96d82")]
            protected string sField3;

            [KTSerializeInclude("14c90ff8-5d78-4c64-9c15-287cb7ac3542")]
            protected string sField4;

            [KTSerializeInclude("f2fd40c1-6852-448c-805b-5a85249c2dde")]
            protected string sField5;

            [KTSerializeInclude("902674ce-7545-4a15-99ec-2ec3915666d2")]
            protected string sField6;

            [KTSerializeInclude("ac3eb747-5488-40af-8764-435e98793941")]
            protected string sField7;

            [KTSerializeInclude("7dabc072-2b0c-4682-836b-38b1d0cde9a7")]
            protected string sField8;

            [KTSerializeInclude("575c48c5-4cb3-4834-80d5-78a37445439c")]
            protected string sField9;
        }

        [KTSerialize("e909e55b-afbb-4084-b340-fca8eded720b")]
        class ARegister001 : ARegister000 { }

        [KTSerialize("2af852ae-9955-48ab-825a-a9a32875e23f")]
        class ARegister002 : ARegister001 { }

        [KTSerialize("ec1a7a10-4e32-4aad-beb6-1c697a661d98")]
        class ARegister003 : ARegister002 { }

        [KTSerialize("1861a884-15fe-4824-bf06-36d6d72e5294")]
        class ARegister004 : ARegister003 { }

        [KTSerialize("0cee81fa-9035-4b57-a996-f9e917ec7899")]
        class ARegister005 : ARegister004 { }

        [KTSerialize("e0bfb484-af36-45f6-8829-ca66639ffa4e")]
        class ARegister006 : ARegister005 { }

        [KTSerialize("655adb90-e1cc-4b5a-a4e7-f82b6914fb6a")]
        class ARegister007 : ARegister006 { }

        [KTSerialize("ec95f5e9-45ec-4a1d-8b40-0208b819e1f2")]
        class ARegister008 : ARegister007 { }

        [KTSerialize("683e12be-8fc6-4eb8-838f-37ace5e04d23")]
        class ARegister009 : ARegister008 { }

        #endregion


        #region Series 2.

        [KTSerialize("900db23d-0670-4a79-ba54-30740bdc0a1d")]
        class ARegister010 : ARegister000 { }

        [KTSerialize("81dcec66-889a-4bb8-9ad5-5ee0f18f3ab5")]
        class ARegister011 : ARegister010 { }

        [KTSerialize("d3506aa8-bd65-43f7-bdac-f4a41fc80527")]
        class ARegister012 : ARegister011 { }

        [KTSerialize("1200cca9-6094-4f31-b74c-e8d0d4eb398c")]
        class ARegister013 : ARegister012 { }

        [KTSerialize("f2e006f7-b51a-43b6-8beb-08ac0afb259a")]
        class ARegister014 : ARegister013 { }

        [KTSerialize("ec703fb9-0598-422a-bbf1-0db303ce6bab")]
        class ARegister015 : ARegister014 { }

        [KTSerialize("f2391712-b2f9-47be-9947-ecf4c4940655")]
        class ARegister016 : ARegister015 { }

        [KTSerialize("0cf13a56-1637-4a97-bfc2-553ebfdecf6d")]
        class ARegister017 : ARegister016 { }

        [KTSerialize("46030f16-d2c5-4a5c-bd7f-3ebabc4a02a6")]
        class ARegister018 : ARegister017 { }

        [KTSerialize("b39c1151-ea2b-49aa-88c5-cf75c73ffe23")]
        class ARegister019 : ARegister018 { }

        #endregion


        #region Series 3.

        [KTSerialize("e8285722-3a9f-4901-8b54-41aca55f4df1")]
        class ARegister020 : ARegister000 { }

        [KTSerialize("6be57686-9388-4735-a46c-f83d91171c75")]
        class ARegister021 : ARegister020 { }

        [KTSerialize("422475a0-1fc8-4b94-bae7-0d6a44304f4e")]
        class ARegister022 : ARegister021 { }

        [KTSerialize("61a8e9e6-5e0a-4aa3-9481-65ab1a9cd2ce")]
        class ARegister023 : ARegister022 { }

        [KTSerialize("06619d64-06c5-4d0a-a0a4-41ba2931dbd4")]
        class ARegister024 : ARegister023 { }

        [KTSerialize("b579453f-395f-41e2-a060-d9e4d1a8b8a5")]
        class ARegister025 : ARegister024 { }

        [KTSerialize("fcd47146-66ff-40d3-b90d-4ca64863d27f")]
        class ARegister026 : ARegister025 { }

        [KTSerialize("4bcbb27f-b2e6-4f6d-9be3-ff63d4873eac")]
        class ARegister027 : ARegister026 { }

        [KTSerialize("0304c1cd-b4b6-4410-9e06-11b7d284077c")]
        class ARegister028 : ARegister027 { }

        [KTSerialize("fc499fa0-82ff-408c-94d1-fb3f9e65133e")]
        class ARegister029 : ARegister028 { }

        #endregion


        #region Series 4.

        [KTSerialize("0bd26ab2-15e4-4eb0-b7ac-4bbde957cd20")]
        class ARegister030 : ARegister000 { }

        [KTSerialize("68cd9a43-0f0e-4479-bfcd-fb43a82d182b")]
        class ARegister031 : ARegister030 { }

        [KTSerialize("6eba0010-b3a2-4b8a-9356-b89f94ed79d0")]
        class ARegister032 : ARegister031 { }

        [KTSerialize("25abf861-471c-4edd-bb7c-da048c5cfec9")]
        class ARegister033 : ARegister032 { }

        [KTSerialize("19cedef3-acb8-431c-88b7-e2509b853814")]
        class ARegister034 : ARegister033 { }

        [KTSerialize("c0a85cc3-d9c6-4bd4-910e-7f411b1b42d0")]
        class ARegister035 : ARegister034 { }

        [KTSerialize("5d057d85-d71d-4ba5-80ab-397f9cb25b7e")]
        class ARegister036 : ARegister035 { }

        [KTSerialize("6916d5cf-f23a-4ea4-b351-e322076f3c71")]
        class ARegister037 : ARegister036 { }

        [KTSerialize("b3073452-f680-400c-8c89-379c75e61e2e")]
        class ARegister038 : ARegister037 { }

        [KTSerialize("15b3011c-99fa-496e-a3a6-d0407cd6887c")]
        class ARegister039 : ARegister038 { }

        #endregion


        #region Series 5.

        [KTSerialize("8de6e5e0-457a-436b-802a-76aefcb38e2f")]
        class ARegister040 : ARegister000 { }

        [KTSerialize("7c6df87f-8d39-4ab4-884a-f10a9334b7ad")]
        class ARegister041 : ARegister040 { }

        [KTSerialize("e68134da-3905-4503-ba45-d8c021083440")]
        class ARegister042 : ARegister041 { }

        [KTSerialize("28578f83-6ae9-4164-b923-2fd29a7dba37")]
        class ARegister043 : ARegister042 { }

        [KTSerialize("c09cd004-54a5-430a-9339-a26327b8051c")]
        class ARegister044 : ARegister043 { }

        [KTSerialize("b913bfec-0aaf-48e2-8d7a-a60fde2e8cc2")]
        class ARegister045 : ARegister044 { }

        [KTSerialize("da4ff847-ab5d-4322-ade8-42bb451bae15")]
        class ARegister046 : ARegister045 { }

        [KTSerialize("39789efb-d911-4391-b0ef-21c3a93cdd7c")]
        class ARegister047 : ARegister046 { }

        [KTSerialize("41defe57-0956-48ba-af32-d6368506de4b")]
        class ARegister048 : ARegister047 { }

        [KTSerialize("82968b7f-9e36-459c-a597-c5442649e544")]
        class ARegister049 : ARegister048 { }

        #endregion

        #endregion


        #region Test interfaces.

        [KTSerialize("636f2c93-0200-4bf7-bbb4-1e47aa04cd0c")]
        interface IA31
        {
            [KTSerializeInclude("cb16f349-87e2-486b-a478-3542ce79c7f5")]
            string sProperty { get; set; }
        }


        [KTSerialize("00b576e3-ff1c-48cf-bdd6-ebc315c6a2cf")]
        class A31 : IA31
        {
            [KTSerializeInclude("cb16f349-87e2-486b-a478-3542ce79c7f5")]
            public string sProperty { get; set; }
        }



        [KTSerialize("2a4a1bc2-e9e8-465a-8da6-f4b868b035c1")]
        class B31
        {
            [KTSerializeInclude("900fb93d-e350-45f7-8ac9-3957b69f8c58")]
            public IA31 iProperty { get; set; }

            [KTSerializeInclude("83f1cba2-292b-4b95-914a-978a5aa3b2c6")]
            public List<IA31> iList;
        }

        #endregion



        #region Test T[].

        #region A.

        [KTSerialize("60796c9d-ecd7-4013-9a55-574ddcccc8ba")]
        class A32
        {
            #region Properties and fields.

            [KTSerializeInclude("41763a96-ba15-48ec-aa20-09774a691ba5")]
            public int iField = 0;

            [KTSerializeInclude("8872ab53-1ef8-48cc-a515-9acc99c95014")]
            public string sProperty
            {
                get;
                set;
            }

            #endregion
        }

        #endregion


        #region B.

        [KTSerialize("712f1d81-6e21-4f2c-9d98-6caa801a96d5")]
        class B32
        {
            [KTSerializeInclude("5b590b6e-7ca6-4773-bb53-acaa61648daa")]
            public A32[] aArray;

            [KTSerializeInclude("0c7bc6f6-62ee-4d63-90c0-1707b78b2371")]
            public A32[] aArrayPreset = new A32[4];

            [KTSerializeInclude("d68ffe38-5d99-4290-a8e2-103441117851")]
            public int?[] aArrayNullable;
        }

        #endregion


        #region C.

        [KTSerialize("ec766b73-ffe8-452a-ab0e-a05a8169621f")]
        class C32
        {
            [KTSerializeInclude("7bfbec31-dd02-42e0-8800-b53b6d6bf5f9")]
            public B32 bField;

            [KTSerializeInclude("be977686-f248-40f9-af0f-207a82bdd016")]
            public B32 bProperty
            {
                get;
                set;
            }
        }

        #endregion

        #endregion


        #region Test T[][].

        #region A.

        [KTSerialize("b499547d-5b8d-4aa1-9b1e-db43e62e477e")]
        class A33
        {
            [KTSerializeInclude("4ebd9659-4cfb-4650-b120-a1965d4647e9")]
            public int iField = 0;

            [KTSerializeInclude("0e33458a-4e6f-4308-9736-1ea2e2e1179a")]
            public string sProperty
            {
                get;
                set;
            }
        }

        #endregion


        #region B.

        [KTSerialize("028b8f2b-ba79-47c7-94ec-6652fe38c973")]
        class B33
        {
            [KTSerializeInclude("31c472e6-e79f-4827-9fb7-4ee54ac4a4e2")]
            public A33[][] aArray;

            [KTSerializeInclude("b71885c1-ea3d-4597-97ea-27b7980d73dd")]
            public int?[][] aArrayNullable;
        }

        #endregion


        #region C.

        [KTSerialize("1448b9ce-dc11-4694-97ce-1a1e096785f5")]
        class C33
        {
            [KTSerializeInclude("dfd23bdd-02ff-4c8b-a0d2-0bb09fd16460")]
            public B33 bField;

            [KTSerializeInclude("9ba80352-2331-4649-981c-f178a7b56951")]
            public B33 bProperty
            {
                get;
                set;
            }
        }

        #endregion

        #endregion

        #endregion

        #endregion
    }
}
