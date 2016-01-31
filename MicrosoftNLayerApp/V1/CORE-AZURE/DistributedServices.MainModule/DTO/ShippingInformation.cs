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
using System.Runtime.Serialization;

namespace Microsoft.Samples.NLayerApp.DistributedServices.MainModule.DTO
{
    /// <summary>
    /// Shipping information DTO
    /// </summary>
    [DataContract(Name = "ShippingInformation", Namespace = "Microsoft.Samples.NLayerApp.DistributedServices.MainModuleService")]
    public class ShippingInformation
    {
        /// <summary>
        /// Get or set a shipping name
        /// </summary>
        [DataMember(Name="ShippingName")]
        public string ShippingName { get; set; }

        /// <summary>
        /// Get or set shipping address
        /// </summary>
        [DataMember(Name="ShippingAddress")]
        public string ShippingAddress { get; set; }

        /// <summary>
        /// Get or set shipping city
        /// </summary>
        [DataMember(Name="ShippingCity")]
        public string ShippingCity { get; set; }

        /// <summary>
        /// Get or set shipping zip
        /// </summary>
        [DataMember(Name="ShippingZip")]
        public string ShippingZip { get; set; }
    }
}
