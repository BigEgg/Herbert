namespace Herbert.API.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddDefaultValueForSupportApplication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "INSERT INTO SupportApplications (ApplicationType, AppId, AppSecret, CreatedTime, LastUpdated) " +
                "VALUES (1, '1b4f7394-b57e-49c7-b596-80988eaf7362', 'KbFHpy7SjvUPa9LdP9MLIBtC5BW3WnDj9YYGI0Fk35n4r35OmQJY1VWGajONG9Y6', '2016-07-17T09:30:00', '2016-07-17T09:30:00')"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "DELETE FROM SupportApplications " +
                "WHERE ApplicationType = 1"
            );
        }
    }
}
