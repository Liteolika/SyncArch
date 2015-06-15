using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysArch.Core.EventSourcing
{
    public abstract class AggregateRoot
    {

        private readonly IList<object> _uncommittedEvents = new List<object>();

        public void Raise(object evt)
        {
            this.Apply(evt);
            _uncommittedEvents.Add(evt);
        }

        public IEnumerable<object> GetUncommittedEvents()
        {
            foreach (var e in _uncommittedEvents)
                yield return e;
            _uncommittedEvents.Clear();
        }

        
    }
}
