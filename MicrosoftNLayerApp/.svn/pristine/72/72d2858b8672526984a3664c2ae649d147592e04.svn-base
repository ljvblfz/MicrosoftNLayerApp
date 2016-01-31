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
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Microsoft.Samples.NLayerApp.Domain.MainModule.Customers;
using Microsoft.Samples.NLayerApp.Domain.MainModule.Entities;
using Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.Logging;
using Microsoft.Samples.NLayerApp.Infrastructure.Data.MainModule.Repositories;
using Microsoft.Samples.NLayerApp.Infrastructure.Data.MainModule.UnitOfWork;


namespace Microsoft.Samples.NLayerApp.Infrastructure.Data.MainModule.Tests.RepositoriesTests
{
    [TestClass()]
    public class CustomerRepositoryTests
        :RepositoryTestsBase<Customer>
    {
        
        public override System.Linq.Expressions.Expression<Func<Customer, bool>> FilterExpression
        {
            get { return c => c.CustomerCode == "A0004"; }
        }

        public override System.Linq.Expressions.Expression<Func<Customer, int>> OrderByExpression
        {
            get { return c => c.CustomerId; }
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FindCustomer_Invoke_NullSpecThrowNewArgumentNullException_Test()
        {
            //Arrange 
            IMainModuleUnitOfWork context = GetUnitOfWork();
            ITraceManager traceManager = this.GetTraceManager();
            ICustomerRepository repository = new CustomerRepository(context,traceManager);

            //Act
            repository.FindCustomer(null);

        }
        [TestMethod()]
        public void FindCustomer_Invoke_Test()
        {
            //Arrange 
            string customerCode = "A0004";
            IMainModuleUnitOfWork context = GetUnitOfWork();
            ITraceManager traceManager = this.GetTraceManager();
            ICustomerRepository repository = new CustomerRepository(context,traceManager);

            CustomerCodeSpecification spec = new CustomerCodeSpecification(customerCode);

            //Act
            Customer customer = repository.FindCustomer(spec);

            //Assert
            Assert.IsNotNull(customer); 
            Assert.IsNotNull(customer.CustomerPicture);
        }
    }
}
