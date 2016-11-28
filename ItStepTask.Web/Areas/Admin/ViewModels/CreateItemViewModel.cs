using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ItStepTask.Web.Areas.Admin.ViewModels
{
    public class CreateItemViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public IEnumerable<SelectListItem> SuppliersSelectListItems { get; set; }

        public int SupplierId { get; set; }

        public IEnumerable<SelectListItem> CategoriesSelectListItems { get; set; }

        public int CategoryId { get; set; }
    }
}