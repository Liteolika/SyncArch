using NUnit.Framework;
using SysArch.Core;
using SysArch.Core.EventSourcing;
using SysArch.Domain.PortalUserContext;
using SysArch.Domain.PortalUserContext.CommandHandlers;
using SysArch.Domain.PortalUserContext.Commands;
using SysArch.Domain.PortalUserContext.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysArch.Domain.Tests
{
    [TestFixture]
    public class when_creating_portaluser : with<CreatePortalUser>
    {

        private readonly Guid _userId = Guid.NewGuid();

        protected override ICommandHandler<CreatePortalUser> WithHandler(IAggregateRepository<Guid> repo)
        {
            return new CreatePortalUserHandler(repo);
        }

        protected override void Given(IGiven given)
        {
            
        }

        protected override CreatePortalUser When()
        {
            return new CreatePortalUser() { UserId = _userId, EmailAddress = "papa", UserName = "koko" };
        }

        [Test]
        public void then_PortalUserCreated_event_is_published()
        {
            For(_userId).Event<PortalUserCreated>(0);
        }

        [Test]
        public void then_userId_in_event_is_correct()
        {
            For(_userId).Event<PortalUserCreated>(0, e => Assert.AreEqual(_userId, e.UserId));
        }

    }
}
