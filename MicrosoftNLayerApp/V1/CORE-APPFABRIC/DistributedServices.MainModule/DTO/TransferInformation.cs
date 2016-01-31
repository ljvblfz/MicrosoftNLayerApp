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
    /// Transfer Information DTO
    /// </summary>
    [DataContract(Name = "TransferInformation", Namespace = "Microsoft.Samples.NLayerApp.DistributedServices.MainModuleService")]
    public class TransferInformation
    {
        /// <summary>
        ///Origin account number in this transfer information
        /// </summary>
        [DataMember(Name="OriginAccountNumber")]
        public string OriginAccountNumber { get; set; }

        /// <summary>
        /// Destination account number in this transfer information
        /// </summary>
        [DataMember(Name="DestinationAccountNumber")]
        public string DestinationAccountNumber { get; set; }

        /// <summary>
        /// Amount of money for this transfer
        /// </summary>
        [DataMember(Name="Amount")]
        public decimal Amount { get; set; }
    }
}
