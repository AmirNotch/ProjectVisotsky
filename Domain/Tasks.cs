using System;
using System.Collections.Generic;

namespace Domain
{
    public class Task
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int Status { get; set; }
        public ListTask ListTask { get; set; }
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}