using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
//using RoleBasedDemo.Authorization;
using RoleBasedDemo.Helpers;
using RoleBasedDemo.Middleware;
using RoleBasedDemo.Services;
using System;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor();
builder.Services.AddServices(builder.Configuration);
builder.Services.AddSession();
builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection(nameof(ConnectionStrings)));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = new PathString("/Account/Login");
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.Name = "AspNetCore.Cookies";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
        options.SlidingExpiration = true;
    });

//Role assigned 
// In Program.cs or Startup.cs
//builder.Services.AddSingleton<IAuthorizationHandler, DynamicAuthorizationHandler>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Maker", policy =>
        policy.RequireRole("Maker"));
    options.AddPolicy("Checker", policy =>
        policy.RequireRole("Checker"));
    options.AddPolicy("Admin", policy =>
        policy.RequireRole("Admin"));
    options.AddPolicy("Super", policy =>
        policy.RequireRole("Super"));
});
//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("RoleAndLocationPolicy", policy =>
//    {
//        policy.Requirements.Add(new DenyAnonymousAuthorizationRequirement()); // Require authentication
//        //policy.Requirements.Add(new DynamicAuthorizationRequirement());
//    });
//});


//test
//builder.Services.AddSingleton<IAuthorizationHandler, DynamicAuthorizationHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


//using Microsoft.AspNetCore.Authentication.Cookies;
//using RoleBasedDemo.Helpers;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddControllersWithViews();
//builder.Services.AddRazorPages();
//builder.Services.AddHttpContextAccessor();
//builder.Services.AddSession();
////builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection(nameof(ConnectionStrings)));
//builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection(nameof(ConnectionStrings)));

//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(options =>
//    {
//        options.LoginPath = new PathString("/Account/Login");
//        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
//        options.Cookie.Name = "AspNetCore.Cookies";
//        options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
//        options.SlidingExpiration = true;
//    });

////builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
////    .AddCookie(options =>
////    {
////        options.LoginPath = new PathString("/Account/Login");
////        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
////        options.Cookie.Name = "AspNetCore.Cookies";
////        options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
////        options.SlidingExpiration = true;
////    });

//// Configure authorization policies dynamically based on permissions retrieved from the database


//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    app.UseHsts();
//}

//app.UseSession();
//app.UseHttpsRedirection();
//app.UseStaticFiles();
//app.UseRouting();

//app.UseAuthentication();
//app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.Run();



