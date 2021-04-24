namespace Application.UseCases.GetJobsList
{
    using System;
    using System.Threading.Tasks;
    using Domain;


    /// <summary>
    /// ��������� ������ ��������
    /// </summary>
    public sealed class GetJobsListUseCase : IGetJobsListUseCase
    {
        #region ����

        /// <summary>
        /// �����������
        /// </summary>
        private readonly IJobsRepository _jobsRepository;

        #endregion

        #region �����������

        /// <summary>
        /// ��������� ������ ��������
        /// </summary>
        public GetJobsListUseCase(IJobsRepository jobsRepository)
        {
            _jobsRepository = jobsRepository;
        }

        #endregion

        #region ������

        /// <summary>
        /// ��������� ��������� �����
        /// </summary>
        void IGetJobsListUseCase.SetOutputPort(IOutputPort outputPort)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
