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
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

using Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.IoC;
using Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.Logging;



namespace Microsoft.Samples.NLayerApp.DistributedServices.Core.ErrorHandlers
{
    /// <summary>
    /// Default error handler for WCF Service Facade
    /// </summary>
    public sealed class DefaultErrorHandler
        :IErrorHandler
    {
        /// <summary>
        /// Enables error-related processing and returns a value that indicates whether
        /// the dispatcher aborts the session and the instance context in certain cases
        /// </summary>
        /// <remarks>
        /// Trace error and handle this
        /// </remarks>
        /// <param name="error">The exception thrown during processing</param>
        /// <returns>
        /// true if should not abort the session (if there is one) and instance context
        /// if the instance context is not System.ServiceModel.InstanceContextMode.Single;
        /// otherwise, false. The default is false.
        /// </returns>
        public bool HandleError(Exception error)
        {
            if (error != null)
            {
                //For Health system trace un-cached exception
                //ITraceManager traceManager = new TraceManager();
                ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();
                traceManager.TraceError(error.Message);
            }
            //set this error is handled 
            return true;
        }

        /// <summary>
        /// Enables the creation of a custom System.ServiceModel.FaultException{TDetail}
        /// that is returned from an exception in the course of a service method.
        /// </summary>
        /// <param name="error">The System.Exception object thrown in the course of the service operation.</param>
        /// <param name="version">The SOAP version of the message.</param>
        /// <param name="fault">The System.ServiceModel.Channels.Message object that is returned to the client, or service in duplex case</param>
        public void ProvideFault(Exception error, System.ServiceModel.Channels.MessageVersion version, ref System.ServiceModel.Channels.Message fault)
        {
            if (error is FaultException<ServiceError>)
            {
                MessageFault messageFault = ((FaultException<ServiceError>)error).CreateMessageFault();

                //propagate FaultException
                fault = Message.CreateMessage(version, messageFault, ((FaultException<ServiceError>)error).Action);
            }
            else
            {
                //create service error
                ServiceError defaultError = new ServiceError()
                {
                    ErrorMessage = Resources.Messages.message_DefaultErrorMessage
                };

                //Create fault exception and message fault
                FaultException<ServiceError> defaultFaultException = new FaultException<ServiceError>(defaultError);
                MessageFault defaultMessageFault = defaultFaultException.CreateMessageFault();

                //propagate FaultException
                fault = Message.CreateMessage(version, defaultMessageFault, defaultFaultException.Action);
            }
        }
    }
}
