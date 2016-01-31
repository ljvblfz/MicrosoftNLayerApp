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
using Microsoft.Samples.NLayerApp.Domain.Core.Entities;

namespace Microsoft.Samples.NLayerApp.Domain.Core
{
    /// <summary>
    /// Contract for UnitOfWork pattern. For more
    /// references see http://martinfowler.com/eaaCatalog/unitOfWork.html or
    /// http://msdn.microsoft.com/en-us/magazine/dd882510.aspx
    /// In this solution sample Unit Of Work is implemented out-of-box in 
    /// ADO.NET Entity Framework persistence engine. But for academic
    /// purposed and for mantein PI ( Persistence Ignorant ) in Domain 
    /// this pattern is implemented.
    /// </summary>
    public interface IUnitOfWork
        :ISQL,IDisposable
    {
        /// <summary>
        /// Commit all changes made in  a container.
        /// </summary>
        ///<remarks>
        /// If entity have fixed properties and optimistic concurrency problem exists 
        /// exception is thrown
        ///</remarks>
        void Commit();

        /// <summary>
        /// Commit all changes made in  a container.
        /// </summary>
        ///<remarks>
        /// If entity have fixed properties and optimistic concurrency problem exists 
        /// client changes are refereshed
        ///</remarks>
        void CommitAndRefreshChanges();


        /// <summary>
        /// Rollback changes not stored in databse at 
        /// this moment. See references of UnitOfWork pattern
        /// </summary>
        void RollbackChanges();

        /// <summary>
        /// Apply changes made in item or related items in your graph. Really this one method
        /// for specify ( registerNew,registerDirty,registerDelete 
        /// and registerClean in traditional UoW docs )
        /// <remarks>
        /// Yeap, indeed IObjectWithChangeTracker may be a moot point...but.....
        /// For our example we have chosen to use for  productivity. In a future brach
        /// you can view more teorical aproach with POCO Entities
        /// You can view this method  ApplyChanges in EF or as a SaveOrUpdate/Merge in NH
        /// </remarks>
        /// </summary>
        /// <typeparam name="TEntity">Type of item</typeparam>
        /// <param name="item">Item with changes</param>
        void RegisterChanges<TEntity>(TEntity item)
            where TEntity : class, IObjectWithChangeTracker;
    }
}
