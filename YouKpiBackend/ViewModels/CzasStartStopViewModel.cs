using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouKpiBackend.ViewModels
{
    public class CzasStartStopViewModel
    {
        public string ProcessId { get; set; }
        public int OfferLineId { get; set; }
        public int StepNum { get; set; }
        public int PracownikId { get; set; }
    }
}
