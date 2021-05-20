using System.Collections.Generic;
using Domain;

namespace Application.UseCases.DeleteJobsList
{
    /// <summary>
    /// Выходной порт
    /// </summary>
    public interface IOutputPort
    {
        /// <summary>
        /// Сбой
        /// </summary>
        void Fail();

        /// <summary>
        /// Успешно
        /// </summary>
        void Ok(IEnumerable<IJob> jobsList);
    }
}
