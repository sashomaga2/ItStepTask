﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ItStepTask.Web.Models
{
    public class ItemViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        public string Image { get; set; }

        public int Discount { get; set; }
    }
}