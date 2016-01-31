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

using Microsoft.Samples.NLayerApp.Domain.Core;
using Microsoft.Samples.NLayerApp.Domain.MainModule.Entities;

namespace Microsoft.Samples.NLayerApp.Domain.MainModule.Countries
{
    /// <summary>
    /// Contract for aggregate Country repository
    /// </summary>
    public interface ICountryRepository
        :IRepository<Country>
    {
    }
}
