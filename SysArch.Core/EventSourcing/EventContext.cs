using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysArch.Core.EventSourcing
{
    public class EventContext
    {
        public object Event { get; set; }
        public int EventNumber { get; set; }
        public IDictionary<string, object> Headers { get; set; }
    }
}
