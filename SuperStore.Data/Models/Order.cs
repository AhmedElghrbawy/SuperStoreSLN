using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperStore.Data.Models
{
    public class Order
    {

        public int Id { get; set; }

        public int OwnerId { get; set; }

        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Date { get; set; }

        public User Owner { get; set; }
        public IEnumerable<OrderItem> Items { get; set; }


        [NotMapped]
        [DataType(DataType.Currency)]
        public Decimal TotalPrice { get { return Items.Sum(item => item.Amount * item.Product.Price); } }
    }
}
