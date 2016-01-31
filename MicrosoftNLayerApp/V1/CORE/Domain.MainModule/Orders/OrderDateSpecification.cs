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
    /// Adhoc specification for finding order
    /// by OrderDate values
    /// </summary>
    public class OrderDateSpecification
        :Specification<Order>
    {
        #region Members

        DateTime? _FromDate = null;
        DateTime? _ToDate = null;
        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor for OrderDateSpecification
        /// </summary>
        /// <param name="fromDate">From date</param>
        /// <param name="toDate">To date</param>
        public OrderDateSpecification(DateTime? fromDate, DateTime? toDate)
        {
            _FromDate = fromDate;
            _ToDate = toDate;
        }

        #endregion

        #region Specification Overrides

        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.Domain.Core.Specification.Specification{TEntity}"/>
        /// </summary>
        /// <returns><see cref="Microsoft.Samples.NLayerApp.Domain.Core.Specification.Specification{TEntity}"/></returns>
        public override System.Linq.Expressions.Expression<Func<Order, bool>> SatisfiedBy()
        {
            Specification<Order> spec = new TrueSpecification<Order>();

            if ( _FromDate.HasValue )
                spec &= new DirectSpecification<Order>(o => o.OrderDate > (_FromDate ?? DateTime.MinValue));

            if ( _ToDate.HasValue )
                spec &= new DirectSpecification<Order>(o=>o.OrderDate< (_ToDate ?? DateTime.MaxValue));

            return spec.SatisfiedBy();
        }

        #endregion
    }
}
