using System.Collections.Generic;
using System.Threading.Tasks;
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
        ValueTask<IEnumerable<IJob>> GetVacanciesList(int count);
    }
}
