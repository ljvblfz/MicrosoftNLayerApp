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
        static List<Product> produtList;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor for this unit of work stub.
        /// This constructor prepare stub with fake contents
        /// </summary>
        public UnitOfWorkStub()
        {
            PrepareStubs();
        }

        #endregion

        #region Private Methods

        void PrepareStubs()
        {
            PrepareEntitySet();

            PrepareProductSet();
        }

        void PrepareEntitySet()
        {
            //prepare fake object set for Entity

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

        void PrepareProductSet()
        {
            //Prepare fake object set for Producto

            produtList = new List<Product>()
            {
                new Product(){ProductId = 1, Description = "Description1"},
                new Software(){ProductId = 2, Description = "Description2",LicenseCode="AABB1"},
                new Software(){ProductId = 3, Description = "Description3",LicenseCode="AABB2"},
            };
            MemorySet<Product> productObjectSet = new MemorySet<Product>(produtList);

            this.CreateSet<Product>(() => productObjectSet as IObjectSet<Product>);

            this.RegisterChangesTEntity<Product>((item) =>
            {
                int index = produtList.IndexOf(item);
                if (index != -1)
                    produtList[index] = item;
                else
                    produtList.Add(item);
            });
        }

        #endregion
    }
}
