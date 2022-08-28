using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RightProject.Models
{
    public class AdminManager
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Managerid { get; set; }
       
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "password do not match")]
        public string PasswordConfirm { get; set; }
        public string Email { get; set; }
        [ScaffoldColumn(true)]
        public bool lockout { get; set; }

        [ScaffoldColumn(true)]

        public DateTime? lockTime { get; set; }

        [ScaffoldColumn(true)]
        public int ErrorLogCount { get; set; }

    }
}
