﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using YouKpiBackend.ModelsEntity;

namespace YouKpiBackend.DbContexts
{
    public partial class YoukpiContext : DbContext
    {
        public YoukpiContext()
        {
        }

        public YoukpiContext(DbContextOptions<YoukpiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BusiessArea> BusiessArea { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Czesci> Czesci { get; set; }
        public virtual DbSet<Komponenty> Komponenty { get; set; }
        public virtual DbSet<Maszyny> Maszyny { get; set; }
        public virtual DbSet<MozliweStatusyOferty> MozliweStatusyOferty { get; set; }
        public virtual DbSet<MozliwyDn> MozliwyDn { get; set; }
        public virtual DbSet<MozliwyOrderType> MozliwyOrderType { get; set; }
        public virtual DbSet<MożliwaGrupaProcesu> MożliwaGrupaProcesu { get; set; }
        public virtual DbSet<Offer> Offer { get; set; }
        public virtual DbSet<OfferLineProcess> OfferLineProcess { get; set; }
        public virtual DbSet<OfferLines> OfferLines { get; set; }
        public virtual DbSet<OfferProcess> OfferProcess { get; set; }
        public virtual DbSet<Pracownik> Pracownik { get; set; }
        public virtual DbSet<PracownikCzasStep> PracownikCzasStep { get; set; }
        public virtual DbSet<PracownikProcess> PracownikProcess { get; set; }
        public virtual DbSet<Process> Process { get; set; }
        public virtual DbSet<ProcessStepsToDelete> ProcessStepsToDelete { get; set; }
        public virtual DbSet<ProdExe> ProdExe { get; set; }
        public virtual DbSet<ProduktCzesci> ProduktCzesci { get; set; }
        public virtual DbSet<Produkty> Produkty { get; set; }
        public virtual DbSet<ProduktyOld> ProduktyOld { get; set; }
        public virtual DbSet<Projects> Projects { get; set; }
        public virtual DbSet<ReasonCodes> ReasonCodes { get; set; }
        public virtual DbSet<StepOfferWykonanie> StepOfferWykonanie { get; set; }
        public virtual DbSet<Steps> Steps { get; set; }
        public virtual DbSet<StepsMachines> StepsMachines { get; set; }
        public virtual DbSet<StepsToDelete> StepsToDelete { get; set; }
        public virtual DbSet<TypWyrobu> TypWyrobu { get; set; }
        public virtual DbSet<TypWyrobuIdDn> TypWyrobuIdDn { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BusiessArea>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Email)
                    .HasColumnName("EMAIL")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.KodPocztowy)
                    .HasColumnName("KOD_POCZTOWY")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Miasto)
                    .HasColumnName("MIASTO")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Nip)
                    .IsRequired()
                    .HasColumnName("NIP")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.NrBudynku)
                    .HasColumnName("NR_BUDYNKU")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.NrTelefonu)
                    .HasColumnName("NR_TELEFONU")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Ulica)
                    .HasColumnName("ULICA")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Czesci>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.GatPodstawowy)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.KomponentId).HasColumnName("KOMPONENT_ID");

                entity.Property(e => e.Nazwa)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NumerRysNorma)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Tj)
                    .HasColumnName("TJ")
                    .HasColumnType("decimal(6, 2)");

                entity.Property(e => e.Tpz)
                    .HasColumnName("TPZ")
                    .HasColumnType("decimal(6, 2)");

                entity.Property(e => e.Wymiary).HasMaxLength(50);

                entity.HasOne(d => d.Komponent)
                    .WithMany(p => p.Czesci)
                    .HasForeignKey(d => d.KomponentId)
                    .HasConstraintName("FK__Czesci__KOMPONEN__0697FACD");
            });

            modelBuilder.Entity<Komponenty>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CenaJednostkowa)
                    .HasColumnName("CENA_JEDNOSTKOWA")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.GatunekPodst)
                    .HasColumnName("GATUNEK_PODST")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.Ilosc)
                    .HasColumnName("ILOSC")
                    .HasColumnType("decimal(11, 6)");

                entity.Property(e => e.Jednostka)
                    .HasColumnName("JEDNOSTKA")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.KomponentId)
                    .HasColumnName("KOMPONENT_ID")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Nazwa)
                    .HasColumnName("NAZWA")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.ProcessId)
                    .HasColumnName("PROCESS_ID")
                    .HasMaxLength(30);

                entity.Property(e => e.Wymiar)
                    .HasColumnName("WYMIAR")
                    .HasMaxLength(300)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Maszyny>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.MiejscePracy)
                    .HasColumnName("MIEJSCE_PRACY")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Nazwa)
                    .HasColumnName("NAZWA")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Opis)
                    .HasColumnName("OPIS")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.OsX)
                    .HasColumnName("OS_X")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.OsY)
                    .HasColumnName("OS_Y")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.OsZ)
                    .HasColumnName("OS_Z")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Rodzaj)
                    .HasColumnName("RODZAJ")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.TrybPracy)
                    .HasColumnName("TRYB_PRACY")
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MozliweStatusyOferty>(entity =>
            {
                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MozliwyDn>(entity =>
            {
                entity.HasKey(e => e.Dn);

                entity.ToTable("MozliwyDN");

                entity.Property(e => e.Dn)
                    .HasColumnName("DN")
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<MozliwyOrderType>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.OrderType)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MożliwaGrupaProcesu>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Offer>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.OfferDate).HasColumnType("datetime");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.OrderType)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PlannedEnd).HasColumnType("datetime");

                entity.Property(e => e.ProjectsId).HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.Clients)
                    .WithMany(p => p.Offer)
                    .HasForeignKey(d => d.ClientsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Offer__ClientsId__078C1F06");

                entity.HasOne(d => d.OfferrerNavigation)
                    .WithMany(p => p.Offer)
                    .HasForeignKey(d => d.Offerrer)
                    .HasConstraintName("FK__Offer__Offerrer__3C69FB99");
            });

            modelBuilder.Entity<OfferLineProcess>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.OfferLineId).HasColumnName("OFFER_LINE_ID");

                entity.Property(e => e.ProcessId)
                    .IsRequired()
                    .HasColumnName("PROCESS_ID")
                    .HasMaxLength(30);

                entity.HasOne(d => d.OfferLine)
                    .WithMany(p => p.OfferLineProcess)
                    .HasForeignKey(d => d.OfferLineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OfferLine__OFFER__40058253");

                entity.HasOne(d => d.Process)
                    .WithMany(p => p.OfferLineProcess)
                    .HasForeignKey(d => d.ProcessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OfferLine__PROCE__09746778");
            });

            modelBuilder.Entity<OfferLines>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Medium).HasMaxLength(150);

                entity.Property(e => e.ProductId)
                    .IsRequired()
                    .HasColumnName("ProductID")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Sale).HasMaxLength(50);

                entity.Property(e => e.W).HasMaxLength(50);

                entity.HasOne(d => d.Offer)
                    .WithMany(p => p.OfferLines)
                    .HasForeignKey(d => d.OfferId)
                    .HasConstraintName("FK__OfferLine__Offer__32767D0B");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OfferLines)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OfferLine__Produ__0B5CAFEA");
            });

            modelBuilder.Entity<OfferProcess>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.OfferId).HasColumnName("OFFER_ID");

                entity.Property(e => e.ProcessId)
                    .IsRequired()
                    .HasColumnName("PROCESS_ID")
                    .HasMaxLength(30);

                entity.HasOne(d => d.Offer)
                    .WithMany(p => p.OfferProcess)
                    .HasForeignKey(d => d.OfferId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OfferProc__OFFER__42E1EEFE");

                entity.HasOne(d => d.Process)
                    .WithMany(p => p.OfferProcess)
                    .HasForeignKey(d => d.ProcessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OfferProc__PROCE__0D44F85C");
            });

            modelBuilder.Entity<Pracownik>(entity =>
            {
                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.Salt).HasMaxLength(20);
            });

            modelBuilder.Entity<PracownikCzasStep>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CzasStart).HasColumnType("datetime");

                entity.Property(e => e.CzasStop).HasColumnType("datetime");

                entity.Property(e => e.OfferLinesId).HasColumnName("OfferLinesID");

                entity.Property(e => e.ProcessId)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.HasOne(d => d.OfferLines)
                    .WithMany(p => p.PracownikCzasStep)
                    .HasForeignKey(d => d.OfferLinesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PracownikCzasStep_OfferLines");

                entity.HasOne(d => d.Pracownik)
                    .WithMany(p => p.PracownikCzasStep)
                    .HasForeignKey(d => d.PracownikId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PracownikCzasStep_Pracownik");

                entity.HasOne(d => d.Process)
                    .WithMany(p => p.PracownikCzasStep)
                    .HasForeignKey(d => d.ProcessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PracownikCzasStep_Process");
            });

            modelBuilder.Entity<PracownikProcess>(entity =>
            {
                entity.Property(e => e.ProcessId)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.HasOne(d => d.Pracownik)
                    .WithMany(p => p.PracownikProcess)
                    .HasForeignKey(d => d.PracownikId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PracownikProcess_Pracownik");
            });

            modelBuilder.Entity<Process>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(30);

                entity.Property(e => e.BusinessArea)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.KontoGłówne).HasMaxLength(50);

                entity.Property(e => e.NazwaGrupyProcesu)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NazwaProcesu)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.TypZlecenia)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("('R')");
            });

            modelBuilder.Entity<ProcessStepsToDelete>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Nazwa)
                    .HasColumnName("NAZWA")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.NumerKroku).HasColumnName("NUMER_KROKU");

                entity.Property(e => e.ProcessId)
                    .HasColumnName("PROCESS_ID")
                    .HasMaxLength(30);

                entity.HasOne(d => d.Process)
                    .WithMany(p => p.ProcessStepsToDelete)
                    .HasForeignKey(d => d.ProcessId)
                    .HasConstraintName("FK_PROCESS_ID");
            });

            modelBuilder.Entity<ProdExe>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ProdExe");

                entity.Property(e => e.Dn).HasColumnName("DN");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(81);

                entity.Property(e => e.IdentyfikatorWyrobu)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.IdentyfikatorZamowienia)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Klient)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.NazwaProcesu)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.NumerKlienta)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.OfferLineId).HasColumnName("OfferLineID");

                entity.Property(e => e.PlannedEnd).HasColumnType("datetime");

                entity.Property(e => e.ProcessId)
                    .IsRequired()
                    .HasColumnName("processId")
                    .HasMaxLength(30);

                entity.Property(e => e.TypWyrobu)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Wersja)
                    .HasColumnName("wersja")
                    .HasMaxLength(5);

                entity.Property(e => e.ZakonczenieData).HasColumnType("datetime");
            });

            modelBuilder.Entity<ProduktCzesci>(entity =>
            {
                entity.Property(e => e.CzesciId)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ProduktyId)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.Czesci)
                    .WithMany(p => p.ProduktCzesci)
                    .HasForeignKey(d => d.CzesciId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ProduktCzesci_Czesci_FK");

                entity.HasOne(d => d.Produkty)
                    .WithMany(p => p.ProduktCzesci)
                    .HasForeignKey(d => d.ProduktyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ProduktCzesci_Produkty_FK");
            });

            modelBuilder.Entity<Produkty>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Ansi)
                    .IsRequired()
                    .HasColumnName("ANSI")
                    .HasMaxLength(5);

                entity.Property(e => e.Cena).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Dn).HasColumnName("DN");

                entity.Property(e => e.NumerRysNorma)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Pn).HasColumnName("PN");

                entity.Property(e => e.TypWyrobuId)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Uszczelnienie)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Waluta).HasMaxLength(5);

                entity.Property(e => e.Wersja)
                    .IsRequired()
                    .HasMaxLength(5);
            });

            modelBuilder.Entity<ProduktyOld>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Produkty_old");

                entity.Property(e => e.Ansi)
                    .IsRequired()
                    .HasColumnName("ANSI")
                    .HasMaxLength(5);

                entity.Property(e => e.Dn).HasColumnName("DN");

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.NumerRysNorma)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Pn).HasColumnName("PN");

                entity.Property(e => e.TypWyrobuId)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Uszczelnienie)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Waga).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.WagaJednostka)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("(N'kg')");

                entity.Property(e => e.Wersja)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.WymiaryDlmm).HasColumnName("WymiaryDLmm");
            });

            modelBuilder.Entity<Projects>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Client).HasMaxLength(5);

                entity.Property(e => e.ClientGroup).HasMaxLength(50);

                entity.Property(e => e.DateOfferd).HasMaxLength(50);

                entity.Property(e => e.DateTd)
                    .HasColumnName("DateTD")
                    .HasMaxLength(20);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Funel).HasMaxLength(40);

                entity.Property(e => e.FunelStep).HasMaxLength(255);

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasColumnName("ID")
                    .HasMaxLength(50);

                entity.Property(e => e.Location).HasMaxLength(10);

                entity.Property(e => e.Nomination).HasMaxLength(8);

                entity.Property(e => e.Phase).HasMaxLength(10);

                entity.Property(e => e.ProductNrClient).HasMaxLength(100);

                entity.Property(e => e.ProductNrInt).HasMaxLength(20);

                entity.Property(e => e.ProductSubType).HasMaxLength(50);

                entity.Property(e => e.ProductType).HasMaxLength(50);

                entity.Property(e => e.QafNr).HasMaxLength(255);

                entity.Property(e => e.RodzajProjektu).HasMaxLength(10);

                entity.Property(e => e.Status).HasMaxLength(20);

                entity.Property(e => e.To).HasMaxLength(25);

                entity.Property(e => e.Version).HasMaxLength(10);
            });

            modelBuilder.Entity<ReasonCodes>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IdDifferenceReasonCode).HasColumnName("ID_DIFFERENCE_REASON_CODE");

                entity.Property(e => e.Opis)
                    .HasColumnName("OPIS")
                    .HasMaxLength(1000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<StepOfferWykonanie>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClosedOn).HasColumnType("datetime");

                entity.Property(e => e.OfferLineId).HasColumnName("OfferLineID");

                entity.Property(e => e.ProcessId)
                    .IsRequired()
                    .HasColumnName("ProcessID")
                    .HasMaxLength(30);

                entity.Property(e => e.ReasonCodeId).HasColumnName("ReasonCodeID");

                entity.Property(e => e.StartedOn).HasColumnType("datetime");

                entity.HasOne(d => d.ReasonCode)
                    .WithMany(p => p.StepOfferWykonanie)
                    .HasForeignKey(d => d.ReasonCodeId)
                    .HasConstraintName("FK_StepOfferWykonanie_ReasonCodes");
            });

            modelBuilder.Entity<Steps>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ProcessesId)
                    .HasColumnName("ProcessesID")
                    .HasMaxLength(30);

                entity.Property(e => e.StepId)
                    .HasColumnName("STEP_ID")
                    .HasMaxLength(61);

                entity.Property(e => e.StepName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.Processes)
                    .WithMany(p => p.Steps)
                    .HasForeignKey(d => d.ProcessesId)
                    .HasConstraintName("FK_PROCESS_STEP_ID");
            });

            modelBuilder.Entity<StepsMachines>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.MachineId)
                    .HasColumnName("MACHINE_ID")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.StepId).HasColumnName("STEP_ID");

                entity.HasOne(d => d.Machine)
                    .WithMany(p => p.StepsMachines)
                    .HasForeignKey(d => d.MachineId)
                    .HasConstraintName("FK__StepsMach__MACHI__0E391C95");

                entity.HasOne(d => d.Step)
                    .WithMany(p => p.StepsMachines)
                    .HasForeignKey(d => d.StepId)
                    .HasConstraintName("FK__StepsMach__STEP___0F2D40CE");
            });

            modelBuilder.Entity<StepsToDelete>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ProcessesId)
                    .IsRequired()
                    .HasColumnName("ProcessesID")
                    .HasMaxLength(50);

                entity.Property(e => e.Sekwencja).HasDefaultValueSql("((1))");

                entity.Property(e => e.StepName)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<TypWyrobu>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.IdgrupyWyrobu)
                    .IsRequired()
                    .HasColumnName("IDGrupyWyrobu")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.TypKonstrukcji)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.TypKuli)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('pływajaca')");

                entity.Property(e => e.TypPrzelotu)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TypWyrobuIdDn>(entity =>
            {
                entity.HasKey(e => e.TypWyrobuId);

                entity.Property(e => e.TypWyrobuId)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Dn).HasColumnName("DN");

                entity.Property(e => e.TempMax).HasDefaultValueSql("((120))");

                entity.Property(e => e.TempMin).HasDefaultValueSql("((-60))");

                entity.Property(e => e.Waga).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.WagaJednostka)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.WymiaryDlmm).HasColumnName("WymiaryDLmm");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}