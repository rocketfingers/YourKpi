﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace YouKpiBackend.ModelsEntity
{
    public partial class OfferLines
    {
        public int Id { get; set; }
        public int? OfferId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public string W { get; set; }
        public string Medium { get; set; }
        public string AdditionalEquipment { get; set; }
        public string Sale { get; set; }

        public virtual Offer Offer { get; set; }
        public virtual Produkty Product { get; set; }
    }
}