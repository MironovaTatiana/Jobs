using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Application.UseCases.GetJobsList;
using Application.UseCases.GetJob;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GetJobsList = Application.UseCases.GetJobsList;
using GetJob = Application.UseCases.GetJob;
using DeleteJobsList = Application.UseCases.DeleteJobsList;
using DeleteJob = Application.UseCases.DeleteJob;
using Application.UseCases.DeleteJobsList;
using Application.UseCases.DeleteJob;

namespace WebApi.Controllers
{
    /// <summary>
    /// Контроллер для получения и удаления вакансий
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class JobsController : ControllerBase, GetJobsList.IOutputPort, GetJob.IOutputPort, DeleteJobsList.IOutputPort, DeleteJob.IOutputPort
    {
        #region Поля

        /// <summary>
        /// Результат запроса
        /// </summary>
        private IActionResult _result;

        #endregion     

        #region Запросы

        /// <summary>
        /// Получение списка вакансий
        /// </summary>
        [HttpGet(Name = "GetJobsList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<IJob>> Get(
            [FromServices] IGetJobsListUseCase useCase,
            [FromQuery][Required] int count)
        {
            useCase.SetOutputPort(this);
            return await useCase.ExecuteAsync(count)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Получение вакансии по идентификатору
        /// </summary>
        [HttpGet("{jobId}", Name = "GetJob")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IJob> Get(
            [FromServices] IGetJobUseCase useCase,
            [FromRoute][Required] int jobId)
        {
            useCase.SetOutputPort(this);

            return await useCase.ExecuteAsync(jobId)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Удаление списка вакансий
        /// </summary>
        [HttpPost(Name = "DeleteJobsList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task Post(
            [FromServices] IDeleteJobsListUseCase useCase)
        {
            useCase.SetOutputPort(this);
            await useCase.ExecuteAsync()
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Удаление вакансии по идентификатору
        /// </summary>
        [HttpPost("{jobId}", Name = "DeleteJob")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task Post(
            [FromServices] IDeleteJobUseCase useCase,
            [FromRoute][Required] int jobId)
        {
            useCase.SetOutputPort(this);
            await useCase.ExecuteAsync(jobId)
                .ConfigureAwait(false);
        }

        #endregion

        #region Реализация портов

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

        /// <summary>
        /// Успешно
        /// </summary>
        [ApiExplorerSettings(IgnoreApi = true)]
        public void Ok(string s, IJob job) => this._result = this.Ok();

        /// <summary>
        /// Успешно
        /// </summary>
        [ApiExplorerSettings(IgnoreApi = true)]
        public void Ok(string s) => this._result = this.Ok();

        #endregion
    }
}
