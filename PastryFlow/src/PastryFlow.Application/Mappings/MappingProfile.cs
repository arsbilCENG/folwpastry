using AutoMapper;
using PastryFlow.Application.DTOs.Branch;
using PastryFlow.Application.DTOs.Category;
using PastryFlow.Application.DTOs.Demand;
using PastryFlow.Application.DTOs.Product;
using PastryFlow.Application.DTOs.Waste;
using PastryFlow.Domain.Entities;

namespace PastryFlow.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Branch, BranchDto>();
        CreateMap<Category, CategoryWithProductsDto>();
        CreateMap<Product, ProductDto>()
            .ForMember(d => d.CategoryName, o => o.MapFrom(s => s.Category.Name))
            .ForMember(d => d.ProductionBranchName, o => o.MapFrom(s => s.ProductionBranch != null ? s.ProductionBranch.Name : null));
        
        CreateMap<Demand, DemandDto>()
            .ForMember(d => d.SalesBranchName, o => o.MapFrom(s => s.SalesBranch.Name))
            .ForMember(d => d.ProductionBranchName, o => o.MapFrom(s => s.ProductionBranch.Name));
            
        CreateMap<DemandItem, DemandItemDto>()
            .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.Name))
            .ForMember(d => d.Unit, o => o.MapFrom(s => s.Product.Unit));
            
        CreateMap<Waste, WasteDto>()
            .ForMember(d => d.BranchName, o => o.MapFrom(s => s.Branch.Name))
            .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.Name));
    }
}
