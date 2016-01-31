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

namespace Microsoft.Samples.NLayerApp.Domain.Core.Entities
{
    /// <summary>
    /// Custom extensions methods for STE entities
    /// </summary>
    public static class ChangeTrackerExtensions
    {
        /// <summary>
        /// Start tracking in all entities associated with <paramref name="entity"/>
        /// </summary>
        /// <param name="entity">Root entity</param>
        public static void StartTrackingAll(this IObjectWithChangeTracker entity)
        {
            using (ChangeTrackerIterator iterator = ChangeTrackerIterator.Create(entity))
                iterator.Execute(ste => ste.StartTracking());
        }

        /// <summary>
        /// Stop tracking in all entities associated with <paramref name="entity"/>
        /// </summary>
        /// <param name="entity">Root entity</param>
        public static void StopTrackingAll(this IObjectWithChangeTracker entity)
        {
            using (ChangeTrackerIterator iterator = ChangeTrackerIterator.Create(entity))
                iterator.Execute(ste => ste.StopTracking());
        }

        /// <summary>
        /// Accept all changes in all entities associated with <paramref name="entity"/>
        /// </summary>
        /// <param name="entity">Root entity</param>
        public static void AcceptAllChanges(this IObjectWithChangeTracker entity)
        {
            using (ChangeTrackerIterator iterator = ChangeTrackerIterator.Create(entity))
                iterator.Execute(ste => ste.AcceptChanges());
        }

        /// <summary>
        /// This method remove duplications in object graph on the client.
        /// <remarks>For more information about duplications problems in Selft Tracking Entities
        /// see http://blogs.msdn.com/b/diego/archive/2010/10/06/self-tracking-entities-applychanges-and-duplicate-entities.aspx
        /// </remarks>
        /// <example>
        /// customer.MergeWith(graphWithCustomer,(c1,c2)=>c1.IdCustomer == c2.IdCustomer);
        /// </example>
        /// </summary>
        /// <typeparam name="TEntity">Entity</typeparam>
        /// <typeparam name="TGraph">Grago</typeparam>
        /// <param name="entity">Type of entity</param>
        /// <param name="graph">Type of root graph</param>
        /// <param name="keyComparer">the key compararer</param>
        /// <returns>returns an instance from the graph with the same key or the original entity </returns>
        public static TEntity MergeWith<TEntity, TGraph>(this TEntity entity, TGraph graph, Func<TEntity, TEntity, bool> keyComparer)
            where TEntity : class,IObjectWithChangeTracker
            where TGraph : class,IObjectWithChangeTracker
        {
            using (ChangeTrackerIterator iterator = ChangeTrackerIterator.Create(graph))
            {
                return iterator.OfType<TEntity>().SingleOrDefault(e => keyComparer(entity, e)) ?? entity;
            }
        }
    }

}
