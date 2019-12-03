using System;

namespace CaboAPI.DTOs
{
    public class TodoItem
    {
        public Guid TodoId { get; set; }
        public string Name { get; set; }
        public bool IsDone { get; set; }
    }
}