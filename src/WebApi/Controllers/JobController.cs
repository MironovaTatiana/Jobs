using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Application.UseCases.GetJob;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    /// <summary>
    /// Контроллер для получения вакансии по идентификатору
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class JobController : ControllerBase, IOutputPort
    {
        #region Поля

        /// <summary>
        /// Результат запроса
        /// </summary>
        private IActionResult _result;

        /// <summary>
        /// Поле для получения вакансии
        /// </summary>
        private readonly IGetJobUseCase _useCase;

        #endregion

        #region Конструктор

        /// <summary>
        /// Контроллер для получения вакансии
        /// </summary>
        public JobController(IGetJobUseCase useCase) 
        {
            this._useCase = useCase;
        }

        #endregion

        #region Запросы

        /// <summary>
        /// Получение вакансии по идентификатору
        /// </summary>
        [HttpGet("{id:int}", Name = "GetJob")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async ValueTask<IJob> Get(
            [FromRoute][Required] int id)
        {
            _useCase.SetOutputPort(this);
            return await _useCase.ExecuteAsync(id)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Неудача
        /// </summary>
        [ApiExplorerSettings(IgnoreApi = true)]
        public void Fail(string s) => this._result = this.BadRequest();

        /// <summary>
        /// Успешно
        /// </summary>
        [ApiExplorerSettings(IgnoreApi = true)]
        public void Ok(string s, IJob job) => this._result = this.Ok();

        #endregion
    }
}
