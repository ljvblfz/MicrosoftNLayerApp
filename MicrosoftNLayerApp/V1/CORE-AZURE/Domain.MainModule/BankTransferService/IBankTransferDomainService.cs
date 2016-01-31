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
using Microsoft.Samples.NLayerApp.Domain.MainModule.Entities;

namespace Microsoft.Samples.NLayerApp.Domain.MainModule.BankAccounts
{
    /// <summary>
    /// Contract for Domain Bank Transfer Service
    /// </summary>
    public interface IBankTransferDomainService
    {
        /// <summary>
        /// Performs money transfer between two valid accounts
        /// </summary>
        /// <param name="originAccount">Origin account where money is charged</param>
        /// <param name="destinationAccount">Destination account where money is credited</param>
        /// <param name="amount">Amount to transfer between accounts</param>
        void PerformTransfer(BankAccount originAccount, BankAccount destinationAccount, decimal amount);
                        
    }
}
