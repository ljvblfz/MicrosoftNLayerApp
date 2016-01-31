using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.IoC.Resources;
using System.Configuration;
using Microsoft.Samples.NLayerApp.Infrastructure.Data.MainModule.UnitOfWork;
using Microsoft.Samples.NLayerApp.Infrastructure.Data.MainModule.Mock;
using Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.IoC.Unity.LifetimeManagers;
using Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.NetFramework.Logging;
using Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.Logging;
using Microsoft.Samples.NLayerApp.Domain.MainModule.BankAccounts;
using Microsoft.Samples.NLayerApp.Domain.MainModule.Products;
using Microsoft.Samples.NLayerApp.Application.MainModule.CustomersManagement;
using Microsoft.Samples.NLayerApp.Application.MainModule.SalesManagement;
using Microsoft.Samples.NLayerApp.Application.MainModule.BankingManagement;
using Microsoft.Samples.NLayerApp.Infrastructure.Data.MainModule.Repositories;
using Microsoft.Samples.NLayerApp.Domain.MainModule.Orders;
using Microsoft.Samples.NLayerApp.Domain.MainModule.Customers;
using Microsoft.Samples.NLayerApp.Domain.MainModule.Countries;

namespace Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.IoC.Unity
{
    /// <summary>
    /// Implemented container in Microsoft Practices Unity
    /// </summary>
    sealed class IoCUnityContainer
        :IContainer
    {
        #region Members

        IDictionary<string, IUnityContainer> _ContainersDictionary;


        #endregion

        #region Constructor

        /// <summary>
        /// Create a new instance of IoCUnitContainer
        /// </summary>
        public IoCUnityContainer()
        {
            _ContainersDictionary = new Dictionary<string, IUnityContainer>();

                //Create root container
            IUnityContainer rootContainer = new UnityContainer();
            _ContainersDictionary.Add("RootContext", rootContainer);

                //Create container for real context, child of root container
            IUnityContainer realAppContainer = rootContainer.CreateChildContainer();
            _ContainersDictionary.Add("RealAppContext", realAppContainer);

                //Create container for testing, child of root container
            IUnityContainer fakeAppContainer = rootContainer.CreateChildContainer();
            _ContainersDictionary.Add("FakeAppContext", fakeAppContainer);

           
            ConfigureRootContainer(rootContainer);
            ConfigureRealContainer(realAppContainer);

            //(CDLTLL) Commented - Probable Dependecies Errors in Windows Azure Cloud (It works in Local Azure-Fabric)
            // At the moment it is ok, as Moles and fake container is not needed in Azure Production
            //ConfigureFakeContainer(fakeAppContainer);
            
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Configure root container.Register types and life time managers for unity builder process
        /// </summary>
        /// <param name="container">Container to configure</param>
        void ConfigureRootContainer(IUnityContainer container)
        {
            // Take into account that Types and Mappings registration could be also done using the UNITY XML configuration
            //But we prefer doing it here (C# code) because we'll catch errors at compiling time instead execution time, 
            //if any type has been written wrong.

            //Register Repositories mappings
            container.RegisterType<IProductRepository, ProductRepository>(new TransientLifetimeManager());
            container.RegisterType<IOrderRepository, OrderRepository>(new TransientLifetimeManager());
            container.RegisterType<IBankAccountRepository, BankAccountRepository>(new TransientLifetimeManager());
            container.RegisterType<ICustomerRepository, CustomerRepository>(new TransientLifetimeManager());
            container.RegisterType<ICountryRepository, CountryRepository>(new TransientLifetimeManager());

            //Register application services mappings

            container.RegisterType<ISalesManagementService, SalesManagementService>(new TransientLifetimeManager());
            container.RegisterType<ICustomerManagementService, CustomerManagementService>(new TransientLifetimeManager());
            container.RegisterType<IBankingManagementService, BankingManagementService>(new TransientLifetimeManager());
            
            //Register domain services mappings
            container.RegisterType<IBankTransferDomainService, BankTransferDomainService>(new TransientLifetimeManager());
            

            //Register crosscuting mappings
            container.RegisterType<ITraceManager, TraceManager>(new TransientLifetimeManager());

        }

        /// <summary>
        /// Configure real container. Register types and life time managers for unity builder process
        /// </summary>
        /// <param name="container">Container to configure</param>
        void ConfigureRealContainer(IUnityContainer container)
        {
            container.RegisterType<IMainModuleUnitOfWork, MainModuleUnitOfWork>(new PerExecutionContextLifetimeManager(),new InjectionConstructor());
        }

        /// <summary>
        /// Configure fake container.Register types and life time managers for unity builder process
        /// </summary>
        /// <param name="container">Container to configure</param>
        void ConfigureFakeContainer(IUnityContainer container)
        {
            //Note: Generic register type method cannot be used here, 
            //MainModuleFakeContext cannot have implicit conversion to IMainModuleContext

            container.RegisterType(typeof(IMainModuleUnitOfWork), typeof(FakeMainModuleUnitOfWork), new PerExecutionContextLifetimeManager());
        }

        #endregion

        #region IServiceFactory Members

        /// <summary>
        /// <see cref="M:Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.IoC.IContainer.Resolve{TService}"/>
        /// </summary>
        /// <typeparam name="TService"><see cref="M:Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.IoC.IContainer.Resolve{TService}"/></typeparam>
        /// <returns><see cref="M:Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.IoC.IContainer.Resolve{TService}"/></returns>
        public TService Resolve<TService>()
        {
            //We use the default container specified in AppSettings
            string containerName = ConfigurationManager.AppSettings["defaultIoCContainer"];

            if (String.IsNullOrEmpty(containerName)
                ||
                String.IsNullOrWhiteSpace(containerName))
            {
                throw new ArgumentNullException(Messages.exception_DefaultIOCSettings);
            }

            if (!_ContainersDictionary.ContainsKey(containerName))
                throw new InvalidOperationException(Messages.exception_ContainerNotFound);

            IUnityContainer container = _ContainersDictionary[containerName];

            return container.Resolve<TService>();
        }
        /// <summary>
        /// <see cref="M:Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.IoC.IContainer.Resolve"/>
        /// </summary>
        /// <param name="type"><see cref="M:Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.IoC.IContainer.Resolve"/></param>
        /// <returns><see cref="M:Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.IoC.IContainer.Resolve"/></returns>
        public object Resolve(Type type)
        {
            //We use the default container specified in AppSettings
            string containerName = ConfigurationManager.AppSettings["defaultIoCContainer"];

            if (String.IsNullOrEmpty(containerName)
                ||
                String.IsNullOrWhiteSpace(containerName))
            {
                throw new ArgumentNullException(Messages.exception_DefaultIOCSettings);
            }

            if (!_ContainersDictionary.ContainsKey(containerName))
                throw new InvalidOperationException(Messages.exception_ContainerNotFound);

            IUnityContainer container = _ContainersDictionary[containerName];

            return container.Resolve(type, null);
        }

        /// <summary>
        /// <see cref="M:Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.IoC.IContainer.RegisterType"/>
        /// </summary>
        /// <param name="type"><see cref="M:Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.IoC.IContainer.RegisterType"/></param>
        public void RegisterType(Type type)
        {
            IUnityContainer container = this._ContainersDictionary["RootContext"];

            if (container != null)
                container.RegisterType(type, new TransientLifetimeManager());
        }

        #endregion
    }
}
