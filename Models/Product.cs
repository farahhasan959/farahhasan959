using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace RightProject.Models
{
    public partial class Product
    {
        public Product()
        {
            ProductShopOrders = new HashSet<ProductShopOrder>();
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Productid { get; set; }
        public int shopId { get; set; }
        public string Name { get; set; }
        public string Properties { get; set; }
        public string Type { get; set; }
        public string Manufacturer { get; set; }
        public string Picture { get; set; }
        public decimal? Price { get; set; }
        public int? Points { get; set; }
        public int? PointsToBuy { get; set; }
        public string Amount { get; set; }

        public virtual Shop shopIdNavigation { get; set; }
        public virtual ICollection<ProductShopOrder> ProductShopOrders { get; set; }

        public static implicit operator List<object>(Product v)
        {
            throw new NotImplementedException();
        }
    }

}
