using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SuperStore.Web.Models
{
    public class ShoppingCartItemViewModel
    {
        public int Id { get; set; }
        public int ShoppingCartId { get; set; }
        public int ProductId { get; set; }

        [Range(minimum: 1, maximum: 1000)]
        [Display(Name = "Amount Orderd")]
        public int Amount { get; set; }

        public ProductViewModel Product { get; set; }
    }
}
