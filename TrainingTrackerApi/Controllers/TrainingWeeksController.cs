using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TrainingTrackerApi.Dtos;
using TrainingTrackerApi.Models;
using TrainingTrackerApi.Services;

namespace TrainingTrackerApi.Controllers;

[ApiController]
[Route("api/trainingweeks")]
public class TrainingWeeksController : ControllerBase
{
    private readonly ITrainingWeekService _service;
    private readonly IMapper _mapper;

    public TrainingWeeksController(ITrainingWeekService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<TrainingWeekResponseDto>>> GetAll([FromQuery] string? search)
    {
        var weeks = await _service.GetAllAsync(search);
        return Ok(_mapper.Map<List<TrainingWeekResponseDto>>(weeks));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TrainingWeekResponseDto>> GetById(int id)
    {
        var week = await _service.GetByIdAsync(id);
        if (week is null) return NotFound();

        return Ok(_mapper.Map<TrainingWeekResponseDto>(week));
    }

    [HttpPost]
    public async Task<ActionResult<TrainingWeekResponseDto>> Create([FromBody] TrainingWeekCreateDto dto)
    {
        var model = _mapper.Map<TrainingWeek>(dto);
        var created = await _service.CreateAsync(model);

        var response = _mapper.Map<TrainingWeekResponseDto>(created);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] TrainingWeekUpdateDto dto)
    {
        var updated = _mapper.Map<TrainingWeek>(dto);
        var ok = await _service.UpdateAsync(id, updated);
        if (!ok) return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _service.DeleteAsync(id);
        if (!ok) return NotFound();

        return NoContent();
    }
}
