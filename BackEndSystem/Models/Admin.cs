namespace BackEndSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Admin")]
    public partial class Admin
    {
        public int AdminID { get; set; }

        [Required]
        [StringLength(20)]
        public string AdminAccount { get; set; }

        [Required]
        [StringLength(20)]
        public string AdminPW { get; set; }
    }
}
