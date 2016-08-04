namespace DatabaseSkeleton
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Caliburn.Micro;
    using Persistence;
    using SimpleInjector;
    public class AppBootstrapper : BootstrapperBase
    {
        Container container;

        public AppBootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            //Check database:
            Db.ConfigureMigrations();
            using (var C = new Db())
            {
                var n = C.Client.Count();
                n = n;
            }

            container = new Container();

            container.RegisterSingleton<IWindowManager, WindowManager>();
            container.RegisterSingleton<IEventAggregator, EventAggregator>();
            container.Register<IShell, ShellViewModel>();
            container.RegisterSingleton<Func<Db>>(() => new Db());

        }

        protected override object GetInstance(Type service, string key)
        {
            var instance = container.GetInstance(service);
            if (instance != null)
                return instance;

            throw new InvalidOperationException("Could not locate any instances.");
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            IServiceProvider provider = container;
            Type collectionType = typeof(IEnumerable<>).MakeGenericType(service);
            var services = (IEnumerable<object>)provider.GetService(collectionType);
            return services ?? Enumerable.Empty<object>();
        }

        protected override void BuildUp(object instance)
        {
            var registration = container.GetRegistration(instance.GetType(), true);
            registration.Registration.InitializeInstance(instance);
        }

        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e)
        {
            DisplayRootViewFor<IShell>();
        }
    }
}