using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoleAdvisor.Adapter.Migrations
{
    /// <inheritdoc />
    public partial class ConnectPositionsWithSkills : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PositionProjectId",
                table: "Skills",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PositionRoleId",
                table: "Skills",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PositionSkill",
                columns: table => new
                {
                    SkillId = table.Column<int>(type: "int", nullable: false),
                    PositionProjectId = table.Column<int>(type: "int", nullable: false),
                    PositionRoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PositionSkill", x => new { x.SkillId, x.PositionProjectId, x.PositionRoleId });
                    table.ForeignKey(
                        name: "FK_PositionSkill_Positions_PositionProjectId_PositionRoleId",
                        columns: x => new { x.PositionProjectId, x.PositionRoleId },
                        principalTable: "Positions",
                        principalColumns: new[] { "ProjectId", "RoleId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PositionSkill_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Skills_PositionProjectId_PositionRoleId",
                table: "Skills",
                columns: new[] { "PositionProjectId", "PositionRoleId" });

            migrationBuilder.CreateIndex(
                name: "IX_PositionSkill_PositionProjectId_PositionRoleId",
                table: "PositionSkill",
                columns: new[] { "PositionProjectId", "PositionRoleId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Positions_PositionProjectId_PositionRoleId",
                table: "Skills",
                columns: new[] { "PositionProjectId", "PositionRoleId" },
                principalTable: "Positions",
                principalColumns: new[] { "ProjectId", "RoleId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Positions_PositionProjectId_PositionRoleId",
                table: "Skills");

            migrationBuilder.DropTable(
                name: "PositionSkill");

            migrationBuilder.DropIndex(
                name: "IX_Skills_PositionProjectId_PositionRoleId",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "PositionProjectId",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "PositionRoleId",
                table: "Skills");
        }
    }
}
