
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

namespace Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client.ViewModels
{
    /// <summary>
    /// ViewModel menu
    /// </summary>
    public class VMMenu
        : ObservableObject
    {
        #region Constructors

        public VMMenu()
        {
            _customers = new VMMenuCustomers();
            _banking = new VMMenuBanking();
            _orders = new VMMenuOrders();
        }

        #endregion

        #region Properties

        private VMMenuCustomers _customers;

        public VMMenuCustomers Customers
        {
            get { return _customers; }
            set
            {
                _customers = value;
                RaisePropertyChanged("Customers");
            }
        }

        private VMMenuOrders _orders;

        public VMMenuOrders Orders
        {
            get { return _orders; }
            set
            {
                _orders = value;
                RaisePropertyChanged("Orders");
            }
        }

        private VMMenuBanking _banking;

        public VMMenuBanking Banking
        {
            get { return _banking; }
            set
            {
                _banking = value;
                RaisePropertyChanged("Banking");
            }
        }



        #endregion
    }
}
