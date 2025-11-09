using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Scrutor;
using SIGEVENT2.Infrastructure;
using SIGEVENT2.Infrastructure.Auth;
using SIGEVENT2.Infrastructure.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add DbContext (scoped by default)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register your service as scoped
// builder.Services.AddScoped<WeatherForecastService>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Identity
builder.Services.AddIdentity<User, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.Scan(scan => scan
    .FromApplicationDependencies(a => a.GetName().Name!.StartsWith("SIGEVENT2"))

    // Register all classes matching the I{Name} naming pattern
    .AddClasses(classes => classes
        .Where(type =>
        {
            var interfaceType = type.GetInterfaces()
                .FirstOrDefault(i => i.Name == $"I{type.Name}");
            return interfaceType != null;
        }))
    .AsImplementedInterfaces()
    .WithScopedLifetime()

    // Additionally, register all IModuleDataInitializer implementations
    .AddClasses(classes => classes.AssignableTo<IModuleDataInitializer>())
    .AsImplementedInterfaces()
    .WithScopedLifetime()

    // Register all authorization policy registrars
    .AddClasses(classes => classes.AssignableTo<IAuthorizationPolicyRegistrar>())
    .AsImplementedInterfaces()
    .WithSingletonLifetime()
);

// Add default authorization service
builder.Services.AddAuthorization();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // Apply any pending migrations
    dbContext.Database.Migrate();

    var initializers = scope.ServiceProvider.GetServices<IModuleDataInitializer>();

    foreach (var initializer in initializers)
    {
        await initializer.EnsureInitialData(scope);
    }

    // Apply custom authorization policies dynamically
    var registrars = scope.ServiceProvider.GetServices<IAuthorizationPolicyRegistrar>();
    var options = scope.ServiceProvider.GetRequiredService<IOptions<AuthorizationOptions>>().Value;

    foreach (var registrar in registrars)
    {
        registrar.RegisterPolicies(options);
        Console.WriteLine($"✅ Registered authorization policies from {registrar.GetType().Name}");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseDefaultFiles();
    app.UseStaticFiles();
    app.MapFallbackToFile("index.html");
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// Root endpoint
app.MapGet("/", () => "Welcome to SIGEVENT2 API!");

app.MapControllers();

// app.UseSpa(spa =>
// {
//     spa.Options.SourcePath = "Web";

//     if (app.Environment.IsDevelopment())
//     {
//         // Proxy Angular dev server (ng serve)
//         spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
//     }
// });
if (!app.Environment.IsDevelopment())
{
    app.UseSpa(spa =>
    {
        spa.Options.SourcePath = "Web";
    });
}

app.Run();