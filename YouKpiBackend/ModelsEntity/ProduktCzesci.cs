﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace YouKpiBackend.ModelsEntity
{
    public partial class ProduktCzesci
    {
        public int Id { get; set; }
        public string ProduktyId { get; set; }
        public string CzesciId { get; set; }
        public int? IloscSztuk { get; set; }

        public virtual Czesci Czesci { get; set; }
        public virtual Produkty Produkty { get; set; }
    }
}