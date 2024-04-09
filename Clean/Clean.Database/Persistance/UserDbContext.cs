using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Clean.Database.Persistance;

public class UserDbContext(DbContextOptions<UserDbContext> options) : IdentityDbContext<IdentityUser>(options)
{
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