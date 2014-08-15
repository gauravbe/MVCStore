using MVCStore.Core.Authentication;
using MVCStore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCStore.Services.Authentication
{
    public class AspNetAuthenticationService: IAuthenticationService
    {
        private readonly IMembership _aspnetMembershipService;

        public AspNetAuthenticationService(IMembership aspnetMembershipService)
        {
            this._aspnetMembershipService = aspnetMembershipService;
        }
        public bool ValidateUser(string userName, string password)
        {            
            return _aspnetMembershipService.ValidateUser(userName, password);
        }
    }
}
