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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Samples.NLayerApp.Domain.MainModule.Entities
{
    /// <summary>
    /// A Product Entity in domain main module 
    /// </summary>
    public partial class Product
    {
        /// <summary>
        /// Check if this product exist in stock
        /// </summary>
        /// <returns>True if exist stock of this product</returns>
        public virtual bool ExistStock()
        {
            return this.AmountInStock > 0;
        }
    }
}
