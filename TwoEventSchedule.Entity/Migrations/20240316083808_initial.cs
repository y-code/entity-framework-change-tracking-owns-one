using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TwoEventSchedule.Entity.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Job",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    End_Executed = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    End_Scheduled = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Start_Executed = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    Start_Scheduled = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Job");
        }
    }
}
