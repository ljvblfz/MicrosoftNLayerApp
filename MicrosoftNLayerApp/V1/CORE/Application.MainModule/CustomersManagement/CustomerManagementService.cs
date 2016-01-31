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

using Microsoft.Samples.NLayerApp.Domain.Core;
using Microsoft.Samples.NLayerApp.Domain.MainModule.Entities;
using Microsoft.Samples.NLayerApp.Domain.MainModule.Countries;
using Microsoft.Samples.NLayerApp.Domain.MainModule.Customers;
using Microsoft.Samples.NLayerApp.Domain.Core.Specification;


namespace Microsoft.Samples.NLayerApp.Application.MainModule.CustomersManagement
{
    /// <summary>
    /// ICustomerManagementService implementation
    /// <see cref="Microsoft.Samples.NLayerApp.Application.MainModule.CustomersManagement.ICustomerManagementService"/>
    /// </summary>
    public class CustomerManagementService
        : ICustomerManagementService
    {

        #region Members

        ICustomerRepository _customerRepository;
        ICountryRepository _countryRepository;

        #endregion

        #region Constructor

        /// <summary>
        /// Create new instance 
        /// </summary>
        /// <param name="customerRepository">Customer repository dependency, intented to be resolved with dependency injection</param>
        /// <param name="countryRepository">Country repository dependency, intended to be resolved with dependency injection</param>
        public CustomerManagementService(ICustomerRepository customerRepository, ICountryRepository countryRepository)
        {
            if (customerRepository == (ICustomerRepository)null)
                throw new ArgumentNullException("customerRepository");

            if (countryRepository == (ICountryRepository)null)
                throw new ArgumentNullException("countryRepository");

            _customerRepository = customerRepository;
            _countryRepository = countryRepository;
        }

        #endregion

        #region ICustomerManagementService Members

        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.Application.MainModule.CustomersManagement.ICustomerManagementService"/>
        /// </summary>
        /// <param name="customer"><see cref="Microsoft.Samples.NLayerApp.Application.MainModule.CustomersManagement.ICustomerManagementService"/></param>
        public void AddCustomer(Customer customer)
        {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            IUnitOfWork unitOfWork = _customerRepository.UnitOfWork as IUnitOfWork;


            _customerRepository.Add(customer);


            //Complete changes in this unit of work
            unitOfWork.Commit();
        }

        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.Application.MainModule.CustomersManagement.ICustomerManagementService"/>
        /// </summary>
        /// <param name="customer"><see cref="Microsoft.Samples.NLayerApp.Application.MainModule.CustomersManagement.ICustomerManagementService"/></param>
        public void ChangeCustomer(Customer customer)
        {
            if (customer == (Customer)null)
                throw new ArgumentNullException("customer");

            IUnitOfWork unitOfWork = _customerRepository.UnitOfWork as IUnitOfWork;

            _customerRepository.Modify(customer);

            unitOfWork.CommitAndRefreshChanges();

        }
        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.Application.MainModule.CustomersManagement.ICustomerManagementService"/>
        /// </summary>
        /// <param name="customer"><see cref="Microsoft.Samples.NLayerApp.Application.MainModule.CustomersManagement.ICustomerManagementService"/></param>
        public void RemoveCustomer(Customer customer)
        {
            if (customer == (Customer)null)
                throw new ArgumentNullException("customer");

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            IUnitOfWork unitOfWork = _customerRepository.UnitOfWork as IUnitOfWork;


            //Set IsEnabled property to false, remove customer is only a logical operation 
            // cannot remove this item in  persistence store

            customer.IsEnabled = false;

            _customerRepository.Modify(customer);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
        }
        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.Application.MainModule.CustomersManagement.ICustomerManagementService"/>
        /// </summary>
        /// <param name="customerCode"><see cref="Microsoft.Samples.NLayerApp.Application.MainModule.CustomersManagement.ICustomerManagementService"/></param>
        /// <returns><see cref="Microsoft.Samples.NLayerApp.Application.MainModule.CustomersManagement.ICustomerManagementService"/></returns>
        public Customer FindCustomerByCode(string customerCode)
        {
            //Create specification
            CustomerCodeSpecification spec = new CustomerCodeSpecification(customerCode);

            return _customerRepository.FindCustomer(spec);
        }
        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.Application.MainModule.CustomersManagement.ICustomerManagementService"/>
        /// </summary>
        /// <param name="pageIndex"><see cref="Microsoft.Samples.NLayerApp.Application.MainModule.CustomersManagement.ICustomerManagementService"/></param>
        /// <param name="pageCount"><see cref="Microsoft.Samples.NLayerApp.Application.MainModule.CustomersManagement.ICustomerManagementService"/></param>
        /// <returns><see cref="Microsoft.Samples.NLayerApp.Application.MainModule.CustomersManagement.ICustomerManagementService"/></returns>
        public List<Customer> FindPagedCustomers(int pageIndex, int pageCount)
        {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            //Create "enabled variable" transform adhoc execution plan in prepared plan
            //for more info: http://geeks.ms/blogs/unai/2010/07/91/ef-4-0-performance-tips-1.aspx
            bool enabled = true;
            Specification<Customer> onlyEnabledSpec = new DirectSpecification<Customer>(c => c.IsEnabled == enabled);

            return _customerRepository.GetPagedElements(pageIndex, pageCount, c => c.CustomerCode, onlyEnabledSpec, true)
                                      .ToList();

            
        }
        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.Application.MainModule.CustomersManagement.ICustomerManagementService"/>
        /// </summary>
        /// <param name="countryName"><see cref="Microsoft.Samples.NLayerApp.Application.MainModule.CustomersManagement.ICustomerManagementService"/></param>
        /// <returns><see cref="Microsoft.Samples.NLayerApp.Application.MainModule.CustomersManagement.ICustomerManagementService"/></returns>
        public List<Country> FindCountriesByName(string countryName)
        {
            CountryNameSpecification spec = new CountryNameSpecification(countryName);

            return _countryRepository.GetBySpec(spec as ISpecification<Country>)
                                     .ToList();
        }
        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.Application.MainModule.CustomersManagement.ICustomerManagementService"/>
        /// </summary>
        /// <param name="pageIndex"><see cref="Microsoft.Samples.NLayerApp.Application.MainModule.CustomersManagement.ICustomerManagementService"/></param>
        /// <param name="pageCount"><see cref="Microsoft.Samples.NLayerApp.Application.MainModule.CustomersManagement.ICustomerManagementService"/></param>
        /// <returns><see cref="Microsoft.Samples.NLayerApp.Application.MainModule.CustomersManagement.ICustomerManagementService"/></returns>
        public List<Country> FindPagedCountries(int pageIndex, int pageCount)
        {
            return _countryRepository.GetPagedElements(pageIndex, pageCount, c => c.CountryName, true)
                                     .ToList();
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Release all resources
        /// </summary>
        public void Dispose()
        {
            //release used unit of work
            //if you have many repositories but  lifetime is per resolve only need
            //dispose one

            if (_customerRepository != (ICustomerRepository)null)
            {

                this._customerRepository
                    .UnitOfWork
                    .Dispose();
            }
        }

        #endregion

    }
}
