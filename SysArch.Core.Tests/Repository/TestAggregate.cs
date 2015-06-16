using SysArch.Core.EventSourcing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysArch.Core.Tests.Repository
{
    public class TestAggregate : AggregateRoot
    {

        public Guid Id;

        public TestAggregate()
        {

        }

        public TestAggregate(Guid id)
        {
            Raise(new TestAggregateCreated() { TestId = id });
        }

        private void Apply(TestAggregateCreated evt)
        {
            Id = evt.TestId;
        }

    }
}
