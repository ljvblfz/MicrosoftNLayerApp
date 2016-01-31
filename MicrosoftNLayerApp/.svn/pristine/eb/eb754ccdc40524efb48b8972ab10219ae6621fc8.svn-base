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
    /// BankAccount information
    /// </summary>
    [DataContract(Name = "BankAccountInformation", Namespace = "Microsoft.Samples.NLayerApp.DistributedServices.MainModuleService")]
    public class BankAccountInformation
    {
        /// <summary>
        /// Bank Account number
        /// </summary>
        [DataMember(Name = "BankAccountNumber")]
        public string BankAccountNumber { get; set; }

        /// <summary>
        /// Customer identifier owner of accounts
        /// </summary>
        [DataMember(Name="CustomerName")]
        public string CustomerName { get; set; }
    }
}
