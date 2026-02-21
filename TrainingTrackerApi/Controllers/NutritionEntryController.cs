using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TrainingTrackerApi.Dtos;
using TrainingTrackerApi.Models;
using TrainingTrackerApi.Services;

namespace TrainingTrackerApi.Controllers;

[ApiController]
[Route("api/nutritionentries")]
public class NutritionEntryController : ControllerBase
{
    private readonly INutritionEntryService _service;
    private readonly IMapper _mapper;

    public NutritionEntryController(INutritionEntryService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<NutritionEntryDto>>> GetAll([FromQuery] int? trainingDayId, [FromQuery] string? title)
    {
        var entries = await _service.GetAllAsync(trainingDayId, title);
        return Ok(_mapper.Map<List<NutritionEntryDto>>(entries));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<NutritionEntryDto>> GetById(int id)
    {
        var entry = await _service.GetByIdAsync(id);
        if (entry is null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<NutritionEntryDto>(entry));
    }

    [HttpPost]
    public async Task<ActionResult<NutritionEntryDto>> Create([FromBody] NutritionEntryCreateDto dto)
    {
        var model = _mapper.Map<NutritionEntry>(dto);
        var created = await _service.CreateAsync(model);

        if (created is null)
        {
            return BadRequest("TrainingDayId does not exist.");
        }

        var response = _mapper.Map<NutritionEntryDto>(created);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] NutritionEntryUpdateDto dto)
    {
        var updated = _mapper.Map<NutritionEntry>(dto);
        var result = await _service.UpdateAsync(id, updated);

        return result switch
        {
            NutritionEntryUpdateResult.Updated => NoContent(),
            NutritionEntryUpdateResult.NotFound => NotFound(),
            NutritionEntryUpdateResult.InvalidTrainingDay => BadRequest("TrainingDayId does not exist."),
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