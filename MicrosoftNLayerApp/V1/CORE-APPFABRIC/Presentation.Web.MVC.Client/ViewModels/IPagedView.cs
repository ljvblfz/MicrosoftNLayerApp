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
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Microsoft.Samples.NLayerApp.Presentation.Web.MVC.Client.ViewModels
{

    /// <summary>
    /// Interface for Viewmodels of views that support pagging.
    /// </summary>
    public interface IPagedView
    {
        #region Members

        /// <summary>
        /// Gets a value indicating whether this instance has previous page.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has previous page; otherwise, <c>false</c>.
        /// </value>
        bool HasPreviousPage
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating whether this instance has next page.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has next page; otherwise, <c>false</c>.
        /// </value>
        bool HasNextPage
        {
            get;
        }

        /// <summary>
        /// Gets the previous page number.
        /// </summary>
        /// <value>The previous page number.</value>
        int PreviousPage
        {
            get;
        }

        /// <summary>
        /// Gets the next page number.
        /// </summary>
        /// <value>The next page number.</value>
        int NextPage
        {
            get;
        }

        /// <summary>
        /// Gets the page items.
        /// </summary>
        /// <value>The page items.</value>
        [UIHint("IEnumerable")]
        IEnumerable PageItems { get; }

        /// <summary>
        /// Gets the size of the page.
        /// </summary>
        /// <value>The size of the page.</value>
        int PageSize { get; }

        #endregion
    }
}
