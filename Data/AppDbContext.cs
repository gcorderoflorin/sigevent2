using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SIGEVENT2.Core.Membership.Models;
using SIGEVENT2.Models;

namespace SIGEVENT2.Data;

public class AppDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>, IDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<WeatherForecast> WeatherForecasts => Set<WeatherForecast>();
}
