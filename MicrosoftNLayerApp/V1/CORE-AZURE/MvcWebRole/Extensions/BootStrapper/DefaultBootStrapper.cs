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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Microsoft.Samples.NLayerApp.Domain.MainModule.Entities;
using Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.IoC;
using Microsoft.Samples.NLayerApp.Presentation.Web.MVC.Client.Extensions.ControllerFactories;
using Microsoft.Samples.NLayerApp.Presentation.Web.MVC.Client.Extensions.CustomModelBinders;

namespace Microsoft.Samples.NLayerApp.Presentation.Web.MVC.Client.Extensions.BootStrapper
{
    /// <summary>
    /// Default boot strapper implementation
    /// <see cref="NLayerApp.Presentation.Web.MVC.Client.Extensions.BootStrapper.IBootStrapper"/>
    /// </summary>
    public class DefaultBootStrapper
        : IBootStrapper
    {
        #region Members

        /// <summary>
        /// Current IoC container
        /// </summary>
        IContainer _CurrentContainer;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance of this boot strapper
        /// </summary>
        /// <param name="container">Default IoC container</param>
        public DefaultBootStrapper(IContainer container)
        {
            if (container == (IContainer)null)
                throw new ArgumentNullException("serviceFactory");

            _CurrentContainer = container;
        }

        #endregion

        #region IBootStrapper Members

        /// <summary>
        /// <see cref="M:NLayerApp.Presentation.Web.MVC.Client.Extensions.BootStrapper.IBootStrapper.Boot"/>
        /// </summary>
        public void Boot()
        {
            //Register controllers into MVC infrastructure
            RegisterControllers();

            //Register new model binders
            RegisterModelBinders();

            //register factories
            RegisterFactories();
        }

        #endregion

        #region Private Methods

        private void RegisterFactories()
        {
            //Create a custom controller factory to resolve controllers with IoC container
            IControllerFactory factory = new IoCControllerFactory(_CurrentContainer);

            //Set new controller factory in ASP.MVC Controller builder
            ControllerBuilder.Current.SetControllerFactory(factory);
        }

        private void RegisterModelBinders()
        {
            //Register a new model binder for customers. This model binder enables the deserialization
            //of a given customer in edit scenarios.
            ModelBinders.Binders.Add(typeof(Customer), new SelfTrackingEntityModelBinder<Customer>());


            //Register a new model binder for customer's picture. This model binder binds the posted
            //image to a the byte array field in the CustomerPicture class.
            ModelBinders.Binders.Add(typeof(CustomerPicture), new CustomerPictureModelBinder());
        }

        private void RegisterControllers()
        { 
            //In this implementations only controllers in same assembly are registered in IoC container. If you
            // have controllers in a different assembly check this code.

            //Recover excuting assembly.
            Assembly assembly =Assembly.GetExecutingAssembly();

            //Recover all controller types in this assembly.
            IEnumerable<Type> controllers = assembly.GetExportedTypes()
                                                    .Where(x => typeof(IController).IsAssignableFrom(x));

            //Register all controllers types
            foreach (Type item in controllers)
                _CurrentContainer.RegisterType(item);
        }

        #endregion 
    }
}
