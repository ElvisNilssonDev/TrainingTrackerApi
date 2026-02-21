using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TrainingTrackerApi.Dtos;
using TrainingTrackerApi.Models;
using TrainingTrackerApi.Services;

namespace TrainingTrackerApi.Controllers;

[ApiController]
[Route("api/trainingdays")]
public class TrainingDaysController : ControllerBase
{
    private readonly ITrainingDayService _service;
    private readonly IMapper _mapper;

    public TrainingDaysController(ITrainingDayService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<TrainingDayResponseDto>>> GetAll([FromQuery] int? trainingWeekId, [FromQuery] string? search)
    {
        var days = await _service.GetAllAsync(trainingWeekId, search);
        return Ok(_mapper.Map<List<TrainingDayResponseDto>>(days));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TrainingDayResponseDto>> GetById(int id)
    {
        var day = await _service.GetByIdAsync(id);
        if (day is null) return NotFound();

        return Ok(_mapper.Map<TrainingDayResponseDto>(day));
    }

    [HttpPost]
    public async Task<ActionResult<TrainingDayResponseDto>> Create([FromBody] TrainingDayCreateDto dto)
    {
        var model = _mapper.Map<TrainingDay>(dto);
        var created = await _service.CreateAsync(model);
        if (created is null) return BadRequest("TrainingWeekId does not exist.");

        var response = _mapper.Map<TrainingDayResponseDto>(created);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] TrainingDayUpdateDto dto)
    {
        var updated = _mapper.Map<TrainingDay>(dto);
        var result = await _service.UpdateAsync(id, updated);

        return result switch
        {
            TrainingDayUpdateResult.Updated => NoContent(),
            TrainingDayUpdateResult.NotFound => NotFound(),
            TrainingDayUpdateResult.InvalidTrainingWeek => BadRequest("TrainingWeekId does not exist."),
            _ => StatusCode(StatusCodes.Status500InternalServerError)
        };
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _service.DeleteAsync(id);
        if (!ok) return NotFound();

        return NoContent();
    }
}
