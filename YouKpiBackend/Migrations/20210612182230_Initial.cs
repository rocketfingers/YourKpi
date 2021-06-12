using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YouKpiBackend.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK__Offer__ClientsId__73852659",
            //    table: "Offer");

            //migrationBuilder.DropForeignKey(
            //    name: "FK__Offer__Offerrer__719CDDE7",
            //    table: "Offer");

            //migrationBuilder.DropForeignKey(
            //    name: "FK__OfferLine__Produ__70A8B9AE",
            //    table: "OfferLines");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_OfferLines_Offer_OfferId",
            //    table: "OfferLines");

            //migrationBuilder.DropForeignKey(
            //    name: "ProduktCzesci_Produkty_FK",
            //    table: "ProduktCzesci");

            //migrationBuilder.DropTable(
            //    name: "OfertaTyp");

            //migrationBuilder.DropTable(
            //    name: "OfertaTyp_old");

            //migrationBuilder.DropTable(
            //    name: "OfferLines_old");

            //migrationBuilder.DropTable(
            //    name: "PracownikOfertaProcesy");

            //migrationBuilder.DropTable(
            //    name: "Produkty_old");

            //migrationBuilder.DropIndex(
            //    name: "IX_ProduktCzesci_ProduktyId",
            //    table: "ProduktCzesci");

            //migrationBuilder.DropColumn(
            //    name: "Rwnolege",
            //    table: "Steps");

            //migrationBuilder.DropColumn(
            //    name: "ProduktyId",
            //    table: "ProduktCzesci");

            //migrationBuilder.RenameColumn(
            //    name: "ProcessesID",
            //    table: "Steps",
            //    newName: "ProcessesId");

            //migrationBuilder.RenameColumn(
            //    name: "ID",
            //    table: "Steps",
            //    newName: "Id");

            //migrationBuilder.RenameColumn(
            //    name: "Id",
            //    table: "Produkty",
            //    newName: "ID");

            //migrationBuilder.RenameColumn(
            //    name: "ProductID",
            //    table: "OfferLines",
            //    newName: "ProductId");

            //migrationBuilder.RenameColumn(
            //    name: "ID",
            //    table: "OfferLines",
            //    newName: "Id");

            //migrationBuilder.RenameIndex(
            //    name: "IX_OfferLines_ProductID",
            //    table: "OfferLines",
            //    newName: "IX_OfferLines_ProductId");

            //migrationBuilder.AlterColumn<int>(
            //    name: "StepNum",
            //    table: "Steps",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(200)",
            //    oldMaxLength: 200);

            //migrationBuilder.AlterColumn<int>(
            //    name: "Sekwencja",
            //    table: "Steps",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int",
            //    oldDefaultValueSql: "((1))");

            //migrationBuilder.AlterColumn<string>(
            //    name: "ProcessesId",
            //    table: "Steps",
            //    type: "nvarchar(30)",
            //    maxLength: 30,
            //    nullable: true,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(50)",
            //    oldMaxLength: 50);

            //migrationBuilder.AlterColumn<int>(
            //    name: "Id",
            //    table: "Steps",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .Annotation("SqlServer:Identity", "1, 1");

            //migrationBuilder.AddColumn<string>(
            //    name: "StepId",
            //    table: "Steps",
            //    type: "nvarchar(61)",
            //    maxLength: 61,
            //    nullable: true);

            //migrationBuilder.AlterColumn<int>(
            //    name: "ID",
            //    table: "Produkty",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldType: "varchar(30)",
            //    oldUnicode: false,
            //    oldMaxLength: 30)
            //    .Annotation("SqlServer:Identity", "1, 1");

            //migrationBuilder.AddColumn<string>(
            //    name: "ProductName",
            //    table: "Produkty",
            //    type: "varchar(100)",
            //    unicode: false,
            //    maxLength: 100,
            //    nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "ProduktId",
            //    table: "ProduktCzesci",
            //    type: "int",
            //    nullable: true);

            //migrationBuilder.AlterColumn<string>(
            //    name: "Email",
            //    table: "Pracownik",
            //    type: "nvarchar(50)",
            //    maxLength: 50,
            //    nullable: true,
            //    oldClrType: typeof(string),
            //    oldType: "nchar(10)",
            //    oldFixedLength: true,
            //    oldMaxLength: 10,
            //    oldNullable: true);

            //migrationBuilder.AlterColumn<int>(
            //    name: "Id",
            //    table: "Pracownik",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .Annotation("SqlServer:Identity", "1, 1");

            //migrationBuilder.AddColumn<int>(
            //    name: "Rola",
            //    table: "Pracownik",
            //    type: "int",
            //    nullable: false,
            //    defaultValueSql: "((1))");

            //migrationBuilder.AddColumn<decimal>(
            //    name: "StawkaGodzinowa",
            //    table: "Pracownik",
            //    type: "decimal(18,2)",
            //    nullable: true);

            //migrationBuilder.AddColumn<decimal>(
            //    name: "StawkaMiesieczna",
            //    table: "Pracownik",
            //    type: "decimal(18,2)",
            //    nullable: true);

            //migrationBuilder.AlterColumn<int>(
            //    name: "ProductId",
            //    table: "OfferLines",
            //    type: "int",
            //    nullable: true,
            //    oldClrType: typeof(string),
            //    oldType: "varchar(30)",
            //    oldUnicode: false,
            //    oldMaxLength: 30);

            //migrationBuilder.AddColumn<decimal>(
            //    name: "PriceInOfferDay",
            //    table: "OfferLines",
            //    type: "decimal(18,2)",
            //    nullable: true);

            //migrationBuilder.AddColumn<decimal>(
            //    name: "SalesPrice",
            //    table: "OfferLines",
            //    type: "decimal(18,2)",
            //    nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "CzasProgramisty",
            //    table: "Czesci",
            //    type: "int",
            //    nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "KomponentId",
            //    table: "Czesci",
            //    type: "int",
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "NumerProgramu",
            //    table: "Czesci",
            //    type: "varchar(10)",
            //    unicode: false,
            //    maxLength: 10,
            //    nullable: true);

            //migrationBuilder.AddColumn<decimal>(
            //    name: "TJ",
            //    table: "Czesci",
            //    type: "decimal(6,2)",
            //    nullable: true);

            //migrationBuilder.AddColumn<decimal>(
            //    name: "TPZ",
            //    table: "Czesci",
            //    type: "decimal(6,2)",
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "Wykonanie",
            //    table: "Czesci",
            //    type: "varchar(500)",
            //    unicode: false,
            //    maxLength: 500,
            //    nullable: true);

            //migrationBuilder.AlterColumn<string>(
            //    name: "NIP",
            //    table: "Client",
            //    type: "char(20)",
            //    unicode: false,
            //    fixedLength: true,
            //    maxLength: 20,
            //    nullable: true,
            //    oldClrType: typeof(string),
            //    oldType: "char(10)",
            //    oldUnicode: false,
            //    oldFixedLength: true,
            //    oldMaxLength: 10);

            //migrationBuilder.AddColumn<string>(
            //    name: "Email",
            //    table: "Client",
            //    type: "varchar(100)",
            //    unicode: false,
            //    maxLength: 100,
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "KodPocztowy",
            //    table: "Client",
            //    type: "varchar(10)",
            //    unicode: false,
            //    maxLength: 10,
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "Miasto",
            //    table: "Client",
            //    type: "varchar(100)",
            //    unicode: false,
            //    maxLength: 100,
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "NrBudynku",
            //    table: "Client",
            //    type: "varchar(15)",
            //    unicode: false,
            //    maxLength: 15,
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "NrTelefonu",
            //    table: "Client",
            //    type: "varchar(15)",
            //    unicode: false,
            //    maxLength: 15,
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "OsobaKontaktowa",
            //    table: "Client",
            //    type: "varchar(500)",
            //    unicode: false,
            //    maxLength: 500,
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "Ulica",
            //    table: "Client",
            //    type: "varchar(100)",
            //    unicode: false,
            //    maxLength: 100,
            //    nullable: true);

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_TypWyrobuIdDn",
            //    table: "TypWyrobuIdDn",
            //    column: "TypWyrobuId");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_Steps",
            //    table: "Steps",
            //    column: "Id");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_PracownikProcess",
            //    table: "PracownikProcess",
            //    column: "Id");

            //migrationBuilder.CreateTable(
            //    name: "CompanyBasicInfo",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Nazwa = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
            //        FormaPrawna = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
            //        KodPocztowy = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
            //        Miasto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
            //        Ulica = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
            //        Numer = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
            //        NIP = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_CompanyBasicInfo", x => x.ID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Komponenty",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        KomponentId = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
            //        Nazwa = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: true),
            //        Wymiar = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: true),
            //        GatunekPodst = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: true),
            //        Ilosc = table.Column<decimal>(type: "decimal(11,6)", nullable: true),
            //        Jednostka = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
            //        CenaJednostkowa = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
            //        ProcessId = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
            //        LTID = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Komponenty", x => x.ID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Kontrahent",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        NIP = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false),
            //        Name = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
            //        KodPocztowy = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
            //        Miasto = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
            //        Ulica = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
            //        NrBudynku = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
            //        NrTelefonu = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
            //        Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Kontrahent", x => x.ID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Lokacja",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Nazwa = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Lokacja", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Maszyny",
            //    columns: table => new
            //    {
            //        Id = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
            //        Nazwa = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
            //        Rodzaj = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
            //        Opis = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
            //        TrybPracy = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
            //        MiejscePracy = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
            //        Krytycznosc = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
            //        Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
            //        DataUruchomienia = table.Column<DateTime>(type: "datetime", nullable: true),
            //        WartoscMaszyny = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
            //        StawkaMiesieczna = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
            //        OsX = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
            //        OsY = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
            //        OsZ = table.Column<decimal>(type: "decimal(18,0)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Maszyny", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "OfferLineProcess",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        OfferLineId = table.Column<int>(type: "int", nullable: false),
            //        ProcessId = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_OfferLineProcess", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK__OfferLine__Offer__34E8D562",
            //            column: x => x.OfferLineId,
            //            principalTable: "OfferLines",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK__OfferLine__Proce__30242045",
            //            column: x => x.ProcessId,
            //            principalTable: "Process",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "OfferProcess",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        OfferId = table.Column<int>(type: "int", nullable: false),
            //        ProcessId = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
            //        CzasWymagany = table.Column<double>(type: "float", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_OfferProcess", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK__OfferProc__Offer__38B96646",
            //            column: x => x.OfferId,
            //            principalTable: "Offer",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK__OfferProc__Proce__39AD8A7F",
            //            column: x => x.ProcessId,
            //            principalTable: "Process",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ProcessesProcess",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        ProcessId = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
            //        RelatedProcessId = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ProcessesProcess", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_ProcessesProcess",
            //            column: x => x.ProcessId,
            //            principalTable: "Process",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_ProcessesRelatedProcess",
            //            column: x => x.RelatedProcessId,
            //            principalTable: "Process",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ReasonCodes",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        IdDifferenceReasonCode = table.Column<int>(type: "int", nullable: true),
            //        Opis = table.Column<string>(type: "varchar(1000)", unicode: false, maxLength: 1000, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ReasonCodes", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Towary",
            //    columns: table => new
            //    {
            //        ID = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
            //        Nazwa = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
            //        ClientId = table.Column<int>(type: "int", nullable: true),
            //        WalutaZakupu = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
            //        WalutaSprzedazy = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
            //        CenaSprzedazy = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
            //        CenaZakupu = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Towary", x => x.ID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "MagazynCzesci",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        ElementId = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
            //        KontrahentId = table.Column<int>(type: "int", nullable: true),
            //        DataPrzyjecia = table.Column<DateTime>(type: "datetime", nullable: true),
            //        LokacjaId = table.Column<int>(type: "int", nullable: true),
            //        NrFakturyId = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
            //        Ilosc = table.Column<int>(type: "int", nullable: true),
            //        Jednostka = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
            //        CenaJdnNetto = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_MagazynCzesci", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Czesci",
            //            column: x => x.ElementId,
            //            principalTable: "Czesci",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_Kontrahent",
            //            column: x => x.KontrahentId,
            //            principalTable: "Kontrahent",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_Lokacje",
            //            column: x => x.LokacjaId,
            //            principalTable: "Lokacja",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "MagazynKomponenty",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        ElementId = table.Column<int>(type: "int", nullable: true),
            //        KontrahentId = table.Column<int>(type: "int", nullable: true),
            //        DataPrzyjecia = table.Column<DateTime>(type: "datetime", nullable: true),
            //        LokacjaId = table.Column<int>(type: "int", nullable: true),
            //        NrFakturyId = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
            //        Ilosc = table.Column<int>(type: "int", nullable: true),
            //        Jednostka = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
            //        CenaJdnNetto = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_MagazynKomponenty", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Komponenty",
            //            column: x => x.ElementId,
            //            principalTable: "Komponenty",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_KontrahentKomponenty",
            //            column: x => x.KontrahentId,
            //            principalTable: "Kontrahent",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_LokacjePKomponenty",
            //            column: x => x.LokacjaId,
            //            principalTable: "Lokacja",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "MagazynProdukty",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        KontrahentId = table.Column<int>(type: "int", nullable: true),
            //        DataPrzyjecia = table.Column<DateTime>(type: "datetime", nullable: true),
            //        LokacjaId = table.Column<int>(type: "int", nullable: true),
            //        NrFakturyId = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
            //        Ilosc = table.Column<int>(type: "int", nullable: true),
            //        Jednostka = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
            //        CenaJdnNetto = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
            //        ElementId = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_MagazynProdukty", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_KontrahentProdukty",
            //            column: x => x.KontrahentId,
            //            principalTable: "Kontrahent",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_LokacjeProdukty",
            //            column: x => x.LokacjaId,
            //            principalTable: "Lokacja",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_Produkty",
            //            column: x => x.ElementId,
            //            principalTable: "Produkty",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "MagazynProduktyNiezgodne",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        ElementId = table.Column<int>(type: "int", nullable: true),
            //        KontrahentId = table.Column<int>(type: "int", nullable: true),
            //        DataPrzyjecia = table.Column<DateTime>(type: "datetime", nullable: true),
            //        LokacjaId = table.Column<int>(type: "int", nullable: true),
            //        NrFakturyId = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
            //        Ilosc = table.Column<int>(type: "int", nullable: true),
            //        Jednostka = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
            //        CenaJdnNetto = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_MagazynProduktyNiezgodne", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_KontrahentProduktyNiezgodne",
            //            column: x => x.KontrahentId,
            //            principalTable: "Kontrahent",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_LokacjeProduktyNiezgodne",
            //            column: x => x.LokacjaId,
            //            principalTable: "Lokacja",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_ProduktyNiezgodne",
            //            column: x => x.ElementId,
            //            principalTable: "Produkty",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "PracownikCzasStep",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        OfferLinesID = table.Column<int>(type: "int", nullable: false),
            //        ProcessId = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
            //        PracownikId = table.Column<int>(type: "int", nullable: false),
            //        LiczbaMinut = table.Column<int>(type: "int", nullable: false),
            //        CzasStart = table.Column<DateTime>(type: "datetime", nullable: true),
            //        CzasStop = table.Column<DateTime>(type: "datetime", nullable: true),
            //        Step = table.Column<int>(type: "int", nullable: false),
            //        CzescId = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
            //        CześćIlosc = table.Column<int>(type: "int", nullable: true),
            //        MaszynyId = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_PracownikCzasStep", x => x.ID);
            //        table.ForeignKey(
            //            name: "FK_PracownikCzasStep_Maszyny",
            //            column: x => x.MaszynyId,
            //            principalTable: "Maszyny",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_PracownikCzasStep_OfferLines",
            //            column: x => x.OfferLinesID,
            //            principalTable: "OfferLines",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_PracownikCzasStep_Pracownik",
            //            column: x => x.PracownikId,
            //            principalTable: "Pracownik",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_PracownikCzasStep_PracownikCzasStep2",
            //            column: x => x.CzescId,
            //            principalTable: "Czesci",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_PracownikCzasStep_Process",
            //            column: x => x.ProcessId,
            //            principalTable: "Process",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "StepsMachines",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        StepId = table.Column<int>(type: "int", nullable: true),
            //        MachineId = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_StepsMachines", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK__StepsMach__Machi__2A6B46EF",
            //            column: x => x.MachineId,
            //            principalTable: "Maszyny",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK__StepsMach__StepI__451F3D2B",
            //            column: x => x.StepId,
            //            principalTable: "Steps",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "StepOfferWykonanie",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        OfferLineID = table.Column<int>(type: "int", nullable: false),
            //        ProcessID = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
            //        LiczbaPomiarow = table.Column<int>(type: "int", nullable: false),
            //        LiczbaPomiarowNok = table.Column<int>(type: "int", nullable: false),
            //        Zakonczonie = table.Column<bool>(type: "bit", nullable: false),
            //        Step = table.Column<int>(type: "int", nullable: false),
            //        ClosesBy = table.Column<int>(type: "int", nullable: true),
            //        ClosedOn = table.Column<DateTime>(type: "datetime", nullable: true),
            //        StartedOn = table.Column<DateTime>(type: "datetime", nullable: true),
            //        ReasonCodeID = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_StepOfferWykonanie", x => x.ID);
            //        table.ForeignKey(
            //            name: "FK_StepOfferWykonanie_ReasonCodes",
            //            column: x => x.ReasonCodeID,
            //            principalTable: "ReasonCodes",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "MagazynTowary",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Nazwa = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
            //        Ilosc = table.Column<int>(type: "int", nullable: true),
            //        DataPrzyjecia = table.Column<DateTime>(type: "datetime", nullable: true),
            //        KontrahentId = table.Column<int>(type: "int", nullable: true),
            //        LokacjaId = table.Column<int>(type: "int", nullable: true),
            //        ElementId = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
            //        CenaJdnNetto = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
            //        NrFakturyId = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
            //        Jednostka = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_MagazynTowary", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK__MagazynTo__Eleme__23BE4960",
            //            column: x => x.ElementId,
            //            principalTable: "Towary",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK__MagazynTo__Kontr__24B26D99",
            //            column: x => x.KontrahentId,
            //            principalTable: "Kontrahent",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK__MagazynTo__Lokac__25A691D2",
            //            column: x => x.LokacjaId,
            //            principalTable: "Lokacja",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Steps_ProcessesId",
            //    table: "Steps",
            //    column: "ProcessesId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ProduktCzesci_ProduktId",
            //    table: "ProduktCzesci",
            //    column: "ProduktId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Czesci_KomponentId",
            //    table: "Czesci",
            //    column: "KomponentId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MagazynCzesci_ElementId",
            //    table: "MagazynCzesci",
            //    column: "ElementId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MagazynCzesci_KontrahentId",
            //    table: "MagazynCzesci",
            //    column: "KontrahentId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MagazynCzesci_LokacjaId",
            //    table: "MagazynCzesci",
            //    column: "LokacjaId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MagazynKomponenty_ElementId",
            //    table: "MagazynKomponenty",
            //    column: "ElementId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MagazynKomponenty_KontrahentId",
            //    table: "MagazynKomponenty",
            //    column: "KontrahentId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MagazynKomponenty_LokacjaId",
            //    table: "MagazynKomponenty",
            //    column: "LokacjaId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MagazynProdukty_ElementId",
            //    table: "MagazynProdukty",
            //    column: "ElementId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MagazynProdukty_KontrahentId",
            //    table: "MagazynProdukty",
            //    column: "KontrahentId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MagazynProdukty_LokacjaId",
            //    table: "MagazynProdukty",
            //    column: "LokacjaId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MagazynProduktyNiezgodne_ElementId",
            //    table: "MagazynProduktyNiezgodne",
            //    column: "ElementId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MagazynProduktyNiezgodne_KontrahentId",
            //    table: "MagazynProduktyNiezgodne",
            //    column: "KontrahentId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MagazynProduktyNiezgodne_LokacjaId",
            //    table: "MagazynProduktyNiezgodne",
            //    column: "LokacjaId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MagazynTowary_ElementId",
            //    table: "MagazynTowary",
            //    column: "ElementId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MagazynTowary_KontrahentId",
            //    table: "MagazynTowary",
            //    column: "KontrahentId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MagazynTowary_LokacjaId",
            //    table: "MagazynTowary",
            //    column: "LokacjaId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_OfferLineProcess_OfferLineId",
            //    table: "OfferLineProcess",
            //    column: "OfferLineId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_OfferLineProcess_ProcessId",
            //    table: "OfferLineProcess",
            //    column: "ProcessId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_OfferProcess_OfferId",
            //    table: "OfferProcess",
            //    column: "OfferId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_OfferProcess_ProcessId",
            //    table: "OfferProcess",
            //    column: "ProcessId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_PracownikCzasStep_CzescId",
            //    table: "PracownikCzasStep",
            //    column: "CzescId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_PracownikCzasStep_MaszynyId",
            //    table: "PracownikCzasStep",
            //    column: "MaszynyId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_PracownikCzasStep_OfferLinesID",
            //    table: "PracownikCzasStep",
            //    column: "OfferLinesID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_PracownikCzasStep_PracownikId",
            //    table: "PracownikCzasStep",
            //    column: "PracownikId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_PracownikCzasStep_ProcessId",
            //    table: "PracownikCzasStep",
            //    column: "ProcessId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ProcessesProcess_ProcessId",
            //    table: "ProcessesProcess",
            //    column: "ProcessId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ProcessesProcess_RelatedProcessId",
            //    table: "ProcessesProcess",
            //    column: "RelatedProcessId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_StepOfferWykonanie_ReasonCodeID",
            //    table: "StepOfferWykonanie",
            //    column: "ReasonCodeID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_StepsMachines_MachineId",
            //    table: "StepsMachines",
            //    column: "MachineId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_StepsMachines_StepId",
            //    table: "StepsMachines",
            //    column: "StepId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK__Czesci__Komponen__79C80F94",
            //    table: "Czesci",
            //    column: "KomponentId",
            //    principalTable: "Komponenty",
            //    principalColumn: "ID",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK__Offer__ClientsId__740F363E",
            //    table: "Offer",
            //    column: "ClientsId",
            //    principalTable: "Client",
            //    principalColumn: "ID",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK__Offer__Offerrer__3C69FB99",
            //    table: "Offer",
            //    column: "Offerrer",
            //    principalTable: "Pracownik",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK__OfferLine__Offer__33008CF0",
            //    table: "OfferLines",
            //    column: "OfferId",
            //    principalTable: "Offer",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK__OfferLine__Produ__33F4B129",
            //    table: "OfferLines",
            //    column: "ProductId",
            //    principalTable: "Produkty",
            //    principalColumn: "ID",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "ProduktCzesci_Produkty_FK",
            //    table: "ProduktCzesci",
            //    column: "ProduktId",
            //    principalTable: "Produkty",
            //    principalColumn: "ID",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_PROCESS_STEP_ID",
            //    table: "Steps",
            //    column: "ProcessesId",
            //    principalTable: "Process",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Czesci__Komponen__79C80F94",
                table: "Czesci");

            migrationBuilder.DropForeignKey(
                name: "FK__Offer__ClientsId__740F363E",
                table: "Offer");

            migrationBuilder.DropForeignKey(
                name: "FK__Offer__Offerrer__3C69FB99",
                table: "Offer");

            migrationBuilder.DropForeignKey(
                name: "FK__OfferLine__Offer__33008CF0",
                table: "OfferLines");

            migrationBuilder.DropForeignKey(
                name: "FK__OfferLine__Produ__33F4B129",
                table: "OfferLines");

            migrationBuilder.DropForeignKey(
                name: "ProduktCzesci_Produkty_FK",
                table: "ProduktCzesci");

            migrationBuilder.DropForeignKey(
                name: "FK_PROCESS_STEP_ID",
                table: "Steps");

            migrationBuilder.DropTable(
                name: "CompanyBasicInfo");

            migrationBuilder.DropTable(
                name: "MagazynCzesci");

            migrationBuilder.DropTable(
                name: "MagazynKomponenty");

            migrationBuilder.DropTable(
                name: "MagazynProdukty");

            migrationBuilder.DropTable(
                name: "MagazynProduktyNiezgodne");

            migrationBuilder.DropTable(
                name: "MagazynTowary");

            migrationBuilder.DropTable(
                name: "OfferLineProcess");

            migrationBuilder.DropTable(
                name: "OfferProcess");

            migrationBuilder.DropTable(
                name: "PracownikCzasStep");

            migrationBuilder.DropTable(
                name: "ProcessesProcess");

            migrationBuilder.DropTable(
                name: "StepOfferWykonanie");

            migrationBuilder.DropTable(
                name: "StepsMachines");

            migrationBuilder.DropTable(
                name: "Komponenty");

            migrationBuilder.DropTable(
                name: "Towary");

            migrationBuilder.DropTable(
                name: "Kontrahent");

            migrationBuilder.DropTable(
                name: "Lokacja");

            migrationBuilder.DropTable(
                name: "ReasonCodes");

            migrationBuilder.DropTable(
                name: "Maszyny");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TypWyrobuIdDn",
                table: "TypWyrobuIdDn");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Steps",
                table: "Steps");

            migrationBuilder.DropIndex(
                name: "IX_Steps_ProcessesId",
                table: "Steps");

            migrationBuilder.DropIndex(
                name: "IX_ProduktCzesci_ProduktId",
                table: "ProduktCzesci");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PracownikProcess",
                table: "PracownikProcess");

            migrationBuilder.DropIndex(
                name: "IX_Czesci_KomponentId",
                table: "Czesci");

            migrationBuilder.DropColumn(
                name: "StepId",
                table: "Steps");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "Produkty");

            migrationBuilder.DropColumn(
                name: "ProduktId",
                table: "ProduktCzesci");

            migrationBuilder.DropColumn(
                name: "Rola",
                table: "Pracownik");

            migrationBuilder.DropColumn(
                name: "StawkaGodzinowa",
                table: "Pracownik");

            migrationBuilder.DropColumn(
                name: "StawkaMiesieczna",
                table: "Pracownik");

            migrationBuilder.DropColumn(
                name: "PriceInOfferDay",
                table: "OfferLines");

            migrationBuilder.DropColumn(
                name: "SalesPrice",
                table: "OfferLines");

            migrationBuilder.DropColumn(
                name: "CzasProgramisty",
                table: "Czesci");

            migrationBuilder.DropColumn(
                name: "KomponentId",
                table: "Czesci");

            migrationBuilder.DropColumn(
                name: "NumerProgramu",
                table: "Czesci");

            migrationBuilder.DropColumn(
                name: "TJ",
                table: "Czesci");

            migrationBuilder.DropColumn(
                name: "TPZ",
                table: "Czesci");

            migrationBuilder.DropColumn(
                name: "Wykonanie",
                table: "Czesci");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "KodPocztowy",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "Miasto",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "NrBudynku",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "NrTelefonu",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "OsobaKontaktowa",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "Ulica",
                table: "Client");

            migrationBuilder.RenameColumn(
                name: "ProcessesId",
                table: "Steps",
                newName: "ProcessesID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Steps",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Produkty",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "OfferLines",
                newName: "ProductID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "OfferLines",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_OfferLines_ProductId",
                table: "OfferLines",
                newName: "IX_OfferLines_ProductID");

            migrationBuilder.AlterColumn<string>(
                name: "StepNum",
                table: "Steps",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Sekwencja",
                table: "Steps",
                type: "int",
                nullable: false,
                defaultValueSql: "((1))",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ProcessesID",
                table: "Steps",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                table: "Steps",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<bool>(
                name: "Rwnolege",
                table: "Steps",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Produkty",
                type: "varchar(30)",
                unicode: false,
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "ProduktyId",
                table: "ProduktCzesci",
                type: "varchar(30)",
                unicode: false,
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Pracownik",
                type: "nchar(10)",
                fixedLength: true,
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Pracownik",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "ProductID",
                table: "OfferLines",
                type: "varchar(30)",
                unicode: false,
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NIP",
                table: "Client",
                type: "char(10)",
                unicode: false,
                fixedLength: true,
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "char(20)",
                oldUnicode: false,
                oldFixedLength: true,
                oldMaxLength: 20,
                oldNullable: true);

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
                    AdditionalEquipment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Medium = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    OfferID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    OfferId2 = table.Column<int>(type: "int", nullable: true),
                    ProductID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Sale = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    W = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferLines_old", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PracownikOfertaProcesy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfferId = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    PracownikId = table.Column<int>(type: "int", nullable: false),
                    ProcessID = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Produkty_old",
                columns: table => new
                {
                    ANSI = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    DN = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    Kvs = table.Column<int>(type: "int", nullable: false),
                    NumerRysNorma = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PN = table.Column<int>(type: "int", nullable: false),
                    TypWyrobuId = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    Uszczelnienie = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Waga = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WagaJednostka = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValueSql: "(N'kg')"),
                    Wersja = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    WymianyRmm = table.Column<int>(type: "int", nullable: false),
                    WymiarHmm = table.Column<int>(type: "int", nullable: false),
                    WymiaryDLmm = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProduktCzesci_ProduktyId",
                table: "ProduktCzesci",
                column: "ProduktyId");

            migrationBuilder.CreateIndex(
                name: "IX_OfertaTyp_OfferId",
                table: "OfertaTyp",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferLines",
                table: "OfferLines_old",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK__Offer__ClientsId__73852659",
                table: "Offer",
                column: "ClientsId",
                principalTable: "Client",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__Offer__Offerrer__719CDDE7",
                table: "Offer",
                column: "Offerrer",
                principalTable: "Pracownik",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__OfferLine__Produ__70A8B9AE",
                table: "OfferLines",
                column: "ProductID",
                principalTable: "Produkty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OfferLines_Offer_OfferId",
                table: "OfferLines",
                column: "OfferId",
                principalTable: "Offer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "ProduktCzesci_Produkty_FK",
                table: "ProduktCzesci",
                column: "ProduktyId",
                principalTable: "Produkty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
