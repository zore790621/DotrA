namespace BackEndSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderDetails
    {
        [Key]
        public int OrderID { get; set; }

        public int ProductID { get; set; }

        public DateTime OrderDate { get; set; }

        public short Quantity { get; set; }

        public float? Discount { get; set; }

        [Column(TypeName = "money")]
        public decimal TotalPrice { get; set; }

        public int PaymentID { get; set; }

        public virtual Orders Orders { get; set; }

        public virtual Payment Payment { get; set; }

        public virtual Products Products { get; set; }
    }
}
