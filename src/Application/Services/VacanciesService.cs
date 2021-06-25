using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos;
using Domain;
using Infrastructure;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Application.Services
{
    /// <summary>
    /// Сервис для получения вакансий
    /// </summary>
    public class VacanciesService : IVacanciesService
    {
        #region Поля

        /// <summary>
        /// Конфигурация
        /// </summary>
        private readonly HhRuConfig _config;

        /// <summary>
        /// Http-клиент
        /// </summary>
        private readonly HttpClient _httpClient;

        #endregion

        #region Конструктор

        /// <summary>
        /// Сервис для получения вакансий
        /// </summary>
        public VacanciesService(IOptions<HhRuConfig> options)
        {
            _config = options.Value;
            _httpClient = new();
            _httpClient.DefaultRequestHeaders.Add(_config.HeaderKey, _config.HeaderValue);
        }

        #endregion

        #region Методы

        /// <summary>
        /// Получение списка вакансий
        /// </summary>
        public async ValueTask<IEnumerable<IJob>> GetVacanciesListAsync(int count)
        {
            try
            {
                var request = _config.RequestVacanciesByCount?.Replace("{count}", count.ToString());
                var response = (await _httpClient.GetAsync(request)).EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                var jobs = new List<JobDto>();
                var jsonResponses = JsonConvert.DeserializeObject<JsonItems>(responseBody);

                jobs = WorkAsync(jsonResponses.ResponseItems);

                return jobs;
            }
            catch
            {
                throw new HttpRequestException();
            }
        }

        /// <summary>
        /// Получение списка вакансий 
        /// </summary>
        private List<JobDto> WorkAsync(List<JsonResponses> items)
        {
            var jobs = new List<JobDto>();

            Parallel.ForEach
                (items,
                 new ParallelOptions { MaxDegreeOfParallelism = 10 },
                item =>
                    {
                        var job = GetVacancyByIdAsync(item.Id);

                        jobs.Add(job.Result);
                    }
                );

            return jobs;
        }

        /// <summary>
        /// Получение детальной информации по конкретной вакансии по идентификатору
        /// </summary>
        public async ValueTask<JobDto> GetVacancyByIdAsync(int id)
        {
            try
            {
                var request = _config.RequestVacancyById?.Replace("{id}", id.ToString());
                var response = (await _httpClient.GetAsync(request)).EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                var jsonResponse = JsonConvert.DeserializeObject<JsonResponse>(responseBody);

                return ConvertJsonResponseToJobDto(jsonResponse);
            }
            catch
            {
                throw new HttpRequestException();
            }
        }

        /// <summary>
        /// Преобразование модели из json в вакансию
        /// </summary>
        private JobDto ConvertJsonResponseToJobDto(JsonResponse jsonResponse)
        {
            try
            {
                return new JobDto
                {
                    Id = jsonResponse.Id,
                    Name = jsonResponse.Name ?? string.Empty,
                    SalaryFrom = jsonResponse.Salary?.SalaryFrom ?? 0m,
                    SalaryTo = jsonResponse.Salary?.SalaryTo ?? 0m,
                    EmployerName = jsonResponse.Employer?.EmployerName ?? string.Empty,
                    ContactName = jsonResponse.Contact?.ContactName ?? string.Empty,
                    Description = jsonResponse.Description ?? string.Empty,
                    Phone = jsonResponse.Contact?.Phone?.Number ?? string.Empty,
                    EmploymentType = jsonResponse.Employment?.EmploymentType ?? string.Empty,
                };
            }
            catch
            {
                return new JobDto();
            }
        }


        #endregion
    }
}
