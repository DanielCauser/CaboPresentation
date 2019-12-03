using System;

namespace CaboAPI.DTOs
{
    public class TodoCaboDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
    }
}
