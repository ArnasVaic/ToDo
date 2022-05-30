using Microsoft.EntityFrameworkCore;

using ToDo.Models;

namespace ToDo.EntityFramework
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
        {

        }
        public DbSet<ToDoEntity> ToDos { get; set; }
        public DbSet<ProjectEntity> Projects { get; set; }
    }
}
