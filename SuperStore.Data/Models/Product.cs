using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperStore.Data.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }

        [Required]
        [Range(Double.Epsilon, 10000000000.00)]
        [Column(TypeName = "decimal(10,2)")]
        public Decimal Price { get; set; }


        public byte[] Image { get; set; }
        public int AmountAvailable { get; set; }
        public int ViewCount { get; set; }

        public int OwnerId { get; set; }
        public int CategoryId { get; set; }

        public User Owner { get; set; }
        public Category Category { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
