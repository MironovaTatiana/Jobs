namespace Application.UseCases.DeleteJobsList
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain;

    /// <summary>
    /// Интерфейс для удаления вакансий
    /// </summary>
    public interface IDeleteJobsListUseCase
    {
        /// <summary>
        /// Установка выходного порта
        /// </summary>
        /// <param name="outputPort"></param>
        void SetOutputPort(IOutputPort outputPort);
    }
}
