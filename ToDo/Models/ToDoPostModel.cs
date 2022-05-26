using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace ToDo.Models
{
    [DebuggerDisplay("Title:{Title}\nDeadline:{Deadline}\nDescription:{Description}\n")]
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