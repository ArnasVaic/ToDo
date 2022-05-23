using ToDo.Models;

namespace ToDo.Dependencies
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly ILogger<ToDoRepository> _logger;
        public ToDoRepository(ILogger<ToDoRepository> logger)
        {
            _logger = logger;
        }

        private readonly Dictionary<String, ToDoPostModel> posts = new Dictionary<String, ToDoPostModel>();

        public void Create(ToDoPostModel post)
        {
            try
            {
                posts.Add(post.Title, post);
            }
            catch (Exception e) when (e is ArgumentNullException || e is ArgumentException)
            {
                _logger.Log(LogLevel.Information, e.Message);
            }
        }
        public ToDoPostModel Get(string title)
        {
            try 
            {
                return posts[title];
            } catch (Exception e) when (e is ArgumentNullException || e is KeyNotFoundException)
            {
                _logger.Log(LogLevel.Information, e.Message);
                return null; 
            }
        }

        public bool Exists(string title)
        {
            try
            {
                return posts.ContainsKey(title);
            }
            catch(ArgumentNullException e)
            {
                return false;
            }
        }

        public void Delete(string title)
        {
            try
            {
                posts.Remove(title);
            }
            catch(ArgumentNullException e)
            {
                _logger.Log(LogLevel.Information, e.Message);
            }
        }

        public void Update(ToDoPostModel post)
        {
            if(post == null)
            {
                _logger.LogInformation("post parameter passed as null");
                return;
            }
            try
            {
                if (ContainsPost(post.Title))
                {
                    posts[post.Title] = post;
                }
            }
            catch (Exception e) when (e is ArgumentNullException || e is KeyNotFoundException)
            {
                _logger.Log(LogLevel.Information, e.Message);
            }
        }
    }
}
