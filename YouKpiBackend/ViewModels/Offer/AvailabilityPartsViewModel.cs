namespace YouKpiBackend.ViewModels.Offer
{
    public class AvailabilityPartsViewModel
    {
        public string Id { get; set; }
        public string Nazwa { get; set; }
        public string GatPodstawowy { get; set; }
        public string NumerRysNorma { get; set; }
        public string Wymiary { get; set; }
        public int? KomponentId { get; set; }
        public decimal? Tpz { get; set; }
        public decimal? Tj { get; set; }
        public int? AvailableParts { get; set; }
        public int? Ilosc { get; set; }
        public AvailabilityStatus Status { get; set; }
        public virtual AvailabilityComponentViewModel Komponent { get; set; }
    }
}
