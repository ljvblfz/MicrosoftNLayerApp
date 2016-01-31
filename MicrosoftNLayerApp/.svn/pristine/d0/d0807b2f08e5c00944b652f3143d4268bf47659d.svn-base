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
using System.Windows.Data;

namespace Microsoft.Samples.NLayerApp.Presentation.Windows.WPF.Client.Converters
{
    /// <summary>
    /// Bool to opposite bool converter, intenteded
    /// to be use in Windows Presentation Foundation 
    /// </summary>
    public class BoolToOppositeBoolConverter: IValueConverter
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
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (targetType == typeof(bool))
                return !(bool)value;

            if (targetType == typeof(Nullable<bool>))
                return !(Nullable<bool>)value;

            return value;
        }

        /// <summary>
        /// <see cref="System.Windows.Data.IvalueConverter"/>
        /// </summary>
        /// <param name="value"><see cref="System.Windows.Data.IvalueConverter"/></param>
        /// <param name="targetType"><see cref="System.Windows.Data.IvalueConverter"/></param>
        /// <param name="parameter"><see cref="System.Windows.Data.IvalueConverter"/></param>
        /// <param name="culture"><see cref="System.Windows.Data.IvalueConverter"/></param>
        /// <returns><see cref="System.Windows.Data.IvalueConverter"/></returns>
        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (targetType == typeof(bool))
                return !(bool)value;

            if (targetType == typeof(Nullable<bool>))
                return !(Nullable<bool>)value;

            return value;
        }

        #endregion
    }
}
