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
using System.Transactions;

using Microsoft.Samples.NLayerApp.Domain.Core.Entities;
using Microsoft.Samples.NLayerApp.Domain.MainModule.BankAccounts;
using Microsoft.Samples.NLayerApp.Domain.MainModule.Entities;

namespace Microsoft.Samples.NLayerApp.Domain.MainModule.BankAccounts
{
    /// <summary>
    /// <see cref="Microsoft.Samples.NLayerApp.Domain.MainModule.BankAccounts.IBankTransferDomainService"/>
    /// </summary>
    public class BankTransferDomainService
        : IBankTransferDomainService
    {

        #region Constructor

        /// <summary>
        /// Default constructor for this service
        /// </summary>
        public BankTransferDomainService()
        {
        }

        #endregion

        #region IBankTransferService Implementation

        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.Domain.MainModule.BankAccounts.IBankTransferDomainService"/>
        /// </summary>
        /// <param name="originAccount"><see cref="Microsoft.Samples.NLayerApp.Domain.MainModule.BankAccounts.IBankTransferDomainService"/></param>
        /// <param name="destinationAccount"><see cref="Microsoft.Samples.NLayerApp.Domain.MainModule.BankAccounts.IBankTransferDomainService"/></param>
        /// <param name="amount"><see cref="Microsoft.Samples.NLayerApp.Domain.MainModule.BankAccounts.IBankTransferDomainService"/></param>
        public void PerformTransfer(BankAccount originAccount, BankAccount destinationAccount, decimal amount)
        {
            //Domain Logic
            //Process: Perform transfer operations to in-memory Domain-Model Objects        
            // 1.- Charge money to origin acc
            // 2.- Credit money to destination acc
            // 3.- Annotate transfer to origin account

            //Number Accounts must be different
            if (originAccount.BankAccountNumber != destinationAccount.BankAccountNumber)
            {
                //1. Charge to origin account (Domain Logic)
                originAccount.ChargeMoney(amount);

                //2. Credit to destination account (Domain Logic)
                destinationAccount.CreditMoney(amount);

                //3. Anotate transfer to related origin account                
                originAccount.BankTransfersFromThis.Add(new BankTransfer()
                {
                    Amount = amount,
                    TransferDate = DateTime.UtcNow,
                    ToBankAccountId = destinationAccount.BankAccountId
                });
            }
            else
                throw new InvalidOperationException(Resources.Messages.exception_InvalidAccountsForTransfer);

        }

        #endregion

    }
}
