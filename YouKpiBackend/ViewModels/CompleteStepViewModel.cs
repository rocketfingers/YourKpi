using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouKpiBackend.ViewModels
{
    public class SaveCompleteStepViewModel
    {
        public string ProcessId { get; set; }
        public int OfferLineId { get; set; }
        public int StepNum { get; set; }
        public int LiczbaPomiarow { get; set; }
        public int LiczbaPomiarowNok { get; set; }
        public bool Zakonczenie { get; set;}
        public int? ReasonCodeId { get; set; }
    }
}
