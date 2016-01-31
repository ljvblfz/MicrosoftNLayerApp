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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Microsoft.Samples.NLayerApp.Domain.Core.Entities
{
    /// <summary>
    /// Selft Tracking Entities graph iterator
    /// </summary>
    public sealed class ChangeTrackerIterator
        : IDisposable,IEnumerable<object>
    {
        #region Members

        List<IObjectWithChangeTracker> _items = new List<IObjectWithChangeTracker>();

        #endregion

        #region Constructor

        /// <summary>
        /// Default private constructor
        /// </summary>
         ChangeTrackerIterator() { }


        #endregion

        #region Factory Methods

        /// <summary>
        /// Create new Tracking Iterator for root entity of type {TEntity}
        /// </summary>
        /// <typeparam name="TEntity">Type of root entity</typeparam>
        /// <param name="entity">Root entity</param>
        /// <returns>STE Graph Iterator</returns>
        internal static ChangeTrackerIterator Create<TEntity>(TEntity entity)
            where TEntity : IObjectWithChangeTracker
        {
            ChangeTrackerIterator iterator = new ChangeTrackerIterator();
            iterator.Visit(entity);

            return iterator;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Execute action for each elements in graph
        /// </summary>
        /// <param name="action">Action to execute</param>
        internal void Execute(Action<IObjectWithChangeTracker> action)
        {
            _items.ForEach(action);
        }
        #endregion

        #region Visit and Traverse Methods

        /// <summary>
        /// Visti entity and traverse it
        /// </summary>
        /// <param name="entity">Entity to visit</param>
        void Visit(IObjectWithChangeTracker entity)
        {
            if (entity != null &&
                    !_items.Contains(entity))
            {
                _items.Add(entity);

                Traverse(entity);
            }
        }
        /// <summary>
        /// Traverse entity graph
        /// </summary>
        /// <param name="entity">Entity to traverse</param>
        void Traverse(IObjectWithChangeTracker entity)
        {
            Type type = entity.GetType();

            //recover all navigation properties 1:0..1
            IEnumerable<PropertyInfo> properties = from pInfo in type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                                   where typeof(IObjectWithChangeTracker).IsAssignableFrom(pInfo.PropertyType)
                                                   select pInfo;

            properties.ToList().ForEach(p => Visit((IObjectWithChangeTracker)p.GetValue(entity, null)));

            //recover all navigation properties for many associations
            properties = from pInfo in type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                         where typeof(ICollection).IsAssignableFrom(pInfo.PropertyType)
                         select pInfo;

            properties.ToList().ForEach(p =>
            {
                IEnumerable enumerable = (IEnumerable)p.GetValue(entity, null);
                if (enumerable != null )
                    enumerable.OfType<IObjectWithChangeTracker>().ToList().ForEach(e => Visit(e));
            });

        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Dispose all resources
        /// </summary>
        public void Dispose()
        {
            //clear entities references
            _items.Clear();
        }

        #endregion

        #region IEnumerable<object> Members

        /// <summary>
        /// Iterator enumerator
        /// </summary>
        /// <returns>Get the enumerator</returns>
        public IEnumerator<object> GetEnumerator()
        {
            return this._items.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        /// <summary>
        /// Iterator enumerator
        /// </summary>
        /// <returns>Get the enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._items.GetEnumerator();
        }

        #endregion
    }
}
