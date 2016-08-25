using CelesteEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CelesteEngineUnitTestFramework;
using System.Collections.Generic;
using System.IO;

namespace CelesteEngineCelesteEngineUnitTestGameProject
{
    [TestClass]
    public class TestXmlDataSerializer : UnitTest
    {
        private string ContentFullPath;

        public TestXmlDataSerializer()
        {
            ContentFullPath = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\..", "Content\\Data\\Test");
        }

        #region Test Empty Data File

        [TestMethod]
        public void Test_XmlDataSerializer_DeserializeTestBaseData()
        {
            TestBaseData baseData = Deserialize<TestBaseData>("Non Defaults\\TestBaseData");

            Assert.IsNotNull(baseData);
        }

        #endregion

        #region Test Float Deserialization

        [TestMethod]
        public void Test_XmlDataSerializer_DeserializeDefaultFloat()
        {
            TestFloatData data = Deserialize<TestFloatData>("Defaults\\TestDefaultFloatData");

            Assert.AreEqual(default(float), data.TestFloat);
        }

        [TestMethod]
        public void Test_XmlDataSerializer_DeserializeFloat()
        {
            TestFloatData data = Deserialize<TestFloatData>("Non Defaults\\TestFloatData");

            Assert.AreEqual(15.5f, data.TestFloat);
        }

        #endregion

        #region Test Int Deserialization

        [TestMethod]
        public void Test_XmlDataSerializer_DeserializeDefaultInt()
        {
            TestIntData data = Deserialize<TestIntData>("Defaults\\TestDefaultIntData");

            Assert.AreEqual(default(int), data.TestInt);
        }

        [TestMethod]
        public void Test_XmlDataSerializer_DeserializeInt()
        {
            TestIntData data = Deserialize<TestIntData>("Non Defaults\\TestIntData");

            Assert.AreEqual(-3, data.TestInt);
        }

        #endregion

        #region Test Bool Deserialization

        [TestMethod]
        public void Test_XmlDataSerializer_DeserializeDefaultBool()
        {
            TestBoolData data = Deserialize<TestBoolData>("Defaults\\TestDefaultBoolData");

            Assert.AreEqual(default(bool), data.TestBool);
        }

        [TestMethod]
        public void Test_XmlDataSerializer_DeserializeBool()
        {
            TestBoolData data = Deserialize<TestBoolData>("Non Defaults\\TestBoolData");

            Assert.AreEqual(true, data.TestBool);
        }

        #endregion

        #region Test String Deserialization

        [TestMethod]
        public void Test_XmlDataSerializer_DeserializeDefaultString()
        {
            TestStringData data = Deserialize<TestStringData>("Defaults\\TestDefaultStringData");

            Assert.AreEqual(default(string), data.TestString);
        }

        [TestMethod]
        public void Test_XmlDataSerializer_DeserializeString()
        {
            TestStringData data = Deserialize<TestStringData>("Non Defaults\\TestStringData");

            Assert.AreEqual("This is a test string", data.TestString);
        }

        #endregion

        #region Test List Deserialization

        [TestMethod]
        public void Test_XmlDataSerializer_DeserializeDefaultList()
        {
            TestListData data = Deserialize<TestListData>("Defaults\\TestDefaultListData");

            Assert.AreEqual(default(List<object>), data.TestList);
        }

        [TestMethod]
        public void Test_XmlDataSerializer_DeserializeFloatList()
        {
            TestFloatListData data = Deserialize<TestFloatListData>("Non Defaults\\TestFloatListData");

            List<float> expected = new List<float>() { 5, 0, -5 };
            Assert.IsTrue(data.TestFloatList.CheckOrderedListsEqual(expected));
        }

        [TestMethod]
        public void Test_XmlDataSerializer_DeserializeBoolList()
        {
            TestBoolListData data = Deserialize<TestBoolListData>("Non Defaults\\TestBoolListData");

            List<bool> expected = new List<bool>() { true, true, false, false };
            Assert.IsTrue(data.TestBoolList.CheckOrderedListsEqual(expected));
        }

        [TestMethod]
        public void Test_XmlDataSerializer_DeserializeStringList()
        {
            TestStringListData data = Deserialize<TestStringListData>("Non Defaults\\TestStringListData");

            List<string> expected = new List<string>() { "This", "is", "a", "test" };
            Assert.IsTrue(data.TestStringList.CheckOrderedListsEqual(expected));
        }

        #endregion

        #region Test Nested Class Deserialization

        [TestMethod]
        public void Test_XmlDataSerializer_DeserializeDataClass()
        {
            TestClassData data = Deserialize<TestClassData>("Non Defaults\\TestClassData");

            Assert.AreEqual(10.2f, data.TestFloatData.TestFloat);
            Assert.AreEqual(true, data.TestBoolData.TestBool);
            Assert.AreEqual("Test", data.TestStringData.TestString);
        }

        [TestMethod]
        public void Test_XMLDataSerializer_DeserializeDataClassWithFloatList()
        {
            TestClassDataWithFloatList data = Deserialize<TestClassDataWithFloatList>("Non Defaults\\TestClassDataWithFloatList");
            List<float> expected = new List<float>() { 5, 0, -5 };

            Assert.IsTrue(data.TestFloatListData.TestFloatList.CheckOrderedListsEqual(expected));
        }

        #endregion

        #region Utility Functions

        /// <summary>
        /// Wrapper function around the XmlDataSerializer.Deserialize so that we don't have to keep adding the paths all the time
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="fileName"></param>
        private T Deserialize<T>(string fileName)
        {
            return XmlDataSerializer.Deserialize<T>(Path.Combine(ContentFullPath, "Test Data Types", fileName));
        }

        #endregion
    }
}