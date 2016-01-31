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
    /// AdHoc specification for finding BankAccounts for number criteria.
    /// For more information about specification <see cref="Microsoft.Samples.NLayerApp.Domain.Core.Specification.ISpecification{TEntity}"/>
    /// </summary>
    public class BankAccountNumberSpecification
        :Specification<BankAccount>
    {
        #region Members

        string _BankAccountNumber = default(String);

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="bankAccountNumber">Bank account number for this specification</param>
        public BankAccountNumberSpecification(string bankAccountNumber)
        {
            if (string.IsNullOrEmpty(bankAccountNumber)
                ||
                string.IsNullOrWhiteSpace(bankAccountNumber))
            {
                throw new ArgumentNullException("bankAccountNumber");
            }

            _BankAccountNumber = bankAccountNumber;
        }

        #endregion

        #region Specification

        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.Domain.Core.Specification.Specification{BankAccount}"/>
        /// </summary>
        /// <returns><see cref="Microsoft.Samples.NLayerApp.Domain.Core.Specification.Specification{BankAccount}"/></returns>
        public override System.Linq.Expressions.Expression<Func<BankAccount, bool>> SatisfiedBy()
        {
            return ba => ba.BankAccountNumber == _BankAccountNumber;
        }

        #endregion
    }
}
