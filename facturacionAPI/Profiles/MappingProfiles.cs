using AutoMapper;
using Domain.Entities;
using facturacionAPI.Dtos;

namespace ApiJwt.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
        {
            CreateMap<Product,ProductDto>()
            .ReverseMap();

            CreateMap<Customer, CustomerDto>()
            .ReverseMap();

            CreateMap<Customer, CustomerDto>()
            .ReverseMap();

             CreateMap<Invoice, InvoiceDto>()
            .ReverseMap();

             CreateMap<InvoiceDetail, InvoiceDetailDto>()
            .ReverseMap();

      

      

    }
}