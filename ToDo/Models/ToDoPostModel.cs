using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

#pragma warning disable CS8618 // Title, Description will never be null because they are required

namespace ToDo.Models
{
    public class ToDoPostModel
    {
        [Required(AllowEmptyStrings = false)]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; }

        [Required()]
        public DateTime Deadline { get; set; }
    }
}