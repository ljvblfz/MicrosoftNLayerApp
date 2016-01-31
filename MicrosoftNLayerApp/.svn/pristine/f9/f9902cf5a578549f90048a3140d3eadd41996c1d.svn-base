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
using Microsoft.Practices.Unity;

using Microsoft.Samples.NLayerApp.Application.MainModule.BankingManagement;
using Microsoft.Samples.NLayerApp.Application.MainModule.CustomersManagement;
using Microsoft.Samples.NLayerApp.Application.MainModule.SalesManagement;

using Microsoft.Samples.NLayerApp.Domain.MainModule.BankAccounts;
using Microsoft.Samples.NLayerApp.Domain.MainModule.Countries;
using Microsoft.Samples.NLayerApp.Domain.MainModule.Customers;
using Microsoft.Samples.NLayerApp.Domain.MainModule.Orders;
using Microsoft.Samples.NLayerApp.Domain.MainModule.Products;

using Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.Caching;
using Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.IoC.Resources;
using Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.IoC.Unity;
using Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.IoC.Unity.LifetimeManagers;
using Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.Logging;
using Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.NetFramework.Caching;
using Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.NetFramework.Logging;
using Microsoft.Samples.NLayerApp.Infrastructure.Data.MainModule.Repositories;
using Microsoft.Samples.NLayerApp.Infrastructure.Data.MainModule.UnitOfWork;
using Microsoft.Samples.NLayerApp.Infrastructure.Data.MainModule.Mock;
using System.Configuration;

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
