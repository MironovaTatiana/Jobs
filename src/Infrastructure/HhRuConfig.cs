using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Infrastructure
{
    /// <summary>
    /// Класс конфигурации для работы с HH.ru
    /// </summary>
    public class HhRuConfig
    {
        #region Константы

        /// <summary>
        /// Название секции
        /// </summary>
        public const string Requests = "Requests";

        #endregion

        #region Свойства

        /// <summary>
        /// Запрос для получения вакансий
        /// </summary>
        public string RequestVacancies { get; set; }

        /// <summary>
        /// Запрос для получения N вакансий
        /// </summary>
        public string RequestVacanciesByCount { get; set; }

        /// <summary>
        /// Запрос для получения вакансии по идентификатору
        /// </summary>
        public string RequestVacancyById { get; set; }

        /// <summary>
        /// Ключ заголовка
        /// </summary>
        public string HeaderKey { get; set; }

        /// <summary>
        /// Заголовок
        /// </summary>
        public string HeaderValue { get; set; }

        #endregion

        #region Конструктор

        /// <summary>
        /// Класс конфигурации
        /// </summary>
        public HhRuConfig()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                .AddJsonFile("appsettings.json", false)
                .Build();

            configuration.GetSection(Requests).Bind(this);
        }

        #endregion
    }
}
