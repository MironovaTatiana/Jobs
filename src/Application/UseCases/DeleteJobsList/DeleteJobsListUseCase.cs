namespace Application.UseCases.DeleteJobsList
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain;

    /// <summary>
    /// �������� ��������
    /// </summary>
    public sealed class DeleteJobsListUseCase : IDeleteJobsListUseCase
    {
        #region ����

        /// <summary>
        /// �����������
        /// </summary>
        private readonly IJobsRepository _jobsRepository;

        #endregion

        #region �����������

        /// <summary>
        /// �������� ����������� �������
        /// </summary>
        public DeleteJobsListUseCase(IJobsRepository jobsRepository)
        {
            _jobsRepository = jobsRepository;
        }

        #endregion

        #region ������

        /// <summary>
        /// ��������� ��������� �����
        /// </summary>
        void IDeleteJobsListUseCase.SetOutputPort(IOutputPort outputPort)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
