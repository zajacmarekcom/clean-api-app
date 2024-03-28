using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Clean.Database.Persistance;

public class UserDbContext(DbContextOptions<UserDbContext> options) : IdentityDbContext<IdentityUser>
{
    
}