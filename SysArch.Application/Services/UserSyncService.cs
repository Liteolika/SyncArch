using SysArch.Application.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysArch.Application.Services
{
    public class UserSyncService : ICommand<ExecuteUserSyncService>, ICommand<ExecuteFileSyncService>
    {



        public void Handle(ExecuteUserSyncService cmd)
        {
            throw new NotImplementedException();
        }

        public void Handle(ExecuteFileSyncService cmd)
        {
            throw new NotImplementedException();
        }
    }
}
