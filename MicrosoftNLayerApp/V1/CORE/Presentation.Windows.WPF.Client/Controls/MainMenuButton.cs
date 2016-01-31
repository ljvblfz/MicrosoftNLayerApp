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
using System.Windows.Markup;

namespace Microsoft.Samples.NLayerApp.Presentation.Windows.WPF.Client.Controls
{
    /// <summary>
    /// Main Menu button control
    /// </summary>
    [ContentProperty("Image")]
    [DefaultProperty("Image")]
    class MainMenuButton : Button
    {

        #region Properties

        [Category("Common Properties")]
        public FrameworkElement Image
        {
            get { return (FrameworkElement)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Image.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("Image", typeof(FrameworkElement), typeof(MainMenuButton), new UIPropertyMetadata(null));

        #endregion
    }
}
