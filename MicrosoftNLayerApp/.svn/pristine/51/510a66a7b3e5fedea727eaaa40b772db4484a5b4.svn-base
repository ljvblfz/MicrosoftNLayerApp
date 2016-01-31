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
using Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.IoC;
using Microsoft.Samples.NLayerApp.Domain.MainModule.Orders;
using Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.Logging;
using Microsoft.Samples.NLayerApp.Application.MainModule.SalesManagement;

namespace Microsoft.Samples.NLayerApp.DistributedServices.MainModule
{
    public partial class MainModuleService
    {

        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.DistributedServices.MainModule.IMainModuleService"/>
        /// </summary>
        /// <param name="orderInformation"><see cref="Microsoft.Samples.NLayerApp.DistributedServices.MainModule.IMainModuleService"/></param>
        /// <returns><see cref="Microsoft.Samples.NLayerApp.DistributedServices.MainModule.IMainModuleService"/></returns>
        public List<Order> GetOrdersByOrderInformation(OrderInformation orderInformation)
        {
            try
            {
                //Resolve root dependencies and perform operations
                using (ISalesManagementService salesManagement = IoCFactory.Instance.CurrentContainer.Resolve<ISalesManagementService>())
                {
                    return salesManagement.FindOrdersByOrderInformation(orderInformation.CustomerId, orderInformation.DateTimeFrom, orderInformation.DateTimeTo);
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
        /// <param name="fromDate"><see cref="Microsoft.Samples.NLayerApp.DistributedServices.MainModule.IMainModuleService"/></param>
        /// <param name="toDate"><see cref="Microsoft.Samples.NLayerApp.DistributedServices.MainModule.IMainModuleService"/></param>
        /// <returns><see cref="Microsoft.Samples.NLayerApp.DistributedServices.MainModule.IMainModuleService"/></returns>
        public List<Order> GetOrdersByDates(DateTime? fromDate, DateTime? toDate)
        {
            //Resolve root dependencies and perform operations
            using (ISalesManagementService salesManagement = IoCFactory.Instance.CurrentContainer.Resolve<ISalesManagementService>())
            {
                return salesManagement.FindOrdersByDates(fromDate, toDate);
            }
        }
        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.DistributedServices.MainModule.IMainModuleService"/>
        /// </summary>
        /// <param name="pagedCriteria"><see cref="Microsoft.Samples.NLayerApp.DistributedServices.MainModule.IMainModuleService"/></param>
        /// <returns><see cref="Microsoft.Samples.NLayerApp.DistributedServices.MainModule.IMainModuleService"/></returns>
        public List<Order> GetPagedOrders(PagedCriteria pagedCriteria)
        {
            try
            {
                //Resolve root dependencies and perform operations
                using (ISalesManagementService salesManagement = IoCFactory.Instance.CurrentContainer.Resolve<ISalesManagementService>())
                {
                    return salesManagement.FindPagedOrders(pagedCriteria.PageIndex, pagedCriteria.PageCount);
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
        /// <param name="shippingData"><see cref="Microsoft.Samples.NLayerApp.DistributedServices.MainModule.IMainModuleService"/></param>
        /// <returns><see cref="Microsoft.Samples.NLayerApp.DistributedServices.MainModule.IMainModuleService"/></returns>
        public List<Order> GetOrdersByShippingData(ShippingInformation shippingData)
        {
            //Resolve root dependencies and perform operation
            using (ISalesManagementService salesManagement = IoCFactory.Instance.CurrentContainer.Resolve<ISalesManagementService>())
            {
                return salesManagement.FindOrdersByShippingData(shippingData.ShippingName,
                                                      shippingData.ShippingAddress,
                                                      shippingData.ShippingCity,
                                                      shippingData.ShippingZip);
            }
        }
        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.DistributedServices.MainModule.IMainModuleService"/>
        /// </summary>
        /// <param name="order"><see cref="Microsoft.Samples.NLayerApp.DistributedServices.MainModule.IMainModuleService"/></param>
        public void ChangeOrder(Order order)
        {
            try
            {
                //Resolve root dependencies and perform operations
                using (ISalesManagementService salesManagement = IoCFactory.Instance.CurrentContainer.Resolve<ISalesManagementService>())
                {
                    salesManagement.ChangeOrder(order);
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
        /// <param name="order"><see cref="Microsoft.Samples.NLayerApp.DistributedServices.MainModule.IMainModuleService"/></param>
        public void AddOrder(Order order)
        {
            try
            {
                //Resolve root dependencies and perform operations
                using (ISalesManagementService salesManagement = IoCFactory.Instance.CurrentContainer.Resolve<ISalesManagementService>())
                {
                    salesManagement.AddOrder(order);
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
        /// <param name="product"><see cref="Microsoft.Samples.NLayerApp.DistributedServices.MainModule.IMainModuleService"/></param>
        public void AddProduct(Product product)
        {
            try
            {
                //Resolve root dependencies and perform query
                using (ISalesManagementService salesManagement = IoCFactory.Instance.CurrentContainer.Resolve<ISalesManagementService>())
                {
                    salesManagement.AddProduct(product);
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
        /// <param name="product"><see cref="Microsoft.Samples.NLayerApp.DistributedServices.MainModule.IMainModuleService"/></param>
        public void ChangeProduct(Product product)
        {
            try
            {
                //Resolve root dependencies and perform query
                using (ISalesManagementService salesManagement = IoCFactory.Instance.CurrentContainer.Resolve<ISalesManagementService>())
                {
                    salesManagement.ChangeProduct(product);
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
        /// <param name="publisherInformation"><see cref="Microsoft.Samples.NLayerApp.DistributedServices.MainModule.IMainModuleService"/></param>
        /// <returns><see cref="Microsoft.Samples.NLayerApp.DistributedServices.MainModule.IMainModuleService"/></returns>
        public List<Product> GetProductByPublisherInformation(PublisherInformation publisherInformation)
        {
            //Resolve root dependencies and perform query
            using (ISalesManagementService salesManagement = IoCFactory.Instance.CurrentContainer.Resolve<ISalesManagementService>())
            {
                return salesManagement.FindProductBySpecification(publisherInformation.PublisherName,
                                                                 publisherInformation.Description);
            }
        }

        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.DistributedServices.MainModule.IMainModuleService"/>
        /// </summary>
        /// <param name="pagedCriteria"><see cref="Microsoft.Samples.NLayerApp.DistributedServices.MainModule.IMainModuleService"/></param>
        /// <returns><see cref="Microsoft.Samples.NLayerApp.DistributedServices.MainModule.IMainModuleService"/></returns>
        public List<Product> GetPagedProducts(PagedCriteria pagedCriteria)
        {
            try
            {
                //Resolve root dependencies and perform query
                using (ISalesManagementService salesManagement = IoCFactory.Instance.CurrentContainer.Resolve<ISalesManagementService>())
                {
                    return salesManagement.FindPagedProducts(pagedCriteria.PageIndex, pagedCriteria.PageCount);
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
    }
}
