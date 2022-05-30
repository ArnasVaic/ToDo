using CsvHelper;
using ToDo.Models;

namespace ToDo.Dependencies
{
    public class ToDoRepositoryFile : IToDoRepository
    {
        //private StreamReader stream;
        //private CsvReader reader;
        public ToDoRepositoryFile()
        {
            //stream = new StreamReader()
        }

        public bool Delete(string title)
        {
            throw new NotImplementedException();
        }

        public ToDoEntity? Get(string title)
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
