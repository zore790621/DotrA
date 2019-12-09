namespace BackEndSystem.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DotrAContext : DbContext
    {
        public DotrAContext()
            : base("name=DotrAContext")
        {
        }

        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Members> Members { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Shippers> Shippers { get; set; }
        public virtual DbSet<Suppliers> Suppliers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>()
                .Property(e => e.AdminAccount)
                .IsUnicode(false);

            modelBuilder.Entity<Admin>()
                .Property(e => e.AdminPW)
                .IsUnicode(false);

            modelBuilder.Entity<Categories>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.Categories)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Members>()
                .Property(e => e.MemberAccount)
                .IsUnicode(false);

            modelBuilder.Entity<Members>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Members>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Members>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Members>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Members)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderDetails>()
                .Property(e => e.TotalPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Orders>()
                .Property(e => e.RecipientPhone)
                .IsUnicode(false);

            modelBuilder.Entity<Orders>()
                .HasOptional(e => e.OrderDetails)
                .WithRequired(e => e.Orders);

            modelBuilder.Entity<Payment>()
                .Property(e => e.PaymentMethod)
                .IsUnicode(false);

            modelBuilder.Entity<Payment>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Payment)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Products>()
                .Property(e => e.UnitPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Products>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Products)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Shippers>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Shippers)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Suppliers>()
                .Property(e => e.CampanyPhone)
                .IsUnicode(false);

            modelBuilder.Entity<Suppliers>()
                .Property(e => e.CompanyAddress)
                .IsUnicode(false);

            modelBuilder.Entity<Suppliers>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.Suppliers)
                .WillCascadeOnDelete(false);
        }
    }
}
