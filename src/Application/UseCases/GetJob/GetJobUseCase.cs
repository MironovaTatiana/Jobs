namespace Application.UseCases.GetJob
{
    using System.Threading.Tasks;
    using Application.Dtos;
    using Application.Services;
    using Domain;


    /// <summary>
    /// Получение вакансии по идентификатору
    /// </summary>
    public sealed class GetJobUseCase : IGetJobUseCase
    {
        #region Поля

        /// <summary>
        /// Репозиторий
        /// </summary>
        private readonly IJobsRepository _jobsRepository;

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
        public GetJobUseCase(IVacanciesService vacanciesService, IJobsRepository jobsRepository)
        {
            _vacanciesService = vacanciesService;
            _jobsRepository = jobsRepository;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Получение вакансии по идентификатору
        /// </summary>
        public async Task<IJob> ExecuteAsync(int id)
        {
            var vacancy = new JobDto();
            var jobFromBd = await this._jobsRepository.GetJobByIdAsync(id).ConfigureAwait(false);

            try
            {
                if (jobFromBd == null)
                {
                    vacancy = await _vacanciesService.GetVacancyByIdAsync(id);
                }
                else
                {
                    vacancy = jobFromBd.ConvertJobToJobDto();
                }

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
            catch 
            {
                throw new JobsException("Отсутствует интернет-соединение");
            }
        }

        /// <summary>
        /// Установка выходного порта
        /// </summary>
        void IGetJobUseCase.SetOutputPort(IOutputPort outputPort) => this._outputPort = outputPort;

        #endregion
    }
}
