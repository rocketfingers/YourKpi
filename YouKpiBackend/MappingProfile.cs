﻿using AutoMapper;
using YouKpiBackend.BusinessLibrary.Production;
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
            CreateMap<MagazynTowary, StoreElementViewModel>()
            .ForMember(dest => dest.Magazyn, opt => opt.MapFrom(from => new StoreViewModel(4, "Towary")))
            .ReverseMap();

            CreateMap<ActivitiesHistoryModel, ActivityHistoryViewModel>();
            CreateMap<CompanyBasicInfo, CompanyBasicInfoViewModel>();
        }
    }
}
