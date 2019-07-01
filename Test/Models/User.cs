using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Test.Models
{
    public class User : IdentityUser
    {
        
        public ICollection<Session> Sessions { get; set; }
        [Required(ErrorMessage = "Write first name")]
        [Display(Name = "First name")]
        public string firstName { get; set; }
        [Required(ErrorMessage = "Write last name")]
        [Display(Name = "Last name")]
        public string lastName { get; set; }
        public User()
        {
            Sessions = new List<Session>();
        }
    }
}