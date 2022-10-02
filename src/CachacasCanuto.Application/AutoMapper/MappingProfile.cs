using AutoMapper;
using CachacasCanuto.Application.ViewModels.Customers;
using CachacasCanuto.Application.ViewModels.Products;
using CachacasCanuto.Application.ViewModels.Reports;
using CachacasCanuto.Application.ViewModels.Sales;
using CachacasCanuto.Core.Entities.Products;
using CachacasCanuto.Core.Entities.Reports;
using CachacasCanuto.Core.Entities.Sales;
using CachacasCanuto.Infrastructure.ExternalResources.ViewModels;

namespace CachacasCanuto.Application.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ExternalProductViewModel, ProductViewModel>()
                 .ForMember(s => s.Id, opt => opt.MapFrom(src => src.Id))
                 .ForMember(s => s.Classification, opt => opt.MapFrom(src => src.Classification))
                 .ForMember(s => s.AlcooholContent, opt => opt.MapFrom(src => src.AlcooholContent))
                 .ForMember(s => s.Price, opt => opt.MapFrom(src => src.Price))
                 .ForMember(s => s.Name, opt => opt.MapFrom(src => src.Name))
                 .ForMember(s => s.Label, opt => opt.MapFrom(src => src.Label))
            .ReverseMap();

            CreateMap<ExternalCustomerViewModel, CustomerViewModel>()
                 .ForMember(s => s.Id, opt => opt.MapFrom(src => src.Id))
                 .ForMember(s => s.Name, opt => opt.MapFrom(src => src.Name))
                 .ForMember(s => s.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
             .ReverseMap();

            CreateMap<ExternalSaleViewModel, Sale>()
                .ForMember(s => s.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(s => s.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
                .ForMember(s => s.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(d => d.Itens, opt => opt.MapFrom(src => src.Itens))
            .ReverseMap();

            CreateMap<ExternalSaleItemViewModel, SaleItem>()
                 .ForMember(s => s.Id, opt => opt.MapFrom(src => src.Id))
                 .ForMember(s => s.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
                 .ForMember(s => s.Quantity, opt => opt.MapFrom(src => src.Quantity))
             .ReverseMap();

            CreateMap<SaleViewModel, Sale>()
                .ForMember(s => s.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(s => s.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
                .ForMember(s => s.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(d => d.Itens, opt => opt.MapFrom(src => src.Itens))
            .ReverseMap();

            CreateMap<SaleItemViewModel, SaleItem>()
                 .ForMember(s => s.Id, opt => opt.MapFrom(src => src.Id))
                 .ForMember(s => s.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
                 .ForMember(s => s.Quantity, opt => opt.MapFrom(src => src.Quantity))
             .ReverseMap();

            CreateMap<ProductViewModel, Product>()
                 .ForMember(s => s.Id, opt => opt.MapFrom(src => src.Id))
                 .ForMember(s => s.Classification, opt => opt.MapFrom(src => src.Classification))
                 .ForMember(s => s.AlcooholContent, opt => opt.MapFrom(src => src.AlcooholContent))
                 .ForMember(s => s.Price, opt => opt.MapFrom(src => src.Price))
                 .ForMember(s => s.Name, opt => opt.MapFrom(src => src.Name))
                 .ForMember(s => s.Label, opt => opt.MapFrom(src => src.Label))
            .ReverseMap();

            CreateMap<CustomerSalesReport, CustomerSalesReportViewModel>()
                 .ForMember(s => s.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
                 .ForMember(s => s.CustomerName, opt => opt.MapFrom(src => src.CustomerName))
                 .ForMember(s => s.TotalSales, opt => opt.MapFrom(src => src.TotalSales))
             .ReverseMap();

            CreateMap<ReportItem, ReportItemViewModel>()
                 .ForMember(s => s.Product, opt => opt.MapFrom(src => src.Product))
                 .ForMember(s => s.QuantitySold, opt => opt.MapFrom(src => src.QuantitySold))
                 .ForMember(s => s.TotalSold, opt => opt.MapFrom(src => src.TotalSold))
             .ReverseMap();
        }
    }
}
