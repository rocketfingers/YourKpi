using System;

namespace YouKpiBackend.ViewModels.Commodity
{
    public class CommodityViewModel 
    {
        public int Id { get; set; }
        public string TowarId { get; set; }
        public int Nr { get; set; }
        public string Nazwa { get; set; }
        public int? Ilosc { get; set; }
        public decimal? CenaJendNet { get; set; }
        public string Magazyn { get; set; }
        public DateTime? DataPrzyjecia { get; set; }
        public int KontrahentId { get; set; }
        public int? LokacjaId { get; set; }
        public string Kontrahent { get; set; }
        public string Lokacja { get; set; }
    }
}
