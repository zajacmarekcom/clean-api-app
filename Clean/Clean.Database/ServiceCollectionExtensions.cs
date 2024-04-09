using System.Text;
using Clean.Core.Interfaces;
using Clean.Database.Persistance;
using Clean.Database.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Clean.Database;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddDbContext<InvoiceDbContext>(options =>
        {
            options.UseInMemoryDatabase("InvoiceDb");
        });
        
        services.AddDbContext<UserDbContext>(options =>
        {
            options.UseInMemoryDatabase("UserDb");
        });
        services.AddIdentityCore<IdentityUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<UserDbContext>()
            .AddApiEndpoints()
            .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("TOKENPROVIDER");

        services.Configure<DataProtectionTokenProviderOptions>(options =>
        {
            options.TokenLifespan = TimeSpan.FromHours(3);
        });

        services.AddScoped<IInvoiceRepository, InvoiceRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();

        return services;
    }

    public static async Task SeedUsers(this WebApplication app)
    {
        var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
        using var scope = scopeFactory.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

        await roleManager.CreateAsync(new IdentityRole("Admin"));
        await roleManager.CreateAsync(new IdentityRole("User"));

        var result = await userManager.CreateAsync(new IdentityUser("admin@admin.pl"), "Password123!");
        
        if (result.Succeeded)
        {
            var user = await userManager.FindByNameAsync("admin");
            await userManager.AddToRoleAsync(user, "Admin");
        }
    }
}