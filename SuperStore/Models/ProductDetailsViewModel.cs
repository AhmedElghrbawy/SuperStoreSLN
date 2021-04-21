using SuperStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperStore.Web.Models
{
    public class ProductDetailsViewModel
    {
        public ProductViewModel Product { get; set; }
        public Review Review { get; set; }
    }
}
