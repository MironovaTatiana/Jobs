using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services;
using Domain;
using Moq;
using NUnit.Framework;
using Application.Dtos;
using Application.UseCases.GetJob;

namespace UnitTests
{
    /// <summary>
    /// Класс для тестирования получения вакансии по идентификатору
    /// </summary>
    [TestFixture(TestOf = typeof(GetJobUseCase))]
    public class GetJobTests
    {
        [Test(Description = "Тест выполняет сравнение id полученной вакансии и переданного параметра")]
        [TestCase(43904540)]
        public async Task ExecuteAsync_CompareJobIdAndParametrValue(int id)
        {
            // Arrange
            var mock = new Mock<IVacanciesService>();

            mock.Setup(r => r.GetVacancyById(id)).Returns(new ValueTask<IJob>(GetVacancy()));

            IGetJobUseCase useCase = new GetJobUseCase(mock.Object);

            // Act
            IJob result = await useCase.ExecuteAsync(id);

            // Assert
            Assert.AreEqual(id, result.Id);
        }

        [Test(Description = "Тест выполняет получение вакансии по невалидному идентификатору и ожидает сообщение об отрицательном результате")]
        [TestCase(2)]
        public async Task ExecuteAsync_GetJobByInvalidIdReturnFail(int id)
        {
            // Arrange
            var mock = new Mock<IVacanciesService>();
            var outputMock = new Mock<IOutputPort>();
            var expectedMessage = string.Empty;

            outputMock
                .Setup(x => x.Fail(It.IsAny<string>()))
                .Callback<string>(s => expectedMessage = s);

            mock.Setup(r => r.GetVacancyById(id)).Returns(null);
            IGetJobUseCase useCase = new GetJobUseCase(mock.Object);

            useCase.SetOutputPort(outputMock.Object);

            // Act
            await useCase.ExecuteAsync(id);

            // Assert
            Assert.AreEqual("Возникла ошибка во время получения вакансии", expectedMessage);
        }

        [Test(Description = "Тест выполняет получение вакансии по валидному идентификатору и ожидает сообщение о положительном результате")]
        [TestCase(43904540)]
        public async Task ExecuteAsync_GetJobByValidIdReturnOk(int id)
        {
            // Arrange
            var mock = new Mock<IVacanciesService>();
            var outputMock = new Mock<IOutputPort>();
            var expectedMessage = string.Empty;

            outputMock.Setup(x => x.Ok(It.IsAny<string>(), It.IsAny<IJob>()))
                       .Callback<string, IJob>((s, e) => expectedMessage = s);

            mock.Setup(r => r.GetVacancyById(id)).Returns(new ValueTask<IJob>(GetVacancy()));
            IGetJobUseCase useCase = new GetJobUseCase(mock.Object);

            useCase.SetOutputPort(outputMock.Object);

            // Act
            await useCase.ExecuteAsync(id);

            // Assert
            Assert.AreEqual("Вакансия получена", expectedMessage);
        }

        /// <summary>
        /// Получение вакансии
        /// </summary>
        private static JobDto GetVacancy() => new() { Name = "Специалист по работе с клиентами", Id = 43904540, SalaryFrom = 1000, SalaryTo = 3000 };
    }
}
