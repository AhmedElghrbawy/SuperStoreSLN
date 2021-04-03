using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperStore.Data.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        [Required]
        public int OwnerId { get; set; }

        public List<ShoppingCartItem> Items { get; set; }

        [NotMapped]
        public Decimal TotalPrice { get { return Items.Sum(item => item.Product.Price); } }

    }
}
