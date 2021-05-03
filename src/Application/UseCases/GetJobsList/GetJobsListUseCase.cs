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
    /// ��������� ������ ��������
    /// </summary>
    public sealed class GetJobsListUseCase : IGetJobsListUseCase
    {
        #region ����

        /// <summary>
        /// ������ ��������
        /// </summary>
        private readonly IVacanciesService _vacanciesService;

        /// <summary>
        /// �������� ����
        /// </summary>
        private IOutputPort _outputPort;

        #endregion

        #region �����������

        /// <summary>
        /// ��������� ������ ��������
        /// </summary>
        public GetJobsListUseCase(IVacanciesService vacanciesService)
        {
            _vacanciesService = vacanciesService;
        }

        #endregion

        #region ������

        /// <summary>
        /// ��������� ������ ��������
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
                this._outputPort?.Ok("������ �������� �������", vacancies);
            }
            else
            {
                this._outputPort?.Fail("�������� ������ �� ����� ��������� ������ ��������");
            }

            return sortedVacancies;
        }

        /// <summary>
        /// ��������� ��������� �����
        /// </summary>
        void IGetJobsListUseCase.SetOutputPort(IOutputPort outputPort) => this._outputPort = outputPort;

        #endregion
    }
}
