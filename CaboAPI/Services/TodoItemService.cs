using System;
using System.Collections.Generic;
using System.Linq;
using CaboAPI.DTOs;
using Microsoft.Extensions.Caching.Memory;

namespace CaboAPI.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly IMemoryCache _memoryCache;

        public TodoItemService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public IEnumerable<TodoItemDto> GetMany(Guid id)
        {
            return _memoryCache.GetOrCreate($"TodoCabo_{id}", entry =>
            {
                entry.SetSlidingExpiration(TimeSpan.FromSeconds(10));
                entry.SetAbsoluteExpiration(TimeSpan.FromSeconds(40));
                return TheList.Where(x => x.TodoCaboId == id);
            });   
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
                Name = "Drink beer",
                TodoCaboId = Guid.Parse("9cb602e9-215c-444d-bffa-d818ab6d6222"),
            },
            new TodoItemDto
            {
                Name = "Have Fun",
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