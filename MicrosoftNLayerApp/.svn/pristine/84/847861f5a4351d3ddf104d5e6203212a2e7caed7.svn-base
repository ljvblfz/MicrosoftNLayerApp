using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace Microsoft.Samples.NLayerApp.Presentation.Web.MVC.Client
{
    public class WebRole : RoleEntryPoint
    {
        public override bool OnStart()
        {
            //(CDLTLL) (OLD-WA SDK 1.2) 
            //DiagnosticMonitor.Start("DiagnosticsConnectionString");

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.
            
            //(CDLTLL) (OLD-WA SDK 1.2) 
            //RoleEnvironment.Changing += RoleEnvironmentChanging;

            return base.OnStart();
        }

        //(CDLTLL) (OLD-WA SDK 1.2) 
        //
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
