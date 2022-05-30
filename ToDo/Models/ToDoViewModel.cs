using System.ComponentModel.DataAnnotations;

namespace ToDo.Models
{
    public class ToDoViewModel
    {
        [Required] public int ProjectId { get; set; }

        [Required(AllowEmptyStrings = false)] public string Title { get; set; }

        [Required] public DateTime Deadline { get; set; }

        [Required(AllowEmptyStrings = false)] public string Description { get; set; }
    }
}
