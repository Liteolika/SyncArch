using MassTransit;
using SysArch.Application.Messages;
using SysArch.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysArch.Application.Services
{
    public class UserSyncService : ICommandHandler<ExecuteUserSyncService>
        
    {

        private readonly IServiceBus _bus;
        public readonly Guid _id;

        public UserSyncService(IServiceBus bus)
        {
            this._id = Guid.NewGuid();
            _bus = bus;
        }

        public void Handle(ExecuteUserSyncService cmd)
        {
            _bus.Publish(new UserSyncServiceExecuted());
        }

        
    }
}
