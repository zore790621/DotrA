namespace BackEndSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Products
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Products()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }

        [Key]
        public int ProductID { get; set; }

        [Required]
        [StringLength(50)]
        public string ProductName { get; set; }

        public int SupplierID { get; set; }

        public int CategoryID { get; set; }

        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }

        [Required]
        //[StringLength(50)]
        public string Picture { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public int Quantity { get; set; }

        public int SalesPrice { get; set; }

        public virtual Categories Categories { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }

        public virtual Suppliers Suppliers { get; set; }
    }
}
