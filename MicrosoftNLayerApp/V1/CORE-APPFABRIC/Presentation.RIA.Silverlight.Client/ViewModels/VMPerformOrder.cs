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
//====================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Windows;
using System.Windows.Input;

using Microsoft.Samples.NLayerApp.Domain.Core.Entities;
using Microsoft.Samples.NLayerApp.Domain.MainModule.Entities;
using Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client.ServiceAgent;

namespace Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client.ViewModels
{
    /// <summary>
    /// ViewModel perform order
    /// </summary>
    public class VMPerformOrder : ObservableObject
    {
        #region Declarations

        private ICommand _closeCommand;
        private ICommand _cancelCommand;
        private ICommand _saveCommand;
        private Order _currentOrder;
        private string _contactName;

        #endregion

        #region Properties

        public Order Order
        {
            get { return _currentOrder; }
            set
            {
                _currentOrder = value;
                RaisePropertyChanged("Order");
            }
        }

        public string ContactName
        {
            get { return _contactName; }
            set
            {
                _contactName = value;
                RaisePropertyChanged("ContactName");
            }
        }

        public string ShippingName
        {
            get { return _currentOrder.ShippingName; }
            set
            {
                _currentOrder.ShippingName = value;
                RaisePropertyChanged("ShippingName");
            }
        }

        public DateTime? DeliveryDate
        {
            get { return _currentOrder.DeliveryDate; }
            set
            {
                _currentOrder.DeliveryDate = value;
                RaisePropertyChanged("DeliveryDate");
            }
        }

        public DateTime? OrderDate
        {
            get { return _currentOrder.OrderDate; }
            set
            {
                _currentOrder.OrderDate = value;
                RaisePropertyChanged("OrderDate");
            }
        }

        public string ShippingCity
        {
            get { return _currentOrder.ShippingCity; }
            set
            {
                _currentOrder.ShippingCity = value;
                RaisePropertyChanged("ShippingCity");
            }
        }

        public string ShippingAddress
        {
            get { return _currentOrder.ShippingAddress; }
            set
            {
                _currentOrder.ShippingAddress = value;
                RaisePropertyChanged("ShippingAddress");
            }
        }

        public string ShippingZip
        {
            get { return _currentOrder.ShippingZip; }
            set
            {
                _currentOrder.ShippingZip = value;
                RaisePropertyChanged("ShippingZip");
            }
        }
        #endregion

        #region Command Properties

        public ICommand CloseCommand
        {
            get
            {
                if (_closeCommand == null)
                {
                    _closeCommand = new DelegateCommand(CloseExecute);
                }
                return _closeCommand;
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                {
                    _cancelCommand = new DelegateCommand(CancelExecute);
                }
                return _cancelCommand;
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new DelegateCommand(SaveExecute);
                }
                return _saveCommand;
            }
        }

        #endregion

        #region Constructors

        public VMPerformOrder()
        {
            this._currentOrder = new Order();
        }

        #endregion

        #region Command Methods

        private void CloseExecute()
        {
            //NavigationController.CloseCurrentView.Execute(null);
        }

        private void CancelExecute()
        {
            //NavigationController.CloseCurrentView.Execute(null);
        }

        private void SaveExecute()
        {
            try
            {
                Customer customer = null;
                if (ContactName != null)
                    if (!string.IsNullOrEmpty(this.ContactName.Trim()))
                    {
                        MainModuleServiceClient client = new MainModuleServiceClient();

                        client.GetPagedCustomerAsync(new PagedCriteria() { PageIndex = 0, PageCount = 100 });

                        client.GetPagedCustomerCompleted += delegate(object s, GetPagedCustomerCompletedEventArgs e)
                        {

                            List<Customer> customers = new List<Customer>();
                            foreach (var item in e.Result)
                            {
                                customers.Add(item);
                            }

                            if (customers != null)
                            {
                                Order.Customer = (from c in customers where c.ContactName.Equals(this.ContactName.Trim(), StringComparison.InvariantCultureIgnoreCase) select c).FirstOrDefault<Customer>();
                            }
                            else
                                this.ContactName = string.Empty;

                            if (this.Order.Customer != null)
                            {
                                this.Order.ChangeTracker.State = ObjectState.Added;

                                client.AddOrderAsync(this.Order);
                                this._currentOrder = new Order();
                                this.Order = new Order();
                            }
                        };

                    }

               
            }
            catch (FaultException<ServiceError> ex)
            {
                MessageBox.Show(ex.Detail.ErrorMessage);
            }
        }

        private bool CanSaveExecute()
        {
            return true;
        }

        private Customer GetCustomerByName()
        {

            Customer customer = null;
            if (ContactName != null)
                if (!string.IsNullOrEmpty(this.ContactName.Trim()))
                {
                    MainModuleServiceClient client = new MainModuleServiceClient();

                    client.GetPagedCustomerAsync(new PagedCriteria() { PageIndex = 0, PageCount = 100 });

                    client.GetPagedCustomerCompleted += delegate(object s, GetPagedCustomerCompletedEventArgs e)
                    {

                        List<Customer> customers = new List<Customer>();
                        foreach (var item in e.Result)
                        {
                            customers.Add(item);
                        }

                        if (customers != null)
                        {
                            customer = (from c in customers where c.ContactName.Equals(this.ContactName.Trim(), StringComparison.InvariantCultureIgnoreCase) select c).FirstOrDefault<Customer>();
                        }
                        else
                            this.ContactName = string.Empty;
                    };

                }
            return customer;
        }

        #endregion
    }
}
