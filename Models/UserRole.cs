using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RightProject.Models
{
    public class UserRole
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [ForeignKey("appRole")]
        public int Roleid { get; set; }
        public virtual AppRole appRole { get; set; }


        public int Userid { get; set; }

        [Required]
        public string Type { get; set; }


    }
}
