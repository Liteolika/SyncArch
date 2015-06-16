using SysArch.Core.EventSourcing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysArch.Domain.Tests
{
    public class MockRepository : IAggregateRepository<Guid>
    {
        private readonly Dictionary<Guid, List<object>> _store = new Dictionary<Guid, List<object>>();
        private readonly Dictionary<Guid, List<object>> _committedStore = new Dictionary<Guid, List<object>>();

        //private readonly IList<IEventDispatcher<Guid>> _eventDispatchers = new List<IEventDispatcher<Guid>>();

        public T GetAggregate<T>(Guid id) where T : AggregateRoot, new()
        {
            List<object> storedEvents;
            if (!_store.TryGetValue(id, out storedEvents))
            {
                return null;
            }

            return AggregateRootExtensions.Build<T>(storedEvents);
        }

        public void Store(Guid id, AggregateRoot aggregateRoot)
        {
            var events = aggregateRoot.GetUncommittedEvents();
            Store(id, events, _store);
            Store(id, events, _committedStore);
        }

        public void Store(Guid id, IEnumerable<object> events)
        {
            var evts = events.ToArray();
            Store(id, evts, _store);
            Store(id, evts, _committedStore);
        }

        private static void Store(Guid id, IEnumerable<object> events, Dictionary<Guid, List<object>> store)
        {
            List<object> storedEvents;
            if (!store.TryGetValue(id, out storedEvents))
            {
                storedEvents = new List<object>();
                store[id] = storedEvents;
            }
            storedEvents.AddRange(events);
        }

        public void ResetCommitted()
        {
            _committedStore.Clear();
        }

        public IEnumerable<object> GetEvents(Guid id)
        {
            List<object> storedEvents;
            if (!_store.TryGetValue(id, out storedEvents))
            {
                return null;
            }
            return storedEvents;
        }

        //public void RegisterDispatcher(IEventDispatcher<Guid> eventDispatcher)
        //{
        //    _eventDispatchers.Add(eventDispatcher);
        //}

        public IEnumerable<object> GetCommittedEvents(Guid id)
        {
            List<object> storedEvents;
            if (!_committedStore.TryGetValue(id, out storedEvents))
            {
                return Enumerable.Empty<object>();
            }
            return storedEvents;
        }

    }

}
