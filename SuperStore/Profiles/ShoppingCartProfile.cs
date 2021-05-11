using AutoMapper;
using SuperStore.Data.Models;
using SuperStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperStore.Web.Profiles
{
    public class ShoppingCartProfile : Profile
    {

        public ShoppingCartProfile()
        {
            CreateMap<ShoppingCart, ShoppingCartViewModel>();
        }

        
    }
}
