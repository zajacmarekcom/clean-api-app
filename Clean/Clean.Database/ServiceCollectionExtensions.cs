using Clean.Core.Interfaces;
using Clean.Database.Persistance;
using Clean.Database.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Clean.Database;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddDbContext<InvoiceDbContext>();
        services.AddDbContext<UserDbContext>();

        services.AddScoped<IInvoiceRepository, InvoiceRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();

        return services;
    }

    public static IServiceCollection AddCookieIdentity(this IServiceCollection services)
    {
        services.AddIdentityCore<IdentityUser>()
            .AddRoles<IdentityRole>()
            .AddRoleManager<RoleManager<IdentityRole>>()
            .AddUserManager<UserManager>()
            .AddSignInManager()
            .AddEntityFrameworkStores<UserDbContext>();

        return services;
    }
    
    public static IServiceCollection AddTokenIdentity(this IServiceCollection services)
    {
        services.AddIdentityCore<IdentityUser>()
            .AddRoles<IdentityRole>()
            .AddRoleManager<RoleManager<IdentityRole>>()
            .AddUserManager<UserManager>()
            .AddEntityFrameworkStores<UserDbContext>()
            .AddApiEndpoints();

        return services;
    }

    public static async Task MigrateDb(this WebApplication app)
    {
        var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
        using var scope = scopeFactory.CreateScope();

        var invoiceDb = scope.ServiceProvider.GetRequiredService<InvoiceDbContext>();
        await invoiceDb.Database.MigrateAsync();
        var userDb = scope.ServiceProvider.GetRequiredService<UserDbContext>();
        await userDb.Database.MigrateAsync();
        
        if (userDb.Users.Any())
        {
            return;
        }
        
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

        await roleManager.CreateAsync(new IdentityRole("Admin"));
        await roleManager.CreateAsync(new IdentityRole("User"));

        var result = await userManager.CreateAsync(new IdentityUser("admin@admin.pl"), "Password123!");
        
        if (result.Succeeded)
        {
            var user = await userManager.FindByNameAsync("admin@admin.pl");
            await userManager.AddToRoleAsync(user, "Admin");
        }
    }
}