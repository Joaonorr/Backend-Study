using AutoMapper;
using WebApplication1.Models;

namespace WebApplication1.DTOs.Mappings;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
        CreateMap<Product, ProductDTO>().ReverseMap();
        CreateMap<Category, CategoryDTO>().ReverseMap();  
    }    
}
