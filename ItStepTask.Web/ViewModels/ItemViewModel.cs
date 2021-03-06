﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ItStepTask.Web.ViewModels
{
    public class ItemViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 symbols!")]
        public string Name { get; set; }

        [Required]
        [Range(0,1000000)]
        public string Price { get; set; }

        [Required]
        [Range(0, 1000000)]
        public int Quantity { get; set; }

        public string Image { get; set; }

        public int Discount { get; set; }
    }
}