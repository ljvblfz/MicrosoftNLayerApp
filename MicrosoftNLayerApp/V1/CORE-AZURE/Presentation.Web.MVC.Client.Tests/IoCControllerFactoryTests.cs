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
using System.Web.Mvc.Moles;

using Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.IoC.Moles;
using Microsoft.Samples.NLayerApp.Presentation.Web.MVC.Client.Controllers;
using Microsoft.Samples.NLayerApp.Presentation.Web.MVC.Client.Extensions.ControllerFactories;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Asp.NetMVC.Client.Tests
{
    [TestClass]
    public class IoCControllerFactoryTests
    {
        [TestMethod]
        [HostType("Moles")]
        public void Get_Controller_Instance_Returns_The_Right_Controller()
        {
            //Arrange
            SIContainer container = new SIContainer();
            
            HomeController controller = new HomeController();
            
            container.ResolveType = x =>controller;
            
            IoCControllerFactory controllerFactory = new IoCControllerFactory(container);
            
            MDefaultControllerFactory baseMole = new MDefaultControllerFactory(controllerFactory);
            //Create a MSTest accessor to access GetControllerInstance

            IoCControllerFactory_Accessor factoryAccessor = new IoCControllerFactory_Accessor(new PrivateObject(controllerFactory));
            
            baseMole.CreateControllerRequestContextString = (context, name) => factoryAccessor.GetControllerInstance(context,typeof(HomeController));
            
            //Act
            HomeController resolvedController = controllerFactory.CreateController(null, "Home") as HomeController;

            //Assert
            Assert.AreSame(controller, resolvedController);
        }

        [TestMethod]
        [HostType("Moles")]
        [ExpectedException(typeof(NullReferenceException))]
        public void Get_Controller_Instance_Throws_Null_Reference_Exception_When_The_Controller_Type_Is_Null()
        {
            //Arrange
            SIContainer serviceFactory = new SIContainer();
            HomeController controller = new HomeController();
            serviceFactory.ResolveType = x => controller;
            
            
            IoCControllerFactory controllerFactory = new IoCControllerFactory(serviceFactory);
            
            MDefaultControllerFactory baseMole = new MDefaultControllerFactory(controllerFactory);
            
            IoCControllerFactory_Accessor factoryAccessor = new IoCControllerFactory_Accessor(new PrivateObject(controllerFactory));
            baseMole.CreateControllerRequestContextString = (context, name) => factoryAccessor.GetControllerInstance(context, null);
            
            //Act
            HomeController resolvedController = controllerFactory.CreateController(null, "Home") as HomeController;
        }
    }
}
