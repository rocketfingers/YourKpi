using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouKpiBackend.ViewModels
{
    public class ReportOtifViewModel
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Week { get; set; }
        public string Process { get; set; }
        public string ProcessName { get; set; }
        public string ProcessGroupName { get; set; }
        public float Target { get; set; }
        public float Difference { get; set; }
        public int Total { get; set; }
        public int OnTime { get; set; }
    }
}
