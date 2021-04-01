using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperStore.Data.Models
{
    public class User : IdentityUser<int>
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string  LastName { get; set; }

        [NotMapped]
        public string FullName { get { return FirstName + " " + LastName; } }

        [Phone]
        public string Phone { get; set; }

        public string City { get; set; }

        public List<Product> OwnedProducts { get; set; }
        public List<Order> OrdersMade { get; set; }
        public ShoppingCart ShoppinngCart { get; set; }
    }

    public class ApplicationRole : IdentityRole<int>
    {
    }
}
