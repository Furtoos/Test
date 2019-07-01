using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Test.Models
{
    public class Session
    {
        [Key]
        public int Id { get; set; }
        public Session() { }
        public string UserId { get; set; }
        public User User { get; set; }
        public DateTime TimeLogin { get; set; }
        public DateTime? TimeLogout { get; set; }
    }
}