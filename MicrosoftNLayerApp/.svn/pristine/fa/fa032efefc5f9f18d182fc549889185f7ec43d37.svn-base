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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;

using Microsoft.Samples.NLayerApp.Domain.MainModule.Entities;
using Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client.ServiceAgent;

namespace Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client.ViewModels
{
    /// <summary>
    /// ViewModel Customer
    /// </summary>
    public class VMCustomer : Silverlight.Client.ObservableObject
    {
        #region Declarations
        private Customer currentCustomer;
        private ObservableCollection<Order> currentCustomerOrders;
        #endregion

        #region Properties

        public Customer Customer
        {
            get { return currentCustomer; }
            set
            {
                currentCustomer = value;
                RaisePropertyChanged("Customer");
            }
        }

        public ObservableCollection<Order> CustomerOrders
        {
            get { return currentCustomerOrders; }
            set
            {
                currentCustomerOrders = value;
                RaisePropertyChanged("CustomerOrders");
            }
        }

        public DateTime Today
        {
            get { return DateTime.Today; }
        }

        #endregion

        #region Constructors
        public VMCustomer()
        {

            editCommand = new DelegateCommand<object>(EditExecute);
            backCommand = new DelegateCommand<object>(BackExecute);
            currentCustomerOrders = new ObservableCollection<Order>();

            if (!IsDesignTime)
            {
                GetFirstCustomer();
            }

        }

        public VMCustomer(Customer current)
        {

            editCommand = new DelegateCommand<object>(EditExecute);
            backCommand = new DelegateCommand<object>(BackExecute);
            currentCustomerOrders = new ObservableCollection<Order>();

            if (!IsDesignTime)
            {
                Customer = current;
                GetCustomerOrders();
            }

        }

        public bool IsDesignTime
        {
            get
            {
                if (Application.Current.RootVisual != null)
                    return DesignerProperties.GetIsInDesignMode(Application.Current.RootVisual);
                return false;
            }
        }

        public void GetFirstCustomer()
        {
            try
            {
                MainModuleServiceClient client = new MainModuleServiceClient();

                client.GetPagedCustomerAsync(new PagedCriteria() { PageIndex = 0, PageCount = 10 });

                client.GetPagedCustomerCompleted += delegate(object sender, GetPagedCustomerCompletedEventArgs e)
                {
                    Customer[] listCustomers = e.Result;
                    if (listCustomers != null && listCustomers.Length > 0)
                    {
                        Customer = listCustomers[0];
                        GetCustomerOrders();
                    }
                };
            }
            catch (Exception excep)
            {
                Debug.WriteLine("GetFirstCustomer: Error at Service:" + excep.ToString());
            }
        }


        private void GetCustomerOrders()
        {
            try
            {
                MainModuleServiceClient client = new MainModuleServiceClient();

                client.GetOrdersByOrderInformationAsync( new OrderInformation()
                                                        {
                                                            CustomerId =  Customer.CustomerId,
                                                            DateTimeFrom = DateTime.MinValue,
                                                            DateTimeTo =  DateTime.Now
                                                        });

                client.GetOrdersByOrderInformationCompleted += delegate(object sender, GetOrdersByOrderInformationCompletedEventArgs e)
                {
                    CustomerOrders.Clear();

                    e.Result.ToList().ForEach(c => CustomerOrders.Add(c));

                };
            }
            catch (Exception excep)
            {
                Debug.WriteLine("GetCustomerOrders: Error at Service:" + excep.ToString());
            }

        }



        #endregion

        #region Commands
        private ICommand editCommand;
        public ICommand EditCommand
        {
            get { return editCommand; }
            set { editCommand = value; }
        }
        private void EditExecute(Object o)
        {
            if (o is Customer)
            {
                Customer current = (Customer)o;
                ((MainPage)App.Current.RootVisual).editCustomer.DataContext = new ViewModels.VMEditCustomer(current.CustomerCode);
                ((MainPage)App.Current.RootVisual).GotoEditCustomer.Begin();
            } 
            Debug.WriteLine("Edit Execute.");
        }

        private ICommand backCommand;
        public ICommand BackCommand
        {
            get { return backCommand; }
            set { backCommand = value; }
        }
        private void BackExecute(Object o)
        {
            ((MainPage)App.Current.RootVisual).ViewCustomerList.Begin();
        }

        #endregion

    }
}
