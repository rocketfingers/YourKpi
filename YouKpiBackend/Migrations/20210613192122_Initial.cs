using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YouKpiBackend.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.EnsureSchema(
            //    name: "log");

            //migrationBuilder.CreateTable(
            //    name: "BusiessArea",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Client",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        NIP = table.Column<string>(type: "char(20)", unicode: false, fixedLength: true, maxLength: 20, nullable: true),
            //        Name = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
            //        KodPocztowy = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
            //        Miasto = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
            //        Ulica = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
            //        NrBudynku = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
            //        NrTelefonu = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
            //        Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
            //        OsobaKontaktowa = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Client", x => x.ID);
            //    });

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
            //    name: "MozliweStatusyOferty",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Status = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_MozliweStatusyOferty", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "MozliwyDN",
            //    columns: table => new
            //    {
            //        DN = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_MozliwyDN", x => x.DN);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "MozliwyOrderType",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        OrderType = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //    });

            //migrationBuilder.CreateTable(
            //    name: "MożliwaGrupaProcesu",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //    });

            //migrationBuilder.CreateTable(
            //    name: "NLogAllErrors",
            //    schema: "log",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Application = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
            //        Logged = table.Column<DateTime>(type: "datetime", nullable: true),
            //        Level = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
            //        Message = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
            //        Exception = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
            //        User = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
            //        Callsite = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
            //        Ip = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_NLogAllErrors", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "NLogAsp",
            //    schema: "log",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Application = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
            //        Logged = table.Column<DateTime>(type: "datetime", nullable: true),
            //        Level = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
            //        Message = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
            //        Url = table.Column<string>(type: "varchar(5000)", unicode: false, maxLength: 5000, nullable: true),
            //        User = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
            //        StatusCode = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
            //        Ip = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_NLogAsp", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "NLogOwn",
            //    schema: "log",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Application = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
            //        Logged = table.Column<DateTime>(type: "datetime", nullable: true),
            //        Level = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
            //        Message = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
            //        User = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
            //        Callsite = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
            //        Ip = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_NLogOwn", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Pracownik",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
            //        TrybPracy = table.Column<int>(type: "int", nullable: false),
            //        CzasDostepuMin = table.Column<int>(type: "int", nullable: false),
            //        IsAdministrator = table.Column<bool>(type: "bit", nullable: false),
            //        Login = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
            //        Password = table.Column<string>(type: "varchar(70)", unicode: false, maxLength: 70, nullable: false),
            //        Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
            //        Salt = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
            //        Rola = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))"),
            //        StawkaMiesieczna = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
            //        StawkaGodzinowa = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Pracownik", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Process",
            //    columns: table => new
            //    {
            //        Id = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
            //        NazwaGrupyProcesu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        BusinessArea = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        NazwaProcesu = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
            //        KontoGłówne = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
            //        TypZlecenia = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false, defaultValueSql: "('R')")
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Process", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Produkty",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        TypWyrobuId = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
            //        NumerRysNorma = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
            //        DN = table.Column<int>(type: "int", nullable: false),
            //        PN = table.Column<int>(type: "int", nullable: false),
            //        ANSI = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
            //        Wersja = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
            //        Uszczelnienie = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
            //        Cena = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
            //        Waluta = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
            //        ProductName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Produkty", x => x.ID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Projects",
            //    columns: table => new
            //    {
            //        ID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
            //        ProductNrClient = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
            //        ProductNrInt = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
            //        QafNr = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
            //        To = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
            //        Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
            //        Phase = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
            //        DateOfferd = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
            //        DateTD = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
            //        Nomination = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
            //        RodzajProjektu = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
            //        Location = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
            //        Version = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
            //        Client = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
            //        ClientGroup = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
            //        ProductType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
            //        ProductSubType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
            //        FunelStep = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
            //        Funel = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true)
            //    },
            //    constraints: table =>
            //    {
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
            //    name: "TypWyrobu",
            //    columns: table => new
            //    {
            //        Id = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
            //        IDGrupyWyrobu = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
            //        TypKonstrukcji = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
            //        TypPrzelotu = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
            //        TypKuli = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false, defaultValueSql: "('pływajaca')")
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TypWyrobu", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TypWyrobuIdDn",
            //    columns: table => new
            //    {
            //        TypWyrobuId = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
            //        WymiaryDLmm = table.Column<int>(type: "int", nullable: false),
            //        WymianyRmm = table.Column<int>(type: "int", nullable: false),
            //        WymiarHmm = table.Column<int>(type: "int", nullable: false),
            //        Kvs = table.Column<int>(type: "int", nullable: false),
            //        Waga = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            //        WagaJednostka = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        DN = table.Column<int>(type: "int", nullable: true),
            //        TempMin = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((-60))"),
            //        TempMax = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((120))")
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TypWyrobuIdDn", x => x.TypWyrobuId);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Czesci",
            //    columns: table => new
            //    {
            //        Id = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
            //        Nazwa = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
            //        GatPodstawowy = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
            //        NumerRysNorma = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
            //        Wymiary = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
            //        TPZ = table.Column<decimal>(type: "decimal(6,2)", nullable: true),
            //        TJ = table.Column<decimal>(type: "decimal(6,2)", nullable: true),
            //        NumerProgramu = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
            //        CzasProgramisty = table.Column<int>(type: "int", nullable: true),
            //        KomponentId = table.Column<int>(type: "int", nullable: true),
            //        Wykonanie = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Czesci", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK__Czesci__Komponen__79C80F94",
            //            column: x => x.KomponentId,
            //            principalTable: "Komponenty",
            //            principalColumn: "ID",
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
            //    name: "Offer",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        ProjectsId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
            //        Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
            //        OrderType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        OfferDate = table.Column<DateTime>(type: "datetime", nullable: true),
            //        ClientsId = table.Column<int>(type: "int", nullable: false),
            //        Offerrer = table.Column<int>(type: "int", nullable: true),
            //        OrderDate = table.Column<DateTime>(type: "datetime", nullable: true),
            //        PlannedEnd = table.Column<DateTime>(type: "datetime", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Offer", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK__Offer__ClientsId__740F363E",
            //            column: x => x.ClientsId,
            //            principalTable: "Client",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK__Offer__Offerrer__3C69FB99",
            //            column: x => x.Offerrer,
            //            principalTable: "Pracownik",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "PracownikProcess",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        PracownikId = table.Column<int>(type: "int", nullable: false),
            //        ProcessId = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_PracownikProcess", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_PracownikProcess_Pracownik",
            //            column: x => x.PracownikId,
            //            principalTable: "Pracownik",
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
            //    name: "Steps",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        StepId = table.Column<string>(type: "nvarchar(61)", maxLength: 61, nullable: true),
            //        StepName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
            //        StepNum = table.Column<int>(type: "int", nullable: false),
            //        ProcessesId = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
            //        Sekwencja = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Steps", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_PROCESS_STEP_ID",
            //            column: x => x.ProcessesId,
            //            principalTable: "Process",
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
            //    name: "ProduktCzesci",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        CzesciId = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
            //        IloscSztuk = table.Column<int>(type: "int", nullable: true),
            //        ProduktId = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ProduktCzesci", x => x.Id);
            //        table.ForeignKey(
            //            name: "ProduktCzesci_Czesci_FK",
            //            column: x => x.CzesciId,
            //            principalTable: "Czesci",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "ProduktCzesci_Produkty_FK",
            //            column: x => x.ProduktId,
            //            principalTable: "Produkty",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "OfferLines",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        OfferId = table.Column<int>(type: "int", nullable: true),
            //        Quantity = table.Column<int>(type: "int", nullable: false),
            //        W = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
            //        Medium = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
            //        AdditionalEquipment = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Sale = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
            //        PriceInOfferDay = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
            //        SalesPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
            //        ProductId = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_OfferLines", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK__OfferLine__Offer__33008CF0",
            //            column: x => x.OfferId,
            //            principalTable: "Offer",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK__OfferLine__Produ__33F4B129",
            //            column: x => x.ProductId,
            //            principalTable: "Produkty",
            //            principalColumn: "ID",
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
            //    name: "IX_Offer_ClientsId",
            //    table: "Offer",
            //    column: "ClientsId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Offer_Offerrer",
            //    table: "Offer",
            //    column: "Offerrer");

            //migrationBuilder.CreateIndex(
            //    name: "IX_OfferLineProcess_OfferLineId",
            //    table: "OfferLineProcess",
            //    column: "OfferLineId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_OfferLineProcess_ProcessId",
            //    table: "OfferLineProcess",
            //    column: "ProcessId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_OfferLines_OfferId",
            //    table: "OfferLines",
            //    column: "OfferId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_OfferLines_ProductId",
            //    table: "OfferLines",
            //    column: "ProductId");

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
            //    name: "IX_PracownikProcess_PracownikId",
            //    table: "PracownikProcess",
            //    column: "PracownikId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ProcessesProcess_ProcessId",
            //    table: "ProcessesProcess",
            //    column: "ProcessId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ProcessesProcess_RelatedProcessId",
            //    table: "ProcessesProcess",
            //    column: "RelatedProcessId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ProduktCzesci_CzesciId",
            //    table: "ProduktCzesci",
            //    column: "CzesciId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ProduktCzesci_ProduktId",
            //    table: "ProduktCzesci",
            //    column: "ProduktId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_StepOfferWykonanie_ReasonCodeID",
            //    table: "StepOfferWykonanie",
            //    column: "ReasonCodeID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Steps_ProcessesId",
            //    table: "Steps",
            //    column: "ProcessesId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_StepsMachines_MachineId",
            //    table: "StepsMachines",
            //    column: "MachineId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_StepsMachines_StepId",
            //    table: "StepsMachines",
            //    column: "StepId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusiessArea");

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
                name: "MozliweStatusyOferty");

            migrationBuilder.DropTable(
                name: "MozliwyDN");

            migrationBuilder.DropTable(
                name: "MozliwyOrderType");

            migrationBuilder.DropTable(
                name: "MożliwaGrupaProcesu");

            migrationBuilder.DropTable(
                name: "NLogAllErrors",
                schema: "log");

            migrationBuilder.DropTable(
                name: "NLogAsp",
                schema: "log");

            migrationBuilder.DropTable(
                name: "NLogOwn",
                schema: "log");

            migrationBuilder.DropTable(
                name: "OfferLineProcess");

            migrationBuilder.DropTable(
                name: "OfferProcess");

            migrationBuilder.DropTable(
                name: "PracownikCzasStep");

            migrationBuilder.DropTable(
                name: "PracownikProcess");

            migrationBuilder.DropTable(
                name: "ProcessesProcess");

            migrationBuilder.DropTable(
                name: "ProduktCzesci");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "StepOfferWykonanie");

            migrationBuilder.DropTable(
                name: "StepsMachines");

            migrationBuilder.DropTable(
                name: "TypWyrobu");

            migrationBuilder.DropTable(
                name: "TypWyrobuIdDn");

            migrationBuilder.DropTable(
                name: "Towary");

            migrationBuilder.DropTable(
                name: "Kontrahent");

            migrationBuilder.DropTable(
                name: "Lokacja");

            migrationBuilder.DropTable(
                name: "OfferLines");

            migrationBuilder.DropTable(
                name: "Czesci");

            migrationBuilder.DropTable(
                name: "ReasonCodes");

            migrationBuilder.DropTable(
                name: "Maszyny");

            migrationBuilder.DropTable(
                name: "Steps");

            migrationBuilder.DropTable(
                name: "Offer");

            migrationBuilder.DropTable(
                name: "Produkty");

            migrationBuilder.DropTable(
                name: "Komponenty");

            migrationBuilder.DropTable(
                name: "Process");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Pracownik");
        }
    }
}
