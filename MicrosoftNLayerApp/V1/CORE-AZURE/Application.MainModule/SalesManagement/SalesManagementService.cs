using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Samples.NLayerApp.Domain.Core;
using Microsoft.Samples.NLayerApp.Domain.Core.Specification;
using Microsoft.Samples.NLayerApp.Domain.MainModule.Entities;
using Microsoft.Samples.NLayerApp.Domain.MainModule.Orders;
using Microsoft.Samples.NLayerApp.Domain.MainModule.Products;

namespace Microsoft.Samples.NLayerApp.Application.MainModule.SalesManagement
{
    /// <summary>
    /// ISalesManagementService implementations
    /// <see cref="Microsoft.Samples.NLayerApp.Application.MainModule.SalesManagement.ISalesManagementService"/>
    /// </summary>
    public class SalesManagementService
        : ISalesManagementService
    {
        #region Members

        IOrderRepository _orderRepository;
        IProductRepository _productRepository;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor for order service
        /// </summary>
        /// <param name="orderRepository">Order repository dependency, this dependency is resolved with IoC</param>
        /// <param name="productRepository">Product repository dependency, this dependency is resolved with IoC</param>
        public SalesManagementService(IOrderRepository orderRepository, IProductRepository productRepository)
        {
            if (orderRepository == (IOrderRepository)null)
                throw new ArgumentNullException(Resources.Messages.exception_DependenciesAreNotInitialized);

            if (productRepository == (IProductRepository)null)
                throw new ArgumentNullException(Resources.Messages.exception_DependenciesAreNotInitialized);

            _productRepository = productRepository;

            _orderRepository = orderRepository;
        }

        #endregion

        #region ISalesManagementService

        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.Application.MainModule.SalesManagement.ISalesManagementService"/>
        /// </summary>
        /// <param name="pageIndex"><see cref="Microsoft.Samples.NLayerApp.Application.MainModule.SalesManagement.ISalesManagementService"/></param>
        /// <param name="pageCount"><see cref="Microsoft.Samples.NLayerApp.Application.MainModule.SalesManagement.ISalesManagementService"/></param>
        /// <returns><see cref="Microsoft.Samples.NLayerApp.Application.MainModule.SalesManagement.ISalesManagementService"/></returns>
        public List<Order> FindPagedOrders(int pageIndex, int pageCount)
        {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");

            //query repository
            return _orderRepository.GetPagedElements(pageIndex, pageCount, o => o.OrderId, true)
                                  .ToList();
        }

        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.Application.MainModule.SalesManagement.ISalesManagementService"/>
        /// </summary>
        /// <param name="from"><see cref="Microsoft.Samples.NLayerApp.Application.MainModule.SalesManagement.ISalesManagementService"/></param>
        /// <param name="to"><see cref="Microsoft.Samples.NLayerApp.Application.MainModule.SalesManagement.ISalesManagementService"/></param>
        /// <returns><see cref="Microsoft.Samples.NLayerApp.Application.MainModule.SalesManagement.ISalesManagementService"/></returns>
        public List<Order> FindOrdersByDates(DateTime? @from, DateTime? to)
        {
            //Create specification
            OrderDateSpecification dateSpecification = new OrderDateSpecification(@from, to);

            //query repository
            return _orderRepository.GetBySpec(dateSpecification as ISpecification<Order>)
                                  .ToList();
        }
        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.Application.MainModule.SalesManagement.ISalesManagementService"/>
        /// </summary>
        /// <param name="shippingName"><see cref="Microsoft.Samples.NLayerApp.Application.MainModule.SalesManagement.ISalesManagementService"/></param>
        /// <param name="shippingAddress"><see cref="Microsoft.Samples.NLayerApp.Application.MainModule.SalesManagement.ISalesManagementService"/></param>
        /// <param name="shippingCity"><see cref="Microsoft.Samples.NLayerApp.Application.MainModule.SalesManagement.ISalesManagementService"/></param>
        /// <param name="shippingZip"><see cref="Microsoft.Samples.NLayerApp.Application.MainModule.SalesManagement.ISalesManagementService"/></param>
        /// <returns><see cref="Microsoft.Samples.NLayerApp.Application.MainModule.SalesManagement.ISalesManagementService"/></returns>
        public List<Order> FindOrdersByShippingData(string shippingName, string shippingAddress, string shippingCity, string shippingZip)
        {
            //Create specification
            OrderShippingSpecification dateSpecification = new OrderShippingSpecification(shippingName, shippingAddress, shippingCity, shippingZip);

            //query repository
            return _orderRepository.GetBySpec(dateSpecification as ISpecification<Order>)
                                  .ToList();
        }

        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.Application.MainModule.SalesManagement.ISalesManagementService"/>
        /// </summary>
        /// <param name="order"><see cref="Microsoft.Samples.NLayerApp.Application.MainModule.SalesManagement.ISalesManagementService"/></param>
        public void ChangeOrder(Order order)
        {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            IUnitOfWork unitOfWork = _orderRepository.UnitOfWork as IUnitOfWork;


            _orderRepository.Modify(order);


            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();

        }

        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.Application.MainModule.SalesManagement.ISalesManagementService"/>
        /// </summary>
        /// <param name="order"><see cref="Microsoft.Samples.NLayerApp.Application.MainModule.SalesManagement.ISalesManagementService"/></param>
        public void AddOrder(Order order)
        {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            IUnitOfWork unitOfWork = _orderRepository.UnitOfWork as IUnitOfWork;


            _orderRepository.Add(order);


            //Complete changes in this unit of work
            unitOfWork.Commit();

        }

        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.Application.MainModule.SalesManagement.ISalesManagementService"/>
        /// </summary>
        /// <param name="customerId"><see cref="Microsoft.Samples.NLayerApp.Application.MainModule.SalesManagement.ISalesManagementService"/></param>
        /// <param name="fromDate"><see cref="Microsoft.Samples.NLayerApp.Application.MainModule.SalesManagement.ISalesManagementService"/></param>
        /// <param name="toDate"><see cref="Microsoft.Samples.NLayerApp.Application.MainModule.SalesManagement.ISalesManagementService"/></param>
        /// <returns><see cref="Microsoft.Samples.NLayerApp.Application.MainModule.SalesManagement.ISalesManagementService"/></returns>
        public List<Order> FindOrdersByOrderInformation(int customerId, DateTime? fromDate, DateTime? toDate)
        {
            OrderFromCustomerDateRangeSpecification specification = new OrderFromCustomerDateRangeSpecification(customerId, fromDate, toDate);

            return _orderRepository.GetBySpec(specification as ISpecification<Order>).ToList();
        }

        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.Application.MainModule.SalesManagement.ISalesManagementService"/>
        /// </summary>
        /// <param name="pageIndex"><see cref="Microsoft.Samples.NLayerApp.Application.MainModule.SalesManagement.ISalesManagementService"/></param>
        /// <param name="pageCount"><see cref="Microsoft.Samples.NLayerApp.Application.MainModule.SalesManagement.ISalesManagementService"/></param>
        /// <returns><see cref="Microsoft.Samples.NLayerApp.Application.MainModule.SalesManagement.ISalesManagementService"/></returns>
        public List<Product> FindPagedProducts(int pageIndex, int pageCount)
        {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");

            //query repository
            return _productRepository.GetPagedElements(pageIndex, pageCount, p => p.ProductId, true)
                                    .ToList();
        }
        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.Application.MainModule.SalesManagement.ISalesManagementService"/>
        /// </summary>
        /// <param name="publisherName"><see cref="Microsoft.Samples.NLayerApp.Application.MainModule.SalesManagement.ISalesManagementService"/></param>
        /// <param name="description"><see cref="Microsoft.Samples.NLayerApp.Application.MainModule.SalesManagement.ISalesManagementService"/></param>
        /// <returns><see cref="Microsoft.Samples.NLayerApp.Application.MainModule.SalesManagement.ISalesManagementService"/></returns>
        public List<Product> FindProductBySpecification(string publisherName, string description)
        {
            //Create specification
            ProductInformationSpecification specification = new ProductInformationSpecification(publisherName, description);

            //query repository
            return _productRepository.GetBySpec(specification as ISpecification<Product>)
                                    .ToList();
        }

        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.Application.MainModule.SalesManagement.ISalesManagementService"/>
        /// </summary>
        /// <param name="product"><see cref="Microsoft.Samples.NLayerApp.Application.MainModule.SalesManagement.ISalesManagementService"/></param>
        public void ChangeProduct(Product product)
        {


            IUnitOfWork unitOfWork = _productRepository.UnitOfWork as IUnitOfWork;


            _productRepository.Modify(product);

            //complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();

        }

        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.Application.MainModule.SalesManagement.ISalesManagementService"/>
        /// </summary>
        /// <param name="product"><see cref="Microsoft.Samples.NLayerApp.Application.MainModule.SalesManagement.ISalesManagementService"/></param>
        public void AddProduct(Product product)
        {


            IUnitOfWork unitOfWork = _productRepository.UnitOfWork as IUnitOfWork;


            _productRepository.Add(product);

            //complete changes in this unit of work
            unitOfWork.Commit();

        }

        #endregion
    }
}
