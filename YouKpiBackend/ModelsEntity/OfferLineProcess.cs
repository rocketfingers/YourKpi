﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace YouKpiBackend.ModelsEntity
{
    public partial class OfferLineProcess
    {
        public int Id { get; set; }
        public int OfferLineId { get; set; }
        public string ProcessId { get; set; }

        public virtual OfferLines OfferLine { get; set; }
        public virtual Process Process { get; set; }
    }
}