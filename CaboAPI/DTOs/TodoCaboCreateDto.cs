using System;

namespace CaboAPI.DTOs
{
    public class TodoCaboCreateDto
    {
        public DateTime DateStarted { get; set; }
        public DateTime DateEnded { get; set; }
        public string NameActivity { get; set; }
        public string Summary { get; set; }
    }
}