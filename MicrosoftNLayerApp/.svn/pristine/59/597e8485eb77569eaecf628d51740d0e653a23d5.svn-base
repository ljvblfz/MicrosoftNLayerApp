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
using System.ServiceModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

using Microsoft.Samples.NLayerApp.Domain.MainModule.Entities;
using Microsoft.Samples.NLayerApp.Presentation.Windows.WPF.Client.ViewModelBase;
using Microsoft.Samples.NLayerApp.Presentation.Windows.WPF.ServiceAgents.Proxies.MainModule;
using Microsoft.Samples.NLayerApp.Presentation.Windows.WPF.ServiceAgents;

namespace Microsoft.Samples.NLayerApp.Presentation.Windows.WPF.Client.ViewModels
{
    /// <summary>
    /// ViewModel for Customer List
    /// </summary>
    public class VMCustomerList : ObservableObject
    {
        #region Members

        ICommand _filterCommand;
        ICommand _viewCommand;
        ICommand _editCommand;
        ICommand _deleteCommand;
        ICommand _addCustomerCommand;
        ICommand _nextPageCommand;
        ICommand _previousPageCommand;
        string _filter;
        ObservableCollection<Customer> _customers;
        ICollectionView _viewData;
        int _pageIndex = 0;
        

        #endregion

        #region Properties

        public string Filter
        {
            get { return _filter; }
            set
            {
                _filter = value;
                RaisePropertyChanged("Filter");
            }
        }

        public ObservableCollection<Customer> Customers
        {
            get { return _customers; }
            set
            {
                _customers = value;
                RaisePropertyChanged("Customers");
            }
        }

        #endregion

        #region Command Properties

        public ICommand FilterCommand
        {
            get
            {
                if (_filterCommand == null)
                {
                    _filterCommand = new DelegateCommand(FilterExecute);
                }
                return _filterCommand;
            }
        }

        public ICommand ViewCommand
        {
            get
            {
                if (_viewCommand == null)
                {
                    _viewCommand = new DelegateCommand<Customer>(ViewExecute, CanViewExecute);
                }
                return _viewCommand;
            }
        }

        public ICommand EditCommand
        {
            get
            {
                if (_editCommand == null)
                {
                    _editCommand = new DelegateCommand<Customer>(EditExecute, CanEditExecute);
                }
                return _editCommand;
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                {
                    _deleteCommand = new DelegateCommand<Customer>(DeleteExecute, CanDeleteExecute);
                }
                return _deleteCommand;
            }
        }

        public ICommand AddCustomerCommand
        {
            get
            {
                if (_addCustomerCommand == null)
                {
                    _addCustomerCommand = new DelegateCommand(AddCustomerExecute);
                }
                return _addCustomerCommand;
            }
        }

        public ICommand NextPageCommand
        {
            get
            {
                if (_nextPageCommand == null)
                {
                    _nextPageCommand = new DelegateCommand(NextPageExecute);
                }
                return _nextPageCommand;
            }
        }

        public ICommand PreviousPageCommand
        {
            get
            {
                if (_previousPageCommand == null)
                {
                    _previousPageCommand = new DelegateCommand(PreviousPageExecute);
                }
                return _previousPageCommand;
            }
        }

        #endregion

        #region Constructors

        public VMCustomerList()
        {
            if (!NavigationController.IsInDesignMode)
            {
                this.GetCustomers();
            }

            
        }

        #endregion

        #region Command Methods

        private void FilterExecute()
        {
            if (this._viewData != null )
                this._viewData.Filter = new Predicate<object>(FilterCustomersCallback);
        }

        private void ViewExecute(Customer customer)
        {
            NavigationController.NavigateToView(new CustomerView(), new VMCustomer(customer));
        }

        private bool CanViewExecute(Customer customer)
        {
            return (customer != null);
        }

        private void EditExecute(Customer customer)
        {
            NavigationController.NavigateToView(new EditCustomerView(), new VMEditCustomer(customer));
        }

        private bool CanEditExecute(Customer customer)
        {
            return (customer != null);
        }

        private void DeleteExecute(Customer customer)
        {
            try
            {
                //remove customer
                IMainModuleService mainModuleService = ProxyLocator.GetMainModuleService();
                mainModuleService.RemoveCustomer(customer);

                //refresh customer list
                this.GetCustomers();
            }
            catch (FaultException<ServiceError> ex)
            {
                MessageBox.Show(ex.Detail.ErrorMessage);
            }
        }

        private bool CanDeleteExecute(Customer customer)
        {
            return (customer != null);
        }

        private void AddCustomerExecute()
        {
            NavigationController.NavigateToView(new AddCustomerView());
        }

        private void NextPageExecute()
        {
            this._pageIndex++;
            this.GetCustomers();
        }

        private void PreviousPageExecute()
        {
            this._pageIndex--;
            if (this._pageIndex < 0) this._pageIndex = 0;
            this.GetCustomers();
        }

        #endregion

        #region Private Methods

        private bool FilterCustomersCallback(object item)
        {
            if (item != null && !string.IsNullOrEmpty(this.Filter))
            {
                Customer customer = item as Customer;
                
                if (customer.ContactName.IndexOf(this.Filter, StringComparison.InvariantCultureIgnoreCase) > -1) return true;
                if (customer.CompanyName.IndexOf(this.Filter, StringComparison.InvariantCultureIgnoreCase) > -1) return true;

                return false;
            }

            return true;
        }
        
        private void GetCustomers()
        {
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                worker.DoWork += delegate(object sender, DoWorkEventArgs e)
                {
                    IMainModuleService mainModuleService = ProxyLocator.GetMainModuleService();
                    e.Result = mainModuleService.GetPagedCustomer(new PagedCriteria() { PageIndex = this._pageIndex, PageCount = 10 });
                };

                worker.RunWorkerCompleted += delegate(object sender, RunWorkerCompletedEventArgs e)
                {
                    if (!e.Cancelled && e.Error == null)
                    {
                        List<Customer> customers = e.Result as List<Customer>;

                        if (customers != null)
                        {
                            this.Customers = new ObservableCollection<Customer>(customers);
                            this._viewData = CollectionViewSource.GetDefaultView(this.Customers);
                            this._viewData.Filter = null;
                        }
                    }
                    else
                        MessageBox.Show(e.Error.Message, "Customer List", MessageBoxButton.OK, MessageBoxImage.Error);
                };

                worker.WorkerSupportsCancellation = true;
                worker.RunWorkerAsync();
            }
        }

        #endregion
    }
}
