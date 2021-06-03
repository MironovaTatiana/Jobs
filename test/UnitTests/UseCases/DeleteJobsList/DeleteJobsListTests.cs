using System.Linq;
using System.Threading.Tasks;
using Domain;
using Moq;
using NUnit.Framework;
using UnitTests.UseCases;
using Application.UseCases.DeleteJobsList;

namespace UnitTests
{
    /// <summary>
    /// Класс для тестирования удаления списка вакансий
    /// </summary>
    [TestFixture(TestOf = typeof(DeleteJobsListUseCase))]
    public class DeleteJobsListTests
    {
        [Test(Description = "Тест пытается удалить записи из пустой БД и получает сообщение обошибке")]
        public async Task ExecuteAsync_DeleteJobsListThrowsFail()
        {
            // Arrange
            var count = 0;
            var mockRepository = new Mock<IJobsRepository>();
            var outputMock = new Mock<IOutputPort>();
            var expectedMessage = string.Empty;

            mockRepository
                .Setup(r => r.GetCountAsync())
                .Returns(new ValueTask<int>(count));

            outputMock
                .Setup(x => x.Fail(It.IsAny<string>()))
                .Callback<string>(s => expectedMessage = s);

            IDeleteJobsListUseCase useCase = new DeleteJobsListUseCase(mockRepository.Object);
            useCase.SetOutputPort(outputMock.Object);

            // Act
            await useCase.ExecuteAsync();

            // Assert
            Assert.AreEqual("База данных не содержит записей", expectedMessage);
        }

        [Test(Description = "Тест проверяет удаление вакансий")]
        public async Task ExecuteAsync_CompareEventsBeforeAndAfterDeleting()
        {
            // Arrange
            var mockRepository = new Mock<IJobsRepository>();
            var jobs = TestData.GetVacanciesList();

            IDeleteJobsListUseCase useCase = new DeleteJobsListUseCase(mockRepository.Object);

            mockRepository
                .Setup(r => r.GetCountAsync())
                .Returns(new ValueTask<int>(jobs.Count));

            mockRepository
                .Setup(r => r.DeleteJobsListAsync())
                .Callback(
                    () => jobs.RemoveAll(s=> s.Name != string.Empty));

            // Act
            await useCase.ExecuteAsync();

            // Assert
            Assert.AreEqual(0, jobs.Count());
        }
    }
}
