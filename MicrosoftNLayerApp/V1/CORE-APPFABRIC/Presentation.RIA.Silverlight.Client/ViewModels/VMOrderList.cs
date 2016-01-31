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
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using Microsoft.Samples.NLayerApp.Domain.MainModule.Entities;
using Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client.ServiceAgent;

namespace Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client.ViewModels
{
    /// <summary>
    /// ViewModel order list
    /// </summary>
    public class VMOrderList : ObservableObject
    {
        #region Declarations

        private ICommand _filterCommand;
        private ICommand _viewCommand;
        private ICommand _editCommand;
        private ICommand _deleteCommand;
        private DateTime? _filterFrom;
        private DateTime? _filterTo;
        private List<Order> _orders;
        private ICollectionView _viewData;

        #endregion

        #region Properties

        public bool IsDesignTime
        {
            get
            {
                if (Application.Current.RootVisual != null)
                    return DesignerProperties.GetIsInDesignMode(Application.Current.RootVisual);
                return false;
            }
        }

        public DateTime? FilterFrom
        {
            get { return _filterFrom; }
            set
            {
                _filterFrom = value;
                RaisePropertyChanged("FilterFrom");
            }
        }

        public DateTime? FilterTo
        {
            get { return _filterTo; }
            set
            {
                _filterTo = value;
                RaisePropertyChanged("FilterTo");
            }
        }

        public List<Order> Orders
        {
            get { return _orders; }
            set
            {
                _orders = value;
                RaisePropertyChanged("Orders");
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
                    _viewCommand = new DelegateCommand<Order>(ViewExecute, CanViewExecute);
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
                    _editCommand = new DelegateCommand<Order>(EditExecute, CanEditExecute);
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
                    _deleteCommand = new DelegateCommand<Order>(DeleteExecute, CanDeleteExecute);
                }
                return _deleteCommand;
            }
        }

        #endregion

        #region Constructors

        public VMOrderList()
        {
            if (!IsDesignTime)
            {
                this.GetOrders();
            }
        }

        #endregion

        #region Command Methods

        private void FilterExecute()
        {
            if (this._viewData != null)
                this._viewData.Filter = new Predicate<object>(FilterOrdersCallback);
        }

        private void ViewExecute(Order order)
        {

        }

        private bool CanViewExecute(Order order)
        {
            return (order != null);
        }

        private void EditExecute(Order order)
        {
        }

        private bool CanEditExecute(Order order)
        {
            return (order != null);
        }

        private void DeleteExecute(Order order)
        {
            //throw new InvalidOperationException("Not implemented");
        }

        private bool CanDeleteExecute(Order order)
        {
            return (order != null);
        }

        #endregion

        #region Private Methods

        private bool FilterOrdersCallback(object item)
        {
            GetOrders();
            if (item != null)
            {
                Order order = item as Order;

                if (order.OrderDate != null && this.FilterFrom != null && order.OrderDate.Value.CompareTo(this.FilterFrom) > 0) return true;
                if (order.OrderDate != null && this.FilterTo != null && order.OrderDate.Value.CompareTo(this.FilterTo) < 0) return true;

                return false;
            }

            return true;
        }


        private void GetOrders()
        {

            MainModuleServiceClient client = new MainModuleServiceClient();

            client.GetPagedOrdersAsync(new PagedCriteria() { PageIndex = 0, PageCount = 100 });


            client.GetPagedOrdersCompleted += delegate(object sender, GetPagedOrdersCompletedEventArgs e)
            {
                if (!e.Cancelled && e.Error == null)
                {
                    List<Order> orders = new List<Order>();

                    foreach (var item in e.Result)
                    {
                        orders.Add(item);
                    }

                    if (orders != null)
                    {
                        this.Orders = orders;
                        CollectionViewSource collectionViewSource = new CollectionViewSource();
                        collectionViewSource.Source = orders;
                        ICollectionView collectionView = collectionViewSource.View;
                        this._viewData = collectionView;
                        this._viewData.Filter = null;
                    }
                }
                else
                    MessageBox.Show(e.Error.Message, "Orders List", MessageBoxButton.OK);
            };

        }

        #endregion
    }
}
