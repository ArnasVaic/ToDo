using ToDo.Models;

namespace ToDo.Dependencies
{
    public class ToDoRepositoryFile : IToDoRepository
    {

        public void Create(ToDoPostModel post)
        {
            throw new NotImplementedException();
        }

        public bool Exists(string title)
        {
            throw new NotImplementedException();
        }

        public void Delete(string title)
        {
            throw new NotImplementedException();
        }

        public ToDoPostModel Get(string title)
        {
            throw new NotImplementedException();
        }

        public void Update(ToDoPostModel post)
        {
            throw new NotImplementedException();
        }
    }
}
