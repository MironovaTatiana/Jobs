using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess
{
    /// <summary>
    /// Репозиторий
    /// </summary>
    public sealed class JobsRepository : IJobsRepository
    {
        #region Поля

        /// <summary>
        /// Контекст
        /// </summary>
        private readonly JobsContext _context;

        #endregion

        #region Конструктор

        /// <summary>
        /// Репозиторий
        /// </summary>
        public JobsRepository(JobsContext context) => this._context = context ??
                                                                          throw new ArgumentNullException(
                                                                              nameof(context));

        #endregion

        #region Методы

        /// <summary>
        /// Добавление вакансии
        /// </summary>
        public async Task AddJobAsync(IJob job)
        {
            var jobFromBd = await GetJobByIdAsync(job.Id);

            if (jobFromBd == null)
            {
                await _context.AddAsync(job);
            }

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Получение вакансии по идентификатору
        /// </summary>
        public async ValueTask<Job> GetJobByIdAsync(int id)
        {
            if (_context.JobsList.Count() > 0)
            {
                Job job = await _context
                .JobsList
                .Where(e => e.Id == id)
                .Select(e => e)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);

                if (job != null)
                {
                    return job;
                }
            }

            return null;
        }

        /// <summary>
        /// Получение n первых вакансий из базы
        /// </summary>
        public async ValueTask<IEnumerable<Job>> GetJobsLimitNAsync(int n)
        {
            var jobs = new List<Job>();

            if (_context.JobsList.Count() >= n)
            {
                 jobs = await _context
                .JobsList
                .OrderBy(x => x.Id)
                .Take(n)
                .Select(e => e)
                .ToListAsync()
                .ConfigureAwait(false);   
            }

            return jobs;
        }

        /// <summary>
        /// Получение количества вакансий из БД
        /// </summary>
        public async ValueTask<int> GetCountAsync()
        {
            int count = _context.JobsList.Count();

            return await new ValueTask<int>(count);
        }

        #endregion
    }
}