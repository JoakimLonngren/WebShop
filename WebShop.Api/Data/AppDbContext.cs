using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebShop.Api.Auth;
using WebShop.Api.Categories.Domain;
using WebShop.Api.Products.Domain;

namespace WebShop.Api.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext>options) : base(options)
        {
        }

        DbSet<Product> Products => Set<Product>();
        DbSet<Category> Categories => Set<Category>();
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Category
            builder.Entity<Category>(cfg =>
            {
                cfg.HasKey(c => c.Id);
                cfg.Property(c => c.Name).IsRequired().HasMaxLength(100);
                cfg.HasIndex(c => c.Name).IsUnique();
            });

            //Product
            builder.Entity<Product>(cfg =>
            {
                cfg.HasKey(p => p.Id);
                cfg.Property(p => p.Name).IsRequired().HasMaxLength(100);
                cfg.HasIndex(p => p.Name).IsUnique();
                cfg.Property(p => p.Price).IsRequired().HasPrecision(18, 2);

                //Relation to Category
                cfg.HasOne(p => p.Category)
                    .WithMany(c => c.Products)
                    .HasForeignKey(p => p.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
