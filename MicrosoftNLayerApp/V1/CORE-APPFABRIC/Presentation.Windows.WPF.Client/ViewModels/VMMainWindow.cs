using System.Windows.Input;
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
using Microsoft.Samples.NLayerApp.Presentation.Windows.WPF.Client.ViewModelBase;

namespace Microsoft.Samples.NLayerApp.Presentation.Windows.WPF.Client.ViewModels
{
    /// <summary>
    /// ViewModel for Main Windows
    /// </summary>
    public class VMMainWindow : ObservableObject
    {
        #region Members

        ICommand _customerListCommand;
        ICommand _addCustomerCommand;
        ICommand _transferListCommand;
        ICommand _performTransferCommand;
        ICommand _orderListCommand;
        ICommand _performOrderCommand;

        ICommand _minimizeWindowCommand;
        ICommand _maximizeWindowCommand;
        ICommand _closeWindowCommand;

        

        #endregion

        #region Command Properties

        public ICommand CustomerListCommand
        {
            get
            {
                if (_customerListCommand == null)
                {
                    _customerListCommand = new DelegateCommand(CustomerListExecute);
                }
                return _customerListCommand;
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
        public ICommand TransferListCommand
        {
            get
            {
                if (_transferListCommand == null)
                {
                    _transferListCommand = new DelegateCommand(TransferListExecute);
                }
                return _transferListCommand;
            }
        }
        public ICommand PerformTransferCommand
        {
            get
            {
                if (_performTransferCommand == null)
                {
                    _performTransferCommand = new DelegateCommand(PerformTransferExecute);
                }
                return _performTransferCommand;
            }
        }
        public ICommand OrderListCommand
        {
            get
            {
                if (_orderListCommand == null)
                {
                    _orderListCommand = new DelegateCommand(OrderListExecute);
                }
                return _orderListCommand;
            }
        }
        public ICommand PerformOrderCommand
        {
            get
            {
                if (_performOrderCommand == null)
                {
                    _performOrderCommand = new DelegateCommand(PerformOrderExecute);
                }
                return _performOrderCommand;
            }
        }

        public ICommand MinimizeWindowCommand
        {
            get
            {
                if (_minimizeWindowCommand == null)
                {
                    _minimizeWindowCommand = new DelegateCommand(MinimizeWindowExecute);
                }
                return _minimizeWindowCommand;
            }
        }
        public ICommand MaximizeWindowCommand
        {
            get
            {
                if (_maximizeWindowCommand == null)
                {
                    _maximizeWindowCommand = new DelegateCommand(MaximizeWindowExecute);
                }
                return _maximizeWindowCommand;
            }
        }
        public ICommand CloseWindowCommand
        {
            get
            {
                if (_closeWindowCommand == null)
                {
                    _closeWindowCommand = new DelegateCommand(CloseWindowExecute);
                }
                return _closeWindowCommand;
            }
        }

        #endregion

        #region Command Methods

        private void CustomerListExecute()
        {
            NavigationController.NavigateToView(new CustomerListView());
        }

        private void AddCustomerExecute()
        {
            NavigationController.NavigateToView(new AddCustomerView());
        }

        private void TransferListExecute()
        {
            NavigationController.NavigateToView(new TransferListView());
        }

        private void PerformTransferExecute()
        {
            NavigationController.NavigateToView(new PerformTransferView());
        }

        private void OrderListExecute()
        {
            NavigationController.NavigateToView(new OrderView());
        }

        private void PerformOrderExecute()
        {
            NavigationController.NavigateToView(new PerformOrderView());
        }

        private void MinimizeWindowExecute()
        {
            App.Current.MainWindow.WindowState = System.Windows.WindowState.Minimized;
        }

        private void MaximizeWindowExecute()
        {
            if (App.Current.MainWindow.WindowState == System.Windows.WindowState.Maximized)
                App.Current.MainWindow.WindowState = System.Windows.WindowState.Normal;
            else
                App.Current.MainWindow.WindowState = System.Windows.WindowState.Maximized;
        }

        private void CloseWindowExecute()
        {
            App.Current.Shutdown();
        }

        #endregion
    }
}
