using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.Caching;

namespace Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.Tests
{
    /// <summary>
    /// Tests class for cache item
    /// </summary>
    [TestClass]
    public class CacheItemTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CacheItem_NullCacheKeyThrowNewArgumentException_Test()
        {
            //Arrange 
            CacheKey key = null;

            //Act
            CacheItemConfig item = new CacheItemConfig(key,new TimeSpan()); 
        }

        [TestMethod()]
        public void CacheItem_Constructor_Test()
        {
            //Arrange 
            CacheKey key = new CacheKey("fakeName");

            //Act
            CacheItemConfig item = new CacheItemConfig(key, new TimeSpan(0, 0, 10));

            //assert

            Assert.AreEqual(key, item.CacheKey);
            Assert.IsTrue(item.ExpirationTime.TotalSeconds == 10);

        }

        [TestMethod()]
        public void CacheItem_DefaultConstructorSet10SecondsToExpirationTime_Test()
        {
            //Arrrange
            CacheKey key = new CacheKey("fakeName");

            //Act
            CacheItemConfig item = new CacheItemConfig(key);

            //Assert
            Assert.IsTrue(item.ExpirationTime.TotalSeconds == 10);
        }
    }
}

