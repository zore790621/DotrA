using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Configuration;
using ConnectionKey;
using System.IO;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DotrA_001.Models
{
    public partial class DotrADbContext : DbContext
    {
        public DotrADbContext() : base("name=DotrADb")
        {
            Database.Connection.ConnectionString = Parameters.ConnectionString;
        }

        public virtual DbSet<Member> Members { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
