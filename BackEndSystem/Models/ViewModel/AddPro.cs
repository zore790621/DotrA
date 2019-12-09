using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BackEndSystem.Models.ViewModel
{
    public class AddPro
    {
        [Key]
        public int ProductId { get; set; }

        [Display(Name = "產品名稱")]
        [Required]
        [StringLength(20)]
        public string PName { get; set; }
        public int SupplierID { get; set; }
        public int CategoryID { get; set; }

        [Display(Name = "產品進價")]
        [Required]
        public decimal PPrice { get; set; }

        [Display(Name = "產品圖片")]
        public HttpPostedFileBase Picture { get; set; }

        [Display(Name = "產品描述")]
        [Required]
        [StringLength(200)]
        public string Pdescript { get; set; }

        [Display(Name = "產品數量")]
        [Required] 
        public int PQuantity { get; set; }

        [Display(Name = "銷售價格")]
        [Required]
        public int PSalesPrice { get; set; }

        public virtual Suppliers Suppliers { get; set; }
        public virtual Categories Categories { get; set; }
        //測試:public virtual Categories Categories { get; set; }
    }
}