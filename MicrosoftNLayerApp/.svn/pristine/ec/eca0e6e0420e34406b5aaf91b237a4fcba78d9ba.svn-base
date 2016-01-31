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
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using Microsoft.Samples.NLayerApp.Presentation.Web.MVC.Client.Extensions.Utilities;
using Microsoft.Samples.NLayerApp.Presentation.Web.MVC.Client.ViewModels;

namespace Microsoft.Samples.NLayerApp.Presentation.Web.MVC.Client.Extensions.HtmlHelpers
{
    /// <summary>
    /// Class containing custom HtmlHelpers for ASP.NET MVC.
    /// </summary>
    public static class NLayerAppHtmlHelpers
    {
        #region HtmlHelper Extension mehtods

        /// <summary>
        /// Displays the name for the selected propery by the expresion.
        /// </summary>
        /// <typeparam name="T">Type of the model.</typeparam>
        /// <typeparam name="K">Type of the selected property.</typeparam>
        /// <param name="helper">Instance of the Extended HtmlHelper class.</param>
        /// <param name="member">Lambda Expression containing the targeted member.</param>
        /// <returns>A piece of HTML to be rendered without HTML encoding it.</returns>
        public static MvcHtmlString DisplayNameFor<T, K>(this HtmlHelper<T> helper, Expression<Func<T, K>> member)
        {
            MemberExpression expression = member.Body as MemberExpression;
            if (expression == null)
            {
                throw new ArgumentException("member must be a MemberExpression", "member");
            }
            //
            return MvcHtmlString.Create(DisplayNameFor(helper.ViewData.ModelMetadata, expression.Member.Name).GetDisplayName());
        }

        /// <summary>
        /// This method gets the name used to display a property from the metadata of the model.
        /// </summary>
        /// <param name="metaData">The metadata for the property.</param>
        /// <param name="memberName">Name of the member.</param>
        /// <returns></returns>
        private static ModelMetadata DisplayNameFor(ModelMetadata metaData, string memberName)
        {

            if (metaData.PropertyName == memberName)
            {
                return metaData;
            }
            else
            {
                foreach (var property in metaData.Properties)
                {
                    ModelMetadata result = DisplayNameFor(property, memberName);
                    if (result != null)
                    {
                        return result;
                    }
                }
                //It's impossible to get here because we correcteness is enforced at compile time.
                // (A MemberExpression with an invalid name wouldn't compile).
                return null;
            }
        }

        /// <summary>
        /// Serializes the target entity as a base 64 string and creates a form hidden field for it.
        /// </summary>
        /// <typeparam name="T">Type of the entity to be serialized.</typeparam>
        /// <param name="helper">Instance of the Extended HtmlHelper class.</param>
        /// <param name="entity">The entity to be serialized as a hidden field.</param>
        /// <returns>A HTML hidden field to be rendered without HTML encoding it.</returns>
        public static MvcHtmlString SerializedHidden<T>(this HtmlHelper<T> helper, T entity)
        {
            if (entity != null)
            {
                MvcHtmlString hidden = helper.Hidden(string.Concat(typeof(T).Name, "STE"), new SelfTrackingEntityBase64Converter<T>().ToBase64(entity));
                return hidden;
            }
            return MvcHtmlString.Empty;
        }

        #endregion

    }
}