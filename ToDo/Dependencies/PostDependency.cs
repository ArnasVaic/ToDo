namespace ToDo2.Dependencies
{
    public class PostDependency : IPostDependency
    {
        public Dictionary<String, ToDoPost> posts = new Dictionary<String, ToDoPost>();
    }
}
