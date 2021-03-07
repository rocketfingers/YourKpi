namespace YouKpiBackend.ViewModels.Offer
{
    public class AvailabilityComponentViewModel
    {
        public int Id { get; set; }
        public string KomponentId { get; set; }
        public string Nazwa { get; set; }
        public string Wymiar { get; set; }
        public string GatunekPodst { get; set; }
        public decimal? Ilosc { get; set; }
        public string Jednostka { get; set; }
        public decimal? CenaJednostkowa { get; set; }
        public string ProcessId { get; set; }
        public int? Ltid { get; set; }
        public int? AvailableComponents { get; set; }
        public AvailabilityStatus Status { get; set; }
    }
}
