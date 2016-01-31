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

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Microsoft.Samples.NLayerApp.Infrastructure.Data.Core.Extensions;
using Microsoft.Samples.NLayerApp.Infrastructure.Data.Core.Tests.StubAndMoles;


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
        [ExpectedException(typeof(ArgumentNullException))]
        public void IncludeWithMemberExpression_Invoke_NullPathThrowArgumentNullException()
        {
            //Arrange
            List<Entity> entities = new List<Entity>()
            {
                new Entity(){Id=0,Field="field"}
            };

            MemorySet<Entity> objectSet = entities.ToMemorySet();

            //Act
            ((IQueryable<Entity>)objectSet).Include((Expression<Func<Entity,object>>)null);
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
        public void IncludeWithMemberExpression_Invoke()
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
    }
}
