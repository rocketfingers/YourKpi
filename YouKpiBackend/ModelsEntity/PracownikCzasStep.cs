﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace YouKpiBackend.ModelsEntity
{
    public partial class PracownikCzasStep
    {
        public int Id { get; set; }
        public int OfferLinesId { get; set; }
        public string ProcessId { get; set; }
        public int PracownikId { get; set; }
        public int LiczbaMinut { get; set; }
        public DateTime? CzasStart { get; set; }
        public DateTime? CzasStop { get; set; }
        public int Step { get; set; }

        public virtual OfferLines OfferLines { get; set; }
        public virtual Pracownik Pracownik { get; set; }
        public virtual Process Process { get; set; }
    }
}