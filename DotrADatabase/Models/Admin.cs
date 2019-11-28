namespace DotrADatabase.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Admin")]
    public partial class Admin
    {
        [Key]
        [Column(Order = 0)]
        public int AdminID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string AdminAccount { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(20)]
        public string AdminPW { get; set; }
    }
}
