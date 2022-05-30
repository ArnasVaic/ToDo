using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ToDo.Models
{
    public class ProjectEntity
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        public ICollection<ToDoEntity> ToDoGet { get; set; }

        [Required]
        public int Id { get; set; }
    }
}
