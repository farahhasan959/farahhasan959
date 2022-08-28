using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RightProject.Models
{
    public class UserProfile
    {
        public int id { get; set; }
        [StringLength(100)]
        public string country { get; set; }
        public int Userid { get; set; }
        [ForeignKey("Userid")]
        public ShopManager shopManager { get; set; }
        [DataType(DataType.Date)]
        public DateTime  DateOfBurth { get; set; }
        [StringLength(400)]
        public string UrlImage { get; set; }


    }
}
