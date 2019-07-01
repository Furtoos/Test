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
    public class MenuController : Controller
    {
        private TestContext db = new TestContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Users()
        {
            return View();
        }
        public ActionResult Report()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ReportOfMonth(DateTime date)
        {
            List<Session> sessions = db.Sessions.Include(s => s.User).Where(s => s.TimeLogin.Month == date.Month).ToList();
            ICollection<TimeFlag> timeFlags = new List<TimeFlag>();
            //Add flags
            foreach(var session in sessions)
            {
                if(session.UserId != null)
                {
                    timeFlags.Add(new TimeFlag { Time = session.TimeLogin, Login = true, UserId = session.User.Id });
                    timeFlags.Add(new TimeFlag { Time = Convert.ToDateTime(session.TimeLogout), Login = false, UserId = session.User.Id });
                }
            }
            //Sort flags
            timeFlags = timeFlags.OrderBy(f => f.Time).ToList();
            //Take first and last day of month
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            Week[] weeks = new Week[5];
            for (int week = 0; week <= 4; week++)
            {
                int day = 1;
                int MaxUsers;
                weeks[week] = new Week();
                weeks[week].Monday = new DateTime(date.Year, date.Month, day + (week * 7)).ToString("d");
                while(day <= 7 && day + (week * 7) <= lastDayOfMonth.Day)
                {
                    MaxUsers = MaxUsersPerDay(timeFlags.Where(f => f.Time.Day == (day + week * 7)).ToList());
                    if (weeks[week].MaxUsers < MaxUsers)
                        weeks[week].MaxUsers = MaxUsers;
                    day++;
                }
            }
            return PartialView(weeks);
        }
        public struct TimeFlag
        {
            public DateTime Time { get; set; }
            public bool Login { get; set; }
            public string UserId { get; set; }
        }
       
        public int MaxUsersPerDay(List<TimeFlag> flags)
        {
            int users = 0;
            //looking for users who entered yesterday
            List<string> usersLoginToday = new List<string>();
            foreach (var flag in flags)
            {
                if (flag.Login)
                    usersLoginToday.Add(flag.UserId);
            }
            foreach (var flag in flags)
            {
                if (!flag.Login && !usersLoginToday.Exists(u => u.Equals(flag.UserId)))
                    users++;
            }  
            int maxUsers = users;
            //looking for the greatest number of concurrently logged 
            foreach (var flag in flags)
            {
                if (flag.Login)
                {
                    users++;
                    if (maxUsers < users)
                        maxUsers = users;
                }
                else
                    users--;
                 
            }
            return maxUsers;
        }
    }
}