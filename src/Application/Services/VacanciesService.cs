using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Application.Dtos;
using Domain;
using Infrastructure;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration;
using System.IO;
using System;

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
                HttpResponseMessage response =
                    (await _httpClient.GetAsync(request)).EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();

                var jobs = new List<JobDto>();
                var responseJson = TryLoadJson(responseBody);

                foreach (var itm in responseJson)
                {
                    foreach (var child in itm.Value.Children())
                    {
                        jobs.Add(ParseToken(child));
                    }
                }

                return jobs;
            }
            catch
            {
                throw new HttpRequestException();
            }
        }

        /// <summary>
        /// Получение детальной информации по конкретной вакансии по идентификатору
        /// </summary>
        public async ValueTask<JobDto> GetVacancyByIdAsync(int id)
        {
            try
            {
                var request = _config.RequestVacancyById?.Replace("{id}", id.ToString());
                HttpResponseMessage response =
                    (await _httpClient.GetAsync(request)).EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                var responseJson = TryLoadJson(responseBody);

                return ParseToken(responseJson);
            }
            catch
            {
                throw new HttpRequestException();
            }
        }

        /// <summary>
        /// Преобразуем токен в вакансию
        /// </summary>
        private JobDto ParseToken(JToken responseJson)
        {
            try
            {
                return new JobDto
                {
                    Id = responseJson["id"].Value<int>(),
                    Name = responseJson["name"]?.Value<string>(),
                    SalaryFrom = responseJson["salary"]?.ToString() != string.Empty ? SetDecimalValue(responseJson["salary"]!["from"]) : 0m,
                    SalaryTo = responseJson["salary"]?.ToString() != string.Empty ? SetDecimalValue(responseJson["salary"]!["to"]) : 0m,
                    EmployerName = responseJson["employer"]?.ToString() != string.Empty ? SetStringValue(responseJson["employer"]!["name"]) : string.Empty,
                    ContactName = responseJson["contacts"]?.ToString() != string.Empty ? SetStringValue(responseJson["contacts"]!["name"]) : string.Empty,
                    Description = SetStringValue(responseJson["description"]?.Value<string>()),
                    Phone = (responseJson["contacts"]?.ToString() != string.Empty && responseJson["contacts"]!["phones"]?.ToString() != string.Empty) ? SetStringValue(responseJson["contacts"]!["phones"]!["number"]) : string.Empty,
                    EmploymentType = responseJson["schedule"]?.ToString() != string.Empty ? SetStringValue(responseJson["schedule"]!["id"]) : string.Empty,
                };
            }
            catch
            {
                return new JobDto();
            }
        }

        /// <summary>
        /// Установка числового значения
        /// </summary>
        private decimal SetDecimalValue(JToken token)
        {
            return token?.ToString() != string.Empty
                                    ? (decimal)token
                                    : 0m;
        }

        /// <summary>
        /// Установка строкового значения
        /// </summary>
        private string SetStringValue(JToken token)
        {
            return token?.ToString() != string.Empty
                                    ? token.Value<string>()
                                    : string.Empty;
        }

        /// <summary>
        /// Попытка загрузить JSON из ответа
        /// </summary>
        private JObject TryLoadJson(string responseText)
        {
            try
            {
                return JObject.Parse(responseText);
            }
            catch
            {
                return null;
            }
        }

        #endregion
    }
}
