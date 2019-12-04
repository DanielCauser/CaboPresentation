using System;
using System.Collections.Generic;
using System.Linq;
using CaboAPI.DTOs;

namespace CaboAPI.Services
{
    public class TodoItemService : ITodoItemService
    {
        public IEnumerable<TodoItemDto> GetMany(Guid id)
        {
            return TheList.Where(x => x.TodoCaboId == id);
        }
        
        private readonly IList<TodoItemDto> TheList = new List<TodoItemDto>
        {
            new TodoItemDto
            {
                Name = "Shovel Snow - NOT",
                TodoCaboId = Guid.Parse("9cb602e9-215c-444d-bffa-d818ab6d6222"),
            },
            new TodoItemDto
            {
                Name = "Have Fun",
                TodoCaboId = Guid.Parse("9cb602e9-215c-444d-bffa-d818ab6d6222"),
            },
            new TodoItemDto
            {
                Name = "Drink beer",
                TodoCaboId = Guid.Parse("ded8f27d-e58b-4e27-8012-8409f38c177b"),
            },
            new TodoItemDto
            {
                Name = "Swim",
                TodoCaboId = Guid.Parse("ded8f27d-e58b-4e27-8012-8409f38c177b"),
            },
        };
    }
}