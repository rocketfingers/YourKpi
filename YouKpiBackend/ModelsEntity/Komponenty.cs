﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace YouKpiBackend.ModelsEntity
{
    public partial class Komponenty
    {
        public Komponenty()
        {
            Czesci = new HashSet<Czesci>();
        }

        public int Id { get; set; }
        public string KomponentId { get; set; }
        public string Nazwa { get; set; }
        public string Wymiar { get; set; }
        public string GatunekPodst { get; set; }
        public decimal? Ilosc { get; set; }
        public string Jednostka { get; set; }
        public decimal? CenaJednostkowa { get; set; }
        public string ProcessId { get; set; }
        public int? Ltid { get; set; }

        public virtual ICollection<Czesci> Czesci { get; set; }
    }
}