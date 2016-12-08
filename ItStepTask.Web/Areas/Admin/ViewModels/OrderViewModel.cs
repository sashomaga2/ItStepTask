using ItStepTask.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ItStepTask.Web.Areas.Admin.ViewModels
{
    // TODO add user address for delivary
    public class OrderViewModel
    {
        [Key]
        public int Id { get; set; }

        public string Image { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must be between 3 and 50 symbols!")]
        public string Name { get; set; }

        [Required]
        [Range(0, 10000)]
        public string Price { get; set; }

        [Required]
        [Range(0, 10000)]
        public int Quantity { get; set; }

        [Required]
        [Range(0, 10000)]
        public int OrderAmount { get; set; }

        [Required]
        [Range(0, 10000)]
        public decimal Total { get; set; }

        [Required]
        public string Discount { get; set; }
    }
}