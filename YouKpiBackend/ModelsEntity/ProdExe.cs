﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace YouKpiBackend.ModelsEntity
{
    public partial class ProdExe
    {
        public int Id { get; set; }
        public string IdentyfikatorZamowienia { get; set; }
        public long? WierszOferty { get; set; }
        public string Klient { get; set; }
        public string NumerKlienta { get; set; }
        public DateTime? PlannedEnd { get; set; }
        public string NazwaProcesu { get; set; }
        public string IdentyfikatorProcesu { get; set; }
        public DateTime ZakonczenieData { get; set; }
        public int? Wynik { get; set; }
        public string IdentyfikatorWyrobu { get; set; }
        public string TypWyrobu { get; set; }
        public int? Dn { get; set; }
        public int? IloscWyrobow { get; set; }
        public string Wersja { get; set; }
        public int CzasSpedzony { get; set; }
        public int? WykonaneStepy { get; set; }
    }
}