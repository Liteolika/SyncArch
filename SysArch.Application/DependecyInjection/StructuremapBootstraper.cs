using MassTransit;
using StructureMap;
using StructureMap.Graph;
using SysArch.Application.Messages;
using SysArch.Application.Services;
using SysArch.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysArch.Application.DependecyInjection
{
    public static class StructuremapBootstraper
    {

        public static IContainer Initialize()
        {
            IContainer container = new Container();

            container.Configure(cfg =>
            {
                cfg.For<IService>().Use<App>();
                cfg.For<ISyncScheduler>().Use<Services.SyncScheduler>();
                cfg.For<ISyncSchedulerCalculator>().Use<Services.SyncSchedulerCalculator>();
                cfg.For<IServiceBus>().Use("ServiceBus", CreateServiceBus);

                cfg.Scan(s =>
                {
                    s.ConnectImplementationsToTypesClosing(typeof(ICommand<>));
                    s.WithDefaultConventions();
                    s.TheCallingAssembly();
                    s.AssembliesFromApplicationBaseDirectory();
                });

            });

            ConfigureSubscribers(container);

            return container;
        }

        private static void ConfigureSubscribers(IContainer container)
        {
            var bus = container.GetInstance<IServiceBus>();

            var commands = container.Model.PluginTypes
                .Where(p => 
                    p.PluginType.IsGenericType && 
                    p.PluginType.GetGenericTypeDefinition() == typeof(ICommand<>))
                    .Select(p => p.PluginType).ToArray();
            
            foreach (var cmd in commands)
            {
                var commandType = cmd.GetGenericArguments()[0];

                var handler = container.ForGenericType(typeof(ICommand<>))
                    .WithParameters(commandType)
                    .GetInstanceAs<object>();

                var methods = handler.GetType().GetMethods();
                var action = methods.First().GetParameters().Where(p => p.ParameterType == commandType).FirstOrDefault();

                var actionDelegate = Delegate.CreateDelegate(typeof(Action<>).MakeGenericType(commandType), action);
                    


                var asd = 1;
            }


            var a = 1;

        }

        private static IServiceBus CreateServiceBus(IContext ctx)
        {
            var bus = ServiceBusFactory.New(cfg =>
            {
                cfg.ReceiveFrom("loopback://localhost/sysarch-bus");
            });
            return bus;
        }


    }
}
