using SysArch.Core.EventSourcing;
using SysArch.Domain.PortalUserContext.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysArch.Domain.PortalUserContext
{
    public class PortalUser : AggregateRoot
    {
        private Guid _userId;
        private string _userName;
        private string _emailAddress;


        public PortalUser()
        {

        }

        public PortalUser(Guid userId, string userName, string emailAddress)
        {
            Raise(new PortalUserCreated() { UserId = userId, UserName = userName, EmailAddress = emailAddress });
        }

        public void SetEmailAddress(string emailAddress)
        {
            Raise(new EmailAddressUpdated() { UserId = _userId, EmailAddress = emailAddress });
        }

        public void SetUserName(string userName)
        {
            Raise(new UserNameUpdated() { UserId = _userId, UserName = userName });
        }

        private void Apply(EmailAddressUpdated evt)
        {
            _emailAddress = evt.EmailAddress;
        }

        private void Apply(UserNameUpdated evt)
        {
            _userName = evt.UserName;
        }

        private void Apply(PortalUserCreated evt)
        {
            _userId = evt.UserId;
            _userName = evt.UserName;
            _emailAddress = evt.EmailAddress;
        }

    }
}
