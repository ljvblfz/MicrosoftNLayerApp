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
using System.Collections.Generic;

using Microsoft.Samples.NLayerApp.Domain.Core;
using Microsoft.Samples.NLayerApp.Domain.MainModule.Entities;


namespace Microsoft.Samples.NLayerApp.Domain.MainModule.Orders
{
    /// <summary>
    /// Contract for Order Aggregate Root Repository
    /// </summary>
    public interface IOrderRepository
        :IRepository<Order>
    {
        /// <summary>
        /// Find orders by customer code
        /// </summary>
        /// <param name="customerCode">A Customer code </param>
        /// <returns>Collection of order that matching criteria</returns>
        IEnumerable<Order> FindOrdersByCustomerCode(string customerCode);
    }
}
