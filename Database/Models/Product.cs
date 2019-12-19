namespace Database.Models
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

        public int ProductID { get; set; }
        [Display(Name = "���~�W��")]
        [Required]
        [StringLength(50)]
        public string ProductName { get; set; }
        [Display(Name = "������")]
        public int SupplierID { get; set; }
        [Display(Name = "���~����")]
        public int CategoryID { get; set; }
        [Display(Name = "���~�i��")]
        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }
        [Display(Name = "���~�Ϥ�")]
        public string Picture { get; set; }
        [Display(Name = "���~�y�z")]
        [StringLength(200)]
        public string Description { get; set; }
        [Display(Name = "���~�ƶq")]
        public int Quantity { get; set; }
        [Display(Name = "�P�����")]
        public int SalesPrice { get; set; }
        [Display(Name = "���~���A")]
        public string Status { get; set; }
        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual Supplier Supplier { get; set; }
        [Display(Name = "���~���A")]
        public string Status { get; set; }
    }
}
