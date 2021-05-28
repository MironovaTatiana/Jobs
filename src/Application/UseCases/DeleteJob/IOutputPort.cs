using System.Collections.Generic;
using Domain;

namespace Application.UseCases.DeleteJob
{
    /// <summary>
    /// Выходной порт
    /// </summary>
    public interface IOutputPort
    {
        /// <summary>
        /// Сбой
        /// </summary>
        void Fail(string message);

        /// <summary>
        /// Успешно
        /// </summary>
        void Ok(string message);
    }
}
