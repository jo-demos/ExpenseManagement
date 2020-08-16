using System;
using System.IO;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpenseManagement.Migrations
{
    public partial class runimport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string script = File.ReadAllText("../../scripts/Meses.sql");
            migrationBuilder.Sql(script);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
