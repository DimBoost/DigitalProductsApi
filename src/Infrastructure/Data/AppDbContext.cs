using DigitalProductsApi.src.Domain.Entities;
using DigitalProductsApi.src.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DigitalProductsApi.src.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Purchase>()
                .HasIndex(p => p.UserId);

            // renamed identity table 
            builder.Entity<ApplicationUser>().ToTable("Users", schema: "identity");
            builder.Entity<IdentityRole<Guid>>().ToTable("Roles", schema: "identity");
            builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles", schema: "identity");
            builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims", schema: "identity");
            builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins", schema: "identity");
            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims", schema: "identity");
            builder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens", schema: "identity");
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
    }
}
