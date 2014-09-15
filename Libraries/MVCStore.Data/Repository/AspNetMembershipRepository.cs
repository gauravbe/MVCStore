using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
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
        
        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public List<string> GetAllRoles()
        {
           return Roles.GetAllRoles().ToList();
        }

        public string CreateUser(string userName, string password, string email, string passwordQuestion, string passwordAnswer)
        {
            MembershipCreateStatus createStatus;
            string status = string.Empty;
            MembershipUser newUser = Membership.CreateUser(userName, password, email, passwordQuestion, passwordAnswer, true,
                out createStatus);

            switch (createStatus)
            {
                case MembershipCreateStatus.Success:
                    status = "The user account was successfully created!";
                    break;
                case MembershipCreateStatus.DuplicateUserName:
                    status = "There already exists a user with this username.";
                    break;

                case MembershipCreateStatus.DuplicateEmail:
                    status = "There already exists a user with this email address.";
                    break;
                case MembershipCreateStatus.InvalidEmail:
                    status = "There email address you provided in invalid.";
                    break;
                case MembershipCreateStatus.InvalidAnswer:
                    status = "There security answer was invalid.";
                    break;
                case MembershipCreateStatus.InvalidPassword:
                    status = "The password you provided is invalid. It must be seven characters long and have at least one non-alphanumeric character.";
                    break;
                default:
                    status = "There was an unknown error; the user account was NOT created.";
                    break;
            }
            return status;
        }
    }
}