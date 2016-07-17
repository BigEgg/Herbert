namespace Herbert.API.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class CreateApplicationUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(maxLength: 256, nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    NickName = table.Column<string>(maxLength: 64, nullable: false),
                    Password = table.Column<string>(maxLength: 256, nullable: false),
                    RegisterSource = table.Column<int>(nullable: false),
                    Role = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUsers", x => x.Id);
                    table.UniqueConstraint("AK_ApplicationUsers_Email", x => x.Email);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsers_Email",
                table: "ApplicationUsers",
                column: "Email");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUsers");
        }
    }
}
