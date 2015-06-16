using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;
using SysArch.Core;
using SysArch.Core.EventSourcing;

namespace SysArch.Domain.Tests
{

    public interface IGiven
    {
        void Event(Guid aggregateId, object evt);
    }

    public abstract class with<TCommand>
    {
        private class GivenImp : IGiven
        {
            private readonly MockRepository _repo;

            public GivenImp(MockRepository repo)
            {
                _repo = repo;
            }

            public void Event(Guid aggregateId, object @event)
            {
                _repo.Store(aggregateId, new[] { @event });
            }

        }

        protected Guid AggregateId = Guid.NewGuid();
        protected object[] PublishedEvents;
        private MockRepository _repository;
        protected Exception ThrownException { get; set; }

        [SetUp]
        public void Setup()
        {
            _repository = new MockRepository();

            var given = new GivenImp(_repository);
            Given(given);

            _repository.ResetCommitted();

            var handler = WithHandler(_repository);
            var command = When();

            try
            {
                handler.Handle(command);
            }
            catch (Exception ex)
            {
                ThrownException = ex;
            }

        }

        protected abstract ICommandHandler<TCommand> WithHandler(IAggregateRepository<Guid> repo);

        protected abstract void Given(IGiven given);

        protected abstract TCommand When();

        protected EventAssertions For(Guid aggregateId)
        {
            return new EventAssertions(_repository.GetCommittedEvents(aggregateId));
        }

        protected class EventAssertions
        {
            private readonly object[] _events;

            public EventAssertions(IEnumerable<object> events)
            {
                _events = events.ToArray();
            }

            public int NumberOfEvents
            {
                get { return _events.Length; }
            }

            public TEvent GetEvent<TEvent>(int eventIndex)
            {
                return ((TEvent)_events[eventIndex]);
            }

            public void Event<TEvent>(int index)
            {
                Assert.IsInstanceOf(typeof(TEvent), _events[index]);
            }

            public void Event<TEvent>(int index, Action<TEvent> eventAssertion)
            {
                eventAssertion((TEvent)_events[index]);
            }
        }

    }
}
