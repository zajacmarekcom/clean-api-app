using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Clean.Database;

public class UserManager : UserManager<IdentityUser>
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserManager(IUserStore<IdentityUser> store, IOptions<IdentityOptions> optionsAccessor,
        IPasswordHasher<IdentityUser> passwordHasher, IEnumerable<IUserValidator<IdentityUser>> userValidators,
        IEnumerable<IPasswordValidator<IdentityUser>> passwordValidators, ILookupNormalizer keyNormalizer,
        IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<IdentityUser>> logger,
        RoleManager<IdentityRole> roleManager) : base(store, optionsAccessor, passwordHasher, userValidators,
        passwordValidators, keyNormalizer, errors, services, logger)
    {
        _roleManager = roleManager;
    }

    public override async Task<IdentityResult> CreateAsync(IdentityUser user, string password)
    {
        var result = await base.CreateAsync(user, password);
        if (result.Succeeded)
        {
            var roles = _roleManager.Roles.ToList();
            await AddToRoleAsync(user, "User");
        }

        return result;
    }
}