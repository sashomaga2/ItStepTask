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

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string CategoryName { get; set; }

        public string SupplierName { get; set; }

        public decimal Discount { get; set; }
    }
}