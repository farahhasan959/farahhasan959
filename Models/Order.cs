using System;
using System.Collections.Generic;

#nullable disable

namespace RightProject.Models
{
    public partial class Order
    {
        public Order()
        {
            ProductShopOrders = new HashSet<ProductShopOrder>();
        }

        public int Orderid { get; set; }
        public int Custid { get; set; }

        public string Parcode { get; set; }

        public virtual Customer Cust { get; set; }
        public virtual Bill bill { get; set; }
        public virtual ICollection<ProductShopOrder> ProductShopOrders { get; set; }
    }
}
