using Microsoft.AspNetCore.Mvc;
using TA.Contracts;

namespace TA.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ITodoService _todoService; 
        public TodosController(ITodoService todoService)
        {
            _todoService = todoService;
        }
    }
}
