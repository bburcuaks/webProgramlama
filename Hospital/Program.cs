using Hospital.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DatabaseContext>(opts=>
{
    opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opts =>
    {
        opts.Cookie.Name = ".Hospital.auth";
        opts.ExpireTimeSpan = TimeSpan.FromDays(7);
        opts.SlidingExpiration = false;
        opts.LoginPath = "/Account/Login";
        opts.LoginPath = "/Account/Logout";
        opts.AccessDeniedPath = "/Home/AccessDenied";
    });
var app = builder.Build();

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
