﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace YouKpiBackend.ModelsEntity
{
    public partial class ProcessStepsToDelete
    {
        public string Id { get; set; }
        public int? NumerKroku { get; set; }
        public string Nazwa { get; set; }
        public string ProcessId { get; set; }

        public virtual Process Process { get; set; }
    }
}