# TrainingTrackerApi

## EF Core migration troubleshooting

If `Add-Migration InitialCreate` fails with a generic "failed when building" error even though you have .NET 10 and EF Core 10 installed, check the following:

1. **Make sure the correct startup project is selected.**
   - In Visual Studio, set `TrainingTrackerApi` as the startup project.
   - In the Package Manager Console, run `Add-Migration InitialCreate -Project TrainingTrackerApi -StartupProject TrainingTrackerApi`.

2. **Confirm the EF Core tools match your SDK version.**
   - Ensure `Microsoft.EntityFrameworkCore.Tools` version matches the EF Core packages in the project.
   - If you use the CLI, install the matching tool: `dotnet tool install --global dotnet-ef --version 10.0.2`.

3. **Verify the project builds cleanly outside of migrations.**
   - Run `dotnet build TrainingTrackerApi/TrainingTrackerApi.csproj` and fix any compile errors first.

4. **Ensure a design-time context can be created.**
   - `AppDbContext` is registered via `AddDbContext` and uses `DefaultConnection` from configuration, so confirm the connection string exists in `appsettings.json` or `appsettings.Development.json`.
   - If configuration isn't loading correctly, add an `IDesignTimeDbContextFactory<AppDbContext>` so migrations can be created without the running host.
