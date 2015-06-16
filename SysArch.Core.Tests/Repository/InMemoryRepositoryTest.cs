using NUnit.Framework;
using SysArch.Core.EventSourcing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysArch.Core.Tests.Repository
{
    [TestFixture]
    public class InMemoryRepositoryTest : RepositoryTestBase
    {

        private InMemoryEventStoreRepository _inMemoryRepo;

        protected override IAggregateRepository<Guid> Repository
        {
            get { return _inMemoryRepo; }
        }

        protected override void FillRepository(Guid aggregateId, object[] events)
        {
            _inMemoryRepo.Store(aggregateId, events);
        }

        [SetUp]
        public void Setup()
        {
            _inMemoryRepo = new InMemoryEventStoreRepository();
        }

        [Test]
        public void when_getting_newly_created_aggregate()
        {

            var testId = Guid.NewGuid(); 
             FillRepository(testId, new object[] {new TestAggregateCreated() {TestId = testId}}); 
 
 
             var agg = Repository.GetAggregate<TestAggregate>(testId); 
              
             Assert.AreEqual(testId, agg.Id); 

        }

        [Test]
        public void when_getting_previously_unstored_aggregate()
        {
            var testId = Guid.NewGuid();
            var agg = Repository.GetAggregate<TestAggregate>(testId);
            Assert.IsNull(agg);
        }

        [Test]
        public void when_getting_unknown_aggregate_type()
        {
            var testId = Guid.NewGuid();
            FillRepository(testId, new object[] { new TestAggregateCreated() { TestId = testId } });
            var agg = Repository.GetAggregate<ForeignAggregate>(testId);
            Assert.IsInstanceOf(typeof(ForeignAggregate), agg);
        }

        [Test]
        public void when_storing_a_new_aggregate()
        {
            var testId = Guid.NewGuid();
            var agg = new TestAggregate(testId);

            Repository.Store(testId, agg);
            var events = Repository.GetEvents(testId);

            Assert.IsInstanceOf(typeof(TestAggregateCreated), Enumerable.First<object>(events));
        }


    }
}
