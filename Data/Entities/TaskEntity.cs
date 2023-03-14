using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Todo_Assignment.API.Data.Entities
{
    public class TaskEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime DueDate { get; set; }
        public string Category { get; set; } = null!;
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedDateTime { get; set; }
        public DateTime LatestUpdatedDateTime { get; set; }
    }
}
