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
using Microsoft.Samples.NLayerApp.Domain.MainModule.Entities.Resources;

namespace Microsoft.Samples.NLayerApp.Domain.MainModule.Entities
{
    /// <summary>
    /// BankAccount domain entity
    /// </summary>
    public partial class BankAccount
    {
        
        /// <summary>
        /// Deduct money to this account
        /// </summary>
        /// <param name="amount">Amount of money to deduct</param>
        public void ChargeMoney(decimal amount)
        {                        
            //Amount to Charge must be greater than 0. --> Domain logic.
            if (amount <= 0)
                throw new ArgumentException(Messages.exception_InvalidArgument, "amount");

            //Account must not be locked, and balance must be greater than cero.
            if (!this.CanBeCharged(amount))
                throw new InvalidOperationException(Resources.Messages.exception_InvalidAccountToBeCharged);

            //Charge means deducting money to this account. --> Domain Logic
            this.Balance -= amount;                
        }

        /// <summary>
        /// Add money to this account
        /// </summary>
        /// <param name="amount">Amount of money to add</param>
        public void CreditMoney(decimal amount)
        {
            //Amount to Credit must be greater than 0. --> Domain logic.
            if (amount <= 0)
                throw new ArgumentException(Messages.exception_InvalidArgument, "amount");

            //Account must not be locked.
            if (this.Locked)
                throw new InvalidOperationException(Resources.Messages.exception_AccountIsLocked);

            //Credit means adding money to this account. --> Domain Logic
            this.Balance += amount;
                
        }


        /// <summary>
        /// Calculate diference between received and sended transfers
        /// </summary>
        /// <returns>Amount</returns>
        public decimal TransferRate(DateTime @from,DateTime to)
        {
            IEnumerable<BankTransfer> resultFrom = from bt
                                                    in this.BankTransfersFromThis
                                                   where
                                                    bt.TransferDate >= @from && bt.TransferDate <= to
                                                   select bt;

            IEnumerable<BankTransfer> resultTo = from bt
                                                  in this.BankTransfersToThis
                                                 where
                                                    bt.TransferDate >= @from && bt.TransferDate <= to
                                                 select bt;

            return resultTo.Sum(bt => bt.Amount) - resultFrom.Sum(bt => bt.Amount);       
        }

        /// <summary>
        /// Check if this bank account is not locked and balance is greater than amount to be charged
        /// </summary>
        /// <param name="amount">Amount of money to charge</param>
        /// <returns>True if this bank account not is locked and balace is greater than <paramref name="amount"/></returns>
        public bool CanBeCharged(decimal amount)
        {  
            return !this.Locked && this.Balance > amount;
        }
    }
}
