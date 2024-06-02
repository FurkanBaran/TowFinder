using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;
using System.Reflection.Emit;
using TowFinder.Models;

namespace TowFinder.Data
{ 
public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Identity tabloları için sütun uzunluklarını ayarlama
        builder.Entity<IdentityRole>(entity => entity.Property(m => m.Id).HasMaxLength(64));
        builder.Entity<IdentityRole>(entity => entity.Property(m => m.NormalizedName).HasMaxLength(64));
        builder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.LoginProvider).ForMySQLHasCharset("latin1"));
        builder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.ProviderKey).HasMaxLength(64));
        builder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(64));
        builder.Entity<IdentityUserRole<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(64));
        builder.Entity<IdentityUserRole<string>>(entity => entity.Property(m => m.RoleId).HasMaxLength(64));
        builder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(64));
        builder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(64));
        builder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.Name).HasMaxLength(64));
        builder.Entity<IdentityUserClaim<string>>(entity => entity.Property(m => m.Id).HasMaxLength(64));
        builder.Entity<IdentityUserClaim<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(64));
        builder.Entity<IdentityRoleClaim<string>>(entity => entity.Property(m => m.Id).HasMaxLength(64));
        builder.Entity<IdentityRoleClaim<string>>(entity => entity.Property(m => m.RoleId).HasMaxLength(64));


        builder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.ProviderKey).ForMySQLHasCharset("latin1"));
        builder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.UserId).ForMySQLHasCharset("latin1"));
        builder.Entity<IdentityUserRole<string>>(entity => entity.Property(m => m.UserId).ForMySQLHasCharset("latin1"));
        builder.Entity<IdentityUserRole<string>>(entity => entity.Property(m => m.RoleId).ForMySQLHasCharset("latin1"));
        builder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.UserId).ForMySQLHasCharset("latin1"));
        builder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.LoginProvider).ForMySQLHasCharset("latin1"));
        builder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.Name).ForMySQLHasCharset("latin1"));
        builder.Entity<IdentityUserClaim<string>>(entity => entity.Property(m => m.Id).ForMySQLHasCharset("latin1"));
        builder.Entity<IdentityUserClaim<string>>(entity => entity.Property(m => m.UserId).ForMySQLHasCharset("latin1"));
        builder.Entity<IdentityRoleClaim<string>>(entity => entity.Property(m => m.Id).ForMySQLHasCharset("latin1"));
        builder.Entity<IdentityRoleClaim<string>>(entity => entity.Property(m => m.RoleId).ForMySQLHasCharset("latin1"));

        builder.Entity<IdentityUser>(b =>
        {
            b.Property(u => u.Email).ForMySQLHasCharset("latin1");
            b.Property(u => u.NormalizedEmail).ForMySQLHasCharset("latin1");
            b.Property(u => u.UserName).ForMySQLHasCharset("latin1");
            b.Property(u => u.NormalizedUserName).ForMySQLHasCharset("latin1");
        });

        builder.Entity<IdentityRole>(b =>
        {
            b.Property(r => r.Name).ForMySQLHasCharset("latin1");
            b.Property(r => r.NormalizedName).ForMySQLHasCharset("latin1");
        });
    }

    public DbSet<TowOperator> TowOperators { get; set; }
}
}