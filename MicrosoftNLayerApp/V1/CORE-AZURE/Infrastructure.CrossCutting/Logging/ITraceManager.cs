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

namespace Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.Logging
{
    /// <summary>
    /// Trace manager contract for trace instrumentation
    /// </summary>
	public interface ITraceManager
	{
        /// <summary>
        /// Start logical operation in trace repository
        /// </summary>
        /// <param name="operationName"></param>
        void TraceStartLogicalOperation(string operationName);

        /// <summary>
        /// Stop actual logical operation in trace repository
        /// </summary>
        void TraceStopLogicalOperation();

        /// <summary>
        /// Send "start" flag to trace repository
        /// </summary>
        void TraceStart();

        /// <summary>
        /// Send "stop" flag to trace repository
        /// </summary>
        void TraceStop();

        /// <summary>
        /// Trace information message to trace repository
        /// <param name="message">Information message to trace</param>
        /// </summary>
        void TraceInfo(string message);

        /// <summary>
        /// Trace warning message to trace repository
        /// </summary>
        /// <param name="message">Warning message to trace</param>
        void TraceWarning(string message);

        /// <summary>
        /// Trace error message to trace repository
        /// </summary>
        /// <param name="message">Error message to trace</param>
        void TraceError(string message);

        /// <summary>
        /// Trace critical message to trace repository
        /// </summary>
        /// <param name="message">Critical message to trace</param>
        void TraceCritical(string message);
	}
}
