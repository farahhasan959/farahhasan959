using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace RightProject.Models
{
    public partial class Shop
    {
        public Shop()
        {
           // GiftShops = new HashSet<GiftShop>();
            Products = new HashSet<Product>();
        }

        public int Shopid { get; set; }
        [ForeignKey("Manager")]
        public int? Managerid { get; set; }
        public string Name { get; set; }
        public string Locathion { get; set; }
        public string Phonenumber { get; set; }
       
      
        public virtual ShopManager Manager { get; set; }
       
        public virtual ICollection<Product> Products { get; set; }
    }
}
