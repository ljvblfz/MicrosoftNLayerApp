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
using System;
using Microsoft.Samples.NLayerApp.Presentation.Web.MVC.Client.ViewModels;

namespace Microsoft.Samples.NLayerApp.Presentation.Web.MVC.Client.Controllers
{
    /// <summary>
    /// Controller for the home page.
    /// </summary>
    [HandleError]
    public class HomeController : Controller
    {
        #region Actions 

        /// <summary>
        /// Index of the web page.
        /// </summary>
        /// <returns>The index view of the web</returns>
        public ActionResult Index()
        {
            ViewData["Message"] = "Welcome to NLayerApp ASP.NET MVC web version!";

            return View();
        }

        [ChildActionOnly]
        public ActionResult Menu()
        {
            string controller = ControllerContext.ParentActionViewContext.RouteData.GetRequiredString("controller");
            string action = ControllerContext.ParentActionViewContext.RouteData.GetRequiredString("action");
            return View(new MenuStateViewModel(controller,action));
        }

        #endregion
    }
}
