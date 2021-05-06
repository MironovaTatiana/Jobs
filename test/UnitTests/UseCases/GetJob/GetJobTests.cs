using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services;
using Domain;
using Moq;
using NUnit.Framework;
using Application.Dtos;
using Application.UseCases.GetJob;
using Application;
using UnitTests.UseCases;

namespace UnitTests
{
    /// <summary>
    /// Класс для тестирования получения вакансии по идентификатору
    /// </summary>
    [TestFixture(TestOf = typeof(GetJobUseCase))]
    public class GetJobTests
    {
        [Test(Description = "Тест выполняет сравнение id полученной вакансии и переданного параметра")]
        public async Task ExecuteAsync_CompareJobIdAndParametrValue()
        {
            // Arrange
            var id = 43904540;
            var mockService = new Mock<IVacanciesService>();
            var mockRepository = new Mock<IJobsRepository>();

            mockService
                .Setup(r => r.GetVacancyByIdAsync(id))
                .Returns(new ValueTask<JobDto>(TestData.GetVacancy()));

            IGetJobUseCase useCase = new GetJobUseCase(mockService.Object, mockRepository.Object);

            // Act
            IJob result = await useCase.ExecuteAsync(id);

            // Assert
            Assert.AreEqual(id, result.Id);
        }

        [Test(Description = "Тест выполняет получение вакансии по невалидному идентификатору и ожидает сообщение об отрицательном результате")]
        [TestCase(2)]
        [TestCase(111)]
        public async Task ExecuteAsync_GetJobByInvalidIdReturnFail(int id)
        {
            // Arrange
            var mockService = new Mock<IVacanciesService>();
            var mockRepository = new Mock<IJobsRepository>();
            var outputMock = new Mock<IOutputPort>();
            var expectedMessage = string.Empty;

            outputMock
                .Setup(x => x.Fail(It.IsAny<string>()))
                .Callback<string>(s => expectedMessage = s);

            mockService
                .Setup(r => r.GetVacancyByIdAsync(id))
                .Returns(null);

            IGetJobUseCase useCase = new GetJobUseCase(mockService.Object, mockRepository.Object);

            useCase.SetOutputPort(outputMock.Object);

            // Act
            await useCase.ExecuteAsync(id);

            // Assert
            Assert.AreEqual("Возникла ошибка во время получения вакансии", expectedMessage);
        }

        [Test(Description = "Тест выполняет получение вакансии по валидному идентификатору и ожидает сообщение о положительном результате")]
        [TestCase(43904540)]
        [TestCase(43955356)]
        public async Task ExecuteAsync_GetJobByValidIdReturnOk(int id)
        {
            // Arrange
            var mockService = new Mock<IVacanciesService>();
            var mockRepository = new Mock<IJobsRepository>();
            var outputMock = new Mock<IOutputPort>();
            var expectedMessage = string.Empty;

            outputMock
                .Setup(x => x.Ok(It.IsAny<string>(), It.IsAny<IJob>()))
                .Callback<string, IJob>((s, e) => expectedMessage = s);

            mockService
                .Setup(r => r.GetVacancyByIdAsync(id))
                .Returns(new ValueTask<JobDto>(TestData.GetVacancy()));
            IGetJobUseCase useCase = new GetJobUseCase(mockService.Object, mockRepository.Object);

            useCase.SetOutputPort(outputMock.Object);

            // Act
            await useCase.ExecuteAsync(id);

            // Assert
            Assert.AreEqual("Вакансия получена", expectedMessage);
        }

        [Test(Description = "Тест выполняет получение вакансии с отключенным интернет-соединением и ожидает ошибку")]
        [TestCase(2)]
        public void ExecuteAsync_GetJobFromApiWithoutInternetConnectionThrowsException(int id)
        {
            // Arrange
            var mockService = new Mock<IVacanciesService>();
            var mockRepository = new Mock<IJobsRepository>();

            mockService
                .Setup(r => r.GetVacancyByIdAsync(id))
                .Throws(new JobsException("Отсутствует интернет-соединение"));

            IGetJobUseCase useCase = new GetJobUseCase(mockService.Object, mockRepository.Object);

            // Act
            AsyncTestDelegate result = () => useCase.ExecuteAsync(id);

            // Assert
            Assert.ThrowsAsync<JobsException>(result);
        }
    }
}
