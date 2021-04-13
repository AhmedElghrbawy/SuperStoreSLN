using SuperStore.Data.Models;
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
        public Decimal TotalPrice { get { return Items.Sum(item => item.Product.Price * item.Amount); } }


        public IEnumerable<ShoppingCartItemViewModel> Items { get; set; }
        public IEnumerable<CartNotification> Notifications { get; set; }
    }
}
