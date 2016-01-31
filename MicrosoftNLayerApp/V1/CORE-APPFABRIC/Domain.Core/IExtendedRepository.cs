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
using System.Text;

using Microsoft.Samples.NLayerApp.Domain.Core;
using Microsoft.Samples.NLayerApp.Domain.Core.Specification;

namespace Microsoft.Samples.NLayerApp.Domain.Core
{
    /// <summary>
    /// Extended repository contract with bit tricky methods
    /// <see cref="Microsoft.Samples.NLayerApp.Domain.Core.IRepository{TEntity}"/>
    /// </summary>
    /// <typeparam name="TEntity">Type of entity for this repository</typeparam>
    public interface IExtendedRepository<TEntity> : IRepository<TEntity> 
        where TEntity:class
    {
        /// <summary>
        /// Sets modified entities into the repository. 
        /// When calling Commit() method in UnitOfWork 
        /// these changes will be saved into the storage
        /// </summary>
        /// <param name="items">Collection of items with changes</param>
        void Modify(ICollection<TEntity> items);

        /// <summary>
        /// Get all elements of type {K} in repository
        /// </summary>
        /// <typeparam name="KEntity">Subtype of {T}</typeparam>
        /// <returns>List of selected elements</returns>
        IEnumerable<KEntity> GetAll<KEntity>() where KEntity : TEntity;

        /// <summary>
        /// Get all elements of type {T} that matching a
        /// Specification <paramref name="specification"/>
        /// </summary>
        /// <param name="specification">Specification that result meet</param>
        /// <returns></returns>
        IEnumerable<KEntity> GetBySpec<KEntity>(ISpecification<KEntity> specification) where KEntity : class,TEntity;

        /// <summary>
        /// Get all elements of type {K} in repository
        /// </summary>
        /// <typeparam name="K">Subtype of {T}</typeparam>
        /// <typeparam name="S">Type of result in order expression</typeparam>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageCount">Number of elements in each page</param>
        /// <param name="orderByExpression">Order by expression for this query</param>
        /// <param name="ascending">Specify if order is ascending</param>
        /// <returns>List of selected elements</returns>
        IEnumerable<K> GetPagedElements<K, S>(int pageIndex, int pageCount, Expression<Func<K, S>> orderByExpression, bool ascending)
            where K : TEntity;


        /// <summary>
        /// Get  elements of type {K} in repository
        /// </summary>
        /// <param name="filter">Filter that each element do match</param>
        /// <param name="orderByExpression">Order by expression for this query</param>
        /// <param name="ascending">Specify if order is ascending</param>
        /// <returns>List of selected elements</returns>
        IEnumerable<TEntity> GetFilteredElements<S>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, S>> orderByExpression, bool ascending);

        /// <summary>
        /// Get all elements of type {K} in repository
        /// </summary>
        /// <typeparam name="K">Subtype of {T}</typeparam>
        /// <param name="filter">Filter that each element do match</param>
        /// <returns>List of selected elements</returns>
        IEnumerable<K> GetFilteredElements<K>(Expression<Func<K, bool>> filter)
            where K : TEntity;

        /// <summary>
        /// Get all elements of type {K} in repository
        /// </summary>
        /// <typeparam name="K">Subtype of {T}</typeparam>
        /// <typeparam name="S">Type of result in order expression</typeparam>
        /// <param name="filter">Filter that each element do match</param>
        ///<param name="orderbyExpression">Order by expression for this query</param>
        /// <param name="ascending">Specify if order is ascending</param>
        /// <returns>List of selected elements</returns>
        IEnumerable<K> GetFilteredElements<K, S>(Expression<Func<K, bool>> filter, Expression<Func<K, S>> orderbyExpression, bool ascending)
            where K : TEntity;

    }
}
