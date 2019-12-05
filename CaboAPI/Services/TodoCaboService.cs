using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using CaboAPI.Entities;
using CaboAPI.Options;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace CaboAPI.Services
{
    public class TodoCaboService : ITodoCaboService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IOptions<ExternalServiceConfiguration> _externalServiceConfiguration;

        public TodoCaboService(IMemoryCache memoryCache,
            IOptions<ExternalServiceConfiguration> externalServiceConfiguration)
        {
            _memoryCache = memoryCache;
            _externalServiceConfiguration = externalServiceConfiguration;
        }

        public IEnumerable<TodoCabo> GetList()
        {
            return _memoryCache.GetOrCreate("TodoCabo_List", entry =>
            {
                entry.SetSlidingExpiration(TimeSpan.FromSeconds(10));
                entry.SetAbsoluteExpiration(TimeSpan.FromSeconds(40));
                return TheList;
            });
        }

        public TodoCabo GetSingle(Guid id)
        {
            return TheList.SingleOrDefault(x => x.Id == id);
        }

        public bool Save(TodoCabo model)
        {
            if (model.Id == Guid.Empty)
            {
                model.Id = Guid.NewGuid();
                TheList.Add(model);
                return true;
            }
            else
            {
                var substitute = TheList.FirstOrDefault(x => x.Id == model.Id);
                substitute = model;
                return true;
            }

            return false;
        }

        public void Delete(TodoCabo existing)
        {
            TheList.Remove(existing);
        }

        private readonly IList<TodoCabo> TheList = new List<TodoCabo>
        {
            new TodoCabo
            {
                Id = Guid.Parse("9cb602e9-215c-444d-bffa-d818ab6d6222"),
                NameActivity = "Name1",
                DateStarted = DateTime.Now,
                DateEnded = DateTime.Now.AddDays(2),
                Summary = "Summary1"
            },
            new TodoCabo
            {
                Id = Guid.Parse("ded8f27d-e58b-4e27-8012-8409f38c177b"),
                NameActivity = "Name2",
                DateStarted = DateTime.Now,
                DateEnded = DateTime.Now.AddDays(6),
                Summary = "Summary2"
            },
            new TodoCabo
            {
                Id = Guid.Parse("a581cf2e-daac-47ee-ad77-30b10653db73"),
                NameActivity = "Name3",
                DateStarted = DateTime.Now,
                DateEnded = DateTime.Now.AddDays(1),
                Summary = "Summary3"
            }
        };
    }
}