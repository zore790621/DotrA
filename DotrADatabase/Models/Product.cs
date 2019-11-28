namespace DotrADatabase.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        [Display(Name = "�s��")]
        public int ProductID { get; set; }

        [Display(Name = "���~�W��")]
        [Required]
        [StringLength(50)]
        public string ProductName { get; set; }

        [Display(Name = "���y���q")]
        public int SupplierID { get; set; }

        public int CategoryID { get; set; }

        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }

        [StringLength(50)]
        public string Picture { get; set; }

        [StringLength(200)]
        public string Description { get; set; }
        [StringLength(200)]
        public string Image { get; set; }


        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
