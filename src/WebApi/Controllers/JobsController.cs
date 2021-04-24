using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Application.UseCases.AddJobsList;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApi.Controllers
{
    /// <summary>
    /// Контроллер для добавления вакансий
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class JobsController : ControllerBase, IOutputPort
    {
        #region Поля

        /// <summary>
        /// Результат запроса
        /// </summary>
        private IActionResult _result;

        /// <summary>
        /// Поле для добавления событий
        /// </summary>
        private readonly IAddJobsListUseCase _useCase;

        #endregion

        #region Конструктор

        /// <summary>
        /// Контроллер для добавления вакансий
        /// </summary>
        public JobsController(IAddJobsListUseCase useCase) 
        {
            this._useCase = useCase;
        }

        #endregion

        #region Запросы

        /// <summary>
        /// Добавление вакансий
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(
            [FromBody][BindRequired] List<JobDto> jobsList)
        {
            _useCase.SetOutputPort(this);
            await _useCase.ExecuteAsync(jobsList)
                .ConfigureAwait(false);

            return this.Ok(); 
            //this._result;
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
