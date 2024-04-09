using Clean.Database.Persistance;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Clean.Presentation.Admin.Pages;

public class Dashboard : PageModel
{
    private readonly UserDbContext _context;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<IdentityUser> _userManager;

    public Dashboard(UserDbContext context, RoleManager<IdentityRole> roleManager,
        UserManager<IdentityUser> userManager)
    {
        _context = context;
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public IEnumerable<UserDto> Users { get; set; }
    public IEnumerable<RoleDto> Roles { get; set; }

    public async Task OnGetAsync()
    {
        Users = (await _context.Users.ToListAsync()).Select(x => new UserDto()
        {
            Id = x.Id,
            Email = x.UserName!,
            Role = string.Join(',', _userManager.GetRolesAsync(x).Result)
        });

        Roles = (await _context.Roles.ToListAsync()).Select(x => new RoleDto()
        {
            Id = x.Id,
            Name = x.Name!
        });
    }
}

public class RoleDto
{
    public string Id { get; set; }
    public string Name { get; set; }
}

public class UserDto
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
}