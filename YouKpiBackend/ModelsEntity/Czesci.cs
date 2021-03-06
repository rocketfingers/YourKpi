﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace YouKpiBackend.ModelsEntity
{
    public partial class Czesci
    {
        public Czesci()
        {
            MagazynCzesci = new HashSet<MagazynCzesci>();
            PracownikCzasStep = new HashSet<PracownikCzasStep>();
            ProduktCzesci = new HashSet<ProduktCzesci>();
        }

        public string Id { get; set; }
        public string Nazwa { get; set; }
        public string GatPodstawowy { get; set; }
        public string NumerRysNorma { get; set; }
        public string Wymiary { get; set; }
        public decimal? Tpz { get; set; }
        public decimal? Tj { get; set; }
        public string NumerProgramu { get; set; }
        public int? CzasProgramisty { get; set; }
        public int? KomponentId { get; set; }
        public string Wykonanie { get; set; }

        public virtual Komponenty Komponent { get; set; }
        public virtual ICollection<MagazynCzesci> MagazynCzesci { get; set; }
        public virtual ICollection<PracownikCzasStep> PracownikCzasStep { get; set; }
        public virtual ICollection<ProduktCzesci> ProduktCzesci { get; set; }
    }
}