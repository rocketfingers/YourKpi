﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace YouKpiBackend.ModelsEntity
{
    public partial class ProcessesProcess
    {
        public int Id { get; set; }
        public string ProcessId { get; set; }
        public string RelatedProcessId { get; set; }

        public virtual Process Process { get; set; }
        public virtual Process RelatedProcess { get; set; }
    }
}