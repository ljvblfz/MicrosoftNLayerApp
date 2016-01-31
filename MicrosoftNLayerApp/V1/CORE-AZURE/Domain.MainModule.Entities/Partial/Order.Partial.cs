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
    /// Orde domain entity
    /// </summary>
    public partial class Order
    {
        /// <summary>
        /// Get number of items in this order
        /// For each OrderDetail  sum amount of items 
        /// </summary>
        /// <returns>Number of items</returns>
        public int GetNumberOfItems()
        {
            int? numberOfItems = 0;

            if (this.OrderDetails != null)
                numberOfItems = this.OrderDetails.Sum(detail=>detail.Amount);


            return numberOfItems??0;
        }
    }
}
