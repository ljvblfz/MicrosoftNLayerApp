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

namespace Microsoft.Samples.NLayerApp.Domain.MainModule.BankAccounts
{
    /// <summary>
    /// Search specification for bankAccounts
    /// </summary>
    public class BankAccountSearchSpecification
        :Specification<BankAccount>
    {
        #region Members

        string _CustomerName;
        string _BankAccountNumber;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor for this specification
        /// </summary>
        /// <param name="bankAccountNumber">BankAccount number</param>
        /// <param name="customerName">A Customer identifier</param>
        public BankAccountSearchSpecification(string bankAccountNumber, string customerName)
        {
            _CustomerName = customerName;
            _BankAccountNumber = bankAccountNumber;
        }

        #endregion

        #region Override Specification

        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.Domain.Core.Specification.Specification{BankAccount}"/>
        /// </summary>
        /// <returns><see cref="Microsoft.Samples.NLayerApp.Domain.Core.Specification.Specification{BankAccount}"/></returns>
        public override System.Linq.Expressions.Expression<Func<BankAccount, bool>> SatisfiedBy()
        {
            Specification<BankAccount> spec = new TrueSpecification<BankAccount>();

            if (!String.IsNullOrEmpty(_BankAccountNumber)
                &&
                !String.IsNullOrWhiteSpace(_BankAccountNumber))
            {
                spec &= new BankAccountNumberSpecification(_BankAccountNumber);
            }
            if (!String.IsNullOrEmpty(_CustomerName)
                &&
                !String.IsNullOrWhiteSpace(_CustomerName))
            {
                spec &= new DirectSpecification<BankAccount>(ba => ba.Customer.ContactName.ToLower().Contains(_CustomerName.ToLower()));
            }

            return spec.SatisfiedBy();
        }

        #endregion
    }
}
