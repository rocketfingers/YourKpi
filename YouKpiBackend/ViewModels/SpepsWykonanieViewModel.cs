using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouKpiBackend.ViewModels
{
    public class SpepsWykonanieViewModel
    {
        public int StepOfferWykonanieId { get; set; }
        public int StepNum { get; set; }
        public string StepName { get; set; }
        public int Sekwencja { get; set; }
        public int LiczbaPomiarow { get; set; }
        public int LiczbaPomiarowNok { get; set; }
        public bool Zakonczone { get; set; }
    }
}
