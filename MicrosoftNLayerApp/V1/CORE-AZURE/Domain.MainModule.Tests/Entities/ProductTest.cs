
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
//===================================================================================using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Samples.NLayerApp.Domain.MainModule.Entities;

namespace Microsoft.Samples.NLayerApp.Domain.MainModule.Tests.Entities
{
    [TestClass()]
    public class ProductTest
    {
        [TestMethod()]
        public void ExistInStock_Invoke_Test()
        {
            //Arrange
            Product product = new Product()
            {
                ProductId = 1,
                AmountInStock = 10
            };
            //act
            bool result = product.ExistStock();

            //Assert
            Assert.IsTrue(result);
        }
        [TestMethod()]
        public void ExistInStock_LessThanZeroAmountInStockReturnFalse_Test()
        {
            //Arrange
            Product product = new Product()
            {
                ProductId = 1,
                AmountInStock = 0
            };
            //act
            bool result = product.ExistStock();

            //Assert
            Assert.IsFalse(result);
        }
    }
}
