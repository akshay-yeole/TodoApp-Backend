using LazyCache;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using TA.Contracts;
using TA.Core.CacheConfiguration;
using TA.Entities.DTOs;

namespace TA.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ITodoService _todoService;
        private readonly ILogger<TodosController> _logger;
        private ICacheProvider _cacheProvider;

        public TodosController(ITodoService todoService, ICacheProvider cacheProvider, ILogger<TodosController> logger)
        {
            _todoService = todoService;
            _cacheProvider = cacheProvider;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TodoDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllTodosAsync()
        {
            _logger.LogInformation("Executing Endpoint GetAllTodosAsync()");

            if (!_cacheProvider.TryGetValue(CacheKeys.Todos, out IEnumerable<TodoDTO> todos))
            {
                todos = await _todoService.GetAllTodosAsync();
                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = CacheConfiguration.AbsoluteExpiration,
                    SlidingExpiration = CacheConfiguration.SlidingExpiration, 
                    Size = CacheConfiguration.MaxCacheSize
                };
                _cacheProvider.Set(CacheKeys.Todos, todos, cacheEntryOptions);
            }
            return Ok(todos);
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
        public async Task<IActionResult> UpdateTodoAsync(int id, [FromBody] TodoDTO todo)
        {
            _logger.LogInformation("Executing Endpoint UpdateTodoAsync()"); ;
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
