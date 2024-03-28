using Clean.Core.Interfaces;
using Clean.Database.Persistance;
using Clean.Database.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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
            .AddEntityFrameworkStores<UserDbContext>()
            .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("TOKENPROVIDER");

        services.Configure<DataProtectionTokenProviderOptions>(options =>
        {
            options.TokenLifespan = TimeSpan.FromHours(3);
        });

        services.AddScoped<IInvoiceRepository, InvoiceRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();

        return services;
    }
}