using System;
using System.Threading.Tasks;
using Domain;


namespace Application.UseCases.DeleteJobsList
{
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

        /// <summary>
        /// Выходной порт
        /// </summary>
        private IOutputPort _outputPort;

        #endregion

        #region Конструктор

        /// <summary>
        /// Удаление вакансий
        /// </summary>
        public DeleteJobsListUseCase(IJobsRepository jobsRepository)
        {
            _jobsRepository = jobsRepository;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Удаление вакансий
        /// </summary>
        public async Task ExecuteAsync()
        {
            var count = await _jobsRepository.GetCountAsync();

            if (count > 0)
            {
                await _jobsRepository.DeleteJobsListAsync();
                _outputPort?.Ok($"Удалено {count} записей из БД");
            }
            else
            {
                _outputPort?.Fail("База данных не содержит записей");
            }
        }

        /// <summary>
        /// Установка выходного порта
        /// </summary>
        void IDeleteJobsListUseCase.SetOutputPort(IOutputPort outputPort) => this._outputPort = outputPort;

        #endregion
    }
}
