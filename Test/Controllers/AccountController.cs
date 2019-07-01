using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Test.Models;
using static Test.Models.ViewModels;

namespace Test.Controllers
{
    public class AccountController : Controller
    {
        private TestUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<TestUserManager>();
            }
        }
        private IAuthenticationManager AuthManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(Login model)
        {
            if (ModelState.IsValid)
            {
                User user = await UserManager.FindAsync(model.Email, model.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Wrong login or password.");
                }
                else
                {
                    ClaimsIdentity ident = await UserManager.CreateIdentityAsync(user,
                    DefaultAuthenticationTypes.ApplicationCookie);
                    AuthManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = false
                    }, ident);
                    //Add usersession
                    using (var db = new TestContext())
                    {
                        var session = new Session();
                        session.UserId = user.Id;
                        session.TimeLogin = DateTime.Now;
                        session.TimeLogout = null;
                        db.Sessions.Add(session);
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index", "Menu");
                }
            }
            return View(model);
        }
        [Authorize]
        public ActionResult Logout()
        {
            using (var db = new TestContext())
            {
                Session userSession = db.Sessions.
                    Include(s => s.User).
                    Where(s => s.User.UserName == User.Identity.Name && s.TimeLogout == null).
                    FirstOrDefault();
                userSession.TimeLogout = DateTime.Now;
                db.Entry(userSession).State = EntityState.Modified;
                db.SaveChanges();
            }
            AuthManager.SignOut();
            return RedirectToAction("Index", "Account");
        }
    }
}