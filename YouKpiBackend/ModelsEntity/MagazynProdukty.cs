﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace YouKpiBackend.ModelsEntity
{
    public partial class MagazynProdukty
    {
        public int Id { get; set; }
        public int? KontrahentId { get; set; }
        public DateTime? DataPrzyjecia { get; set; }
        public int? LokacjaId { get; set; }
        public string NrFakturyId { get; set; }
        public int? Ilosc { get; set; }
        public string Jednostka { get; set; }
        public decimal? CenaJdnNetto { get; set; }
        public int? ElementId { get; set; }

        public virtual Produkty Element { get; set; }
        public virtual Kontrahent Kontrahent { get; set; }
        public virtual Lokacja Lokacja { get; set; }
    }
}