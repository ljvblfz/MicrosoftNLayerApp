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
using System.Collections.Generic;
using System.Linq;
using Microsoft.Samples.NLayerApp.Domain.MainModule.Entities;

namespace Microsoft.Samples.NLayerApp.Presentation.Web.MVC.Client.ViewModels
{
    /// <summary>
    /// A ViewModel representing a paged list of bank transfers.
    /// </summary>
    public class TransfersListViewModel : IPagedView
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TransfersListViewModel"/> class.
        /// </summary>
        /// <param name="transfers">The bank transferences.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        public TransfersListViewModel(IEnumerable<BankTransfer> transfers, int page, int pageSize)
        {
            _transfers = transfers;
            _page = page;
            _pageSize = pageSize;
        }

        #endregion

        #region Members

        /// <summary>
        /// The bank transfers of the current page.
        /// </summary>
        private IEnumerable<BankTransfer> _transfers;
        /// <summary>
        /// The size of the current page.
        /// </summary>
        private int _pageSize;
        /// <summary>
        /// The current page.
        /// </summary>
        private int _page;

        /// <summary>
        /// Gets a value indicating whether this instance has previous page.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has previous page; otherwise, <c>false</c>.
        /// </value>
        public bool HasPreviousPage
        {
            get
            {
                return _page > 0;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has next page.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has next page; otherwise, <c>false</c>.
        /// </value>
        public bool HasNextPage
        {
            get
            {
                return _transfers.Count() >= _pageSize;
            }
        }

        /// <summary>
        /// Gets the previous page number.
        /// </summary>
        /// <value>The previous page number.</value>
        public int PreviousPage
        {
            get
            {
                return _page - 1;
            }
        }

        /// <summary>
        /// Gets the next page number.
        /// </summary>
        /// <value>The next page number.</value>
        public int NextPage
        {
            get
            {
                return _page + 1;
            }
        }

        /// <summary>
        /// Gets the page items.
        /// </summary>
        /// <value>The page items.</value>
        public IEnumerable PageItems
        {
            get { return _transfers; }
        }

        /// <summary>
        /// Gets the size of the page.
        /// </summary>
        /// <value>The size of the page.</value>
        public int PageSize
        {
            get { return _pageSize; }
        }

        #endregion

    }
}