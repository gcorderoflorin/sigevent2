using Microsoft.EntityFrameworkCore;
using SIGEVENT2.Data;
using SIGEVENT2.Models;

namespace SIGEVENT2.Services
{
    public class WeatherForecastService
    {
        private readonly AppDbContext _dbContext;

        public WeatherForecastService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<WeatherForecast>> GetForecastsAsync()
        {
            // Returns all records from the database
            return await _dbContext.WeatherForecasts.ToListAsync();
        }

        public async Task AddForecastAsync(WeatherForecast forecast)
        {
            _dbContext.WeatherForecasts.Add(forecast);
            await _dbContext.SaveChangesAsync();
        }
    }
}
