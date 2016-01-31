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
using System.Linq;
using System.Text;
using System.Linq.Expressions;

using Microsoft.Samples.NLayerApp.Domain.MainModule.Entities;
using Microsoft.Samples.NLayerApp.Domain.Core.Specification;



namespace Microsoft.Samples.NLayerApp.Domain.MainModule.Customers
{
    /// <summary>
    /// Spec for get countries by country name
    /// </summary>
    public class CountryNameSpecification
        :Specification<Country>
    {

        #region Members

        string _countryName = default(String);

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="countryName">Country name that result match</param>
        public CountryNameSpecification(string countryName)
        {
            _countryName = countryName;
        }
        #endregion

        #region Specification<Country> Members

        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.Domain.Core.Specification.Specification{Country}"/>
        /// </summary>
        /// <returns><see cref="Microsoft.Samples.NLayerApp.Domain.Core.Specification.Specification{Country}"/></returns>
        public override System.Linq.Expressions.Expression<Func<Country, bool>> SatisfiedBy()
        {
            Specification<Country> spec = new TrueSpecification<Country>();

            if (!String.IsNullOrEmpty(_countryName)
                &&
                !String.IsNullOrWhiteSpace(_countryName))
            {
                //construct expression and ad new condition to this specification
                Expression<Func<Country, bool>> expression = country => country.CountryName.ToLower() == _countryName.ToLower();
 
                spec &= new DirectSpecification<Country>(expression);
            }


            return spec.SatisfiedBy();
        }

        #endregion
    }
}
