using System.Collections.Generic;
using System.Data.SqlClient;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Security.Claims;
using System.Threading.Tasks;
using HomeOwners_Exemption.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace HomeOwners_Exemption.Services
{
    public class AdminUserService : IUserService
    {
        private IDictionary<string, (string PasswordHash, User User)> _users = new Dictionary<string, (string PasswordHash, User User)>();




        Task<bool> IUserService.ValidateCredentials(string username,  string password, homeownerContext DbContext, out User user)
        {
            user = null;
            const string LDAP_PATH = "ldap://laassessor.co.la.ca.us";
            const string LDAP_DOMAIN = "laassessor.co.la.ca.us";

            using (var context = new PrincipalContext(ContextType.Domain, LDAP_DOMAIN, "service_acct_user", "service_acct_pswd"))
            {
                if (context.ValidateCredentials(username, password))
                {
                    using (var de = new DirectoryEntry(LDAP_PATH))
                    using (var ds = new DirectorySearcher(de))
                    {
                        // other logic to verify user has correct permissions
                        
                        var EmpID = new SqlParameter("@EmployeeID", username);
                        var modelUser = DbContext.user.FromSql("sp_UserInfo @EmployeeID", EmpID).FirstOrDefaultAsync().Result;

                        
                        //If user does not exist in Database Return False
                        if (modelUser == null)
                        {
                            return Task.FromResult(AuthenticateResult.Fail("Invalid key").Succeeded);
                        }


                        user = new User(username,modelUser.Name, modelUser.RoleTitle);
                        // User authenticated and authorized
                        var identities = new List<ClaimsIdentity> { new ClaimsIdentity("custom auth type") };
                        var ticket = new AuthenticationTicket(new ClaimsPrincipal(identities), username);
                        return Task.FromResult(AuthenticateResult.Success(ticket).Succeeded);
                    }
                }
            }

            // User not authenticated
            return Task.FromResult(AuthenticateResult.Fail("Invalid key").Succeeded);
        }

      
    }
}
     
