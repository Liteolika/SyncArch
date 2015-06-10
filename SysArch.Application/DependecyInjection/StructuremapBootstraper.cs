using StructureMap;
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
            });

            return container;
        }

    }
}
