using System.Collections.Generic;
using Application.Dtos;
using Domain;

namespace UnitTests.UseCases
{
    /// <summary>
    /// Класс с данными для тестов
    /// </summary>
    class TestData
    {
        #region Методы

        /// <summary>
        /// Получение списка вакансий
        /// </summary>
        public static IEnumerable<JobDto> GetVacanciesDtoList()
        {
            return new List<JobDto>()
            {
                new JobDto {  Name = "Специалист по работе с клиентами", Id = 43904540, SalaryFrom = 1000, SalaryTo = 3000 },
                new JobDto {  Name = "Менеджер по продажам", Id = 43904541, SalaryFrom = 1000, SalaryTo = 4000 },
                new JobDto {  Name = "Аналитик", Id = 43904542, SalaryFrom = 1000, SalaryTo = 5000 },
            };
        }

        /// <summary>
        /// Получение списка вакансий
        /// </summary>
        public static List<Job> GetVacanciesList()
        {
            return new List<Job>()
            {
                new Job {  Name = "Специалист по работе с клиентами", Id = 43904540, SalaryFrom = 1000, SalaryTo = 3000 },
                new Job {  Name = "Менеджер по продажам", Id = 43904541, SalaryFrom = 1000, SalaryTo = 4000 },
                new Job {  Name = "Аналитик", Id = 43904542, SalaryFrom = 1000, SalaryTo = 5000 },
            };
        }

        /// <summary>
        /// Получение вакансии
        /// </summary>
        public static JobDto GetVacancy() => new() { Name = "Специалист по работе с клиентами", Id = 43904540, SalaryFrom = 1000, SalaryTo = 3000 };

        /// <summary>
        /// Получение вакансии
        /// </summary>
        public static Job GetJob() => new() { Name = "Специалист по работе с клиентами", Id = 43904540, SalaryFrom = 1000, SalaryTo = 3000 };
        #endregion
    }
}
