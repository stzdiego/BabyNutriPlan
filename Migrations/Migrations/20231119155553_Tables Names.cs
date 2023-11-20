using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrations.Migrations
{
    /// <inheritdoc />
    public partial class TablesNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CITIES_States_rowid_state",
                table: "CITIES");

            migrationBuilder.DropForeignKey(
                name: "FK_States_Countries_rowid_country",
                table: "States");

            migrationBuilder.DropPrimaryKey(
                name: "PK_States",
                table: "States");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Countries",
                table: "Countries");

            migrationBuilder.RenameTable(
                name: "States",
                newName: "STATES");

            migrationBuilder.RenameTable(
                name: "Countries",
                newName: "COUNTRIES");

            migrationBuilder.RenameIndex(
                name: "IX_States_rowid_country",
                table: "STATES",
                newName: "IX_STATES_rowid_country");

            migrationBuilder.AddPrimaryKey(
                name: "PK_STATES",
                table: "STATES",
                column: "rowid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_COUNTRIES",
                table: "COUNTRIES",
                column: "rowid");

            migrationBuilder.AddForeignKey(
                name: "FK_CITIES_STATES_rowid_state",
                table: "CITIES",
                column: "rowid_state",
                principalTable: "STATES",
                principalColumn: "rowid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_STATES_COUNTRIES_rowid_country",
                table: "STATES",
                column: "rowid_country",
                principalTable: "COUNTRIES",
                principalColumn: "rowid",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CITIES_STATES_rowid_state",
                table: "CITIES");

            migrationBuilder.DropForeignKey(
                name: "FK_STATES_COUNTRIES_rowid_country",
                table: "STATES");

            migrationBuilder.DropPrimaryKey(
                name: "PK_STATES",
                table: "STATES");

            migrationBuilder.DropPrimaryKey(
                name: "PK_COUNTRIES",
                table: "COUNTRIES");

            migrationBuilder.RenameTable(
                name: "STATES",
                newName: "States");

            migrationBuilder.RenameTable(
                name: "COUNTRIES",
                newName: "Countries");

            migrationBuilder.RenameIndex(
                name: "IX_STATES_rowid_country",
                table: "States",
                newName: "IX_States_rowid_country");

            migrationBuilder.AddPrimaryKey(
                name: "PK_States",
                table: "States",
                column: "rowid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Countries",
                table: "Countries",
                column: "rowid");

            migrationBuilder.AddForeignKey(
                name: "FK_CITIES_States_rowid_state",
                table: "CITIES",
                column: "rowid_state",
                principalTable: "States",
                principalColumn: "rowid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_States_Countries_rowid_country",
                table: "States",
                column: "rowid_country",
                principalTable: "Countries",
                principalColumn: "rowid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
