namespace Application.UseCases.GetJob
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
        void Ok(string message, IJob job);
    }
}
