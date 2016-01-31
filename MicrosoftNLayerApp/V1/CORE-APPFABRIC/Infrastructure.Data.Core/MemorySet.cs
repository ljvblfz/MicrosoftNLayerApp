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
using System.Data.Objects;

namespace Microsoft.Samples.NLayerApp.Infrastructure.Data.Core
{
    /// <summary>
    /// In memory IObjectSet. This class is intended only
    /// for testing purposes.
    /// </summary>
    /// <typeparam name="TEntity">Type of elements in objectSet</typeparam>
    public sealed class MemorySet<TEntity> : IObjectSet<TEntity>
        where TEntity : class
    {
        #region Members

        List<TEntity> _InnerList;
        List<string> _IncludePaths;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor 
        /// </summary>
        /// <param name="innerList">A List{T} with inner values of this IObjectSet</param>
        public MemorySet(List<TEntity> innerList)
        {
            if (innerList == (List<TEntity>)null)
                throw new ArgumentNullException("innerList");

            _InnerList = innerList;
            _IncludePaths = new List<string>();


        }

        #endregion

        #region Methods

        /// <summary>
        /// Include path in query objects
        /// </summary>
        /// <param name="path">Path to include</param>
        /// <returns>IObjectSet with include path</returns>
        public MemorySet<TEntity> Include(string path)
        {
            if (String.IsNullOrEmpty(path))
                throw new ArgumentNullException("path");

            _IncludePaths.Add(path);

            return this;
        }
        #endregion

        #region IObjectSet<T> Members

        /// <summary>
        /// <see cref="System.Data.Objects.IObjectSet{T}"/>
        /// </summary>
        /// <param name="entity"><see cref="System.Data.Objects.IObjectSet{T}"/></param>
        public void AddObject(TEntity entity)
        {
            if (entity != null)
                _InnerList.Add(entity);
        }
        /// <summary>
        /// <see cref="System.Data.Objects.IObjectSet{T}"/>
        /// </summary>
        /// <param name="entity"><see cref="System.Data.Objects.IObjectSet{T}"/></param>
        public void Attach(TEntity entity)
        {
            if (entity != null
                &&
                !_InnerList.Contains(entity))
            {
                _InnerList.Add(entity);
            }
        }
        /// <summary>
        /// <see cref="System.Data.Objects.IObjectSet{T}"/>
        /// </summary>
        /// <param name="entity"><see cref="System.Data.Objects.IObjectSet{T}"/></param>
        public void Detach(TEntity entity)
        {
            if (entity != null)
                _InnerList.Remove(entity);
        }
        /// <summary>
        /// <see cref="System.Data.Objects.IObjectSet{T}"/>
        /// </summary>
        /// <param name="entity"><see cref="System.Data.Objects.IObjectSet{T}"/></param>
        public void DeleteObject(TEntity entity)
        {
            if (entity != null)
                _InnerList.Remove(entity);
        }

        #endregion

        #region IEnumerable<T> Members

        /// <summary>
        /// <see cref="System.Collections.IEnumerable.GetEnumerator"/>
        /// </summary>
        /// <returns><see cref="System.Collections.IEnumerable.GetEnumerator"/></returns>
        public IEnumerator<TEntity> GetEnumerator()
        {
            foreach (TEntity item in _InnerList)
                yield return item;
        }

        #endregion

        #region IEnumerable Members

        /// <summary>
        /// <see cref="System.Collections.IEnumerable.GetEnumerator"/>
        /// </summary>
        /// <returns><see cref="System.Collections.IEnumerable.GetEnumerator"/></returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        #region IQueryable Members

        /// <summary>
        /// <see cref="System.Linq.IQueryable{T}"/>
        /// </summary>
        public Type ElementType
        {
            get { return typeof(TEntity); }
        }
        /// <summary>
        /// <see cref="System.Linq.IQueryable{T}"/>
        /// </summary>
        public System.Linq.Expressions.Expression Expression
        {
            get { return _InnerList.AsQueryable().Expression; }
        }
        /// <summary>
        /// <see cref="System.Linq.IQueryable{T}"/>
        /// </summary>
        public IQueryProvider Provider
        {
            get { return _InnerList.AsQueryable().Provider; }
        }

        #endregion
    }
}
