
using HomeOwners_Exemption.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeOwners_Exemption.Services
{
    public interface IUserService
    {
        Task<bool> ValidateCredentials(string username,  string password, homeownerContext DbContext, out User user);
    }
    public class User
    {
        public User(string username, string fullname, string role)
        {

            Username = username;
            UserFullName = fullname;
            Role = role;
        }
        public string Username { get; }
        public string UserFullName { get; }
        public string Role { get; }
    }
}
