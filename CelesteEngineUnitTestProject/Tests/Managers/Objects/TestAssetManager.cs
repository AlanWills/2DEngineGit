using CelesteEngine;
using CelesteEngineData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CelesteEngineUnitTestFramework;
using System.Collections.Generic;

namespace CelesteEngineUnitTestGameProject
{
    public class TestAssetManager : UnitTest
    {
        #region Extra Custom Tests

        [TestMethod]
        public void TestGetAllDataOfType()
        {
            List<BaseData> data = AssetManager.GetAllDataOfType<BaseData>();
        }

        #endregion
    }
}
