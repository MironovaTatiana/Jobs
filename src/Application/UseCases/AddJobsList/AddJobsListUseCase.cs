namespace Application.UseCases.AddJobsList
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Application.Dtos;
    using Domain;

    /// <summary>
    /// Добавление вакансий
    /// Валидирует вакансию перед добавлением, добавляет вакансию в БД и возвращает добавленные вакансии
    /// </summary>
    public sealed class AddJobsListUseCase : IAddJobsListUseCase
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
        /// Добавление вакансий
        /// </summary>
        public AddJobsListUseCase(IJobsRepository jobsRepository)
        {
            _jobsRepository = jobsRepository;
        }

        #endregion

        #region Задачи

        /// <summary>
        /// Выполнение
        /// </summary>
        public Task ExecuteAsync(IEnumerable<JobDto> jobsList)
        {
            var validJobsList = new List<Job>();

            var jobs = jobsList.Select(x => new Job
            {
            });

            foreach (var job in jobs)
            {
                if (!(job != null))
                {
                    this._outputPort?.Fail("Список содержит невалидную вакансию");

                    return Task.CompletedTask;
                    //throw new JobsException("Вакансии не добавлены");
                }

                validJobsList.Add(job);               
            }

            this._outputPort?.Ok("Все вакансии в списке валидные", validJobsList);

            return this._jobsRepository.AddRange(validJobsList);
        }

        #endregion

        #region Методы

        /// <summary>
        /// Установка выходного порта
        /// </summary>
        void IAddJobsListUseCase.SetOutputPort(IOutputPort outputPort) => this._outputPort = outputPort;

        #endregion
    }
}
