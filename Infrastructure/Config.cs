using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Infrastructure
{
    /// <summary>
    /// Класс конфигурации
    /// </summary>
    public class Config
    {
        #region Свойства

        /// <summary>
        /// Секция запросов
        /// </summary>
        IConfigurationSection Requests { get; set; }

        /// <summary>
        /// Запрос для получения вакансий
        /// </summary>
        public string RequestVacanciesByCount => Requests.GetSection("RequestVacanciesByCount")?.Value;

        /// <summary>
        /// Запрос для получения вакансии по идентификатору
        /// </summary>
        public string RequestVacancyById => Requests.GetSection("RequestVacancyById")?.Value;

        /// <summary>
        /// Ключ заголовка
        /// </summary>
        public string HeaderKey => Requests.GetSection("HeaderKey")?.Value;

        /// <summary>
        /// Заголовок
        /// </summary>
        public string HeaderValue => Requests.GetSection("HeaderValue")?.Value;

        #endregion

        #region Конструктор

        /// <summary>
        /// Класс конфигурации
        /// </summary>
        public Config()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                .AddJsonFile("appsettings.json", false)
                .Build();
            Requests = configuration.GetSection("Requests");
        }

        #endregion
    }
}
