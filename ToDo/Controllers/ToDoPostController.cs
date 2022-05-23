using Microsoft.AspNetCore.Mvc;
using ToDo2.Dependencies;

namespace ToDo2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoPostController : ControllerBase
    {

        private readonly ILogger<ToDoPostController> _logger;

        public PostDependency dep;

        public ToDoPostController(ILogger<ToDoPostController> logger, PostDependency dep)
        {
            _logger = logger;
            this.dep = dep;
        }

        [HttpGet("ByTitle")]
        public ToDoPost Get(String title)
        {
            try
            {
                return dep.posts[title];
            } catch(KeyNotFoundException e)
            {
                Console.WriteLine(e.Message);
                return new ToDoPost()
                {
                    Title = "no title",
                    Deadline = DateTime.Today,
                    Description = "no desc"
                };
            }
        }

        [HttpPost("Create")]
        public void Post(String title, DateTime deadline, String description)
        {
            dep.posts.Add(title, new ToDoPost()
            {
                Title = title,
                Deadline = deadline,
                Description = description
            });
        }
    }
}