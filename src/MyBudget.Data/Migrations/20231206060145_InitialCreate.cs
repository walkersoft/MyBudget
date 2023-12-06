using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBudget.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExpenseCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Expense",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ExpenseType = table.Column<int>(type: "INTEGER", nullable: false),
                    Source = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", precision: 2, nullable: true),
                    EffectiveDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    ExpirationDate = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    ExpenseCategoryId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expense", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expense_ExpenseCategory_ExpenseCategoryId",
                        column: x => x.ExpenseCategoryId,
                        principalTable: "ExpenseCategory",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ExpenseEntry",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ExpenseId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreationDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", precision: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpenseEntry_Expense_ExpenseId",
                        column: x => x.ExpenseId,
                        principalTable: "Expense",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Expense_ExpenseCategoryId",
                table: "Expense",
                column: "ExpenseCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseEntry_ExpenseId",
                table: "ExpenseEntry",
                column: "ExpenseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpenseEntry");

            migrationBuilder.DropTable(
                name: "Expense");

            migrationBuilder.DropTable(
                name: "ExpenseCategory");
        }
    }
}
