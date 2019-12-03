using System;
using System.Collections.Generic;
using System.Linq;
using CaboAPI.DTOs;
using CaboAPI.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CaboAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CaboController : ControllerBase
    {
        private readonly ExternalServiceConfiguration _serviceConfig;
        private readonly CaboApiConfiguration _caboConfig;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public CaboController(IOptions<ExternalServiceConfiguration> serviceConfig, IOptions<CaboApiConfiguration> caboConfig)
        {
            _serviceConfig = serviceConfig.Value;
            _caboConfig = caboConfig.Value;
        }

        [HttpGet]
        public IEnumerable<CaboDto> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new CaboDto
            {
                Date = DateTime.Now.AddDays(index),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
