using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.IoC;
using Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.Logging;

//(CDLTLL) Not required if using UNITY
//using Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.NetFramework.Logging;

namespace WCFWebRole
{
    public class TestService : ITestService
    {
        public string SayHello()
        {
            System.Diagnostics.Trace.TraceInformation("Entramos en SayHello - INFO");

            //ITraceManager traceManager = new TraceManager();
            ITraceManager traceManager = IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();
            
            traceManager.TraceError("Init SayHello() using ITraceManager");

            System.Diagnostics.Trace.TraceInformation("AFTER exec. TraceManager with UNITY");

            return "Cheers my!!";
        }

        public void DoWork()
        {
        }
    }
}

