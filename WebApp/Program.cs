using DataAccess.DAOs;
using CoreApp;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Configure data access with connection string from user-secrets / environment
SqlDao.Configure(builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("ConnectionStrings:DefaultConnection is not configured. Use dotnet user-secrets."));

EmailManager.Configure(builder.Configuration["AzureCommunication:ConnectionString"]
    ?? throw new InvalidOperationException("AzureCommunication:ConnectionString is not configured. Use dotnet user-secrets."));

ApptAlertManager.Configure(builder.Configuration["AzureCommunication:ConnectionString"]!);

builder.Services.AddRazorPages();

builder.Services.AddDbContext<CalendarDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CalendarContext")
        ?? throw new InvalidOperationException("ConnectionStrings:CalendarContext is not configured.")));

builder.Services.AddScoped<UserManager>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
        options.LogoutPath = "/Logout";
        options.AccessDeniedPath = "/Error";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Lax;
    });

builder.Services.AddAuthorization();

// Allow the WebAPI (7144) to receive AJAX requests from browser pages served by this app
builder.Services.AddCors(options =>
{
    options.AddPolicy("WebApiCors", policy =>
        policy.WithOrigins("https://localhost:7144")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseCors("WebApiCors");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllers();

app.Run();
