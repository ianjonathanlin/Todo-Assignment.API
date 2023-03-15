namespace Todo_Assignment.API.Models
{
    public class TaskModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime DueDate { get; set; }

        public string Category { get; set; } = string.Empty;

        public bool IsDeleted { get; set; } = false;
        
        public DateTime CreatedDateTime { get; set; } 

        public DateTime LatestUpdatedDateTime { get; set; }
    }
}
