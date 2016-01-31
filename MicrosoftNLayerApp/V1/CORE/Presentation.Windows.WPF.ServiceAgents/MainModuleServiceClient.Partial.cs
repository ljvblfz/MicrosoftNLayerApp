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

namespace Microsoft.Samples.NLayerApp.Presentation.Windows.WPF.ServiceAgents.Proxies.MainModule
{

    /// <summary>
    /// MainModule service client proxy
    /// </summary>
    public partial class MainModuleServiceClient
    {
        /// <summary>
        /// Constructor for use Dynamic Endpoint transparency
        /// <remarks>
        /// This constructor is created Adhoc not by svcutil
        /// </remarks>
        /// </summary>
        /// <param name="endpoint">dynamic endpoint with criteria to discover this service</param>
        public MainModuleServiceClient(System.ServiceModel.Description.ServiceEndpoint endpoint)
            : base(endpoint)
        {
        }
    }
}
