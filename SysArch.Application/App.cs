using SysArch.Application.Services;
using SysArch.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysArch.Application
{
    public class App : IService
    {

        private ISyncScheduler _scheduler;

        public App(ISyncScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public void Start()
        {
            Console.WriteLine("Service Start Command");

            ISyncSchedulerCalculator calculator = new SyncSchedulerCalculator();

            _scheduler.Start(calculator);

            //throw new NotImplementedException();
        }

        public void Stop()
        {
            Console.WriteLine("Service Stop Command");
            _scheduler.Stop();

            //throw new NotImplementedException();
        }
    }
}
