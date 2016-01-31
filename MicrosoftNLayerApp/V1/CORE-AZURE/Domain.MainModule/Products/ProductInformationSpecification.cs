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

namespace Microsoft.Samples.NLayerApp.Domain.MainModule.Products
{
    /// <summary>
    /// Adhoc specification for finding values by 
    /// publisher and description information
    /// </summary>
    public class ProductInformationSpecification
        :Specification<Product>
    {
        #region Members

        string _Publisher = default(String);
        string _Description = default(string);

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor for this specification
        /// </summary>
        /// <param name="productPublisher">Publisher of product or null to not include this value in search process</param>
        /// <param name="productDescription">Product description or null to not inclide this value in search process</param>
        public ProductInformationSpecification(string productPublisher, string productDescription)
        {
            _Publisher = productPublisher;
            _Description = productDescription;
        }

        #endregion

        #region Specification overrides

        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.Domain.Core.Specification.Specification{TEntity}"/>
        /// </summary>
        /// <returns><see cref="Microsoft.Samples.NLayerApp.Domain.Core.Specification.Specification{TEntity}"/></returns>
        public override System.Linq.Expressions.Expression<Func<Product, bool>> SatisfiedBy()
        {
            Specification<Product> beginSpec = new TrueSpecification<Product>();

            if ( _Publisher != null )
                beginSpec &= new DirectSpecification<Product>(p=>p.Publisher != null &&p.Publisher.Contains(_Publisher));

            if (_Description != null)
                beginSpec &= new DirectSpecification<Product>(p => p.ProductDescription != null &&  p.ProductDescription.Contains(_Description));

            return beginSpec.SatisfiedBy();
        }

        #endregion
    }
}
