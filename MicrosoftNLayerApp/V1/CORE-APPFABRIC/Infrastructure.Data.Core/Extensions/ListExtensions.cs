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
using System.Collections.Generic;

namespace Microsoft.Samples.NLayerApp.Infrastructure.Data.Core.Extensions
{
    /// <summary>
    /// Define extension methods in List class
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Extensor Method for translate a list into a InMemoryObjectSet.
        /// This extension method is only for testing purposed.
        /// </summary>
        /// <typeparam name="T">Typeof elements</typeparam>
        /// <param name="list">List to translate into a IObjectSet</param>
        /// <returns>InMemoryObjectSet</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public static MemorySet<T> ToMemorySet<T>(this List<T> list)
            where T : class
        {
            return new MemorySet<T>(list);
        }
    }
}
