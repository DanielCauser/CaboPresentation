using System;

namespace CaboAPI.DTOs
{
    public class TodoItemDto
    {
        public Guid TodoCaboId { get; set; }
        public string Name { get; set; }
        public bool IsDone { get; set; } = true;
    }
}