namespace Application.UseCases.AddJobsList
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain;

    /// <summary>
    /// Интерфейс для добавления вакансий
    /// </summary>
    public interface IAddJobsListUseCase
    {
        /// <summary>
        /// Выполнение
        /// </summary>
        Task ExecuteAsync(IEnumerable<JobDto> jobsList);

        /// <summary>
        /// Установка выходного порта
        /// </summary>
        /// <param name="outputPort"></param>
        void SetOutputPort(IOutputPort outputPort);
    }


    }
