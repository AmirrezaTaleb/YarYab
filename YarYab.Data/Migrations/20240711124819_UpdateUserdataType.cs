using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YarYab.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserdataType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TelegramId",
                table: "Users",
                newName: "ChanelId");

            migrationBuilder.AlterColumn<int>(
                name: "Gender",
                table: "Users",
                type: "integer",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(char),
                oldType: "character(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<short>(
                name: "Age",
                table: "Users",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ChanelId",
                table: "Users",
                newName: "TelegramId");

            migrationBuilder.AlterColumn<char>(
                name: "Gender",
                table: "Users",
                type: "character(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<int>(
                name: "Age",
                table: "Users",
                type: "integer",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");
        }
    }
}
