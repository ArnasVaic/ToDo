using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDo.Dependencies;
using ToDo.EntityFramework;
using ToDo.Models;

namespace ToDo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IGenericRepository<ProjectEntity> _projectRepository;

        private readonly ILogger<ToDoController> _logger;
        public ProjectController(ILogger<ToDoController> logger, IGenericRepository<ProjectEntity> projectRepository)
        {
            _logger = logger;
            _projectRepository = projectRepository;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var project = _projectRepository.GetById(id);
            if (project == null) return NotFound();
            return Ok(project);
        }

        [HttpPost]
        public IActionResult Post([FromQuery][Required(AllowEmptyStrings = false)]string name)
        {
            var ent = new ProjectEntity { Name = name };
            _projectRepository.Add(ent);
            return Ok(ent);
        }
    }
}
