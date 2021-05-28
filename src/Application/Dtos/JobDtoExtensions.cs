using System.Collections.Generic;
using System.Linq;
using Domain;


namespace Application.Dtos
{
    /// <summary>
    /// Класс с методами расширения для JobDto
    /// </summary>
    public static class JobDtoExtensions
    {
        #region Методы

        /// <summary>
        /// Преобразование списка JobDto в Job
        /// </summary>
        public static IEnumerable<Job> ConvertJobDtoListToJobList(this IEnumerable<JobDto> vacanciesDto)
        {
            IEnumerable<Job> vacancies = vacanciesDto.Select(x => new Job
            {
                Id = x.Id,
                Name = x.Name,
                SalaryFrom = x.SalaryFrom,
                SalaryTo = x.SalaryTo,
                EmployerName = x.EmployerName,
                ContactName = x.ContactName,
                Phone = x.Phone,
                Description = x.Description,
                EmploymentType = x.EmploymentType,
            });

            return vacancies;
        }

        /// <summary>
        /// Преобразование списка Job в JobDto
        /// </summary>
        public static IEnumerable<JobDto> ConvertJobListToJobDtoList(this IEnumerable<Job> vacancies)
        {
            IEnumerable<JobDto> vacanciesDto = vacancies.Select(x => new JobDto
            {
                Id = x.Id,
                Name = x.Name,
                SalaryFrom = x.SalaryFrom,
                SalaryTo = x.SalaryTo,
                EmployerName = x.EmployerName,
                ContactName = x.ContactName,
                Phone = x.Phone,
                Description = x.Description,
                EmploymentType = x.EmploymentType,
            });

            return vacanciesDto;
        }

        /// <summary>
        /// Преобразование Job в JobDto
        /// </summary>
        public static JobDto ConvertJobToJobDto(this Job vacancy)
        {
            JobDto vacancyDto = new JobDto
            {
                Id = vacancy.Id,
                Name = vacancy.Name,
                SalaryFrom = vacancy.SalaryFrom,
                SalaryTo = vacancy.SalaryTo,
                EmployerName = vacancy.EmployerName,
                ContactName = vacancy.ContactName,
                Phone = vacancy.Phone,
                Description = vacancy.Description,
                EmploymentType = vacancy.EmploymentType,
            };

            return vacancyDto;
        }

        #endregion
    }
}
