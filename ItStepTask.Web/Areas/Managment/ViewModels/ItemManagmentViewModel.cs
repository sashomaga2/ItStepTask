using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ItStepTask.Web.Areas.Managment.ViewModels
{
    public class ItemManagmentViewModel
    {
        [Key]
        public int Id { get; set; }

        public string Image { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 symbols!")]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 symbols!")]
        public string CategoryName { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 symbols!")]
        public string SupplierName { get; set; }

        [StringLength(5, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 5 symbols!")]
        public string Discount { get; set; }
    }
}