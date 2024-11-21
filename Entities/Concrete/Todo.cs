using Entities.Abstract;

namespace Entities.Concrete
{
    public class Todo : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public TodoStatus Status { get; set; }
        public string CreatedUserId { get; set; }
        public User CreatedUser { get; set; }
    }

    public enum TodoStatus
    {
        Pending,
        Completed,
        InProgress,
        Cancelled
    }
}
