using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SysPlan.Core.Interface;
using SysPlan.Data.Context;

namespace SysPlan.Application.Helper
{
    public static class DatabaseHelper
    {
        public static void ConfigureService(IServiceCollection services)
        {
            var stringConexaoComBancoDeDados = SettingsDefault.connectionString();
            services.AddDbContext<SysPlanContext>(options =>
                options.UseSqlServer(stringConexaoComBancoDeDados).EnableSensitiveDataLogging(true));

            services.AddScoped<IDbContext, SysPlanContext>();
        }

        public static SysPlanContext GetInstance()
        {
            var optionsBuilder = new DbContextOptionsBuilder<SysPlanContext>();
            optionsBuilder.UseSqlServer(SettingsDefault.connectionString());

            return new SysPlanContext(optionsBuilder.Options);
        }
    }
}
