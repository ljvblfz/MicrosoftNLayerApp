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
using Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.IoC;
using Microsoft.Practices.Unity;

namespace Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.Tests
{
    [TestClass()]
    public class IoCTests
    {
        [TestMethod()]
        public void ShortLivedManager_IsTransient_Test()
        {
            //Arrange
            ShortLivedObject actual = IoCFactory.Instance.CurrentContainer.Resolve<ShortLivedObject>();

            //Act
            actual.Dispose();
            ShortLivedObject newShortLivedObject = IoCFactory.Instance.CurrentContainer.Resolve<ShortLivedObject>();
            //Assert
            Assert.AreNotEqual(actual, newShortLivedObject);
            Assert.IsFalse(newShortLivedObject.IsDisposed);
        }
    }
}
