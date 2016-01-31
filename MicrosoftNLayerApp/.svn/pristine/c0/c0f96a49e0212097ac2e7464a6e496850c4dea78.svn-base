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
using Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.IoC.Unity;


namespace Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.IoC
{
    /// <summary>
    /// IServiceFactory implementation
    /// </summary>
    public sealed class IoCFactory
    {
        #region Singleton

        static readonly IoCFactory instance = new IoCFactory();

        /// <summary>
        /// Get singleton instance of IoCFactory
        /// </summary>
        public static IoCFactory Instance
        {
            get
            {
                return instance;
            }
        }

        #endregion

        #region Members

        IContainer _CurrentContainer;

        /// <summary>
        /// Get current configured IContainer
        /// <remarks>
        /// At this moment only IoCUnityContainer existss
        /// </remarks>
        /// </summary>
        public IContainer CurrentContainer
        {
            get
            {
                return _CurrentContainer;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Only for singleton pattern, remove before field init IL anotation
        /// </summary>
        static IoCFactory() { }
        IoCFactory()
        {
            _CurrentContainer = new IoCUnityContainer();
        }
        
        #endregion
    }
}
