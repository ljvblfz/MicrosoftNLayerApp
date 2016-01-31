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
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Moles;
using System.Web.Routing;
using System.Xml.Linq;

using Microsoft.Samples.NLayerApp.Domain.MainModule.Entities;
using Microsoft.Samples.NLayerApp.Presentation.Web.MVC.Client.Extensions.HtmlHelpers;
using Microsoft.Samples.NLayerApp.Presentation.Web.MVC.Client.Extensions.Utilities;
using Microsoft.Samples.NLayerApp.Presentation.Web.MVC.Client.ViewModels.Moles;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Asp.NetMVC.Client.Tests
{
    [TestClass]
    public class NLayerAppHtmlHelpersTests
    {
        [TestMethod]
        public void DisplayNameFor_Returns_Display_Name_Based_On_Metadata()
        {
            //Arrange
            ModelMetadata metadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(Customer));
            
            ViewDataDictionary viewData = new ViewDataDictionary();
            viewData.ModelMetadata = metadata;
            SViewContext viewContext = new SViewContext();
            viewContext.ViewData = viewData;
            
            //Create a viewDataContainerMock
            SIViewDataContainer viewDataContainer = new SIViewDataContainer();
            
            viewDataContainer.ViewDataGet = () => viewData;
            
            HtmlHelper<Customer> helper = new HtmlHelper<Customer>(viewContext,viewDataContainer);

            //Act
            MvcHtmlString result = helper.DisplayNameFor(x => x.CustomerId);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("CustomerId", result.ToHtmlString());
        }

        [TestMethod]
        public void Serialized_Hidden_Returns_A_Hidden_Field_With_The_Entity_Serialized()
        {
            //Arrang
            Customer customer = new Customer();

            
            string serializedCustomer = new SelfTrackingEntityBase64Converter<Customer>().ToBase64(customer);
            
            ViewDataDictionary viewData = new ViewDataDictionary();
            
            SViewContext viewContext = new SViewContext();
            viewContext.ViewData = viewData;
            
            SIViewDataContainer viewDataContainer = new SIViewDataContainer();
            viewDataContainer.ViewDataGet = () => viewData;
            

            HtmlHelper<Customer> helper = new HtmlHelper<Customer>(viewContext, viewDataContainer);
            
            //Act
            MvcHtmlString result = helper.SerializedHidden(customer);
            
            //Assert
            //Parse the result (it creates XML-Compliant HTML)
            XElement element = XElement.Parse(result.ToHtmlString());

            //Ensure it's an input tag.
            Assert.AreEqual("input", element.Name.LocalName);

            //Ensure it's a hidden field.
            Assert.AreEqual("hidden",element.Attribute("type").Value);

            //Ensure it has the correct name
            Assert.AreEqual("CustomerSTE",element.Attribute("name").Value);

            //Ensure it has the correct id
            Assert.AreEqual("CustomerSTE",element.Attribute("id").Value);

            //Ensure the serialized customer is serialized correctly.
            Assert.AreEqual(serializedCustomer, element.Attribute("value").Value);
        }

        [TestMethod]
        public void Serialized_Hidden_Returns_An_Empty_String_When_Entity_Is_Null()
        {
            //Arrange
            ViewDataDictionary viewData = new ViewDataDictionary();

            
            SViewContext viewContext = new SViewContext();
            viewContext.ViewData = viewData;

            
            SIViewDataContainer viewDataContainer = new SIViewDataContainer();
            viewDataContainer.ViewDataGet = () => viewData;

            
            HtmlHelper<Customer> helper = new HtmlHelper<Customer>(viewContext, viewDataContainer);
            
            //Act
            MvcHtmlString result = helper.SerializedHidden(null);
            
            //Assert
            Assert.AreEqual(MvcHtmlString.Empty.ToHtmlString(), result.ToHtmlString());
        }
    }
}
