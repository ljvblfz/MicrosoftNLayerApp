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
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Samples.NLayerApp.Presentation.Windows.WPF.Client.ViewModelBase;

namespace Microsoft.Samples.NLayerApp.Presentation.Windows.WPF.Client
{
    /// <summary>
    /// Application Navigation controller
    /// </summary>
    public static class NavigationController
    {
        #region Properties

        private static UserControl currentView;
        private static ICommand closeCurrentViewCommand;

        public static ICommand CloseCurrentView
        {
            get
            {
                if (NavigationController.closeCurrentViewCommand == null)
                {
                    NavigationController.closeCurrentViewCommand = new DelegateCommand(
                                                                                        () => { 
                                                                                            if (NavigationController.currentView != null)
                                                                                            {
                                                                                                ((MainWindow)App.Current.MainWindow).ContentHolder.Children.Remove(NavigationController.currentView);

                                                                                                NavigationController.currentView = null;
                                                                                            }
                                                                                        }
                    );
                }
                return NavigationController.closeCurrentViewCommand;
            }
        }

        public static bool IsInDesignMode
        {
            get
            {
                if (App.Current.MainWindow==null)
                    return true;

                return  DesignerProperties.GetIsInDesignMode(App.Current.MainWindow);
            }
        }

        #endregion

        #region Public navigation methods

        public static void NavigateToView(UserControl view)
        {
            NavigateToView(view, null);
        }

        public static void NavigateToView(UserControl view, ObservableObject viewModel)
        {
            if (view != null)
            {
                ((MainWindow)App.Current.MainWindow).ContentHolder.Children.Remove(NavigationController.currentView);
                
                if ( viewModel != null )
                    view.DataContext = viewModel;

                ((MainWindow)App.Current.MainWindow).ContentHolder.Children.Add(view);
                NavigationController.currentView = view;
            }
        }

        #endregion
    }
}
