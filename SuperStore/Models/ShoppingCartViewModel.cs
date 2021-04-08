﻿using System;
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
        public Decimal TotalPrice { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}
