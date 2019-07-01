namespace Test.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;
    using System.Data.Entity.Migrations;
    using Test.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Test.Models.TestContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Test.Models.TestContext context)
        {
            //Add four users through the user manager
            var userManager = new TestUserManager(new UserStore<User>(context));
            User user1 = new User { UserName = "Furtooss", firstName = "Maxim", lastName = "Semeniuk" };
            string password = "12345678";
            userManager.Create(user1, password);
            User user2 = new User { UserName = "Jsmith", firstName = "James", lastName = "Smith" };
            userManager.Create(user2, password);
            User user3 = new User { UserName = "Adowson", firstName = "Mark", lastName = "Spilberg" };
            userManager.Create(user3, password);
            User user4 = new User { UserName = "Mholes", firstName = "Dmitriy", lastName = "Govard" };
            userManager.Create(user4, password);
            //Add sessions for 11 task
            context.Sessions.
                Add(new Session { UserId = user2.Id, TimeLogin = new DateTime(2018, 4, 1, 12, 1, 0), TimeLogout = new DateTime(2018, 4, 1, 12, 23, 0) });
            context.Sessions.
                Add(new Session { UserId = user3.Id, TimeLogin = new DateTime(2018, 4, 2, 11, 11, 0), TimeLogout = new DateTime(2018, 4, 2, 15, 31, 0) });
            context.Sessions.
                Add(new Session { UserId = user2.Id, TimeLogin = new DateTime(2018, 4, 1, 14, 34, 0), TimeLogout = new DateTime(2018, 4, 1, 18, 12, 1) });
            context.Sessions.
                Add(new Session { UserId = user3.Id, TimeLogin = new DateTime(2018, 4, 30, 15, 43, 1), TimeLogout = new DateTime(2018, 4, 30, 21, 32, 50) });
            context.Sessions.
                Add(new Session { UserId = user4.Id, TimeLogin = new DateTime(2018, 4, 30, 16, 20, 1), TimeLogout = new DateTime(2018, 4, 30, 20, 1, 15) });
            context.Sessions.
                Add(new Session { UserId = user4.Id, TimeLogin = new DateTime(2018, 4, 1, 10, 0, 1), TimeLogout = new DateTime(2018, 4, 1, 11, 10, 0) });
            context.Sessions.
                Add(new Session { UserId = user2.Id, TimeLogin = new DateTime(2018, 4, 30, 20, 0, 1), TimeLogout = new DateTime(2018, 4, 30, 20, 22, 20) });
            context.SaveChanges();
            base.Seed(context);
        }
    }
}
