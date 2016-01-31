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
using System.Web.Mvc;
using System.Web.Routing;

using Microsoft.Samples.NLayerApp.Domain.MainModule.Entities;
using Microsoft.Samples.NLayerApp.Presentation.Web.MVC.Client.ViewModels;
using Microsoft.Samples.NLayerApp.Application.MainModule.CustomersManagement;

namespace Microsoft.Samples.NLayerApp.Presentation.Web.MVC.Client.Controllers
{
    /// <summary>
    /// This controller handles all the actions related to a customer.
    /// </summary>
    [HandleError]
    public class CustomerController : Controller
    {

        #region Members

        /// <summary>
        /// Instance of the customer service.
        /// </summary>
        private ICustomerManagementService _CustomerService;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerController"/> class.
        /// </summary>
        /// <param name="customerService">The customer service.</param>
        public CustomerController(ICustomerManagementService customerService)
        {
            _CustomerService = customerService;
        }

        #endregion

        #region Actions

        /// <summary>
        /// Display a paged listing of the existing customers.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">The page size.</param>
        /// <returns></returns>
        public ActionResult Index(int page, int pageSize)
        {

            IList<Customer> customers = _CustomerService.FindPagedCustomers(page, pageSize);
            return View(new CustomerListViewModel(customers, page, pageSize));
        }

        /// <summary>
        /// Returns the picture of the customer.
        /// </summary>
        /// <param name="customerCode">The customer code.</param>
        /// <returns>The picture of the customer.</returns>
        public ActionResult CustomerPicture(string customerCode)
        {
            //Get the customer aggregate
            CustomerPicture photo = _CustomerService.FindCustomerByCode(customerCode).CustomerPicture;
            //Check if the customer has a photo and if it is a valid photo.
            if (photo != null && photo.Photo != (byte [])null)
            {
                //Return the associated customer photo.
                return File(photo.Photo, "img");
            }
            else
            {
                // Return a default picture if the customer hasn't got any photo.
                return File("~/Content/Images/Unknown.png", "img/png");
            }

        }

        /// <summary>
        /// Shows a specific customer in detail.
        /// </summary>
        /// <param name="customerCode">The customer code.</param>
        /// <returns>A view of the customer details.</returns>
        public ActionResult Details(string customerCode)
        {
            //Get the customer aggregate.
            Customer customer = _CustomerService.FindCustomerByCode(customerCode);
            //Display its details.
            return View(customer);
        }


        /// <summary>
        /// Displays a form to create a new Customer.
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Creates the specified customer based on the request data and displays it.
        /// </summary>
        /// <param name="customer">The customer to add.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            //Check if there aren't validation errors on the customer.
            if (ModelState.IsValid)
            {
                //Add the customer to the data base.
                _CustomerService.AddCustomer(customer);
                //Show the fist page of the customers list.
                return RedirectToAction("Index", new { page = 0, pageSize = 10 });
            }
            else
            {
                //Return the Create view and display the validation errors.
                return View();
            }
        }


        /// <summary>
        /// Displays a form to edit the customer.
        /// </summary>
        /// <param name="customerCode">The customer code.</param>
        /// <returns></returns>
        public ActionResult Edit(string customerCode)
        {
            //Get the customer aggregate to edit.
            Customer customer = _CustomerService.FindCustomerByCode(customerCode);
            //Return the
            return View(customer);
        }


        /// <summary>
        /// Saves the changes on the editions performed to the specified customer.
        /// </summary>
        /// <param name="customer">The customer.</param>
        /// <returns>The details of the customer.</returns>
        [HttpPost]
        public ActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _CustomerService.ChangeCustomer(customer);
                return RedirectToAction("Details", new RouteValueDictionary() { { "customerCode", customer.CustomerCode } });
            }
            else
            {
                return View();
            }
        }


        /// <summary>
        /// Deletes the specified customer code.
        /// </summary>
        /// <param name="customerCode">The customer code.</param>
        /// <returns>The details of the customer.</returns>
        [HttpDelete]
        public ActionResult Delete(string customerCode)
        {
            Customer customer = _CustomerService.FindCustomerByCode(customerCode);
            _CustomerService.RemoveCustomer(customer);
            return RedirectToAction("Index", new RouteValueDictionary() { { "page", 0 }, { "pageSize", 10 } });
        }

        #endregion

    }
}
