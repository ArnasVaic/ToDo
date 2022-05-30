using ToDo.Models;

namespace ToDo.Dependencies
{
    public interface IToDoRepository
    {
        /// <summary>
        /// Post a new ToDo
        /// </summary>
        /// <param name="todo"></param>
        /// <returns> true </returns>
        public bool Post(ToDoPostModel todo);

        /// <summary>
        /// Retrieve a ToDo by title
        /// </summary>
        /// <param name="title"></param>
        /// <returns>post with matching title, if there exists no such post returns null</returns>
        public ToDoEntity? Get(string title);

        /// <summary>
        /// Remove a ToDo by title
        /// </summary>
        /// <param name="title"></param>
        public bool Delete(string title);

        /// <summary>
        /// Replace existing ToDo
        /// </summary>
        /// <param name="todo"></param>
        /// <returns></returns>
        public bool Put(ToDoPostModel todo);

        /// <summary>
        /// Update description of an existing ToDo
        /// </summary>
        /// <param name="todo"></param>
        /// <returns></returns>
        public bool Patch(ToDoPatchModel todo);
    }
}
