namespace Database.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Supplier
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Supplier()
        {
            Products = new HashSet<Product>();
        }

        public int SupplierID { get; set; }
        [Display(Name = "廠商名稱")]
        [Required]
        [StringLength(50)]
        public string CompanyName { get; set; }
        [Display(Name = "聯絡電話")]
        [StringLength(50)]
        public string CampanyPhone { get; set; }

        [Display(Name = "聯絡地址")]
        [StringLength(20)]
        public string CompanyAddress { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
