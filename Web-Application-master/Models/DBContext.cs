using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class MyDBContext : DbContext
    {
        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options) { }

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
    }
}
