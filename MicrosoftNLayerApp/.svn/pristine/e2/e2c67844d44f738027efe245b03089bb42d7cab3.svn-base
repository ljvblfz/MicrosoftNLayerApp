using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Samples.NLayerApp.Domain.Core.Entities;

namespace Microsoft.Samples.NLayerApp.Infrastructure.Data.Core.Tests.StubAndMoles
{
    public class Product
        :IObjectWithChangeTracker
    {
        #region Properties

        public int ProductId { get; set; }

        public string Description { get; set; }

        #endregion

        #region IObjectWithChangeTracker Members

        ObjectChangeTracker changeTracker;
        public ObjectChangeTracker ChangeTracker
        {
            get { return changeTracker; }
        }

        #endregion
    }
}
