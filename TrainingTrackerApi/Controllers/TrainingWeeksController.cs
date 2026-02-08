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

    public TrainingWeeksController(ITrainingWeekService service)
    {
        _service = service;
    }

    // GET /api/trainingweeks?search=...
    [HttpGet]
    public async Task<ActionResult<List<TrainingWeekResponseDto>>> GetAll([FromQuery] string? search)
    {
        var weeks = await _service.GetAllAsync(search);
        var dto = weeks.Select(w => new TrainingWeekResponseDto
        {
            Id = w.Id,
            Title = w.Title,
            Description = w.Description,
            WeekStart = w.WeekStart
        }).ToList();

        return Ok(dto);
    }

    // GET /api/trainingweeks/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<TrainingWeekResponseDto>> GetById(int id)
    {
        var week = await _service.GetByIdAsync(id);
        if (week is null) return NotFound();

        return Ok(new TrainingWeekResponseDto
        {
            Id = week.Id,
            Title = week.Title,
            Description = week.Description,
            WeekStart = week.WeekStart
        });
    }

    // POST /api/trainingweeks
    [HttpPost]
    public async Task<ActionResult<TrainingWeekResponseDto>> Create([FromBody] TrainingWeekCreateDto dto)
    {
        var model = new TrainingWeek
        {
            Title = dto.Title,
            Description = dto.Description,
            WeekStart = dto.WeekStart
        };

        var created = await _service.CreateAsync(model);

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, new TrainingWeekResponseDto
        {
            Id = created.Id,
            Title = created.Title,
            Description = created.Description,
            WeekStart = created.WeekStart
        });
    }

    // PUT /api/trainingweeks/{id}
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] TrainingWeekUpdateDto dto)
    {
        var updated = new TrainingWeek
        {
            Title = dto.Title,
            Description = dto.Description,
            WeekStart = dto.WeekStart
        };

        var ok = await _service.UpdateAsync(id, updated);
        if (!ok) return NotFound();

        return NoContent();
    }

    // DELETE /api/trainingweeks/{id}
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _service.DeleteAsync(id);
        if (!ok) return NotFound();

        return NoContent();
    }
}

