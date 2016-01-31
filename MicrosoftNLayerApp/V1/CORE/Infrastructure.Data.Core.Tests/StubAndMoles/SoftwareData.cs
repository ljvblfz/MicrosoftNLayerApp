using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Samples.NLayerApp.Infrastructure.Data.Core.Tests.StubAndMoles
{
    public class SoftwareData
    {
        public int IdSoftwareData { get; set; }
        public string Data { get; set; }
        public int IdSoftware { get; set; }
        public Software Software { get; set; }
    }
}
