using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using Test.Models;
[assembly: OwinStartup(typeof(Test.App_Start.Startup))]

namespace Test.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<TestContext>(TestContext.Create);
            app.CreatePerOwinContext<TestUserManager>(TestUserManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Index"),
            });
        }
    }
}