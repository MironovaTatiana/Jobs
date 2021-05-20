using System.Collections.Generic;
using System.Threading.Tasks;


namespace Domain
{
    /// <summary>
    /// Интерфейс для вакансий
    /// </summary>
    public interface IJobsRepository
    {
        /// <summary>
        /// Получение вакансии по идентификатору
        /// </summary>
        ValueTask<Job> GetJobByIdAsync(int id);

        /// <summary>
        /// Получение количества вакансий из БД
        /// </summary>
        ValueTask<int> GetCountAsync();

        /// <summary>
        /// Добавить вакансию
        /// </summary>
        Task AddJobAsync(IJob job);

        /// <summary>
        /// Получение n первых вакансий из базы
        /// </summary>
        ValueTask<IEnumerable<Job>> GetJobsLimitNAsync(int n);
    }
}
