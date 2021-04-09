using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SuperStore.Web.Models
{
    public class ShoppingCartViewModel
    {
        public int Id { get; set; }
        [Required]
        public int OwnerId { get; set; }

        [Range(Double.Epsilon, 10000000000.00)]
        [DataType(DataType.Currency)]
        public Decimal TotalPrice { get { return Products.Sum(item => item.Price); } }


        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}
