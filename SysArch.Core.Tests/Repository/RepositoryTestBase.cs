using SysArch.Core.EventSourcing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysArch.Core.Tests.Repository
{
    public abstract class RepositoryTestBase
    {

        protected abstract IAggregateRepository<Guid> Repository { get; }

        protected abstract void FillRepository(Guid aggregateId, object[] events);


    }
}
