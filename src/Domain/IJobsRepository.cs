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
        /// Добавить список вакансий
        /// </summary>
        Task AddRange(IEnumerable<Job> jobList);
    }
}
