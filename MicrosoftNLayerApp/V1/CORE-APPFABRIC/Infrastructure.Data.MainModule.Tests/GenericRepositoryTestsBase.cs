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
using System.Reflection;
using System.Text;

using Microsoft.Samples.NLayerApp.Domain.Core;
using Microsoft.Samples.NLayerApp.Domain.Core.Entities;
using Microsoft.Samples.NLayerApp.Domain.Core.Specification;
using Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.IoC;
using Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.Logging;
using Microsoft.Samples.NLayerApp.Infrastructure.Data.Core;
using Microsoft.Samples.NLayerApp.Infrastructure.Data.MainModule.UnitOfWork;

using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Microsoft.Samples.NLayerApp.Infrastructure.Data.MainModule.Tests
{
    /// <summary>
    /// This is a base class for testing repositories. This base class
    /// implement all method defined in IRepository for any type. Inherit
    /// of this class to add test implementation in a concrete repository.
    /// For view all tests correctly in a TestView windows please add column Full Class Name
    /// </summary>
    /// <typeparam name="TEntity">Inner type of respository</typeparam>
    [TestClass()]
    [DeploymentItem("Microsoft.Samples.NLayerApp.Infrastructure.Data.MainModule.Mock.dll")]
    public abstract class RepositoryTestsBase<TEntity>
        where TEntity : class,IObjectWithChangeTracker, new()
    {

        #region Virtual and abstract elements for inheritance unit tests

        /// <summary>
        /// Specification of filter expression for a particular type
        /// </summary>
        public abstract Expression<Func<TEntity, bool>> FilterExpression { get; }
        /// <summary>
        /// Specification of order by expression for a particular type
        /// </summary>
        public abstract Expression<Func<TEntity, int>> OrderByExpression { get; }

        /// <summary>
        /// Get true if  a particular type is a parent of a hierarchy. If is true
        /// this base class add test for derived types in hierarchy
        /// </summary>
        public virtual bool HashNestedTypes
        {
            get
            {
                return false;
            }
        }

        #endregion

        #region Test Helper for testing purposes

        /// <summary>
        /// Get Active UnitOfWork for testing
        /// </summary>
        /// <param name="initializeContainer">True if unit of work is initialized</param>
        /// <returns>Active unit of work for testing</returns>
        public IMainModuleUnitOfWork GetUnitOfWork(bool initializeContainer = true)
        {
            // Get unit of work specified in unity configuration
            // Set active unit of work for 
            // testing with fake or real unit of work in application configuration file 
            // "defaultIoCContainer" setting

            return IoCFactory.Instance.CurrentContainer.Resolve<IMainModuleUnitOfWork>();
        }
        public ITraceManager GetTraceManager()
        {
            //Get configured trace manager

            return IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();
        }

        #endregion

        #region Test Methods


        [TestMethod()]
        public virtual void Repository_InvokeConstructor_Test()
        {
            //Arrange
            IQueryableUnitOfWork unitOfWork = GetUnitOfWork();
            ITraceManager traceManager = this.GetTraceManager();

            //Act
            Repository<TEntity> repository = new Repository<TEntity>(unitOfWork,traceManager);
            IUnitOfWork actual = repository.UnitOfWork;

            //Assret
            Assert.AreEqual(unitOfWork, actual);
        }
        [TestMethod()]
        public virtual void Repository_AddValidItem_Test()
        {
            //Arrange
            IQueryableUnitOfWork unitOfWork = GetUnitOfWork();
            ITraceManager traceManager = this.GetTraceManager();

            //Act
            Repository<TEntity> repository = new Repository<TEntity>(unitOfWork,traceManager);
            TEntity item = new TEntity();
            repository.Add(item);
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public virtual void Repository_AddNullItemThrowArgumentNullException_Test()
        {
            //Arrange
            IQueryableUnitOfWork unitOfWork = GetUnitOfWork();
            ITraceManager traceManager = this.GetTraceManager();
            //Act
            Repository<TEntity> repository = new Repository<TEntity>(unitOfWork,traceManager);
            repository.Add(null);
        }
        [TestMethod()]
        public virtual void Repository_DeleteValidItemTest()
        {
            //Arrange
            IQueryableUnitOfWork unitOfWork = GetUnitOfWork();
            ITraceManager traceManager = this.GetTraceManager();

            //Act
            Repository<TEntity> repository = new Repository<TEntity>(unitOfWork,traceManager);
            TEntity item = new TEntity();
            repository.Remove(item);

            
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public virtual void Repository_DeleteNullItemThrowNewArgumentNullException_Test()
        {
            //Arrange
            IQueryableUnitOfWork unitOfWork = GetUnitOfWork();
            ITraceManager traceManager = this.GetTraceManager();

            //Act
            Repository<TEntity> repository = new Repository<TEntity>(unitOfWork,traceManager);
            repository.Remove(null);
        }
        [TestMethod()]
        public virtual void Repository_ApplyChangesValidItem_Test()
        {
            //Arrange
            IQueryableUnitOfWork unitOfWork = GetUnitOfWork();
            ITraceManager traceManager = this.GetTraceManager();

            //Act
            Repository<TEntity> repository = new Repository<TEntity>(unitOfWork,traceManager);
            TEntity item = new TEntity();
            repository.Modify(item);
            
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public virtual void GenericRepositoryApplyChangesWithNullItemThrowNewArgumentNullException_Test()
        {
            //Arrange
            IQueryableUnitOfWork unitOfWork = GetUnitOfWork();
            ITraceManager traceManager = this.GetTraceManager();

            //Act
            Repository<TEntity> repository = new Repository<TEntity>(unitOfWork,traceManager);
            repository.Modify((TEntity)null);
        }
        [TestMethod()]
        public virtual void Repository_ApplyChangesInCollection_Test()
        {
            //Arrange
            IQueryableUnitOfWork unitOfWork = GetUnitOfWork();
            ITraceManager traceManager = this.GetTraceManager();

            //Act
            ExtendedRepository<TEntity> repository = new ExtendedRepository<TEntity>(unitOfWork,traceManager);
            List<TEntity> items = new List<TEntity>();
            items.Add(new TEntity());
            repository.Modify(items);

        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public virtual void Repository_ApplyChangesWithNullCollectionThrowNewArgumentNullException_Test()
        {
            //Arrange
            IQueryableUnitOfWork unitOfWork = GetUnitOfWork();
            ITraceManager traceManager = this.GetTraceManager();

            //Act
            ExtendedRepository<TEntity> repository = new ExtendedRepository<TEntity>(unitOfWork,traceManager);
            repository.Modify((List<TEntity>)null);
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public virtual void Repository_ConstructorWithNullContainerThrowNewArgumentNullException_Test()
        {
            //Arrange
            IQueryableUnitOfWork unitOfWork = null;
            ITraceManager traceManager = this.GetTraceManager();

            //Act
            Repository<TEntity> repository = new Repository<TEntity>(unitOfWork,traceManager);
        }
        [TestMethod()]
        public virtual void GetAllTest()
        {
            //Arrange
            IQueryableUnitOfWork unitOfWork = GetUnitOfWork();
            ITraceManager traceManager = this.GetTraceManager();

            Repository<TEntity> repository = new Repository<TEntity>(unitOfWork,traceManager);

            //Act
            IEnumerable<TEntity> entities = repository.GetAll();

            //If Entity is parent in hierarchy invoke GetAll<K> where K:TEntity
            //for all K types in assembly
            if (HashNestedTypes)
            {

                foreach (Type type in typeof(TEntity).Assembly.GetExportedTypes())
                {
                    if (typeof(TEntity) != type && typeof(TEntity).IsAssignableFrom(type))
                    {
                        MethodInfo genericGetAll = (from m
                                                        in repository.GetType().GetMethods()
                                                    where m.Name == "GetAll" && m.IsGenericMethod
                                                    select m).First();

                        var result = genericGetAll.MakeGenericMethod(new Type[] { type })
                                                  .Invoke(repository, null);

                        //Assert
                        Assert.IsNotNull(result);
                    }
                }
            }
        }
        [TestMethod()]
        public virtual void Repository_GetFilteredElements_Invoke_Test()
        {
            //Arrange
            IQueryableUnitOfWork unitOfWork = GetUnitOfWork();
            ITraceManager traceManager = this.GetTraceManager();

            Repository<TEntity> repository = new Repository<TEntity>(unitOfWork,traceManager);

            //Act
            IEnumerable<TEntity> entities = repository.GetFilteredElements(FilterExpression);

            //Assert
            Assert.IsNotNull(entities);
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Repository_GetFilteredElementsWithNullFilterThrowNewArgumentNullException_Test()
        {
            //Arrange
            IQueryableUnitOfWork unitOfWork = GetUnitOfWork();
            ITraceManager traceManager = this.GetTraceManager();

            Repository<TEntity> repository = new Repository<TEntity>(unitOfWork,traceManager);

            //Act
            IEnumerable<TEntity> entities = repository.GetFilteredElements(null);
        }
        [TestMethod()]
        public void Repository_GetFilteredElementsWithOrderAscending_Invoke_Test()
        {
            //Arrange
            IQueryableUnitOfWork unitOfWork = GetUnitOfWork();
            ITraceManager traceManager = this.GetTraceManager();

            Repository<TEntity> repository = new Repository<TEntity>(unitOfWork,traceManager);

            //Act
            IEnumerable<TEntity> entities = repository.GetFilteredElements<int>(FilterExpression, OrderByExpression,true);

            //Assert
            Assert.IsNotNull(entities);
        }
        [TestMethod()]
        public void Repository_GetFilteredElementsWithOrderDescending_Invoke_Test()
        {
            //Arrange
            IQueryableUnitOfWork unitOfWork = GetUnitOfWork();
            ITraceManager traceManager = this.GetTraceManager();

            Repository<TEntity> repository = new Repository<TEntity>(unitOfWork,traceManager);

            //Act
            IEnumerable<TEntity> entities = repository.GetFilteredElements<int>(FilterExpression, OrderByExpression,false);

            //Assert
            Assert.IsNotNull(entities);
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Repository_GetFilteredElementsWithOrderNullThrowNewArgumentNullException_Test()
        {
            //Arrange
            IQueryableUnitOfWork unitOfWork = GetUnitOfWork();
            ITraceManager traceManager = this.GetTraceManager();

            Repository<TEntity> repository = new Repository<TEntity>(unitOfWork,traceManager);

            //Act
            IEnumerable<TEntity> entities = repository.GetFilteredElements<int>(FilterExpression, null,false);
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Repository_GetFilteredElementsWithFilterNullAndOrderNotNullThrowNewArgumentNullException_Test()
        {
            //Arrange
            IQueryableUnitOfWork unitOfWork = GetUnitOfWork();
            ITraceManager traceManager = this.GetTraceManager();

            Repository<TEntity> repository = new Repository<TEntity>(unitOfWork,traceManager);

            //Act
            IEnumerable<TEntity> entities = repository.GetFilteredElements<int>(null, OrderByExpression,false);
        }
        [TestMethod()]
        public void Repository_GetFilteredElementsWithPaggingAscending_Invoke_Test()
        {
            //Arrange
            IQueryableUnitOfWork unitOfWork = GetUnitOfWork();
            ITraceManager traceManager = this.GetTraceManager();

            Repository<TEntity> repository = new Repository<TEntity>(unitOfWork,traceManager);
            int pageIndex = 0;
            int pageCount = 1;

            //Act
            IEnumerable<TEntity> entities = repository.GetFilteredElements(FilterExpression, pageIndex, pageCount, OrderByExpression,true);

            //Assert
            Assert.IsNotNull(entities);

        }
        [TestMethod()]
        public void Repository_GetFilteredElementsWithPaggingDescending_Test()
        {
            //Arrange
            IQueryableUnitOfWork unitOfWork = GetUnitOfWork();
            ITraceManager traceManager = this.GetTraceManager();

            Repository<TEntity> repository = new Repository<TEntity>(unitOfWork,traceManager);
            int pageIndex = 0;
            int pageCount = 1;

            //Act
            IEnumerable<TEntity> entities = repository.GetFilteredElements(FilterExpression, pageIndex, pageCount, OrderByExpression, false);

            //Assert
            Assert.IsNotNull(entities);

        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void Repository_GetFilteredElementsWithPaggingInvalidPageIndexThrowNewArgumentException_Test()
        {
            //Arrange
            IQueryableUnitOfWork unitOfWork = GetUnitOfWork();
            ITraceManager traceManager = this.GetTraceManager();

            Repository<TEntity> repository = new Repository<TEntity>(unitOfWork,traceManager);
            int pageIndex = -1;
            int pageCount = 1;

            //Act
            IEnumerable<TEntity> entities = repository.GetFilteredElements(FilterExpression, pageIndex, pageCount, OrderByExpression, false);

        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void Repository_GetFilteredElementsWithPaggingInvalidPageCountThrowNewArgumentException_Test()
        {
            //Arrange
            IQueryableUnitOfWork unitOfWork = GetUnitOfWork();
            ITraceManager traceManager = this.GetTraceManager();

            Repository<TEntity> repository = new Repository<TEntity>(unitOfWork,traceManager);
            int pageIndex = 1;
            int pageCount = 0;

            //Act
            IEnumerable<TEntity> entities = repository.GetFilteredElements(FilterExpression, pageIndex, pageCount, OrderByExpression, false);
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Repository_GetFilteredElementsWithPaggingNullOrderExpressionThrowNewArgumentNullException_Test()
        {
            //Arrange
            IQueryableUnitOfWork unitOfWork = GetUnitOfWork();
            ITraceManager traceManager = this.GetTraceManager();

            Repository<TEntity> repository = new Repository<TEntity>(unitOfWork,traceManager);
            int pageIndex = 1;
            int pageCount = 1;

            //Act
            IEnumerable<TEntity> entities = repository.GetFilteredElements<int>(FilterExpression, pageIndex, pageCount, null, false);

        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Repository_GetFilteredElementsWithPaggingNullFilterExpressionThrowNewArgumentNullException_Test()
        {
            //Arrange
            IQueryableUnitOfWork unitOfWork = GetUnitOfWork();
            ITraceManager traceManager = this.GetTraceManager();

            Repository<TEntity> repository = new Repository<TEntity>(unitOfWork,traceManager);
            int pageIndex = 1;
            int pageCount = 1;

            //Act
            IEnumerable<TEntity> entities = repository.GetFilteredElements<int>(null, pageIndex, pageCount, null, false);

        }
        [TestMethod()]
        public void GenericrRepository_GetPagedElementsAscending_Invoke_Test()
        {
            //Arrange
            IQueryableUnitOfWork unitOfWork = GetUnitOfWork();
            ITraceManager traceManager = this.GetTraceManager();

            Repository<TEntity> repository = new Repository<TEntity>(unitOfWork,traceManager);
            int pageIndex = 0;
            int pageCount = 1;

            //Act
            IEnumerable<TEntity> entities = repository.GetPagedElements<int>(pageIndex, pageCount, OrderByExpression,true);

            //Assert
            Assert.IsNotNull(entities);

        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void Repository_GetPagedElementsInvalidPageIndexThrowNewArgumentException_Test()
        {
            //Arrange
            IQueryableUnitOfWork unitOfWork = GetUnitOfWork();
            ITraceManager traceManager = this.GetTraceManager();

            Repository<TEntity> repository = new Repository<TEntity>(unitOfWork,traceManager);
            int pageIndex = -1;
            int pageCount = 1;

            //Act
            IEnumerable<TEntity> entities = repository.GetPagedElements<int>(pageIndex, pageCount, OrderByExpression,false);
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void Repository_GetPagedElementsInvalidPageCount_ThrowNewArgumentException_Test()
        {
            //Arrange
            IQueryableUnitOfWork unitOfWork = GetUnitOfWork();
            ITraceManager traceManager = this.GetTraceManager();

            Repository<TEntity> repository = new Repository<TEntity>(unitOfWork,traceManager);
            int pageIndex = 0;
            int pageCount = 0;

            //Act
            IEnumerable<TEntity> entities = repository.GetPagedElements<int>(pageIndex, pageCount, OrderByExpression,false);
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Repository_GetPagedElementsInvalidOrderByExpressionThrowNewArgumentNullException_Test()
        {
            //Arrange
            IQueryableUnitOfWork unitOfWork = GetUnitOfWork();
            ITraceManager traceManager = this.GetTraceManager();

            Repository<TEntity> repository = new Repository<TEntity>(unitOfWork,traceManager);
            int pageIndex = 0;
            int pageCount = 1;

            //Act
            IEnumerable<TEntity> entities = repository.GetPagedElements<int>(pageIndex, pageCount, null,false);
        }
        [TestMethod()]
        public void Repository_GetPagedElementsDescending_Invoke_Test()
        {
            //Arrange
            IQueryableUnitOfWork unitOfWork = GetUnitOfWork();
            ITraceManager traceManager = this.GetTraceManager();

            Repository<TEntity> repository = new Repository<TEntity>(unitOfWork,traceManager);
            int pageIndex = 0;
            int pageCount = 1;
            bool ascending = false;

            //Act
            IEnumerable<TEntity> entities = repository.GetPagedElements<int>(pageIndex, pageCount, OrderByExpression, ascending);

            //Assert
            Assert.IsNotNull(entities);
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Repository_GetPagedElementsWithNullSpecThrowNewArgumentNullException_Test()
        {
            //Arrange
            IQueryableUnitOfWork unitOfWork = GetUnitOfWork();
            ITraceManager traceManager = this.GetTraceManager();
            Repository<TEntity> repository = new Repository<TEntity>(unitOfWork,traceManager);
            int pageIndex = 0;
            int pageCount = 10;

            //Act
            IEnumerable<TEntity> entities = repository.GetPagedElements<int>(pageIndex, pageCount, OrderByExpression,null, false);
        }
        [TestMethod()]
        public void Repository_GetPagedElementsWithSpec_Invoke_Test()
        {
            //Arrange
            IQueryableUnitOfWork unitOfWork = GetUnitOfWork();
            ITraceManager traceManager = this.GetTraceManager();

            Repository<TEntity> repository = new Repository<TEntity>(unitOfWork,traceManager);
            ISpecification<TEntity> spec = new TrueSpecification<TEntity>();

            int pageIndex = 0;
            int pageCount = 10;

            //Act
            IEnumerable<TEntity> entities = repository.GetPagedElements<int>(pageIndex, pageCount, OrderByExpression, spec, false);
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Repository_GetBySpecWithNullSpecThrowArgumentNullException_Test()
        {
            //Arrange
            IQueryableUnitOfWork unitOfWork = GetUnitOfWork();
            ITraceManager traceManager = this.GetTraceManager();

            Repository<TEntity> repository = new Repository<TEntity>(unitOfWork,traceManager);

            //Act
            repository.GetBySpec((ISpecification<TEntity>)null);
        }
        [TestMethod()]
        public void Repository_GetBySpecDirectSpec_Invoke_Test()
        {
            //Arrange
            IQueryableUnitOfWork unitOfWork = GetUnitOfWork();
            ITraceManager traceManager = this.GetTraceManager();

            Repository<TEntity> repository = new Repository<TEntity>(unitOfWork,traceManager);
            ISpecification<TEntity> specification = new DirectSpecification<TEntity>(this.FilterExpression);

            //Act
            IEnumerable<TEntity> result = repository.GetBySpec(specification);

            //Assert
            Assert.IsNotNull(result);
        }
        #endregion
    }
}
