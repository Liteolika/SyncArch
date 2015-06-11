using MassTransit;
using SysArch.Application.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysArch.Application.Services
{
    public class FileSyncService : ICommandHandler<ExecuteFileSyncService>
    {

        private readonly IServiceBus _bus;
        public readonly Guid _id;

        public FileSyncService(IServiceBus bus)
        {
            this._id = Guid.NewGuid();
            _bus = bus;
        }

        public void Handle(ExecuteFileSyncService cmd)
        {
            _bus.Publish(new FileSyncServiceExecuted());
        }

    }
}
