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
                cfg.For<IServiceBus>().Singleton().Use("ServiceBus", CreateServiceBus);

                cfg.Scan(s =>
                {
                    s.WithDefaultConventions();
                    s.TheCallingAssembly();
                    s.AssembliesFromApplicationBaseDirectory();
                    s.ConnectImplementationsToTypesClosing(typeof(ICommandHandler<>));
                });

            });

            var bus = container.GetInstance<IServiceBus>();
            bus.LoadHandlersFromContainer(container, typeof(ICommandHandler<>));
            container.Inject(bus);

            return container;
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
