using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JWT_Web_Api.Models
{
    public class Usuario
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string IpRequest { get; set; }

        public enum UserRole
        {
            NORMAL,
            ADMIN
        }
    }
}