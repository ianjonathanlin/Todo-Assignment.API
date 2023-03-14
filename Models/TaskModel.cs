using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Todo_Assignment.API.Models
{
    public class TaskModel
    {
        public int Id { get; }

        [Required]
        [StringLength(250)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(2000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        [StringLength(2000)]
        public string Category { get; set; } = string.Empty;

        [Column(TypeName = "bit(1)")]
        public bool IsDeleted { get; } = false;

        public DateTime CreatedDateTime { get; }

        public DateTime LatestUpdatedDateTime { get; }
    }
}
