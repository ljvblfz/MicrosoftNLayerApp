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
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace Microsoft.Samples.NLayerApp.Presentation.Windows.WPF.Client.Controls
{
    /// <summary>
    /// Menu button icon class control
    /// </summary>
    public class MenuIconButton : ToggleButton
    {
        #region Properties

        [Category("Common Properties")]
        public String Caption
        {
            get { return (String)GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CaptionProperty =
            DependencyProperty.Register("Caption", typeof(String), typeof(MenuIconButton), new UIPropertyMetadata(String.Empty));

        #endregion
    }
}
