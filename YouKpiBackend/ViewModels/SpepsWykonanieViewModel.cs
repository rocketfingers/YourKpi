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
        public bool StepStarted { get; set; }
        public bool StepStartedByMe { get; set; }
        public bool StepUsedBySomeoneElse { get; set; }
        public DateTime? StartedDate { get; set; }
        public DateTime? ShouldStartBefore { get; set; }
        public TimeToEnd PlannedStartStatus { get; set; }
        public int? ReasonCodeId { get; set; }
        public int TimeSpendMe { get; set; }
        public int TimeSpendOther { get; set; }
        public DateTime? ClosedOn { get; set; }
        public int? GScore { get; set; }
    }
    public enum TimeToEnd
    {
        OnTime = 0,
        AlmostRed = 1,
        Red = 2,
            Unknown = 3
    }
}
