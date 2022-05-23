using ToDo.Models;

namespace ToDo.Dependencies
{
    public interface IToDoRepository
    {
        /// <summary>
        /// Adds a ToDoPostModel to this repository
        /// </summary>
        /// <param name="post"></param>
        public void Create(ToDoPostModel post);

        /// <summary>
        /// Finds post by title
        /// </summary>
        /// <param name="title"></param>
        /// <returns>post with matching title, if there exists no such post returns null</returns>
        public ToDoPostModel Get(string title);

        /// <summary>
        /// Check whether a post with a certain title exists
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public bool Exists(string title);

        /// <summary>
        /// Remove a post by title
        /// </summary>
        /// <param name="title"></param>
        public void Delete(string title);

        /// <summary>
        /// Replace an existing post with matching title
        /// </summary>
        /// <param name="post"></param>
        public void Update(ToDoPostModel post);
    }
}
