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
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CaboController : ControllerBase
    {
        private readonly ILogger<CaboController> _logger;
        private readonly ExternalServiceConfiguration _serviceConfig;
        private readonly CaboApiConfiguration _caboConfig;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public CaboController(IOptions<ExternalServiceConfiguration> serviceConfig,
            IOptions<CaboApiConfiguration> caboConfig,
            ILogger<CaboController> logger)
        {
            _logger = logger;
            _serviceConfig = serviceConfig.Value;
            _caboConfig = caboConfig.Value;
        }

        [HttpGet]
        [ApiVersion("2.0")]
        [ProducesResponseType(typeof(IEnumerable<TodoCabo2Dto>), 200)]
        public IEnumerable<TodoCabo2Dto> Get2()
        {
            _logger.LogInformation("Get Cabo V2 Was called");
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new TodoCabo2Dto
                {
                    DateStarted = DateTime.Now.AddDays(index),
                    DateEnded = DateTime.Now.AddDays(index + 5),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TodoCaboDto>), 200)]

    public IEnumerable<TodoCaboDto> Get()
        {
            _logger.LogInformation("Get Cabo V1 Was called");
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new TodoCaboDto
                {
                    Date = DateTime.Now.AddDays(index),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();
        }
    }
}
