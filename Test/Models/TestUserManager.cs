using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test.Models
{
    public class TestUserManager : UserManager<User>
    {
        public TestUserManager(IUserStore<User> store)
                : base(store)
        {
        }
        public static TestUserManager Create(IdentityFactoryOptions<TestUserManager> options,
                                                IOwinContext context)
        {
            TestContext db = context.Get<TestContext>();
            TestUserManager manager = new TestUserManager(new UserStore<User>(db));
            return manager;
        }
    }
}