using SysArch.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysArch.Domain.PortalUserContext.Events
{
    public class EmailAddressUpdated : Event
    {
        public Guid UserId { get; set; }

        public string EmailAddress { get; set; }
    }
}
