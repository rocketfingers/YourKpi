﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace YouKpiBackend.ModelsEntity
{
    public partial class Process
    {
        public Process()
        {
            OfferLineProcess = new HashSet<OfferLineProcess>();
            OfferProcess = new HashSet<OfferProcess>();
            PracownikCzasStep = new HashSet<PracownikCzasStep>();
            ProcessStepsToDelete = new HashSet<ProcessStepsToDelete>();
            ProcessesProcessProcess = new HashSet<ProcessesProcess>();
            ProcessesProcessRelatedProcess = new HashSet<ProcessesProcess>();
            Steps = new HashSet<Steps>();
        }

        public string Id { get; set; }
        public string NazwaGrupyProcesu { get; set; }
        public string BusinessArea { get; set; }
        public string NazwaProcesu { get; set; }
        public string KontoGłówne { get; set; }
        public string TypZlecenia { get; set; }

        public virtual ICollection<OfferLineProcess> OfferLineProcess { get; set; }
        public virtual ICollection<OfferProcess> OfferProcess { get; set; }
        public virtual ICollection<PracownikCzasStep> PracownikCzasStep { get; set; }
        public virtual ICollection<ProcessStepsToDelete> ProcessStepsToDelete { get; set; }
        public virtual ICollection<ProcessesProcess> ProcessesProcessProcess { get; set; }
        public virtual ICollection<ProcessesProcess> ProcessesProcessRelatedProcess { get; set; }
        public virtual ICollection<Steps> Steps { get; set; }
    }
}