using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ItStepTask.Web.Areas.Admin.ViewModels
{
    public class CreateItemViewModel
    {
        [Key] 
        [Range(0, 10000, ErrorMessage = "Must be from 0 to 10000")]
        [Required]
        public int Quantity { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must be between 3 and 50 symbols!")]
        public string Name { get; set; }

        [Required]
        [Range(0, 10000, ErrorMessage = "Must be from 0 to 10000")]
        public decimal Price { get; set; }

        public IEnumerable<SelectListItem> SuppliersSelectListItems { get; set; }

        [Required]
        [DisplayName("Supplier")]
        public int SupplierId { get; set; }

        public IEnumerable<SelectListItem> CategoriesSelectListItems { get; set; }

        [Required]
        [DisplayName("Category")]
        public int CategoryId { get; set; }

        public byte[] Image { get; set; }
    }
}