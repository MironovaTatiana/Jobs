namespace WebApi.Modules
{
    using DataAccess;
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Класс, содержащий специальные методы расширения для IServiceCollection
    /// </summary>
    public static class PostgreSQLExtensions
    {
        /// <summary>
        /// Конфигурация сервисов соединения к БД
        /// </summary>
        public static IServiceCollection DataBaseConnectionConfiguration(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<JobsContext>(
                options => options.UseNpgsql(
                    configuration.GetValue<string>("ConnectionStrings:DefaultConnection")));
            
            services.AddScoped<IJobsRepository, JobsRepository>();

            return services;
        }
    }
}
