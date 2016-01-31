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
using System.Linq.Expressions;

using Microsoft.Samples.NLayerApp.Infrastructure.Data.Core.Extensions;
using Microsoft.Samples.NLayerApp.Infrastructure.Data.Core.Tests.StubAndMoles;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Objects;


namespace Microsoft.Samples.NLayerApp.Infrastructure.Data.Core.Tests
{
    [TestClass()]
    public class IQueryableExtensionTest
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IncludeWithPath_Invoke_NullPathThrowArgumentNullException()
        {
            //Arrange
            List<Entity> entities = new List<Entity>()
            {
                new Entity(){Id=0,Field="field"}
            };

            MemorySet<Entity> objectSet = entities.ToMemorySet();

            //Act
            ((IQueryable<Entity>)objectSet).Include((string)null);
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void IncludeWithMemberExpression_Invoke_NullPathThrowArgumentException()
        {
            //Arrange
            List<Entity> entities = new List<Entity>()
            {
                new Entity(){Id=0,Field="field"}
            };

            MemorySet<Entity> objectSet = entities.ToMemorySet();

            //Act
            ((IQueryable<Entity>)objectSet).Include((Expression<Func<Entity, object>>)null);
        }
        [TestMethod()]
        public void IncludeWithPath_Invoke()
        {
            //Arrange
            List<Entity> entities = new List<Entity>()
            {
                new Entity(){Id=0,Field="field"}
            };

            MemorySet<Entity> objectSet = entities.ToMemorySet();
            string includePath = "Field";
            //Act
            ((IQueryable<Entity>)objectSet).Include(includePath);

            //Assert
            List<string> actual = new PrivateObject(objectSet).GetField("_IncludePaths") as List<string>;
            Assert.IsTrue(actual.Where(s => s == includePath).Count() == 1);
        }
        [TestMethod()]
        public void IncludeWithMemberExpression_Invoke_Test()
        {
            //Arrange
            List<Entity> entities = new List<Entity>()
            {
                new Entity(){Id=0,Field="field"}
            };

            MemorySet<Entity> objectSet = entities.ToMemorySet();
            Expression<Func<Entity, object>> navigationExpression = t => t.Field;

            //Act
            ((IQueryable<Entity>)objectSet).Include(navigationExpression);

            //Assert
            List<string> actual = new PrivateObject(objectSet).GetField("_IncludePaths") as List<string>;
            Assert.IsTrue(actual.Where(s => s == "Field").Count() == 1);
        }
        [TestMethod()]
        public void TryParseMultipleInclude_Invoke_Test()
        {
            //Arrange
            PrivateType type = new PrivateType(typeof(IQueryableExtensions));

            Type[] types = new Type[] { typeof(Expression), typeof(String).MakeByRefType() };
            Expression<Func<Master, object>> expression = m => m.Details.Select(d => d.LineDetails);
            string includeResult = string.Empty;
            object[] args = new object[] { expression.Body, includeResult };


            //Act 
            bool result = false;
            result = (bool)type.InvokeStatic("TryParsePath", types, args);

            //Assert
            Assert.IsTrue(result);
            Assert.AreEqual(args[1].ToString(), "Details.LineDetails");
        }
        [TestMethod()]
        public void UsingIncludeInOfTypeObjectSet_Test()
        {
            //Arrange
            List<Product> products = new List<Product>()
            {
                new Product(){ProductId = 1, Description ="Description 1"},
                new Software(){ProductId = 2, Description ="Description 2",LicenseCode = "A01"},
                new Software(){ProductId = 3, Description ="Description 3",LicenseCode = "A02"},
            };

            //Act
            MemorySet<Product> productSet = products.ToMemorySet<Product>();

            IQueryable<Software> softwareSet  = productSet.OfType<Product, Software>()
                                                          .Include("SoftwareData");
            
            //Assert
            List<string> actual = new PrivateObject(softwareSet).GetField("_IncludePaths") as List<string>;
            Assert.IsTrue(actual.Where(s => s == "SoftwareData").Count() == 1);
        }

    }
}
