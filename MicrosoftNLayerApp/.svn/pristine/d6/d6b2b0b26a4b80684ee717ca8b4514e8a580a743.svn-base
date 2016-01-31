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
using Microsoft.Samples.NLayerApp.Domain.Core;
using Microsoft.Samples.NLayerApp.Domain.MainModule.Entities;
using Microsoft.Samples.NLayerApp.Domain.Core.Specification;

namespace Microsoft.Samples.NLayerApp.Domain.MainModule.Orders
{
    /// <summary>
    /// Adhoc specification for finding orders 
    /// by customer identifier
    /// </summary>
    public class OrderFromCustomerSpecification
        :Specification<Order>
    {
        #region Members

        int _CustomerId = default(Int32);

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor for this customer
        /// </summary>
        /// <param name="customerId">Customer identifier</param>
        public OrderFromCustomerSpecification(int customerId)
        {
            _CustomerId = customerId;
        }

        #endregion

        #region Specification overrides

        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.Domain.Core.Specification.Specification{TEntity}"/>
        /// </summary>
        /// <returns><see cref="Microsoft.Samples.NLayerApp.Domain.Core.Specification.Specification{TEntity}"/></returns>
        public override System.Linq.Expressions.Expression<Func<Order, bool>> SatisfiedBy()
        {
            Specification<Order> spec = new TrueSpecification<Order>();

            spec &= new DirectSpecification<Order>(order => order.CustomerId == _CustomerId);

            return spec.SatisfiedBy();
        }

        #endregion
    }
}
