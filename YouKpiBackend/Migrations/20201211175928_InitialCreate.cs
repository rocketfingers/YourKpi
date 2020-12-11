using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YouKpiBackend.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BusiessArea",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NIP = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Czesci",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Nazwa = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    GatPodstawowy = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    NumerRysNorma = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    Wymiary = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Czesci", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MozliweStatusyOferty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MozliweStatusyOferty", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MozliwyDN",
                columns: table => new
                {
                    DN = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MozliwyDN", x => x.DN);
                });

            migrationBuilder.CreateTable(
                name: "MozliwyOrderType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderType = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "MożliwaGrupaProcesu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "OfertaTyp_old",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfferId = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    Type = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "OfferLines_old",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfferID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ProductID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    W = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Medium = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    AdditionalEquipment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sale = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OfferId2 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferLines_old", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Pracownik",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    TrybPracy = table.Column<int>(type: "int", nullable: false),
                    CzasDostepuMin = table.Column<int>(type: "int", nullable: false),
                    IsAdministrator = table.Column<bool>(type: "bit", nullable: false),
                    Login = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "varchar(70)", unicode: false, maxLength: 70, nullable: false),
                    Email = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    Salt = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pracownik", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PracownikOfertaProcesy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PracownikId = table.Column<int>(type: "int", nullable: false),
                    OfferId = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ProcessID = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Process",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    NazwaGrupyProcesu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BusinessArea = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NazwaProcesu = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    KontoGłówne = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TypZlecenia = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false, defaultValueSql: "('R')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Process", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produkty",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    TypWyrobuId = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    NumerRysNorma = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DN = table.Column<int>(type: "int", nullable: false),
                    PN = table.Column<int>(type: "int", nullable: false),
                    ANSI = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Wersja = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Uszczelnienie = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Cena = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Waluta = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produkty", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produkty_old",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    TypWyrobuId = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    NumerRysNorma = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DN = table.Column<int>(type: "int", nullable: false),
                    PN = table.Column<int>(type: "int", nullable: false),
                    ANSI = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Wersja = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Uszczelnienie = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    WymiaryDLmm = table.Column<int>(type: "int", nullable: false),
                    WymianyRmm = table.Column<int>(type: "int", nullable: false),
                    WymiarHmm = table.Column<int>(type: "int", nullable: false),
                    Kvs = table.Column<int>(type: "int", nullable: false),
                    Waga = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WagaJednostka = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValueSql: "(N'kg')")
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ProductNrClient = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ProductNrInt = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    QafNr = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    To = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Phase = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    DateOfferd = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DateTD = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Nomination = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    RodzajProjektu = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Location = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Version = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Client = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    ClientGroup = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ProductType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ProductSubType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FunelStep = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Funel = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Steps",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    StepName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    StepNum = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ProcessesID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Sekwencja = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))"),
                    Rwnolege = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "TypWyrobu",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    IDGrupyWyrobu = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    TypKonstrukcji = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    TypPrzelotu = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    TypKuli = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false, defaultValueSql: "('pływajaca')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypWyrobu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypWyrobuIdDn",
                columns: table => new
                {
                    TypWyrobuId = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    WymiaryDLmm = table.Column<int>(type: "int", nullable: false),
                    WymianyRmm = table.Column<int>(type: "int", nullable: false),
                    WymiarHmm = table.Column<int>(type: "int", nullable: false),
                    Kvs = table.Column<int>(type: "int", nullable: false),
                    Waga = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WagaJednostka = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DN = table.Column<int>(type: "int", nullable: true),
                    TempMin = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((-60))"),
                    TempMax = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((120))")
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Offer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProjectsId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrderType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OfferDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ClientsId = table.Column<int>(type: "int", nullable: false),
                    Offerrer = table.Column<int>(type: "int", nullable: true),
                    OrderDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    PlannedEnd = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offer", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Offer__ClientsId__73852659",
                        column: x => x.ClientsId,
                        principalTable: "Client",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Offer__Offerrer__719CDDE7",
                        column: x => x.Offerrer,
                        principalTable: "Pracownik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PracownikProcess",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PracownikId = table.Column<int>(type: "int", nullable: false),
                    ProcessId = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_PracownikProcess_Pracownik",
                        column: x => x.PracownikId,
                        principalTable: "Pracownik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProduktCzesci",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProduktyId = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    CzesciId = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    IloscSztuk = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProduktCzesci", x => x.Id);
                    table.ForeignKey(
                        name: "ProduktCzesci_Czesci_FK",
                        column: x => x.CzesciId,
                        principalTable: "Czesci",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "ProduktCzesci_Produkty_FK",
                        column: x => x.ProduktyId,
                        principalTable: "Produkty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OfertaTyp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfferId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK__OfertaTyp__Offer__7A3223E8",
                        column: x => x.OfferId,
                        principalTable: "Offer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OfferLines",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfferId = table.Column<int>(type: "int", nullable: true),
                    ProductID = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    W = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Medium = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    AdditionalEquipment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sale = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferLines", x => x.ID);
                    table.ForeignKey(
                        name: "FK__OfferLine__Produ__70A8B9AE",
                        column: x => x.ProductID,
                        principalTable: "Produkty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OfferLines_Offer_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OfferOfferLinesConnection",
                columns: table => new
                {
                    OfferLineId = table.Column<int>(type: "int", nullable: false),
                    OfferID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK__OfferOffe__Offer__01D345B0",
                        column: x => x.OfferLineId,
                        principalTable: "OfferLines",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__OfferOffe__Offer__7E02B4CC",
                        column: x => x.OfferID,
                        principalTable: "Offer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OfertaTyp_OfferId",
                table: "OfertaTyp",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_Offer_ClientsId",
                table: "Offer",
                column: "ClientsId");

            migrationBuilder.CreateIndex(
                name: "IX_Offer_Offerrer",
                table: "Offer",
                column: "Offerrer");

            migrationBuilder.CreateIndex(
                name: "IX_OfferLines_OfferId",
                table: "OfferLines",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferLines_ProductID",
                table: "OfferLines",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_OfferLines",
                table: "OfferLines_old",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_OfferOfferLinesConnection_OfferID",
                table: "OfferOfferLinesConnection",
                column: "OfferID");

            migrationBuilder.CreateIndex(
                name: "IX_OfferOfferLinesConnection_OfferLineId",
                table: "OfferOfferLinesConnection",
                column: "OfferLineId");

            migrationBuilder.CreateIndex(
                name: "IX_PracownikProcess_PracownikId",
                table: "PracownikProcess",
                column: "PracownikId");

            migrationBuilder.CreateIndex(
                name: "IX_ProduktCzesci_CzesciId",
                table: "ProduktCzesci",
                column: "CzesciId");

            migrationBuilder.CreateIndex(
                name: "IX_ProduktCzesci_ProduktyId",
                table: "ProduktCzesci",
                column: "ProduktyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusiessArea");

            migrationBuilder.DropTable(
                name: "MozliweStatusyOferty");

            migrationBuilder.DropTable(
                name: "MozliwyDN");

            migrationBuilder.DropTable(
                name: "MozliwyOrderType");

            migrationBuilder.DropTable(
                name: "MożliwaGrupaProcesu");

            migrationBuilder.DropTable(
                name: "OfertaTyp");

            migrationBuilder.DropTable(
                name: "OfertaTyp_old");

            migrationBuilder.DropTable(
                name: "OfferLines_old");

            migrationBuilder.DropTable(
                name: "OfferOfferLinesConnection");

            migrationBuilder.DropTable(
                name: "PracownikOfertaProcesy");

            migrationBuilder.DropTable(
                name: "PracownikProcess");

            migrationBuilder.DropTable(
                name: "Process");

            migrationBuilder.DropTable(
                name: "ProduktCzesci");

            migrationBuilder.DropTable(
                name: "Produkty_old");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Steps");

            migrationBuilder.DropTable(
                name: "TypWyrobu");

            migrationBuilder.DropTable(
                name: "TypWyrobuIdDn");

            migrationBuilder.DropTable(
                name: "OfferLines");

            migrationBuilder.DropTable(
                name: "Czesci");

            migrationBuilder.DropTable(
                name: "Produkty");

            migrationBuilder.DropTable(
                name: "Offer");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Pracownik");
        }
    }
}
