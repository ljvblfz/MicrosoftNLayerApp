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
using System.Web.Mvc;

using Microsoft.Samples.NLayerApp.Presentation.Web.MVC.Client.Controllers;
using Microsoft.Samples.NLayerApp.Presentation.Web.MVC.Client.ViewModels;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Asp.NetMVC.Client.Tests
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void Index_Returns_Welcome_Message()
        {
            //Arrange.
            HomeController controller = new HomeController();

            //Act
            ViewResult result = controller.Index() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Menu_Returns_The_Correct_Current_Menu_Option()
        {
            //Arrange
            HomeController controller = new HomeController();
            
            ControllerContext context = new ControllerContext();
            ViewContext parentView = new ViewContext();

            //Set the parent's view routeData
            parentView.RouteData.Values["controller"] = "Customer";
            parentView.RouteData.Values["action"] = "Index";

            context.RouteData.DataTokens["ParentActionViewContext"] = parentView;

            //Set the context for the controller.
            controller.ControllerContext = context;

            //Act
            ViewResult result = controller.Menu() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            MenuStateViewModel model = result.ViewData.Model as MenuStateViewModel;
            Assert.IsNotNull(model);
            Assert.IsTrue(model.IsCurrentAction("CustomerList"));
        }
    }
}
