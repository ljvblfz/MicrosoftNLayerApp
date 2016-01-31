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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Microsoft.Samples.NLayerApp.Infrastructure.Data.Core.Extensions
{
    /// <summary>
    /// Expression extensions methods for helping coding
    /// </summary>
    public static class ExpressionExtensions
    {
        /// <summary>
        /// RemoveConvert extension method. This method remove all "Convert" elements in
        /// a expression
        /// </summary>
        /// <param name="expression">The expression to remove convert</param>
        /// <returns>Expression with removed "Convert"</returns>
        public static Expression RemoveConvert(this Expression expression)
        {
            while ((expression != null) && ((expression.NodeType == ExpressionType.Convert) || (expression.NodeType == ExpressionType.ConvertChecked)))
            {
                expression = ((UnaryExpression)expression).Operand.RemoveConvert();
            }
            return expression;
        }
    }
}
