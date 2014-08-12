using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVCStore.Core.Authentication;
using MVCStore.Plugin.Auth.AspNetMembership;

namespace MVCStore.Data.Repository
{
    public class UserRepository
    {
        public bool ValidateUser(string userName, string password)
        {
            IMembershipService userMembershipService = new AspNetMembershipService();
            return userMembershipService.ValidateUser(userName, password);
        }
    }
}
