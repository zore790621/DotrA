namespace BackEndSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Orders
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Orders()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }

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

        public int PaymentID { get; set; }

        public DateTime OrderDate { get; set; }

        public virtual Members Members { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }

        public virtual Payment Payment { get; set; }

        public virtual Shippers Shippers { get; set; }
    }
}
