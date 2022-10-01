using eFoodHub.Entities;
using eFoodHub.Repositories;
using eFoodHub.Services.Configuration;
using eFoodHub.Services.Models;
using eFoodHub.UI.Configuration;

using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var mvcBuilder = builder.Services.AddControllersWithViews();
if (builder.Environment.IsDevelopment())
{
    mvcBuilder.AddRazorRuntimeCompilation();
}
builder.Services.ConfigureRepositoryServices(builder.Configuration);
builder.Services.ConfigureDependenciesServices();

builder.Services.Configure<PayStackConfig>(builder.Configuration.GetSection("PayStackConfig"));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

    ctx.Database.EnsureCreated();
    var email = "admin@food.com";
    var phone = "090909008744";

    if (!ctx.Users.Any(u => u.Email == email))
    {
        var adminUser = new User
        {
            Name = "Admininistrator",
            PhoneNumber = phone,
            Email = email,
            NormalizedEmail = email.ToUpper(),
            UserName = email,
            NormalizedUserName = email,
            SecurityStamp = Guid.NewGuid().ToString(),
        };
        var result = userMgr.CreateAsync(adminUser, "Password").GetAwaiter().GetResult();
        userMgr.AddToRoleAsync(adminUser, "Admin").GetAwaiter().GetResult();
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
     name: "areas",
     pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();