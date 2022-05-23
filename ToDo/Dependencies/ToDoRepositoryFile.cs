using ToDo.Models;

namespace ToDo.Dependencies
{
    public class ToDoRepositoryFile : IToDoRepository
    {

        public void AddPost(ToDoPostModel post)
        {
            throw new NotImplementedException();
        }

        public bool ContainsPost(string title)
        {
            throw new NotImplementedException();
        }

        public void DeletePost(string title)
        {
            throw new NotImplementedException();
        }

        public ToDoPostModel GetPost(string title)
        {
            throw new NotImplementedException();
        }

        public void ReplacePost(ToDoPostModel post)
        {
            throw new NotImplementedException();
        }
    }
}
