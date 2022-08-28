using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace RightProject.Models
{
    public partial class Customer
    {
        public Customer()
        {
          //  CustomerGiftShops = new HashSet<CustomerGiftShop>();
            Orders = new HashSet<Order>();
        }

        public int Customerid { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
       
        public string PasswordConfirm { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Username { get; set; }
        public DateTime? Bdate { get; set; }
        public string Address { get; set; }
        [ScaffoldColumn(true)]
        public bool lockout { get; set; }
        public int Points { get; set; }


        [ScaffoldColumn(true)]

        public DateTime? lockTime { get; set; }

        [ScaffoldColumn(true)]
        public int ErrorLogCount { get; set; }
        //public virtual ICollection<CustomerGiftShop> CustomerGiftShops { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
