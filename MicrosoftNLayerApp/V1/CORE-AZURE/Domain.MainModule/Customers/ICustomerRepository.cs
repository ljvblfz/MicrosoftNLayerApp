using Microsoft.Samples.NLayerApp.Domain.Core;
using Microsoft.Samples.NLayerApp.Domain.Core.Specification;
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
using Microsoft.Samples.NLayerApp.Domain.MainModule.Entities;

namespace Microsoft.Samples.NLayerApp.Domain.MainModule.Customers
{
    /// <summary>
    /// Contract for Customer Aggregate Root Repository
    /// </summary>
    public interface ICustomerRepository 
        : IRepository<Customer>
    {
        /// <summary>
        /// Find customers by customer specification and include customer picture association
        /// </summary>
        /// <param name="specification">Specification to satisfy</param>
        /// <returns>Selected customer that match specification</returns>
        Customer FindCustomer(ISpecification<Customer> specification);
    }
}
