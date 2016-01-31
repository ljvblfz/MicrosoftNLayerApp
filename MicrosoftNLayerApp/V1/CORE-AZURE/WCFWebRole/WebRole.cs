using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace WCFWebRole
{
    public class WebRole : RoleEntryPoint
    {
        public override bool OnStart()
        {
            //(CDLTLL) (OLD-WA SDK 1.2) 
            //DiagnosticMonitor.Start("DiagnosticsConnectionString");
            
            // (New SDK 1.3) To enable the AzureLocalStorageTraceListner for TRACES, uncomment relevent section in the web.config  
            DiagnosticMonitorConfiguration diagnosticConfig = DiagnosticMonitor.GetDefaultInitialConfiguration();
            diagnosticConfig.Directories.ScheduledTransferPeriod = TimeSpan.FromMinutes(1);
            diagnosticConfig.Directories.DataSources.Add(AzureLocalStorageTraceListener.GetLogDirectory());

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.
            //(CDLTLL) (OLD-WA SDK 1.2) 
            //RoleEnvironment.Changing += RoleEnvironmentChanging;

            return base.OnStart();
        }

        //(CDLTLL) (OLD-WA SDK 1.2) 
        //private void RoleEnvironmentChanging(object sender, RoleEnvironmentChangingEventArgs e)
        //{
        //    // If a configuration setting is changing
        //    if (e.Changes.Any(change => change is RoleEnvironmentConfigurationSettingChange))
        //    {
        //        // Set e.Cancel to true to restart this role instance
        //        e.Cancel = true;
        //    }
        //}
    }
}
