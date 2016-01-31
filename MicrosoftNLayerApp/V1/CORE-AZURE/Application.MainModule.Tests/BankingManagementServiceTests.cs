using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.IoC;
using Microsoft.Samples.NLayerApp.Domain.MainModule.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Samples.NLayerApp.Application.MainModule.BankingManagement;

namespace Application.MainModule.Tests
{
    [TestClass()]
    [DeploymentItem("Microsoft.Samples.NLayerApp.Infrastructure.Data.MainModule.Mock.dll")]
    [DeploymentItem("Microsoft.Samples.NLayerApp.Infrastructure.Data.MainModule.dll")]
    public class BankingManagementServiceTests
    {
        [TestMethod()]
        public void FindPagedBankAccounts_Invoke_Test()
        {
            //Arrange
            IBankingManagementService bankAccountService = IoCFactory.Instance.CurrentContainer.Resolve<IBankingManagementService>();
            int pageIndex = 0;
            int pageCount = 1;
            //Act
            List<BankAccount> bankAccounts = bankAccountService.FindPagedBankAccounts(pageIndex, pageCount);

            //Assert
            Assert.IsNotNull(bankAccounts);
            Assert.IsTrue(bankAccounts.Count == 1);
            Assert.IsTrue(bankAccounts.First().BankAccountNumber == "BAC0000001");
        }
        [TestMethod()]
        public void FindBanksAccounts_Invoke_Test()
        {
            //Arrange
            string accountNumber = "BAC0000001";

            IBankingManagementService bankAccountService = IoCFactory.Instance.CurrentContainer.Resolve<IBankingManagementService>();

            //Act
            List<BankAccount> bankAccounts = bankAccountService.FindBankAccounts(accountNumber, null);

            //Assert
            Assert.IsNotNull(bankAccounts);


        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void FindPagedBankAccounts_Invoke_NullPageIndexThrowArgumentException_Test()
        {
            //Arrange
            IBankingManagementService bankAccountService = IoCFactory.Instance.CurrentContainer.Resolve<IBankingManagementService>();

            //Act
            List<BankAccount> bankAccounts = bankAccountService.FindPagedBankAccounts(-1, 1);
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void FindPagedBankAccounts_Invoke_NullPageCountThrowArgumentException_Test()
        {
            //Arrange
            IBankingManagementService bankAccountService = IoCFactory.Instance.CurrentContainer.Resolve<IBankingManagementService>();

            //Act
            List<BankAccount> bankAccounts = bankAccountService.FindPagedBankAccounts(0, 0);
        }
        [TestMethod()]
        public void FindBankAccountByNumber_Invoke_Test()
        {
            //Arrange
            IBankingManagementService bankAccountService = IoCFactory.Instance.CurrentContainer.Resolve<IBankingManagementService>();

            //Act
            BankAccount actual = bankAccountService.FindBankAccountByNumber("BAC0000001");

            //Assert
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.BankAccountNumber == "BAC0000001");
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ChangeBankAccount_Invoke_NullItemThrowNewArgumentNullException_Test()
        {
            //Arrange
            IBankingManagementService bankAccountService = IoCFactory.Instance.CurrentContainer.Resolve<IBankingManagementService>();
            bankAccountService.ChangeBankAccount(null);
        }
        [TestMethod()]
        public void ChangeBankAccount_Invoke_Test()
        {
            //Arrange
            IBankingManagementService bankAccountService = IoCFactory.Instance.CurrentContainer.Resolve<IBankingManagementService>();

            //Act
            BankAccount actual = bankAccountService.FindBankAccountByNumber("BAC0000001");
            decimal balance = actual.Balance;
            actual.Balance += 10M;

            bankAccountService.ChangeBankAccount(actual);


            BankAccount expected = bankAccountService.FindBankAccountByNumber("BAC0000001");

            //Assert
            Assert.IsNotNull(expected != null);
            Assert.IsTrue(expected.Balance == (balance + 10M));
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddBankAccount_Invoke_NullItemThrowNewArgumentException_Test()
        {
            //Arrange
            IBankingManagementService bankAccountService = IoCFactory.Instance.CurrentContainer.Resolve<IBankingManagementService>();

            //Act
            bankAccountService.AddBankAccount(null);
        }
        [TestMethod()]
        public void AddBankAccount_Invoke_Test()
        {
            //Arrange
            IBankingManagementService bankAccountService = IoCFactory.Instance.CurrentContainer.Resolve<IBankingManagementService>();
            string bankAccountNumber = "ABC0001222";

            //Act
            BankAccount bankAccount = new BankAccount()
            {
                Balance = 1000,
                BankAccountNumber = bankAccountNumber,
                CustomerId = 1
            };

            bankAccountService.AddBankAccount(bankAccount);
            BankAccount actual = bankAccountService.FindBankAccountByNumber(bankAccountNumber);

            //Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(actual.Balance, bankAccount.Balance);
        }
        [TestMethod()]
        public void PerformTransfer_Invoke_Test()
        {
            //Arrange

            IBankingManagementService bankTransfersService = IoCFactory.Instance.CurrentContainer.Resolve<IBankingManagementService>();
            IBankingManagementService bankAccountService = IoCFactory.Instance.CurrentContainer.Resolve<IBankingManagementService>();

            string bankAccountFrom = "BAC0000001";
            string bankAcccountTo = "BAC0000002";
            decimal amount = 10M;
            decimal actualBanlance = 0M;

            //Act

            //find actual balance in to account
            actualBanlance = bankAccountService.FindBankAccountByNumber(bankAcccountTo).Balance;

            bankTransfersService.PerformTransfer(bankAccountFrom, bankAcccountTo, amount);


            //Assert

            //check balance 
            decimal balance = bankAccountService.FindBankAccounts(bankAcccountTo, null).SingleOrDefault().Balance;


            Assert.AreEqual(actualBanlance + amount, balance);


        }
        [TestMethod()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PerformTransfer_Invoke_InvalidAmountThrowNewInvalidOperationException_Test()
        {
            //Arrange
            IBankingManagementService bankTransfersService = IoCFactory.Instance.CurrentContainer.Resolve<IBankingManagementService>();
            string bankAccountFrom = "BAC0000001";
            string bankAcccountTo = "BAC0000002";
            decimal amount = 1000000000000M; //account one not have sufficient money

            //Act
            bankTransfersService.PerformTransfer(bankAccountFrom, bankAcccountTo, amount);
        }
        [TestMethod()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PerformTransfer_Invoke_LockedAccountThrowNewInvalidOperationException_Test()
        {
            //Arrange
            IBankingManagementService bankTransfersService = IoCFactory.Instance.CurrentContainer.Resolve<IBankingManagementService>();
            string bankAccountFrom = "BAC0000003";//bankAccount is locked
            string bankAcccountTo = "BAC0000002";
            decimal amount = 10M;

            //Act
            bankTransfersService.PerformTransfer(bankAccountFrom, bankAcccountTo, amount);
        }

    }
}
