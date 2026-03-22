# BodyCore
A training app for my own goal of becoming a bodybuilder for asthetic purposes. All apps for training is pay to use so I will create my own!<br/>
* The app will contain a way for Nutrition entries to be added for steady calorie count.
* The app will give you the resources to logg your weeks, days and specific lifts.

# Installation requirements
Install these first:<br/>
<br/>
Visual Studio 2022 or newer
During install, include:
ASP.NET and web development
Data storage and processing if available
.NET 10 SDK
Your API project targets net10.0, so an older SDK will fail to build. Microsoft’s current .NET download page shows .NET 10 as an LTS release.<br/>
<br/>
SQL Server Express
Microsoft currently offers SQL Server 2025 Express as the free edition.<br/>
<br/>
SSMS
Microsoft’s current generally available SSMS release is SSMS 22, and Microsoft says SSMS 22 is the latest GA version.<br/>
<br/>
VS Code
With these extensions:
Live Server
JavaScript and HTML/CSS support
optional: REST Client<br/>
<br/>
# Installation part 1
Step 1

Clone the API repo:

git clone https://github.com/ElvisNilssonDev/BodyCoreApi.git <br/>

Step 2

Open the folder in Visual Studio.

Open either:

the solution file if Visual Studio sees it, or
the TrainingTrackerApi project folder directly <br/>

Step 3

Wait for NuGet packages to restore.

Your project uses:

Entity Framework Core
SQL Server provider
EF tools
Swagger
AutoMapper

If restore fails, run this in the Package Manager Console or terminal: dotnet restore<br/>
# Installation part 2
Set up SQL Server Express and SSMS

Your current API connection string in appsettings.json is:

"DefaultConnection": "Server=.\\SQLEXPRESS;Database=TrainingTrackerDb;Trusted_Connection=True;TrustServerCertificate=True"

That means the API expects a local SQL Server Express instance named SQLEXPRESS and a database called TrainingTrackerDb.

Step 1

Install SQL Server Express.

During install, make sure the instance name is:

SQLEXPRESS

If you pick a different name, you must update appsettings.json.

Step 2

Install SSMS and open it.

In SSMS, connect using:

Server type: Database Engine
Server name: .\SQLEXPRESS
Authentication: Windows Authentication

If that connects, SQL Server Express is working.
# Installation part 3
In VisualStudio In your program.cs you see CORS<br/>
make sure your localhost match with what you have, if you use live studio you can copy that adress to fool proof the CORS.<br/>

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy
                .WithOrigins(
                    "https://localhost:7239",
                    "http://localhost:5122",
                    "http://127.0.0.1:5501",
                    "http://127.0.0.1:5501/index.html"
                )
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
<br/>

Now clone and open the frontend named BodyCore https://github.com/ElvisNilssonDev/BodyCore in VSCODE make sure your<br/>

const API_BASE = "https://localhost:7239/api";

in script.js match your own localhost.<br/>
# Installation part 4
Create the database from the API

Your API has:

AppDbContext
DbSet<TrainingWeek>
DbSet<TrainingDay>
DbSet<LiftEntry>
DbSet<NutritionEntry>

So now you need the database schema created.

Option A: Use existing migrations

If your repo already has valid migrations, run this in the TrainingTrackerApi PackageManager:

Update-Database <br/>

Option B: If migrations are missing or outdated

Run:

Add-Migration InitialCreate
Update-Database
<br/>

If dotnet ef is not installed globally run in powershell:

dotnet tool install --global dotnet-ef
After that

Refresh SSMS.
You should see a database called:

TrainingTrackerDb

and tables for your training/nutrition data.
# Run the app
Now with everything installed you should be able to run the API with the database connected.
if you havent already downloaded the LiveServer in visualstudio code do so and then click on index.html, then click in the right corner on the LiveServer icon [ ((o))Go Live ]
and if everything is done correctly it should work and open the app.


# BodyCore features:
![Front:](https://github.com/ElvisNilssonDev/BodyCoreApi/blob/631910cb421735ccfef6ec16c08ff4d47651af24/Images/BodyCoreFront.png)<br/>
# Add Lift
![AddLift:](https://github.com/ElvisNilssonDev/BodyCoreApi/blob/631910cb421735ccfef6ec16c08ff4d47651af24/Images/BodyCoreAddLift.png)
# Add Nutrition
![AddNutrition:](https://github.com/ElvisNilssonDev/BodyCoreApi/blob/631910cb421735ccfef6ec16c08ff4d47651af24/Images/BodyCoreNutritionAdd.png)<br/>
# Management buttons
![Manage:](https://github.com/ElvisNilssonDev/BodyCoreApi/blob/631910cb421735ccfef6ec16c08ff4d47651af24/Images/BodyCoreManage.png)<br/>
# Update a entry
![Update:](https://github.com/ElvisNilssonDev/BodyCoreApi/blob/631910cb421735ccfef6ec16c08ff4d47651af24/Images/BodyCoreUpdate.png)<br/>
# Delete an Entry
![Delete:](https://github.com/ElvisNilssonDev/BodyCoreApi/blob/631910cb421735ccfef6ec16c08ff4d47651af24/Images/BodyCoreDelete.png)<br/>

