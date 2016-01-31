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
using System.Web.Mvc;
using Microsoft.Samples.NLayerApp.Application.MainModule.BankingManagement;
using Microsoft.Samples.NLayerApp.Domain.Core.Entities;
using Microsoft.Samples.NLayerApp.Domain.MainModule.Entities;
using Microsoft.Samples.NLayerApp.Presentation.Web.MVC.Client.ViewModels;

namespace Microsoft.Samples.NLayerApp.Presentation.Web.MVC.Client.Controllers
{
    /// <summary>
    /// This controller handles all the actions related to bank accounts and bank transfers.
    /// </summary>
    [HandleError]
    public class BankAccountController : Controller
    {

        #region Members

        /// <summary>
        /// Instance of the banking service.
        /// </summary>
        private IBankingManagementService _BankingService;

        #endregion

        #region Constructors
        
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="bankingService">Banking service dependency</param>
        public BankAccountController(IBankingManagementService bankingService)
        {
            _BankingService = bankingService;
          
        }

        #endregion

        #region Actions

        /// <summary>
        /// Listing of the transferences performed in the system.
        /// </summary>
        /// <returns>A paged view with the transferences performed in the system.</returns>
        public ActionResult Index(int page, int pageSize)
        {
            IEnumerable<BankTransfer> transfers = _BankingService.FindBankTransfers(page, pageSize);
            return View(new TransfersListViewModel(transfers,page,pageSize));
        }


        /// <summary>
        /// Displays a view to perform transferences between accounts.
        /// </summary>
        /// <returns>A view to perform transferences and lock accounts.</returns>
        public ActionResult TransferMoney()
        {
            BankAccountListViewModel accounts = new BankAccountListViewModel(_BankingService.FindPagedBankAccounts(0, int.MaxValue));
            return View(accounts);
        }

        /// <summary>
        /// Performs the money transference between the two accounts.
        /// </summary>
        /// <param name="sourceAccount">The source account.</param>
        /// <param name="destinationAccount">The destination account.</param>
        /// <param name="amount">The amount.</param>
        /// <returns>A view to perform transferences and lock accounts.</returns>
        [HttpPost]
        public ActionResult TransferMoney(string sourceAccount, string destinationAccount, decimal amount)
        {
            _BankingService.PerformTransfer(sourceAccount, destinationAccount, amount);
            return RedirectToAction("TransferMoney");
        }


        /// <summary>
        /// Locks the account.
        /// </summary>
        /// <param name="accountNumber">The account number.</param>
        /// <returns>A view to perform transferences and lock accounts.</returns>
        [HttpPost]
        public ActionResult LockAccount(string accountNumber)
        {
            BankAccount account = _BankingService.FindBankAccountByNumber(accountNumber);
            account.StartTrackingAll();
            account.Locked = !account.Locked;
            _BankingService.ChangeBankAccount(account);

            return RedirectToAction("TransferMoney");
        }
        
        #endregion

    }
}
