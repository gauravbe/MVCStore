using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace MVCStore.Core.Authentication
{
    public interface IMembership
    {
        bool ValidateUser(string userName, string password);
        string CreateUser(string userName, string password, string email, string passwordQuestion, string passwordAnswer);
        bool ChangePassword(string userName, string oldPassword, string newPassword);
        List<string> GetAllRoles();
    }
}
