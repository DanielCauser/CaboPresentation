using System;

namespace CaboAPI.Entities
{
    public class TodoCabo
    {
        public Guid Id { get; set; }
        public DateTime DateStarted { get; set; }
        public DateTime DateEnded { get; set; }
        public string NameActivity { get; set; }
        public string Summary { get; set; }
    }
}