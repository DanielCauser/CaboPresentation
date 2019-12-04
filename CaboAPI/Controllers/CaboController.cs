using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using AutoMapper;
using CaboAPI.DTOs;
using CaboAPI.Entities;
using CaboAPI.Options;
using CaboAPI.Services;
using Microsoft.AspNetCore.Http;
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
        [ApiVersion("1.0", Deprecated = true)]
        [ProducesResponseType(typeof(IEnumerable<TodoCaboDto>), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            _logger.LogInformation("Get Cabo V1 Was called");
            return Ok(_mapper.Map<IEnumerable<TodoCaboDto>>(_caboService.GetList()));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TodoCabo2Dto>), StatusCodes.Status200OK)]
        public IActionResult Get2()
        {
            _logger.LogInformation("Get Cabo V2 Was called");
            return Ok(_mapper.Map<IEnumerable<TodoCabo2Dto>>(_caboService.GetList()));
        }

        [HttpGet]
        [ProducesResponseType(typeof(TodoCabo2Dto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("{id}")]
        public IActionResult GetSingleTodoCabo([FromRoute] Guid id, ApiVersion apiVersion)
        {
            if (Guid.Empty == id)
                return BadRequest();
            
            var result = _caboService.GetSingle(id);

            if (result is null)
                return NotFound();
            
            return Ok(_mapper.Map<TodoCabo2Dto>(result));
        }

        [HttpPost]
        [ProducesResponseType(typeof(TodoCabo2Dto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddCabo([FromBody] TodoCaboCreateDto todoCaboCreateDto, ApiVersion apiVersion)
        {
            var toAdd = _mapper.Map<TodoCabo>(todoCaboCreateDto);

            var result = _caboService.Save(toAdd);

            if (!result)
                return StatusCode(500);

            return CreatedAtAction(nameof(GetSingleTodoCabo),
                new
                {
                    id = toAdd.Id,
                    apiVersion = HttpContext.GetRequestedApiVersion().ToString()
                }, _mapper.Map<TodoCabo2Dto>(toAdd));
        }

        [HttpPut]
        [ProducesResponseType(typeof(TodoCabo2Dto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("{id}")]
        public IActionResult UpdateCabo([FromRoute] Guid id, [FromBody] TodoCaboUpdateDto todoCaboCreateDto,
            ApiVersion apiVersion)
        {
            if (Guid.Empty == id)
                return BadRequest();

            var existing = _caboService.GetSingle(id);

            if (existing is null)
                return NotFound();

            var toUpdate = _mapper.Map<TodoCabo>(todoCaboCreateDto);

            var result = _caboService.Save(toUpdate);

            if (!result)
                return StatusCode(500);

            return Ok(_mapper.Map<TodoCabo2Dto>(toUpdate));
        }
    }
}
