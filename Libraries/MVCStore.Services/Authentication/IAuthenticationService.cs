using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCStore.Services.Authentication
{    
    public interface IAuthenticationService
    {
        bool ValidateUser(string userName, string password);
        string Createuser(string userName, string password, string email, string passwordQuestion, string passwordAnswer);
        List<string> GetAllRoles();
    }
}
