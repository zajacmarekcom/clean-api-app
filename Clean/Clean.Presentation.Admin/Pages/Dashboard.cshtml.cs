using Clean.Database.Persistance;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Clean.Presentation.Admin.Pages;

public class Dashboard : PageModel
{
    private readonly UserDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public Dashboard(UserDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [BindProperty]
    public UserDto? User { get; set; }
    public IEnumerable<UserDto> Users { get; set; }
    public IEnumerable<RoleDto> Roles { get; set; }

    public async Task OnGetAsync()
    {
        Users = (await _context.Users.ToListAsync()).Select(x => new UserDto()
        {
            Id = x.Id,
            Email = x.UserName!,
            Roles = _userManager.GetRolesAsync(x).Result.ToArray()
        });

        Roles = (await _context.Roles.ToListAsync()).Select(x => new RoleDto()
        {
            Id = x.Id,
            Name = x.Name!
        });
    }
    
    public async Task OnPostAsync()
    {
        if (User is null || string.IsNullOrWhiteSpace(User.Email))
        {
            await OnGetAsync();
            return;
        }
        
        var user = await _userManager.FindByNameAsync(User.Email);

        if (user is null)
        {
            return;
        }
        
        if (_userManager.IsInRoleAsync(user, "Admin").Result)
        {
            await _userManager.RemoveFromRoleAsync(user, "Admin");
        }
        else
        {
            await _userManager.AddToRoleAsync(user, "Admin");
        }
        
        await OnGetAsync();
    }

    public async Task<IActionResult> OnPostLogoutAsync()
    {
        await _signInManager.SignOutAsync();
        return RedirectToPage("Index");
    }

}

public class RoleDto
{
    public string? Id { get; set; }
    public string? Name { get; set; }
}

public class UserDto
{
    public string? Id { get; set; }
    public string? Email { get; set; }
    public string[]? Roles { get; set; }
}