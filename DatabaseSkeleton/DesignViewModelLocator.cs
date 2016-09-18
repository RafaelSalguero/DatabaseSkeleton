using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Moq;
using Persistence;
using SimpleInjector;

namespace DatabaseSkeleton
{
    /// <summary>
    /// Locates and solve view models on design time
    /// </summary>
    public class DesignViewModelLocator : System.Windows.Markup.MarkupExtension
    {
        private static readonly Container container;
        static DesignViewModelLocator()
        {
            if (!Execute.InDesignMode) return;

            AssemblySource.Instance.Clear();
            AssemblySource.Instance.Add(typeof(DesignViewModelLocator).Assembly);
            container = new Container();

            IoC.GetInstance = (type, key) => container.GetInstance(type);
            IoC.GetAllInstances = (service) => ContainerAdapter.GetAllInstances(container, service);
            IoC.BuildUp = (instance) => ContainerAdapter.BuildUp(container, instance);

            container.RegisterSingleton<IEventAggregator, EventAggregator>();

            Configure();
        }

        /// <summary>
        /// Configure design time dependencies
        /// </summary>
        static void Configure()
        {
            var DatabaseId = Guid.NewGuid();

            //Add fake data:
            using(var C = Tonic.DbContextMock.Persistent<Db>(DatabaseId))
            {
                C.Customer.Add(new Persistence.Models.Customer { Id = 1, Name = "Rafael Salguero", Email = "rafaelsalgueroiturrios@gmail.com" });
                C.Customer.Add(new Persistence.Models.Customer { Id = 2, Name = "Jose Luis", Email = "JoseLuis@gmail.com" });
            }

            container.RegisterSingleton<Func<Db>>(() => Tonic.DbContextMock.Persistent<Db>(DatabaseId));
            container.RegisterSingleton(new Mock<IWindowManager>().Object);
        }

        /// <summary>
        /// Create a new view model locator for a given view model
        /// </summary>
        /// <param name="ViewModelType"></param>
        public DesignViewModelLocator(Type ViewModelType)
        {
            this.ViewModelType = ViewModelType;
        }
        readonly Type ViewModelType;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return container.GetInstance(ViewModelType);
        }
    }
}
