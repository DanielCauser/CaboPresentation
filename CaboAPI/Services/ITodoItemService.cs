using System;
using System.Collections.Generic;
using CaboAPI.DTOs;

namespace CaboAPI.Services
{
    public interface ITodoItemService
    {
        IEnumerable<TodoItemDto> GetMany(Guid id);
    }
}