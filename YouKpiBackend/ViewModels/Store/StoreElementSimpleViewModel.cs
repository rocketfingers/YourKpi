using System;

namespace YouKpiBackend.ViewModels.Store
{
    public class StoreElementSimpleViewModel
    {
        public StoreElementSimpleViewModel(string id, string name, decimal? price)
        {
            Id = id;
            Name = name;
            Price = Math.Round(price ?? 0, 2);
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
