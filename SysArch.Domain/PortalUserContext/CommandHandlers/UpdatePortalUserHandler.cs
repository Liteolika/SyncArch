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
    public class UpdatePortalUserHandler : ICommandHandler<UpdatePortalUser>
    {

        protected readonly IAggregateRepository<Guid> _repository;

        public UpdatePortalUserHandler(IAggregateRepository<Guid> repository)
        {
            _repository = repository;
        }

        public void Handle(UpdatePortalUser cmd)
        {
            var portalUser = _repository.GetAggregate<PortalUser>(cmd.UserId);
            if (portalUser == null)
                throw new Exception("User does not exist");

            portalUser.SetEmailAddress(cmd.EmailAddress);
            portalUser.SetUserName(cmd.UserName);
            _repository.Store(cmd.UserId, portalUser, cmd.ApplyCommandHeaders());
        }
    }
}
