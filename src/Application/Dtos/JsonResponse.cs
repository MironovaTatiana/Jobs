using System.Collections.Generic;
using Newtonsoft.Json;


namespace Application.Dtos
{
    /// <summary>
    /// Модель ответа в виде json
    /// </summary>
    public class JsonResponse
    {
        #region Свойства

        /// <summary>
        /// Идентификатор
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Название вакансии
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Оклад
        /// </summary>
        public JsonSalary Salary { get; set; }

        /// <summary>
        /// Работодатель
        /// </summary>
        public Employer Employer { get; set; }

        /// <summary>
        /// Контактное лицо
        /// </summary>
        public JsonContact Contact { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Занятость
        /// </summary>
        public JsonSchedule Employment { get; set; }

        #endregion
    }

    /// <summary>
    /// Занятость
    /// </summary>
    public class JsonSchedule
    {
        #region Свойства

        /// <summary>
        /// Тип занятости
        /// </summary>
        [JsonProperty("id")]
        public string EmploymentType { get; set; }

        #endregion
    }

    /// <summary>
    /// Контакт
    /// </summary>
    public class JsonContact
    {
        #region Свойства

        /// <summary>
        /// Контактное лицо
        /// </summary>
        [JsonProperty("name")]
        public string ContactName { get; set; }

        /// <summary>
        /// Телефон
        /// </summary>
        public Phone Phone { get; set; }

        #endregion
    }

    /// <summary>
    /// Телефон
    /// </summary>
    public class Phone
    {
        #region Свойства

        /// <summary>
        /// Телефон
        /// </summary>
        [JsonProperty("number")]
        public string Number { get; set; }

        #endregion
    }

    /// <summary>
    /// Работодатель
    /// </summary>
    public class Employer
    {
        #region Свойства

        /// <summary>
        /// Название организации
        /// </summary>
        [JsonProperty("name")]
        public string EmployerName { get; set; }

        #endregion
    }

    /// <summary>
    /// Заработок
    /// </summary>
    public class JsonSalary
    {
        #region Свойства

        /// <summary>
        /// Оклад (минимальный предел)
        /// </summary>
        [JsonProperty("from")]
        public decimal? SalaryFrom { get; set; }

        /// <summary>
        /// Оклад (максимальный предел)
        /// </summary>
        [JsonProperty("to")]
        public decimal? SalaryTo { get; set; }

        #endregion
    }

    /// <summary>
    /// items
    /// </summary>
    public class JsonItems
    {
        #region Свойства

        /// <summary>
        /// Идентификатор
        /// </summary>
        [JsonProperty("items")]
        public List<JsonResponses> ResponseItems { get; set; }

        #endregion
    }

    /// <summary>
    /// id
    /// </summary>
    public class JsonResponses
    {
        #region Свойства

        /// <summary>
        /// Идентификатор
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        #endregion
    }
}
