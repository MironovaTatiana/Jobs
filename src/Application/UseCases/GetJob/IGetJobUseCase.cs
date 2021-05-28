using System.Threading.Tasks;
using Domain;


namespace Application.UseCases.GetJob
{
    /// <summary>
    /// Интерфейс для получения вакансии по идентификатору
    /// </summary>
    public interface IGetJobUseCase
    {
        /// <summary>
        /// Выполнение
        /// </summary>
        Task<IJob> ExecuteAsync(int id);

        /// <summary>
        /// Установка выходного порта
        /// </summary>
        /// <param name="outputPort"></param>
        void SetOutputPort(IOutputPort outputPort);
    }
}
