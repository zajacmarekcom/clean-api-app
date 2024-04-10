using Clean.Database;
using Clean.Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizePage("/Dashboard", "Admin");
});

builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddIdentityCookies(options =>
    {
        options.ApplicationCookie.Configure(cfg =>
        {
            cfg.LoginPath = "/";
            cfg.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        });
    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
});

builder.Services.AddDatabase();
builder.Services.AddCookieIdentity();
builder.Services.AddLogging();
builder.Services.AddInfrastructure();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

app.MapRazorPages();

await app.MigrateDb();

app.Run();