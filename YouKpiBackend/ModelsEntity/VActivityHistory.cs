﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace YouKpiBackend.ModelsEntity
{
    [NotMapped]
    public partial class VActivityHistory
    {
        public int PracownikId { get; set; }
        public string PracownikName { get; set; }
        public string ProcessId { get; set; }
        public string ProcessName { get; set; }
        public DateTime? CzasStart { get; set; }
        public DateTime? CzasStop { get; set; }
        public int ClientsId { get; set; }
        public string Client { get; set; }
        public string PartId { get; set; }
        public string PartName { get; set; }
        public string WyrobId { get; set; }
        public int IloscCzesci { get; set; }
    }
}