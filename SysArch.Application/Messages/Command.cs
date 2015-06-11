using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysArch.Application.Messages
{
    public abstract class Command
    {
        public Guid CommandId { get; private set; }

        public Command()
        {
            this.CommandId = Guid.NewGuid();
        }
    }
}
