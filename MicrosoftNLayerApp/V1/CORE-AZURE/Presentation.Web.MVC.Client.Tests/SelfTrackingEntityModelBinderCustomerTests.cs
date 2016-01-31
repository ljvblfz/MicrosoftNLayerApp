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

using Microsoft.Samples.NLayerApp.Domain.MainModule.Entities;
using Microsoft.Samples.NLayerApp.Presentation.Web.MVC.Client.Extensions.Utilities;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Asp.NetMVC.Client.Tests
{
    [TestClass]
    public class SelfTrackingEntityModelBinderCustomerTests : SelfTrackingEntityModelBinderTests<Customer>
    {

        #region Override Methods

        public override NameValueCollection CreateCollectionParameters()
        {
            return new NameValueCollection()
            {
                {"Customer.CompanyName","Micro"},
                {"Customer.CustomerId","5"}
            };
        }

        public override IEnumerable<Tuple<object, Func<Customer, object>>> Expectations()
        {
            yield return Tuple.Create<object,Func<Customer,object>>("Micro",c => c.CompanyName);
            yield return Tuple.Create<object, Func<Customer, object>>(5, c => c.CustomerId);
        }

        public override string GetSerializedEntity()
        {
            Customer c = new Customer();
            string result = new SelfTrackingEntityBase64Converter<Customer>().ToBase64(c);
            return result;
        }

        #endregion
    }
}
