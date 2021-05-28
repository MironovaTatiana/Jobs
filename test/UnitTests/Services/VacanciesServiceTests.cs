using Application.Services;
using Infrastructure;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;

namespace UnitTests.Services
{
    /// <summary>
    /// Класс для тестирования сервиса вакансий
    /// </summary>
    [TestFixture(TestOf = typeof(VacanciesService))]
    public class VacanciesServiceTests
    {
        [Test(Description = "Тест выполняет получение n вакансий и не возвращает исключения")]
        public async Task GetVacanciesListAsync_ThrowsNoException()
        {
            // Arrange
            var mockService = new Mock<IOptions<HhRuConfig>>();
            var config = new HhRuConfig() 
            { 
              HeaderKey = "User-Agent", 
              HeaderValue = "d-fens HttpClient",
              RequestVacanciesByCount = "https://api.hh.ru/vacancies/?per_page={count}&only_with_salary=true" 
            };

            mockService
                .Setup(e => e.Value)
                .Returns(config);

            var vacanciesService = new VacanciesService(mockService.Object);

            int count = 3;

            // Act
            var result = await vacanciesService.GetVacanciesListAsync(count);

            // Assert
            Assert.DoesNotThrow(() => new HttpRequestException());
        }

        [Test(Description = "Тест выполняет получение вакансии по идентификатору и не возвращает исключения")]
        public async Task GetVacancyByIdAsync_ThrowsNoException()
        {
            // Arrange
            var mockService = new Mock<IOptions<HhRuConfig>>();
            var config = new HhRuConfig() 
            { 
                HeaderKey = "User-Agent", 
                HeaderValue = "d-fens HttpClient", 
                RequestVacancyById = "https://api.hh.ru/vacancies/{id}" 
            };

            mockService
                .Setup(e => e.Value)
                .Returns(config);

            var vacanciesService = new VacanciesService(mockService.Object);

            int id = 43904540;

            // Act
            var result = await vacanciesService.GetVacancyByIdAsync(id);

            // Assert
            Assert.DoesNotThrow(() => new HttpRequestException());
        }
    }
}
