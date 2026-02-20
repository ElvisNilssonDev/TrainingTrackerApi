using Microsoft.EntityFrameworkCore;
using TrainingTrackerApi.Data;
using TrainingTrackerApi.Services;
using TrainingTrackerApi.Mapping;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProfile));

var useInMemoryDatabase = builder.Configuration.GetValue<bool>("UseInMemoryDatabase");
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    if (string.IsNullOrWhiteSpace(connectionString))
        throw new InvalidOperationException("DefaultConnection is missing. Set it in appsettings.json or enable UseInMemoryDatabase properly.");

    opt.UseSqlServer(connectionString);
});

// DI: AddScoped (krav)
builder.Services.AddScoped<ITrainingWeekService, TrainingWeekService>();
// builder.Services.AddScoped<ILiftEntryService, LiftEntryService>();
// builder.Services.AddScoped<INutritionEntryService, NutritionEntryService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "TrainingTracker API v1");
    options.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
