using StructureMap;
using SysArch.Application;
using SysArch.Application.DependecyInjection;
using SysArch.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace SysArch.WinService
{
    class Program
    {
        static void Main(string[] args)
        {

            IContainer container = StructuremapBootstraper.Initialize();

            HostFactory.Run(x =>
            {
                x.Service<IService>(c =>
                {
                    c.ConstructUsing(() => container.GetInstance<IService>());
                    c.WhenStarted(s => s.Start());
                    c.WhenStopped(s => s.Stop());
                });

                x.RunAsLocalSystem();
                x.SetServiceName("SysArch.WinService");
                x.SetDisplayName("SysArch Windows Service");
                x.SetDescription("Main Service");
                
            });

            Console.ReadKey();

        }
    }
}
