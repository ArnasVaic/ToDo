using System.ComponentModel.DataAnnotations;
using ToDo.Models;

#pragma warning disable CS0168

namespace ToDo.Dependencies
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly ILogger<ToDoRepository> _logger;
        public ToDoRepository(ILogger<ToDoRepository> logger)
        {
            _logger = logger;
        }

        // store ToDoGetModel type because it contains the most information?
        private readonly Dictionary<String, ToDoGetModel> _toDoDictionary = new Dictionary<String, ToDoGetModel>();

        public bool Post([Required]ToDoPostModel todo)
        {
            try
            {
                _toDoDictionary.Add(todo.Title, new ToDoGetModel
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

        public ToDoGetModel? Get([Required(AllowEmptyStrings = false)]string title)
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
                _toDoDictionary[todo.Title] = new ToDoGetModel
                {
                    Title = todo.Title,
                    Deadline = todo.Deadline,
                    Description = todo.Description
                };
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
