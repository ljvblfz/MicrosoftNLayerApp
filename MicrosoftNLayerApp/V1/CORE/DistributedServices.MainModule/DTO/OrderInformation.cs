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
using System.Runtime.Serialization;

namespace Microsoft.Samples.NLayerApp.DistributedServices.MainModule.DTO
{
    /// <summary>
    /// Order information
    /// </summary>
    [DataContract(Name = "OrderInformation", Namespace = "Microsoft.Samples.NLayerApp.DistributedServices.MainModuleService")]
    public class OrderInformation
    {
        /// <summary>
        /// Customer identifier of orders
        /// </summary>
        [DataMember(Name="CustomerId")]
        public int CustomerId { get; set; }

        /// <summary>
        /// Orders from this date
        /// </summary>
        [DataMember(Name="DateTimeFrom")]
        public DateTime? DateTimeFrom { get; set; }

        /// <summary>
        /// Orders to this date
        /// </summary>
        [DataMember(Name="DateTimeTo")]
        public DateTime? DateTimeTo { get; set; }
    }
}
