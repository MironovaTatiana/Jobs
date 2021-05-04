namespace Application.Dtos
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Domain;

    /// <summary>
    /// Вакансия
    /// </summary>
    public class JobDto : IJob
    {
        #region Свойства

        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название вакансии
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Оклад (минимальный предел)
        /// </summary>
        public decimal SalaryFrom { get; set; }

        /// <summary>
        /// Оклад (максимальный предел)
        /// </summary>
        public decimal SalaryTo { get; set; }

        /// <summary>
        /// Название организации
        /// </summary>
        public string EmployerName { get; set; }

        /// <summary>
        /// Контактное лицо
        /// </summary>
        public string ContactName { get; set; }

        /// <summary>
        /// Телефон
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Тип занятости
        /// </summary>
        public string EmploymentType { get; set; }

        #endregion
    }
}
