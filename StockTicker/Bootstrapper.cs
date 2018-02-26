using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using Caliburn.Micro;
using StockTicker.ViewModels;
using StockTicker.Views;
using Autofac;
using StockTicker.Models;
using StockTicker.Interfaces;

namespace StockTicker
{
    public class Bootstrapper : BootstrapperBase
    {
        ILog _logger = new DebugLog(typeof(Bootstrapper));
        Autofac.IContainer _container;
        public Bootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<TickerFactory>().As<ITickerFactory>();
            builder.RegisterAssemblyTypes(AssemblySource.Instance.ToArray())
                .Where(type => type.Name.EndsWith("ViewModel"))
                .Where(type => type.GetInterface(typeof(INotifyPropertyChanged).Name) != null)
                .AsSelf().InstancePerDependency();
            _logger.Info("ViewModels bound");

            builder.RegisterAssemblyTypes(AssemblySource.Instance.ToArray())
                .Where(type => type.Name.EndsWith("View"))
                .AsSelf().InstancePerDependency();

            builder.Register<IWindowManager>(c => new WindowManager()).InstancePerLifetimeScope();
            builder.Register<IEventAggregator>(c => new EventAggregator()).InstancePerLifetimeScope();

            _container = builder.Build();
        }

        protected override object GetInstance(Type service, string key)
        {
            object obj = key == null ? _container.ResolveOptional(service) : _container.ResolveNamed(key, service);
            return obj;
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            var type = typeof(IEnumerable<>).MakeGenericType(service);
            return _container.Resolve(type) as IEnumerable<object>;
        }

        protected override void BuildUp(object instance)
        {
            _container.InjectProperties(instance);
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            var assemblies = base.SelectAssemblies().ToList();
            assemblies.Add(typeof(TickerViewModel).GetTypeInfo().Assembly);
            assemblies.Add(typeof(TickerView).GetTypeInfo().Assembly);

            return assemblies;
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<TickersViewModel>();
        }
    }
}
