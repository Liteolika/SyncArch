using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysArch.Application.Messages
{
    public abstract class Event
    {
        public Guid EventId { get; private set; }

        public Event()
        {
            this.EventId = Guid.NewGuid();
        }
    }
}
