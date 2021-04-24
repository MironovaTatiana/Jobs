namespace Application.UseCases.DeleteJobsList
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain;

    /// <summary>
    /// Удаление вакансий
    /// </summary>
    public sealed class DeleteJobsListUseCase : IDeleteJobsListUseCase
    {
        #region Поля

        /// <summary>
        /// Репозиторий
        /// </summary>
        private readonly IJobsRepository _jobsRepository;

        #endregion

        #region Конструктор

        /// <summary>
        /// Удаление календарных событий
        /// </summary>
        public DeleteJobsListUseCase(IJobsRepository jobsRepository)
        {
            _jobsRepository = jobsRepository;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Установка выходного порта
        /// </summary>
        void IDeleteJobsListUseCase.SetOutputPort(IOutputPort outputPort)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
