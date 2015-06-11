using MassTransit;
using SysArch.Application.Messages;
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
        private IServiceBus _bus;

        public App(ISyncScheduler scheduler, IServiceBus bus)
        {
            _scheduler = scheduler;
            _bus = bus;
        }

        public void Start()
        {
            //Console.WriteLine("Service Start Command");

            ISyncSchedulerCalculator calculator = new SyncSchedulerCalculator();

            _bus.SubscribeHandler<UserSyncServiceExecuted>(x => {
                Console.WriteLine("UserSyncServiceExecuted: {0}", x.EventId);
            });

            _bus.SubscribeHandler<FileSyncServiceExecuted>(x =>
            {
                Console.WriteLine("FileSyncServiceExecuted: {0}", x.EventId);
            });

            _scheduler.Start(calculator);

        }

        public void Stop()
        {
            //Console.WriteLine("Service Stop Command");
            _scheduler.Stop();
        }
    }
}
