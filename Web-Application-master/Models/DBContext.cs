using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class MyDBContext : IdentityDbContext<User>
    {
<<<<<<< HEAD
        public MyDBContext() { }
        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options) { }
=======
        public MyDBContext(DbContextOptions options) : base(options) { }
>>>>>>> cbba2290ddc111f855844f127aedfe2fe8393ad9

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductAttachment> ProductAttachments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Category>(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration<Product>(new ProductConfiguration());
            modelBuilder.ApplyConfiguration<ProductAttachment>(new ProductAttachmentConfiguration());
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer("Data Source=.; Initial Catalog=Store;Integrated Security=True; TrustServerCertificate=True;");
                
        }
    }
}
