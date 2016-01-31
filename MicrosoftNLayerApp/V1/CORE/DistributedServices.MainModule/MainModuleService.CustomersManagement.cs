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
using System.ServiceModel;

using Microsoft.Samples.NLayerApp.Domain.MainModule.Entities;
using Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.IoC;
using Microsoft.Samples.NLayerApp.DistributedServices.Core.ErrorHandlers;
using Microsoft.Samples.NLayerApp.DistributedServices.MainModule.DTO;
using Microsoft.Samples.NLayerApp.Domain.MainModule.Customers;
using Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.Logging;
using Microsoft.Samples.NLayerApp.Application.MainModule.CustomersManagement;


namespace Microsoft.Samples.NLayerApp.DistributedServices.MainModule
{
    public partial class MainModuleService
    {
        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.DistributedServices.MainModule.IMainModuleService"/>
        /// </summary>
        /// <param name="customerCode"><see cref="Microsoft.Samples.NLayerApp.DistributedServices.MainModule.IMainModuleService"/></param>
        /// <returns><see cref="Microsoft.Samples.NLayerApp.DistributedServices.MainModule.IMainModuleService"/></returns>
        public Customer GetCustomerByCode(string customerCode)
        {
            try
            {
                //Resolve root dependency and perform operation
                using (ICustomerManagementService customerService = IoCFactory.Instance.CurrentContainer.Resolve<ICustomerManagementService>())
                {
                    return customerService.FindCustomerByCode(customerCode);
                }
            }
            catch (ArgumentNullException ex)
            {
                //trace data for internal health system and return specific FaultException here!
                //Log and throw is a knowed anti-pattern but in this point ( entry point for clients this is admited!)

                //log exception for manage health system
                ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();
                traceManager.TraceError(ex.Message);

                //propagate exception to client
                ServiceError detailedError = new ServiceError()
                {
                    ErrorMessage = Resources.Messages.exception_InvalidArguments
                };

                throw new FaultException<ServiceError>(detailedError);
            }
        }

        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.DistributedServices.MainModule.IMainModuleService"/>
        /// </summary>
        /// <param name="customer"><see cref="Microsoft.Samples.NLayerApp.DistributedServices.MainModule.IMainModuleService"/></param>
        public void ChangeCustomer(Customer customer)
        {
            try
            {
                //Resolve root dependency and perform operation
                using (ICustomerManagementService customerService = IoCFactory.Instance.CurrentContainer.Resolve<ICustomerManagementService>())
                {
                    customerService.ChangeCustomer(customer);
                }
            }
            catch (ArgumentNullException ex)
            {
                //trace data for internal health system and return specific FaultException here!
                //Log and throw is a knowed anti-pattern but in this point ( entry point for clients this is admited!)

                //log exception for manage health system
                ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();
                traceManager.TraceError(ex.Message);

                //propagate exception to client
                ServiceError detailedError = new ServiceError()
                {
                    ErrorMessage = Resources.Messages.exception_InvalidArguments
                };

                throw new FaultException<ServiceError>(detailedError);
            }

        }

        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.DistributedServices.MainModule.IMainModuleService"/>
        /// </summary>
        /// <param name="customer"><see cref="Microsoft.Samples.NLayerApp.DistributedServices.MainModule.IMainModuleService"/></param>
        public void RemoveCustomer(Customer customer)
        {
            try
            {
                using (ICustomerManagementService customerService = IoCFactory.Instance.CurrentContainer.Resolve<ICustomerManagementService>())
                {
                    customerService.RemoveCustomer(customer);
                }
            }
            catch (ArgumentNullException ex)
            {
                //trace data for internal health system and return specific FaultException here!
                //Log and throw is a knowed anti-pattern but in this point ( entry point for clients this is admited!)

                //log exception for manage health system
                ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();
                traceManager.TraceError(ex.Message);

                //propagate exception to client
                ServiceError detailedError = new ServiceError()
                {
                    ErrorMessage = Resources.Messages.exception_InvalidArguments
                };

                throw new FaultException<ServiceError>(detailedError);
            }
        }

        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.DistributedServices.MainModule.IMainModuleService"/>
        /// </summary>
        /// <param name="customer"><see cref="Microsoft.Samples.NLayerApp.DistributedServices.MainModule.IMainModuleService"/></param>
        public void AddCustomer(Customer customer)
        {
            try
            {
                //Resolve root dependency and perform operation
                using (ICustomerManagementService customerService = IoCFactory.Instance.CurrentContainer.Resolve<ICustomerManagementService>())
                {
                    customerService.AddCustomer(customer);
                }
            }
            catch (ArgumentNullException ex)
            {
                //trace data for internal health system and return specific FaultException here!
                //Log and throw is a knowed anti-pattern but in this point ( entry point for clients this is admited!)

                //log exception for manage health system
                ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();
                traceManager.TraceError(ex.Message);

                //propagate exception to client
                ServiceError detailedError = new ServiceError()
                {
                    ErrorMessage = Resources.Messages.exception_InvalidArguments
                };

                throw new FaultException<ServiceError>(detailedError);
            }
        }

        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.DistributedServices.MainModule.IMainModuleService"/>
        /// </summary>
        /// <param name="pagedCriteria"><see cref="Microsoft.Samples.NLayerApp.DistributedServices.MainModule.IMainModuleService"/></param>
        /// <returns><see cref="Microsoft.Samples.NLayerApp.DistributedServices.MainModule.IMainModuleService"/></returns>
        public List<Customer> GetPagedCustomer(PagedCriteria pagedCriteria)
        {
            try
            {
                //resolve root dependencies and perform query
                using (ICustomerManagementService customerService = IoCFactory.Instance.CurrentContainer.Resolve<ICustomerManagementService>())
                {
                    return customerService.FindPagedCustomers(pagedCriteria.PageIndex, pagedCriteria.PageCount);
                }
            }
            catch (ArgumentException ex)
            {
                //trace data for internal health system and return specific FaultException here!
                //Log and throw is a knowed anti-pattern but in this point ( entry point for clients this is admited!)

                //log exception for manage health system
                ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();
                traceManager.TraceError(ex.Message);

                //propagate exception to client
                ServiceError detailedError = new ServiceError()
                {
                    ErrorMessage = Resources.Messages.exception_InvalidArguments
                };

                throw new FaultException<ServiceError>(detailedError);
            }
            catch (NullReferenceException ex)
            {
                //trace data for internal health system and return specific FaultException here!
                //Log and throw is a knowed anti-pattern but in this point ( entry point for clients this is admited!)

                //log exception for manage health system
                ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();
                traceManager.TraceError(ex.Message);

                //propagate exception to client
                ServiceError detailedError = new ServiceError()
                {
                    ErrorMessage = Resources.Messages.exception_InvalidArguments
                };

                throw new FaultException<ServiceError>(detailedError);
            }
        }
        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.DistributedServices.MainModule.IMainModuleService"/>
        /// </summary>
        /// <param name="pagedCriteria"><see cref="Microsoft.Samples.NLayerApp.DistributedServices.MainModule.IMainModuleService"/></param>
        /// <returns><see cref="Microsoft.Samples.NLayerApp.DistributedServices.MainModule.IMainModuleService"/></returns>
        public List<Country> GetPagedCountries(PagedCriteria pagedCriteria)
        {
            try
            {
                //Resolve root dependencies and perform operations
                using (ICustomerManagementService customerService = IoCFactory.Instance.CurrentContainer.Resolve<ICustomerManagementService>())
                {
                    return customerService.FindPagedCountries(pagedCriteria.PageIndex, pagedCriteria.PageCount);
                }
            }
            catch (ArgumentException ex)
            {
                //trace data for internal health system and return specific FaultException here!
                //Log and throw is a knowed anti-pattern but in this point ( entry point for clients this is admited!)

                //log exception for manage health system
                ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();
                traceManager.TraceError(ex.Message);

                //propagate exception to client
                ServiceError detailedError = new ServiceError()
                {
                    ErrorMessage = Resources.Messages.exception_InvalidArguments
                };

                throw new FaultException<ServiceError>(detailedError);
            }

        }
        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.DistributedServices.MainModule.IMainModuleService"/>
        /// </summary>
        /// <param name="countryName"><see cref="Microsoft.Samples.NLayerApp.DistributedServices.MainModule.IMainModuleService"/></param>
        /// <returns><see cref="Microsoft.Samples.NLayerApp.DistributedServices.MainModule.IMainModuleService"/></returns>
        public List<Country> GetCountriesByName(string countryName)
        {
            try
            {
                //Resolve root dependencies and perform operations
                using (ICustomerManagementService countryService = IoCFactory.Instance.CurrentContainer.Resolve<ICustomerManagementService>())
                {

                    return countryService.FindCountriesByName(countryName);
                }
            }
            catch (ArgumentException ex)
            {
                //trace data for internal health system and return specific FaultException here!
                //Log and throw is a knowed anti-pattern but in this point ( entry point for clients this is admited!)

                //log exception for manage health system
                ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();
                traceManager.TraceError(ex.Message);

                //propagate exception to client
                ServiceError detailedError = new ServiceError()
                {
                    ErrorMessage = Resources.Messages.exception_InvalidArguments
                };

                throw new FaultException<ServiceError>(detailedError);
            }
        }
    }
}

