using Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.Caching;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.Tests
{
   
    /// <summary>
    /// Test class for cache key
    ///</summary>
    [TestClass()]
    public class CacheKeyTest
    {
        [TestMethod()]
        public void CacheKey_Constructor_Test()
        {
            //arrange
            string keyName = "fakeName";

            //act
            CacheKey key = new CacheKey(keyName);

            //assert
            Assert.AreEqual(key.KeyName, keyName);
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void CacheKey_ConstructorWithNullKeyName_ThrowArgumentException_Test()
        {
            //arrange
            string keyName = null;

            //act
            CacheKey key = new CacheKey(keyName); 
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void CacheKey_ConstructorWithWhitespaceKeyName_ThrowArgumentException_Test()
        {
            //arrange
            string keyName = "";

            //act
            CacheKey key = new CacheKey(keyName);
        }

        [TestMethod()]
        public void CacheKey_CheckVaryParamsFromNullAnonimousType_Test()
        {
            //arrange
            string keyName = "fakeName";
            object varyParams = null;

            //act
            CacheKey key = new CacheKey(keyName, varyParams);

            string result = key.GetCacheKey();

            //assert
            Assert.AreEqual(result, string.Format("{0}#", keyName));

        }
        [TestMethod()]
        public void CacheKey_CheckVaryParamsFromAnonimousType_Test()
        {
            //arrange
            string keyName = "fakeName";
            object varyParams = new { PropertyA = "ParamA", PropertyB = 2 };

            //act
            CacheKey key = new CacheKey(keyName, varyParams);

            string result = key.GetCacheKey();

            string expected = string.Format("{0}#{1};{2};{3};{4}", keyName, "PropertyA", "ParamA", "PropertyB", "2");

            //assert
            Assert.AreEqual(result, expected);

        }
    }
}
