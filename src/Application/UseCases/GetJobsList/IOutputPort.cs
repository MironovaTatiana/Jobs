using System.Collections.Generic;
using Domain;

namespace Application.UseCases.GetJobsList
{
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
