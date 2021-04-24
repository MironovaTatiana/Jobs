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
        /// Сбой
        /// </summary>
        void Fail();

        /// <summary>
        /// Успешно
        /// </summary>
        void Ok(IEnumerable<IJob> jobsList);
    }
}
