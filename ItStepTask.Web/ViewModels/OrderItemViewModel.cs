using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ItStepTask.Web.ViewModels
{
    public class OrderItemViewModel : LayoutViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; }

        [Required]
        [Range(0, 100)]
        public int Quantity { get; set; }

        public int? DiscountedPrice { get; set; }

        public int OrderAmount { get; set; }

        public string Image { get; set; }
    }
}