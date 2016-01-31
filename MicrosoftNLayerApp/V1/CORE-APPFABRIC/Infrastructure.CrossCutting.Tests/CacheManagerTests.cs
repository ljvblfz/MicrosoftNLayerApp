using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.NetFramework.Caching;
using Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.Caching;

namespace Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.Tests
{
    [TestClass]
    public class CacheManagerTests
    {
      
        [TestMethod()]
        public void CacheManager_Constructor_Tests()
        {
            //Arrange
            CacheManager cacheManager;

            //act
            cacheManager = new CacheManager();

            //assert 
            Assert.IsNotNull(cacheManager);
        }
        [TestMethod()]
        public void CacheManager_TryGet_NotInCacheReturnFalse_Test()
        {
            //Arrange
            CacheManager cacheManager = new CacheManager();

            CacheKey key = new CacheKey(Guid.NewGuid().ToString());
            CacheItemConfig item = new CacheItemConfig(key);

            //Act
            object cacheditem = null;
            bool result = cacheManager.TryGet<object>(item, out cacheditem);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void CacheManager_TryGet_InCacheReturnTrue_Test()
        {
            //Arrange
            CacheManager cacheManager = new CacheManager();

            CacheKey key = new CacheKey(Guid.NewGuid().ToString());
            CacheItemConfig cacheitem = new CacheItemConfig(key);

            //Act
            object item = new object();
            object expected;
            cacheManager.Add(cacheitem, item);
            bool result = cacheManager.TryGet<object>(cacheitem, out expected);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void CacheManager_Add_Test()
        {
            //Arrange 
            CacheManager cacheManager = new CacheManager();

            CacheKey key = new CacheKey("fakeKey");
            CacheItemConfig cacheItem = new CacheItemConfig(key);

            //act
            
            cacheManager.Add(cacheItem,new object());

            //assert
            object result;
            Assert.IsTrue(cacheManager.TryGet(cacheItem, out result));
            Assert.IsNotNull(result);

        }
    }
}
