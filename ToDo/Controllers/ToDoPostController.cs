using Microsoft.AspNetCore.Mvc;
using ToDo.Dependencies;
using ToDo.Models;

namespace ToDo2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoPostController : ControllerBase
    {
        private readonly ILogger<ToDoPostController> _logger;

        private readonly IToDoRepository _toDoRepository;
        public ToDoPostController(ILogger<ToDoPostController> logger, IToDoRepository toDoRepository)
        {
            _logger = logger;
            _toDoRepository = toDoRepository;
        }

        /// <summary>
        /// Get To Do post filtered by title.
        /// </summary>
        /// <param name="title">Title of the task</param>
        /// <returns>To Do post</returns>
        [HttpGet]
        public IActionResult Get([FromQuery] string title)
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
        /// Add a To Do Post to database
        /// </summary>
        /// <param name="title"></param>
        /// <param name="deadline"></param>
        /// <param name="description"></param>
        /// <returns>Status code</returns>
        [HttpPost]
        public IActionResult Post([FromQuery] string title, [FromQuery] DateTime deadline, [FromQuery] String description)
        {

            // TODO: model validation
            if (string.IsNullOrEmpty(title))
            {
                _logger.LogInformation("Title parameter pass as null");
                return BadRequest();
            }

            if (string.IsNullOrEmpty(description))
            {
                _logger.LogInformation("Description parameter pass as null");
                return BadRequest();
            }

            if (deadline.CompareTo(DateTime.UtcNow) < 0)
            {
                _logger.LogInformation("Cannot create a ToDo post with a deadline that has already passed");
                return BadRequest();
            }

            if (_toDoRepository.Exists(title))
            {
                _logger.LogInformation($"Post with title: \"{title}\" already exists");
                return BadRequest();
            }

            var post = new ToDoPostModel()
            {
                Title = title,
                Deadline = deadline,
                Description = description
            };
            _toDoRepository.Create(post);
            _logger.LogInformation($"POST /ToDo post with data: {post} was created");
            return Ok();
        }

        /// <summary>
        /// Update the description of a post
        /// </summary>
        /// <param name="title"></param>
        /// <param name="newDescription"></param>
        /// <returns>Status code</returns>
        [HttpPatch]
        public IActionResult Patch([FromQuery] string title, [FromQuery] string newDescription)
        {
            if (string.IsNullOrEmpty(title))
            {
                _logger.LogInformation("Title must not be non-empty and non-null");
                return BadRequest();
            }
            if (string.IsNullOrEmpty(newDescription))
            {
                _logger.LogInformation("Description must not be non-empty and non-null");
                return BadRequest();
            }
            if (_toDoRepository.Exists(title))
            {
                _toDoRepository.Get(title).Description = newDescription;
                _logger.LogInformation($"PATCH /ToDo post with title: {title} was given a new description {newDescription}");
                return Ok();
            }
            else
            {
                _logger.LogInformation($"Post titled: {title} could not be updated because it does not exist");
                return NotFound();
            }
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery] string title)
        {
            if (_toDoRepository.Exists(title))
            {
                _toDoRepository.Delete(title);
                _logger.LogInformation($"DELETE /ToDo post with title: {title} was deleted");
                return Ok();
            }
            else
            {
                _logger.LogInformation($"Post titled: {title} could not be deleted because it does not exist");
                return NotFound();
            }
        }

        [HttpPut]
        public IActionResult Put([FromQuery] string title, [FromQuery] DateTime deadline, [FromQuery] String description)
        {
            if (string.IsNullOrEmpty(title))
            {
                _logger.LogInformation("Title parameter pass as null");
                return BadRequest("null or empty title");
            }

            if (string.IsNullOrEmpty(description))
            {
                _logger.LogInformation("Description parameter pass as null");
                return BadRequest("null or empty description");
            }

            if (deadline.CompareTo(DateTime.UtcNow) < 0)
            {
                _logger.LogInformation("Cannot create a ToDo post with a deadline that has already passed");
                return BadRequest();
            }

            if (!_toDoRepository.Exists(title))
            {
                _logger.LogInformation($"Post with title: \"{title}\" does not exist");
                return BadRequest();
            }

            var post = new ToDoPostModel()
            {
                Title = title,
                Deadline = deadline,
                Description = description
            };
            _toDoRepository.Update(post);
            _logger.LogInformation($"PUT /ToDo post with title: {title} was updated with data {post}");
            return Ok();
        }
    }
}