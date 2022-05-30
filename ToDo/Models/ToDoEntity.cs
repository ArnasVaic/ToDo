using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

#pragma warning disable CS8618 // Title, Description will never be null because they are required

namespace ToDo.Models
{
    public class ToDoEntity
    {
        public ToDoEntity()
        {

        }
        public ToDoEntity(ToDoPostModel post)
        {
            Replace(post);
            ProjectId = post.ProjectId;
        }

        public void Update(ToDoPatchModel patch)
        {
            this.Description = patch.Description;
        }

        public void Replace(ToDoPostModel post)
        {
            Title = post.Title;
            Description = post.Description;
            Deadline = post.Deadline;
        }

        [Required()]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; }

        [Required()]
        public DateTime Deadline { get; set; }

        public int ProjectId { get; set; }
        public ProjectEntity Project { get; set; }
    }
}
