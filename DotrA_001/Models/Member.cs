namespace DotrA_001.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Member
    {
        [Display(Name = "�|���s��")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MemberID { get; set; }
        
        [Display(Name = "�b��")]
        [Required]
        [StringLength(20)]
        public string MemberAccount { get; set; }
        [Display(Name = "�K�X")]
        [Required]
        [StringLength(20)]
        public string Password { get; set; }
        [Display(Name = "�m�W")]
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Display(Name = "�H�c")]
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        [Display(Name = "�a�}")]
        [Required]
        [StringLength(50)]
        public string Address { get; set; }
        [Display(Name = "��ʹq��")]
        [Required]
        [RegularExpression(@"^\d{4}\-?\d{3}\-?\d{3}$", ErrorMessage = "�ݬ�09xx-xxx-xxx�榡")]
        [StringLength(20)]
        public string Phone { get; set; }
    }
}