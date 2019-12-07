using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace DotrA_001.Models
{
    [Table("Admin")]
    public partial class Admin
    {
        [Display(Name = "�޲z�̽s��")]
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdminID { get; set; }

        [Display(Name = "�޲z�̱b��")]
        [Column(Order = 1)]
        [StringLength(20)]
        public string AdminAccount { get; set; }

        [Display(Name = "�޲z�̱K�X")]
        [Column(Order = 2)]
        [StringLength(20)]
        public string AdminPW { get; set; }
    }
}
