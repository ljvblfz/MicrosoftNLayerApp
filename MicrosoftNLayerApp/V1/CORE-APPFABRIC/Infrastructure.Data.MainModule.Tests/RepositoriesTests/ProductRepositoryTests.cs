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

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Microsoft.Samples.NLayerApp.Domain.MainModule.Entities;
using Microsoft.Samples.NLayerApp.Domain.MainModule.Products;
using Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.Logging;
using Microsoft.Samples.NLayerApp.Infrastructure.Data.MainModule.Repositories;
using Microsoft.Samples.NLayerApp.Infrastructure.Data.MainModule.UnitOfWork;


namespace Microsoft.Samples.NLayerApp.Infrastructure.Data.MainModule.Tests.RepositoriesTests
{
    [TestClass()]
    public class ProductRepositoryTests
        :RepositoryTestsBase<Product>
    {
        int productId = 1;
        public override System.Linq.Expressions.Expression<Func<Product, bool>> FilterExpression
        {
            get { return p => p.ProductId == productId; }
        }

        public override System.Linq.Expressions.Expression<Func<Product, int>> OrderByExpression
        {
            get { return p=>p.ProductId;}
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FindProductByProductInformation_NullSpecThrowNewArgumentNullException_Test()
        {
            //Arrange
            IMainModuleUnitOfWork context = this.GetUnitOfWork();
            ITraceManager traceManager = this.GetTraceManager();

            ProductRepository repository = new ProductRepository(context,traceManager);

            //Act
            repository.GetBySpec(null);
        }
        [TestMethod()]
        public void FindProductByProductInformation_Invoke_Test()
        {
            //Arrange
            IMainModuleUnitOfWork context = this.GetUnitOfWork();
            ITraceManager traceManager = this.GetTraceManager();

            ProductRepository repository = new ProductRepository(context,traceManager);

            
            string publisher = "Krasis Press";
            string description = null;
            ProductInformationSpecification specification = new ProductInformationSpecification(publisher, description);

            //Act
            IEnumerable<Product> result = repository.GetBySpec(specification);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() > 0);
        }
    }
}
