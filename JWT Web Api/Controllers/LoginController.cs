using JWT_Web_Api.Models;
using JWT_Web_Api.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JWT_Web_Api.Controllers
{
    public class LoginController : ApiController
    {

        [HttpPost]
        public HttpResponseMessage Login(Usuario user)
        {
            //http://localhost:62632/api/Login
            //Post-Raw-Json
            //{
            //  Username: "admin",
            //	Password: "admin123"
            //}
            Usuario u = new UserRepository().GetUser(user.Username);
            if (u == null)
                return Request.CreateResponse(HttpStatusCode.NotFound,
                     "The user was not found.");
            bool credentials = u.Password.Equals(user.Password);
            if (!credentials) return Request.CreateResponse(HttpStatusCode.Forbidden,
                "The username/password combination was wrong.");
            return Request.CreateResponse(HttpStatusCode.OK,
                 TokenManager.GenerateToken(user.Username,user.IpRequest));
        }

        [HttpGet]
        public HttpResponseMessage Validate(string token, string username)
        {
            bool exists = new UserRepository().GetUser(username) != null;
            if (!exists) return Request.CreateResponse(HttpStatusCode.NotFound,
                 "The user was not found.");
            string tokenUsername = TokenManager.ValidateToken(token);
            if (username.Equals(tokenUsername))
                return Request.CreateResponse(HttpStatusCode.OK);
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }


    }
}
