//===================================================================================
// Microsoft Developer & Platform Evangelism
//=================================================================================== 
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
//===================================================================================
// Copyright (c) Microsoft Corporation.  All Rights Reserved.
// This code is released under the terms of the MS-LPL license, 
// http://microsoftnlayerapp.codeplex.com/license
//===================================================================================
using System;

using Microsoft.ApplicationServer.Caching;//Added reference to Microsoft.ApplicationServer.Caching and Microsoft.ApplicationServer.Caching.Client

using Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.Caching;

namespace Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.NetFramework.Caching
{
    /// <summary>
    /// Cache manager implementation with Windows Server App Fabric Cache
    /// </summary>
    public sealed class CacheManager
        :ICacheManager,IDisposable
    {
        #region Members

        DataCacheFactory _cacheFactory;
        
        #endregion

        #region Constructor

        /// <summary>
        /// Create a new constructor of cache manager. 
        /// Is recomended using "singleton" life time in the selected IoC
        /// </summary>
        public CacheManager()
        {
            //configuration for this cache factory is delegated in application configuration file
            _cacheFactory = new DataCacheFactory();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// <see cref="M:Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.Caching.TryGet{TResult}"/>
        /// </summary>
        /// <typeparam name="TResult"><see cref="M:Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.Caching.TryGet{TResult}"/></typeparam>
        /// <param name="cacheItemConfig"><see cref="M:Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.Caching.TryGet{TResult}"/></param>
        /// <param name="result"<see cref="M:Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.Caching.TryGet{TResult}"/>></param>
        /// <returns><see cref="M:Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.Caching.TryGet{TResult}"/></returns>
        public bool TryGet<TResult>(CacheItemConfig cacheItemConfig, out TResult result)
        {
            if (cacheItemConfig != null)
            {

                //get default cache
                DataCache defaultCache = _cacheFactory.GetDefaultCache();

                string cacheKey = cacheItemConfig.CacheKey.GetCacheKey();

                //get object from cache and check if exists
                object cachedItem = defaultCache.Get(cacheKey);

                if (cachedItem != null)
                {
                    result = (TResult)cachedItem;

                    return true;
                }
                else
                {
                    result = default(TResult);

                    return false;
                }
            }
            else
                throw new ArgumentNullException("cacheItem");
        }

        /// <summary>
        /// <see cref="M:Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.Caching.TryGet{TResult}"/>
        /// </summary>
        /// <param name="cacheItemConfig"><see cref="M:Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.Caching.TryGet{TResult}"/></param>
        /// <param name="value"><see cref="M:Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.Caching.TryGet{TResult}"/></param>
        public void Add(CacheItemConfig cacheItemConfig, object value)
        {
            if (value != null
                &&
                cacheItemConfig != null)
            {
                //get default cache
                DataCache defaultCache = _cacheFactory.GetDefaultCache();

                string cachekey = cacheItemConfig.CacheKey.GetCacheKey();
                TimeSpan expirationTime = cacheItemConfig.ExpirationTime;

                defaultCache.Put(cachekey, value,expirationTime);
            }
        }

        /// <summary>
        /// <see cref="M:Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.Caching.Remove"/>
        /// </summary>
        /// <param name="cacheKey"><see cref="M:Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.Caching.Remove"/></param>
        public bool Remove(CacheKey cacheKey)
        {
            if (cacheKey != null)
            {
                DataCache defaultCache = _cacheFactory.GetDefaultCache();

                return defaultCache.Remove(cacheKey.GetCacheKey());
            }
            else
                return false;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (_cacheFactory != null)
                _cacheFactory.Dispose();
        }

        #endregion
    }
}
