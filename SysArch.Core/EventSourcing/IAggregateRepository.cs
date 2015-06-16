using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysArch.Core.EventSourcing
{
    public interface IAggregateRepository<TId>
    {
        T GetAggregate<T>(TId id) where T : AggregateRoot, new();
        //void Store(TId id, AggregateRoot aggregateRoot, Action<IDictionary<string, object>> applyHeaders);
        void Store(TId id, AggregateRoot aggregateRoot);
        IEnumerable<object> GetEvents(TId id);
        //void RegisterDispatcher(IEventDispatcher<TId> eventDispatcher);
    }
}
