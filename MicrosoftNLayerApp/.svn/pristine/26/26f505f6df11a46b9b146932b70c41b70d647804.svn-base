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
using Microsoft.Samples.NLayerApp.Domain.MainModule.BankAccounts;
using Microsoft.Samples.NLayerApp.Domain.MainModule.Entities;
using Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.IoC;
using Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.Logging;
using Microsoft.Samples.NLayerApp.Application.MainModule.BankingManagement;

namespace Microsoft.Samples.NLayerApp.DistributedServices.MainModule
{
    public partial class MainModuleService
    {
        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.DistributedServices.MainModule.IMainModuleService"/>
        /// </summary>
        ///<param name="pagedCriteria"><see cref="Microsoft.Samples.NLayerApp.DistributedServices.MainModule.IMainModuleService"/></param>
        /// <returns><see cref="Microsoft.Samples.NLayerApp.DistributedServices.MainModule.IMainModuleService"/></returns>
        public List<BankAccount> GetPagedBankAccounts(PagedCriteria pagedCriteria)
        {
            try
            {
                //Resolve root dependency and perform operation
                
                IBankingManagementService bankingManagement = IoCFactory.Instance.CurrentContainer.Resolve<IBankingManagementService>();

                List<BankAccount> bankAccounts = null;

                //perform work!
                bankAccounts = bankingManagement.FindPagedBankAccounts(pagedCriteria.PageIndex, pagedCriteria.PageCount);

                return bankAccounts;
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
        /// <param name="bankAccountInformation"><see cref="Microsoft.Samples.NLayerApp.DistributedServices.MainModule.IMainModuleService"/></param>
        /// <returns><see cref="Microsoft.Samples.NLayerApp.DistributedServices.MainModule.IMainModuleService"/></returns>
        public List<BankAccount> GetBankAccounts(BankAccountInformation bankAccountInformation)
        {

            //Resolve root dependency and perform operation
            IBankingManagementService bankingManagement = IoCFactory.Instance.CurrentContainer.Resolve<IBankingManagementService>();

            List<BankAccount> bankAccounts = null;

            //perform work!
            bankAccounts = bankingManagement.FindBankAccounts(bankAccountInformation.BankAccountNumber, bankAccountInformation.CustomerName);

            return bankAccounts;

        }
        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.DistributedServices.MainModule.IMainModuleService"/>
        /// </summary>
        /// <param name="bankAccountNumber"><see cref="Microsoft.Samples.NLayerApp.DistributedServices.MainModule.IMainModuleService"/></param>
        /// <returns><see cref="Microsoft.Samples.NLayerApp.DistributedServices.MainModule.IMainModuleService"/></returns>
        public BankAccount GetBankAccountByNumber(string bankAccountNumber)
        {
            try
            {
                //Resolve root dependency and perform operation
                IBankingManagementService bankingManagement = IoCFactory.Instance.CurrentContainer.Resolve<IBankingManagementService>();
                BankAccount bankAccount = null;

                //perform work!

                bankAccount = bankingManagement.FindBankAccountByNumber(bankAccountNumber);

                return bankAccount;
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
        /// <param name="bankAccount"><see cref="Microsoft.Samples.NLayerApp.DistributedServices.MainModule.IMainModuleService"/></param>
        public void ChangeBankAccount(Domain.MainModule.Entities.BankAccount bankAccount)
        {
            try
            {
                //Resolve root dependency and perform operation
                IBankingManagementService bankingManagement = IoCFactory.Instance.CurrentContainer.Resolve<IBankingManagementService>();
                bankingManagement.ChangeBankAccount(bankAccount);
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
        /// <param name="bankAccount"><see cref="Microsoft.Samples.NLayerApp.DistributedServices.MainModule.IMainModuleService"/></param>
        public void AddBankAccount(Domain.MainModule.Entities.BankAccount bankAccount)
        {
            try
            {
                //Resolve root dependency and perform operation
                IBankingManagementService bankingManagement = IoCFactory.Instance.CurrentContainer.Resolve<IBankingManagementService>();
                bankingManagement.AddBankAccount(bankAccount);
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
        public List<BankTransfer> GetPagedTransfers(PagedCriteria pagedCriteria)
        {
            try
            {
                IBankingManagementService bankingManagement = IoCFactory.Instance.CurrentContainer.Resolve<IBankingManagementService>();

                return bankingManagement.FindBankTransfers(pagedCriteria.PageIndex, pagedCriteria.PageCount);
            }
            catch (ArgumentException ex)
            {
                //trace data for internal health system and return specific FaultException here!
                //Log and throw is a knowed anti-pattern but in this point ( entry point for clients this is admited!)

                //log exception for manage health system
                ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();
                traceManager.TraceError(ex.Message);

                //propagate bussines exception to client
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
        /// <param name="transferInformation"><see cref="Microsoft.Samples.NLayerApp.DistributedServices.MainModule.IMainModuleService"/></param>
        public void PerformBankTransfer(TransferInformation transferInformation)
        {
            try
            {
                IBankingManagementService bankingManagement = IoCFactory.Instance.CurrentContainer.Resolve<IBankingManagementService>();
                bankingManagement.PerformTransfer(transferInformation.OriginAccountNumber, transferInformation.DestinationAccountNumber, transferInformation.Amount);
            }
            catch (InvalidOperationException ex)
            {
                //trace data for internal health system and return specific FaultException here!
                //Log and throw is a knowed anti-pattern but in this point ( entry point for clients this is admited!)

                //log exception for manage health system
                ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();
                traceManager.TraceError(ex.Message);

                //propagate bussines exception to client
                ServiceError detailedError = new ServiceError()
                {
                    ErrorMessage = Resources.Messages.exception_InvalidBankAccountForTransfer
                };

                throw new FaultException<ServiceError>(detailedError);
            }
            catch (ArgumentException ex)
            {
                //trace data for internal health system and return specific FaultException here!
                //Log and throw is a knowed anti-pattern but in this point ( entry point for clients this is admited!)

                //log exception for manage health system
                ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();
                traceManager.TraceError(ex.Message);

                //propagate bussines exception to client
                ServiceError detailedError = new ServiceError()
                {
                    ErrorMessage = Resources.Messages.exception_InvalidArguments
                };

                throw new FaultException<ServiceError>(detailedError);
            }
        }
    }
}
