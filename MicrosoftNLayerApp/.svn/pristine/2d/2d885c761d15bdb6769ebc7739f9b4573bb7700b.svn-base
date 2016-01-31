using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Samples.NLayerApp.Application.MainModule.SalesManagement;
using Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.IoC;
using Microsoft.Samples.NLayerApp.Domain.MainModule.Entities;

namespace Application.MainModule.Tests
{
    [TestClass()]
    [DeploymentItem("Microsoft.Samples.NLayerApp.Infrastructure.Data.MainModule.Mock.dll")]
    [DeploymentItem("Microsoft.Samples.NLayerApp.Infrastructure.Data.MainModule.dll")]
    public class SalesManagementServiceTests
    {
        [TestMethod()]
        public void FindPagedOrders_Invoke_Test()
        {
            //Arrange
            using (ISalesManagementService orderService = IoCFactory.Instance.CurrentContainer.Resolve<ISalesManagementService>())
            {

                //act
                List<Order> orders = orderService.FindPagedOrders(0, 1);

                //assert
                Assert.IsNotNull(orders);
                Assert.IsTrue(orders.Count == 1);
            }
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void FindPagedOrders_Invoke_NullPageIndexThrowArgumentException_Test()
        {
            //Arrange
            using (ISalesManagementService orderService = IoCFactory.Instance.CurrentContainer.Resolve<ISalesManagementService>())
            {
                //Act
                List<Order> orders = orderService.FindPagedOrders(-1, 1);
            }
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void FindPagedOrders_Invoke_NullPageCountThrowArgumentException_Test()
        {
            //Arrange
            using (ISalesManagementService orderService = IoCFactory.Instance.CurrentContainer.Resolve<ISalesManagementService>())
            {

                //Act
                List<Order> orders = orderService.FindPagedOrders(0, 0);
            }
        }
        [TestMethod()]
        public void FindOrdersByDates_Invoke_test()
        {
            //Arrange
            using (ISalesManagementService orderService = IoCFactory.Instance.CurrentContainer.Resolve<ISalesManagementService>())
            {
                DateTime initDate = new DateTime(2001, 1, 1);
                DateTime endDate = new DateTime(2010, 5, 1);

                //act
                List<Order> orders = orderService.FindOrdersByDates(initDate, endDate);

                //Assert
                Assert.IsNotNull(orders);
                Assert.IsTrue(orders.Count > 0);
            }
        }
        [TestMethod()]
        public void FindOrdersByShippingData_Invoke_test()
        {
            //Arrange
            using (ISalesManagementService orderService = IoCFactory.Instance.CurrentContainer.Resolve<ISalesManagementService>())
            {
                DateTime initDate = new DateTime(2010, 1, 1);
                DateTime endDate = new DateTime(2010, 5, 1);

                string shippingName = "Windows Seven buy";
                string shippingZip = "28081";
                string shippingCity = "Madrid";
                string shippingAddress = "Sebastian el Cano";

                //act
                List<Order> orders = orderService.FindOrdersByShippingData(shippingName, null, null, null);

                //Assert
                Assert.IsNotNull(orders);
                Assert.IsTrue(orders.Count == 1);

                //act
                orders = orderService.FindOrdersByShippingData(null, null, null, shippingZip);

                //Assert
                Assert.IsNotNull(orders);
                Assert.IsTrue(orders.Count >= 2);

                //act
                orders = orderService.FindOrdersByShippingData(null, null, shippingCity, null);

                //Assert
                Assert.IsNotNull(orders);
                Assert.IsTrue(orders.Count >= 1);

                //act
                orders = orderService.FindOrdersByShippingData(null, shippingAddress, null, null);

                //Assert
                Assert.IsNotNull(orders);
                Assert.IsTrue(orders.Count >= 1);

                //act
                orders = orderService.FindOrdersByShippingData(shippingName, shippingAddress, shippingCity, shippingZip);

                //Assert
                Assert.IsNotNull(orders);
                Assert.IsTrue(orders.Count == 1);
            }

        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddOrder_Invoke_NullItemThrowNewArgumentNullException_Test()
        {
            //Arrange
            using (ISalesManagementService orderService = IoCFactory.Instance.CurrentContainer.Resolve<ISalesManagementService>())
            {
                orderService.AddOrder(null);
            }
        }
        [TestMethod()]
        public void AddOrder_Invoke_Test()
        {
            //Arrange
            using (ISalesManagementService orderService = IoCFactory.Instance.CurrentContainer.Resolve<ISalesManagementService>())
            {
                string shippingZip = "9999";
                Order order = new Order()
                {
                    OrderDate = DateTime.Now,
                    DeliveryDate = DateTime.Now.AddDays(1),
                    ShippingAddress = "shipping address",
                    ShippingCity = "Shipping City",
                    ShippingName = "Shipping Name",
                    ShippingZip = shippingZip,
                    CustomerId = 1
                };

                //act
                orderService.AddOrder(order);
                List<Order> products = orderService.FindOrdersByShippingData(null, null, null, shippingZip);

                //assert
                Assert.IsNotNull(products);
                Assert.IsTrue(products.Count == 1);
            }

        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ChangeOrder_Invoke_NullItemThrowNullArgumentException_Test()
        {
            //Arrange
            using (ISalesManagementService orderService = IoCFactory.Instance.CurrentContainer.Resolve<ISalesManagementService>())
            {
                //Act
                orderService.ChangeOrder(null);
            }
        }

        [TestMethod()]
        public void FindOrdersByOrderInformation_Invoke_Test()
        {
            //Arrange
            using (ISalesManagementService orderService = IoCFactory.Instance.CurrentContainer.Resolve<ISalesManagementService>())
            {
                int customerId = 1;
                DateTime? from = new DateTime(2008, 12, 1);
                DateTime? to = new DateTime(2010, 1, 2);

                //Act
                List<Order> result = orderService.FindOrdersByOrderInformation(customerId, from, to);
                List<Order> result2 = orderService.FindOrdersByOrderInformation(customerId, null, null);

                //Assert
                Assert.IsNotNull(result);
                Assert.IsTrue(result.Count >= 1);

                Assert.IsNotNull(result2);
                Assert.IsTrue(result2.Count >= 1);
            }
        }

        [TestMethod()]
        public void ChangeOrder_Invoke_Test()
        {
            //Arrange
            using (ISalesManagementService orderService = IoCFactory.Instance.CurrentContainer.Resolve<ISalesManagementService>())
            {
                string shippingName = "Book";
                string newShippingCity = "Pozuelo";
                //Act
                List<Order> orders = orderService.FindOrdersByShippingData(shippingName, null, null, null);
                Order order = orders.First();
                order.ShippingCity = newShippingCity;
                orderService.ChangeOrder(order);
                orders = orderService.FindOrdersByShippingData(shippingName, null, null, null);

                //Assert
                Assert.IsNotNull(orders);
                Assert.IsTrue(orders.Count > 0);
                Assert.IsTrue(orders.First().ShippingCity == newShippingCity);
            }

        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddProduct_Invoke_NullItemThrowNewArgumentNullException_Test()
        {
            //Arrange
            using (ISalesManagementService productService = IoCFactory.Instance.CurrentContainer.Resolve<ISalesManagementService>())
            {
                //Act
                productService.AddProduct(null);
            }
        }
        [TestMethod()]
        public void AddProduct_Invoke_Test()
        {
            //Arrange
            using (ISalesManagementService productService = IoCFactory.Instance.CurrentContainer.Resolve<ISalesManagementService>())
            {
                Product product = new Product()
                {
                    AmountInStock = 10,
                    ProductDescription = "product description",
                    Publisher = "Microsoft Samples",
                    UnitAmount = "€",
                    UnitPrice = 100M
                };

                //Act
                productService.AddProduct(product);
                List<Product> products = productService.FindProductBySpecification("Microsoft Samples", null);

                //Assert
                Assert.IsNotNull(products);
                Assert.IsTrue(products.Count == 1);
            }
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ChangeProduct_Invoke_NullItemThrowArgumentNullException_Test()
        {
            //Arrange
            using (ISalesManagementService productService = IoCFactory.Instance.CurrentContainer.Resolve<ISalesManagementService>())
            {
                productService.ChangeProduct(null);
            }

        }
        [TestMethod()]
        public void ChangeProduct_Invoke_Test()
        {
            //Arrange
            using (ISalesManagementService productService = IoCFactory.Instance.CurrentContainer.Resolve<ISalesManagementService>())
            {
                string publisherName = "Krasis Press";
                //Act
                List<Product> products = productService.FindProductBySpecification(publisherName, null);
                Product product = products.FirstOrDefault();

                product.UnitAmount = "$";

                productService.ChangeProduct(product);
                products = productService.FindProductBySpecification(publisherName, null);
                product = products.FirstOrDefault();

                //Assert
                Assert.IsNotNull(product);
                Assert.IsTrue(product.UnitAmount == "$");
            }

        }
        [TestMethod()]
        public void FindPagedProducts_Invoke_Tests()
        {
            //Arrange
            using (ISalesManagementService productService = IoCFactory.Instance.CurrentContainer.Resolve<ISalesManagementService>())
            {
                int pageIndex = 0;
                int pageCount = 1;

                //Act
                List<Product> products = productService.FindPagedProducts(pageIndex, pageCount);

                //Assert
                Assert.IsNotNull(products);
                Assert.IsTrue(products.Count == 1);
            }
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void FindPagedProducts_Invoke_NullPageIndexThrowArgumentException_Test()
        {
            //Arrange
            using (ISalesManagementService productService = IoCFactory.Instance.CurrentContainer.Resolve<ISalesManagementService>())
            {
                //Act
                List<Product> products = productService.FindPagedProducts(-1, 1);
            }
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void FindPagedProducts_Invoke_NullPageCountThrowArgumentException_Test()
        {
            //Arrange
            using (ISalesManagementService productService = IoCFactory.Instance.CurrentContainer.Resolve<ISalesManagementService>())
            {

                //Act
                List<Product> products = productService.FindPagedProducts(0, 0);
            }
        }
        [TestMethod()]
        public void FindProductBySpecification_Invoke_Test()
        {
            //Arrange
            using (ISalesManagementService productService = IoCFactory.Instance.CurrentContainer.Resolve<ISalesManagementService>())
            {
                
                string publisherName = "Krasis Press";
                string description = "Windows";

                //Act
                List<Product> products = productService.FindProductBySpecification(publisherName, null);

                //Assert
                Assert.IsNotNull(products);
                Assert.IsTrue(products.Count == 1);

                //Act
                products = productService.FindProductBySpecification(null, description);

                //Assert
                Assert.IsNotNull(products);
                Assert.IsTrue(products.Count == 1);
            }
        }
    }
}
