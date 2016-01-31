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
using System.Data.Objects;

using Microsoft.Moles.Framework;
using Microsoft.Moles.Framework.Behaviors;

using Microsoft.Samples.NLayerApp.Infrastructure.Data.Core;
using Microsoft.Samples.NLayerApp.Infrastructure.Data.Core.Extensions;
using Entities = Microsoft.Samples.NLayerApp.Domain.MainModule.Entities;



namespace Microsoft.Samples.NLayerApp.Infrastructure.Data.MainModule.Mock
{
    /// <summary>
    /// Fake container for testing purposes.
    /// Implemented with Microsoft Moles and Stubs
    /// </summary>
    public class FakeMainModuleUnitOfWork
        : Microsoft.Samples.NLayerApp.Infrastructure.Data.MainModule.UnitOfWork.Moles.SIMainModuleUnitOfWork
    {
        #region Members

        static List<Entities.BankAccount> _BankAccounts;
        static List<Entities.BankTransfer> _BankTransfer;
        static List<Entities.Country> _Countries;
        static List<Entities.Product> _Products;
        static List<Entities.Order> _Orders;
        static List<Entities.Customer> _Customers;
        static List<Entities.OrderDetail> _OrdersDetails;

        #endregion

        #region Constructor

        public FakeMainModuleUnitOfWork()
        {
            //Set default behavior for not stubbed elements ( DEFAULT BEHAVIOR THROW NOT IMPLEMENTED EXCEPTION)
            this.InstanceBehavior = BehavedBehaviors.DefaultValue;

            InitiateInnerCollection();
            InitiateFakeData();
        }

        #endregion

        #region Methods for configure Fake for testing

        private void InitiateInnerCollection()
        {
            if (_Countries == null)
            {
                _Countries = new List<Entities.Country>()
                {
                    new Entities.Country(){CountryId = 1,CountryName="Spain"},
                    new Entities.Country(){CountryId = 2,CountryName="United States"},
                    new Entities.Country(){CountryId = 3,CountryName="United Kingdom"},
                    new Entities.Country(){CountryId = 4,CountryName="Germany"},
                    new Entities.Country(){CountryId = 5,CountryName="France"},
                    new Entities.Country(){CountryId = 6,CountryName="Sweeden"},
                    new Entities.Country(){CountryId = 7,CountryName="Norway"},
                    new Entities.Country(){CountryId = 8,CountryName="Portugal"},
                    new Entities.Country(){CountryId = 9,CountryName="Italy"},
                    new Entities.Country(){CountryId = 10,CountryName="Holland"},
                    new Entities.Country(){CountryId = 11,CountryName="Belgium"},
                    new Entities.Country(){CountryId = 12,CountryName="Brazil"},
                    new Entities.Country(){CountryId = 13,CountryName="Argentina"},
                    new Entities.Country(){CountryId = 14,CountryName="Russia"},
                    new Entities.Country(){CountryId = 15,CountryName="China"}
                };
            }
            if (_Customers == null)
            {
                _Customers = new List<Entities.Customer>()
                {
                    new Entities.Customer(){
                                CustomerId = 1,
                                CustomerCode = "A0001",
                                ContactTitle = "DPE Architect Evangelist",
                                CompanyName = "Microsoft",
                                ContactName = "Cesar de la Torre",
                                Address = new Entities.AddressInformation()
                                {
                                    Telephone  ="+34981666666",
                                    PostalCode = "28081",
                                    Fax = "+34981666666",
                                    City = "Madrid",
                                    Address  = "Parque empresaria deLa Finca"
                                },
                                CountryId = 1,
                                Country = _Countries[0],
                                IsEnabled = true,
                                CustomerPicture = new Entities.CustomerPicture(){CustomerId=1,Photo=null}
                             },
                    new Entities.Customer(){
                                CustomerId = 2,
                                CustomerCode = "A0002",
                                ContactTitle = "Developer Team Leader",
                                CompanyName = "Plain Concepts",
                                ContactName = "Unai Zorrilla",
                                Address = new Entities.AddressInformation()
                                {
                                    Telephone  ="+34981555000",
                                    PostalCode = "28081",
                                    Fax = "+34981555001",
                                    City = "Madrid",
                                    Address  = "Sebastian el Cano"
                                },
                                CountryId = 2,
                                Country = _Countries[1],
                                IsEnabled = true,
                                CustomerPicture = new Entities.CustomerPicture(){CustomerId=2,Photo=null}
                             },
                      new Entities.Customer(){
                                CustomerId = 3,
                                CustomerCode = "A0003",
                                ContactTitle = "Developer Advisor",
                                CompanyName = "Plain Concepts",
                                ContactName = "Fernando Cortés",
                                Address = new Entities.AddressInformation()
                                {
                                    Telephone  ="+34981555000",
                                    PostalCode = "28081",
                                    Fax = "+34981555001",
                                    City = "Madrid",
                                    Address  = "Sebastian el Cano"
                                },
                                CountryId = 2,
                                Country = _Countries[1],
                                IsEnabled = false,
                                CustomerPicture = new Entities.CustomerPicture(){CustomerId=3,Photo=null}
                             },
                        new Entities.Customer(){
                                CustomerId = 4,
                                CustomerCode = "A0004",
                                ContactTitle = "ALM Team Lead",
                                CompanyName = "Plain Concepts",
                                ContactName = "Rodrigo Corral",
                                Address = new Entities.AddressInformation()
                                {
                                    Telephone  ="+34981555000",
                                    PostalCode = "28081",
                                    Fax = "+34981555001",
                                    City = "Madrid",
                                    Address  = "Sebastian el Cano"
                                },
                                CountryId = 2,
                                Country = _Countries[1],
                                IsEnabled = true,
                                CustomerPicture = new Entities.CustomerPicture(){CustomerId=4,Photo=null}
                             }
                };

                _Customers[0].Orders.Add(new Entities.Order() { OrderId = 1 });
            }
            if (_Products == null)
            {
                _Products = new List<Entities.Product>()
                {

                    new Entities.Software(){ 
                                ProductId = 2,
                                UnitPrice =200M,
                                UnitAmount="1",
                                ProductDescription="Windows Seven Operating System",
                                LicenseCode = "ABCX-0002-33333-66666",
                                AmountInStock = 20
                                },
                    new Entities.Book() {
                                    ProductId = 1,
                                    UnitPrice =40M,
                                    UnitAmount="1",
                                    ProductDescription="Book for ADO.NET Entity Framework",
                                    Publisher = "Krasis Press",
                                    AmountInStock = 20
                                }
                };
            }


            if (_BankAccounts == null)
            {
                _BankAccounts = new List<Entities.BankAccount>()
                {
                    new Entities.BankAccount(){BankAccountId = 1,BankAccountNumber="BAC0000001",CustomerId=1,Balance=100,Locked=false,Customer = _Customers[0]},
                    new Entities.BankAccount(){BankAccountId = 2,BankAccountNumber="BAC0000002",CustomerId=2,Balance=200,Locked=false,Customer = _Customers[1]},
                    new Entities.BankAccount(){BankAccountId = 3,BankAccountNumber="BAC0000003",CustomerId=2,Balance=200,Locked=true,Customer = _Customers[2]}
                };
            }
            if (_BankTransfer == null)
            {
                _BankTransfer = new List<Entities.BankTransfer>()
                {
                    new Entities.BankTransfer()
                    {
                        BankTransferId = 1,
                        Amount = 50,
                        FromBankAccountId = 1,
                        ToBankAccountId = 2,
                        TransferDate = new DateTime(2009,1,1)
                    },
                    new Entities.BankTransfer()
                    {
                        BankTransferId = 1,
                        Amount = 50,
                        FromBankAccountId = 2,
                        ToBankAccountId = 1,
                        TransferDate = new DateTime(2009,5,1)
                    },
                };
            }


            if (_Orders == null)
            {
                _Orders = new List<Entities.Order>()
                {
                        new Entities.Order(){
                                OrderId = 1,
                                DeliveryDate = new DateTime(2010,1,2),
                                ShippingAddress = "Sebastian el Cano",
                                ShippingCity  = "Madrid",
                                ShippingName = "Book EF buy",
                                ShippingZip = "28081",
                                OrderDate = new DateTime(2010,1,1),
                                Customer = _Customers[0]
                           },
                           new Entities.Order(){
                                OrderId = 2,
                                DeliveryDate = new DateTime(2010,5,6),
                                ShippingAddress = "Sebastian el Cano",
                                ShippingCity  = "Madrid",
                                ShippingName = "Windows Seven buy",
                                ShippingZip = "28081",
                                OrderDate = new DateTime(2010,5,1),
                                Customer = _Customers[1]
                           },
                           new Entities.Order(){
                                OrderId = 3,
                                DeliveryDate = new DateTime(2010,2,5),
                                ShippingAddress = "Sebastian el Cano",
                                ShippingCity  = "Madrid",
                                ShippingName = "Book EF buy",
                                ShippingZip = "28081",
                                OrderDate = new DateTime(2010,2,1),
                                Customer = _Customers[0]
                           },
                           new Entities.Order(){
                                OrderId = 4,
                                DeliveryDate = new DateTime(2010,1,5),
                                ShippingAddress = "Sebastian el Cano",
                                ShippingCity  = "Madrid",
                                ShippingName = "Book EF buy",
                                ShippingZip = "28081",
                                OrderDate = new DateTime(2010,1,1),
                                Customer = _Customers[0]
                           },
                           new Entities.Order(){
                                OrderId = 5,
                                DeliveryDate = new DateTime(2010,6,5),
                                ShippingAddress = "Sebastian el Cano",
                                ShippingCity  = "Madrid",
                                ShippingName = "Book EF buy",
                                ShippingZip = "28081",
                                OrderDate = new DateTime(2010,6,1),
                                Customer = _Customers[0]
                           },
                           new Entities.Order(){
                                OrderId = 6,
                                DeliveryDate = new DateTime(2010,4,5),
                                ShippingAddress = "Sebastian el Cano",
                                ShippingCity  = "Madrid",
                                ShippingName = "Book EF buy",
                                ShippingZip = "28081",
                                OrderDate = new DateTime(2010,4,1),
                                Customer = _Customers[0]
                           },
                };

            }

            if (_OrdersDetails == null)
            {
                _OrdersDetails = new List<Entities.OrderDetail>()
                {
                    new Entities.OrderDetail(){OrderDetailsId=1,UnitPrice = 40M,Amount=20,Discount=0f,ProductId=1,Product = _Products[0]},
                    new Entities.OrderDetail(){OrderDetailsId=2,UnitPrice = 200M,Amount=20,Discount=0f,ProductId=2,Product = _Products[1]}
                };
            }
        }

        private void InitiateFakeData()
        {
            //
            // CONFIGURE OBJECT SET FOR QUERIES
            //

            
            //configure country
            this.CountriesGet = () => CreateCountryObjectSet();
            this.CreateSet<Entities.Country>(() => CreateCountryObjectSet());

            //Configure Customers
            this.CustomersGet = () => CreateCustomerObjectSet();
            this.CreateSet<Entities.Customer>(() => CreateCustomerObjectSet());

            //configure Products
            this.ProductsGet = () => CreateProductObjectSet();
            this.CreateSet<Entities.Product>(() => CreateProductObjectSet());

            //configure bank account
            this.BankAccountsGet = () => CreateBankAccountObjectSet();
            this.CreateSet<Entities.BankAccount>(() => CreateBankAccountObjectSet());

            //configure Bank Transfers
            this.BankTransfersGet = () => CreateBankTransferObjectSet();
            this.CreateSet<Entities.BankTransfer>(() => CreateBankTransferObjectSet());

            //configure Orders
            this.OrdersGet = () => CreateOrderObjectSet();
            this.CreateSet<Entities.Order>(() => CreateOrderObjectSet());

            //configure Order details
            this.OrderDetailsGet = () => CreateOrdersDetailObjectSet();
            this.CreateSet<Entities.OrderDetail>(() => CreateOrdersDetailObjectSet());





            //
            //CONFIGURE APPLY CHANGES
            //
            this.RegisterChangesTEntity<Entities.BankAccount>(
                                                            ba
                                                            =>
                                                            {
                                                                if (ba != null
                                                                    &&
                                                                    _BankAccounts != null)
                                                                {
                                                                    int index = _BankAccounts.IndexOf(ba);
                                                                    if (index != -1)
                                                                        _BankAccounts[index] = ba;
                                                                    else
                                                                        _BankAccounts.Add(ba);
                                                                }
                                                            }
                                                            );
            this.RegisterChangesTEntity<Entities.Customer>(
                                                            c
                                                            =>
                                                            {
                                                                if (c != null
                                                                    &&
                                                                    _Customers != null)
                                                                {
                                                                    int index = _Customers.IndexOf(c);
                                                                    if (index != -1)
                                                                        _Customers[index] = c;
                                                                    else
                                                                        _Customers.Add(c);
                                                                }
                                                            }
                                                            );
            this.RegisterChangesTEntity<Entities.Product>(
                                                            p
                                                            =>
                                                            {
                                                                if (p != null
                                                                    &&
                                                                    _Products != null)
                                                                {
                                                                    int index = _Products.IndexOf(p);
                                                                    if (index != -1)
                                                                        _Products[index] = p;
                                                                    else
                                                                        _Products.Add(p);
                                                                }
                                                            }
                                                            );
            
            this.RegisterChangesTEntity<Entities.Order>(
                                                            o
                                                            =>
                                                            {
                                                                if (o != null
                                                                    &&
                                                                    _Orders != null)
                                                                {
                                                                    int index = _Orders.IndexOf(o);
                                                                    if (index != -1)
                                                                        _Orders[index] = o;
                                                                    else
                                                                        _Orders.Add(o);
                                                                }
                                                            }
                                                            );
           
            //Set Commit stub
            this.Commit = () => { };
            this.CommitAndRefreshChanges = () => { };




        }
        IObjectSet<Entities.BankAccount> CreateBankAccountObjectSet()
        {
            return _BankAccounts.ToMemorySet();
        }

        IObjectSet<Entities.BankTransfer> CreateBankTransferObjectSet()
        {
            return _BankTransfer.ToMemorySet<Entities.BankTransfer>();
        }

        IObjectSet<Entities.Country> CreateCountryObjectSet()
        {
            return _Countries.ToMemorySet();
        }

        IObjectSet<Entities.Product> CreateProductObjectSet()
        {
            return _Products.ToMemorySet();
        }

        IObjectSet<Entities.Order> CreateOrderObjectSet()
        {
            return _Orders.ToMemorySet();
        }

        IObjectSet<Entities.Customer> CreateCustomerObjectSet()
        {
            return _Customers.ToMemorySet();
        }

        IObjectSet<Entities.OrderDetail> CreateOrdersDetailObjectSet()
        {
            return _OrdersDetails.ToMemorySet();
        }

        #endregion
    }
}
