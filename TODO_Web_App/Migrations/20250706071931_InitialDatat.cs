using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TODO_Web_App.Migrations
{
    public partial class InitialDatat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    categoryID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    categoryName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.categoryID);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    statusID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    statusName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.statusID);
                });

            migrationBuilder.CreateTable(
                name: "ToDOs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    categoryID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    statusID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDOs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ToDOs_Categories_categoryID",
                        column: x => x.categoryID,
                        principalTable: "Categories",
                        principalColumn: "categoryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ToDOs_Statuses_statusID",
                        column: x => x.statusID,
                        principalTable: "Statuses",
                        principalColumn: "statusID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "categoryID", "categoryName" },
                values: new object[,]
                {
                    { "work", "Work" },
                    { "home", "Home" },
                    { "shop", "Shopping" },
                    { "call", "Contact" },
                    { "ex", "Exercise" }
                });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "statusID", "statusName" },
                values: new object[,]
                {
                    { "open", "Open" },
                    { "closed", "Closed" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ToDOs_categoryID",
                table: "ToDOs",
                column: "categoryID");

            migrationBuilder.CreateIndex(
                name: "IX_ToDOs_statusID",
                table: "ToDOs",
                column: "statusID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ToDOs");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Statuses");
        }
    }
}
