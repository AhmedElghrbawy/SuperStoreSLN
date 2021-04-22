using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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


        [Required(ErrorMessage ="You need to give your feedback")]
        [Display(Name ="What's Good About This Product?")]
        public string Good { get; set; }


        [Required(ErrorMessage = "You need to give your feedback")]
        [Display(Name = "What's Bad About This Product?")]
        public string Bad { get; set; }


        [Required(ErrorMessage = "Please Rate This Product")]
        [Range(1, 5)]
        [Display(Name ="How do you rate this product?")]
        public int Stars { get; set; }


        public User Owner { get; set; }
    }
}
