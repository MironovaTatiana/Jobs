namespace Application.UseCases.GetJobsList
{
    using System.Collections.Generic;
    using Domain;

    /// <summary>
    /// Выходной порт
    /// </summary>
    public interface IOutputPort
    {
        /// <summary>
        /// Неудача
        /// </summary>
        void Fail(string message);

        /// <summary>
        /// Успешно
        /// </summary>
        void Ok(string message, IEnumerable<IJob> jobsList);
    }
}
