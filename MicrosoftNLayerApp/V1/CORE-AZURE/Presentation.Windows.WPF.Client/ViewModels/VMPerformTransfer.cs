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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ServiceModel;
using System.Windows;
using System.Windows.Input;

using Microsoft.Samples.NLayerApp.Domain.MainModule.Entities;
using Microsoft.Samples.NLayerApp.Presentation.Windows.WPF.Client.ViewModelBase;
using Microsoft.Samples.NLayerApp.Presentation.Windows.WPF.ServiceAgents.Proxies.MainModule;
using Microsoft.Samples.NLayerApp.Presentation.Windows.WPF.ServiceAgents;

namespace Microsoft.Samples.NLayerApp.Presentation.Windows.WPF.Client.ViewModels
{
    /// <summary>
    /// ViewModel for Perform transfer
    /// </summary>
    public class VMPerformTransfer : ObservableObject
    {
        #region Members

        ICommand _closeCommand;
        ICommand _transferCommand;
        ICommand _lockBankAccountCommand;

        ICommand _openAccountSelectorCommand;
        ICommand _searchAccountCommand;
        ICommand _setSelectedAccountCommand;

        
        int _filterSelector = 1;
        decimal _bankTransferAmount;
        string _filterBankAccount = string.Empty;        

        ObservableCollection<BankAccount> _bankAccounts;
        ObservableCollection<BankAccount> _bankAccountsStatus;
        BankAccount _bankAccountSelected;
        string _bankAccountNumberSource;
        string _bankAccountNumberDestination;
        BankAccount _bankAccountToLock;

        bool _isVisiblePopUp = false;
        string _launcherType = "0";

        #endregion

        #region Properties

        public bool IsVisiblePopUp
        {
            get { return _isVisiblePopUp; }
            set
            {
                _isVisiblePopUp = value;
                RaisePropertyChanged("IsVisiblePopUp");
            }
        }

        public int FilterSelector
        {
            get { return _filterSelector; }
            set
            {
                _filterSelector = value;
                RaisePropertyChanged("FilterSelector");
            }
        }

        public string FilterBankAccount
        {
            get { return _filterBankAccount; }
            set
            {
                _filterBankAccount = value.Trim();
                RaisePropertyChanged("FilterBankAccount");
            }
        }

        public ObservableCollection<BankAccount> BankAccounts
        {
            get { return _bankAccounts; }
            set
            {
                _bankAccounts = value;
                RaisePropertyChanged("BankAccounts");
            }
        }

        public ObservableCollection<BankAccount> BankAccountsStatus
        {
            get { return _bankAccountsStatus; }
            set
            {
                _bankAccountsStatus = value;
                RaisePropertyChanged("BankAccountsStatus");
            }
        }

        public BankAccount BankAccountSelected
        {
            get { return _bankAccountSelected; }
            set
            {
                _bankAccountSelected = value;
                RaisePropertyChanged("BankAccountSelected");
            }
        }

        public string BankAccountNumberSource
        {
            get { return _bankAccountNumberSource; }
            set
            {
                _bankAccountNumberSource = value;
                RaisePropertyChanged("BankAccountNumberSource");
            }
        }

        public string BankAccountNumberDestination
        {
            get { return _bankAccountNumberDestination; }
            set
            {
                _bankAccountNumberDestination = value;
                RaisePropertyChanged("BankAccountNumberDestination");
            }
        }

        public BankAccount BankAccountToLock
        {
            get { return _bankAccountToLock; }
            set
            {
                _bankAccountToLock = value;
                RaisePropertyChanged("BankAccountToLock");
            }
        }

        public decimal BankTransferAmount
        {
            get { return _bankTransferAmount; }
            set
            {
                _bankTransferAmount = value;
                RaisePropertyChanged("BankTransferAmount");
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

        public ICommand TransferCommand
        {
            get
            {
                if (_transferCommand == null)
                {
                    _transferCommand = new DelegateCommand<bool>(TransferExecute, CanTransferExecute);
                }
                return _transferCommand;
            }
        }

        public ICommand LockBankAccountCommand
        {
            get
            {
                if (_lockBankAccountCommand == null)
                {
                    _lockBankAccountCommand = new DelegateCommand(LockAccountExecute);
                }
                return _lockBankAccountCommand;
            }
        }


        public ICommand SearchAccountCommand
        {
            get
            {
                if (_searchAccountCommand == null)
                {
                    _searchAccountCommand = new DelegateCommand(SearchAccountExecute);
                }
                return _searchAccountCommand;
            }
        }

        public ICommand SetSelectedAccountCommand
        {
            get
            {
                if (_setSelectedAccountCommand == null)
                {
                    _setSelectedAccountCommand = new DelegateCommand(SetSelectedAccountExecute);
                }
                return _setSelectedAccountCommand;
            }
        }


        public ICommand OpenAccountSelectorCommand
        {
            get
            {
                if (_openAccountSelectorCommand == null)
                {
                    _openAccountSelectorCommand = new DelegateCommand<string>(OpenAccountSelectorExecute);
                }
                return _openAccountSelectorCommand;
            }
        }

        
        #endregion

        #region Constructors

        public VMPerformTransfer()
        {
            //Initialize proxy
            IMainModuleService mainModuleService = ProxyLocator.GetMainModuleService();
            mainModuleService = ProxyLocator.GetMainModuleService();

            if (!NavigationController.IsInDesignMode)
            {
                this.RefreshBankAccountsStatus();
            }
        }

        #endregion

        #region Command Methods

        private void CloseExecute()
        {
            NavigationController.CloseCurrentView.Execute(null);
        }
                
        private void TransferExecute(bool isValidData)
        {
            this.PerformTransfer();
        }

        private bool CanTransferExecute(bool isValidData)
        {
            return isValidData;
        }

        private void LockAccountExecute()
        {
            this.LockAccount();
        }
        
        private void SearchAccountExecute()
        {
            this.GetBankAccounts();
        }

        private void SetSelectedAccountExecute()
        {
            this.IsVisiblePopUp = false;
            if (this.BankAccountSelected != null)
            {
                switch (this._launcherType)
                {
                    case "0":
                        this.BankAccountNumberSource = this.BankAccountSelected.BankAccountNumber;
                        break;
                    case "1":
                        this.BankAccountNumberDestination = this.BankAccountSelected.BankAccountNumber;
                        break;
                    case "2":
                        this.BankAccountToLock = this.BankAccountSelected;
                        break;
                }
            }
        }

        private void OpenAccountSelectorExecute(string laucherType)
        {
            this.FilterSelector = 1;
            this.FilterBankAccount = string.Empty;        
            this.BankAccounts = new ObservableCollection<BankAccount>();
            this.BankAccountSelected = null;
            this._launcherType = laucherType;
            this.IsVisiblePopUp = true;
        }

        #endregion

        #region Private Members

        private void GetBankAccounts()
        {
            if (!string.IsNullOrEmpty(this.FilterBankAccount))
            {
                using (BackgroundWorker worker = new BackgroundWorker())
                {
                    worker.DoWork += delegate(object sender, DoWorkEventArgs e)
                    {
                        IMainModuleService mainModuleService = ProxyLocator.GetMainModuleService();
                        if (this.FilterSelector == 0)
                            e.Result = mainModuleService.GetBankAccounts(new BankAccountInformation() { BankAccountNumber = this.FilterBankAccount });
                        else
                            e.Result = mainModuleService.GetBankAccounts(new BankAccountInformation() { CustomerName = this.FilterBankAccount });
                    };

                    worker.RunWorkerCompleted += delegate(object sender, RunWorkerCompletedEventArgs e)
                    {
                        if (!e.Cancelled && e.Error == null)
                        {
                            List<BankAccount> bankAccounts = e.Result as List<BankAccount>;

                            if (bankAccounts != null)
                            {
                                this.BankAccounts = new ObservableCollection<BankAccount>(bankAccounts);
                            }
                        }
                        else
                            MessageBox.Show(e.Error.Message, "Perform Transfer", MessageBoxButton.OK, MessageBoxImage.Error);
                    };

                    worker.WorkerSupportsCancellation = true;
                    worker.RunWorkerAsync();
                }
            }
        }
        
        private void LockAccount()
        {
            if (this.BankAccountToLock != null)
            {
                using (BackgroundWorker worker = new BackgroundWorker())
                {
                    worker.DoWork += delegate(object sender, DoWorkEventArgs e)
                    {
                        IMainModuleService mainModuleService = ProxyLocator.GetMainModuleService();
                        mainModuleService.ChangeBankAccount(this.BankAccountToLock);
                    };

                    worker.RunWorkerCompleted += delegate(object sender, RunWorkerCompletedEventArgs e)
                    {
                        if (!e.Cancelled && e.Error == null)
                        {
                            this.BankAccountToLock = null;
                            this.RefreshBankAccountsStatus();
                        }
                        else
                            MessageBox.Show(e.Error.Message, "Perform Transfer", MessageBoxButton.OK, MessageBoxImage.Error);
                    };

                    worker.WorkerSupportsCancellation = true;
                    worker.RunWorkerAsync();
                }
            }
        }

        private void PerformTransfer()
        {
            if (!string.IsNullOrEmpty(this.BankAccountNumberDestination) && !string.IsNullOrEmpty(this.BankAccountNumberSource))
            {
                using (BackgroundWorker worker = new BackgroundWorker())
                {
                    worker.DoWork += delegate(object sender, DoWorkEventArgs e)
                    {
                        IMainModuleService mainModuleService = ProxyLocator.GetMainModuleService();
                        mainModuleService.PerformBankTransfer(new TransferInformation() { Amount = this.BankTransferAmount, OriginAccountNumber = this.BankAccountNumberSource.Trim(), DestinationAccountNumber = this.BankAccountNumberDestination.Trim() });
                    };

                    worker.RunWorkerCompleted += delegate(object sender, RunWorkerCompletedEventArgs e)
                    {
                        if (!e.Cancelled && e.Error == null)
                        {
                            this.BankAccountNumberSource = string.Empty;
                            this.BankAccountNumberDestination = string.Empty;
                            this.BankTransferAmount = 0;
                            this.RefreshBankAccountsStatus();
                        }
                        else
                        {
                            var exception = e.Error as FaultException<ServiceError>;
                            if ( exception != null)
                                MessageBox.Show(exception.Detail.ErrorMessage, "Perform Transfer", MessageBoxButton.OK, MessageBoxImage.Error);
                            else
                                MessageBox.Show(e.Error.Message, "Perform Transfer", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    };

                    worker.WorkerSupportsCancellation = true;
                    worker.RunWorkerAsync();
                }
            }
        }


        private void RefreshBankAccountsStatus()
        {
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                worker.DoWork += delegate(object sender, DoWorkEventArgs e)
                {
                    IMainModuleService mainModuleService = ProxyLocator.GetMainModuleService();
                    e.Result = mainModuleService.GetPagedBankAccounts(new PagedCriteria { PageIndex = 0, PageCount = 50 });
                };

                worker.RunWorkerCompleted += delegate(object sender, RunWorkerCompletedEventArgs e)
                {
                    if (!e.Cancelled && e.Error == null)
                    {
                        List<BankAccount> bankAccounts = e.Result as List<BankAccount>;

                        if (bankAccounts != null)
                        {
                            this.BankAccountsStatus = new ObservableCollection<BankAccount>(bankAccounts);
                        }
                    }
                    else
                        MessageBox.Show(e.Error.Message, "Perform Transfer", MessageBoxButton.OK, MessageBoxImage.Error);
                };

                worker.WorkerSupportsCancellation = true;
                worker.RunWorkerAsync();
            }
        }

        #endregion Private Members
    }
}
