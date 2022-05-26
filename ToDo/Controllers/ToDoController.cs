using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ToDo.Dependencies;
using ToDo.Models;

namespace ToDo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoController : ControllerBase
    {
        private readonly ILogger<ToDoController> _logger;

        private readonly IToDoRepository _toDoRepository;
        public ToDoController(ILogger<ToDoController> logger, IToDoRepository toDoRepository)
        {
            _logger = logger;
            _toDoRepository = toDoRepository;
        }

        /// <summary>
        /// Get ToDo by title.
        /// </summary>
        /// <param name="title"></param>
        /// <returns>ToDo</returns>
        [HttpGet]
        public IActionResult Get([FromQuery][Required(AllowEmptyStrings = false)] string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                _logger.LogInformation("title must be non-null and non-empty");
                return BadRequest();
            } 
            var post = _toDoRepository.Get(title);
            if (post == null) return NotFound();
            _logger.LogInformation($"GET /ToDo post with data: {post} was found");
            return Ok(post);
        }

        /// <summary>
        /// Add a new ToDo
        /// </summary>
        /// <param name="todo"></param>
        /// <returns>ToDo item</returns>
        [HttpPost]
        public IActionResult Post([FromBody][Required]ToDoPostModel todo)
        {
            if (todo.Deadline.CompareTo(DateTime.UtcNow) < 0)
            {
                string msg = $"POST /ToDo post with data: {todo} cannot be created because deadline has already passed";
                _logger.LogInformation(msg);
                return BadRequest(msg);
            }
            if (_toDoRepository.Post(todo))
            {
                _logger.LogInformation($"POST /ToDo post with data: {todo} was created");
                return Ok(todo);
            }
            else
            {
                string msg = $"POST /ToDo post with data: {todo} already exists";
                _logger.LogInformation(msg);
                return BadRequest(msg);
            }
        }

        /// <summary>
        /// Patch the description of a ToDo
        /// </summary>
        /// <param name="todo"></param>
        /// <returns>Status code</returns>
        [HttpPatch]
        public IActionResult Patch([FromBody][Required]ToDoPatchModel todo)
        {
            string title = todo.Title;
            if (_toDoRepository.Patch(todo))
            {
                string msg = $"PATCH /ToDo post with title: {title} was given a new description {todo.Description}";
                _logger.LogInformation(msg);
                return Ok(msg);
            }
            else
            {
                string msg = $"PATCH /ToDo post with title: {title} could not be patched because it does not exist";
                _logger.LogInformation(msg);
                return NotFound(msg);
            }
        }

        /// <summary>
        /// Delete ToDo by title
        /// </summary>
        /// <param name="title"></param>
        /// <returns>Status code</returns>
        [HttpDelete]
        public IActionResult Delete([FromQuery][Required]string title)
        {
            if (_toDoRepository.Delete(title))
            {
                string msg = $"DELETE /ToDo post was deleted";
                _logger.LogInformation(msg);
                return Ok(msg);
            }
            else
            {
                string msg = $"DELETE /ToDo post does not exist";
                _logger.LogInformation(msg);
                return NotFound(msg);
            }
        }

        /// <summary>
        /// Replace ToDo with an existing title
        /// </summary>
        /// <param name="todo"></param>
        /// <returns>Status code</returns>
        [HttpPut]
        public IActionResult Put([FromQuery][Required]ToDoPostModel todo)
        {
            if (todo.Deadline.CompareTo(DateTime.UtcNow) < 0)
            {
                const string msg = "PUT /ToDo post could not be created because deadline already passed";
                _logger.LogInformation(msg);
                return BadRequest(msg);
            }
            if(_toDoRepository.Put(todo))
            {
                string msg = $"PUT /ToDo post was updated with data {todo}";
                _logger.LogInformation(msg);
                return Ok(msg);
            } 
            else
            {
                string msg = $"PUT /ToDo post does not exist";
                _logger.LogInformation(msg);
                return BadRequest(msg);
            }
        }
    }
}