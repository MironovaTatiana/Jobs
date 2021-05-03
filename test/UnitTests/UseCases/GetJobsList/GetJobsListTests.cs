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
    /// Класс для тестирования получения списка вакансий
    /// </summary>
    [TestFixture(TestOf = typeof(GetJobsListUseCase))]
    public class GetJobsListTests
    {
        [Test(Description = "Тест выполняет сравнение количества полученных вакансий и переданного параметра")]
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

        [Test(Description = "Тест выполняет сравнение количества полученных вакансий и переданного параметра и ожидает сообщение об отрицательном результате")]
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
            Assert.AreEqual("Возникла ошибка во время получения списка вакансий", expectedMessage);
        }

        [Test(Description = "Тест выполняет сравнение количества полученных вакансий и переданного параметра и ожидает сообщение о положительном результате")]
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
            Assert.AreEqual("Список вакансий получен", expectedMessage);
        }

        /// <summary>
        /// Получение списка вакансий
        /// </summary>
        private static IEnumerable<JobDto> GetVacanciesList()
        {
            return new List<JobDto>()
            {
                new JobDto {  Name = "Специалист по работе с клиентами", Id = 43904540, SalaryFrom = 1000, SalaryTo = 3000 },
                new JobDto {  Name = "Менеджер по продажам", Id = 43904541, SalaryFrom = 1000, SalaryTo = 4000 },
                new JobDto {  Name = "Аналитик", Id = 43904542, SalaryFrom = 1000, SalaryTo = 5000 },
            };
        }
    }
}
