using System.ComponentModel.DataAnnotations;

#pragma warning disable CS8618 // Title, Description will never be null because they are required

namespace ToDo.Models
{
    public class ToDoPatchModel
    {
        [Required(AllowEmptyStrings = false)]
        public string Title { get; }

        [Required(AllowEmptyStrings = false)]
        public string Description { get; }
    }
}
