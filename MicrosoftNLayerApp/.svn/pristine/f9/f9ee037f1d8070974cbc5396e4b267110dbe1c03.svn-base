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
using System.Web.Mvc;
using Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.IoC;
using System.Web.Routing;

namespace Microsoft.Samples.NLayerApp.Presentation.Web.MVC.Client.Extensions.ControllerFactories
{
    /// <summary>
    /// Custom controller factory for ASP.NET MVC. In this controller
    /// factory, controller instances are instantiated using an IoC container
    /// </summary>
    public class IoCControllerFactory : DefaultControllerFactory
    {
        #region Members

        private IContainer _Container;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="container"></param>
        public IoCControllerFactory(IContainer container)
        {
            if (container == (IContainer)null)
                throw new ArgumentNullException("serviceFactory");

            _Container = container;
        }

        #endregion

        #region DefaultControllerFactory Overrides

        /// <summary>
        /// This method create a new instance of a controller using an IoC container in order to handle a users request.
        /// </summary>
        /// <param name="requestContext">The context of the current HTTP request.</param>
        /// <param name="controllerType">The type of the controller that handles the request.</param>
        /// <returns>An instance of the controller.</returns>
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType != (Type)null)
            {
                //Try to resolve the controller using asociated IoC container.
                return _Container.Resolve(controllerType) as IController;
            }
            else
            {
                //Fallback to default behavior.
                return base.GetControllerInstance(requestContext, controllerType);
            }

        }
        #endregion
    }
}