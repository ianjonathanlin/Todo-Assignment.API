using System.ComponentModel.DataAnnotations;

namespace Todo_Assignment.API.Models
{
    public class TaskModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Title { get; set; }

        [StringLength(2000)]
        public string Description { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        [StringLength(2000)]
        public string Category { get; set; }

        public bool IsDeleted { get; set; } = false;
        
        public DateTime Created { get; set; } 

        public DateTime Updated { get; set; }
    }
}
