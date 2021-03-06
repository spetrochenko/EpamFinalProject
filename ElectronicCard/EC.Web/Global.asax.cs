﻿using EC.BusinessLogic.Auth;
using EC.Common.Logger;
using EC.Entities.Entities;
using EC.Web.DependencyResolution;
using Newtonsoft.Json;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace EC.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            var cookie = HttpContext.Current.Request.Cookies.Get(FormsAuthentication.FormsCookieName);

            if (cookie != null)
            {
                var decryptCookie = FormsAuthentication.Decrypt(cookie.Value);

                var deserialize = JsonConvert.DeserializeObject<SerializeModel>(decryptCookie.UserData);

                var user = new User
                {
                    Login = deserialize.Login,
                    Roles = deserialize.Roles
                };

                var principal = new UserPrincipal(user);

                HttpContext.Current.User = principal;
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();

            var container = IoC.Initialize();

            var logger = container.GetInstance<ICustomLogger>();

            var separatorString = new string('-', 100);

            var exceptionString = string.Format($"EXCEPTION TYPE: {exception.GetType().FullName} \n EXCEPTION MESSAGE: {exception.Message} \n EXCEPTION DLL: {exception.Source} \n EXCEPTION METHOD: {exception.TargetSite.Name} \n EXCEPTION STACKTRACE: {exception.StackTrace} \n {separatorString} \n {string.Empty}");

            logger.WriteToFile(exceptionString);

            if (exception is HttpException httpException)
            {
                switch (httpException.GetHttpCode())
                {
                    case 401:
                        Response.Redirect("~/Auth/SignIn");
                        break;

                    case 403:
                        Response.Redirect("~/Error/Forbidden");
                        break;

                    case 404:
                        Response.Redirect("~/Error/NotFound");
                        break;

                    case 500:
                        Response.Redirect("~/Error/ServerError");
                        break;
                }
            }
        }
    }
}
