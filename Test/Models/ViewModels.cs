using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Test.Models
{
    public class ViewModels
    {
        public class Login
        {
            [Required(ErrorMessage = "Write your username")]
            [Display(Name = "User")]
            public string Email { get; set; }
            [Required(ErrorMessage = "Write password")]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }
        }
        public class JsonUser
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }
    }
}