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
using System.Windows.Input;

using Microsoft.Samples.NLayerApp.Domain.MainModule.Entities;
using Microsoft.Samples.NLayerApp.Presentation.Windows.WPF.Client.ViewModelBase;
using Microsoft.Samples.NLayerApp.Presentation.Windows.WPF.ServiceAgents.Proxies.MainModule;
using Microsoft.Samples.NLayerApp.Presentation.Windows.WPF.ServiceAgents;

namespace Microsoft.Samples.NLayerApp.Presentation.Windows.WPF.Client.ViewModels
{
    /// <summary>
    /// ViewModel for transfer list
    /// </summary>
    public class VMTransferList : ObservableObject
    {
        #region Members

        ICommand _searchCommand;
        ICommand _addCommand;
        ICommand _editCommand;
        ICommand _deleteCommand;
        ICommand _nextPageCommand;
        ICommand _previousPageCommand;
        string _filterCompanyName;
        DateTime _filterInitialDate;
        DateTime _filterEndDate;
        ObservableCollection<BankTransfer> _transfers;
        int _pageIndex = 0;

        #endregion

        #region Properties

        public string FilterCompanyName
        {
            get { return _filterCompanyName; }
            set
            {
                _filterCompanyName = value;
                RaisePropertyChanged("FilterCompanyName");
            }
        }

        public DateTime FilterInitialDate
        {
            get { return _filterInitialDate; }
            set
            {
                _filterInitialDate = value;
                RaisePropertyChanged("FilterInitialDate");
            }
        }

        public DateTime FilterEndDate
        {
            get { return _filterEndDate; }
            set
            {
                _filterEndDate = value;
                RaisePropertyChanged("FilterEndDate");
            }
        }

        public ObservableCollection<BankTransfer> Transfers
        {
            get { return _transfers; }
            set
            {
                _transfers = value;
                RaisePropertyChanged("Transfers");
            }
        }

        #endregion

        #region Command Properties

        public ICommand SearchCommand
        {
            get
            {
                if (_searchCommand == null)
                {
                    _searchCommand = new DelegateCommand(SearchExecute);
                }
                return _searchCommand;
            }
        }

        public ICommand AddCommand
        {
            get
            {
                if (_addCommand == null)
                {
                    _addCommand = new DelegateCommand(AddExecute);
                }
                return _addCommand;
            }
        }

        public ICommand EditCommand
        {
            get
            {
                if (_editCommand == null)
                {
                    _editCommand = new DelegateCommand<BankTransfer>(EditExecute, CanEditExecute);
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
                    _deleteCommand = new DelegateCommand<BankTransfer>(DeleteExecute, CanDeleteExecute);
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

        public VMTransferList()
        {
            //Initialize Proxy
            IMainModuleService mainModuleService = ProxyLocator.GetMainModuleService();
            mainModuleService = ProxyLocator.GetMainModuleService();

            if (!NavigationController.IsInDesignMode)
            {
                this.GetTransfers();
            }
        }

        #endregion

        #region Command Methods

        private void SearchExecute()
        {
            this.GetTransfers(this.FilterCompanyName, this.FilterInitialDate, this.FilterEndDate);
        }

        private void AddExecute()
        {

        }

        private void EditExecute(BankTransfer transfer)
        {
        }

        private bool CanEditExecute(BankTransfer transfer)
        {
            return (transfer != null);
        }

        private void DeleteExecute(BankTransfer transfer)
        {
            try
            {   
                this.GetTransfers();
            }
            catch (FaultException<ServiceError> ex)
            {
                MessageBox.Show(ex.Detail.ErrorMessage);
            }
        }

        private bool CanDeleteExecute(object transfer)
        {
            return (transfer != null);
        }

        private void NextPageExecute()
        {
            this._pageIndex++;
            this.GetTransfers();
        }

        private void PreviousPageExecute()
        {
            this._pageIndex--;
            if (this._pageIndex < 0) this._pageIndex = 0;
            this.GetTransfers();
        }
        #endregion

        #region Private Members

        private void GetTransfers()
        {
            this.GetTransfers(string.Empty, DateTime.MinValue, DateTime.MaxValue);
        }

        private void GetTransfers(string companyName, DateTime fromDate, DateTime toDate)
        {
            //TODO: Add service for recover transfer for this specification, at this moment only get all transfer in paged mode
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                worker.DoWork += delegate(object sender, DoWorkEventArgs e)
                {
                    IMainModuleService mainModuleService = ProxyLocator.GetMainModuleService();
                    e.Result = mainModuleService.GetPagedTransfers(new PagedCriteria() { PageIndex = this._pageIndex, PageCount = 10 });
                    
                };

                worker.RunWorkerCompleted += delegate(object sender, RunWorkerCompletedEventArgs e)
                {
                    if (!e.Cancelled && e.Error == null)
                    {
                        List<BankTransfer> transfers = e.Result as List<BankTransfer>;
                        if ( transfers != null )
                            this.Transfers = new ObservableCollection<BankTransfer>(transfers);
                    }
                    else
                        MessageBox.Show(e.Error.Message, "Transfers List", MessageBoxButton.OK, MessageBoxImage.Error);
                };

                worker.WorkerSupportsCancellation = true;
                worker.RunWorkerAsync();

            }
        }

        #endregion
    }
}
