using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Todo : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public TodoStatus Status { get; set; }
    }

    public enum TodoStatus
    {
        Pending,
        Completed,
        InProgress,
        Cancelled
    }
}
