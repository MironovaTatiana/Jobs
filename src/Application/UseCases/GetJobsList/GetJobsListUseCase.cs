namespace Application.UseCases.GetJobsList
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Application.Dtos;
    using Application.Services;
    using Domain;


    /// <summary>
    /// Получение списка вакансий
    /// </summary>
    public sealed class GetJobsListUseCase : IGetJobsListUseCase
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
        public GetJobsListUseCase(IVacanciesService vacanciesService)
        {
            _vacanciesService = vacanciesService;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Получение списка вакансий
        /// </summary>
        public async ValueTask<IEnumerable<IJob>> ExecuteAsync(int count)
        {
            IEnumerable<IJob> vacancies = await _vacanciesService.GetVacanciesList(count);
            var sortedVacancies = new List<JobDto>();

            foreach (JobDto job in vacancies.OrderBy(w => w.Name))
            {
                sortedVacancies.Add(job);
            }

            if (vacancies.Count() == count)
            {
                this._outputPort?.Ok("Список вакансий получен", vacancies);
            }
            else
            {
                this._outputPort?.Fail("Возникла ошибка во время получения списка вакансий");
            }

            return sortedVacancies;
        }

        /// <summary>
        /// Установка выходного порта
        /// </summary>
        void IGetJobsListUseCase.SetOutputPort(IOutputPort outputPort) => this._outputPort = outputPort;

        #endregion
    }
}
