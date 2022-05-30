using System.Linq.Expressions;
using ToDo.EntityFramework;
using ToDo.Models;

namespace ToDo.Dependencies
{
    public class ProjectRepositoryDb : IGenericRepository<ProjectEntity>
    {
        private readonly ProjectContext _dbContext;

        public ProjectRepositoryDb(ProjectContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ProjectEntity? GetById(int id)
        {
            return _dbContext.Projects.Find(id);
        }

        public IEnumerable<ProjectEntity> GetAll()
        {
            return _dbContext.Projects.ToList();
        }

        public IEnumerable<ProjectEntity> Find(Expression<Func<ProjectEntity, bool>> expression)
        {
            return _dbContext.Projects.Where(expression);
        }

        public bool Add(ProjectEntity entity)
        {
            _dbContext.Projects.Add(entity);
            return _dbContext.SaveChanges() > 0;
        }

        public bool AddRange(IEnumerable<ProjectEntity> entities)
        {
            _dbContext.Projects.AddRange(entities);
            return _dbContext.SaveChanges() > 0;
        }

        public bool Remove(ProjectEntity entity)
        {
            _dbContext.Projects.Remove(entity);
            return _dbContext.SaveChanges() > 0;
        }

        public bool RemoveRange(IEnumerable<ProjectEntity> entities)
        {
            _dbContext.Projects.RemoveRange(entities);
            return _dbContext.SaveChanges() > 0;
        }
    }
}
