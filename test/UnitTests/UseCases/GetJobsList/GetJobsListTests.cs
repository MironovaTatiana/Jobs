using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services;
using Application.UseCases.GetJobsList;
using Domain;
using Moq;
using NUnit.Framework;
using Application.Dtos;

namespace UnitTests
{
    /// <summary>
    /// ����� ��� ������������ ��������� ������ ��������
    /// </summary>
    [TestFixture(TestOf = typeof(GetJobsListUseCase))]
    public class GetJobsListTests
    {
        [Test(Description = "���� ��������� ��������� ���������� ���������� �������� � ����������� ���������")]
        [TestCase(3)]
        public async Task ExecuteAsync_CompareJobsCountAndParametrValue(int count)
        {
            // Arrange
            var mock = new Mock<IVacanciesService>();

            mock.Setup(r => r.GetVacanciesList(count)).Returns(new ValueTask<IEnumerable<IJob>>(GetVacanciesList()));

            IGetJobsListUseCase useCase = new GetJobsListUseCase(mock.Object);

            // Act
            IEnumerable<IJob> result = await useCase.ExecuteAsync(count);

            // Assert
            Assert.AreEqual(count, result.Count());
        }

        [Test(Description = "���� ��������� ��������� ���������� ���������� �������� � ����������� ��������� � ������� ��������� �� ������������� ����������")]
        [TestCase(2)]
        public async Task ExecuteAsync_CompareJobsCountAndParametrValueReturnFail(int count)
        {
            // Arrange
            var mock = new Mock<IVacanciesService>();
            var outputMock = new Mock<IOutputPort>();
            var expectedMessage = string.Empty;

            outputMock
                .Setup(x => x.Fail(It.IsAny<string>()))
                .Callback<string>(s => expectedMessage = s);

            mock.Setup(r => r.GetVacanciesList(count)).Returns(new ValueTask<IEnumerable<IJob>>(GetVacanciesList()));
            IGetJobsListUseCase useCase = new GetJobsListUseCase(mock.Object);

            useCase.SetOutputPort(outputMock.Object);

            // Act
            await useCase.ExecuteAsync(count);

            // Assert
            Assert.AreEqual("�������� ������ �� ����� ��������� ������ ��������", expectedMessage);
        }

        [Test(Description = "���� ��������� ��������� ���������� ���������� �������� � ����������� ��������� � ������� ��������� � ������������� ����������")]
        [TestCase(3)]
        public async Task ExecuteAsync_CompareJobsCountAndParametrValueReturnOk(int count)
        {
            // Arrange
            var mock = new Mock<IVacanciesService>();
            var outputMock = new Mock<IOutputPort>();
            var expectedMessage = string.Empty;

            outputMock.Setup(x => x.Ok(It.IsAny<string>(), It.IsAny<IEnumerable<IJob>>()))
                       .Callback<string, IEnumerable<IJob>>((s, e) => expectedMessage = s);

            mock.Setup(r => r.GetVacanciesList(count)).Returns(new ValueTask<IEnumerable<IJob>>(GetVacanciesList()));
            IGetJobsListUseCase useCase = new GetJobsListUseCase(mock.Object);

            useCase.SetOutputPort(outputMock.Object);

            // Act
            await useCase.ExecuteAsync(count);

            // Assert
            Assert.AreEqual("������ �������� �������", expectedMessage);
        }

        /// <summary>
        /// ��������� ������ ��������
        /// </summary>
        private static IEnumerable<JobDto> GetVacanciesList()
        {
            return new List<JobDto>()
            {
                new JobDto {  Name = "���������� �� ������ � ���������", Id = 43904540, SalaryFrom = 1000, SalaryTo = 3000 },
                new JobDto {  Name = "�������� �� ��������", Id = 43904541, SalaryFrom = 1000, SalaryTo = 4000 },
                new JobDto {  Name = "��������", Id = 43904542, SalaryFrom = 1000, SalaryTo = 5000 },
            };
        }
    }
}
