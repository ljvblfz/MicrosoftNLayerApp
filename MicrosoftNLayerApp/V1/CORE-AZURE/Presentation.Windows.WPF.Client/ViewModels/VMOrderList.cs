using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// ViewModel for Order
    /// </summary>
    public class VMOrderList : ObservableObject
    {
        #region Members

        ICommand _filterCommand;
        ICommand _viewCommand;
        ICommand _editCommand;
        ICommand _deleteCommand;
        ICommand _nextPageCommand;
        ICommand _previousPageCommand;
        DateTime? _filterFrom;
        DateTime? _filterTo;
        ObservableCollection<Order> _orders;
        ICollectionView _viewData;
        int _pageIndex = 0;
        

        #endregion

        #region Properties

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

        public ObservableCollection<Order> Orders
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

        public VMOrderList()
        {
            //Initialize proxy
            IMainModuleService mainModuleService = ProxyLocator.GetMainModuleService();
            mainModuleService = ProxyLocator.GetMainModuleService();

            if (!NavigationController.IsInDesignMode)
            {
                this.GetOrders();
            }
        }

        #endregion

        #region Command Methods

        private void FilterExecute()
        {
            if (this._viewData != null )
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
            throw new InvalidOperationException("Not implemented");
        }

        private bool CanDeleteExecute(Order order)
        {
            return (order != null);
        }

        private void NextPageExecute()
        {
            this._pageIndex++;
            this.GetOrders();
        }

        private void PreviousPageExecute()
        {
            this._pageIndex--;
            if (this._pageIndex < 0) this._pageIndex = 0;
            this.GetOrders();
        }
        #endregion

        #region Private Methods

        private bool FilterOrdersCallback(object item)
        {
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
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                worker.DoWork += delegate(object sender, DoWorkEventArgs e)
                {
                    IMainModuleService mainModuleService = ProxyLocator.GetMainModuleService();
                    e.Result = mainModuleService.GetPagedOrders(new PagedCriteria() { PageIndex = this._pageIndex, PageCount = 10 });
                };

                worker.RunWorkerCompleted += delegate(object sender, RunWorkerCompletedEventArgs e)
                {
                    if (!e.Cancelled && e.Error == null)
                    {
                        List<Order> orders = e.Result as List<Order>;

                        if (orders != null)
                        {
                            this.Orders = new ObservableCollection<Order>(orders);
                            this._viewData = CollectionViewSource.GetDefaultView(this.Orders);
                            this._viewData.Filter = null;
                        }
                    }
                    else
                        MessageBox.Show(e.Error.Message, "Orders List", MessageBoxButton.OK, MessageBoxImage.Error);
                };

                worker.WorkerSupportsCancellation = true;
                worker.RunWorkerAsync();
            }
        }

        #endregion
    }
}
