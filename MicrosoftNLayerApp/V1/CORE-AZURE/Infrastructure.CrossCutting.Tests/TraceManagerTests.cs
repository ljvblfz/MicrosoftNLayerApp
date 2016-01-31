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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.Logging;
using Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.IoC;

namespace Infrastructure.CrossCutting.NetFramework.Tests
{

    [TestClass]
    public partial class TraceManagerTest
    {
        [TestMethod()]
        public void TraceStartLogicalOperation_Invoke_Test()
        {
            //Arrange and Act            
            ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();
            traceManager.TraceStartLogicalOperation("Operation Name");
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TraceStartLogicalOperation_Invoke_NullOperationNameThrowNewArgumentNullException()
        {
            //Arrange and Act
            ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();
            traceManager.TraceStartLogicalOperation(null);
        }
        [TestMethod()]
        public void TraceStopLogicalOperation_Invoke_Test()
        {
            //Arrange and Act
            ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();
            traceManager.TraceStopLogicalOperation();
        }
        [TestMethod()]
        public void TraceStart_Invoke_Test()
        {
            //Arrange and act
            ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();
            traceManager.TraceStart();
        }
        [TestMethod()]
        public void TraceStop_Invoke_Test()
        {
            //Arrange and Act
            ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();
            traceManager.TraceStop();
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TraceInfo_Invoke_NullMessageThrowNewArgumentNullException_Test()
        {
            //Arrange and act
            ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();
            traceManager.TraceInfo(null);
        }
        [TestMethod()]
        public void TraceInfo_Invoke_Test()
        {
            //Arrange and Act
            ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();
            traceManager.TraceInfo("Message");
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TraceWarning_Invoke_NullMessageThrowNewArgumentNullException_Test()
        {
            //Arrange and act
            ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();
            traceManager.TraceWarning(null);
        }
        [TestMethod()]
        public void TraceWarning_Invoke_Test()
        {
            //Arrange and Act
            ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();
            traceManager.TraceWarning("Message");
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TraceError_Invoke_NullMessageThrowNewArgumentNullException_Test()
        {
            //Arrange and act
            ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();
            traceManager.TraceError(null);
        }
        [TestMethod()]
        public void TraceError_Invoke_Test()
        {
            //Arrange and Act
            ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();
            traceManager.TraceError("Message");
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TraceCritical_Invoke_NullMessageThrowNewArgumentNullException_Test()
        {
            //Arrange and act
            ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();
            traceManager.TraceError(null);
        }
        [TestMethod()]
        public void TraceCritical_Invoke_Test()
        {
            //Arrange and Act
            ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();
            traceManager.TraceError("Message");
        }

    }
}
