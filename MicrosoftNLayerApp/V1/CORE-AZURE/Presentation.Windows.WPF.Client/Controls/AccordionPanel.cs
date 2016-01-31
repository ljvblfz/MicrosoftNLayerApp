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
using System.Windows;
using System.Windows.Controls;

namespace Microsoft.Samples.NLayerApp.Presentation.Windows.WPF.Client.Controls
{
    /// <summary>
    /// Accordion panel for use in Windows Presentation Foundation
    /// </summary>
    public class AccordionPanel : StackPanel
    {
        #region Properties

        public bool IsAllCollapsed
        {
            get { return (bool)GetValue(IsAllCollapsedProperty); }
            set { SetValue(IsAllCollapsedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllCollapsed.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllCollapsedProperty =
            DependencyProperty.Register("IsAllCollapsed", typeof(bool), typeof(AccordionPanel), new UIPropertyMetadata(false, IsAllCollapsedChanged));

        #endregion

        #region Constructor

        public AccordionPanel()
        {
            this.AddHandler(Expander.ExpandedEvent, new RoutedEventHandler(ExpanderExpanded));
        }

        #endregion

        #region Private Methods

        private void ExpanderExpanded(object sender, RoutedEventArgs e)
        {
            if (this.Children.Contains((Expander)e.OriginalSource))
            {
                foreach (var item in this.Children)
                {
                    if (item is Expander)
                    {
                        if (!object.ReferenceEquals(item, e.OriginalSource))
                        {
                            ((Expander)item).IsExpanded = false;
                        }
                    }
                }
            }

            e.Handled = true;
        }

        [Category("Common Properties")]
        private static void IsAllCollapsedChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (((bool)e.NewValue) == true)
            {
                AccordionPanel control = (AccordionPanel)sender;
                foreach (var item in control.Children)
                {
                    if (item is Expander)
                    {
                        ((Expander)item).IsExpanded = false;
                    }
                }
            }
        }

        #endregion
    }
}
