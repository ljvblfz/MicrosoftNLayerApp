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
using System.Linq;
using System.Globalization;

using Microsoft.Samples.NLayerApp.Domain.Core.Specification;
using Microsoft.Samples.NLayerApp.Domain.MainModule.Customers;
using Microsoft.Samples.NLayerApp.Domain.MainModule.Entities;
using Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.Logging;
using Microsoft.Samples.NLayerApp.Infrastructure.Data.Core;
using Microsoft.Samples.NLayerApp.Infrastructure.Data.Core.Extensions;
using Microsoft.Samples.NLayerApp.Infrastructure.Data.MainModule.Resources;
using Microsoft.Samples.NLayerApp.Infrastructure.Data.MainModule.UnitOfWork;



namespace Microsoft.Samples.NLayerApp.Infrastructure.Data.MainModule.Repositories
{
    /// <summary>
    /// ICustomer repository implementation
    /// <see cref="Microsoft.Samples.NLayerApp.Domain.MainModule.Customers.ICustomerRepository"/>
    /// </summary>
    public class CustomerRepository
        :Repository<Customer>,ICustomerRepository
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="traceManager">Trace manager dependency</param>
        /// <param name="unitOfWork">Specific unitOfWork for this repository</param>
        public CustomerRepository(IMainModuleUnitOfWork unitOfWork, ITraceManager traceManager) : base(unitOfWork, traceManager) { }

        #endregion

        #region ICustomerRepository implementation

        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.Domain.MainModule.Customers.ICustomerRepository"/>
        /// </summary>
        /// <param name="specification"><see cref="Microsoft.Samples.NLayerApp.Domain.MainModule.Customers.ICustomerRepository"/></param>
        /// <returns>Customer that match <paramref name="specification"/></returns>

        public Customer FindCustomer(ISpecification<Customer> specification)
        {
            //validate specification
            if (specification == (ISpecification<Customer>)null)
                throw new ArgumentNullException("specification");

            IMainModuleUnitOfWork activeContext = this.UnitOfWork as IMainModuleUnitOfWork;
            if (activeContext != null)
            {
                //perform operation in this repository
                return activeContext.Customers
                                    .Include(c => c.CustomerPicture)
                                    .Where(specification.SatisfiedBy())
                                    .SingleOrDefault();
            }
            else
                throw new InvalidOperationException(string.Format(
                                                                CultureInfo.InvariantCulture,
                                                                Messages.exception_InvalidStoreContext,
                                                                this.GetType().Name));
        }

        #endregion

    }
}
