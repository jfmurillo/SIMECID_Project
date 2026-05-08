using CoreApp;
using DataAccess.DAOs;

var builder = WebApplication.CreateBuilder(args);

// Configure data access with connection string from user-secrets / environment
SqlDao.Configure(builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("ConnectionStrings:DefaultConnection is not configured. Use dotnet user-secrets."));

EmailManager.Configure(builder.Configuration["AzureCommunication:ConnectionString"]
    ?? throw new InvalidOperationException("AzureCommunication:ConnectionString is not configured. Use dotnet user-secrets."));

ApptAlertManager.Configure(builder.Configuration["AzureCommunication:ConnectionString"]!);

builder.Services.AddControllers();
builder.Services.AddScoped<ValidateOTPManager>();
builder.Services.AddScoped<UserManager>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Allow AJAX requests from the WebApp frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("WebAppCors", policy =>
        policy.WithOrigins("https://localhost:7176", "http://localhost:5204")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("WebAppCors");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
