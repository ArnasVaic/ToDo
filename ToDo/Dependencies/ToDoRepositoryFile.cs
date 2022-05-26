using ToDo.Models;

namespace ToDo.Dependencies
{
    public class ToDoRepositoryFile : IToDoRepository
    {
        public bool Delete(string title)
        {
            throw new NotImplementedException();
        }

        public ToDoGetModel? Get(string title)
        {
            throw new NotImplementedException();
        }

        public bool Patch(ToDoPatchModel todo)
        {
            throw new NotImplementedException();
        }

        public bool Post(ToDoPostModel todo)
        {
            throw new NotImplementedException();
        }

        public bool Put(ToDoPostModel todo)
        {
            throw new NotImplementedException();
        }
    }
}
