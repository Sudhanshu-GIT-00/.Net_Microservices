using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mango.Services.AuthAPI.Migrations
{
    /// <inheritdoc />
    public partial class addSecurityQuestionsTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SecurityQuestions",
                columns: table => new
                {
                    SecurtyQstnId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SecurtyQstn = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityQuestions", x => x.SecurtyQstnId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SecurityQuestions");
        }
    }
}
