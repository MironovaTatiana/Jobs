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
        /// Добавить вакансии
        /// </summary>
        public async Task AddRange(IEnumerable<Job> jobsList)
        {
            using (var context = this._context)
            {
                context.AddRange(jobsList);

                await context.SaveChangesAsync();
            }
        }

        #endregion
    }
}