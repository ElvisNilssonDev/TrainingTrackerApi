using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TrainingTrackerApi.Dtos;
using TrainingTrackerApi.Models;
using TrainingTrackerApi.Services;

namespace TrainingTrackerApi.Controllers;

[ApiController]
[Route("api/liftentries")]
public class LiftEntryController : ControllerBase
{
    private readonly ILiftEntryService _service;
    private readonly IMapper _mapper;

    public LiftEntryController(ILiftEntryService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<LiftEntryResponseDto>>> GetAll([FromQuery] int? trainingDayId, [FromQuery] string? exercise)
    {
        var entries = await _service.GetAllAsync(trainingDayId, exercise);
        return Ok(_mapper.Map<List<LiftEntryResponseDto>>(entries));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<LiftEntryResponseDto>> GetById(int id)
    {
        var entry = await _service.GetByIdAsync(id);
        if (entry is null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<LiftEntryResponseDto>(entry));
    }

    [HttpPost]
    public async Task<ActionResult<LiftEntryResponseDto>> Create([FromBody] LiftEntryCreateDto dto)
    {
        var model = _mapper.Map<LiftEntry>(dto);
        var created = await _service.CreateAsync(model);

        if (created is null)
        {
            return BadRequest("TrainingDayId does not exist.");
        }

        var response = _mapper.Map<LiftEntryResponseDto>(created);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] LiftEntryUpdateDto dto)
    {
        var updated = _mapper.Map<LiftEntry>(dto);
        var result = await _service.UpdateAsync(id, updated);

        return result switch
        {
            LiftEntryUpdateResult.Updated => NoContent(),
            LiftEntryUpdateResult.NotFound => NotFound(),
            LiftEntryUpdateResult.InvalidTrainingDay => BadRequest("TrainingDayId does not exist."),
            _ => StatusCode(StatusCodes.Status500InternalServerError)
        };
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _service.DeleteAsync(id);
        if (!ok)
        {
            return NotFound();
        }

        return NoContent();
    }
}
