using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysArch.Views
{
    public class PortalUserDetailViewModel
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
    }
}
