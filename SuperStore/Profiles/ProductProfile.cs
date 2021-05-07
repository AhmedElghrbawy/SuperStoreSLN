using AutoMapper;
using SuperStore.Data.Models;
using SuperStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperStore.Web.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductViewModel>()
                .ForMember(dest => dest.ImageData, opt => opt.MapFrom(src => src.Image))
                .ForMember(dest => dest.InCart, opt => opt.MapFrom<ProductInCartResolver>())
                .ReverseMap();
        }

    }

    public class ProductInCartResolver : IValueResolver<Product, ProductViewModel, bool>
    {
        public bool Resolve(Product source, ProductViewModel destination, bool destMember, ResolutionContext context)
        {
            var userCart = (ShoppingCart) context.Items["cart"];

            return userCart.Items?.Any(item => item.ProductId == source.Id) ?? false;
        }
    }
}
