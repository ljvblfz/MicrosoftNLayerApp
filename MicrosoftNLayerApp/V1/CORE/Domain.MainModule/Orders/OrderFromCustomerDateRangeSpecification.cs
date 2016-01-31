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


using Microsoft.Samples.NLayerApp.Domain.MainModule.Entities;
using Microsoft.Samples.NLayerApp.Domain.Core.Specification;

namespace Microsoft.Samples.NLayerApp.Domain.MainModule.Orders
{
    /// <summary>
    /// AdHoc specification for finding
    /// orders for specific customers
    /// in a range of date
    /// </summary>
    public  class OrderFromCustomerDateRangeSpecification
        :Specification<Order>
    {
        #region Members

        int _CustomerId = default(Int32);
        DateTime? _FromDate = null;
        DateTime? _ToDate = null;
        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor for this specification
        /// </summary>
        /// <param name="customerId">A customer identifier</param>
        /// <param name="from">Orders from this date</param>
        /// <param name="to">Orders to this date</param>
        public OrderFromCustomerDateRangeSpecification(int customerId, DateTime? from, DateTime? to)
        {
            _CustomerId = customerId;
            _FromDate = from;
            _ToDate = to;
        }

        #endregion

        #region Specification overrides

        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.Domain.Core.Specification.Specification{TEntity}"/>
        /// </summary>
        /// <returns><see cref="Microsoft.Samples.NLayerApp.Domain.Core.Specification.Specification{TEntity}"/></returns>
        public override System.Linq.Expressions.Expression<Func<Order, bool>> SatisfiedBy()
        {
            Specification<Order> order = new OrderFromCustomerSpecification(_CustomerId) && new OrderDateSpecification(_FromDate, _ToDate);

            return order.SatisfiedBy();
        }

        #endregion
    }
}
