using System;
using System.Collections.Generic;

#nullable disable

namespace RightProject.Models
{
    public partial class Gift
    {
     
        public Gift()
        {
           // CustomerGiftShops = new HashSet<CustomerGiftShop>();
        }
        public int Giftid { get; set; }
      //  public int IdCustomer { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public int Points { get; set; }
        public virtual Customer IdCustomerNavigation { get; set; }
       // public virtual ICollection<CustomerGiftShop> CustomerGiftShops { get; set; }
        //public virtual ICollection<GiftShop> GiftShops { get; set; }
    }
}
