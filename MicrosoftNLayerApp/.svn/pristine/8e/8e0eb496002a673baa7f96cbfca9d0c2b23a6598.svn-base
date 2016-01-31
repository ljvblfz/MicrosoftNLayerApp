using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Samples.NLayerApp.Domain.Core.Entities;

namespace Microsoft.Samples.NLayerApp.Infrastructure.Data.Core.Tests.StubAndMoles
{
    /// <summary>
    /// Simple class for support GenericRepositoryTest fixtures
    /// </summary>
    class Entity : IObjectWithChangeTracker
    {
        #region Properties

        public int Id { get; set; }
        public string Field { get; set; }

        #endregion

        #region Constructor

        public Entity()
        {
            this.changeTracker = new ObjectChangeTracker();
        }
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
