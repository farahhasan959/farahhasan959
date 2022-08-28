using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace RightProject.Models
{
    public partial class ProductShopOrder
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IdOrder { get; set; }
        public int IdProduct { get; set; }

        public string Amount { get; set; }
        public virtual Order IdOrderNavigation { get; set; }
        public virtual Product IdProductNavigation { get; set; }
    }
}
