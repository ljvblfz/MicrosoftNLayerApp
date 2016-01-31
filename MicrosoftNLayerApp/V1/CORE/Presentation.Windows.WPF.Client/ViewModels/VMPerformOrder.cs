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
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
using System.Windows;
using System.Windows.Input;

using Microsoft.Samples.NLayerApp.Domain.Core.Entities;
using Microsoft.Samples.NLayerApp.Domain.MainModule.Entities;
using Microsoft.Samples.NLayerApp.Presentation.Windows.WPF.Client.ViewModelBase;
using Microsoft.Samples.NLayerApp.Presentation.Windows.WPF.ServiceAgents.Proxies.MainModule;
using Microsoft.Samples.NLayerApp.Presentation.Windows.WPF.ServiceAgents;


namespace Microsoft.Samples.NLayerApp.Presentation.Windows.WPF.Client.ViewModels
{
    /// <summary>
    /// ViewModel for perform order
    /// </summary>
    public class VMPerformOrder : ObservableObject
    {
        #region Members

        ICommand _closeCommand;
        ICommand _cancelCommand;
        ICommand _saveCommand;
        Order _currentOrder;
        string _contactName;

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
                    _saveCommand = new DelegateCommand<bool>(SaveExecute, CanSaveExecute);
                }
                return _saveCommand;
            }
        }

        #endregion

        #region Constructors

        public VMPerformOrder()
        {
            //Initialize proxy
            IMainModuleService mainModuleService = ProxyLocator.GetMainModuleService();
            mainModuleService = ProxyLocator.GetMainModuleService();

            this._currentOrder = new Order();
        }

        #endregion

        #region Command Methods

        private void CloseExecute()
        {
            NavigationController.CloseCurrentView.Execute(null);
        }

        private void CancelExecute()
        {
            NavigationController.CloseCurrentView.Execute(null);
        }

        private void SaveExecute(bool isValidData)
        {
            try
            {
                this.Order.Customer = this.GetCustomerByName();

                if (this.Order.Customer != null)
                {
                    this.Order.MarkAsAdded<Order>();


                    IMainModuleService mainModuleService = ProxyLocator.GetMainModuleService();
                    mainModuleService.AddOrder(this.Order);

                    this.Order.AcceptChanges();

                    NavigationController.CloseCurrentView.Execute(null);
                }
            }
            catch (FaultException<ServiceError> ex)
            {
                MessageBox.Show(ex.Detail.ErrorMessage);
            }
        }

        private bool CanSaveExecute(bool isValidData)
        {
            return isValidData;
        }

        private Customer GetCustomerByName()
        {
            Customer customer = null;

            if ( !string.IsNullOrEmpty(this.ContactName.Trim()))
            {

                IMainModuleService mainModuleService = ProxyLocator.GetMainModuleService();
                List<Customer> customers = mainModuleService.GetPagedCustomer(new PagedCriteria() { PageIndex = 0, PageCount = 100 });

                if (customers != null)
                {
                    customer = (from c in customers where c.ContactName.Equals(this.ContactName.Trim(), StringComparison.InvariantCultureIgnoreCase) select c).FirstOrDefault<Customer>();
                }
                else
                    this.ContactName = string.Empty;
            }

            return customer;           
        }

        #endregion
    }
}
