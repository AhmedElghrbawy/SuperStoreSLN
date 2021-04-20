using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperStore.Data.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int OwnerId { get; set; }
        public int ProductId { get; set; }
        public string Good { get; set; }
        public string Bad { get; set; }
        public int Stars { get; set; }


        public User Owner { get; set; }
    }
}
