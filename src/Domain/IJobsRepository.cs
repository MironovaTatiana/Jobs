namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    /// <summary>
    /// Интерфейс для вакансий
    /// </summary>
    public interface IJobsRepository
    {
        /// <summary>
        /// Получение вакансии по идентификатору
        /// </summary>
        ValueTask<Job> GetJobById(int id);

        /// <summary>
        /// Получение количества вакансий из БД
        /// </summary>
        ValueTask<int> GetCount();

        /// <summary>
        /// Добавить вакансию
        /// </summary>
        Task AddJob(IJob job);

        /// <summary>
        /// Получение n первых вакансий из базы
        /// </summary>
        ValueTask<IEnumerable<Job>> GetJobsLimitN(int n);
    }
}
