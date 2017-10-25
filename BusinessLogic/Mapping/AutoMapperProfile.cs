using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.Logic;
using Infrastructure.Entities;

namespace BusinessLogic.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<OrderEntity, DtoOrder>()
                .ForMember(dest => dest.Details, opts => opts.MapFrom(src => new List<DtoOrderDetails>()));

            CreateMap<OrderDetailsEntity, DtoOrderDetails>()
                .ForMember(dest => dest.Code, opts => opts.MapFrom(src => src.Product.Code))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Description, opts => opts.MapFrom(src => src.Product.Description))
                .ForMember(dest => dest.Price, opts => opts.MapFrom(src => src.Product.Price))
                .ForMember(dest => dest.Quantity, opts => opts.MapFrom(src => src.ProductQuantity));
        }
    }
}
