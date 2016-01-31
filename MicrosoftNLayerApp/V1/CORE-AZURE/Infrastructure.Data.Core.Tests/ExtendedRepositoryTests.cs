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
using System.Collections.ObjectModel;
using System.Linq;

using Microsoft.Samples.NLayerApp.Domain.Core.Entities;
using Microsoft.Samples.NLayerApp.Domain.Core.Specification;
using Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.IoC;
using Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.Logging;
using Microsoft.Samples.NLayerApp.Infrastructure.Data.Core.Tests.StubAndMoles;
using Microsoft.Samples.NLayerApp.Domain.Core;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.Samples.NLayerApp.Infrastructure.Data.Core.Tests
{
        /// <summary>
        ///This is a test class for RepositoryTest and is intended
        ///to contain all common RepositoryTest Unit Tests
        ///</summary>
        [TestClass()]
        public class ExtendedRepositoryTests
        {
            /// <summary>
            ///A test for Container
            ///</summary>
            public void unitOfWorkTestHelper<T>()
                where T : class ,IObjectWithChangeTracker, new()
            {
                //Arrange
                IQueryableUnitOfWork actual = new UnitOfWorkStub();
                ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();

                //Act
                ExtendedRepository<T> target = new ExtendedRepository<T>(actual, traceManager);

                //Assert
                IUnitOfWork expected;
                expected = target.UnitOfWork as IUnitOfWork;

                Assert.AreEqual(expected, actual);

            }

            [TestMethod()]
            public void UoW_Creation_Test()
            {
                unitOfWorkTestHelper<Entity>();
            }
            [TestMethod()]
            [ExpectedException(typeof(ArgumentNullException))]
            public void UoW_Creation_NullunitOfWorkThrowArgumentNullException_Test()
            {
                ExtendedRepository<Entity> repository = new ExtendedRepository<Entity>(null, null);
            }

            [TestMethod()]
            [ExpectedException(typeof(ArgumentNullException))]
            public void ApplyChanges_NullEntityThrowNewArgumentNullException_Test()
            {
                //Arrange
                IQueryableUnitOfWork actual = new UnitOfWorkStub();
                ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();

                //Act
                ExtendedRepository<Entity> target = new ExtendedRepository<Entity>(actual, traceManager);

                //Assert
                target.Modify((Entity)null);
            }
            [TestMethod()]
            [ExpectedException(typeof(ArgumentNullException))]
            public void ApplyChanges_NullEntityCollectionThrowNewArgumentNullException_Test()
            {
                //Arrange
                IQueryableUnitOfWork actual = new UnitOfWorkStub();
                ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();

                //Act
                ExtendedRepository<Entity> target = new ExtendedRepository<Entity>(actual, traceManager);

                //Assert
                target.Modify((Collection<Entity>)null);
            }
            [TestMethod()]
            public void ApplyChanges_Test()
            {
                //Arrange
                IQueryableUnitOfWork actual = new UnitOfWorkStub();
                ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();

                //Act
                ExtendedRepository<Entity> target = new ExtendedRepository<Entity>(actual, traceManager);
                Entity item = target.GetAll().First();

                //Assert
                target.Modify(item);
            }
            [TestMethod()]
            public void ApplyChanges_Collection_Test()
            {
                //Arrange
                IQueryableUnitOfWork actual = new UnitOfWorkStub();
                ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();

                //Act
                ExtendedRepository<Entity> target = new ExtendedRepository<Entity>(actual, traceManager);
                ICollection<Entity> items = target.GetAll().ToList() as ICollection<Entity>;

                //Assert
                target.Modify(items);
            }
            /// <summary>
            ///A test for GetPagedElements
            ///</summary>
            [TestMethod()]
            [ExpectedException(typeof(ArgumentException))]
            public void GetPagedElements_InvalidPageIndexThrowArgumentException_Test()
            {
                IQueryableUnitOfWork unitOfWork = new UnitOfWorkStub();
                ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();

                ExtendedRepository<Entity> target = new ExtendedRepository<Entity>(unitOfWork, traceManager);
                int pageIndex = -1;
                int pageCount = 0;

                target.GetPagedElements<int>(pageIndex, pageCount, null, false);
            }
            /// <summary>
            ///A test for GetPagedElements
            ///</summary>
            [TestMethod()]
            [ExpectedException(typeof(ArgumentException))]
            public void GetPagedElements_InvalidPageCountThrowArgumentException_Test()
            {
                IQueryableUnitOfWork unitOfWork = new UnitOfWorkStub();
                ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();

                ExtendedRepository<Entity> target = new ExtendedRepository<Entity>(unitOfWork, traceManager);
                int pageIndex = 0;
                int pageCount = 0;

                target.GetPagedElements<int>(pageIndex, pageCount, null, false);
            }
            /// <summary>
            ///A test for GetPagedElements
            ///</summary>
            [TestMethod()]
            [ExpectedException(typeof(ArgumentNullException))]
            public void GetPagedElements_InvalidOrderExpressionThrowArgumentNullException_Test()
            {
                IQueryableUnitOfWork unitOfWork = new UnitOfWorkStub();
                ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();

                ExtendedRepository<Entity> target = new ExtendedRepository<Entity>(unitOfWork, traceManager);
                int pageIndex = 0;
                int pageCount = 1;


                target.GetPagedElements<int>(pageIndex, pageCount, null, false);
            }
            /// <summary>
            ///A test for GetPagedElements
            ///</summary>
            [TestMethod()]
            [ExpectedException(typeof(ArgumentException))]
            public void GetPagedElementsSubType_InvalidPageIndexThrowArgumentException_Test()
            {
                IQueryableUnitOfWork unitOfWork = new UnitOfWorkStub();
                ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();

                ExtendedRepository<SubEntity> target = new ExtendedRepository<SubEntity>(unitOfWork, traceManager);
                int pageIndex = -1;
                int pageCount = 0;

                target.GetPagedElements<SubEntity, int>(pageIndex, pageCount, null, false);
            }
            /// <summary>
            ///A test for GetPagedElements
            ///</summary>
            [TestMethod()]
            [ExpectedException(typeof(ArgumentException))]
            public void GetPagedElementsSubType_InvalidPageCountThrowArgumentException_Test()
            {
                IQueryableUnitOfWork unitOfWork = new UnitOfWorkStub();
                ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();

                ExtendedRepository<SubEntity> target = new ExtendedRepository<SubEntity>(unitOfWork, traceManager);
                int pageIndex = 1;
                int pageCount = -1;

                target.GetPagedElements<SubEntity, int>(pageIndex, pageCount, null, false);
            }
            /// <summary>
            ///A test for GetPagedElements
            ///</summary>
            [TestMethod()]
            [ExpectedException(typeof(ArgumentNullException))]
            public void GetPagedElementsSubType_InvalidOrderExpressionArgumentException_Test()
            {
                IQueryableUnitOfWork unitOfWork = new UnitOfWorkStub();
                ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();

                ExtendedRepository<SubEntity> target = new ExtendedRepository<SubEntity>(unitOfWork, traceManager);
                int pageIndex = 1;
                int pageCount = 1;

                target.GetPagedElements<SubEntity, int>(pageIndex, pageCount, null, false);
            }
            /// <summary>
            ///A test for GetPagedElements
            ///</summary>
            [TestMethod()]
            public void GetPagedElements_DescendingOrder_Test()
            {
                //Arrange
                IQueryableUnitOfWork unitOfWork = new UnitOfWorkStub();
                ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();

                ExtendedRepository<Entity> target = new ExtendedRepository<Entity>(unitOfWork, traceManager);
                int pageIndex = 0;
                int pageCount = 1;

                //Act
                IEnumerable<Entity> result = target.GetPagedElements(pageIndex, pageCount, e => e.Id > 0, false);

                //Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(result.Count(), pageCount);
            }
            /// <summary>
            ///A test for GetPagedElements
            ///</summary>
            [TestMethod()]
            public void GetPagedElementsSubType_DescendingOrder_Test()
            {
                //Arrange
                IQueryableUnitOfWork unitOfWork = new UnitOfWorkStub();
                ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();

                ExtendedRepository<Entity> target = new ExtendedRepository<Entity>(unitOfWork, traceManager);
                int pageIndex = 0;
                int pageCount = 1;

                //Act
                IEnumerable<SubEntity> result = target.GetPagedElements<SubEntity, int>(pageIndex, pageCount, e => e.Id, false);

                //Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(result.Count(), pageCount);
            }
            /// <summary>
            ///A test for GetPagedElements
            ///</summary>
            [TestMethod()]
            public void GetPagedElementsSubType_AscendingOrder_Test()
            {
                //Arrange
                IQueryableUnitOfWork unitOfWork = new UnitOfWorkStub();
                ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();

                ExtendedRepository<Entity> target = new ExtendedRepository<Entity>(unitOfWork, traceManager);
                int pageIndex = 0;
                int pageCount = 1;

                //Act
                IEnumerable<SubEntity> result = target.GetPagedElements<SubEntity, int>(pageIndex, pageCount, e => e.Id, true);

                //Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(result.Count(), pageCount);
            }
            /// <summary>
            ///A test for GetPagedElements
            ///</summary>
            [TestMethod()]
            public void GetPagedElements_AscendingOrder_Test()
            {
                //Arrange
                IQueryableUnitOfWork unitOfWork = new UnitOfWorkStub();
                ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();

                ExtendedRepository<Entity> target = new ExtendedRepository<Entity>(unitOfWork, traceManager);
                int pageIndex = 0;
                int pageCount = 1;

                //Act
                IEnumerable<Entity> result = target.GetPagedElements(pageIndex, pageCount, e => e.Id > 0, true);

                //Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(result.Count(), pageCount);
            }

            /// <summary>
            ///A test for GetFilteredElements
            ///</summary>
            [TestMethod()]
            [ExpectedException(typeof(ArgumentNullException))]
            public void GetFilteredElements_FilterNullThrowArgumentNullException_Test()
            {
                //Arrange
                IQueryableUnitOfWork unitOfWork = new UnitOfWorkStub();
                ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();

                ExtendedRepository<Entity> target = new ExtendedRepository<Entity>(unitOfWork, traceManager);

                //Act
                target.GetFilteredElements(null);
            }
            /// <summary>
            ///A test for GetFilteredElements
            ///</summary>
            [TestMethod()]
            [ExpectedException(typeof(ArgumentNullException))]
            public void GetFilteredElements_SpecificKOrder_AscendingOrderAndFilterNullThrowArgumentNullException_Test()
            {
                //Arrange
                IQueryableUnitOfWork unitOfWork = new UnitOfWorkStub();
                ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();

                ExtendedRepository<Entity> target = new ExtendedRepository<Entity>(unitOfWork, traceManager);

                //Act
                target.GetFilteredElements<int>(null, t => t.Id, true);
            }
            /// <summary>
            ///A test for GetFilteredElements
            ///</summary>
            [TestMethod()]
            [ExpectedException(typeof(ArgumentNullException))]
            public void GetFilteredElements_SpecificKOrder_DescendingOrderAndFilterNullThrowArgumentNullException_Test()
            {
                //Arrange
                IQueryableUnitOfWork unitOfWork = new UnitOfWorkStub();
                ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();

                ExtendedRepository<Entity> target = new ExtendedRepository<Entity>(unitOfWork, traceManager);

                //Act
                target.GetFilteredElements<int>(null, t => t.Id, false);
            }
            /// <summary>
            ///A test for GetFilteredElements
            ///</summary>
            [TestMethod()]
            [ExpectedException(typeof(ArgumentNullException))]
            public void GetFilteredElements_SpecificKFilterAndSOrder_ORderNullThrowArgumentNullException_Test()
            {
                //Arrange
                IQueryableUnitOfWork unitOfWork = new UnitOfWorkStub();
                ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();

                ExtendedRepository<Entity> target = new ExtendedRepository<Entity>(unitOfWork, traceManager);

                //Act
                target.GetFilteredElements<SubEntity, int>(t => t.Id == 3, null, false);
            }
            /// <summary>
            ///A test for GetFilteredElements
            ///</summary>
            [TestMethod()]
            [ExpectedException(typeof(ArgumentNullException))]
            public void GetFilteredElements_SpecificKFilterAndSOrder_FilterNullThrowArgumentNullException_Test()
            {
                //Arrange
                IQueryableUnitOfWork unitOfWork = new UnitOfWorkStub();
                ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();

                ExtendedRepository<Entity> target = new ExtendedRepository<Entity>(unitOfWork, traceManager);

                //Act
                target.GetFilteredElements<SubEntity, int>(null, t => t.Id, true);
            }
            /// <summary>
            ///A test for GetFilteredElements
            ///</summary>
            [TestMethod()]
            public void GetFilteredElements_SpecificKFilterAndSOrder_AscendingOrder_Test()
            {
                //Arrange
                IQueryableUnitOfWork unitOfWork = new UnitOfWorkStub();
                ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();

                ExtendedRepository<Entity> target = new ExtendedRepository<Entity>(unitOfWork, traceManager);

                //Act
                target.GetFilteredElements<SubEntity, int>(se => se.Id == 1, t => t.Id, true);
            }
            /// <summary>
            ///A test for GetFilteredElements
            ///</summary>
            [TestMethod()]
            public void GetFilteredElements_SpecificKFilterAndSOrder_DescendingOrder_Test()
            {
                //Arrange
                IQueryableUnitOfWork unitOfWork = new UnitOfWorkStub();
                ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();

                ExtendedRepository<Entity> target = new ExtendedRepository<Entity>(unitOfWork, traceManager);

                //Act
                target.GetFilteredElements<SubEntity, int>(se => se.Id == 1, t => t.Id, false);
            }
            /// <summary>
            ///A test for GetFilteredElements
            ///</summary>
            [TestMethod()]
            public void GetFilteredElements_SpecificKOrder_DescendingOrder_Test()
            {
                //Arrange
                IQueryableUnitOfWork unitOfWork = new UnitOfWorkStub();
                ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();

                ExtendedRepository<Entity> target = new ExtendedRepository<Entity>(unitOfWork, traceManager);

                //Act
                target.GetFilteredElements<int>(e => e.Id == 1, t => t.Id, false);
            }
            /// <summary>
            ///A test for GetFilteredElements
            ///</summary>
            [TestMethod()]
            public void GetFilteredElements_SpecificKOrder_Test()
            {
                //Arrange
                IQueryableUnitOfWork unitOfWork = new UnitOfWorkStub();
                ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();
                ExtendedRepository<Entity> target = new ExtendedRepository<Entity>(unitOfWork, traceManager);

                //Act
                target.GetFilteredElements<SubEntity>(e => e.Id == 1);
            }
            /// <summary>
            ///A test for GetFilteredElements
            ///</summary>
            [TestMethod()]
            [ExpectedException(typeof(ArgumentNullException))]
            public void GetFilteredElements_SpecificKOrder_NullFilterThrowArgumentNullException_Test()
            {
                //Arrange
                IQueryableUnitOfWork unitOfWork = new UnitOfWorkStub();
                ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();

                ExtendedRepository<Entity> target = new ExtendedRepository<Entity>(unitOfWork, traceManager);

                //Act
                target.GetFilteredElements<SubEntity>(null);
            }
            /// <summary>
            ///A test for GetFilteredElements
            ///</summary>
            [TestMethod()]
            [ExpectedException(typeof(ArgumentNullException))]
            public void GetFilteredElements_SpecificKOrderFullSignature_NullFilterThrowArgumentNullException_Test()
            {
                //Arrange
                IQueryableUnitOfWork unitOfWork = new UnitOfWorkStub();
                ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();

                ExtendedRepository<Entity> target = new ExtendedRepository<Entity>(unitOfWork, traceManager);

                //Act
                target.GetFilteredElements<int>(null, 1, 1, t => t.Id, true);
            }
            /// <summary>
            ///A test for GetFilteredElements
            ///</summary>
            [TestMethod()]
            public void GetFilteredElements_SpecificKOrder_AscendingOrder_Test()
            {
                //Arrange
                IQueryableUnitOfWork unitOfWork = new UnitOfWorkStub();
                ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();

                ExtendedRepository<Entity> target = new ExtendedRepository<Entity>(unitOfWork, traceManager);

                //Act
                target.GetFilteredElements<int>(e => e.Id == 1, t => t.Id, true);
            }
            /// <summary>
            ///A test for GetFilteredElements
            ///</summary>
            [TestMethod()]
            public void GetFilteredElementsTest()
            {
                //Arrange
                IQueryableUnitOfWork unitOfWork = new UnitOfWorkStub();
                ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();

                ExtendedRepository<Entity> target = new ExtendedRepository<Entity>(unitOfWork, traceManager);

                //Act
                IEnumerable<Entity> result = target.GetFilteredElements(e => e.Id == 1);

                //Assert
                Assert.IsNotNull(result);
                Assert.IsTrue(result.Count() == 1);
                Assert.IsTrue(result.First().Id == 1);

            }
            /// <summary>
            ///A test for GetFilteredElements
            ///</summary>
            [TestMethod()]
            [ExpectedException(typeof(ArgumentNullException))]
            public void GetFilteredAndOrderedElements_InvalidOrderByExpressionThrowArgumentNullException_Test()
            {
                //Arrange
                IQueryableUnitOfWork unitOfWork = new UnitOfWorkStub();
                ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();

                ExtendedRepository<Entity> target = new ExtendedRepository<Entity>(unitOfWork, traceManager);

                //Act
                IEnumerable<Entity> result = target.GetFilteredElements<int>(e => e.Id == 1, null, false);

                //Assert
                Assert.IsTrue(result != null);
                Assert.IsTrue(result.Count() == 1);
            }
            /// <summary>
            ///A test for GetFilteredElements
            ///</summary>
            [TestMethod()]
            [ExpectedException(typeof(ArgumentNullException))]
            public void GetFilteredAndOrderedAndPagedElements_InvalidOrderByExpressionThrowArgumentNullException_Test()
            {
                //Arrange
                IQueryableUnitOfWork unitOfWork = new UnitOfWorkStub();
                ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();

                ExtendedRepository<Entity> target = new ExtendedRepository<Entity>(unitOfWork, traceManager);
                int pageIndex = 0;
                int pageCount = 1;

                //Act
                IEnumerable<Entity> result = target.GetFilteredElements<int>(e => e.Id == 1, pageIndex, pageCount, null, false);

                //Assert
                Assert.IsTrue(result != null);
                Assert.IsTrue(result.Count() == 1);
            }
            /// <summary>
            ///A test for GetFilteredElements
            ///</summary>
            [TestMethod()]
            [ExpectedException(typeof(ArgumentException))]
            public void GetFilteredAndOrderedAndPagedElements_InvalidPageIndexThrowArgumentException_Test()
            {
                //Arrange
                IQueryableUnitOfWork unitOfWork = new UnitOfWorkStub();
                ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();

                ExtendedRepository<Entity> target = new ExtendedRepository<Entity>(unitOfWork, traceManager);
                int pageIndex = -1;
                int pageCount = 1;

                //Act
                IEnumerable<Entity> result = target.GetFilteredElements<int>(e => e.Id == 1, pageIndex, pageCount, null, false);
            }
            /// <summary>
            ///A test for GetFilteredElements
            ///</summary>
            [TestMethod()]
            [ExpectedException(typeof(ArgumentException))]
            public void GetFilteredAndOrderedAndPagedElements_InvalidPageCountThrowArgumentException_Test()
            {
                //Arrange
                IQueryableUnitOfWork unitOfWork = new UnitOfWorkStub();
                ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();

                ExtendedRepository<Entity> target = new ExtendedRepository<Entity>(unitOfWork, traceManager);
                int pageIndex = 0;
                int pageCount = 0;

                //Act
                IEnumerable<Entity> result = target.GetFilteredElements<int>(e => e.Id == 1, pageIndex, pageCount, null, false);
            }
            /// <summary>
            ///A test for GetFilteredElements
            ///</summary>
            [TestMethod()]
            public void GetFiltered_WithDescendingOrderedAndPagedElements_Test()
            {
                //Arrange
                IQueryableUnitOfWork unitOfWork = new UnitOfWorkStub();
                ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();

                ExtendedRepository<Entity> target = new ExtendedRepository<Entity>(unitOfWork, traceManager);
                int pageIndex = 0;
                int pageCount = 1;

                //Act
                IEnumerable<Entity> result = target.GetFilteredElements<int>(e => e.Id == 1, pageIndex, pageCount, e => e.Id, false);

                //Assert
                Assert.IsTrue(result != null);
                Assert.IsTrue(result.Count() == 1);
            }
            /// <summary>
            ///A test for GetFilteredElements
            ///</summary>
            [TestMethod()]
            public void GetFiltered_WithAscendingOrderedAndPagedElements_Test()
            {
                //Arrange
                IQueryableUnitOfWork unitOfWork = new UnitOfWorkStub();
                ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();

                ExtendedRepository<Entity> target = new ExtendedRepository<Entity>(unitOfWork, traceManager);
                int pageIndex = 0;
                int pageCount = 1;

                //Act
                IEnumerable<Entity> result = target.GetFilteredElements<int>(e => e.Id == 1, pageIndex, pageCount, e => e.Id, true);

                //Assert
                Assert.IsTrue(result != null);
                Assert.IsTrue(result.Count() == 1);
            }
            /// <summary>
            ///A test for GetAll
            ///</summary>
            [TestMethod()]
            public void GetAllTest()
            {
                //Arrange
                IQueryableUnitOfWork unitOfWork = new UnitOfWorkStub();
                ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();

                ExtendedRepository<Entity> target = new ExtendedRepository<Entity>(unitOfWork, traceManager);

                //Act
                IEnumerable<Entity> result = target.GetAll();

                //Assert
                Assert.IsNotNull(result);
                Assert.IsTrue(result.Count() == 3);
            }
            /// <summary>
            ///A test for GetAll
            ///</summary>
            [TestMethod()]
            public void GetAll_ForSubtypes_Test()
            {
                //Arrange
                IQueryableUnitOfWork unitOfWork = new UnitOfWorkStub();
                ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();

                ExtendedRepository<Entity> target = new ExtendedRepository<Entity>(unitOfWork, traceManager);

                //Act
                IEnumerable<SubEntity> result = target.GetAll<SubEntity>();

                //Assert
                Assert.IsNotNull(result);
                Assert.IsTrue(result.Count() == 1);
            }
            /// <summary>
            ///A test for Add
            ///</summary>
            [TestMethod()]
            public void AddTest()
            {
                //Arrange
                IQueryableUnitOfWork unitOfWork = new UnitOfWorkStub();
                ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();

                ExtendedRepository<Entity> target = new ExtendedRepository<Entity>(unitOfWork, traceManager);
                Entity entity = new Entity()
                {
                    Id = 4,
                    Field = "field 4"
                };


                //Act
                target.Add(entity);
                IEnumerable<Entity> result = target.GetAll();

                //Assert
                Assert.IsNotNull(result);
                Assert.IsTrue(result.Count() == 4);
                Assert.IsTrue(result.Contains(entity));
            }

            /// <summary>
            ///A test for Add
            ///</summary>
            [TestMethod()]
            [ExpectedException(typeof(ArgumentNullException))]
            public void Add_NullItemThrowArgumentNullException_Test()
            {
                //Arrange
                IQueryableUnitOfWork unitOfWork = new UnitOfWorkStub();
                ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();

                ExtendedRepository<Entity> target = new ExtendedRepository<Entity>(unitOfWork, traceManager);
                Entity entity = null;

                //Act
                target.Add(entity);
            }
            /// <summary>
            ///A test for Delte
            ///</summary>
            [TestMethod()]
            public void DeleteTest()
            {
                //Arrange
                IQueryableUnitOfWork unitOfWork = new UnitOfWorkStub();
                ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();

                ExtendedRepository<Entity> target = new ExtendedRepository<Entity>(unitOfWork, traceManager);

                //Act
                IEnumerable<Entity> result = target.GetAll();

                Entity firstEntity = result.First();
                target.Remove(firstEntity);

                IEnumerable<Entity> postResult = target.GetAll();

                //Assert
                Assert.IsNotNull(postResult);
                Assert.IsFalse(postResult.Contains(firstEntity));

            }
            /// <summary>
            ///A test for Delete
            ///</summary>
            [TestMethod()]
            [ExpectedException(typeof(ArgumentNullException))]
            public void Delete_NullItem_Test()
            {
                //Arrange
                IQueryableUnitOfWork unitOfWork = new UnitOfWorkStub();
                ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();

                ExtendedRepository<Entity> target = new ExtendedRepository<Entity>(unitOfWork, traceManager);
                Entity entity = null;

                //Act
                target.Remove(entity);
            }
            [TestMethod()]
            [ExpectedException(typeof(ArgumentNullException))]
            public void Attach_NullItem_Test()
            {
                //Arrange
                IQueryableUnitOfWork unitOfWork = new UnitOfWorkStub();
                ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();

                ExtendedRepository<Entity> target = new ExtendedRepository<Entity>(unitOfWork, traceManager);
                Entity entity = null;

                //Act
                target.RegisterItem(entity);
            }
            [TestMethod()]
            public void Attach_Test()
            {
                //Arrange
                IQueryableUnitOfWork unitOfWork = new UnitOfWorkStub();
                ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();

                ExtendedRepository<Entity> target = new ExtendedRepository<Entity>(unitOfWork, traceManager);
                Entity entity = new Entity() { Id = 5, Field = "test" };

                //Act
                target.RegisterItem(entity);

                //Assert
                Assert.IsTrue(target.GetFilteredElements(t => t.Id == 5).Count() == 1);
            }
            [TestMethod()]
            [ExpectedException(typeof(ArgumentNullException))]
            public void GetBySpec_NullSpecThrowArgumentNullException_Test()
            {
                //Arrange
                IQueryableUnitOfWork unitOfWork = new UnitOfWorkStub();
                ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();

                ExtendedRepository<Entity> target = new ExtendedRepository<Entity>(unitOfWork, traceManager);
                ISpecification<Entity> spec = new DirectSpecification<Entity>(t => t.Id == 1);

                //Act
                target.GetBySpec((ISpecification<Entity>)null);

            }
            [TestMethod()]
            public void GetBySpec_Test()
            {
                //Arrange
                IQueryableUnitOfWork unitOfWork = new UnitOfWorkStub();
                ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();

                ExtendedRepository<Entity> target = new ExtendedRepository<Entity>(unitOfWork, traceManager);
                ISpecification<Entity> spec = new DirectSpecification<Entity>(t => t.Id == 1);

                //Act
                IEnumerable<Entity> result = target.GetBySpec(spec);

                //Assert
                Assert.IsTrue(result.Count() == 1);

            }
            [TestMethod()]
            [ExpectedException(typeof(ArgumentNullException))]
            public void GetBySpecWithSubType_NullSpecThrowArgumentNullException_Test()
            {
                //Arrange
                IQueryableUnitOfWork unitOfWork = new UnitOfWorkStub();
                ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();

                ExtendedRepository<Entity> target = new ExtendedRepository<Entity>(unitOfWork, traceManager);

                //Act
                IEnumerable<Entity> result = target.GetBySpec<SubEntity>((ISpecification<SubEntity>)null);
            }
            [TestMethod()]
            public void GetBySpecWithSubType_Test()
            {
                //Arrange
                IQueryableUnitOfWork unitOfWork = new UnitOfWorkStub();
                ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();

                ExtendedRepository<Entity> target = new ExtendedRepository<Entity>(unitOfWork, traceManager);
                ISpecification<SubEntity> spec = new DirectSpecification<SubEntity>(t => t.Id == 3);

                //Act
                IEnumerable<Entity> result = target.GetBySpec<SubEntity>(spec);

                //Assert
                Assert.IsTrue(result.Count() == 1);
                Assert.IsTrue(result.Single().Id == 3);
            }
        }
}
