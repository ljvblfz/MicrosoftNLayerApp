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
using System.Runtime.Serialization;

namespace Microsoft.Samples.NLayerApp.Presentation.Web.MVC.Client.Extensions.Utilities
{
    /// <summary>
    /// This class serializes and deserializes an entity to a base 64 string.
    /// </summary>
    /// <typeparam name="T">Type of the entity to be serialized or deserialized.</typeparam>
    public class SelfTrackingEntityBase64Converter<T>
    {
        #region Members

        /// <summary>
        /// Deserializes the entity from a base 64 string.
        /// </summary>
        /// <param name="value">The base 64 string containing the serialized entity.</param>
        /// <returns>The resulting entity after deserialization.</returns>
        public T ToEntity(string value)
        {
            var bytes = Convert.FromBase64String(value);
            MemoryStream memStream = new MemoryStream(bytes);
            DataContractSerializer deserializer = new DataContractSerializer(typeof(T));
            T result = (T)deserializer.ReadObject(memStream);
            return result;
        }

        /// <summary>
        /// Serialized the entity to a base 64 string.
        /// </summary>
        /// <param name="entity">The entity to be serialized.</param>
        /// <returns>The base 64 string containing the serialized entity.</returns>
        public string ToBase64(T entity)
        {

            MemoryStream memStream = new MemoryStream();
            DataContractSerializer serializer = new DataContractSerializer(typeof(T));
            serializer.WriteObject(memStream, entity);
            string result = Convert.ToBase64String(memStream.ToArray());
            return result;
        }

        #endregion
    }
}