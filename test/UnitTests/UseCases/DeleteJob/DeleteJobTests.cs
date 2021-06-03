using System.Linq;
using System.Threading.Tasks;
using Domain;
using Moq;
using NUnit.Framework;
using UnitTests.UseCases;
using Application.UseCases.DeleteJob;

namespace UnitTests
{
    /// <summary>
    /// Класс для тестирования удаления вакансии по идентификатору
    /// </summary>
    [TestFixture(TestOf = typeof(DeleteJobUseCase))]
    public class DeleteJobTests
    {
        [Test(Description = "Тест пытается удалить вакансию из пустой БД и получает сообщение об ошибке")]
        public async Task ExecuteAsync_DeleteJobThrowsFail()
        {
            // Arrange
            var count = 0;
            var id = 43904540;
            var mockRepository = new Mock<IJobsRepository>();
            var outputMock = new Mock<IOutputPort>();
            var expectedMessage = string.Empty;

            mockRepository
                .Setup(r => r.GetCountAsync())
                .Returns(new ValueTask<int>(count));

            outputMock
                .Setup(x => x.Fail(It.IsAny<string>()))
                .Callback<string>(s => expectedMessage = s);

            IDeleteJobUseCase useCase = new DeleteJobUseCase(mockRepository.Object);
            useCase.SetOutputPort(outputMock.Object);

            // Act
            await useCase.ExecuteAsync(id);

            // Assert
            Assert.AreEqual("База данных не содержит записей", expectedMessage);
        }

        [Test(Description = "Тест проверяет удаление вакансии по идентификатору")]
        public async Task ExecuteAsync_CompareEventsBeforeAndAfterDeleting()
        {
            // Arrange
            var mockRepository = new Mock<IJobsRepository>();
            var jobs = TestData.GetVacanciesList();
            var id = 43904540;
            var job = new ValueTask<Job>(TestData.GetJob());

            IDeleteJobUseCase useCase = new DeleteJobUseCase(mockRepository.Object);

            mockRepository
                .Setup(r => r.GetCountAsync())
                .Returns(new ValueTask<int>(jobs.Count));

            mockRepository
                .Setup(r => r.GetJobByIdAsync(It.IsAny<int>()))
                .Returns(() => job);

            mockRepository
                .Setup(r => r.DeleteJobByIdAsync(It.IsAny<int>()))
                .Callback(
                    () => jobs.RemoveAt(0));

            // Act
            await useCase.ExecuteAsync(id);

            // Assert
            Assert.AreEqual(2, jobs.Count());
        }
    }
}
