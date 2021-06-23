using System.Collections.Generic;
using YouKpiBackend.ViewModels.Offer;

namespace YouKpiBackend.ViewModels.Offer
{
    public class AvailabilityOfferLinesViewModel
    {
        public int Id { get; set; }
        public int? OfferId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string W { get; set; }
        public string Medium { get; set; }
        public string AdditionalEquipment { get; set; }
        public string Sale { get; set; }
        public decimal? PriceInOfferDay { get; set; }
        public decimal? SalesPrice { get; set; }
        public int? AvailableProducts { get; set; }
        public AvailabilityStatus Status { get; set; }
        public List<AvailabilityPartsViewModel> Parts { get; set; }
    }
}
