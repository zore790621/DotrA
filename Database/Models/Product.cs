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
        [Display(Name = "產品名稱")]
        [Required]
        [StringLength(50)]
        public string ProductName { get; set; }
        [Display(Name = "供應商")]
        public int SupplierID { get; set; }
        [Display(Name = "產品種類")]
        public int CategoryID { get; set; }
        [Display(Name = "產品進價")]
        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }
        [Display(Name = "產品圖片")]
        public string Picture { get; set; }
        [Display(Name = "產品描述")]
        [StringLength(200)]
        public string Description { get; set; }
        [Display(Name = "產品數量")]
        public int Quantity { get; set; }
        [Display(Name = "銷售價格")]
        public int SalesPrice { get; set; }
        [Display(Name = "產品狀態")]
        public string Status { get; set; }
        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual Supplier Supplier { get; set; }
        [Display(Name = "產品狀態")]
        public string Status { get; set; }
    }
}
