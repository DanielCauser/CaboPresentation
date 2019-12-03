using System;
using System.Collections;
using System.Collections.Generic;
using CaboAPI.Entities;

namespace CaboAPI.Services
{
    public interface ITodoCaboService
    {
        IEnumerable<TodoCabo> GetList();
        TodoCabo GetSingle(Guid id);
    }
}