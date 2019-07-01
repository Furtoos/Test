using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test.Models;
using static Test.Models.ViewModels;

namespace Test.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private TestContext db = new TestContext();
        public string GetUsers()
        {
            List<User> users = db.Users.ToList();
            return JsonConvert.SerializeObject(users);
        }
        public string GetMovements()
        {
            List<Movement> movements = db.Movements.ToList();
            return JsonConvert.SerializeObject(movements);
        }
        [HttpPost]
        public string Create([Bind(Exclude = "Id")] User user)
        {
            string msg;
            try
            {
                if (ModelState.IsValid)
                {
                    db.Database.ExecuteSqlCommand("disable trigger Users_Insert on AspNetUsers");
                    db.Users.Add(user);
                    User presentUser = db.Users.
                        Where(u => u.UserName == User.Identity.Name).
                        FirstOrDefault();
                    Movement movement = new Movement
                    {
                        UserName = presentUser.UserName,
                        Date = DateTime.Now,
                        Change = "added account with name " + user.UserName + "."
                    };
                    db.Movements.Add(movement);
                    db.SaveChanges();
                    db.Database.ExecuteSqlCommand("enable trigger Users_Insert on AspNetUsers");
                    db.SaveChanges();
                    msg = "Saved Successfully";
                }
                else
                {
                    msg = "Validation data not successfully";
                }
            }
            catch (Exception ex)
            {
                msg = "Error occured:" + ex.Message;
            }
            return msg;
        }
        [HttpPost]
        public string Edit(User user)
        {
            string msg;
            try
            {
                if (ModelState.IsValid)
                {
                    Movement movement;
                    using (TestContext tempContext = new TestContext())
                    {
                        User oldUser = tempContext.Users.Find(user.Id);
                        User presentUser = tempContext.Users.
                            Where(u => u.UserName == User.Identity.Name).
                            FirstOrDefault();
                        movement = new Movement
                        {
                            UserName = presentUser.UserName,
                            Date = DateTime.Now,
                            Change = "eddit account with name " + oldUser.UserName + " changed:"
                        };
                        if (user.UserName != oldUser.UserName)
                            movement.Change += " user name " + oldUser.UserName + " to " + user.UserName + ",";
                        if (user.firstName != oldUser.firstName)
                            movement.Change += " first name " + oldUser.UserName + " to " + user.UserName + ",";
                        if (user.UserName != oldUser.UserName)
                            movement.Change += " last name " + oldUser.UserName + " to " + user.UserName + ",";
                        if (user.UserName != oldUser.UserName)
                            movement.Change += " password";
                        movement.Change += ".";
                    }
                    db.Database.ExecuteSqlCommand("disable trigger Users_Update on AspNetUsers");
                    db.Entry(user).State = EntityState.Modified;
                    db.Movements.Add(movement);
                    db.SaveChanges();
                    db.Database.ExecuteSqlCommand("enable trigger Users_Update on AspNetUsers");
                    db.SaveChanges();
                    msg = "Saved Successfully";
                }
                else
                {
                    msg = "Validation data not successfully";
                }
            }
            catch (Exception ex)
            {
                msg = "Error occured:" + ex.Message;
            }
            return msg;
        }
        [HttpPost]
        public string Delete(string Id)
        {
            User user = db.Users.
                Include(u => u.Sessions).
                Where(u => u.Id == Id).FirstOrDefault();
            User presentUser = db.Users.
                Where(u => u.UserName == User.Identity.Name).
                FirstOrDefault();
            Movement movement = new Movement
            {
                UserName = presentUser.UserName,
                Date = DateTime.Now,
                Change = "delete account with name " + user.UserName + "."
            };
            db.Database.ExecuteSqlCommand("disable trigger Users_Delete on AspNetUsers");
            db.Movements.Add(movement);
            db.Users.Remove(user);
            db.SaveChanges();
            db.Database.ExecuteSqlCommand("enable trigger Users_Delete on AspNetUsers");
            db.SaveChanges();
            return "Deleted successfully"; 
        }
        
    }
}