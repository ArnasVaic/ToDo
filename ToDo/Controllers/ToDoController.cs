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
        /// Add a new To Do
        /// </summary>
        /// <param name="title">title of the task</param>
        /// <param name="deadline">task deadline</param>
        /// <param name="description">task description</param>
        /// <returns>To Do item</returns>

        /// <summary>
        /// Add a new To Do
        /// </summary>
        /// <param name="post">post to add</param>
        /// <returns>To Do item</returns>
        [HttpPost]
        public IActionResult Post([FromBody] ToDoPostModel post)
        {
            if (post.Deadline.CompareTo(DateTime.UtcNow) < 0)
            {
                string msg = $"POST /ToDo post with data: {post} cannot be created because deadline has already passed";
                _logger.LogInformation(msg);
                return BadRequest(msg);
            }

            if (_toDoRepository.Exists(post.Title))
            {
                string msg = $"POST /ToDo post with data: {post} already exists";
                _logger.LogInformation(msg);
                return BadRequest(msg);
            }

            _toDoRepository.Create(post);
            _logger.LogInformation($"POST /ToDo post with data: {post} was created");
            return Ok(post);
        }

        /// <summary>
        /// Update the description of a post
        /// </summary>
        /// <param name="title">task title</param>
        /// <param name="newDescription">new task description</param>
        /// <returns>Status code</returns>
        [HttpPatch]
        public IActionResult Patch([FromQuery] string title, [FromQuery] string newDescription)
        {
            if (string.IsNullOrEmpty(title))
            {
                const string msg = "Title must not be non-empty and non-null";
                _logger.LogInformation(msg);
                return BadRequest(msg);
            }
            if (string.IsNullOrEmpty(newDescription))
            {
                const string msg = "Description must not be non-empty and non-null";
                _logger.LogInformation(msg);
                return BadRequest(msg);
            }
            if (_toDoRepository.Exists(title))
            {
                string msg = $"PATCH /ToDo post with title: {title} was given a new description {newDescription}";
                _toDoRepository.Get(title).Description = newDescription;
                _logger.LogInformation(msg);
                return Ok(msg);
            }
            else
            {
                string msg = $"Post titled: {title} could not be updated because it does not exist";
                _logger.LogInformation(msg);
                return NotFound(msg);
            }
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery] string title)
        {
            if (_toDoRepository.Exists(title))
            {
                string msg = $"DELETE /ToDo post with title: {title} was deleted";
                _toDoRepository.Delete(title);
                _logger.LogInformation(msg);
                return Ok(msg);
            }
            else
            {
                string msg = $"Post titled: {title} could not be deleted because it does not exist";
                _logger.LogInformation(msg);
                return NotFound(msg);
            }
        }

        [HttpPut]
        public IActionResult Put([FromQuery] string title, [FromQuery] DateTime deadline, [FromQuery] String description)
        {
            if (string.IsNullOrEmpty(title))
            {
                const string msg = "Title param passed as null or as an empty string";
                _logger.LogInformation(msg);
                return BadRequest(msg);
            }

            if (string.IsNullOrEmpty(description))
            {
                const string msg = "Description param passed as null or as an empty string";
                _logger.LogInformation(msg);
                return BadRequest(msg);
            }

            if (deadline.CompareTo(DateTime.UtcNow) < 0)
            {
                const string msg = "Cannot create a ToDo with a deadline that has already passed";
                _logger.LogInformation(msg);
                return BadRequest(msg);
            }

            if (!_toDoRepository.Exists(title))
            {
                string msg = $"Post with title: \"{title}\" does not exist";
                _logger.LogInformation(msg);
                return BadRequest(msg);
            }

            var post = new ToDoPostModel()
            {
                Title = title,
                Deadline = deadline,
                Description = description
            };

            _toDoRepository.Update(post);
            string okMsg = $"PUT /ToDo post with title: {title} was updated with data {post}";
            _logger.LogInformation(okMsg);
            return Ok(okMsg);
        }
    }
}