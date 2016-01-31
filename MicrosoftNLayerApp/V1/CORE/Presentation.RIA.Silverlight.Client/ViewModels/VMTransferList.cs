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
using System.ServiceModel;

using Microsoft.Samples.NLayerApp.Domain.MainModule.Entities;
using Microsoft.Samples.NLayerApp.Domain.Core.Entities;
using Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client.ServiceAgent;



namespace Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client.ViewModels
{
    /// <summary>
    /// ViewModel transfer list
    /// </summary>
    public class VMTransferList : ObservableObject
    {
        #region Declarations

        private ICommand _searchCommand;
        private ICommand _addCommand;
        private ICommand _editCommand;
        private ICommand _deleteCommand;
        private string _filterCompanyName;
        private DateTime _filterInitialDate;
        private DateTime _filterEndDate;
        private List<BankTransfer> _transfers;

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

        public List<BankTransfer> Transfers
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

        #endregion

        #region Constructors

        public VMTransferList()
        {

            this.GetTransfers();
           
        }

        #endregion

        #region Command Methods

        private void SearchExecute()
        {
            this.GetTransfers(this.FilterCompanyName, this.FilterInitialDate, this.FilterEndDate);
        }

        private void AddExecute()
        {
            VisualStateManager.GoToState(((MainPage)App.Current.RootVisual), "ToAddTransfer", true);
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
                transfer.ChangeTracker.State = ObjectState.Deleted;

                //MainModuleServiceClient client = new MainModuleServiceClient();
                //client.RemoveBankTransferAsync(transfer);

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

        #endregion

        #region Private Members

        private void GetTransfers()
        {
            this.GetTransfers(string.Empty, DateTime.MinValue, DateTime.MaxValue);
        }

        private void GetTransfers(string companyName, DateTime fromDate, DateTime toDate)
        {
            //TODO: Add service for recover transfer for this specification, at this moment only get all transfer in paged mode

            MainModuleServiceClient client = new MainModuleServiceClient();
            client.GetPagedTransfersAsync(new PagedCriteria() { PageIndex = 0, PageCount = 100 });

            client.GetPagedTransfersCompleted += delegate(object sender, GetPagedTransfersCompletedEventArgs e)
            {
                if (!e.Cancelled && e.Error == null)
                {
                    this.Transfers = new List<BankTransfer>();
                    foreach (var item in e.Result)
                    {
                        this.Transfers.Add(item);
                    }
                }
            };

        }

        #endregion
    }
}
