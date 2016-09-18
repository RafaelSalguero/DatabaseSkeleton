using System;
using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using Persistence;
using SimpleInjector;

namespace DatabaseSkeleton
{

    public class AppBootstrapper : BootstrapperBase
    {
        Container container;

        public AppBootstrapper()
        {
            Initialize();
        }

        /// <summary>
        /// Configure the runtime dependencies
        /// </summary>
        protected override void Configure()
        {
            //Check database:
            Db.ConfigureMigrations();

            container = new Container();

            container.RegisterSingleton<IWindowManager, WindowManager>();
            container.RegisterSingleton<IEventAggregator, EventAggregator>();
            container.Register<IShell, ShellViewModel>();
            container.RegisterSingleton<Func<Db>>(() => new Db());

        }

        protected override object GetInstance(Type service, string key)
        {
            var instance = ContainerAdapter.GetInstance(container, service, key);
            if (instance != null)
                return instance;

            throw new InvalidOperationException("Could not locate any instances.");
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return ContainerAdapter.GetAllInstances(container, service);
        }

        protected override void BuildUp(object instance)
        {
            ContainerAdapter.BuildUp(container, instance);
        }

        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e)
        {
            DisplayRootViewFor<IShell>();
        }
    }

    /// <summary>
    /// Adapt SimpleInjector container methods to be compatible with Caliburn.Micro container specification
    /// </summary>
    public static class ContainerAdapter
    {
        /// <summary>
        /// SimpleInjector just ignores the key parameter
        /// </summary>
        /// <param name="container"></param>
        /// <param name="service"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object GetInstance(Container container, Type service, string key)
        {
            return container.GetInstance(service);
        }

        /// <summary>
        /// SimpleInjector returns error when the service type is not defined, but Caliburn.Micro instead expects an empty collection
        /// </summary>
        /// <param name="container"></param>
        /// <param name="service"></param>
        /// <returns></returns>
        public static IEnumerable<object> GetAllInstances(Container container, Type service)
        {
            IServiceProvider provider = container;
            Type collectionType = typeof(IEnumerable<>).MakeGenericType(service);
            var services = (IEnumerable<object>)provider.GetService(collectionType);
            return services ?? Enumerable.Empty<object>();
        }

        /// <summary>
        /// SimpleInjector doesn't have a BuildUp method, this methods adapts this
        /// </summary>
        /// <param name="container"></param>
        /// <param name="instance"></param>
        public static void BuildUp(Container container, object instance)
        {
        }
    }
}