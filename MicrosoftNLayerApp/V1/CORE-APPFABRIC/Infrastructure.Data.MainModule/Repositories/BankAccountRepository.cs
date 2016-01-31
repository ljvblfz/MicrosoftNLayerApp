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
using System.Globalization;
using System.Linq;

using Microsoft.Samples.NLayerApp.Domain.MainModule.BankAccounts;
using Microsoft.Samples.NLayerApp.Domain.MainModule.Entities;
using Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.Logging;
using Microsoft.Samples.NLayerApp.Infrastructure.Data.Core;
using Microsoft.Samples.NLayerApp.Infrastructure.Data.Core.Extensions;
using Microsoft.Samples.NLayerApp.Infrastructure.Data.MainModule.Resources;
using Microsoft.Samples.NLayerApp.Infrastructure.Data.MainModule.UnitOfWork;

namespace Microsoft.Samples.NLayerApp.Infrastructure.Data.MainModule.Repositories
{
    /// <summary>
    /// IBankAccountRepository implementation
    /// For more information <see cref="Microsoft.Samples.NLayerApp.Domain.MainModule.BankAccounts.IBankAccountRepository"/>
    /// </summary>
    public class BankAccountRepository
        :Repository<BankAccount>,IBankAccountRepository
    {
        #region Constructor

        /// <summary>
        /// Default constructor for this repository
        /// </summary>
        /// <param name="traceManager">ITraceManager dependency, intented to be resolved with IoC</param>
        /// <param name="unitOfWork">IMainModuleUnitOfWork dependency, intented to be resolved with IoC</param>
        public BankAccountRepository(IMainModuleUnitOfWork unitOfWork, ITraceManager traceManager) : base(unitOfWork, traceManager) { }

        #endregion

        #region IBankAccountRepository implementation


        /// <summary>
        ///  <see cref="Microsoft.Samples.NLayerApp.Domain.MainModule.BankAccounts.IBankAccountRepository"/>
        /// </summary>
        /// <param name="pageIndex"> <see cref="Microsoft.Samples.NLayerApp.Domain.MainModule.BankAccounts.IBankAccountRepository"/></param>
        /// <param name="pageCount"> <see cref="Microsoft.Samples.NLayerApp.Domain.MainModule.BankAccounts.IBankAccountRepository"/></param>
        /// <returns> <see cref="Microsoft.Samples.NLayerApp.Domain.MainModule.BankAccounts.IBankAccountRepository"/></returns>
        public IEnumerable<BankAccount> FindPagedBankAccountsWithTransferInformation(int pageIndex, int pageCount)
        {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");

            IMainModuleUnitOfWork context = this.UnitOfWork as IMainModuleUnitOfWork;

            if (context != null)
            {
                return (from ba
                           in context.BankAccounts.Include(ba => ba.BankTransfersFromThis)
                        orderby ba.BankAccountNumber
                        select ba).AsEnumerable();
            }
            else
                throw new InvalidOperationException(string.Format(
                                                                CultureInfo.InvariantCulture,
                                                                Messages.exception_InvalidStoreContext,
                                                                this.GetType().Name));

        }
        #endregion
    }
}
