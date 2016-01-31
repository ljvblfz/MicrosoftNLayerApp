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
using System.ServiceModel;

using Microsoft.Samples.NLayerApp.DistributedServices.Core.ErrorHandlers;
using Microsoft.Samples.NLayerApp.DistributedServices.MainModule.DTO;
using Microsoft.Samples.NLayerApp.Domain.MainModule.Entities;

namespace Microsoft.Samples.NLayerApp.DistributedServices.MainModule
{
    /// <summary>
    /// Contract for MainModule distributed service
    /// </summary>
    [ServiceContract(Name="IMainModuleService",
                    Namespace="Microsoft.Samples.NLayerApp.DistributedServices.MainModuleService"
                    ,SessionMode=SessionMode.NotAllowed)]
    public interface IMainModuleService
    {
        #region Banking Management

        /// <summary>
        /// Get bank accounts in paged mode
        /// </summary>
        ///<param name="pagedCriteria">Criteria for pagging information</param>
        /// <returns>A collection of BankAccount</returns>
        [OperationContract(Name="GetPagedBankAccounts")]
        [FaultContract(typeof(ServiceError))]
        List<BankAccount> GetPagedBankAccounts(PagedCriteria pagedCriteria);


        /// <summary>
        /// Get bank accounts by number of customer owner identifier
        /// </summary>
        /// <param name="bankAccountInformation">BankAccount information</param>
        /// <returns>A collection of BankAccount</returns>
        [OperationContract(Name = "GetBankAccounts")]
        [FaultContract(typeof(ServiceError))]
        List<BankAccount> GetBankAccounts(BankAccountInformation bankAccountInformation);

        /// <summary>
        /// Get specific BankAccount by account number specification
        /// </summary>
        /// <param name="bankAccountNumber">Account number specification</param>
        /// <returns>Requested BankAccount or null if BankAccount not exists</returns>
        [OperationContract(Name = "GetBankAccountByNumber")]
        [FaultContract(typeof(ServiceError))]
        BankAccount GetBankAccountByNumber(string bankAccountNumber);

        /// <summary>
        /// Change existing BankAccount
        /// </summary>
        /// <param name="bankAccount">Existing BankAccount with changes</param>
        [OperationContract(Name = "ChangeBankAccount")]
        [FaultContract(typeof(ServiceError))]
        void ChangeBankAccount(BankAccount bankAccount);

        /// <summary>
        /// Add new BankAccount 
        /// </summary>
        /// <param name="bankAccount">New BankAccount to add</param>
        [OperationContract(Name = "AddBankAccount")]
        [FaultContract(typeof(ServiceError))]
        void AddBankAccount(BankAccount bankAccount);

        /// <summary>
        /// Get bank transfer in paged mode
        /// </summary>
        /// <param name="pagedCriteria">Paged criteria</param>
        /// <returns>A collection of bank transfer in paged specified with <paramref name="pagedCriteria"/></returns>
        [OperationContract(Name = "GetPagedTransfers")]
        [FaultContract(typeof(ServiceError))]
        List<BankTransfer> GetPagedTransfers(PagedCriteria pagedCriteria);

        /// <summary>
        /// Perform new bank transfer operation
        /// </summary>
        /// <param name="transferInformation"></param>
        [OperationContract(Name = "PerformBankTransfer")]
        [FaultContract(typeof(ServiceError))]
        void PerformBankTransfer(TransferInformation transferInformation);

        #endregion

        #region Customer Management

        /// <summary>
        /// Request Customer by code specification
        /// </summary>
        /// <param name="customerCode">Customer code specification</param>
        /// <returns>Customer requested or null if not exist customer with code equals to <paramref name="customerCode"/></returns>
        [OperationContract(Name = "GetCustomerByCode")]
        [FaultContract(typeof(ServiceError))]
        Customer GetCustomerByCode(string customerCode);

        /// <summary>
        /// Change existing customer
        /// </summary>
        /// <param name="customer">Customer with changes</param>
        [OperationContract(Name = "ChangeCustomer")]
        [FaultContract(typeof(ServiceError))]
        void ChangeCustomer(Customer customer);

        /// <summary>
        /// Add new customer
        /// </summary>
        /// <param name="customer">A customre to add</param>
        [OperationContract(Name = "AddCustomer")]
        [FaultContract(typeof(ServiceError))]
        void AddCustomer(Customer customer);

        /// <summary>
        /// Remove customer
        /// </summary>
        /// <param name="customer">A customer code specification</param>
        [OperationContract(Name="RemoveCustomer")]
        [FaultContract(typeof(ServiceError))]
        void RemoveCustomer(Customer customer);

        /// <summary>
        /// Get Customer in paged mode
        /// </summary>
        ///<param name="pagedCriteria">Criteria for pagging information</param>
        /// <returns>A collection of Customer in requested page</returns>
        [OperationContract(Name = "GetPagedCustomer")]
        [FaultContract(typeof(ServiceError))]
        List<Customer> GetPagedCustomer(PagedCriteria pagedCriteria);

        /// <summary>
        /// Get paged countries
        /// </summary>
        /// <param name="pagedCriteria">A Paged criteria</param>
        /// <returns>A collection of countries in specific page</returns>
        [OperationContract(Name = "GetPagedCountries")]
        [FaultContract(typeof(ServiceError))]
        List<Country> GetPagedCountries(PagedCriteria pagedCriteria);

        /// <summary>
        /// Get countries by name
        /// </summary>
        /// <param name="countryName">A country name</param>
        /// <returns>A collection of countries</returns>
        [OperationContract(Name = "GetCountriesByName")]
        [FaultContract(typeof(ServiceError))]
        List<Country> GetCountriesByName(string countryName);

        #endregion

        #region Sales Management

        /// <summary>
        /// Get paged products
        /// </summary>
        /// <returns>A collection of requested products</returns>
        [OperationContract(Name = "GetPagedProducts")]
        [FaultContract(typeof(ServiceError))]
        List<Product> GetPagedProducts(PagedCriteria pagedCriteria);

        /// <summary>
        /// Get a collection of products by publisher criteria
        /// </summary>
        /// <param name="publisherInformation">A publisher information</param>
        /// <returns>A collection of requested products</returns>
        [OperationContract(Name = "GetProductByPublisherInformation")]
        [FaultContract(typeof(ServiceError))]
        List<Product> GetProductByPublisherInformation(PublisherInformation publisherInformation);

        /// <summary>
        /// Change existing product 
        /// </summary>
        /// <param name="product">A product with changes</param>
        [OperationContract(Name = "ChangeProduct")]
        [FaultContract(typeof(ServiceError))]
        void ChangeProduct(Product product);

        /// <summary>
        /// Add new product
        /// </summary>
        /// <param name="product">A product to add</param>
        [OperationContract(Name = "AddProduct")]
        [FaultContract(typeof(ServiceError))]
        void AddProduct(Product product);

        /// <summary>
        /// Get orders by this order information
        /// </summary>
        /// <param name="orderInformation">Order information criteria</param>
        /// <returns> collection of orders requested</returns>
        [OperationContract(Name = "GetOrdersByOrderInformation")]
        [FaultContract(typeof(ServiceError))]
        List<Order> GetOrdersByOrderInformation(OrderInformation orderInformation);

        /// <summary>
        /// Get Order by dates specification
        /// </summary>
        /// <param name="fromDate">From date for specification</param>
        /// <param name="toDate">To date specification</param>
        /// <returns>A collection of orders requested</returns>
        [OperationContract(Name = "GetOrdersByDates")]
        [FaultContract(typeof(ServiceError))]
        List<Order> GetOrdersByDates(DateTime? fromDate, DateTime? toDate);

        /// <summary>
        /// Get orders by paged specification
        /// </summary>
        ///<param name="pagedCriteria">Criteria for pagging information</param>
        /// <returns>A collection of orders requested</returns>
        [OperationContract(Name = "GetPagedOrders")]
        [FaultContract(typeof(ServiceError))]
        List<Order> GetPagedOrders(PagedCriteria pagedCriteria);

        /// <summary>
        /// Get orders by shipping specification
        /// </summary>
        /// <param name="shippingData">A shipping data </param>
        /// <returns>A collection of orders requested</returns>
        [OperationContract(Name = "GetOrdersByShippingData")]
        [FaultContract(typeof(ServiceError))]
        List<Order> GetOrdersByShippingData(ShippingInformation shippingData);

        /// <summary>
        /// Change existing orders
        /// </summary>
        /// <param name="order">Existing order with changes</param>
        [OperationContract(Name = "ChangeOrder")]
        [FaultContract(typeof(ServiceError))]
        void ChangeOrder(Order order);

        /// <summary>
        /// Add new order 
        /// </summary>
        /// <param name="order">Order to add</param>
        [OperationContract(Name = "AddOrder")]
        [FaultContract(typeof(ServiceError))]
        void AddOrder(Order order);

        #endregion
    }
}

