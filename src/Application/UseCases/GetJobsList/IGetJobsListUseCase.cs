namespace Application.UseCases.GetJobsList
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Интерфейс для получения списка вакансий
    /// </summary>
    public interface IGetJobsListUseCase
    {
        /// <summary>
        /// Установка выходного порта
        /// </summary>
        /// <param name="outputPort"></param>
        void SetOutputPort(IOutputPort outputPort);
    }
}
