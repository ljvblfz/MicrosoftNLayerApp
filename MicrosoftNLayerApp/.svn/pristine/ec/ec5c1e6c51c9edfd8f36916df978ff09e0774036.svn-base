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
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Microsoft.Samples.NLayerApp.Presentation.Windows.WPF.Client.Converters
{
    /// <summary>
    /// Binary-Image converter, intende to be use with convertes in Windows Presentaton Foundation
    /// </summary>
    class BinaryImageConverter
        :IValueConverter
    {
        #region IValueConverter Members

        /// <summary>
        /// <see cref="System.Windows.Data.IvalueConverter"/>
        /// </summary>
        /// <param name="value"><see cref="System.Windows.Data.IvalueConverter"/></param>
        /// <param name="targetType"><see cref="System.Windows.Data.IvalueConverter"/></param>
        /// <param name="parameter"><see cref="System.Windows.Data.IvalueConverter"/></param>
        /// <param name="culture"><see cref="System.Windows.Data.IvalueConverter"/></param>
        /// <returns><see cref="System.Windows.Data.IvalueConverter"/></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            byte[] buffer = value as byte[];
            if (buffer != null)
            {
                BitmapImage bmpImage = new BitmapImage();
                bmpImage.BeginInit();
                bmpImage.StreamSource = new MemoryStream(buffer);
                bmpImage.EndInit();

                return bmpImage;
            }
            else
            {
                return new BitmapImage(new Uri("/Images/Unknown.png", UriKind.RelativeOrAbsolute));
            }
        }

        /// <summary>
        /// <see cref="System.Windows.Data.IvalueConverter"/>
        /// </summary>
        /// <param name="value"><see cref="System.Windows.Data.IvalueConverter"/></param>
        /// <param name="targetType"><see cref="System.Windows.Data.IvalueConverter"/></param>
        /// <param name="parameter"><see cref="System.Windows.Data.IvalueConverter"/></param>
        /// <param name="culture"><see cref="System.Windows.Data.IvalueConverter"/></param>
        /// <returns><see cref="System.Windows.Data.IvalueConverter"/></returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //feature not implemented!
            throw new NotImplementedException();
        }

        #endregion
    }
}
