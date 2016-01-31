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

namespace Microsoft.Samples.NLayerApp.Domain.MainModule.Customers
{
    /// <summary>
    /// AdHoc specification for find customres by code criteria
    /// For more information about specification <see cref=" Microsoft.Samples.NLayerApp.Domain.Core.Specification.ISpecification{TEntity}"/>
    /// </summary>
    public class CustomerCodeSpecification
        :Specification<Customer>
    {
        #region Members

        string _CustomerCode = null;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="customerCode">A Customer code detail</param>
        public CustomerCodeSpecification(string customerCode)
        {
            if (string.IsNullOrEmpty(customerCode)
                ||
                string.IsNullOrWhiteSpace(customerCode)
               )
            {
                throw new ArgumentNullException("customerCode");
            }

            this._CustomerCode = customerCode;
        }

        #endregion

        #region Specification overrides

        /// <summary>
        /// <see cref=" Microsoft.Samples.NLayerApp.Domain.Core.Specification.Specification{TEntity}"/>
        /// </summary>
        /// <returns><see cref=" Microsoft.Samples.NLayerApp.Domain.Core.Specification.Specification{TEntity}"/></returns>
        public override System.Linq.Expressions.Expression<Func<Customer, bool>> SatisfiedBy()
        {
            Specification<Customer> spec = new TrueSpecification<Customer>();

            //only enabled customers
            spec &= new DirectSpecification<Customer>(t => t.IsEnabled);

            //customer code spec
            spec &= new DirectSpecification<Customer>(c => c.CustomerCode != null && (c.CustomerCode.ToUpper() == _CustomerCode.ToUpper()));

            return spec.SatisfiedBy();
        }

        #endregion
    }
}
