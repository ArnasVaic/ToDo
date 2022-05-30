using System.Linq.Expressions;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.EntityFrameworkCore;
using ToDo.EntityFramework;
using ToDo.Models;

namespace ToDo.Dependencies
{
    public class ToDoRepositoryDb : IGenericRepository<ToDoEntity>
    {
        private readonly ProjectContext _dbContext;

        public ToDoRepositoryDb(ProjectContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ToDoEntity? GetById(int id)
        {
            return _dbContext.ToDos.Find(id);
        }

        public IEnumerable<ToDoEntity> GetAll()
        {
            return _dbContext.ToDos.ToList();
        }

        public IEnumerable<ToDoEntity> Find(Expression<Func<ToDoEntity, bool>> expression)
        {
            return _dbContext.ToDos.Where(expression);
        }

        public bool Add(ToDoEntity entity)
        {
            _dbContext.ToDos.Add(entity);
            return _dbContext.SaveChanges() > 0;
        }

        public bool AddRange(IEnumerable<ToDoEntity> entities)
        {
            _dbContext.ToDos.AddRange(entities);
            return _dbContext.SaveChanges() > 0;
        }

        public bool Remove(ToDoEntity entity)
        {
            _dbContext.ToDos.Remove(entity);
            return _dbContext.SaveChanges() > 0;
        }

        public bool RemoveRange(IEnumerable<ToDoEntity> entities)
        {
            _dbContext.ToDos.RemoveRange(entities);
            return _dbContext.SaveChanges() > 0;
        }
    }
}
