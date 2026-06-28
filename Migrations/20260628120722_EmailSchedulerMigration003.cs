using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmailScheduler.Migrations
{
    /// <inheritdoc />
    public partial class EmailSchedulerMigration003 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SmtpClientSettingsId",
                table: "EmailSchedulers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "SmtpClientSettingsSet",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Server = table.Column<string>(type: "text", nullable: false),
                    Port = table.Column<int>(type: "integer", nullable: false),
                    SenderName = table.Column<string>(type: "text", nullable: false),
                    SenderEmail = table.Column<string>(type: "text", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmtpClientSettingsSet", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmailSchedulers_SmtpClientSettingsId",
                table: "EmailSchedulers",
                column: "SmtpClientSettingsId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailSchedulers_SmtpClientSettingsSet_SmtpClientSettingsId",
                table: "EmailSchedulers",
                column: "SmtpClientSettingsId",
                principalTable: "SmtpClientSettingsSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailSchedulers_SmtpClientSettingsSet_SmtpClientSettingsId",
                table: "EmailSchedulers");

            migrationBuilder.DropTable(
                name: "SmtpClientSettingsSet");

            migrationBuilder.DropIndex(
                name: "IX_EmailSchedulers_SmtpClientSettingsId",
                table: "EmailSchedulers");

            migrationBuilder.DropColumn(
                name: "SmtpClientSettingsId",
                table: "EmailSchedulers");
        }
    }
}
