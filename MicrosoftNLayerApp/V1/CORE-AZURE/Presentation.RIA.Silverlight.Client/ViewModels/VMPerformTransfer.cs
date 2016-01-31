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
using System.Linq;
using System.ComponentModel;
using System.ServiceModel;

using Microsoft.Samples.NLayerApp.Domain.MainModule.Entities;
using Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client.ServiceAgent;

namespace Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client.ViewModels
{
    /// <summary>
    /// ViewModel perform transfer
    /// </summary>
    public class VMPerformTransfer : ObservableObject
    {
        #region Declarations

        private ICommand _closeCommand;
        private ICommand _transferCommand;
        private ICommand _lockBankAccountCommand;

        private ICommand _openAccountSelectorCommand;
        private ICommand _searchAccountCommand;
        private ICommand _setSelectedAccountCommand;

        private int _filterSelector = 1;
        private decimal _bankTransferAmount;
        private string _filterBankAccount = string.Empty;

        private List<BankAccount> _bankAccounts;
        private BankAccount _bankAccountSelected;
        private BankAccount _bankAccountSource;
        private BankAccount _bankAccountDestination;
        private BankAccount _bankAccountToLock;

        private bool _isVisiblePopUp = false;
        private string _launcherType = "0";

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

        public List<BankAccount> BankAccounts
        {
            get { return _bankAccounts; }
            set
            {
                _bankAccounts = value;
                RaisePropertyChanged("BankAccounts");
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

        public BankAccount BankAccountSource
        {
            get { return _bankAccountSource; }
            set
            {
                _bankAccountSource = value;
                RaisePropertyChanged("BankAccountSource");
            }
        }

        public BankAccount BankAccountDestination
        {
            get { return _bankAccountDestination; }
            set
            {
                _bankAccountDestination = value;
                RaisePropertyChanged("BankAccountDestination");
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
                    _transferCommand = new DelegateCommand(TransferExecute);
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
            if (!IsDesignTime)
                this.GetBankAccounts();
        }

        #endregion

        #region Command Methods

        private void CloseExecute()
        {
            //TODO: Make the close implementation
        }

        private void TransferExecute()
        {
            this.PerformTransfer();
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
                        this.BankAccountSource = this.BankAccountSelected;
                        break;
                    case "1":
                        this.BankAccountDestination = this.BankAccountSelected;
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
            this.BankAccounts = new List<BankAccount>();
            this.BankAccountSelected = null;
            this._launcherType = laucherType;
            this.IsVisiblePopUp = true;
        }

        #endregion

        #region Private Members

        public void GetBankAccounts()
        {
            if (!string.IsNullOrEmpty(this.FilterBankAccount))
            {
                MainModuleServiceClient client = new MainModuleServiceClient();

                if (this.FilterSelector == 0)
                    client.GetBankAccountsAsync(new BankAccountInformation() { BankAccountNumber = this.FilterBankAccount });
                else
                    client.GetBankAccountsAsync(new BankAccountInformation() { CustomerName = this.FilterBankAccount });

                client.GetBankAccountsCompleted += delegate(object s, GetBankAccountsCompletedEventArgs e)
                {
                    if (!e.Cancelled && e.Error == null)
                    {
                        List<BankAccount> bankAccounts = new List<BankAccount>();
                        e.Result.ToList().ForEach(ba => bankAccounts.Add(ba));

                        if (bankAccounts != null)
                            this.BankAccounts = bankAccounts;
                        
                    }
                    else
                    {
                        MessageBox.Show(e.Error.Message, "Perform Transfer", MessageBoxButton.OK);
                    }
                };

            }
            else
            {
                MainModuleServiceClient client = new MainModuleServiceClient();
                client.GetBankAccountsAsync(new BankAccountInformation());

                client.GetBankAccountsCompleted += delegate(object s, GetBankAccountsCompletedEventArgs e)
                {
                    if (!e.Cancelled && e.Error == null)
                    {
                        List<BankAccount> bankAccounts = new List<BankAccount>();
                        e.Result.ToList().ForEach(ba => bankAccounts.Add(ba));

                        if (bankAccounts != null)
                            this.BankAccounts = bankAccounts;
                    }
                    else
                    {
                        MessageBox.Show(e.Error.Message, "Perform Transfer", MessageBoxButton.OK);
                    }
                };
            }
        }

        private void LockAccount()
        {
            if (this.BankAccountToLock != null)
            {
                MainModuleServiceClient client = new MainModuleServiceClient();
                client.ChangeBankAccountAsync(this.BankAccountToLock);

                client.ChangeBankAccountCompleted += delegate(object s, AsyncCompletedEventArgs e)
                {
                    if (!e.Cancelled && e.Error == null)
                    {
                        this.BankAccountToLock = null;
                        this.RefreshBankAccountsStatus();
                    }
                    else
                        MessageBox.Show(e.Error.Message, "Perform Transfer", MessageBoxButton.OK);
                };
            }
        }

        private void PerformTransfer()
        {
            if (this.BankAccountDestination != null && this.BankAccountSource != null)
            {
                MainModuleServiceClient client = new MainModuleServiceClient();
                client.PerformBankTransferAsync(new TransferInformation() { Amount = this.BankTransferAmount, OriginAccountNumber = this.BankAccountSource.BankAccountNumber, DestinationAccountNumber = this.BankAccountDestination.BankAccountNumber });

                client.PerformBankTransferCompleted += delegate(object s, AsyncCompletedEventArgs e)
                {
                    if (!e.Cancelled && e.Error == null)
                    {
                        this.BankAccountSource = null;
                        this.BankAccountDestination = null;
                        this.BankTransferAmount = 0;
                        this.RefreshBankAccountsStatus();
                    }
                    else
                    {
                        var exception = e.Error as FaultException<ServiceError>;
                        if (exception != null)
                            MessageBox.Show(exception.Detail.ErrorMessage, "Perform Transfer", MessageBoxButton.OK);
                        else
                            MessageBox.Show(e.Error.Message, "Perform Transfer", MessageBoxButton.OK);
                    }
                };

            }
        }


        private void RefreshBankAccountsStatus()
        {
            MainModuleServiceClient client = new MainModuleServiceClient();
            client.GetPagedBankAccountsAsync(new PagedCriteria { PageIndex = 0, PageCount = 50 });

            client.GetPagedBankAccountsCompleted += delegate(object sender, GetPagedBankAccountsCompletedEventArgs e)
            {
                if (!e.Cancelled && e.Error == null)
                {
                    List<BankAccount> bankAccounts = new List<BankAccount>();
                    e.Result.ToList().ForEach(ba => bankAccounts.Add(ba));
                    
                    if (bankAccounts != null)
                    {
                        this.BankAccounts = bankAccounts;
                    }
                }
                else
                    MessageBox.Show(e.Error.Message, "Perform Transfer", MessageBoxButton.OK);
            };


        }

        #endregion Private Members
    }
}
