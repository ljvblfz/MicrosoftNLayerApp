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
using System.Transactions;

using Microsoft.Samples.NLayerApp.Domain.Core;
using Microsoft.Samples.NLayerApp.Domain.Core.Entities;
using Microsoft.Samples.NLayerApp.Domain.MainModule.BankAccounts;
using Microsoft.Samples.NLayerApp.Domain.MainModule.Entities;
using Microsoft.Samples.NLayerApp.Domain.Core.Specification;

namespace Microsoft.Samples.NLayerApp.Application.MainModule.BankingManagement
{
    /// <summary>
    /// IBankingManagementService implementation
    /// <see cref="Microsoft.Samples.NLayerApp.Application.MainModule.BankingManagement.IBankingManagementService"/>
    /// </summary>
    public class BankingManagementService
        :IBankingManagementService
    {
        #region Members
        
        IBankTransferDomainService _bankTransferDomainService;
        IBankAccountRepository _bankAccountRepository;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor for this service
        /// </summary>
        ///<param name="bankTransferDomainService">Bank transfer domain service dependency</param>
        /// <param name="bankAccountRepository">Bank account repository dependency</param>
        public BankingManagementService(IBankTransferDomainService bankTransferDomainService, IBankAccountRepository bankAccountRepository)
        {
            if (bankTransferDomainService == (IBankTransferDomainService)null)
                throw new ArgumentNullException("bankTransferDomainService", Resources.Messages.exception_DependenciesAreNotInitialized);

            if (bankAccountRepository == (IBankAccountRepository)null)
                throw new ArgumentNullException("bankAccountRepository", Resources.Messages.exception_DependenciesAreNotInitialized);

            _bankTransferDomainService = bankTransferDomainService;
            _bankAccountRepository = bankAccountRepository;
        }

        #endregion

        #region IBankingManagementService Members

        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.Application.MainModule.BankingManagement.IBankingManagementService"/>
        /// </summary>
        /// <param name="fromAccountNumber"><see cref="Microsoft.Samples.NLayerApp.Application.MainModule.BankingManagement.IBankingManagementService"/></param>
        /// <param name="toAccountNumber"><see cref="Microsoft.Samples.NLayerApp.Application.MainModule.BankingManagement.IBankingManagementService"/></param>
        /// <param name="amount"><see cref="Microsoft.Samples.NLayerApp.Application.MainModule.BankingManagement.IBankingManagementService"/></param>
        public void PerformTransfer(string fromAccountNumber, string toAccountNumber, decimal amount)
        {
            //Process: 1º Start Transaction
            //         2º Get Accounts objects from Repositories
            //         3º Call PerformTransfer method in Domain Service
            //         4º If no exceptions, save changes using repositories and Commit Transaction
            
            //Create a transaction context for this operation
            TransactionOptions txSettings = new TransactionOptions()
            {
                Timeout = TransactionManager.DefaultTimeout,
                IsolationLevel = IsolationLevel.Serializable // review this option
            };

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
            {
                //Get Unit of Work
                IUnitOfWork unitOfWork = _bankAccountRepository.UnitOfWork as IUnitOfWork;

                //Create Queries' Specifications
                BankAccountNumberSpecification originalAccountQuerySpec = new BankAccountNumberSpecification(fromAccountNumber);
                BankAccountNumberSpecification destinationAccountQuerySpec = new BankAccountNumberSpecification(toAccountNumber);

                //Query Repositories to get accounts
                BankAccount originAccount = _bankAccountRepository.GetBySpec(originalAccountQuerySpec as ISpecification<BankAccount>)
                                                                  .SingleOrDefault();

                BankAccount destinationAccount = _bankAccountRepository.GetBySpec(destinationAccountQuerySpec as ISpecification<BankAccount>)
                                                                  .SingleOrDefault();

                if (originAccount == null || destinationAccount == null)
                    throw new InvalidOperationException(Resources.Messages.exception_InvalidAccountsForTransfer);

                ////Start tracking STE entities (Self Tracking Entities)
                originAccount.StartTrackingAll();
                destinationAccount.StartTrackingAll();

                //Excute Domain Logic for the Transfer (In Domain Service) 
                _bankTransferDomainService.PerformTransfer(originAccount, destinationAccount, amount);


                //Save changes and commit operations. 
                //This opeation is problematic with concurrency.
                //"balance" property in bankAccount is configured 
                //to FIXED in "WHERE concurrency checked predicates"

                _bankAccountRepository.Modify(originAccount);
                _bankAccountRepository.Modify(destinationAccount);

                //Complete changes in this Unit of Work
                unitOfWork.CommitAndRefreshChanges();

                //Commit the transaction
                scope.Complete();
            }
        }
        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.Application.MainModule.BankingManagement.IBankingManagementService"/>
        /// </summary>
        /// <param name="pageIndex"><see cref="Microsoft.Samples.NLayerApp.Application.MainModule.BankingManagement.IBankingManagementService"/></param>
        /// <param name="pageCount"><see cref="Microsoft.Samples.NLayerApp.Application.MainModule.BankingManagement.IBankingManagementService"/></param>
        /// <returns><see cref="Microsoft.Samples.NLayerApp.Application.MainModule.BankingManagement.IBankingManagementService"/></returns>
        public List<BankTransfer> FindBankTransfers(int pageIndex, int pageCount)
        {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");
            
                                    
            //Query BankAccount Repository to get Transfers related
            return _bankAccountRepository.FindPagedBankAccountsWithTransferInformation(pageIndex,pageCount)
                                         .SelectMany(ba => ba.BankTransfersFromThis)
                                         .ToList();
        }
        
        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.Application.MainModule.BankingManagement.IBankingManagementService"/>
        /// </summary>
        /// <param name="bankAccount"><see cref="Microsoft.Samples.NLayerApp.Application.MainModule.BankingManagement.IBankingManagementService"/></param>
        public void AddBankAccount(BankAccount bankAccount)
        {
            if (bankAccount == (BankAccount)null)
                throw new ArgumentNullException("bankAccount");

            IUnitOfWork unitOfWork = _bankAccountRepository.UnitOfWork as IUnitOfWork;



            _bankAccountRepository.Add(bankAccount);

            //complete changes in this unit of work
            unitOfWork.Commit();
        }
        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.Application.MainModule.BankingManagement.IBankingManagementService"/>
        /// </summary>
        /// <param name="bankAccount"><see cref="Microsoft.Samples.NLayerApp.Application.MainModule.BankingManagement.IBankingManagementService"/></param>
        public void ChangeBankAccount(BankAccount bankAccount)
        {
            if (bankAccount == (BankAccount)null)
                throw new ArgumentNullException("bankAccount");

            IUnitOfWork unitOfWork = _bankAccountRepository.UnitOfWork as IUnitOfWork;

            //charge in balance and commit operation. This opeation is 
            //problematic with concurrency. "balance" propety in bankAccount
            //is configured to FIXED in "WHERE concurrency checked predicates"

            _bankAccountRepository.Modify(bankAccount);

            //complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();

        }
        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.Application.MainModule.BankingManagement.IBankingManagementService"/>
        /// </summary>
        /// <param name="bankAccountNumber"><see cref="Microsoft.Samples.NLayerApp.Application.MainModule.BankingManagement.IBankingManagementService"/></param>
        /// <returns><see cref="Microsoft.Samples.NLayerApp.Application.MainModule.BankingManagement.IBankingManagementService"/></returns>
        public BankAccount FindBankAccountByNumber(string bankAccountNumber)
        {
            BankAccountNumberSpecification specification = new BankAccountNumberSpecification(bankAccountNumber);

            //query repository
            return _bankAccountRepository.GetBySpec(specification as ISpecification<BankAccount>).SingleOrDefault();
        }
        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.Application.MainModule.BankingManagement.IBankingManagementService"/>
        /// </summary>
        /// <param name="pageIndex"><see cref="Microsoft.Samples.NLayerApp.Application.MainModule.BankingManagement.IBankingManagementService"/></param>
        /// <param name="pageCount"><see cref="Microsoft.Samples.NLayerApp.Application.MainModule.BankingManagement.IBankingManagementService"/></param>
        /// <returns><see cref="Microsoft.Samples.NLayerApp.Application.MainModule.BankingManagement.IBankingManagementService"/></returns>
        public List<BankAccount> FindPagedBankAccounts(int pageIndex, int pageCount)
        {
            return _bankAccountRepository.GetPagedElements(pageIndex, pageCount,b=>b.BankAccountNumber,true)
                                         .ToList();
        }
        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.Application.MainModule.BankingManagement.IBankingManagementService"/>
        /// </summary>
        /// <param name="bankAccountNumber"><see cref="Microsoft.Samples.NLayerApp.Application.MainModule.BankingManagement.IBankingManagementService"/></param>
        /// <param name="customerName"><see cref="Microsoft.Samples.NLayerApp.Application.MainModule.BankingManagement.IBankingManagementService"/></param>
        /// <returns><see cref="Microsoft.Samples.NLayerApp.Application.MainModule.BankingManagement.IBankingManagementService"/></returns>
        public List<BankAccount> FindBankAccounts(string bankAccountNumber, string customerName)
        {
            BankAccountSearchSpecification specification = new BankAccountSearchSpecification(bankAccountNumber, customerName);

            //query repository
            return _bankAccountRepository.GetBySpec(specification as ISpecification<BankAccount>).ToList();
        }

        #endregion
    }
}
