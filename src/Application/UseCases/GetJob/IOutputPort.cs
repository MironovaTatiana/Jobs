using Domain;

namespace Application.UseCases.GetJob
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
        void Ok(string message, IJob job);
    }
}
