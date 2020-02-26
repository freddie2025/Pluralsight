using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace Paladin.Infrastructure
{
    public class HttpAuthenticate : FilterAttribute, IAuthenticationFilter
    {
        private string _username { get; set; }
        private string _password { get; set; }

        public HttpAuthenticate(string username, string password)
        {
            _username = username;
            _password = password;
        }

        public void OnAuthentication(AuthenticationContext filterContext)
        {
            var authHeader = filterContext.HttpContext.Request.Headers["Authorization"];
            if (!String.IsNullOrEmpty(authHeader))
            {
                var credentials = ASCIIEncoding.ASCII.GetString(
                    Convert.FromBase64String(authHeader.Replace("Basic", ""))).Split(':');
                var username = credentials[0];
                var password = credentials[1];
                if (username != _username || password != _password)
                {
                    filterContext.Result = new HttpUnauthorizedResult();
                }
            }
            else
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            
        }
    }
}