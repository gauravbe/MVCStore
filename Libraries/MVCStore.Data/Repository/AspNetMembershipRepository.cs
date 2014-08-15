using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using MVCStore.Core.Authentication;

namespace MVCStore.Data
{
    public class AspNetMembershipRepository: IMembership
    {
        public bool ValidateUser(string userName, string password)
        {
            var provider = GetMemeberShipProvider(userName);
            if (provider != null)
            {
                return provider.ValidateUser(userName, password);
            }
            return false;
        }

        private MembershipProvider GetMemeberShipProvider(string userName)
        {
            var user = Membership.GetUser(userName);
            if (user != null)
            {
                return Membership.Provider;
            }
            return null;
        }


        public System.Web.Security.MembershipCreateStatus CreateUser(string userName, string password, string email)
        {
            throw new NotImplementedException();
        }

        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }
    }
}