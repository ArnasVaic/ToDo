using System.ComponentModel.DataAnnotations;
using ToDo.Models;

#pragma warning disable CS0168

namespace ToDo.Dependencies
{
    public class ToDoRepositoryMem : IToDoRepository
    {
        // store ToDoGetModel type because it contains the most information?
        private readonly Dictionary<String, ToDoEntity> _toDoDictionary = new Dictionary<String, ToDoEntity>();

        public bool Post([Required]ToDoPostModel todo)
        {
            try
            {
                _toDoDictionary.Add(todo.Title, new ToDoEntity
                {
                    Title = todo.Title,
                    Deadline = todo.Deadline,
                    Description = todo.Description,
                    Id = 0 // TODO: figure out how to generate ids
                });
                return true;
            } catch(ArgumentException e) {
                // key already exists
                return false;
            }
        }

        public ToDoEntity? Get([Required(AllowEmptyStrings = false)]string title)
        {
            return _toDoDictionary.ContainsKey(title) ? _toDoDictionary[title] : null;
        }

        public bool Delete([Required(AllowEmptyStrings = false)] string title)
        {
            return _toDoDictionary.Remove(title);
        }

        public bool Put([Required]ToDoPostModel todo)
        {
            try
            {
                _toDoDictionary[todo.Title] = new ToDoEntity(todo);
                return true;
            }
            catch(KeyNotFoundException e)
            {
                return false;
            }
        }

        public bool Patch([Required] ToDoPatchModel todo)
        {
            try
            {
                _toDoDictionary[todo.Title].Description = todo.Description;
                return true;
            }
            catch (KeyNotFoundException e)
            {
                return false;
            }
        }
    }
}
