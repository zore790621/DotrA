namespace Database.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Member
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Member()
        {
            Orders = new HashSet<Order>();
        }

        public int MemberID { get; set; }
        [Display(Name = "�b��")]
        [Required(ErrorMessage = "�������")]
        [StringLength(20)]
        public string MemberAccount { get; set; }

        [Display(Name = "�K�X")]
        [Required(ErrorMessage = "�������")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "�m�W")]
        [Required(ErrorMessage = "�������")]
        [StringLength(50)]
        public string Name { get; set; }

        [Display(Name = "�H�c")]
        [Required(ErrorMessage = "�������")]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "�a�}")]
        [Required(ErrorMessage = "�������")]
        [StringLength(50)]
        public string Address { get; set; }

        [Display(Name = "�q��")]
        [Required(ErrorMessage = "�������")]
        [StringLength(20)]
        public string Phone { get; set; }

        //[Required]
        //[StringLength(250)]
        public string HashCode { get; set; }

        public bool EmailVerified { get; set; }

        public Guid ActivationCode { get; set; }

        [StringLength(100)]
        public string ResetPasswordCode { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
