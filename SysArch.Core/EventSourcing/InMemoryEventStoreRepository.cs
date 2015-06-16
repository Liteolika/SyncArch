using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysArch.Core.EventSourcing
{
    public class InMemoryEventStoreRepository : IAggregateRepository<Guid>
    {

        private readonly ConcurrentDictionary<Guid, List<EventContext>> _store = 
            new ConcurrentDictionary<Guid, List<EventContext>>();

        //private readonly ConcurrentBag<IEventDispatcher<Guid>> _eventDispatchers = 
        //    new ConcurrentBag<IEventDispatcher<Guid>>();

        public T GetAggregate<T>(Guid id) where T : AggregateRoot, new()
        {
            List<EventContext> storedEvents;

            if (!_store.TryGetValue(id, out storedEvents))
            {
                return null;
            }

            return AggregateRootExtensions.Build<T>(storedEvents.Select(ec => ec.Event));
        }

        public void Store(Guid id, AggregateRoot aggregateRoot)
        {
            Store(id, aggregateRoot.GetUncommittedEvents());
        }

        //public void Store(Guid id, AggregateRoot aggregateRoot, Action<IDictionary<string, object>> applyHeaders)
        //{
        //    Store(id, aggregateRoot.GetUncommittedEvents(), applyHeaders);
        //}

        public void Store(Guid id, IEnumerable events)
        {
            List<EventContext> storedEvents;
            if (!_store.TryGetValue(id, out storedEvents))
            {
                storedEvents = new List<EventContext>();
                _store[id] = storedEvents;
            }

            var lastIndex = storedEvents.Count;

            var eventContexts = events.OfType<object>().Select(e =>
            {
                var ec = new EventContext()
                {
                    Event = e,
                    EventNumber = lastIndex++,
                    Headers = new Dictionary<string, object>()
                };
                return ec;
            }).ToArray();

            storedEvents.AddRange(eventContexts);
        }

        //public void Store(Guid id, IEnumerable events, Action<IDictionary<string, object>> applyHeaders)
        //{
        //    List<EventContext> storedEvents;
        //    if (!_store.TryGetValue(id, out storedEvents))
        //    {
        //        storedEvents = new List<EventContext>();
        //        _store[id] = storedEvents;
        //    }

        //    var lastIndex = storedEvents.Count;
            
        //    var eventContexts = events.OfType<object>().Select(e =>
        //    {
        //        var ec = new EventContext()
        //        {
        //            Event = e,
        //            EventNumber = lastIndex++,
        //            Headers = new Dictionary<string, object>()
        //        };

        //        applyHeaders(ec.Headers);
        //        return ec;
        //    }).ToArray();

        //    storedEvents.AddRange(eventContexts);

        //    //foreach (var dispatcher in _eventDispatchers)
        //    //{
        //    //    dispatcher.Dispatch(id, eventContexts);
        //    //}
        //}

        public IEnumerable<object> GetEvents(Guid id)
        {
            List<EventContext> storedEvents;
            if (!_store.TryGetValue(id, out storedEvents))
            {
                return null;
            }
            return storedEvents.Select(ec => ec.Event);
        }

        //public void RegisterDispatcher(IEventDispatcher<Guid> eventDispatcher)
        //{
        //    _eventDispatchers.Add(eventDispatcher);
        //}
    }

}
