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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.Caching
{
    /// <summary>
    /// A cache item configuration
    /// </summary>
    public class CacheItemConfig
    {
        #region Properties


        CacheKey _cacheKey;
        /// <summary>
        /// Get the associated cached key
        /// </summary>
        public CacheKey CacheKey
        {
            get
            {
                return _cacheKey;
            }
        }

        TimeSpan _expirationTime;
        /// <summary>
        /// Get the associted expiration time
        /// </summary>
        public TimeSpan ExpirationTime 
        {
            get
            {
                return _expirationTime;
            }
        }

        #endregion

        #region Constructor
       
        /// <summary>
        /// Create a new instance of cache item
        /// </summary>
        /// <param name="cacheKey">The cached key</param>
        public CacheItemConfig(CacheKey cacheKey)
            :this(cacheKey,new TimeSpan(0,0,10))
        {
        }

        /// <summary>
        /// Create a new instance of cache item
        /// </summary>
        /// <param name="cacheKey">The cached key</param>
        /// <param name="expirationTime">Associated expiration time</param>
        public CacheItemConfig(CacheKey cacheKey, TimeSpan expirationTime)
        {
            if (cacheKey == (CacheKey)null)
                throw new ArgumentNullException("cacheKey");

            _cacheKey = cacheKey;
            _expirationTime = expirationTime;

        }

        #endregion

    }
}
