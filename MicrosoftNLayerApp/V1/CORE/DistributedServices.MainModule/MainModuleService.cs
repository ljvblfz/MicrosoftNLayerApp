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
using System.ServiceModel;

using Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.IoC;
using Microsoft.Samples.NLayerApp.Domain.MainModule.Entities;
using Microsoft.Samples.NLayerApp.DistributedServices.Core.ErrorHandlers;


namespace Microsoft.Samples.NLayerApp.DistributedServices.MainModule
{
    /// <summary>
    /// Service facade for MainModule 
    /// </summary>
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.Single,
                    ConcurrencyMode=ConcurrencyMode.Multiple,
                    Namespace="Microsoft.Samples.DistributedServices.MainModule")]
    [ErrorHandlerServiceBehavior()]
    public sealed partial  class MainModuleService
        :IMainModuleService
    {
    }
}
