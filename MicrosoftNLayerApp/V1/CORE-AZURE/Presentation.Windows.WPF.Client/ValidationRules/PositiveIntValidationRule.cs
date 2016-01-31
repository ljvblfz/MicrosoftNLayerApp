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
using System.Windows.Controls;

namespace Microsoft.Samples.NLayerApp.Presentation.Windows.WPF.Client.ValidationRules
{
    /// <summary>
    /// Custom validator for check positive integers
    /// </summary>
    public class PositiveIntValidationRule : ValidationRule
    {
        /// <summary>
        /// <see cref="System.Windows.Control.ValidationRule"/>
        /// </summary>
        /// <param name="value"><see cref="System.Windows.Control.ValidationRule"/></param>
        /// <param name="cultureInfo"><see cref="System.Windows.Control.ValidationRule"/></param>
        /// <returns><see cref="System.Windows.Control.ValidationRule"/></returns>
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            string strLong = value.ToString();
            int newInt = -1;

            if (!int.TryParse(strLong, out newInt))
                return new ValidationResult(false, "El valor debe ser un número mayor que cero");

            if (newInt < 0)
                return new ValidationResult(false, "El valor debe ser un número mayor que cero");

            return new ValidationResult(true, null);
        }
    }
}
