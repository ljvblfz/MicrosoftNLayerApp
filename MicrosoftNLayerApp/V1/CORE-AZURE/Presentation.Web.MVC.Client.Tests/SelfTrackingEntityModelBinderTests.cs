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
using System.Collections.Specialized;
using System.Web.Mvc;

using Microsoft.Samples.NLayerApp.Domain.Core.Entities;
using Microsoft.Samples.NLayerApp.Presentation.Web.MVC.Client.Extensions.CustomModelBinders;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Asp.NetMVC.Client.Tests
{
    [TestClass]
    public abstract class SelfTrackingEntityModelBinderTests<T> where T : class, IObjectWithChangeTracker, new()
    {

        public SelfTrackingEntityModelBinder<T> ModelBinder
        {
            get
            {
                return new SelfTrackingEntityModelBinder<T>();
            }
        }

        /// <summary>
        /// Returns a base64 string with the serialized entity.
        /// </summary>
        /// <returns>A base64 string with the serialized entity.</returns>
        public abstract string GetSerializedEntity();

        /// <summary>
        /// Returns the forms parameters of the request. This parameters would be what the browser
        /// sends to the server.
        /// </summary>
        /// <returns>A collection of request parameters</returns>
        public abstract NameValueCollection CreateCollectionParameters();

        /// <summary>
        /// This method returns a tuple with the expected object and a function that is used as an accessor
        /// for the actual data.
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerable<Tuple<object, Func<T, object>>> Expectations();

        [TestMethod]
        public void SelfTrackingEntityModelBinder_Returns_A_New_Entity_When_There_Isnt_Any_Serialized_Entity_On_The_Request()
        {
            //Arrange
            NameValueCollection postParameters = CreateCollectionParameters();
            NameValueCollectionValueProvider valueProvider =
                new NameValueCollectionValueProvider(postParameters, null);

            
            ModelMetadata metaData = ModelMetadataProviders.Current.GetMetadataForType(null, new T().GetType());
            ControllerContext controllerContext = new ControllerContext();
            ModelBindingContext bindingContext = new ModelBindingContext()
            {
                ModelName = typeof(T).Name,
                ValueProvider = valueProvider,
                ModelMetadata = metaData
            };

            //Act
            
            T result = ModelBinder.BindModel(controllerContext, bindingContext) as T;
            
            //Assert
            Assert.IsNotNull(result);
            foreach (var expectation in Expectations())
                Assert.AreEqual(expectation.Item1, expectation.Item2(result));
        }
        [TestMethod]
        public void SelfTrackingEntityModelBinder_Returns_The_Updated_Entity_When_There_Is_A_Serialized_Entity_On_The_Request()
        {
            //Arrange
            NameValueCollection postParameters = CreateCollectionParameters();
            postParameters.Add(typeof(T).Name + "STE", GetSerializedEntity());
            NameValueCollectionValueProvider valueProvider =
                new NameValueCollectionValueProvider(postParameters, null);


            ModelMetadata metaData = ModelMetadataProviders.Current.GetMetadataForType(null, new T().GetType());
            ControllerContext controllerContext = new ControllerContext();
            ModelBindingContext bindingContext = new ModelBindingContext()
            {
                ModelName = typeof(T).Name,
                ValueProvider = valueProvider,
                ModelMetadata = metaData
            };

            //Act
            T result = ModelBinder.BindModel(controllerContext, bindingContext) as T;

            //Assert
            Assert.IsNotNull(result);
            foreach (var expectation in Expectations())
                Assert.AreEqual(expectation.Item1, expectation.Item2(result));
        }
    }
}
