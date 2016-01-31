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
using Microsoft.Samples.NLayerApp.Domain.Core.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.Samples.NLayerApp.Domain.Core.Tests
{
    /// <summary>
    ///This is a test class for ChangeTrackerIteratorTest and is intended
    ///to contain all ChangeTrackerIteratorTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ChangeTrackerIteratorTest
    {

        /// <summary>
        ///A test for ChangeTrackerIterator Constructor
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Microsoft.Samples.NLayerApp.Domain.Core.Entities.dll")]
        public void ChangeTrackerIterator_Constructor_Test()
        {
            PrivateObject privateObject = new PrivateObject(typeof(ChangeTrackerIterator), null, null);

        }

       
        [TestMethod()]
        public void ChangeTrackerIterator_InvokeCreateMethod_Test()
        {
           CreateTestHelper<Master>(GetFakeData());
        }

        [TestMethod()]
        public void ChangeTrackerIterator_CheckGraph_Test()
        {
            //Arrange
            Master fake = GetFakeData();
            ChangeTrackerIterator iterator = ChangeTrackerIterator.Create<Master>(fake);

            //Act
            PrivateObject privateObject = new PrivateObject(iterator);
            List<IObjectWithChangeTracker> items = (List<IObjectWithChangeTracker>)privateObject.GetField("_items");

            //Assert
            Assert.IsTrue(items.Count == 3);
            Assert.IsTrue(items.Contains(fake));
            Assert.IsTrue(items.Contains(fake.ToOne));
            Assert.IsTrue(items.Contains(fake.ToMany[0]));
        }

       

        /// <summary>
        ///A test for Dispose
        ///</summary>
        [TestMethod()]
        public void ChangeTrackerIterator_InvokeDispose_Test()
        {
            //arrange
            ChangeTrackerIterator actual;
            actual = ChangeTrackerIterator.Create<Master>(GetFakeData());

            PrivateObject privateObject = new PrivateObject(actual);
            

            //act
            List<IObjectWithChangeTracker> items;
            items = (List<IObjectWithChangeTracker>)privateObject.GetField("_items");

            //assert
            Assert.IsTrue(items.Count > 0);

            //act
            actual.Dispose();
            items = (List<IObjectWithChangeTracker>)privateObject.GetField("_items");

            //assert
            Assert.IsTrue(items.Count == 0);
        }

        [TestMethod()]
        public void ChangeTrackerStopAllTracking_Invoke_Test()
        {
            //arrange
            Master fake = GetFakeData();

            //act and assert

            
            fake.StopTrackingAll();

            
            Assert.IsFalse(fake.ChangeTracker.ChangeTrackingEnabled);
            Assert.IsFalse(fake.ToOne.ChangeTracker.ChangeTrackingEnabled);
            Assert.IsFalse(fake.ToMany[0].ChangeTracker.ChangeTrackingEnabled);

        }
        [TestMethod()]
        public void ChangeTrackerStartAllTracking_Invoke_Test()
        {
            //arrange
            Master fake = GetFakeData();

            //act and assert

            
            fake.StartTrackingAll();
            
            Assert.IsTrue(fake.ChangeTracker.ChangeTrackingEnabled);
            Assert.IsTrue(fake.ToOne.ChangeTracker.ChangeTrackingEnabled);
            Assert.IsTrue(fake.ToMany[0].ChangeTracker.ChangeTrackingEnabled);

        }
        [TestMethod()]
        public void ChangeTrackerAcceptAllChanges_Invoke_Test()
        {
            //arrange
            Master fake = GetFakeData();

            //act and assert

            fake.AcceptAllChanges();

            Assert.IsTrue(fake.ChangeTracker.State == ObjectState.Unchanged);
            Assert.IsTrue(fake.ToOne.ChangeTracker.State == ObjectState.Unchanged);
            Assert.IsTrue(fake.ToMany[0].ChangeTracker.State == ObjectState.Unchanged);
        }


        [TestMethod()]
        public void ChangeTrackerMergeWith_Invoke_Test()
        {
            //Arrange
            Master master = GetFakeData();

            //Act

            DetailOneToOne newToOne = new DetailOneToOne()
            {
                DetailOneToOneId = 1
            };
            master.ToOne = newToOne.MergeWith(master, (dto1, dto2) => dto1.DetailOneToOneId == dto2.DetailOneToOneId);

            //Assert
            Assert.IsFalse(Object.ReferenceEquals(master.ToOne, newToOne));

            master.ToOne = null;

            master.ToOne = newToOne.MergeWith(master, (dto1, dto2) => dto1.DetailOneToOneId == dto2.DetailOneToOneId);

            Assert.IsTrue(object.ReferenceEquals(master.ToOne, newToOne));
        }


        #region Helper Methods

        /// <summary>
        ///A test for Create
        ///</summary>
        public void CreateTestHelper<TEntity>(TEntity entity)
            where TEntity : IObjectWithChangeTracker
        {


            ChangeTrackerIterator actual;
            actual = ChangeTrackerIterator.Create<TEntity>(entity);
            Assert.IsNotNull(actual);

        }

        private static Master GetFakeData()
        {
            TrackableCollection<DetailToMany> manyDetail = new TrackableCollection<DetailToMany>();
            manyDetail.Add(new DetailToMany() { DetailToManyId = 1 });
            Master master = new Master()
            {
                MasterId = 1,
                ToOne = new DetailOneToOne()
                {
                    DetailOneToOneId = 1
                },
                ToMany = manyDetail
            };
            return master;
        }

        #endregion

        #region Nested Classes

        public class Master : IObjectWithChangeTracker
        {
            public int MasterId { get; set; }

            public DetailOneToOne ToOne { get; set; }

            public TrackableCollection<DetailToMany> ToMany { get; set; }

            

            #region IObjectWithChangeTracker Members

            ObjectChangeTracker tracker = new ObjectChangeTracker();

            public ObjectChangeTracker ChangeTracker
            {
                get { return tracker; }
            }

            #endregion
        }

        public class DetailOneToOne : IObjectWithChangeTracker
        {
            public int DetailOneToOneId { get; set; }

            #region IObjectWithChangeTracker Members

            ObjectChangeTracker tracker = new ObjectChangeTracker();
            public ObjectChangeTracker ChangeTracker
            {
                get { return tracker; }
            }

            #endregion
        }

        public class DetailToMany : IObjectWithChangeTracker
        {
            public int DetailToManyId { get; set; }

            #region IObjectWithChangeTracker Members

            ObjectChangeTracker tracker = new ObjectChangeTracker();
            public ObjectChangeTracker ChangeTracker
            {
                get { return tracker; }
            }

            #endregion
        }

        #endregion

    }
}
