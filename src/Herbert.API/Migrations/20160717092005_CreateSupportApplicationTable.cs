namespace Herbert.API.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class CreateSupportApplicationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SupportApplications",
                columns: table => new
                {
                    ApplicationType = table.Column<int>(nullable: false),
                    AppId = table.Column<Guid>(nullable: false),
                    AppSecret = table.Column<string>(maxLength: 64, nullable: false),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportApplications", x => x.ApplicationType);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SupportApplications_AppId_AppSecret",
                table: "SupportApplications",
                columns: new[] { "AppId", "AppSecret" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SupportApplications");
        }
    }
}
