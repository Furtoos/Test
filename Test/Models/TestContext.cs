using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Test.Models
{
    //context Database
    public class TestContext : IdentityDbContext<User>
    {
        public TestContext() : base("PrimaryDatabase") { }

        public static TestContext Create()
        {
            return new TestContext();
        }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Movement> Movements { get; set; }
    }
}