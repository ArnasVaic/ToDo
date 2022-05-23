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

        public readonly IToDoRepository _toDoRepository;

        public ToDoPostController(ILogger<ToDoPostController> logger, IToDoRepository toDoRepository)
        {
            _logger = logger;
            _toDoRepository = toDoRepository;
        }

        /// <summary>
        /// Get a ToDoPostModel object by title
        /// </summary>
        /// <param name="title"></param>
        /// <returns>Post model</returns>
        [HttpGet]
        public IActionResult Get([FromQuery] string title)
        {
            if (title == null) return BadRequest();
            ToDoPostModel post = _toDoRepository.GetPost(title);
            _logger.LogInformation("{post}", post);
            return (post == null) ? NotFound() : Ok(post);
        }

        /// <summary>
        /// Add a ToDoPostModel to database
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
                _logger.LogInformation("Post with title: \"{0}\" already exists", title);
                return BadRequest();
            }

            var post = new ToDoPostModel()
            {
                Title = title,
                Deadline = deadline,
                Description = description
            };
            //_logger.LogInformation("{post}", post);
            _toDoRepository.Create(post);
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
                _logger.LogInformation("Post titled: {0} updated with new description: {1}", title, newDescription);
                return Ok();
            }
            else
            {
                _logger.LogInformation("Post titled: {0} could not be updated because it does not exist", title);
                return NotFound();
            }
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery] string title)
        {
            if (_toDoRepository.Exists(title))
            {
                _toDoRepository.Delete(title);
                _logger.LogInformation($"Post titled: \"{title}\" has been deleted");
                return Ok();
            }
            else
            {
                _logger.LogInformation("Post titled: {0} could not be deleted because it does not exist", title);
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
            ///TODO deadline null check
            if (deadline.CompareTo(DateTime.UtcNow) < 0)
            {
                _logger.LogInformation("Cannot create a ToDo post with a deadline that has already passed");
                return BadRequest();
            }

            if (!_toDoRepository.Exists(title))
            {
                _logger.LogInformation("Post with title: \"{0}\" does not exist", title);
                return BadRequest();
            }

            var post = new ToDoPostModel()
            {
                Title = title,
                Deadline = deadline,
                Description = description
            };
            _toDoRepository.Update(post);
            return Ok();
        }
    }
}