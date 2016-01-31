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

using Microsoft.Samples.NLayerApp.Domain.MainModule.Countries;
using Microsoft.Samples.NLayerApp.Domain.MainModule.Customers;
using Microsoft.Samples.NLayerApp.Domain.MainModule.Entities;
using Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.Logging;
using Microsoft.Samples.NLayerApp.Infrastructure.Data.MainModule.Repositories;
using Microsoft.Samples.NLayerApp.Infrastructure.Data.MainModule.UnitOfWork;


namespace Microsoft.Samples.NLayerApp.Infrastructure.Data.MainModule.Tests.RepositoriesTests
{
    [TestClass()]
    public class CountryRepositoryTests
        : RepositoryTestsBase<Country>
    {
        int countryId = 1;
        public override System.Linq.Expressions.Expression<Func<Country, bool>> FilterExpression
        {
            get { return c => c.CountryId == countryId; }
        }

        public override System.Linq.Expressions.Expression<Func<Country, int>> OrderByExpression
        {
            get { return c => c.CountryId; }
        }

        [TestMethod()]
        public void FindCountriesByName_Invoke_Test()
        {
            //Arrange 
            IMainModuleUnitOfWork context = GetUnitOfWork();
            ITraceManager traceManager = this.GetTraceManager();
            ICountryRepository repository = new CountryRepository(context, traceManager);

            string name = "Germany";
            CountryNameSpecification spec =new CountryNameSpecification(name);

            //Act
            IEnumerable<Country> countries = repository.GetBySpec(spec);

            //Assert
            Assert.IsNotNull(countries);
            Assert.IsTrue(countries.Count() > 0);
        }
    }
}
