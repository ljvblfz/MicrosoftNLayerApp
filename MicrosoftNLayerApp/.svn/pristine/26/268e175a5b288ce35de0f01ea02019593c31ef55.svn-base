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
//===================================================================================using System;

using Microsoft.Samples.NLayerApp.Domain.MainModule.Countries;
using Microsoft.Samples.NLayerApp.Domain.MainModule.Entities;
using Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.Logging;
using Microsoft.Samples.NLayerApp.Infrastructure.Data.Core;
using Microsoft.Samples.NLayerApp.Infrastructure.Data.MainModule.UnitOfWork;

namespace Microsoft.Samples.NLayerApp.Infrastructure.Data.MainModule.Repositories
{
    /// <summary>
    /// ICountryRepository implementation
    /// for more information <see cref="Microsoft.Samples.NLayerApp.Domain.MainModule.Countries.ICountryRepository"/>
    /// </summary>
    public class CountryRepository
        :Repository<Country>,ICountryRepository
    {
        #region Constructor

        /// <summary>
        /// Default constructor for this repository
        /// </summary>
        /// <param name="unitOfWork">IMainModuleUnitOfWork dependency, intented to be resolved with IoC</param>
        /// <param name="traceManager">ITraceManager context, intended to be resolved wiht IoC</param>
        public CountryRepository(IMainModuleUnitOfWork unitOfWork, ITraceManager traceManager) : base(unitOfWork, traceManager) { }

        #endregion
    }
}
