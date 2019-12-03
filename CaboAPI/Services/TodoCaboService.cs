using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using CaboAPI.Entities;
using CaboAPI.Options;
using Microsoft.Extensions.Options;

namespace CaboAPI.Services
{
    public class TodoCaboService : ITodoCaboService
    {
        private readonly IOptions<ExternalServiceConfiguration> _externalServiceConfiguration;

        public TodoCaboService(IOptions<ExternalServiceConfiguration> externalServiceConfiguration)
        {
            _externalServiceConfiguration = externalServiceConfiguration;
        }
        public IEnumerable<TodoCabo> GetList()
        {
            return TheList;
        }

        public TodoCabo GetSingle(Guid id)
        {
            return TheList.SingleOrDefault(x => x.Id == id);
        }
        
        private readonly IEnumerable<TodoCabo> TheList = new List<TodoCabo>
        {
            new TodoCabo
            {
                Id = Guid.NewGuid(),
                NameActivity = "Name1",
                DateStarted = DateTime.Now,
                DateEnded = DateTime.Now.AddDays(2),
                Summary = "Summary1"
            },
            new TodoCabo
            {
                Id = Guid.NewGuid(),
                NameActivity = "Name2",
                DateStarted = DateTime.Now,
                DateEnded = DateTime.Now.AddDays(2),
                Summary = "Summary2"
            }
            ,
            new TodoCabo
            {
                Id = Guid.NewGuid(),
                NameActivity = "Name3",
                DateStarted = DateTime.Now,
                DateEnded = DateTime.Now.AddDays(2),
                Summary = "Summary3"
            }
        };
    }
}