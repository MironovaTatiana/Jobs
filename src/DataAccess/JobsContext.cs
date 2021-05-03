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
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Job>().HasKey(u => u.Id);
        }

        /// <summary>
        /// Контекст 
        /// </summary>
        public JobsContext(DbContextOptions<JobsContext> options) : base(options)
        {
            Database.EnsureDeleted();   // удаляем бд со старой схемой
            Database.EnsureCreated();
        }
    }
}
