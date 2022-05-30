using System.ComponentModel.DataAnnotations;
using AutoMapper;
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

        private readonly IGenericRepository<ToDoEntity> _toDoRepository;
        private readonly IGenericRepository<ProjectEntity> _projectRepository;
        private readonly IMapper _mapper;
        public ToDoController(
            ILogger<ToDoController> logger, 
            IGenericRepository<ToDoEntity> toDoRepository,
            IGenericRepository<ProjectEntity> projectRepository,
            IMapper mapper)
        {
            _logger = logger;
            _toDoRepository = toDoRepository;
            _projectRepository = projectRepository;
            _mapper = mapper;
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
            var posts = _toDoRepository.Find(ent => ent.Title == title);

            if(!posts.Any()) return NotFound($"GET /ToDo post titled: {title} has not been found");

            var post = posts.First();

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

            if (_toDoRepository.Find(_ => _.Title == todo.Title).Any())
            {
                string msg = $"POST /ToDo post with data: {todo} already exists";
                _logger.LogInformation(msg);
                return BadRequest(msg);
            }

            var project = _projectRepository.GetById(todo.ProjectId);
            if (project == null)
            {
                string msg = $"Project with id:{todo.ProjectId} does not exist";
                _logger.LogInformation(msg);
                return BadRequest(msg);
            }
            _toDoRepository.Add(_mapper.Map<ToDoEntity>(todo));
            _logger.LogInformation($"POST /ToDo post with data: {todo} was created");
            return Ok(_mapper.Map<ToDoViewModel>(todo));
        }

        /// <summary>
        /// Patch the description of a ToDo
        /// </summary>
        /// <param name="todo"></param>
        /// <returns>Status code</returns>
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Patch([FromBody][Required]ToDoPatchModel patch)
        {
            string title = patch.Title;
            var posts = _toDoRepository.Find(_ => _.Title == patch.Title);
            var ent = posts.First();
            if (ent == null)
            {
                string msg = $"PATCH /ToDo post with title: {title} could not be patched because it does not exist";
                _logger.LogInformation(msg);
                return NotFound(msg);
            }
            else
            {
                ent.Update(patch);
                _logger.LogInformation($"PATCH /ToDo post with title: {title} was given a new description {patch.Description}");
                return NoContent();
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
            var todos = _toDoRepository.Find(_ => _.Title == title);
            if (todos.Any())
            {
                var todo = todos.First();
                _toDoRepository.Remove(todo);
                _logger.LogInformation($"DELETE /ToDo post titled: {title} was deleted");
                return NoContent();
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
        public IActionResult Put([FromBody][Required]ToDoPostModel todo)
        {
            if (todo.Deadline.CompareTo(DateTime.UtcNow) < 0)
            {
                const string msg = "PUT /ToDo post could not be created because deadline already passed";
                _logger.LogInformation(msg);
                return BadRequest(msg);
            }

            var posts = _toDoRepository.Find(_ => _.Title == todo.Title);
            var ent = posts.First();
            if (ent == null)
            {
                string msg = $"PUT /ToDo post does not exist";
                _logger.LogInformation(msg);
                return NotFound();
            }
            else
            {
                ent.Replace(todo);
                string msg = $"PUT /ToDo post was updated with data {todo}";
                _logger.LogInformation(msg);
                return NoContent();
            }
        }
    }
}