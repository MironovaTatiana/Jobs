using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Application.UseCases.GetJobsList;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    /// <summary>
    /// Контроллер для получения списка вакансий
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class JobsListController : ControllerBase, IOutputPort
    {
        #region Поля

        /// <summary>
        /// Результат запроса
        /// </summary>
        private IActionResult _result;

        /// <summary>
        /// Поле для получения вакансий
        /// </summary>
        private readonly IGetJobsListUseCase _useCase;

        #endregion

        #region Конструктор

        /// <summary>
        /// Контроллер для получения списка вакансий
        /// </summary>
        public JobsListController(IGetJobsListUseCase useCase) 
        {
            this._useCase = useCase;
        }

        #endregion

        #region Запросы

        /// <summary>
        /// Получение списка вакансий
        /// </summary>
        [HttpGet("{count:int}", Name = "GetJobsList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async ValueTask<IEnumerable<IJob>> Get(
            [FromRoute][Required] int count)
        {
            _useCase.SetOutputPort(this);
            return await _useCase.ExecuteAsync(count)
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
        public void Ok(string s, IEnumerable<IJob> jobsList) => this._result = this.Ok();

        #endregion
    }
}
