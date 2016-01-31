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
    /// Paged criteria
    /// </summary>
    [DataContract(Name = "PagedCriteria", Namespace = "Microsoft.Samples.NLayerApp.DistributedServices.MainModuleService")]
    public  class PagedCriteria
    {
        /// <summary>
        /// Index of page 
        /// </summary>
        [DataMember(Name="PageIndex")]
        public int PageIndex { get; set; }

        /// <summary>
        /// Number of elements in each page
        /// </summary>
        [DataMember(Name = "PageCount")]
        public int PageCount { get; set; }
    }
}
