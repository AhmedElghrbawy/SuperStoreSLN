using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperStore.Data.Models
{
    public class CartNotification
    {
        public int Id { get; set; }
        public int CartId { get; set; }

        public string Text { get; set; }

        public ShoppingCart Cart { get; set; }
    }
}
