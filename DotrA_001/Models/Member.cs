namespace DotrA_001.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Member
    {
        public int MemberID { get; set; }
        [Display(Name = "�b��")]
        [Required(ErrorMessage = "�������")]
        [StringLength(20)]
        public string MemberAccount { get; set; }
        [Display(Name = "�K�X")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "�������")]
        //[StringLength(20)]
        public string Password { get; set; }
        [Display(Name = "�m�W")]
        [Required(ErrorMessage = "�������")]
        [StringLength(50)]
        public string Name { get; set; }
        [Display(Name = "�H�c")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "�������")]
        [StringLength(50)]
        public string Email { get; set; }
        [Display(Name = "�a�}")]
        [Required(ErrorMessage = "�������")]
        [StringLength(50)]
        public string Address { get; set; }
        [Display(Name = "�q��")]
        [Required(ErrorMessage = "�������")]
        [StringLength(20)]
        public string Phone { get; set; }
        public string HashCode { get; set; }//hash�[�K
        public bool EmailVerified { get; set; }
        public Guid ActivationCode { get; set; }
        public string ResetPasswordCode { get; set; }
    }
}
