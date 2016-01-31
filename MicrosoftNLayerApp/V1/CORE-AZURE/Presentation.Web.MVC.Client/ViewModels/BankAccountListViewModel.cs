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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.Samples.NLayerApp.Domain.MainModule.Entities;

namespace Microsoft.Samples.NLayerApp.Presentation.Web.MVC.Client.ViewModels
{
    /// <summary>
    /// A Viewmodel to display a list of bank accounts.
    /// </summary>
    public class BankAccountListViewModel : IEnumerable<BankAccount>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BankAccountListViewModel"/> class.
        /// </summary>
        /// <param name="bankAccounts">The bank accounts.</param>
        public BankAccountListViewModel(IEnumerable<BankAccount> bankAccounts)
        {
            BankAccounts = bankAccounts;
        }

        #endregion

        #region Members

        /// <summary>
        /// Gets the bank accounts.
        /// </summary>
        /// <value>The bank accounts.</value>
        [UIHint("IEnumerable")]
        public IEnumerable<BankAccount> BankAccounts { get; private set; }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<BankAccount> GetEnumerator()
        {
            return BankAccounts.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return BankAccounts.GetEnumerator();
        }

        #endregion
    }
}