using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services;
using Application.UseCases.GetJobsList;
using Domain;
using Moq;
using NUnit.Framework;
using Application;
using System;
using UnitTests.UseCases;

namespace UnitTests
{
    /// <summary>
    /// Класс для тестирования получения списка вакансий
    /// </summary>
    [TestFixture(TestOf = typeof(GetJobsListUseCase))]
    public class GetJobsListTests
    {
        [Test(Description = "Тест выполняет попытку получения вакансий с сайта, так как параметр запроса больше числа записей в БД, " +
            "с неактивным интернетом и ожидает ошибку")]
        [TestCase(4)]
        [TestCase(3)]
        [TestCase(2)]
        public Task ExecuteAsync_GetJobsFromApiWithoutInternetConnectionThrowsException(int count)
        {
            // Arrange
            var mockService = new Mock<IVacanciesService>();
            var mockRepository = new Mock<IJobsRepository>();

            mockService
                .Setup(r => r.GetVacanciesListAsync(count))
                .Throws(new JobsException("Отсутствует интернет-соединение"));

            mockRepository
              .Setup(r => r.GetCountAsync())
              .Returns(new ValueTask<int>(count - 1));

            IGetJobsListUseCase useCase = new GetJobsListUseCase(mockService.Object, mockRepository.Object);

            // Act
            AsyncTestDelegate result = ()=> useCase.ExecuteAsync(count);

            // Assert
            Assert.ThrowsAsync<JobsException>(result);
            return Task.CompletedTask;
        }

        [Test(Description = "Тест выполняет получение вакансий из БД, так как параметр запроса меньше или равен числу записей в БД, " +
            "с неактивным интернетом и ожидает сообщение о положительном результате")]
        public async Task ExecuteAsync_GetJobsFromDataBaseWithoutInternetConnectionThrowsOk()
        {
            // Arrange
            var count = 3;
            var mockService = new Mock<IVacanciesService>();
            var mockRepository = new Mock<IJobsRepository>();
            var outputMock = new Mock<IOutputPort>();
            var expectedMessage = string.Empty;

            mockService
                .Setup(r => r.GetVacanciesListAsync(count))
                .Throws(new JobsException("Отсутствует интернет-соединение"));

            mockRepository
                .Setup(r => r.GetCountAsync())
                .Returns(new ValueTask<int>(count));
            mockRepository
                .Setup(r => r.GetJobsLimitNAsync(count))
                .Returns(new ValueTask<IEnumerable<Job>>(TestData.GetVacanciesList()));

            outputMock
                .Setup(x => x.Ok(It.IsAny<string>(), It.IsAny<IEnumerable<IJob>>()))
                .Callback<string, IEnumerable<IJob>>((s, e) => expectedMessage = s);

            IGetJobsListUseCase useCase = new GetJobsListUseCase(mockService.Object, mockRepository.Object);
            useCase.SetOutputPort(outputMock.Object);

            // Act
            await useCase.ExecuteAsync(count);

            // Assert
            Assert.AreEqual("Список вакансий получен из БД", expectedMessage);
        }

        [Test(Description = "Тест выполняет получение вакансий из БД, так как параметр запроса меньше или равен числу записей в БД, " +
            "с активным интернетом и ожидает сообщение о положительном результате и ожидается что число записей в БД до и после будет одинаковым")]
        public async Task ExecuteAsync_GetJobsFromDataBaseWithInternetConnectionThrowsOk()
        {
            // Arrange
            var count = 3;
            var mockService = new Mock<IVacanciesService>();
            var mockRepository = new Mock<IJobsRepository>();
            var outputMock = new Mock<IOutputPort>();
            var expectedMessage = string.Empty;

            mockService
                .Setup(r => r.GetVacanciesListAsync(count))
                .Returns(new ValueTask<IEnumerable<IJob>>(TestData.GetVacanciesDtoList())); 

            mockRepository
                .Setup(r => r.GetCountAsync())
                .Returns(new ValueTask<int>(count));
            mockRepository
                .Setup(r => r.GetJobsLimitNAsync(count))
                .Returns(new ValueTask<IEnumerable<Job>>(TestData.GetVacanciesList()));

            outputMock
                .Setup(x => x.Ok(It.IsAny<string>(), It.IsAny<IEnumerable<IJob>>()))
                .Callback<string, IEnumerable<IJob>>((s, e) => expectedMessage = s);

            IGetJobsListUseCase useCase = new GetJobsListUseCase(mockService.Object, mockRepository.Object);
            useCase.SetOutputPort(outputMock.Object);

            // Act
            var jobs = await useCase.ExecuteAsync(count);

            // Assert
            Assert.AreEqual("Список вакансий получен из БД", expectedMessage);
            Assert.AreEqual(TestData.GetVacanciesList().Count(), jobs.Count());
        }


        [Test(Description = "Тест выполняет получение вакансий из APi, так как параметр запроса больше числа записей в БД, " +
            "с активным интернетом и ожидает сообщение о положительном результате и ожидается что число записей в БД увеличится на переданный параметр")]
        public async Task ExecuteAsync_GetJobsFromApiWithInternetConnectionThrowsOk()
        {
            // Arrange
            var count = 3;
            var mockService = new Mock<IVacanciesService>();
            var mockRepository = new Mock<IJobsRepository>();
            var outputMock = new Mock<IOutputPort>();
            var expectedMessage = string.Empty;

            mockService
                .Setup(r => r.GetVacanciesListAsync(count))
                .Returns(new ValueTask<IEnumerable<IJob>>(TestData.GetVacanciesDtoList()));

            mockRepository
                .Setup(r => r.GetCountAsync())
                .Returns(new ValueTask<int>(0));

            outputMock
                .Setup(x => x.Ok(It.IsAny<string>(), It.IsAny<IEnumerable<IJob>>()))
                .Callback<string, IEnumerable<IJob>>((s, e) => expectedMessage = s);

            IGetJobsListUseCase useCase = new GetJobsListUseCase(mockService.Object, mockRepository.Object);
            useCase.SetOutputPort(outputMock.Object);

            // Act
            await useCase.ExecuteAsync(count);

            // Assert
            Assert.AreEqual("Список вакансий получен с сайта", expectedMessage);
        }
    }
}
