using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;


namespace Application.UseCases.GetJobsList
{
    /// <summary>
    /// Интерфейс для получения списка вакансий
    /// </summary>
    public interface IGetJobsListUseCase
    {
        /// <summary>
        /// Выполнение
        /// </summary>
        Task<IEnumerable<IJob>> ExecuteAsync(int count);

        /// <summary>
        /// Установка выходного порта
        /// </summary>
        /// <param name="outputPort"></param>
        void SetOutputPort(IOutputPort outputPort);
    }
}
