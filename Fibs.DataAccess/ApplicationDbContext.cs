using Fibs.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Fibs.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.Entity<ApplicationUser>().ToTable("users").Ignore(e=>e.PhoneNumberConfirmed);
            //builder.HasDefaultSchema("Security");
            builder.Entity<ApplicationUser>().ToTable("Users", "Security");
            builder.Entity<IdentityRole>().ToTable("Roles", "Security");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "Security");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "Security");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "Security");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserToken", "Security");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("UserRoleClaim", "Security");
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<FavoriteProduct> FavoriteProducts { get; set; }
        public DbSet<ShoppingBag> ShoppingBags { get; set; }
    }
}