namespace BackEndSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Orders
    {
        [Key]
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

        public virtual Members Members { get; set; }

        public virtual OrderDetails OrderDetails { get; set; }

        public virtual Shippers Shippers { get; set; }
    }
}
