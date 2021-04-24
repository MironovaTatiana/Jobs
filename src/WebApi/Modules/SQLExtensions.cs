namespace WebApi.Modules
{
    using DataAccess;
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// �����, ���������� ����������� ������ ���������� ��� IServiceCollection
    /// </summary>
    public static class SQLExtensions
    {
        /// <summary>
        /// ���������� SQL ����������
        /// </summary>
        public static IServiceCollection AddSQLServer(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<JobsContext>(
                options => options.UseNpgsql(
                    configuration.GetValue<string>("ConnectionStrings:DefaultConnection")));

            services.AddTransient<IJobsRepository, JobsRepository>();

            return services;
        }
    }
}
