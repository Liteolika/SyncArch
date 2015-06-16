using MassTransit;
using SysArch.Application.Messages;
using SysArch.Application.Services;
using SysArch.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
            ISyncSchedulerCalculator calculator = new SyncSchedulerCalculator();

            _bus.SubscribeHandler<UserSyncServiceExecuted>(x =>
            {
                Console.WriteLine("UserSyncServiceExecuted: {0}", x.EventId);
            });

            _bus.SubscribeHandler<FileSyncServiceExecuted>(x =>
            {
                Console.WriteLine("FileSyncServiceExecuted: {0}", x.EventId);
            });

            _scheduler.Start(calculator);

            _bus.Publish(
                new SysArch.Domain.PortalUserContext.Commands.CreatePortalUser() { 
                    UserId = Guid.NewGuid(), UserName = "petcar", EmailAddress = "peter@liteolika.se" });

            //Thread.Sleep(10000);

            _bus.Publish(
                new SysArch.Domain.PortalUserContext.Commands.CreatePortalUser()
                {
                    UserId = Guid.NewGuid(),
                    UserName = "petcar",
                    EmailAddress = "peter@liteolika.se"
                });

            //Thread.Sleep(10000);

            Guid theId = Guid.NewGuid();

            _bus.Publish(
                new SysArch.Domain.PortalUserContext.Commands.CreatePortalUser()
                {
                    UserId = theId,
                    UserName = "petcar",
                    EmailAddress = "peter@liteolika.se"
                });

            Thread.Sleep(2000);

            _bus.Publish(new SysArch.Domain.PortalUserContext.Commands.UpdatePortalUser()
                {
                    UserId = theId,
                    EmailAddress = "peter.carlsson@se.fujitsu.com",
                    UserName = "swepcc"
                });

        }

        public void Stop()
        {
            _scheduler.Stop();
        }
    }
}
