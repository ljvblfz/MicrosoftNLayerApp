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
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

using Microsoft.Samples.NLayerApp.Application.MainModule.CustomersManagement.Moles;
using Microsoft.Samples.NLayerApp.Domain.MainModule.Entities;
using Microsoft.Samples.NLayerApp.Presentation.Web.MVC.Client.Controllers;
using Microsoft.Samples.NLayerApp.Presentation.Web.MVC.Client.ViewModels;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Asp.NetMVC.Client.Tests
{
    [TestClass]
    public class CustomerControllerTests
    {
        #region Helper Methods

        IList<Customer> GetFakeCustomers()
        {
            int seedId = 0;
            
            return  new List<Customer>()
            {
                new Customer(){ CustomerId = ++seedId},
                new Customer(){ CustomerId = ++seedId},
                new Customer(){ CustomerId = ++seedId},
                new Customer(){ CustomerId = ++seedId},
                new Customer(){ CustomerId = ++seedId},
                new Customer(){ CustomerId = ++seedId},
                new Customer(){ CustomerId = ++seedId},
                new Customer(){ CustomerId = ++seedId},
                new Customer(){ CustomerId = ++seedId},
                new Customer(){ CustomerId = ++seedId}
            };
        }

        #endregion

        #region Test Methods

        [TestMethod]
        public void Index_Returns_A_Paged_List_Of_Customers()
        {
            /*
             *   Arrange 
             * 1º - Create a dummy list of customers
             * 2º - Initialize stub of SICustomerManagementService
             * 3º - Create controller to test
             */

            IList<Customer> customers = GetFakeCustomers();

            SICustomerManagementService customerService = new SICustomerManagementService();
            customerService.FindPagedCustomersInt32Int32 = (page, pageSize) => customers.Skip(page * pageSize).Take(pageSize).ToList();

            
            CustomerController controller = new CustomerController(customerService);

            //Act
            ViewResultBase result = controller.Index(0, 5) as ViewResult;

            //Assert
            Assert.IsNotNull(result, "Expected a view");
            CustomerListViewModel model = result.ViewData.Model as CustomerListViewModel;

            Assert.IsNotNull(model, "model is null or different than CustomerListViewModel");
            IList<Customer> resultCustomers = (model.PageItems as IEnumerable<Customer>).ToList();

            Assert.AreEqual(5, resultCustomers.Count(), "Expected a different number of customers");
            
        }

        [TestMethod]
        public void CustomerPicture_Returns_A_Default_Image_When_The_Customer_Doesnt_Have_Image()
        {
            /*
             *   Arrange 
             * 1º - Create a dummy list of customers
             * 2º - Initialize stub of SICustomerManagementService
             * 3º - Create controller to test
             */

            //Create a customer
            Customer customer = new Customer(){ CustomerCode = "A0001", CustomerPicture = null};
            
            SICustomerManagementService customerService = new SICustomerManagementService();
            customerService.FindCustomerByCodeString = x => customer;

            CustomerController controller = new CustomerController(customerService);

            //Act
            FilePathResult result = controller.CustomerPicture("A0001") as FilePathResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("img/png", result.ContentType);
            Assert.IsTrue(result.FileName.EndsWith("Unknown.png"));
        }

        [TestMethod]
        public void CustomerPicture_Returns_Customer_Image_When_The_Customer_Has_Image()
        {
            /*
             *   Arrange 
             * 1º - Create a dummy list of customers
             * 2º - Initialize stub of SICustomerManagementService
             * 3º - Create controller to test
             */

            byte[] customerPhoto = new byte[] { 0x10 };
            Customer customer = new Customer()
            {
                CustomerCode = "A0001",
                CustomerPicture = new CustomerPicture()
                {
                    Photo = customerPhoto
                }
            };
           
            SICustomerManagementService customerService = new SICustomerManagementService();
            customerService.FindCustomerByCodeString = x => customer;

            //Create the controller
            CustomerController controller = new CustomerController(customerService);

            //Act
            FileContentResult result = controller.CustomerPicture("A0001") as FileContentResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("img", result.ContentType);
            Assert.AreEqual(customerPhoto.Length, result.FileContents.Length);
            for (int i = 0; i < customerPhoto.Length; i++)
            {
                Assert.AreEqual(customerPhoto[i], result.FileContents[i]);
            }

        }

        [TestMethod]
        public void Details_Returns_The_Requested_Customer_Details()
        {
            /*
             *   Arrange 
             * 1º - Create a dummy list of customers
             * 2º - Initialize stub of SICustomerManagementService
             * 3º - Create controller to test
             */

            Customer customer = new Customer() { CustomerCode = "A0001", CustomerPicture = null };
            
            SICustomerManagementService customerService = new SICustomerManagementService();
            customerService.FindCustomerByCodeString = x => customer;

            
            CustomerController controller = new CustomerController(customerService);

            //Act
            ViewResult result = controller.Details("A0001") as ViewResult;
            
            //Assert
            Assert.IsNotNull(result);

            Assert.AreEqual("", result.ViewName);

            Customer model = result.ViewData.Model as Customer;
            Assert.IsNotNull(model);

            Assert.AreEqual("A0001", model.CustomerCode);
        }

        [TestMethod]
        public void Create_Get_Returns_Create_View()
        {
            /*
             *   Arrange 
             * 1º - Initialize stub of SICustomerManagementService
             * 2º - Create controller to test
             */

            SICustomerManagementService customerService = new SICustomerManagementService();
            
            CustomerController controller = new CustomerController(customerService);

            //Act
            ViewResult result = controller.Create() as ViewResult;

            //Assert
            Assert.IsNotNull(result);

            //This is a MVC convention. When you don't specify a view 
            //it takes the view with the same name as the controller action.
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void Create_Post_Adds_The_Customer_And_Redirects_To_The_Index_Page_With_A_Valid_Customer()
        {
            /*
            *   Arrange 
            * 1º - Create a dummy list of customers
            * 2º - Initialize stub of SICustomerManagementService
            * 3º - Create controller to test
            */

            Customer customer = new Customer() { CustomerCode = "A0001" };
            IList<Customer> customers = new List<Customer>();
            
            SICustomerManagementService customerService = new SICustomerManagementService();
            customerService.AddCustomerCustomer = x => customers.Add(customer);
            
            CustomerController controller = new CustomerController(customerService);

            //Act
            RedirectToRouteResult result =  controller.Create(customer) as RedirectToRouteResult;

            //Assert
            Assert.IsNotNull(result);

            Assert.AreEqual(0, result.RouteValues["page"]);

            Assert.AreEqual(10, result.RouteValues["pageSize"]);

            Assert.AreEqual(1, customers.Count);

            Assert.AreEqual("A0001", customers.First().CustomerCode);
        }

        [TestMethod]
        public void Create_Post_Returns_Create_View_With_An_Invalid_Customer()
        {
            /*
            *   Arrange 
            * 1º - Create a dummy list of customers
            * 2º - Initialize stub of SICustomerManagementService
            * 3º - Create controller to test
            */

            Customer customer = new Customer() { CustomerCode = "A0001" };
            IList<Customer> customers = new List<Customer>();
            

            SICustomerManagementService customerService = new SICustomerManagementService();
            customerService.AddCustomerCustomer = x => customers.Add(customer);

            
            CustomerController controller = new CustomerController(customerService);

            ModelState state = new ModelState();
            state.Value = new ValueProviderResult("A0001", "A0001", CultureInfo.CurrentCulture);
            state.Errors.Add("Invalid Customer Code");
            controller.ModelState.Add("CustomerCode", state);

            //Act
            ViewResult result = controller.Create(customer) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("A0001", result.ViewData.ModelState["CustomerCode"].Value.AttemptedValue);
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void Edit_Get_Returns_The_Correct_Customer_To_Edit()
        {
           /*
           *   Arrange 
           * 1º - Create a dummy list of customers
           * 2º - Initialize stub of SICustomerManagementService
           * 3º - Create controller to test
           */

            Customer customer = new Customer() { CustomerCode = "A0001" };
            IList<Customer> customers = new List<Customer>();
            
            SICustomerManagementService customerService = new SICustomerManagementService();
            customerService.FindCustomerByCodeString = x => customer;

            CustomerController controller = new CustomerController(customerService);

            //Act
            ViewResult result = controller.Edit("A0001") as ViewResult;

            //Assert
            Assert.IsNotNull(result);

            Customer model = result.ViewData.Model as Customer;
            Assert.IsNotNull(model);

            Assert.AreSame(customer, model);

            Assert.AreEqual("", result.ViewName);

        }

        [TestMethod]
        public void Edit_Post_Saves_A_Valid_Customer_And_Redirects_To_Details_Of_The_Customer()
        {
           /*
           *   Arrange 
           * 1º - Create a dummy list of customers
           * 2º - Initialize stub of SICustomerManagementService
           * 3º - Create controller to test
           */

            Customer customer = new Customer() { CustomerCode = "A0001" };
            IList<Customer> customers = new List<Customer>();
            
            bool changeCustomerWasCalled = false;
            
            SICustomerManagementService customerService = new SICustomerManagementService();
            customerService.ChangeCustomerCustomer = x => changeCustomerWasCalled = true;
            
            CustomerController controller = new CustomerController(customerService);

            //Act
            RedirectToRouteResult result = controller.Edit(customer) as RedirectToRouteResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(changeCustomerWasCalled);
            Assert.AreEqual("Details", result.RouteValues["action"]);

            //Asp.Net MVC convention to say "Same Controller"
            Assert.IsNull(result.RouteValues["controller"]);
            Assert.AreEqual("A0001", result.RouteValues["customerCode"]);
        }

        [TestMethod]
        public void Edit_Post_Returns_Edit_View_With_An_Invalid_Customer()
        {
           /*
           *   Arrange 
           * 1º - Create a dummy list of customers
           * 2º - Initialize stub of SICustomerManagementService
           * 3º - Create controller to test
           */

            Customer customer = new Customer() { CustomerCode = "A0001" };
            SICustomerManagementService customerService = new SICustomerManagementService();
            
            CustomerController controller = new CustomerController(customerService);
            
            ModelState state = new ModelState();
            state.Value = new ValueProviderResult("A0001", "A0001", CultureInfo.CurrentCulture);
            state.Errors.Add("Invalid Customer Code");
            controller.ModelState.Add("CustomerCode", state);

            //Act
            ViewResult result = controller.Edit(customer) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("A0001", result.ViewData.ModelState["CustomerCode"].Value.AttemptedValue);
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void Delete_Action_Deletes_The_Customer_And_Redirects_To_Index()
        {
           /*
           *   Arrange 
           * 1º - Create a dummy list of customers
           * 2º - Initialize stub of SICustomerManagementService
           * 3º - Create controller to test
           */
            
            Customer customer = new Customer() { CustomerCode = "A0001" };
            IList<Customer> customers = new List<Customer>() { customer };
            
            SICustomerManagementService customerService = new SICustomerManagementService();
            customerService.FindCustomerByCodeString = x => customer;
            customerService.RemoveCustomerCustomer = x => customers.Remove(customer);
            
            
            CustomerController controller = new CustomerController(customerService);

            //Act
            RedirectToRouteResult result = controller.Delete("A0001") as RedirectToRouteResult;

            //Assert
            Assert.IsNotNull(result); 

            Assert.AreEqual(0, customers.Count);

            Assert.AreEqual("Index", result.RouteValues["action"]);

            Assert.IsNull(result.RouteValues["controller"]);

            Assert.AreEqual(0, result.RouteValues["page"]);

            Assert.AreEqual(10, result.RouteValues["pageSize"]);

        }

        #endregion
    }
}
