﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperStore.Data.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }

        [Range(minimum: 1, maximum: 1000)]
        public int Amount { get; set; }


        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
