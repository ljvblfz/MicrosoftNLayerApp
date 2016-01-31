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
using System.ComponentModel;
using System.IO;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using Microsoft.Samples.NLayerApp.Domain.Core.Entities;
using Microsoft.Samples.NLayerApp.Domain.MainModule.Entities;
using Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client.ServiceAgent;

namespace Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client.ViewModels
{
    /// <summary>
    /// ViewModel Edit Customer
    /// </summary>
    public class VMEditCustomer : ObservableObject
    {
        #region Declarations

        private ICommand _closeCommand;
        private ICommand _cancelCommand;
        private ICommand _saveCommand;
        private ICommand _addPictureCommand;
        private Customer _currentCustomer;

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
                    _saveCommand = new DelegateCommand<string>(SaveExecute, CanSaveExecute);
                }
                return _saveCommand;
            }
        }

        #endregion

        #region Constructors

        public VMEditCustomer()
        {
            this._currentCustomer = new Customer()
            {
                Address = new AddressInformation(),
                CustomerPicture = new CustomerPicture()
            };

        }

        public VMEditCustomer(string customerCode)
        {
            MainModuleServiceClient client = new MainModuleServiceClient();

            client.GetCustomerByCodeCompleted += delegate(object sender, GetCustomerByCodeCompletedEventArgs e)
            {
                if (e.Error == null)
                {
                    if (e.Result != null)
                    {
                        Customer = e.Result;
                        if (this.Customer.CustomerPicture == null)
                        {
                            this.Customer.CustomerPicture = new CustomerPicture();
                        }
                    }
                }
            };

            client.GetCustomerByCodeAsync(customerCode);            
        }

        #endregion

        #region Command Methods

        private void AddPictureExecute()
        {

            //NOTE: DEPRECATED
            //Create a file dialog
            OpenFileDialog selectPictureDialog = new OpenFileDialog();

            //set properties for this dialog ( only once file, title and check that file exist )
            selectPictureDialog.Multiselect = false;

            if (selectPictureDialog.ShowDialog() == true)
            {
                string picturePath = selectPictureDialog.File.FullName;
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
            //TODO: Make the close implementation
        }

        private void CancelExecute()
        {
            MainModuleServiceClient client = new MainModuleServiceClient();

            client.GetCustomerByCodeCompleted += delegate(object sender, GetCustomerByCodeCompletedEventArgs e)
            {
                if (e.Error == null)
                {
                    if (e.Result != null)
                    {
                        Customer = e.Result;
                    }
                }
            };

            client.GetCustomerByCodeAsync(Customer.CustomerCode);
            ((MainPage)App.Current.RootVisual).GoBackEditCustomer.Begin();
        }

        private void SaveExecute(string isValidData)
        {
            try
            {
                if (this.Photo == null)
                    this.Customer.CustomerPicture = new CustomerPicture();

                this.Customer.MarkAsModified();

                MainModuleServiceClient client = new MainModuleServiceClient();
                client.ChangeCustomerCompleted += delegate(object sender, AsyncCompletedEventArgs e)
                {
                    if (e.Error == null)
                    {
                        Customer.AcceptChanges();
                        ((VMCustomer)((MainPage)App.Current.RootVisual).viewCustomer.DataContext).Customer = Customer;
                        ((VMCustomerListView)((MainPage)App.Current.RootVisual).viewListCustomers.DataContext).GetCustomers();
                    }
                    ((MainPage)App.Current.RootVisual).GoBackEditCustomer.Begin();
                };
                client.ChangeCustomerAsync(this.Customer);

            }
            catch (FaultException<ServiceError> ex)
            {
                MessageBox.Show(ex.Detail.ErrorMessage);
            }
        }

        private bool CanSaveExecute(string isValidData)
        {
            return (this.Customer != null);
        }

        #endregion
    }
}
