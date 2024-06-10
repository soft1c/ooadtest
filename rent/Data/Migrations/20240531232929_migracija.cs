using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rent.Data.Migrations
{
    /// <inheritdoc />
    public partial class migracija : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Korisnik",
                columns: table => new
                {
                    IdKorisnika = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipKorisnika = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnik", x => x.IdKorisnika);
                });

            migrationBuilder.CreateTable(
                name: "ProfilKorisnika",
                columns: table => new
                {
                    IdKorisnika = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrojTelefona = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfilKorisnika", x => x.IdKorisnika);
                });

            migrationBuilder.CreateTable(
                name: "Recenzija",
                columns: table => new
                {
                    IdRecenzije = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAutora = table.Column<long>(type: "bigint", nullable: false),
                    IdResursa = table.Column<long>(type: "bigint", nullable: false),
                    Ocjena = table.Column<int>(type: "int", nullable: false),
                    Komentar = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recenzija", x => x.IdRecenzije);
                });

            migrationBuilder.CreateTable(
                name: "Resurs",
                columns: table => new
                {
                    IdResursa = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdVlasnika = table.Column<long>(type: "bigint", nullable: false),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ocjena = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resurs", x => x.IdResursa);
                });

            migrationBuilder.CreateTable(
                name: "Rezervacija",
                columns: table => new
                {
                    IdRezervacije = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdOsobe = table.Column<long>(type: "bigint", nullable: false),
                    IdResursa = table.Column<long>(type: "bigint", nullable: false),
                    Pocetak = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Kraj = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezervacija", x => x.IdRezervacije);
                });

            migrationBuilder.CreateTable(
                name: "Nekretnina",
                columns: table => new
                {
                    IdResursa = table.Column<long>(type: "bigint", nullable: false),
                    Tip = table.Column<int>(type: "int", nullable: false),
                    Povrsina = table.Column<double>(type: "float", nullable: false),
                    BrojSoba = table.Column<int>(type: "int", nullable: false),
                    Parking = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nekretnina", x => x.IdResursa);
                    table.ForeignKey(
                        name: "FK_Nekretnina_Resurs_IdResursa",
                        column: x => x.IdResursa,
                        principalTable: "Resurs",
                        principalColumn: "IdResursa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vozilo",
                columns: table => new
                {
                    IdResursa = table.Column<long>(type: "bigint", nullable: false),
                    Tip = table.Column<int>(type: "int", nullable: false),
                    Marka = table.Column<int>(type: "int", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Godiste = table.Column<int>(type: "int", nullable: false),
                    BrojSjedista = table.Column<int>(type: "int", nullable: false),
                    TipGoriva = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vozilo", x => x.IdResursa);
                    table.ForeignKey(
                        name: "FK_Vozilo_Resurs_IdResursa",
                        column: x => x.IdResursa,
                        principalTable: "Resurs",
                        principalColumn: "IdResursa",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Korisnik");

            migrationBuilder.DropTable(
                name: "Nekretnina");

            migrationBuilder.DropTable(
                name: "ProfilKorisnika");

            migrationBuilder.DropTable(
                name: "Recenzija");

            migrationBuilder.DropTable(
                name: "Rezervacija");

            migrationBuilder.DropTable(
                name: "Vozilo");

            migrationBuilder.DropTable(
                name: "Resurs");
        }
    }
}
