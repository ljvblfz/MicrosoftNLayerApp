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
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Samples.NLayerApp.Domain.MainModule.Entities;

namespace Microsoft.Samples.NLayerApp.Domain.MainModule.Tests.Entities
{
    [TestClass()]
    public class OrderTest
    {
        [TestMethod()]
        public void GetNumberOfItems_Test()
        {
            //Arrange
            Order order = new Order()
            {
                OrderId = 1,
                OrderDate = DateTime.Now,
                CustomerId = 1,
                OrderDetails = new Core.Entities.TrackableCollection<OrderDetail>()
                               {
                                   new OrderDetail(){OrderDetailsId = 1,Discount = 0,ProductId = 1, Amount = 3},
                                   new OrderDetail(){OrderDetailsId = 1,Discount = 0,ProductId = 2, Amount = 12},
                               }
            };

            //Act
            int numberOfItems = order.GetNumberOfItems();

            //Asert
            Assert.AreEqual(15,numberOfItems);
        }
    }

}
