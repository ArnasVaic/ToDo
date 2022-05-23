using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace ToDo.Models
{
    [DebuggerDisplay("Title:{Title}\nDeadline:{Deadline}\nDescription:{Description}\n")]
    public class ToDoPostModel
    {
        [Required]
        public string Title { get; set; }
        public DateTime Deadline { get; set; }
        public string Description { get; set; }
    }
}