using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFirstApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Edges",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartNodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EndNodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EdgeCost = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Edges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Nodes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Lat = table.Column<double>(type: "float", nullable: false),
                    Lon = table.Column<double>(type: "float", nullable: false),
                    EdgeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Nodes_Edges_EdgeId",
                        column: x => x.EdgeId,
                        principalTable: "Edges",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Edges_EndNodeId",
                table: "Edges",
                column: "EndNodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Edges_StartNodeId",
                table: "Edges",
                column: "StartNodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Nodes_EdgeId",
                table: "Nodes",
                column: "EdgeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Edges_Nodes_EndNodeId",
                table: "Edges",
                column: "EndNodeId",
                principalTable: "Nodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Edges_Nodes_StartNodeId",
                table: "Edges",
                column: "StartNodeId",
                principalTable: "Nodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Edges_Nodes_EndNodeId",
                table: "Edges");

            migrationBuilder.DropForeignKey(
                name: "FK_Edges_Nodes_StartNodeId",
                table: "Edges");

            migrationBuilder.DropTable(
                name: "Nodes");

            migrationBuilder.DropTable(
                name: "Edges");
        }
    }
}
