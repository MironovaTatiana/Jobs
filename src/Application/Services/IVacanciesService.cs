using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Dtos;
using Domain;

namespace Application.Services
{
    /// <summary>
    /// Интерфейс списка вакансий
    /// </summary>
    public interface IVacanciesService
    {
        /// <summary>
        /// Получение списка вакансий
        /// </summary>
        ValueTask<IEnumerable<IJob>> GetVacanciesListAsync(int count);

        /// <summary>
        /// Получение вакансии по идентификатору
        /// </summary>
        ValueTask<JobDto> GetVacancyByIdAsync(int id);

    }
}
