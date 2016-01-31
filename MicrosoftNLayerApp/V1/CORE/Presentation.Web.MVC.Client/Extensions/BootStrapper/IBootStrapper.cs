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

namespace Microsoft.Samples.NLayerApp.Presentation.Web.MVC.Client.Extensions.BootStrapper
{
    /// <summary>
    /// Application boot strapper contract. 
    /// </summary>
    public interface IBootStrapper
    {
        /// <summary>
        /// Initializes application
        /// <remarks>
        /// Registry a controller factory
        /// binders
        /// extensions 
        /// ...
        /// </remarks>
        /// </summary>
        void Boot();
    }
}
