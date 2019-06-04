
using HomeOwners_Exemption.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeOwners_Exemption.Services
{
    public interface IUserService
    {
        Task<bool> ValidateCredentials(string username, string password, out User user);
    }
    public class User
    {
        public User(string username)
        {

            Username = username;
        }
        public string Username { get; }
    }
}
