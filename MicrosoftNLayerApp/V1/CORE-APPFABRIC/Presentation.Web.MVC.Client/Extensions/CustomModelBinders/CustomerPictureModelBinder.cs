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

using System.Web;
using System.Web.Mvc;

namespace Microsoft.Samples.NLayerApp.Presentation.Web.MVC.Client.Extensions.CustomModelBinders
{

    /// <summary>
    /// Custom model binder class to bind uploaded customers images to its CustomerPicture field transparently
    /// </summary>
    public class CustomerPictureModelBinder : DefaultModelBinder
    {
        #region DefaultModelBinder overrides


        /// <summary>
        /// This method changes de default behavior of a modelbinder when it has to bind a posted photo to the
        /// Photo property of customer picture. Otherwise it behaves default.
        /// </summary>
        /// <param name="controllerContext">The context within which the controller operates. The context information includes the controller, HTTP content, request context, and route data.</param>
        /// <param name="bindingContext">The context within which the model is bound. The context includes information such as the model object, model name, model type, property filter, and value provider.</param>
        /// <param name="propertyDescriptor">The descriptor for the property to access. The descriptor provides information such as the component type, property type, and property value. It also provides methods to get or set the property value.</param>
        /// <param name="propertyBinder">An object that provides a way to bind the property.</param>
        /// <returns>
        /// An object that represents the property value.
        /// </returns>
        protected override object GetPropertyValue(ControllerContext controllerContext, ModelBindingContext bindingContext, System.ComponentModel.PropertyDescriptor propertyDescriptor, IModelBinder propertyBinder)
        {
            // Check to see if we are binding the Photo Property, otherwise fallback to default behavior
            if (propertyDescriptor.Name == "Photo")
            {
                IValueProvider provider = bindingContext.ValueProvider;
                // Check if there is a value on the request for the Photo property
                var result = provider.GetValue(bindingContext.ModelName);
                // The result should be a posted file
                HttpPostedFileBase file = result.ConvertTo(typeof(HttpPostedFileBase)) as HttpPostedFileBase;
                // Check if there's actually a Photo in the request.
                if (file == (HttpPostedFileBase)null)
                {
                    //There is no photo.
                    return null;
                }
                // Create a byte array with the length of the posted file.
                byte [] resultValue = new byte[file.ContentLength];
                // Read the contents of the posted file to the byte array.
                file.InputStream.Read(resultValue, 0, file.ContentLength);
                // Return the byte array.
                return resultValue;
            }
            //If we are binding another property fallback to default behavior.
            return base.GetPropertyValue(controllerContext, bindingContext, propertyDescriptor, propertyBinder);
        }

        #endregion
    }
}