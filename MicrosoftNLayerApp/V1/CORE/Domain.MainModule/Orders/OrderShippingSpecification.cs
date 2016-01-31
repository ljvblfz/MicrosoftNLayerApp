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
    /// AdHoc specification for finding orders
    /// by shipping values
    /// </summary>
    public class OrderShippingSpecification
        :Specification<Order>
    {
        #region Members

        string _ShippingName = default(String);
        string _ShippingAddress = default(String);
        string _ShippingCity = default(String);
        string _ShippingZip = default(String);

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor for this specification
        /// </summary>
        /// <param name="shippingName">Shipping Name or null for not include this value in search</param>
        /// <param name="shippingAddress">Shipping Address or null for not include this vlaue in search</param>
        /// <param name="shippingCity">Shipping City or null for not include this value in search</param>
        /// <param name="shippingZip">Shipping Zip or null for not include this value in search</param>
        public OrderShippingSpecification(string shippingName,string shippingAddress,string shippingCity,string shippingZip)
        {
            _ShippingName = shippingName;
            _ShippingAddress = shippingAddress;
            _ShippingCity = shippingCity;
            _ShippingZip = shippingZip;
        }

        #endregion

        #region Specification Overrides

        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.Domain.Core.Specification.Specification{TEntity}"/>
        /// </summary>
        /// <returns><see cref="Microsoft.Samples.NLayerApp.Domain.Core.Specification.Specification{TEntity}"/></returns>
        public override System.Linq.Expressions.Expression<Func<Order, bool>> SatisfiedBy()
        {
            Specification<Order> beginSpec = new TrueSpecification<Order>();

            if (_ShippingName != null)
                beginSpec &= new DirectSpecification<Order>(o =>o.ShippingName!=null &&  o.ShippingName.Contains(_ShippingName));

            if (_ShippingAddress != null)
                beginSpec &= new DirectSpecification<Order>(o => o.ShippingAddress !=null && o.ShippingAddress.Contains(_ShippingAddress));

            if (_ShippingCity != null)
                beginSpec &= new DirectSpecification<Order>(o => o.ShippingCity != null && o.ShippingCity.Contains(_ShippingCity));

            if (_ShippingZip != null)
                beginSpec &= new DirectSpecification<Order>(o => o.ShippingZip != null && o.ShippingZip.Contains(_ShippingZip));

            return beginSpec.SatisfiedBy();

        }

        #endregion
    }

}
