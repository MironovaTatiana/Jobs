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
        public GetJobsListUseCase(IVacanciesService vacanciesService, IJobsRepository jobsRepository)
        {
            _vacanciesService = vacanciesService;
            _jobsRepository = jobsRepository;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Получение списка вакансий
        /// </summary>
        public async ValueTask<IEnumerable<IJob>> ExecuteAsync(int count)
        {
            var countFromDb = this._jobsRepository.GetCount();
            IEnumerable<JobDto> vacanciesDto = new List<JobDto>();

            if (count > await countFromDb)
            {
                vacanciesDto = (IEnumerable<JobDto>)await _vacanciesService.GetVacanciesList(count);

                var vacancies = JobDtoHelper.ConvertJobDtoListToJobList(vacanciesDto);

                foreach (var vacancy in vacancies)
                {
                    await _jobsRepository.AddJob(vacancy).ConfigureAwait(false);
                }
            }

            var vacanciesFromDb = await _jobsRepository.GetJobsLimitN(count);

            vacanciesDto = JobDtoHelper.ConvertJobListToJobDtoList(vacanciesFromDb);

            var sortedVacancies = new List<JobDto>();

            foreach (JobDto job in vacanciesDto.OrderBy(w => w.Name))
            {
                sortedVacancies.Add(job);
            }

            if (vacanciesDto.Count() == count)
            {
                this._outputPort?.Ok("Список вакансий получен", vacanciesDto);
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
