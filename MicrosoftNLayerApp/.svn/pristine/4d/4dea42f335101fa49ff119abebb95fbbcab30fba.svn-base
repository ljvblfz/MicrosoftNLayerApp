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

using Microsoft.Samples.NLayerApp.Domain.MainModule.Entities;

namespace Microsoft.Samples.NLayerApp.Application.MainModule.SalesManagement
{
    /// <summary>
    /// Contract for sales management application operations
    /// </summary>
    public interface ISalesManagementService : IDisposable
    {
        /// <summary>
        /// Add new order
        /// </summary>
        /// <param name="order">Order with data to add</param>
        void AddOrder(Order order);

        /// <summary>
        /// Change existing order
        /// </summary>
        /// <param name="order">Order with changes to modify</param>
        void ChangeOrder(Order order);

        /// <summary>
        /// Find orders by shipping specification
        /// </summary>
        /// <param name="shippingName">A shipping name</param>
        /// <param name="shippingAddress">A shipping address</param>
        /// <param name="shippingCity">A shipping city</param>
        /// <param name="shippingZip">A shipping zip</param>
        /// <returns>A collection of order that matches specification</returns>
        List<Order> FindOrdersByShippingData(string shippingName, string shippingAddress, string shippingCity, string shippingZip);

        /// <summary>
        /// Find orders in paged format
        /// </summary>
        /// <param name="pageIndex">Index of page<remarks>Index [0,n]</remarks></param>
        /// <param name="pageCount">Number of elements in each page</param>
        /// <returns>A collection of order in specified page index</returns>
        List<Order> FindPagedOrders(int pageIndex, int pageCount);

        /// <summary>
        /// Find orders by dates specification
        /// </summary>
        /// <param name="fromDate">Orders from this date</param>
        /// <param name="toDate">Orders to this date</param>
        /// <returns>A collection of stored ordes in this dates range</returns>
        List<Order> FindOrdersByDates(DateTime? fromDate, DateTime? toDate);


        /// <summary>
        /// Find orders by order informaiton
        /// </summary>
        /// <param name="customerId">A Customer identifier</param>
        /// <param name="fromDate">Order from this date</param>
        /// <param name="toDate">Orders to this date</param>
        /// <returns>A ollection of stored orders in this order information</returns>
        List<Order> FindOrdersByOrderInformation(int customerId, DateTime? fromDate, DateTime? toDate);

        /// <summary>
        /// Add new product
        /// </summary>
        /// <param name="product">Product item to add</param>
        void AddProduct(Product product);

        /// <summary>
        /// Modify existing product
        /// </summary>
        /// <param name="product">Existing product with changes</param>
        void ChangeProduct(Product product);

        /// <summary>
        /// Find products in pages
        /// </summary>
        /// <param name="pageIndex">Index of page</param>
        /// <param name="pageCount">Number of elements in page</param>
        /// <returns>A Collection of products in specified page index</returns>
        List<Product> FindPagedProducts(int pageIndex, int pageCount);

        /// <summary>
        /// Find products by publisher or description specification
        /// </summary>
        /// <param name="publisherName">Name of publisher </param>
        /// <param name="description">Description of product</param>
        /// <returns>A Collection of products that maches selected specification</returns>
        List<Product> FindProductBySpecification(string publisherName, string description);
    
    }
}
