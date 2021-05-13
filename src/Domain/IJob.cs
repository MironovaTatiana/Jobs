namespace Domain
{
    /// <summary>
    /// Интерфейс для вакансий
    /// </summary>
    public interface IJob
    {
        /// <summary>
        /// Название вакансии
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }
    }
}
