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
using System.IO;
using System.ServiceModel;
using System.Windows;
using System.Windows.Input;

using Microsoft.Samples.NLayerApp.Domain.Core.Entities;
using Microsoft.Samples.NLayerApp.Domain.MainModule.Entities;
using Microsoft.Samples.NLayerApp.Presentation.Windows.WPF.Client.ViewModelBase;
using Microsoft.Samples.NLayerApp.Presentation.Windows.WPF.ServiceAgents;
using Microsoft.Samples.NLayerApp.Presentation.Windows.WPF.ServiceAgents.Proxies.MainModule;

using Microsoft.Win32;

namespace Microsoft.Samples.NLayerApp.Presentation.Windows.WPF.Client.ViewModels
{
    /// <summary>
    /// ViewModel for Add Customer
    /// </summary>
    public class VMAddCustomer : ObservableObject
    {
        #region Members

        ICommand _closeCommand;
        ICommand _cancelCommand;
        ICommand _saveCommand;
        ICommand _addPictureCommand;
        Customer _currentCustomer;
        

        ObservableCollection<Country> _countries;
        

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

        public string CompanyName
        {
            get { return _currentCustomer.CompanyName; }
            set
            {
                _currentCustomer.CompanyName = value;
                _currentCustomer.CustomerCode = (value.ToString().Length > 3)? value.ToString().Substring(0, 3) : null;
                RaisePropertyChanged("CompanyName");
            }
        }

        public string ContactName
        {
            get { return _currentCustomer.ContactName; }
            set
            {
                _currentCustomer.ContactName = value;
                RaisePropertyChanged("ContactName");
            }
        }

        public string Address
        {
            get { return _currentCustomer.Address.Address; }
            set
            {
                _currentCustomer.Address.Address = value;
                RaisePropertyChanged("Address");
            }
        }

        public string City
        {
            get { return _currentCustomer.Address.City; }
            set
            {
                _currentCustomer.Address.City = value;
                RaisePropertyChanged("City");
            }
        }

        public string Zip
        {
            get { return _currentCustomer.Address.PostalCode; }
            set
            {
                _currentCustomer.Address.PostalCode = value;
                RaisePropertyChanged("Zip");
            }
        }

        public string Telephone
        {
            get { return _currentCustomer.Address.Telephone; }
            set
            {
                _currentCustomer.Address.Telephone = value;
                RaisePropertyChanged("Telephone");
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

        public int? CountryID
        {
            get
            {
                return _currentCustomer.CountryId;
            }
            set
            {
                _currentCustomer.CountryId = value;
                RaisePropertyChanged("CountryID");
            }
        }

        public ObservableCollection<Country> Countries
        {
            get { return _countries; }
            set
            {
                _countries = value;
                RaisePropertyChanged("Countries");
            }
        }

        #endregion

        #region Command Properties

        public ICommand AddPictureCommand
        {
            get
            {
                if (_addPictureCommand == null)
                {
                    _addPictureCommand = new DelegateCommand(AddPictureExecute);
                }
                return _addPictureCommand;
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

        public ICommand CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                {
                    _cancelCommand = new DelegateCommand(CancelExecute);
                }
                return _cancelCommand;
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new DelegateCommand<bool>(SaveExecute, CanSaveExecute);
                }
                return _saveCommand;
            }
        }

        #endregion

        #region Constructors

        public VMAddCustomer()
        {         

            //create default customer information

            this._currentCustomer = new Customer();
            this._currentCustomer.IsEnabled = true;

            this._currentCustomer.Address = new AddressInformation();
            this._currentCustomer.CustomerPicture = new CustomerPicture()
                                                    {
                                                       CustomerId = _currentCustomer.CustomerId,
                                                       Customer = _currentCustomer,
                                                       Photo = null
                                                    };

            this.CompanyName = "Company Name Here...";

            if (!NavigationController.IsInDesignMode)
                this.LoadCountries(); 
        }

        #endregion

        #region Command Methods

        private void AddPictureExecute()
        {
            //Create a file dialog
            OpenFileDialog selectPictureDialog = new OpenFileDialog();

            //set properties for this dialog ( only once file, title and check that file exist )
            selectPictureDialog.CheckFileExists = true;
            selectPictureDialog.Multiselect = false;
            selectPictureDialog.Title = "Select picture for Customer";

            if (selectPictureDialog.ShowDialog() == true)
            {
                string picturePath = selectPictureDialog.FileName;
                byte[] buffer;
                using (FileStream stream = new FileStream(picturePath, FileMode.Open, FileAccess.ReadWrite))
                {
                    buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, buffer.Length);
                }

                //assign selected picture
                Photo = buffer;
            }
        }

        private void CloseExecute()
        {
            NavigationController.CloseCurrentView.Execute(null);
        }

        private void CancelExecute()
        {
            NavigationController.CloseCurrentView.Execute(null);
        }

        private void SaveExecute(bool isValidData)
        {
            try
            {
                //mark as added
                this._currentCustomer.MarkAsAdded<Customer>();
                
                //save in server
                IMainModuleService mainModuleService = ProxyLocator.GetMainModuleService();
                mainModuleService.AddCustomer(this._currentCustomer);

                //accept changes
                this._currentCustomer.AcceptChanges();

                NavigationController.NavigateToView(new CustomerListView());
            }
            catch (FaultException<ServiceError> ex)
            {
                MessageBox.Show(ex.Detail.ErrorMessage);
            }
        }

        private bool CanSaveExecute(bool isValidData)
        {
            return isValidData;
        }

        private void LoadCountries()
        {
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                worker.DoWork += delegate(object sender, DoWorkEventArgs e)
                {
                    IMainModuleService mainModuleService = ProxyLocator.GetMainModuleService();
                    e.Result = mainModuleService.GetPagedCountries(new PagedCriteria { PageIndex = 0, PageCount = 100 });
                };

                worker.RunWorkerCompleted += delegate(object sender, RunWorkerCompletedEventArgs e)
                {
                    if (!e.Cancelled && e.Error == null)
                    {
                        List<Country> countries = e.Result as List<Country>;

                        if (countries != null)
                        {
                            this.Countries = new ObservableCollection<Country>(countries);
                        }
                    }
                    else
                        MessageBox.Show(e.Error.Message, "Add Customer", MessageBoxButton.OK, MessageBoxImage.Error);
                };

                worker.WorkerSupportsCancellation = true;
                worker.RunWorkerAsync();
            }
        }

        #endregion
    }
}
