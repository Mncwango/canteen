using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System.Configuration;

namespace Canteen
{
    public partial class Startup
    {

        public void ConfigureAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");
            string fbClientId = !string.IsNullOrEmpty(ConfigurationManager.AppSettings["FaceBookClientId"] as string) ? ConfigurationManager.AppSettings["FaceBookClientId"].ToString() : "";
            string fbClientSecret = !string.IsNullOrEmpty(ConfigurationManager.AppSettings["FaceBookClientSecret"] as string) ? ConfigurationManager.AppSettings["FaceBookClientSecret"].ToString() : "";
            if (!string.IsNullOrEmpty(fbClientId) && !string.IsNullOrEmpty(fbClientSecret))
            {
                app.UseFacebookAuthentication(
                   appId: "814354765377510",
                   appSecret: "856194652bde936ce6ae53a3d5f326b2");
            }

           // app.UseGoogleAuthentication(clientId: "814354765377510", clientSecret: "856194652bde936ce6ae53a3d5f326b2");
        }

    }
}