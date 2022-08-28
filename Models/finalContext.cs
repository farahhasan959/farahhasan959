using System;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;


#nullable disable

namespace RightProject.Models
{
    public partial class finalContext : DbContext
    {
        public finalContext()
        {
        }

        public finalContext(DbContextOptions<finalContext> options)
            : base(options)
        {
        }

        public virtual DbSet<UserProfile> UserProfiles { get; set; }
        public virtual DbSet<AppRole> AppRoles { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
      //  public virtual DbSet<CustomerGiftShop> CustomerGiftShops { get; set; }
        public virtual DbSet<Gift> Gifts { get; set; }
        
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        
        public virtual DbSet<ProductShopOrder> ProductShopOrders { get; set; }
        public virtual DbSet<Shop> Shops { get; set; }
        public virtual DbSet<ShopManager> ShopManagers { get; set; }
        public virtual DbSet<AdminManager> AdminManagers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //Confgi
              //  optionsBuilder.UseSqlServer("Server=LOCALHOST\\MSSQLSERVER;Database=myfinaldb;Integrated Security=SSPI;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Arabic_CI_AS");

            modelBuilder.Entity<AppRole>()
              .HasData(
               new AppRole { id = 1, RoleName = "Admin" },
               new AppRole { id = 2, RoleName = "ShopManager" },
               new AppRole { id = 3, RoleName = "Customer" }
               );
           

           
        }

           

         

         

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
