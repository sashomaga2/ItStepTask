using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace ItStepTask.Web.Areas.Managment.ViewModels
{
    public class EditDiscountViewModel
    {
        [Key]
        [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        public string Image { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 symbols!")]
        public string Name { get; set; }


        [Range(0, 1000000, ErrorMessage = "Must be must be between 0 and 1000000")]
        public decimal Price { get; set; }


        [Range(0, 1000000, ErrorMessage = "Must be must be between 0 and 1000000")]
        public int Quantity { get; set; }

        
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 symbols!")]
        public string CategoryName { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 symbols!")]
        public string SupplierName { get; set; }

        [Range(0, 90, ErrorMessage = "Must be in range 0 - 90%")]
        public int Discount { get; set; }
    }
}