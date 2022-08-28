using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace RightProject.Models
{
    public partial class ShopManager
    {
      
        [Key]
        public int Shopmanagerid { get; set; }
        public decimal? Salary { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        [Compare("Password",ErrorMessage ="password do not match")]
        public string PasswordConfirm { get; set; }
        public string Email { get; set; }
        [ScaffoldColumn(true)]
        public bool lockout { get; set; }

        [ScaffoldColumn(true)]

        public DateTime? lockTime { get; set; }

        [ScaffoldColumn(true)]
        public int ErrorLogCount { get; set; }

        public virtual Shop Shops { get; set; }
    }
}
