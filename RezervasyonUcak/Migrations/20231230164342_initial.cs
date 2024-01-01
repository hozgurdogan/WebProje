using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RezervasyonUcak.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Firma",
                columns: table => new
                {
                    FirmaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirmaAdi = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Firma", x => x.FirmaId);
                });

            migrationBuilder.CreateTable(
                name: "Tarih",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UcusTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarih", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ucak",
                columns: table => new
                {
                    UcakId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirmaId = table.Column<int>(type: "integer", nullable: false),
                    ModelNo = table.Column<string>(type: "text", nullable: false),
                    KoltukSayisi = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ucak", x => x.UcakId);
                    table.ForeignKey(
                        name: "FK_Ucak_Firma_FirmaId",
                        column: x => x.FirmaId,
                        principalTable: "Firma",
                        principalColumn: "FirmaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UcusKonum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BaslangicKonum = table.Column<string>(type: "text", nullable: false),
                    VarisKonum = table.Column<string>(type: "text", nullable: false),
                    TarihId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UcusKonum", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UcusKonum_Tarih_TarihId",
                        column: x => x.TarihId,
                        principalTable: "Tarih",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employee_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Koltuk",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    KoltukNo = table.Column<string>(type: "text", nullable: false),
                    DoluMu = table.Column<bool>(type: "boolean", nullable: false),
                    UcakId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Koltuk", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Koltuk_Ucak_UcakId",
                        column: x => x.UcakId,
                        principalTable: "Ucak",
                        principalColumn: "UcakId");
                });

            migrationBuilder.CreateTable(
                name: "UcusSefers",
                columns: table => new
                {
                    UcusId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UcusKonumId = table.Column<int>(type: "integer", nullable: false),
                    BaslangicSaat = table.Column<string>(type: "text", nullable: false),
                    VarisSaati = table.Column<string>(type: "text", nullable: false),
                    UcakId = table.Column<int>(type: "integer", nullable: false),
                    UcusFiyat = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UcusSefers", x => x.UcusId);
                    table.ForeignKey(
                        name: "FK_UcusSefers_Ucak_UcakId",
                        column: x => x.UcakId,
                        principalTable: "Ucak",
                        principalColumn: "UcakId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UcusSefers_UcusKonum_UcusKonumId",
                        column: x => x.UcusKonumId,
                        principalTable: "UcusKonum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bilets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UcusSeferUcusId = table.Column<int>(type: "integer", nullable: false),
                    MusteriId1 = table.Column<int>(type: "integer", nullable: false),
                    KesimTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    BiletFiyat = table.Column<double>(type: "double precision", nullable: false),
                    IptalMi = table.Column<bool>(type: "boolean", nullable: false),
                    KoltukId = table.Column<int>(type: "integer", nullable: false),
                    MusteriId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bilets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bilets_Employee_MusteriId",
                        column: x => x.MusteriId,
                        principalTable: "Employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bilets_Koltuk_KoltukId",
                        column: x => x.KoltukId,
                        principalTable: "Koltuk",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bilets_UcusSefers_UcusSeferUcusId",
                        column: x => x.UcusSeferUcusId,
                        principalTable: "UcusSefers",
                        principalColumn: "UcusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bilets_Users_MusteriId1",
                        column: x => x.MusteriId1,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bilets_KoltukId",
                table: "Bilets",
                column: "KoltukId");

            migrationBuilder.CreateIndex(
                name: "IX_Bilets_MusteriId",
                table: "Bilets",
                column: "MusteriId");

            migrationBuilder.CreateIndex(
                name: "IX_Bilets_MusteriId1",
                table: "Bilets",
                column: "MusteriId1");

            migrationBuilder.CreateIndex(
                name: "IX_Bilets_UcusSeferUcusId",
                table: "Bilets",
                column: "UcusSeferUcusId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_UserId",
                table: "Employee",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Koltuk_UcakId",
                table: "Koltuk",
                column: "UcakId");

            migrationBuilder.CreateIndex(
                name: "IX_Ucak_FirmaId",
                table: "Ucak",
                column: "FirmaId");

            migrationBuilder.CreateIndex(
                name: "IX_UcusKonum_TarihId",
                table: "UcusKonum",
                column: "TarihId");

            migrationBuilder.CreateIndex(
                name: "IX_UcusSefers_UcakId",
                table: "UcusSefers",
                column: "UcakId");

            migrationBuilder.CreateIndex(
                name: "IX_UcusSefers_UcusKonumId",
                table: "UcusSefers",
                column: "UcusKonumId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bilets");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Koltuk");

            migrationBuilder.DropTable(
                name: "UcusSefers");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Ucak");

            migrationBuilder.DropTable(
                name: "UcusKonum");

            migrationBuilder.DropTable(
                name: "Firma");

            migrationBuilder.DropTable(
                name: "Tarih");
        }
    }
}
