using System;
using System.Text;
using System.Collections.Generic;

using KT.Common.Classes.Application;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KT.Serializer.Tests
{
    /// <summary>
    /// Example for KT serializer.
    /// </summary>
    [TestClass]
    public class ExampleTest
    {
        [TestMethod]
        public void ExampleMethod()
        {
            KTSerializer serializer = new KTSerializer(true);

            AOne o1, o2;

            o1 = new AOne()
            {
                iProperty = 10,
                notSerializedField = 20
            };

            using (MemoryStream headerStream = new MemoryStream())
            using (MemoryStream stream = new MemoryStream())
            {
                serializer.Serialize(stream, o1, headerStream);

                headerStream.Position = 0;
                stream.Position = 0;

                o2 = serializer.Deserialize<AOne>(stream, headerStream);
            }
        }
    }


    [KTSerialize("c40c7c35-c4d5-4964-9996-943435e32336")]
    class AOne
    {
        [KTSerializeInclude("f7363d24-d6a9-485c-9e44-e5c4e64e816d")]
        private int iField = 0;


        private int _iProperty;
        [KTSerializeInclude("a1067eed-5653-410b-9960-93636bf70c94")]
        public int iProperty
        {
            get { return _iProperty; }
            set
            {
                _iProperty = value;
                iField = value * 2;
            }
        }


        public int notSerializedField = 100;
    }
}
