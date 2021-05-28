using System;
using System.Threading.Tasks;
using Domain;


namespace Application.UseCases.DeleteJob
{
    /// <summary>
    /// Удаление вакансии
    /// </summary>
    public sealed class DeleteJobUseCase : IDeleteJobUseCase
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
        /// Удаление вакансии
        /// </summary>
        public DeleteJobUseCase(IJobsRepository jobsRepository)
        {
            _jobsRepository = jobsRepository;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Удаление вакансии по идентификатору
        /// </summary>
        public async Task ExecuteAsync(int id)
        {
            if (await _jobsRepository.GetCountAsync() > 0)
            {
                if (await _jobsRepository.GetJobByIdAsync(id) != default(Job))
                {
                    await _jobsRepository.DeleteJobByIdAsync(id);
                    _outputPort?.Ok($"Из базы данных удалена вакансия с идентификатором {id}");
                }
                else
                {
                    _outputPort?.Fail($"Вакансия с идентификатором {id} не найдена");
                }
            }
            else
            {
                _outputPort?.Fail("База данных не содержит записей");
            }           
        }

        /// <summary>
        /// Установка выходного порта
        /// </summary>
        void IDeleteJobUseCase.SetOutputPort(IOutputPort outputPort) => this._outputPort = outputPort;

        #endregion
    }
}
