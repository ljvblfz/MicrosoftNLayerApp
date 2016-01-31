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
using System.Collections.Generic;

using Microsoft.Samples.NLayerApp.Domain.MainModule.Entities;
using Microsoft.Samples.NLayerApp.Domain.Core;

namespace Microsoft.Samples.NLayerApp.Domain.MainModule.BankAccounts
{
    /// <summary>
    /// Contract for bank account aggregate root repository
    /// </summary>
    public interface IBankAccountRepository
        :IRepository<BankAccount>
    {
        
        /// <summary>
        /// Find all bank accounts in page with bank transfer information
        /// </summary>
        /// <returns>A collection of bank account with transfer information</returns>
        IEnumerable<BankAccount> FindPagedBankAccountsWithTransferInformation(int pageIndex,int pageCount);
    }
}
