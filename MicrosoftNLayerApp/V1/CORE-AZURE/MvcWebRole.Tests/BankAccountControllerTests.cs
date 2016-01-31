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
using System.Linq;
using System.Web.Mvc;

using Microsoft.Samples.NLayerApp.Application.MainModule.BankingManagement.Moles;
using Microsoft.Samples.NLayerApp.Domain.MainModule.Entities;
using Microsoft.Samples.NLayerApp.Presentation.Web.MVC.Client.Controllers;
using Microsoft.Samples.NLayerApp.Presentation.Web.MVC.Client.ViewModels;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Asp.NetMVC.Client.Tests
{
    [TestClass]
    public class BankAccountControllerTests
    {
        #region Helper Methods

        IList<BankTransfer> GetFakeTransfers()
        {
            //return a dummy list of transfers
            return new List<BankTransfer>()
            {
                new BankTransfer(){ BankTransferId = 1},
                new BankTransfer(){ BankTransferId = 2},
                new BankTransfer(){ BankTransferId = 3},
                new BankTransfer(){ BankTransferId = 4},
                new BankTransfer(){ BankTransferId = 5},
                new BankTransfer(){ BankTransferId = 6},
                new BankTransfer(){ BankTransferId = 7},
                new BankTransfer(){ BankTransferId = 8},
                new BankTransfer(){ BankTransferId = 9},
                new BankTransfer(){ BankTransferId = 10},
                new BankTransfer(){ BankTransferId = 11},
            };
        }
        IList<BankAccount> GetFakeBankAccounts()
        {
            
            int seedId = 0;
            decimal seedBalance = 0;
            bool seedLock = false;
            
            return new List<BankAccount>()
            {
                new BankAccount(){ BankAccountId = ++seedId, Balance = (seedBalance += 50), Locked = (seedLock = !seedLock)},
                new BankAccount(){ BankAccountId = ++seedId, Balance = (seedBalance += 50), Locked = (seedLock = !seedLock)},
                new BankAccount(){ BankAccountId = ++seedId, Balance = (seedBalance += 50), Locked = (seedLock = !seedLock)},
                new BankAccount(){ BankAccountId = ++seedId, Balance = (seedBalance += 50), Locked = (seedLock = !seedLock)},
                new BankAccount(){ BankAccountId = ++seedId, Balance = (seedBalance += 50), Locked = (seedLock = !seedLock)},
                new BankAccount(){ BankAccountId = ++seedId, Balance = (seedBalance += 50), Locked = (seedLock = !seedLock)},
                new BankAccount(){ BankAccountId = ++seedId, Balance = (seedBalance += 50), Locked = (seedLock = !seedLock)},
                new BankAccount(){ BankAccountId = ++seedId, Balance = (seedBalance += 50), Locked = (seedLock = !seedLock)},
                new BankAccount(){ BankAccountId = ++seedId, Balance = (seedBalance += 50), Locked = (seedLock = !seedLock)},
                new BankAccount(){ BankAccountId = ++seedId, Balance = (seedBalance += 50), Locked = (seedLock = !seedLock)},
            };

        }
        #endregion

        #region Tests Methods

        [TestMethod]
        public void Index_Action_Returns_The_List_Of_Bank_Trasnfers()
        {

            /*
             *   Arrange 
             * 1º - Create a dummy list of transfers
             * 2º - Initialize stub of IBankingManagementService
             * 3º - Create controller to test
             */ 

            IList<BankTransfer> transfers = GetFakeTransfers();

          
            SIBankingManagementService bankingService = new SIBankingManagementService();
            bankingService.FindBankTransfersInt32Int32 = (page, pageSize)
                                                         =>
                                                         {
                                                             return transfers.Skip(page * pageSize).Take(pageSize).ToList();
                                                         };
           
            BankAccountController controller = new BankAccountController(bankingService);


            //Act

            ViewResultBase result = controller.Index(0, 5) as ViewResult;


            //Assert
            Assert.IsNotNull(result, "The action result is null or is not an instance of ViewResult");

            TransfersListViewModel model = result.ViewData.Model as TransfersListViewModel;

            Assert.IsNotNull(model, "The model is null or is not an instance of TransfersListViewModel");

            IList<BankTransfer> modelTransfers = (model.PageItems as IEnumerable<BankTransfer>).ToList();

        }

        [TestMethod]
        public void Transfer_Money_Shows_A_List_Of_Bank_Accounts()
        {
            /*
             *   Arrange 
             * 1º - Create a dummy list of transfers
             * 2º - Initialize stub of IBankingManagementService
             * 3º - Create controller to test
             */ 

            IList<BankAccount> accounts = GetFakeBankAccounts();

            SIBankingManagementService bankingService = new SIBankingManagementService();
            bankingService.FindPagedBankAccountsInt32Int32 = (page, pageSize) => accounts.Skip(page * pageSize).Take(pageSize).ToList();

            BankAccountController controller = new BankAccountController(bankingService);

            //Act
            ViewResultBase result = controller.TransferMoney() as ViewResult;

            //Assert
            Assert.IsNotNull(result);

            BankAccountListViewModel model = result.ViewData.Model as BankAccountListViewModel;
            Assert.IsNotNull(model);
        }

        [TestMethod]
        public void Transfer_Money_Performs_The_Transfer_Of_The_Given_Amount_Between_The_Given_Accounts()
        {
             /*
              *   Arrange 
              * 1º - Create a fake data
              * 2º - Initialize stub of IBankingManagementService
              * 3º - Create controller to test
              */ 
            decimal transferAmount = 50;
            string sourceAccount = "eX325";
            string targetAccount = "eX287";

          

            SIBankingManagementService bankingService = new SIBankingManagementService();
            bankingService.PerformTransferStringStringDecimal = (source, target, amount) => { };
            
            BankAccountController controller = new BankAccountController(bankingService);

            //Act
            RedirectToRouteResult result = controller.TransferMoney(sourceAccount, targetAccount, transferAmount) as RedirectToRouteResult;

            //Assert
            Assert.IsNotNull(result, "Expected a RedirectToRouteResult");

            Assert.AreEqual("TransferMoney", result.RouteValues["action"], "Expected a redirection to TransferMoney");

           
        }

        [TestMethod]
        public void Lock_Account_Locks_The_Given_Account()
        {
             /*
              *   Arrange 
              * 1º - Create a fake data
              * 2º - Initialize stub of IBankingManagementService
              * 3º - Create controller to test
              */

            string accountNumber = "EX325";
            BankAccount account = new BankAccount() { BankAccountNumber = accountNumber, Locked = false };

        
            SIBankingManagementService bankingService = new SIBankingManagementService();
            bankingService.FindBankAccountByNumberString = accNumber => account;
            bankingService.ChangeBankAccountBankAccount = bankAccount => { };
           
            BankAccountController controller = new BankAccountController(bankingService);


            //Act
            RedirectToRouteResult result = controller.LockAccount(accountNumber) as RedirectToRouteResult;
            
            //Assert
            Assert.IsNotNull(result, "Expected a RedirectToRouteResult");
            Assert.AreEqual("TransferMoney", result.RouteValues["action"], "Expected a redirection to TransferMoney");

        }

        #endregion
    }
}
