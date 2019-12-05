using System;
using System.Collections.Generic;
using AutoMapper;
using CaboAPI.DTOs;
using CaboAPI.Entities;
using CaboAPI.Filters;
using CaboAPI.Services;
using CaboAPI.Validations;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.JsonPatch;


namespace CaboAPI.Controllers
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class CaboController : ControllerBase
    {
        private readonly ILogger<CaboController> _logger;
        private readonly IMapper _mapper;
        private readonly ITodoCaboService _caboService;
        private readonly ITodoItemService _todoItemService;

        public CaboController(ILogger<CaboController> logger,
            IMapper mapper,
            ITodoCaboService caboService,
            ITodoItemService todoItemService)
        {
            _logger = logger;
            _mapper = mapper;
            _caboService = caboService;
            _todoItemService = todoItemService;
        }

        [HttpGet]
        [ApiVersion("1.0", Deprecated = true)]
        [AddHeader("Author", "Causer")]
        [ResponseCache(Duration = 30)]
        [ProducesResponseType(typeof(IEnumerable<TodoCaboDto>), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            _logger.LogInformation("Get Cabo V1 Was called");
            return Ok(_mapper.Map<IEnumerable<TodoCaboDto>>(_caboService.GetList()));
        }

        [HttpGet]
        [ResponseCache(Duration = 30)]
        [ProducesResponseType(typeof(IEnumerable<TodoCabo2Dto>), StatusCodes.Status200OK)]
        public IActionResult Get2()
        {
            _logger.LogInformation("Get Cabo V2 Was called");
            return Ok(_mapper.Map<IEnumerable<TodoCabo2Dto>>(_caboService.GetList()));
        }

        [HttpGet]
        [ProducesResponseType(typeof(TodoCabo2Dto), StatusCodes.Status200OK)]
        [ResponseCache(Duration = 30, VaryByQueryKeys = new []{"*"})]
        [Route("{id}")]
        public IActionResult GetSingleTodoCabo([FromRoute] Guid id, ApiVersion apiVersion)
        {
            if (Guid.Empty == id)
                return BadRequest(ModelState);

            var result = _caboService.GetSingle(id);

            if (result is null)
                return NotFound();

            return Ok(_mapper.Map<TodoCabo2Dto>(result));
        }

        [HttpPost]
        [ProducesResponseType(typeof(TodoCabo2Dto), StatusCodes.Status200OK)]
        public IActionResult AddCabo([FromBody] TodoCaboCreateDto todoCaboCreateDto, ApiVersion apiVersion)
        {   
            if (!ModelState.IsValid)
                BadRequest(ModelState);

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
        [Route("{id}")]
        public IActionResult UpdateCabo([FromRoute] Guid id, [FromBody] TodoCaboUpdateDto todoCaboUpdateDto,
            ApiVersion apiVersion)
        {
            if (!ModelState.IsValid)
                BadRequest(ModelState);
            
            if (Guid.Empty == id)
                return BadRequest();

            var existing = _caboService.GetSingle(id);

            if (existing is null)
                return NotFound();

            var toUpdate = _mapper.Map<TodoCabo>(todoCaboUpdateDto);

            var result = _caboService.Save(toUpdate);

            if (!result)
                return StatusCode(500);

            return Ok(_mapper.Map<TodoCabo2Dto>(toUpdate));
        }

        [HttpPatch]
        [ProducesResponseType(typeof(TodoCabo2Dto), StatusCodes.Status200OK)]
        [Route("{id}")]
        public IActionResult PartiallyUpdate([FromRoute] Guid id,
            [FromBody] JsonPatchDocument<TodoCaboUpdateDto> todoCaboUpdateDto,
            ApiVersion apiVersion)
        {
            var existing = _caboService.GetSingle(id);

            if (existing is null)
                return NotFound();

            var toPatch = _mapper.Map<TodoCaboUpdateDto>(existing);
            todoCaboUpdateDto.ApplyTo(toPatch);
            
            var validator = new TodoCaboUpdateDtoValidator();
            var results = validator.Validate(toPatch);

            results.AddToModelState(ModelState, null);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _mapper.Map(toPatch, existing);

            var result = _caboService.Save(existing);

            if (!result)
                return StatusCode(500);

            return Ok(_mapper.Map<TodoCabo2Dto>(existing));
        }
        
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Route("{id}")]
        public IActionResult Remove([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest(ModelState);

            var existing = _caboService.GetSingle(id);

            if (existing is null)
                return NotFound();

            _caboService.Delete(existing);

            return NoContent();
        }
        
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TodoItemDto>),StatusCodes.Status200OK)]
        [Route("{id}/items")]
        public IActionResult GetItems([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest(ModelState);

            var existing = _todoItemService.GetMany(id);

            if (existing is null)
                return NotFound();

            return Ok(_mapper.Map<IEnumerable<TodoItemDto>>(existing));
        }
    }
}
