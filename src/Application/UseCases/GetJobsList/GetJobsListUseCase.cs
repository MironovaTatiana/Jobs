using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Services;
using Domain;


namespace Application.UseCases.GetJobsList
{
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
        public async Task<IEnumerable<IJob>> ExecuteAsync(int count)
        {
            var countFromDb = this._jobsRepository.GetCountAsync();
            IEnumerable<JobDto> vacanciesDto = new List<JobDto>();

            if (count > await countFromDb)
            {
                try
                {
                    //Берем вакансии из API
                    vacanciesDto = (IEnumerable<JobDto>)await _vacanciesService.GetVacanciesListAsync(count);

                    var vacancies = vacanciesDto.ConvertJobDtoListToJobList();

                    foreach (var vacancy in vacancies)
                    {
                        await _jobsRepository.AddJobAsync(vacancy).ConfigureAwait(false);
                    }

                    if (vacanciesDto.Count() == count)
                    {
                        this._outputPort?.Ok("Список вакансий получен с сайта", vacanciesDto);
                    }
                    else
                    {
                        this._outputPort?.Fail("Возникла ошибка во время получения списка вакансий с сайта");
                    }
                }
                catch
                {
                    this._outputPort?.Fail("Отсутствует интернет-соединение");
                    throw new JobsException("Отсутствует интернет-соединение");
                }
            }
            else
            {
                //Берем вакансии из БД
                var vacanciesFromDb = await _jobsRepository.GetJobsLimitNAsync(count);

                vacanciesDto = vacanciesFromDb.ConvertJobListToJobDtoList();

                if (vacanciesDto.Count() == count)
                {
                    this._outputPort?.Ok("Список вакансий получен из БД", vacanciesDto);
                }
                else
                {
                    this._outputPort?.Fail("Возникла ошибка во время получения списка вакансий из БД");
                }
            }

            var sortedVacancies = new List<JobDto>();

            foreach (JobDto job in vacanciesDto.OrderBy(w => w.Name))
            {
                sortedVacancies.Add(job);
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
