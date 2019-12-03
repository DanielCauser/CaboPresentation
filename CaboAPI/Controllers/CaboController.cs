using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using AutoMapper;
using CaboAPI.DTOs;
using CaboAPI.Options;
using CaboAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CaboAPI.Controllers
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CaboController : ControllerBase
    {
        private readonly ILogger<CaboController> _logger;
        private readonly IMapper _mapper;
        private readonly ITodoCaboService _caboService;

        public CaboController(ILogger<CaboController> logger,
            IMapper mapper,
            ITodoCaboService caboService)
        {
            _logger = logger;
            _mapper = mapper;
            _caboService = caboService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TodoCabo2Dto>), 200)]
        public IActionResult Get2()
        {
            _logger.LogInformation("Get Cabo V2 Was called");
            return Ok(_mapper.Map<IEnumerable<TodoCabo2Dto>>(_caboService.GetList()));
        }

        [HttpGet]
        [ApiVersion("1.0", Deprecated = true)]
        [ProducesResponseType(typeof(IEnumerable<TodoCaboDto>), 200)]
        public IActionResult Get()
        {
            _logger.LogInformation("Get Cabo V1 Was called");
            return Ok(_mapper.Map<IEnumerable<TodoCaboDto>>(_caboService.GetList()));
        }

        [HttpGet]
        [ProducesResponseType(typeof(TodoCabo2Dto), 200)]
        [Route("{id}")]
        public IActionResult Get([FromRoute] Guid id)
        {
            if (Guid.Empty == id)
                return BadRequest();
            
            var result = _caboService.GetSingle(id);

            if (result is null)
                return NotFound();
            
            return Ok(_mapper.Map<TodoCabo2Dto>(result));
        }
    }
}
