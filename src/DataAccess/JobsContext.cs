namespace DataAccess
{
    using Microsoft.EntityFrameworkCore;
    using Domain;

    /// <summary>
    /// Контекст 
    /// </summary>
    public sealed class JobsContext : DbContext
    {
        /// <summary>
        /// Вакансии
        /// </summary>
        public DbSet<Job> JobsList { get; set; }

        /// <summary>
        /// Контекст 
        /// </summary>
        public JobsContext(DbContextOptions<JobsContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
