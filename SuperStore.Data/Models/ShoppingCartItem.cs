using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperStore.Data.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }
        public int ShoppingCartId { get; set; }
        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
