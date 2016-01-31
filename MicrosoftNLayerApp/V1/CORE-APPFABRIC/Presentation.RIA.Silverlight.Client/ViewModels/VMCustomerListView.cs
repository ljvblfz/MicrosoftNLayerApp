
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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Windows;
using System.Windows.Input;

using Microsoft.Samples.NLayerApp.Domain.MainModule.Entities;
using Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client.ServiceAgent;

namespace Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client.ViewModels
{
    /// <summary>
    /// ViewModel Customer List
    /// </summary>
    public class VMCustomerListView : ObservableObject
    {

        #region Declarations
        private Customer currentCustomer;
        private ObservableCollection<Customer> customers;
        private ICommand _nextPageCommand;
        private ICommand _previousPageCommand;
        private int _pageIndex = 0;
        #endregion

        #region Properties
        public Customer SelectedCustomer
        {
            get { return currentCustomer; }
            set
            {
                currentCustomer = value;
                RaisePropertyChanged("SelectedCustomer");
            }
        }

        public ObservableCollection<Customer> Customers
        {
            get { return customers; }
            set
            {
                customers.Clear();
                customers = value;
                RaisePropertyChanged("Customers");
            }
        }

        public int CurrentPage
        {
            get { return _pageIndex; }
            set
            {
                _pageIndex = value;
                RaisePropertyChanged("CurrentPage");
            }
        }

        #endregion

        #region Constructors
        public VMCustomerListView()
        {
            deleteCommand = new DelegateCommand<object>(DeleteExecute);
            editCommand = new DelegateCommand<object>(EditExecute);
            viewCommand = new DelegateCommand<object>(ViewExecute);
            addCommand = new DelegateCommand<object>(AddExecute);
            searchCommand = new DelegateCommand<object>(SearchExecute);

            customers = new ObservableCollection<Customer>();
            this.currentCustomer = new Customer();

            if (!IsDesignTime)
            {
                GetCustomers();
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

        public void GetCustomers()
        {
            try
            {
                MainModuleServiceClient client = new MainModuleServiceClient();

                client.GetPagedCustomerCompleted += delegate(object sender, GetPagedCustomerCompletedEventArgs e)
                {
                    Customer[] listCustomers = e.Result;
                    if (listCustomers != null && listCustomers.Length > 0)
                    {
                        Customers.Clear();
                        foreach (var item in listCustomers)
                        {
                            Customers.Add(item);
                        }
                    }
                    if (listCustomers.Length < 4 || listCustomers.Length == 0)
                    {
                        if (this.CurrentPage > 1)
                        {
                            this.CurrentPage--;
                        }
                    }
                };

                client.GetPagedCustomerAsync(new PagedCriteria() { PageIndex = this.CurrentPage, PageCount = 4 });
            }
            catch (Exception excep)
            {
                Debug.WriteLine("GetCustomers: Error at Service:" + excep.ToString());
            }
        }

        public void SearchCustomers(string name)
        {
            try
            {
                MainModuleServiceClient client = new MainModuleServiceClient();

                client.GetPagedCustomerCompleted += delegate(object sender, GetPagedCustomerCompletedEventArgs e)
                {
                    Customer[] listCustomers = e.Result;
                    if (listCustomers != null && listCustomers.Length > 0)
                    {
                        var resultOrders = from o in listCustomers
                                           where o.ContactName.ToLower().Contains(name.ToLower())
                                           select o;
                        Customers.Clear();
                        foreach (var item in resultOrders)
                        {
                            Customers.Add(item);
                        }

                    }
                };

                client.GetPagedCustomerAsync(new PagedCriteria() { PageIndex = this.CurrentPage, PageCount = 4 });
            }
            catch (Exception excep)
            {
                Debug.WriteLine("SearchCustomers: Error at Service:" + excep.ToString());
            }
        }

        public void DeleteCustomers(Customer customer)
        {
            try
            {
                //remove customer

                MainModuleServiceClient client = new MainModuleServiceClient();

                client.RemoveCustomerCompleted += delegate
                {
                    //refresh customer list
                    this.GetCustomers();
                };

                client.RemoveCustomerAsync(customer);
            }
            catch (FaultException<ServiceError> ex)
            {
                MessageBox.Show(ex.Detail.ErrorMessage);
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
        private ICommand deleteCommand;
        public ICommand DeleteCommand
        {
            get { return deleteCommand; }
            set { deleteCommand = value; }
        }
        private ICommand searchCommand;

        public ICommand SearchCommand
        {
            get { return searchCommand; }
            set { searchCommand = value; }
        }
        private ICommand viewCommand;

        public ICommand ViewCommand
        {
            get { return viewCommand; }
            set { viewCommand = value; }
        }
        private ICommand addCommand;

        public ICommand AddCommand
        {
            get { return addCommand; }
            set { addCommand = value; }
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

        private void EditExecute(Object o)
        {
            if (o is Customer)
            {
                Customer current = (Customer)o;
                if (current.CustomerCode != null)
                {
                    ((MainPage)App.Current.RootVisual).editCustomer.DataContext = new ViewModels.VMEditCustomer(current.CustomerCode);
                    ((MainPage)App.Current.RootVisual).GotoEditCustomer.Begin();
                }
            }
            Debug.WriteLine("Edit Execute.");
        }
        private void DeleteExecute(Object o)
        {
            //if (o is Customer)
            //{
            //    Customer current = (Customer)o;
            //    MainModuleServiceClient client = new MainModuleServiceClient();
            //    client.RemoveCustomerCompleted += delegate
            //    {
            //        GetCustomers();
            //    };

            //    client.RemoveCustomerAsync(current);
            //}
            //Debug.WriteLine("Delete Customer.");
        }
        private void SearchExecute(object param)
        {
            if (param is string)
            {
                this.CurrentPage = 0;
                SearchCustomers(param as string);
            }

            Debug.WriteLine("Search Customer.");
        }
        private void ViewExecute(Object o)
        {
            if (o is Customer)
            {
                Customer current = (Customer)o;
                MainModuleServiceClient client = new MainModuleServiceClient();

                client.GetCustomerByCodeCompleted += delegate(object sender, GetCustomerByCodeCompletedEventArgs e)
                {
                    if (e.Error == null)
                    {
                        if (e.Result != null)
                        {
                            ((MainPage)App.Current.RootVisual).viewCustomer.DataContext = new ViewModels.VMCustomer(e.Result);
                            ((MainPage)App.Current.RootVisual).ViewCustomer.Begin();
                        }
                    }
                };

                client.GetCustomerByCodeAsync(current.CustomerCode);
            }
        }
        private void AddExecute(Object o)
        {

            Customer current = (Customer)o;
            ((MainPage)App.Current.RootVisual).addCustomer.DataContext = new ViewModels.VMAddCustomer();
            ((MainPage)App.Current.RootVisual).GotoAddCustomer.Begin();

            Debug.WriteLine("Add Customer.");
        }

        private void NextPageExecute()
        {
            this.CurrentPage++;
            this.GetCustomers();
        }

        private void PreviousPageExecute()
        {
            this.CurrentPage--;
            if (this.CurrentPage < 0) this.CurrentPage = 0;
            this.GetCustomers();
        }

        #endregion

    }

}
