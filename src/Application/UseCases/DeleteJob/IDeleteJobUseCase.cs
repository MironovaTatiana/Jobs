using System.Threading.Tasks;

namespace Application.UseCases.DeleteJob
{
    /// <summary>
    /// Интерфейс для удаления вакансии
    /// </summary>
    public interface IDeleteJobUseCase
    {
        /// <summary>
        /// Выполнение
        /// </summary>
        Task ExecuteAsync(int id);

        /// <summary>
        /// Установка выходного порта
        /// </summary>
        /// <param name="outputPort"></param>
        void SetOutputPort(IOutputPort outputPort);
    }
}
