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
using System.Linq;
using System.Linq.Expressions;
using System.Data.Objects;
using System.Globalization;

namespace Microsoft.Samples.NLayerApp.Infrastructure.Data.Core.Extensions
{

    /// <summary>
    /// Class for IQuerable extensions methods
    /// <remarks>
    /// Include method in IQueryable ( base contract for IObjectSet ) is 
    /// intended for mock Include method in ObjectQuery{T}.
    /// Paginate solve not parametrized queries issues with skip and take L2E methods
    /// </remarks>
    /// </summary>
    public static class IQueryableExtensions
    {

        #region Extension Methods

        /// <summary>
        /// Include method for IQueryable
        /// </summary>
        /// <typeparam name="TEntity">Type of elements</typeparam>
        /// <param name="queryable">Queryable object</param>
        /// <param name="path">Path to include</param>
        /// <returns>Queryable object with include path information</returns>
        public static IQueryable<TEntity> Include<TEntity>(this IQueryable<TEntity> queryable, string path)
            where TEntity : class
        {
            if (String.IsNullOrEmpty(path))
                throw new ArgumentNullException(Resources.Messages.exception_IncludePathCannotBeNullOrEmpty);


            ObjectQuery<TEntity> query = queryable as ObjectQuery<TEntity>;

            if (query != null)//if is a EF ObjectQuery object
                return query.Include(path);
            else
            {
                //a fake or in memory object set for testing
                MemorySet<TEntity> fakeQuery = queryable as MemorySet<TEntity>;
                return fakeQuery.Include(path);

            }
        }

        /// <summary>
        /// Include extension method for IQueryable
        /// </summary>
        /// <typeparam name="TEntity">Type of elements in IQueryable</typeparam>
        /// <param name="queryable">Queryable object</param>
        /// <param name="path">Expression with path to include</param>
        /// <returns>Queryable object with include path information</returns>
        public static IQueryable<TEntity> Include<TEntity>(this IQueryable<TEntity> queryable, Expression<Func<TEntity, object>> path)
            where TEntity : class
        {
            return Include<TEntity>(queryable, AnalyzeExpressionPath(path));
        }

        /// <summary>
        /// Paginate query in a specific page range
        /// </summary>
        /// <typeparam name="TEntity">Typeof entity in underlying query</typeparam>
        /// <typeparam name="S">Typeof ordered data value</typeparam>
        /// <param name="queryable">Query to paginate</param>
        /// <param name="orderBy">Order by expression used in paginate method
        /// <remarks>
        /// At this moment Order by expression only support simple order by c=>c.CustomerCode. If you need
        /// add more complex order functionality don't use this extension method
        /// </remarks>
        /// </param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageCount">Page count</param>
        /// <param name="ascending">order direction</param>
        /// <returns>A paged queryable</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        public static IQueryable<TEntity> Paginate<TEntity,S>(this IQueryable<TEntity> queryable, Expression<Func<TEntity, S>> orderBy,int pageIndex,int pageCount,bool ascending)
            where TEntity:class
        {
            ObjectQuery<TEntity> query = queryable as ObjectQuery<TEntity>;

            if (query != null)
            {
                //this paginate method use ESQL for solve problems with Parametrized queries
                //in L2E and Skip/Take methods

                string orderPath = AnalyzeExpressionPath<TEntity,S>(orderBy);

                return query.Skip(string.Format(CultureInfo.InvariantCulture,"it.{0} {1}", orderPath,(ascending)?"asc":"desc"), "@skip", new ObjectParameter("skip", (pageIndex) * pageCount))
                            .Top("@limit", new ObjectParameter("limit",pageCount));

            }
            else // for In-Memory object set
                return queryable.OrderBy(orderBy).Skip((pageIndex*pageCount)).Take(pageCount);
        }

        #endregion

        #region Private Methods

        static string AnalyzeExpressionPath<TEntity,S>(Expression<Func<TEntity, S>> expression)
            where TEntity:class
        {
            if ( expression ==(Expression<Func<TEntity,S>>)null)
                throw new ArgumentNullException(Resources.Messages.exception_ExpressionPathNotValid);

            MemberExpression body = expression.Body as MemberExpression;
            if (
                    (
                    (body == null)
                    ||
                    !body.Member.DeclaringType.IsAssignableFrom(typeof(TEntity))
                    )
                    ||
                    (body.Expression.NodeType != ExpressionType.Parameter))
            {
                throw new ArgumentException(Resources.Messages.exception_ExpressionPathNotValid);
            }
            else
                return  body.Member.Name;
        }
        #endregion
    }
}
