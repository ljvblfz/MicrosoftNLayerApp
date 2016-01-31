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
using System.Collections.Generic;
using System.Windows.Input;
using System.ComponentModel;
using System.Linq;
using System;
using System.Globalization;

using Microsoft.Samples.NLayerApp.Presentation.Windows.WPF.Client.ViewModelBase;
using Microsoft.Samples.NLayerApp.Domain.MainModule.Entities;
using Microsoft.Samples.NLayerApp.Presentation.Windows.WPF.ServiceAgents.Proxies.MainModule;
using Microsoft.Samples.NLayerApp.Presentation.Windows.WPF.ServiceAgents;

namespace Microsoft.Samples.NLayerApp.Presentation.Windows.WPF.Client.ViewModels
{
    /// <summary>
    /// ViewModel for Customer 
    /// </summary>
    public class VMCustomer : ObservableObject
    {
        #region Members

        ICommand _closeCommand;
        ICommand _editCommand;
        ICommand _sendCommand;
        ICommand _transferCommand;
        Customer _currentCustomer;
        List<Order> _currentCustomerOrders;
        List<object> _currentCustomerOrdersPie;
        

        #endregion

        #region Properties
        
        public Customer Customer
        {
            get { return _currentCustomer; }
            set
            {
                _currentCustomer = value;
                RaisePropertyChanged("Customer");
            }
        }

        public byte[] Photo
        {
            get
            {
                return _currentCustomer.CustomerPicture.Photo;
            }
            set
            {
                _currentCustomer.CustomerPicture.Photo = value;
                RaisePropertyChanged("Photo");
            }
        }

        public List<Order> CustomerOrders
        {
            get { return _currentCustomerOrders; }
            set
            {
                _currentCustomerOrders = value;
                RaisePropertyChanged("CustomerOrders");
            }
        }

        public List<object> CurrentCustomerOrdersPie
        {
            get { return _currentCustomerOrdersPie; }
            set
            {
                _currentCustomerOrdersPie = value;
                RaisePropertyChanged("CurrentCustomerOrdersPie");
            }
        }

        public DateTime Today
        {
            get { return DateTime.Today; }
        }

        #endregion

        #region Command Properties

        public ICommand TransferCommand
        {
            get
            {
                if (_transferCommand == null)
                {
                    _transferCommand = new DelegateCommand(TransferExecute);
                }
                return _transferCommand;
            }
        }

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

        public ICommand EditCommand
        {
            get
            {
                if (_editCommand == null)
                {
                    _editCommand = new DelegateCommand(EditExecute);
                }
                return _editCommand;
            }
        }

        public ICommand SendCommand
        {
            get
            {
                if (_sendCommand == null)
                {
                    _sendCommand = new DelegateCommand(SendExecute);
                }
                return _sendCommand;
            }
        }

        #endregion

        #region Constructors

        public VMCustomer()
        {
            //create default data
            this._currentCustomer = new Customer()
            {
                CustomerPicture = new CustomerPicture() { }
            };

            
        }

        public VMCustomer(Customer customer)
        {
            if (customer != null)
            {
                //initialize main module service
                IMainModuleService mainModuleService = ProxyLocator.GetMainModuleService();

                this.Customer = mainModuleService.GetCustomerByCode(customer.CustomerCode);

                //load orders for this client
                using (BackgroundWorker worker = new BackgroundWorker())
                {
                    worker.DoWork += delegate(object sender, DoWorkEventArgs e)
                    {
                        try
                        {
                            e.Result = mainModuleService.GetOrdersByOrderInformation(new OrderInformation()
                                                                                    {
                                                                                        CustomerId = customer.CustomerId,
                                                                                        DateTimeFrom = DateTime.MinValue,
                                                                                        DateTimeTo  = DateTime.Now
                                                                                    });
                        }
                        catch
                        {
                            e.Cancel = true;
                        }
                    }; 
                    worker.RunWorkerCompleted += delegate(object sender, RunWorkerCompletedEventArgs e)
                    {
                        if (!e.Cancelled && e.Error == null)
                        {
                            List<Order> orders = e.Result as List<Order>;

                            if (orders != null)
                            {
                                DateTimeFormatInfo dtformatInfo = new DateTimeFormatInfo();

                                this.CustomerOrders = orders;
                                this.CurrentCustomerOrdersPie = (from co in this.CustomerOrders 
                                                                 where co.OrderDate.Value.Year == this.Today.Year 
                                                                 group co by co.OrderDate.Value.Month 
                                                                 into g 
                                                                 select new { id = g.Key, Month = dtformatInfo.GetMonthName(g.Key), Count = g.Count() }
                                                                 ).OrderByDescending(o => o.id)
                                                                 .ToList<object>();
                            }
                        }
                    };

                    worker.WorkerSupportsCancellation = true;
                    worker.RunWorkerAsync();
                }
            }
        }

        #endregion

        #region Command Methods

        private void TransferExecute()
        {
            NavigationController.NavigateToView(new PerformTransferView());
        }

        private void CloseExecute()
        {
            NavigationController.NavigateToView(new CustomerListView());
        }

        private void EditExecute()
        {
            NavigationController.NavigateToView(new EditCustomerView(), new VMEditCustomer(this._currentCustomer));
        }

        private void SendExecute()
        {
            // TODO Send Customer Profile    
        }

        #endregion
    }
}
