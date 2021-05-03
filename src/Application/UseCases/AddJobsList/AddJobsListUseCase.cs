namespace Application.UseCases.AddJobsList
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Application.Dtos;
    using Domain;

    /// <summary>
    /// ���������� ��������
    /// ���������� �������� ����� �����������, ��������� �������� � �� � ���������� ����������� ��������
    /// </summary>
    public sealed class AddJobsListUseCase : IAddJobsListUseCase
    {
        #region ����

        /// <summary>
        /// �����������
        /// </summary>
        private readonly IJobsRepository _jobsRepository;

        /// <summary>
        /// �������� ����
        /// </summary>
        private IOutputPort _outputPort;

        #endregion

        #region �����������

        /// <summary>
        /// ���������� ��������
        /// </summary>
        public AddJobsListUseCase(IJobsRepository jobsRepository)
        {
            _jobsRepository = jobsRepository;
        }

        #endregion

        #region ������

        /// <summary>
        /// ����������
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
                    this._outputPort?.Fail("������ �������� ���������� ��������");

                    return Task.CompletedTask;
                    //throw new JobsException("�������� �� ���������");
                }

                validJobsList.Add(job);               
            }

            this._outputPort?.Ok("��� �������� � ������ ��������", validJobsList);

            return this._jobsRepository.AddRange(validJobsList);
        }

        #endregion

        #region ������

        /// <summary>
        /// ��������� ��������� �����
        /// </summary>
        void IAddJobsListUseCase.SetOutputPort(IOutputPort outputPort) => this._outputPort = outputPort;

        #endregion
    }
}
