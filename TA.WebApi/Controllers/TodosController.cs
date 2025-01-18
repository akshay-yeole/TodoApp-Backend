using Microsoft.AspNetCore.Mvc;
using TA.Contracts;
using TA.Entities.DTOs;

namespace TA.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ITodoService _todoService;
        private readonly ILogger<TodosController> _logger;

        public TodosController(ITodoService todoService, ILogger<TodosController> logger)
        {
            _todoService = todoService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TodoDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllTodosAsync()
        {
            _logger.LogInformation("Executing Endpoint GetAllTodosAsync()");
            var res = await _todoService.GetAllTodosAsync();
            return Ok(res);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TodoDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTodoByIdAsyc(int id)
        {
            _logger.LogInformation("Executing Endpoint GetTodoByIdAsyc()");
            var res = await _todoService.GetTodoById(id);
            return Ok(res);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddTodoAsync([FromBody] TodoDTO todo)
        {
            _logger.LogInformation("Executing Endpoint AddTodoAsync()");
            await _todoService.AddTodoAsync(todo);
            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateTodoAsync(int id, TodoDTO todo)
        {
            _logger.LogInformation("Executing Endpoint UpdateTodoAsync()");;
            await _todoService.UpdateTodoAsync(id, todo);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteTodoAsync(int id)
        {
            _logger.LogInformation("Executing Endpoint DeleteTodoAsync()");
            await _todoService.DeleteTodoAsync(id);
            return NoContent();
        }
    }
}
