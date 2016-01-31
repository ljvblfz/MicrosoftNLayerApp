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
using System.IO;
using System.Web.Moles;
using System.Web.Mvc;
using System.Web.Mvc.Moles;

using Microsoft.Samples.NLayerApp.Domain.MainModule.Entities;
using Microsoft.Samples.NLayerApp.Presentation.Web.MVC.Client.Extensions.CustomModelBinders;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Asp.NetMVC.Client.Tests
{
    [TestClass]
    public class CustomerPictureModelBinderTests
    {
        [TestMethod]
        public void CustomerPictureModelBinder_Returns_CustomerPicture_With_Photo()
        {
            //Arrange
            int customerId = 15;
            string photoAsString = Convert.ToBase64String(new byte[] { 0x10, 0x10, 0x01 });

            //Create a mock of the posted file.
            SHttpPostedFileBase customerPhoto = new SHttpPostedFileBase();
            customerPhoto.InputStreamGet = () => new MemoryStream(Convert.FromBase64String(photoAsString));
            customerPhoto.ContentLengthGet = () => photoAsString.Length;


            //Create the binder
            IValueProvider provider = new SIValueProvider()
            {
                //Mock the contains prefix string to return true only for the Photo and the CustomerId
                ContainsPrefixString = x => x.StartsWith("CustomerPicture.Photo") || x.StartsWith("CustomerPicture.CustomerId") || x.Equals("CustomerPicture"),
                //Mock the values returned for Photo and CustomerId.
                GetValueString = x =>
                {
                    switch (x)
                    {
                        case "CustomerPicture.Photo":
                            return new ValueProviderResult(customerPhoto, photoAsString, null);
                        case "CustomerPicture.CustomerId":
                            return new ValueProviderResult(15, "15", null);
                        default:
                            return null;
                    }
                }
            };

            
            ModelMetadata metadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(CustomerPicture));
            
            CustomerPictureModelBinder binder = new CustomerPictureModelBinder();
            
            ControllerContext context = new ControllerContext();
            
            ModelBindingContext bindingContext = new ModelBindingContext()
            {
                ModelName = "CustomerPicture",
                ValueProvider = provider,
                ModelMetadata = metadata
            };

            //Act
            CustomerPicture result = binder.BindModel(context, bindingContext) as CustomerPicture;


            //Assert
            Assert.AreEqual(customerId, result.CustomerId);
            Assert.IsNull(result.Customer);
        }

        [TestMethod]
        public void CustomerPictureModelBinder_Returns_CustomerPicture_Without_Photo_When_There_Isnt_Photo()
        {
            //Arrange
            int customerId = 15;
            IValueProvider provider = new SIValueProvider()
            {
                
                ContainsPrefixString = x => x.StartsWith("CustomerPicture.CustomerId") || x.Equals("CustomerPicture"),
                GetValueString = x =>
                {
                    switch (x)
                    {
                        case "CustomerPicture.CustomerId":
                            return new ValueProviderResult(15, "15", null);
                        default:
                            return null;
                    }
                }
            };
            
            ModelMetadata metadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(CustomerPicture));
            
            CustomerPictureModelBinder binder = new CustomerPictureModelBinder();
            
            ControllerContext context = new ControllerContext();
            
            ModelBindingContext bindingContext = new ModelBindingContext()
            {
                ModelName = "CustomerPicture",
                ValueProvider = provider,
                ModelMetadata = metadata
            };

            //Act
            CustomerPicture result = binder.BindModel(context, bindingContext) as CustomerPicture;

            //Assert
            Assert.AreEqual(customerId, result.CustomerId);
            Assert.IsNull(result.Customer);
            Assert.IsNull(result.Photo);
        }

        [TestMethod]
        public void CustomerPictureModelBinder_Returns_CustomerPicture_Without_Photo_When_Photo_Isnt_HttpPostedFile()
        {
            //Arrange
            int customerId = 15;

            SValueProviderResult photoResult = new SValueProviderResult(null, "AAA", null);
            photoResult.ConvertToTypeCultureInfo = (type, culture) => null;
            IValueProvider provider = new SIValueProvider()
            {
                
                ContainsPrefixString = x => x.StartsWith("CustomerPicture.Photo") || x.StartsWith("CustomerPicture.CustomerId") || x.Equals("CustomerPicture"),
                GetValueString = x =>
                {
                    switch (x)
                    {
                        case "CustomerPicture.Photo":
                            return photoResult;
                        case "CustomerPicture.CustomerId":
                            return new ValueProviderResult(15, "15", null);
                        default:
                            return null;
                    }
                }
            };
            
            ModelMetadata metadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(CustomerPicture));
            CustomerPictureModelBinder binder = new CustomerPictureModelBinder();
            ControllerContext context = new ControllerContext();

            ModelBindingContext bindingContext = new ModelBindingContext()
            {
                ModelName = "CustomerPicture",
                ValueProvider = provider,
                ModelMetadata = metadata
            };

            //Act
            CustomerPicture result = binder.BindModel(context, bindingContext) as CustomerPicture;


            //Assert
            Assert.AreEqual(customerId, result.CustomerId);
            Assert.IsNull(result.Customer);
            Assert.IsNull(result.Photo);
        }
    }
}
