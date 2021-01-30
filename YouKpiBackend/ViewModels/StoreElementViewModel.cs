using System;

namespace YouKpiBackend.ViewModels
{
    public class StoreElementViewModel 
    {
        public int Id { get; set; }
        public string ElementId { get; set; }
        public int? KontrahentId { get; set; }
        public DateTime? DataPrzyjecia { get; set; }
        public int? LokacjaId { get; set; }
        public string NrFakturyId { get; set; }
        public int? Ilosc { get; set; }
        public string Jednostka { get; set; }
        public StoreViewModel Magazyn { get; set; }
        public decimal? CenaJdnNetto { get; set; }
    }

    public class StoreViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
