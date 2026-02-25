using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainingTrackerApi.Migrations
{
    /// <inheritdoc />
    public partial class IntialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TrainingWeeks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WeekStart = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingWeeks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainingDays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrainingWeekId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingDays_TrainingWeeks_TrainingWeekId",
                        column: x => x.TrainingWeekId,
                        principalTable: "TrainingWeeks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LiftEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Exercise = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WeightKg = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Reps = table.Column<int>(type: "int", nullable: false),
                    Sets = table.Column<int>(type: "int", nullable: false),
                    TrainingDayId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiftEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LiftEntries_TrainingDays_TrainingDayId",
                        column: x => x.TrainingDayId,
                        principalTable: "TrainingDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NutritionEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Calories = table.Column<int>(type: "int", nullable: false),
                    ProteinGrams = table.Column<int>(type: "int", nullable: true),
                    CarbsGrams = table.Column<int>(type: "int", nullable: true),
                    FatGrams = table.Column<int>(type: "int", nullable: true),
                    TrainingDayId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NutritionEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NutritionEntries_TrainingDays_TrainingDayId",
                        column: x => x.TrainingDayId,
                        principalTable: "TrainingDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LiftEntries_TrainingDayId",
                table: "LiftEntries",
                column: "TrainingDayId");

            migrationBuilder.CreateIndex(
                name: "IX_NutritionEntries_TrainingDayId",
                table: "NutritionEntries",
                column: "TrainingDayId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingDays_TrainingWeekId",
                table: "TrainingDays",
                column: "TrainingWeekId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LiftEntries");

            migrationBuilder.DropTable(
                name: "NutritionEntries");

            migrationBuilder.DropTable(
                name: "TrainingDays");

            migrationBuilder.DropTable(
                name: "TrainingWeeks");
        }
    }
}
