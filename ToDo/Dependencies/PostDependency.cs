using ToDo.Models;

namespace ToDo.Dependencies
{
    public class PostDependency : IPostDependency
    {
        public Dictionary<String, ToDoPostModel> posts = new Dictionary<String, ToDoPostModel>();
    }
}
