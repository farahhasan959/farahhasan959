using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace RightProject.Models
{
    public partial class Bill
    {
        public int Billid { get; set; }
        public string PayMethod { get; set; }
        public string Returned { get; set; }
        [ForeignKey("orderN")]
        public int order { get; set; }
        public virtual Order orderN { get; set; }
    }
}
