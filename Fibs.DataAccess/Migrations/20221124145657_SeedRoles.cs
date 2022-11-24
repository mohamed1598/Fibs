using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fibs.Data.Migrations
{
    public partial class SeedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table:"Roles",
                columns: new[] {"Id","Name","NormalizedName","ConcurrencyStamp"},
                values: new object[] { Guid.NewGuid().ToString(),"User","User".ToUpper(),Guid.NewGuid().ToString()},
                schema:"Security"
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from [Security].[Roles]");
        }
    }
}
