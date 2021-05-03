namespace Application.UseCases.GetJob
{
    using System.Threading.Tasks;
    using Application.Services;
    using Domain;


    /// <summary>
    /// Получение вакансии по идентификатору
    /// </summary>
    public sealed class GetJobUseCase : IGetJobUseCase
    {
        #region Поля

        /// <summary>
        /// Сервис вакансий
        /// </summary>
        private readonly IVacanciesService _vacanciesService;

        /// <summary>
        /// Выходной порт
        /// </summary>
        private IOutputPort _outputPort;

        #endregion

        #region Конструктор

        /// <summary>
        /// Получение списка вакансий
        /// </summary>
        public GetJobUseCase(IVacanciesService vacanciesService)
        {
            _vacanciesService = vacanciesService;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Получение вакансии по идентификатору
        /// </summary>
        public async ValueTask<IJob> ExecuteAsync(int id)
        {
            var vacancy = await _vacanciesService.GetVacancyById(id);
            
            if (vacancy is not null)
            {
                this._outputPort?.Ok("Вакансия получена", vacancy);
            }
            else
            {
                this._outputPort?.Fail("Возникла ошибка во время получения вакансии");
            }

            return vacancy;
        }

        /// <summary>
        /// Установка выходного порта
        /// </summary>
        void IGetJobUseCase.SetOutputPort(IOutputPort outputPort) => this._outputPort = outputPort;

        #endregion
    }
}
