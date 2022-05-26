using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace ToDo.Models
{
    [DebuggerDisplay("Title:{Title}\nDeadline:{Deadline}\nDescription:{Description}\nId:{Id}")]
    public class ToDoGetModel
    {
        [Required(AllowEmptyStrings = false)]
        public string Title { get; }

        [Required(AllowEmptyStrings = false)]
        public string Description { get; }

        [Required()]
        public DateTime Deadline { get; }

        [Required()]
        public int Id { get; }
    }
}
