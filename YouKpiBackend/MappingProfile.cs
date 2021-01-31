using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YouKpiBackend.ModelsEntity;
using YouKpiBackend.ViewModels;

namespace YouKpiBackend
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<Pracownik, PracownikViewModel>().ReverseMap();
            CreateMap<MagazynCzesci, StoreElementViewModel>()
                .ForMember(dest => dest.Magazyn, opt => opt.MapFrom(from => new StoreViewModel(1, "Części")))
                .ReverseMap();
            CreateMap<MagazynProdukty, StoreElementViewModel>()
                .ForMember(dest => dest.Magazyn, opt => opt.MapFrom(from => new StoreViewModel(2, "Produkty")))
                .ReverseMap();
            CreateMap<MagazynKomponenty, StoreElementViewModel>()
                 .ForMember(dest => dest.Magazyn, opt => opt.MapFrom(from => new StoreViewModel(3, "Komponenty")))
                 .ReverseMap();
        }
    }
}
