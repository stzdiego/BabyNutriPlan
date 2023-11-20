using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Migrations.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ATTACHS",
                columns: table => new
                {
                    rowid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    path = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    type = table.Column<string>(type: "varchar(50)", nullable: false),
                    rowversion = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: true),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modification_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ATTACHS", x => x.rowid);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    rowid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "varchar(100)", nullable: false),
                    code = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.rowid);
                });

            migrationBuilder.CreateTable(
                name: "FOOD_GROUPS",
                columns: table => new
                {
                    rowid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    color = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    rowversion = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: true),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modification_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FOOD_GROUPS", x => x.rowid);
                });

            migrationBuilder.CreateTable(
                name: "ROLES",
                columns: table => new
                {
                    rowid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "varchar(100)", nullable: false),
                    description = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROLES", x => x.rowid);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    rowid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "varchar(100)", nullable: false),
                    code = table.Column<string>(type: "varchar(10)", nullable: false),
                    rowid_country = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.rowid);
                    table.ForeignKey(
                        name: "FK_States_Countries_rowid_country",
                        column: x => x.rowid_country,
                        principalTable: "Countries",
                        principalColumn: "rowid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FOODS",
                columns: table => new
                {
                    rowid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    is_allergeneric = table.Column<bool>(type: "bool", nullable: false),
                    rowid_food_group = table.Column<int>(type: "int", nullable: false),
                    rowversion = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: true),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modification_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FOODS", x => x.rowid);
                    table.ForeignKey(
                        name: "FK_FOODS_FOOD_GROUPS_rowid_food_group",
                        column: x => x.rowid_food_group,
                        principalTable: "FOOD_GROUPS",
                        principalColumn: "rowid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USERS",
                columns: table => new
                {
                    rowid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    password = table.Column<string>(type: "varchar(255)", maxLength: 24, nullable: true),
                    attempts = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "varchar(20)", nullable: false),
                    rowid_role = table.Column<int>(type: "int", nullable: false),
                    rowversion = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: true),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modification_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS", x => x.rowid);
                    table.ForeignKey(
                        name: "FK_USERS_ROLES_rowid_role",
                        column: x => x.rowid_role,
                        principalTable: "ROLES",
                        principalColumn: "rowid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CITIES",
                columns: table => new
                {
                    rowid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "varchar(100)", nullable: false),
                    code = table.Column<string>(type: "varchar(10)", nullable: false),
                    rowid_state = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CITIES", x => x.rowid);
                    table.ForeignKey(
                        name: "FK_CITIES_States_rowid_state",
                        column: x => x.rowid_state,
                        principalTable: "States",
                        principalColumn: "rowid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ATTENDANTS",
                columns: table => new
                {
                    rowid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    children = table.Column<short>(type: "smallint", nullable: false),
                    rowid_user = table.Column<int>(type: "integer", nullable: false),
                    rowversion = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: true),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modification_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    id_type = table.Column<string>(type: "varchar(20)", nullable: false),
                    id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    birth_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    tel = table.Column<string>(type: "varchar(50)", nullable: true),
                    cel = table.Column<string>(type: "varchar(50)", nullable: false),
                    occupation = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    address = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    country = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    state = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    city = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    neighborhood = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    gender = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ATTENDANTS", x => x.rowid);
                    table.ForeignKey(
                        name: "FK_ATTENDANTS_USERS_rowid_user",
                        column: x => x.rowid_user,
                        principalTable: "USERS",
                        principalColumn: "rowid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PEDIATRICIANS",
                columns: table => new
                {
                    rowid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    university = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    promotion_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    id_professional = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    rowid_user = table.Column<int>(type: "int", nullable: false),
                    rowid_attach_photo = table.Column<int>(type: "int", nullable: true),
                    rowversion = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: true),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modification_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    id_type = table.Column<string>(type: "varchar(20)", nullable: false),
                    id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    birth_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    tel = table.Column<string>(type: "varchar(50)", nullable: true),
                    cel = table.Column<string>(type: "varchar(50)", nullable: false),
                    occupation = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    address = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    country = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    state = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    city = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    neighborhood = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    gender = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PEDIATRICIANS", x => x.rowid);
                    table.ForeignKey(
                        name: "FK_PEDIATRICIANS_ATTACHS_rowid_attach_photo",
                        column: x => x.rowid_attach_photo,
                        principalTable: "ATTACHS",
                        principalColumn: "rowid");
                    table.ForeignKey(
                        name: "FK_PEDIATRICIANS_USERS_rowid_user",
                        column: x => x.rowid_user,
                        principalTable: "USERS",
                        principalColumn: "rowid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PATIENTS",
                columns: table => new
                {
                    rowid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    weight = table.Column<double>(type: "double precision", nullable: false),
                    height = table.Column<double>(type: "double precision", nullable: false),
                    rowid_pediatrician = table.Column<int>(type: "integer", nullable: false),
                    AttendantRowId = table.Column<int>(type: "integer", nullable: true),
                    rowversion = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: true),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modification_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    id_type = table.Column<string>(type: "varchar(20)", nullable: false),
                    id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    birth_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    tel = table.Column<string>(type: "varchar(50)", nullable: true),
                    cel = table.Column<string>(type: "varchar(50)", nullable: false),
                    occupation = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    address = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    country = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    state = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    city = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    neighborhood = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    gender = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PATIENTS", x => x.rowid);
                    table.ForeignKey(
                        name: "FK_PATIENTS_ATTENDANTS_AttendantRowId",
                        column: x => x.AttendantRowId,
                        principalTable: "ATTENDANTS",
                        principalColumn: "rowid");
                    table.ForeignKey(
                        name: "FK_PATIENTS_PEDIATRICIANS_rowid_pediatrician",
                        column: x => x.rowid_pediatrician,
                        principalTable: "PEDIATRICIANS",
                        principalColumn: "rowid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ATTENDANTS_PATIENTS",
                columns: table => new
                {
                    rowid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    attendant_type = table.Column<string>(type: "varchar(20)", nullable: false),
                    rowid_attendant = table.Column<int>(type: "integer", nullable: false),
                    rowid_patient = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ATTENDANTS_PATIENTS", x => x.rowid);
                    table.ForeignKey(
                        name: "FK_ATTENDANTS_PATIENTS_ATTENDANTS_rowid_attendant",
                        column: x => x.rowid_attendant,
                        principalTable: "ATTENDANTS",
                        principalColumn: "rowid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ATTENDANTS_PATIENTS_PATIENTS_rowid_patient",
                        column: x => x.rowid_patient,
                        principalTable: "PATIENTS",
                        principalColumn: "rowid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PLANS",
                columns: table => new
                {
                    rowid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    initial_day = table.Column<DateOnly>(type: "date", nullable: false),
                    rowid_patient = table.Column<int>(type: "int", nullable: false),
                    rowversion = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: true),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modification_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PLANS", x => x.rowid);
                    table.ForeignKey(
                        name: "FK_PLANS_PATIENTS_rowid_patient",
                        column: x => x.rowid_patient,
                        principalTable: "PATIENTS",
                        principalColumn: "rowid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PLAN_DAY",
                columns: table => new
                {
                    rowid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    day = table.Column<DateOnly>(type: "date", nullable: false),
                    rowid_food = table.Column<int>(type: "int", nullable: false),
                    rowid_plan = table.Column<int>(type: "int", nullable: false),
                    rowversion = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: true),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modification_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PLAN_DAY", x => x.rowid);
                    table.ForeignKey(
                        name: "FK_PLAN_DAY_FOODS_rowid_food",
                        column: x => x.rowid_food,
                        principalTable: "FOODS",
                        principalColumn: "rowid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PLAN_DAY_PLANS_rowid_plan",
                        column: x => x.rowid_plan,
                        principalTable: "PLANS",
                        principalColumn: "rowid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "INCIDENTS",
                columns: table => new
                {
                    rowid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    rowid_attendant = table.Column<int>(type: "int", nullable: false),
                    rowid_patient = table.Column<int>(type: "int", nullable: false),
                    rowid_plan_day = table.Column<int>(type: "int", nullable: false),
                    rowversion = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: true),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modification_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INCIDENTS", x => x.rowid);
                    table.ForeignKey(
                        name: "FK_INCIDENTS_ATTENDANTS_rowid_attendant",
                        column: x => x.rowid_attendant,
                        principalTable: "ATTENDANTS",
                        principalColumn: "rowid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_INCIDENTS_PATIENTS_rowid_patient",
                        column: x => x.rowid_patient,
                        principalTable: "PATIENTS",
                        principalColumn: "rowid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_INCIDENTS_PLAN_DAY_rowid_plan_day",
                        column: x => x.rowid_plan_day,
                        principalTable: "PLAN_DAY",
                        principalColumn: "rowid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ATTACHS_name",
                table: "ATTACHS",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "IX_ATTACHS_path",
                table: "ATTACHS",
                column: "path",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ATTENDANTS_id_id_type",
                table: "ATTENDANTS",
                columns: new[] { "id", "id_type" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ATTENDANTS_rowid_user",
                table: "ATTENDANTS",
                column: "rowid_user");

            migrationBuilder.CreateIndex(
                name: "IX_ATTENDANTS_PATIENTS_rowid_attendant_rowid_patient",
                table: "ATTENDANTS_PATIENTS",
                columns: new[] { "rowid_attendant", "rowid_patient" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ATTENDANTS_PATIENTS_rowid_patient",
                table: "ATTENDANTS_PATIENTS",
                column: "rowid_patient");

            migrationBuilder.CreateIndex(
                name: "IX_CITIES_code",
                table: "CITIES",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CITIES_rowid_state",
                table: "CITIES",
                column: "rowid_state");

            migrationBuilder.CreateIndex(
                name: "IX_FOODS_rowid_food_group",
                table: "FOODS",
                column: "rowid_food_group");

            migrationBuilder.CreateIndex(
                name: "IX_INCIDENTS_rowid_attendant",
                table: "INCIDENTS",
                column: "rowid_attendant");

            migrationBuilder.CreateIndex(
                name: "IX_INCIDENTS_rowid_patient",
                table: "INCIDENTS",
                column: "rowid_patient");

            migrationBuilder.CreateIndex(
                name: "IX_INCIDENTS_rowid_plan_day",
                table: "INCIDENTS",
                column: "rowid_plan_day");

            migrationBuilder.CreateIndex(
                name: "IX_PATIENTS_AttendantRowId",
                table: "PATIENTS",
                column: "AttendantRowId");

            migrationBuilder.CreateIndex(
                name: "IX_PATIENTS_id_id_type",
                table: "PATIENTS",
                columns: new[] { "id", "id_type" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PATIENTS_rowid_pediatrician",
                table: "PATIENTS",
                column: "rowid_pediatrician");

            migrationBuilder.CreateIndex(
                name: "IX_PEDIATRICIANS_id_id_type",
                table: "PEDIATRICIANS",
                columns: new[] { "id", "id_type" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PEDIATRICIANS_rowid_attach_photo",
                table: "PEDIATRICIANS",
                column: "rowid_attach_photo");

            migrationBuilder.CreateIndex(
                name: "IX_PEDIATRICIANS_rowid_user",
                table: "PEDIATRICIANS",
                column: "rowid_user");

            migrationBuilder.CreateIndex(
                name: "IX_PLAN_DAY_rowid_food",
                table: "PLAN_DAY",
                column: "rowid_food");

            migrationBuilder.CreateIndex(
                name: "IX_PLAN_DAY_rowid_plan",
                table: "PLAN_DAY",
                column: "rowid_plan");

            migrationBuilder.CreateIndex(
                name: "IX_PLANS_rowid_patient",
                table: "PLANS",
                column: "rowid_patient");

            migrationBuilder.CreateIndex(
                name: "IX_States_rowid_country",
                table: "States",
                column: "rowid_country");

            migrationBuilder.CreateIndex(
                name: "IX_USERS_email",
                table: "USERS",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_USERS_rowid_role",
                table: "USERS",
                column: "rowid_role");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ATTENDANTS_PATIENTS");

            migrationBuilder.DropTable(
                name: "CITIES");

            migrationBuilder.DropTable(
                name: "INCIDENTS");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "PLAN_DAY");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "FOODS");

            migrationBuilder.DropTable(
                name: "PLANS");

            migrationBuilder.DropTable(
                name: "FOOD_GROUPS");

            migrationBuilder.DropTable(
                name: "PATIENTS");

            migrationBuilder.DropTable(
                name: "ATTENDANTS");

            migrationBuilder.DropTable(
                name: "PEDIATRICIANS");

            migrationBuilder.DropTable(
                name: "ATTACHS");

            migrationBuilder.DropTable(
                name: "USERS");

            migrationBuilder.DropTable(
                name: "ROLES");
        }
    }
}
