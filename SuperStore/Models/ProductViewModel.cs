﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using SuperStore.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SuperStore.Web.Models
{
    public class ProductViewModel
    {

        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }

        [Required]
        [Range(Double.Epsilon, 10000000000.00)]
        public Decimal Price { get; set; }

        [Required]
        public IFormFile ImageFormFile
        {
            set
            {
                if (value == null)
                    return;
                using (var ms = new MemoryStream())
                {
                    value.CopyTo(ms);
                    ImageData = ms.ToArray();
                }
                
            }
        }

        public byte[] ImageData { get; set; }


        [Range(minimum:1, maximum: 1000)]
        public int AmountAvailable { get; set; }

        public int ViewCount { get; }

        public int OwnerId { get; set; }



        [Required]
        public int CategoryId { get; set; }


        public string ImageUrl
        {
            get
            {
                if (ImageData == null)
                    return "";
                string imageBase64Data = Convert.ToBase64String(ImageData);
                return string.Format("data:image/jpeg;base64,{0}", imageBase64Data);
            }
        }

        public SelectList CategorySelectList { get; set; }

        public User Owner { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
