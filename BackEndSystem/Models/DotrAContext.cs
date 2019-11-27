using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BackEndSystem.Models
{
    public class DotrAContext : DbContext
    {
        public DotrAContext():base("DotrAContext")
        {

        }

        public DbSet<Member> Members { get; set; }
    }

   
}