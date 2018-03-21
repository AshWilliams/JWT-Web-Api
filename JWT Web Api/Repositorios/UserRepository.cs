using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JWT_Web_Api.Models;
namespace JWT_Web_Api.Repositorios
{
    public class UserRepository
    {
        public List<Usuario> TestUsers;
        public UserRepository()
        {
            TestUsers = new List<Usuario>();
            TestUsers.Add(new Usuario() { Username = "admin", Password = "admin123", IpRequest="10.233.32.252" });
            TestUsers.Add(new Usuario() { Username = "rrozas", Password = "admin123", IpRequest= "10.233.32.252" });
        }
        public Usuario GetUser(string username)
        {
            try
            {
                return TestUsers.First(user => user.Username.Equals(username));
            }
            catch
            {
                return null;
            }
        }

        public bool AddUser(Usuario newUser)
        {
            try
            {
                TestUsers.Add(newUser);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}