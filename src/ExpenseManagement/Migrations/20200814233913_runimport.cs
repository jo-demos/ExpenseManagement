﻿using System;
using System.IO;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpenseManagement.Migrations
{
    public partial class runimport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            const string script = @"INSERT INTO Mes VALUES (1, 'Janeiro');
                            INSERT INTO Mes VALUES (2, 'Fevereiro');
                            INSERT INTO Mes VALUES (3, 'Março');
                            INSERT INTO Mes VALUES (4, 'Abril');
                            INSERT INTO Mes VALUES (5, 'Maio');
                            INSERT INTO Mes VALUES (6, 'Junho');
                            INSERT INTO Mes VALUES (7, 'Julho');
                            INSERT INTO Mes VALUES (8, 'Agosto');
                            INSERT INTO Mes VALUES (9, 'Setembro');
                            INSERT INTO Mes VALUES (10, 'Outubro');
                            INSERT INTO Mes VALUES (11, 'Novembro');
                            INSERT INTO Mes VALUES (12, 'Dezembro');";
            migrationBuilder.Sql(script);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
