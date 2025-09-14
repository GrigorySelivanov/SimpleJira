using Data.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleJira.Web.Services.Interfaces;
using System.Security.Claims;

namespace SimpleJira.Web.Controllers
{
    [ApiController]
    [Route("task")]
    [Authorize]
    public class TaskController(ITaskService taskService, ILogger<TaskController> logger) : ControllerBase
    {
        private readonly ITaskService _taskService = taskService;
        private readonly ILogger<TaskController> _logger = logger;

        /// <summary>
        /// Получение всех задач.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //P.S.: При неискусственной реализации желательно добавить query-параметры для постраничного вывода задач, а не всех сразу.
            var tasks = await _taskService.GetAllTasksAsync();
            return Ok(tasks);
        }

        /// <summary>
        /// Получение задачи по ID.
        /// </summary>
        /// <param name="id">ID задачи</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var task = await _taskService.GetTaskByIDAsync(id);
                return Ok(task);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning("{}", ex.Message);
                return NotFound(ex.Message);
            }
        }


        /// <summary>
        /// Метод по созданию задачи.
        /// </summary>
        /// <param name="model"><br>Модель создания задачи.</br>
        /// <br>Свойство "Priority" принимает 3 возможных параметра (Low, Medium, High).</br>
        /// </param>
        [HttpPost("create")]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskDTO model)
        {
            try
            {
                if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
                    throw new Exception("Невозможно идентифицировать автора.");

                model.AuthorId = userId;

                var createdTask = await _taskService.CreateTaskAsync(model);
                return CreatedAtAction(nameof(Get), new { id = createdTask.Id }, createdTask);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning("{}", ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("{}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Метод по обновлению задачи.
        /// </summary>
        /// <param name="model"><br>Модель обновления задачи.</br>
        /// <br>Свойство "Priority" принимает 3 возможных параметра (Low, Medium, High).</br>
        /// <br>Свойство "State" принимает 3 возможных параметра (New, InProgress, Done).</br>
        /// </param>
        [HttpPut("update")]
        public async Task<IActionResult> UpdateTask([FromBody] UpdateTaskDTO model)
        {
            try
            {
                await _taskService.UpdateTaskAsync(model);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning("{}", ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("{}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Метод удаления задачи.
        /// </summary>
        /// <param name="id">ID задачи</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _taskService.RemoveAsync(id);
            return Ok();
        }
    }
}
