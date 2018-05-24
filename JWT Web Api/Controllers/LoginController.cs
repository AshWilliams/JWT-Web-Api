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
    [RoutePrefix("jwtdemo")]
    public class LoginController : ApiController
    {
        
        [HttpPost]
        [Route("login")]
        public HttpResponseMessage Login(Usuario user)
        {
            //http://localhost:62632/jwtdemo/login
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
        [Route("validarheader")]
        public HttpResponseMessage ValidateHeader()
        {
            //http://localhost:62632/jwtdemo/validarheader
            var headers = Request.Headers;
            var tokenBearer =  headers.GetValues("tokenBearer").First();
            var username = headers.GetValues("username").First();

            bool exists = new UserRepository().GetUser(username) != null;
            if (!exists) return Request.CreateResponse(HttpStatusCode.NotFound,
                 "The user was not found.");
            string tokenUsername = TokenManager.ValidateToken(tokenBearer);
            if (username.Equals(tokenUsername))
                return Request.CreateResponse(HttpStatusCode.OK,"Token válido para usuario "+ tokenUsername);
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [HttpGet]
        [Route("validartoken")]
        public HttpResponseMessage ValidateToken(string token, string username)
        {
            //http://localhost:62632/jwtdemo/validartoken
            //var headers = Request.Headers;
            //var authToken =  headers.GetValues("tokenBearer").First();

            bool exists = new UserRepository().GetUser(username) != null;
            if (!exists) return Request.CreateResponse(HttpStatusCode.NotFound,
                 "The user was not found.");
            string tokenUsername = TokenManager.ValidateToken(token);
            if (username.Equals(tokenUsername))
                return Request.CreateResponse(HttpStatusCode.OK, "Token válido para usuario " + tokenUsername);
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

    }
}
