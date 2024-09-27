using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Testovoe.Infrastructure.AppContext
{
    public static class RegistrationContext
    {
        public static IServiceCollection AddAppContext(this IServiceCollection services)
        {
            string connection = "Server=(localdb)\\mssqllocaldb;Database=applicationdb;Trusted_Connection=True;";

            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

            return services;
        }
    }
}
