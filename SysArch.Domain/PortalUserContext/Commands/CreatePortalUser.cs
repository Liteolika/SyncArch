using SysArch.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysArch.Domain.PortalUserContext.Commands
{
    public class CreatePortalUser : Command
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }

    }
}
