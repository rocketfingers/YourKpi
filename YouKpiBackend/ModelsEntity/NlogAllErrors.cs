﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace YouKpiBackend.ModelsEntity
{
    public partial class NlogAllErrors
    {
        public int Id { get; set; }
        public string Application { get; set; }
        public DateTime? Logged { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
        public string User { get; set; }
        public string Callsite { get; set; }
        public string Ip { get; set; }
    }
}