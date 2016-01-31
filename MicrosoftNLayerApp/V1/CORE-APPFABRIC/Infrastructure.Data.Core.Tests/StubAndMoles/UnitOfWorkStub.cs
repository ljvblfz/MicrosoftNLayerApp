using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;

using Microsoft.Samples.NLayerApp.Infrastructure.Data.Core;
using Microsoft.Samples.NLayerApp.Infrastructure.Data.Core.Extensions;




namespace Microsoft.Samples.NLayerApp.Infrastructure.Data.Core.Tests.StubAndMoles
{
    /// <summary>
    /// UnitOfWork stub for test a GenericRepository base class
    /// </summary>
    public class UnitOfWorkStub
        : Microsoft.Samples.NLayerApp.Infrastructure.Data.Core.Moles.SIQueryableUnitOfWork
    {
        #region Members

        static List<Entity> entityList;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor for this unit of work stub.
        /// This constructor prepare stub with fake contents
        /// </summary>
        public UnitOfWorkStub()
        {
            PrepareStub();
        }

        #endregion

        #region Private Methods

        private void PrepareStub()
        {
            //List prepare fake object set

            entityList = new List<Entity>()
            {
                new Entity(){Id=1,Field="field 1"},
                new Entity(){Id=2,Field="field 2"},
                new SubEntity(){Id=3,Field="field 3"}
            };

            MemorySet<Entity> fakeObjectSet = entityList.ToMemorySet();

            //Assign this object set when CreateObjectSet<Entity> is executed!
            this.CreateSet<Entity>(() => fakeObjectSet as IObjectSet<Entity>);

            //prepare ApplyChanges stub 
            this.RegisterChangesTEntity<Entity>((item) =>
                                           {
                                               int index = entityList.IndexOf(item);
                                               if (index != -1)
                                                   entityList[index] = item;
                                               else
                                                   entityList.Add(item);
                                           });
        }

        #endregion
    }
}
