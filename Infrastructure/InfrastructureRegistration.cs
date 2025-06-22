using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HotelReservation.Infrastructure
{
    public static class InfrastructureRegistration
    {
        public static IServiceCollection InfrastureConfiguration(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(options =>
                                                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
