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
    /// Base contract for cache manager. This contract
    /// expose basic methods for work with cache in solution
    /// </summary>
    public interface ICacheManager
    {
        /// <summary>
        /// Try get if object exist in cache and set result in <paramref name="result"/>
        /// </summary>
        /// <typeparam name="TResult">type of returned result</typeparam>
        /// <param name="cacheItemConfig">Cahe item specification</param>
        /// <param name="result">result if exist in cache</param>
        /// <returns>True if object exist in cache, else false</returns>
        bool TryGet<TResult>(CacheItemConfig cacheItemConfig, out TResult result);

        /// <summary>
        /// Add new object in underliying cache
        /// </summary>
        /// <param name="cacheItemConfig">The cache item spec</param>
        /// <param name="value">The item to add</param>
        void Add(CacheItemConfig cacheItemConfig, object value);

        /// <summary>
        /// Remove object in cache
        /// </summary>
        /// <param name="cacheKey">Key identifier of item to delete</param>
        /// <returns>
        /// True if element is removed in cache, if not false
        /// </returns>
        bool Remove(CacheKey cacheKey);
    }
}
