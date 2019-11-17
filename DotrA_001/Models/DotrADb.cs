using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Configuration;

namespace DotrA_001.Models
{
    public partial class DotrADb : DbContext
    {
        public DotrADb() : base("name=DotrADb")
        {
            //var context = new ConnectionString.ConnectionString();
            //this.Database.Connection.ConnectionString = context.Godb();
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    }
}
