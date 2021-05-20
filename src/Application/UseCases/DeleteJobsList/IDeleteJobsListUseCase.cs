namespace Application.UseCases.DeleteJobsList
{
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
