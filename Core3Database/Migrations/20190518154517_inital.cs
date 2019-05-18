using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core3Database.Migrations
{
    public partial class inital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
               name: "Name",
               table: "tbl_Users",
               newName: "NameX");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NameX",
                table: "tbl_Users",
                newName: "Name");
        }
    }
}
