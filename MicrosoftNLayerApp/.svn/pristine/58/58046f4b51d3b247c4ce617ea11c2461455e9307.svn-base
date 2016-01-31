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
using System.Collections.Generic;

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
        /// OfType extension method for IQueryable. This
        /// method is only for supporting mock OfType
        /// </summary>
        /// <typeparam name="KEntity">The type to filter the elements of the sequence on. </typeparam>
        /// <param name="queryable">The queryable object</param>
        /// <returns>
        /// A new IQueryable hat contains elements from
        /// the input sequence of type TResult
        /// </returns>
        public static IQueryable<KEntity> OfType<TEntity,KEntity>(this IQueryable<TEntity> queryable)
            where TEntity:class
            where KEntity:class,TEntity
        {
            ObjectQuery<TEntity> query = queryable as ObjectQuery<TEntity>;

             if (query != null)//if is a EF ObjectQuery object
                 return query.OfType<KEntity>();
             else
             {
                 //a fake or in memory object set for testing

                 MemorySet<TEntity> fakeQuery = queryable as MemorySet<TEntity>;
                 return fakeQuery.OfType<KEntity>();
             }
        }

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
        /// <example>
        /// var query = ReturnTheQuery();
        /// query = query.Include(customer=>customer.Orders);//"Orders"
        /// //or
        /// query 0 query.Include(customer=>customer.Orders.Select(o=>o.OrderDetails) //"Orders.OrderDetails"
        /// </example>
        /// </summary>
        /// <typeparam name="TEntity">Type of elements in IQueryable</typeparam>
        /// <typeparam name="TEntity">Type of navigated element</typeparam>
        /// <param name="queryable">Queryable object</param>
        /// <param name="path">Expression with path to include</param>
        /// <returns>Queryable object with include path information</returns>
        public static IQueryable<TEntity> Include<TEntity, TProperty>(this IQueryable<TEntity> queryable, Expression<Func<TEntity, TProperty>> path)
            where TEntity : class
        {
            string strPath;
            if (path == null)
                throw new ArgumentException(Resources.Messages.exception_ExpressionPathNotValid);
            if (TryParsePath(path.Body, out strPath))
            {
                return Include<TEntity>(queryable, strPath);
            }
            else
                throw new ArgumentException(Resources.Messages.exception_ExpressionPathNotValid);


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
        public static IQueryable<TEntity> Paginate<TEntity, S>(this IQueryable<TEntity> queryable, Expression<Func<TEntity, S>> orderBy, int pageIndex, int pageCount, bool ascending)
            where TEntity : class
        {
            ObjectQuery<TEntity> query = queryable as ObjectQuery<TEntity>;

            if (query != null)
            {
                //this paginate method use ESQL for solve problems with Parametrized queries
                //in L2E and Skip/Take methods

                string orderPath = AnalyzeExpressionPath(orderBy);



                return query.Skip(string.Format(CultureInfo.InvariantCulture, "it.{0} {1}", orderPath, (ascending) ? "asc" : "desc"), "@skip", new ObjectParameter("skip", (pageIndex) * pageCount))
                            .Top("@limit", new ObjectParameter("limit", pageCount));
            }
            else // for In-Memory object set
                return queryable.OrderBy(orderBy).Skip((pageIndex * pageCount)).Take(pageCount);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Analize simple expression path
        /// </summary>
        /// <typeparam name="TEntity">Type of entity</typeparam>
        /// <typeparam name="TProperty">Type of property member</typeparam>
        /// <param name="expression">The member access expression</param>
        /// <returns>The expression path</returns>
        static string AnalyzeExpressionPath<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> expression)
            where TEntity : class
        {
            if (expression == (Expression<Func<TEntity, TProperty>>)null)
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
                return body.Member.Name;
        }

        /// <summary>
        /// Parse EF "include" expression and translet this into convention strings.
        /// <example>
        /// e=>e.Order.Select(o=>o.OrderDetails) is translated to Order.OrderDetails
        /// </example>
        /// </summary>
        /// <param name="expression">The expression to parse</param>
        /// <param name="path">The parsed expression into string</param>
        /// <returns>True if parse is ok</returns>
        static bool TryParsePath(Expression expression, out string path)
        {
            path = null;
            Expression expression2 = expression.RemoveConvert();
            MemberExpression expression3 = expression2 as MemberExpression;
            MethodCallExpression expression4 = expression2 as MethodCallExpression;
            if (expression3 != null)
            {
                string str2;
                string name = expression3.Member.Name;
                if (!TryParsePath(expression3.Expression, out str2))
                {
                    return false;
                }
                path = (str2 == null) ? name : (str2 + "." + name);
            }
            else if (expression4 != null)
            {
                if ((expression4.Method.Name == "Select") && (expression4.Arguments.Count == 2))
                {
                    string str3;
                    if (!TryParsePath(expression4.Arguments[0], out str3))
                    {
                        return false;
                    }
                    if (str3 != null)
                    {
                        LambdaExpression expression5 = expression4.Arguments[1] as LambdaExpression;
                        if (expression5 != null)
                        {
                            string str4;
                            if (!TryParsePath(expression5.Body, out str4))
                            {
                                return false;
                            }
                            if (str4 != null)
                            {
                                path = str3 + "." + str4;
                                return true;
                            }
                        }
                    }
                }
                return false;
            }
            return true;
        }

        #endregion
    }
}
