namespace Application.UseCases.GetJobsList
{
    using System;
    using System.Threading.Tasks;
    using Domain;


    /// <summary>
    /// Получение списка вакансий
    /// </summary>
    public sealed class GetJobsListUseCase : IGetJobsListUseCase
    {
        #region Поля

        /// <summary>
        /// Репозиторий
        /// </summary>
        private readonly IJobsRepository _jobsRepository;

        #endregion

        #region Конструктор

        /// <summary>
        /// Получение списка вакансий
        /// </summary>
        public GetJobsListUseCase(IJobsRepository jobsRepository)
        {
            _jobsRepository = jobsRepository;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Установка выходного порта
        /// </summary>
        void IGetJobsListUseCase.SetOutputPort(IOutputPort outputPort)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
