using Microsoft.EntityFrameworkCore;
using TrainingTrackerApi.Data;
using TrainingTrackerApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
