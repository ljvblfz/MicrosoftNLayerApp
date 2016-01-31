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

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Microsoft.Samples.NLayerApp.Domain.MainModule.BankAccounts;
using Microsoft.Samples.NLayerApp.Domain.MainModule.Entities;
using Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.Logging;
using Microsoft.Samples.NLayerApp.Infrastructure.Data.MainModule.Repositories;
using Microsoft.Samples.NLayerApp.Infrastructure.Data.MainModule.UnitOfWork;


namespace Microsoft.Samples.NLayerApp.Infrastructure.Data.MainModule.Tests.RepositoriesTests
{
    [TestClass()]
    public class BankAccountRepositoryTests
        :RepositoryTestsBase<BankAccount>
    {
        int bankAccountIdentifier = 1;
        string bankAccountNumber = "BAC0000001";

        public override System.Linq.Expressions.Expression<Func<BankAccount, bool>> FilterExpression
        {
            get { return ba => ba.BankAccountId == bankAccountIdentifier; }
        }

        public override System.Linq.Expressions.Expression<Func<BankAccount, int>> OrderByExpression
        {
            get { return ba => ba.BankAccountId; }
        }

        [TestMethod()]
        public void FindPagedBankAccountsWithTransferInformation_Invoke_Test()
        {
            //Arrange
            IMainModuleUnitOfWork context = this.GetUnitOfWork();
            ITraceManager traceManager = this.GetTraceManager();
            BankAccountRepository repository = new BankAccountRepository(context,traceManager);
            int pageIndex = 0;
            int pageCount = 10;

            //act
            IEnumerable<BankAccount> result = repository.FindPagedBankAccountsWithTransferInformation(pageIndex, pageCount);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() > 0);

        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void FindPagedBankAccountsWithTransferInformation_Invoke_InvalidPageIndexThrowArgumentException_Test()
        {
            //Arrange
            IMainModuleUnitOfWork context = this.GetUnitOfWork();
            ITraceManager traceManager = this.GetTraceManager();
            BankAccountRepository repository = new BankAccountRepository(context,traceManager);
            int pageIndex = -1;
            int pageCount = 10;

            //act
            IEnumerable<BankAccount> result = repository.FindPagedBankAccountsWithTransferInformation(pageIndex, pageCount);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() > 0);

        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void FindPagedBankAccountsWithTransferInformation_Invoke_InvalidPageCountThrowArgumentException_Test()
        {
            //Arrange
            IMainModuleUnitOfWork context = this.GetUnitOfWork();
            ITraceManager traceManager = this.GetTraceManager();
            BankAccountRepository repository = new BankAccountRepository(context,traceManager);
            int pageIndex = 0;
            int pageCount = 0;

            //act
            IEnumerable<BankAccount> result = repository.FindPagedBankAccountsWithTransferInformation(pageIndex, pageCount);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() > 0);

        }

        [TestMethod()]
        public void FindBankAccount_Invoke_Test()
        {
            //Arrange
            IMainModuleUnitOfWork context = this.GetUnitOfWork();
            ITraceManager traceManager = this.GetTraceManager();
            BankAccountRepository repository = new BankAccountRepository(context,traceManager);
            
            BankAccountNumberSpecification spec = new BankAccountNumberSpecification(bankAccountNumber);

            //Act
            BankAccount actual;

            actual = repository.GetBySpec(spec)
                               .SingleOrDefault();

            //Assert
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.BankAccountId == 1);
            
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FindBanksAccounts_Invoke_NullSpecThrowNewArgumentNullException_Test()
        {
            //Arrange
            IMainModuleUnitOfWork context = this.GetUnitOfWork();
            ITraceManager traceManager = this.GetTraceManager();
            BankAccountRepository repository = new BankAccountRepository(context,traceManager);
   
            repository.GetBySpec(null);
        }
        [TestMethod()]
        public void FindBanksAccounts_Invoke_AccountNumber_Test()
        {
            //Arrange
            IMainModuleUnitOfWork context = this.GetUnitOfWork();
            ITraceManager traceManager = this.GetTraceManager();
            BankAccountRepository repository = new BankAccountRepository(context,traceManager);
            BankAccountSearchSpecification spec = new BankAccountSearchSpecification(bankAccountNumber, null);

            //Act
            IEnumerable<BankAccount> actual;

            actual = repository.GetBySpec(spec);

            //Assert
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Count() == 1);

        }
        [TestMethod()]
        public void FindBanksAccounts_Invoke_CustomerName_Test()
        {
            //Arrange
            IMainModuleUnitOfWork context = this.GetUnitOfWork();
            ITraceManager traceManager = this.GetTraceManager();
            BankAccountRepository repository = new BankAccountRepository(context,traceManager);

            string name = "Unai";
            BankAccountSearchSpecification spec = new BankAccountSearchSpecification(null, name);

            //Act
            IEnumerable<BankAccount> actual;

            actual = repository.GetBySpec(spec);

            //Assert
            Assert.IsNotNull(actual);

        }
        [TestMethod()]
        public void FindBanksAccounts_Invoke_CustomerIdAndAccountNumber_Test()
        {
            //Arrange
            IMainModuleUnitOfWork context = this.GetUnitOfWork();
            ITraceManager traceManager = this.GetTraceManager();
            BankAccountRepository repository = new BankAccountRepository(context,traceManager);

            string name = "Cesar";
            BankAccountSearchSpecification spec = new BankAccountSearchSpecification(bankAccountNumber, name);

            //Act
            IEnumerable<BankAccount> actual;

            actual = repository.GetBySpec(spec);

            //Assert
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Any());

        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FindBankAccount_Invoke_NullSpecThrowArgumentNullException_Test()
        {
            //Arrange
            IMainModuleUnitOfWork context = this.GetUnitOfWork();
            ITraceManager traceManager = this.GetTraceManager();
            BankAccountRepository repository = new BankAccountRepository(context,traceManager);

            //Act
            BankAccount actual;

            actual = repository.GetBySpec(null)
                               .SingleOrDefault();
        }
        [TestMethod()]
        public void FindBankAccount_Invoke_InvalidCodeReturnNullObject_Test()
        {
            //Arrange
            IMainModuleUnitOfWork context = this.GetUnitOfWork();
            ITraceManager traceManager = this.GetTraceManager();
            BankAccountRepository repository = new BankAccountRepository(context,traceManager);

            string invalidCode = "0200011111";
            BankAccountNumberSpecification spec = new BankAccountNumberSpecification(invalidCode);

            //Act
            BankAccount actual;

            actual = repository.GetBySpec(spec)
                               .SingleOrDefault();

            //Assert
            Assert.IsNull(actual);
        }
    }
}
