using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using System;

namespace YouKpiBackend.BusinessLibrary.Production
{
    public class ActivitiesHistoryModel
    {
        public string PracownikName { get; set; }
        public string ProcessId { get; set; }
        public string ProcessName { get; set; }
        public DateTime Day { get; set; }
        public double TimeSpend { get; set; }
        public int IloscPomiarow { get; set; }
        public int IloscPomiarowNok { get; set; }
        public int ClientId { get; set; }
        public string Client { get; set; }
        public string WyrobId { get; set; }
        public string PartId { get; set; }
        public string PartName { get; set; }
        public int CountPart { get; set; }
    }
}