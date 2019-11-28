using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Database.Models
{
    public partial class Order
    {
        public int OrderID { get; set; }

        public int MemberID { get; set; }

        public int ShipperID { get; set; }

        [Required]
        [StringLength(50)]
        public string RecipientName { get; set; }

        [Required]
        [StringLength(20)]
        public string RecipientPhone { get; set; }

        [Required]
        [StringLength(50)]
        public string RecipientAddress { get; set; }

        public virtual Member Member { get; set; }

        public virtual OrderDetail OrderDetail { get; set; }

        public virtual Shipper Shipper { get; set; }
    }
}
