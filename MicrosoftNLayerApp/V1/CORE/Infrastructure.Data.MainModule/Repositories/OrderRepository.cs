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
using System.Globalization;
using System.Linq;

using Microsoft.Samples.NLayerApp.Domain.MainModule.Entities;
using Microsoft.Samples.NLayerApp.Domain.MainModule.Orders;
using Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.Logging;
using Microsoft.Samples.NLayerApp.Infrastructure.Data.Core;
using Microsoft.Samples.NLayerApp.Infrastructure.Data.MainModule.Resources;
using Microsoft.Samples.NLayerApp.Infrastructure.Data.MainModule.UnitOfWork;


namespace Microsoft.Samples.NLayerApp.Infrastructure.Data.MainModule.Repositories
{
    /// <summary>
    /// IOrderRepository implementation
    /// <see cref="Microsoft.Samples.NLayerApp.Domain.MainModule.Orders.IOrderRepository"/>
    /// </summary>
    public class OrderRepository
        :Repository<Order>,IOrderRepository
    {
        #region Constructor

        /// <summary>
        /// Default constructor for this repository
        /// </summary>
        /// <param name="unitOfWork">unitOfWork dependency, intented to be resolved with IoC</param>
        /// <param name="traceManager">ITraceManager dependency, intended to be resolved wiht IoC</param>
        public OrderRepository(IMainModuleUnitOfWork unitOfWork, ITraceManager traceManager) : base(unitOfWork, traceManager) { }

        #endregion

        #region IOrderRepository Implementation

        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.Domain.MainModule.Orders.IOrderRepository"/>
        /// </summary>
        /// <param name="customerCode"><see cref="Microsoft.Samples.NLayerApp.Domain.MainModule.Orders.IOrderRepository"/></param>
        /// <returns><see cref="Microsoft.Samples.NLayerApp.Domain.MainModule.Orders.IOrderRepository"/></returns>
        public IEnumerable<Order> FindOrdersByCustomerCode(string customerCode)
        {
            if (String.IsNullOrEmpty(customerCode)
                ||
                String.IsNullOrWhiteSpace(customerCode)
                )
            {
                throw new ArgumentNullException("customerCode");
            }

            //
            // Other valid query for this case is base.GetFilteredElements(o => o.Customer.CustomerCode == customerCode);
            // In this case also you can use OrderFromCustomerSpecification but for sample purposed this method
            // is created using linq to entities implementation
            //

            IMainModuleUnitOfWork actualContext = base.UnitOfWork as IMainModuleUnitOfWork;
            if (actualContext != null)
            {
                return (from order
                            in actualContext.Orders
                        where
                           order.Customer.CustomerCode == customerCode
                        select
                           order).AsEnumerable();
            }
            else
                throw new InvalidOperationException(string.Format(
                                                                CultureInfo.InvariantCulture,
                                                                Messages.exception_InvalidStoreContext,
                                                                this.GetType().Name));
        }

        #endregion
    }
}
