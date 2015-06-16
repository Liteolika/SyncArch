using SysArch.Core;
using SysArch.Core.EventSourcing;
using SysArch.Domain.PortalUserContext.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysArch.Domain.PortalUserContext.CommandHandlers
{

    public class CreatePortalUserHandler : ICommandHandler<CreatePortalUser>
    {

        protected readonly IAggregateRepository<Guid> _repository;

        public CreatePortalUserHandler(IAggregateRepository<Guid> repository)
        {
            _repository = repository;
        }

        public void Handle(CreatePortalUser cmd)
        {
            var portalUser = _repository.GetAggregate<PortalUser>(cmd.UserId);
            if (portalUser != null)
                throw new Exception("User already created");
            portalUser = new PortalUser(cmd.UserId, cmd.UserName, cmd.EmailAddress);
            //_repository.Store(cmd.UserId, portalUser, cmd.ApplyCommandHeaders());
            _repository.Store(cmd.UserId, portalUser);
        }
    }


}
