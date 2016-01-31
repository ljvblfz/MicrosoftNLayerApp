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


namespace Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.IoC
{
    /// <summary>
    /// Base contract for locator and register dependencies
    /// </summary>
    public interface IContainer
    {
        /// <summary>
        /// Solve TService dependency
        /// </summary>
        /// <typeparam name="TService">Type of dependency</typeparam>
        /// <returns>instance of TService</returns>
        TService Resolve<TService>();

        /// <summary>
        /// Solve type construction and return the object as a TService instance
        /// </summary>
        /// <returns>instance of this type</returns>
        object Resolve(Type type);

        /// <summary>
        /// Register type into service locator
        /// </summary>
        /// <param name="type">Type to register</param>
        void RegisterType(Type type);
    }
}
