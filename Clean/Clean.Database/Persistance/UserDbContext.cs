using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Clean.Database.Persistance;

public class UserDbContext : IdentityDbContext<IdentityUser>
{
    public string DbPath { get; }
    
    public UserDbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "blogging.db");
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={DbPath}");
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        var adminRole = new IdentityRole("Admin");
        var userRole = new IdentityRole("User");
        var userId = Guid.NewGuid().ToString();
        var passwordHasher = new PasswordHasher<IdentityUser>();

        builder.Entity<IdentityRole>().HasData([adminRole, userRole]);

        builder.Entity<IdentityUser>().HasData([
            new IdentityUser
            {
                Id = userId,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@admin.pl",
                EmailConfirmed = true,
                NormalizedEmail = "ADMIN@ADMIN.PL",
                PasswordHash = passwordHasher.HashPassword(null, "Password123!")
            }
        ]);

        builder.Entity<IdentityUserRole<string>>().HasData([
            new IdentityUserRole<string>()
            {
                UserId = userId,
                RoleId = adminRole.Id
            }
        ]);
        
        base.OnModelCreating(builder);
    }
}