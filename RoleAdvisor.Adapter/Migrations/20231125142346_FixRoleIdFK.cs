using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoleAdvisor.Adapter.Migrations
{
    /// <inheritdoc />
    public partial class FixRoleIdFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Roles_ProjectId",
                table: "Positions");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_RoleId",
                table: "Positions",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Roles_RoleId",
                table: "Positions",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Roles_RoleId",
                table: "Positions");

            migrationBuilder.DropIndex(
                name: "IX_Positions_RoleId",
                table: "Positions");

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Roles_ProjectId",
                table: "Positions",
                column: "ProjectId",
                principalTable: "Roles",
                principalColumn: "Id");
        }
    }
}
