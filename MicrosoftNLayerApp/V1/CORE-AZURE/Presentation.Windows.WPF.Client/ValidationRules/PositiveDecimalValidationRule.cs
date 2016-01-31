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
    /// Custom validation rule for validate positive decimal values
    /// </summary>
    public class PositiveDecimalValidationRule : ValidationRule
    {
        /// <summary>
        /// <see cref="System.Windows.Control.ValidationRule"/>
        /// </summary>
        /// <param name="value"><see cref="System.Windows.Control.ValidationRule"/></param>
        /// <param name="cultureInfo"><see cref="System.Windows.Control.ValidationRule"/></param>
        /// <returns><see cref="System.Windows.Control.ValidationRule"/></returns>
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            string strDecimal = value.ToString();
            decimal newDecimal = -1;

            if ( string.IsNullOrEmpty(strDecimal) )
                return new ValidationResult(false, "Este campo es obligatorio");

            if (!decimal.TryParse(strDecimal, out newDecimal))
                return new ValidationResult(false, "El valor debe ser un número mayor que cero");

            if (newDecimal <= 0)
                return new ValidationResult(false, "El valor debe ser un número mayor que cero");

            return new ValidationResult(true, null);
        }
    }
}
