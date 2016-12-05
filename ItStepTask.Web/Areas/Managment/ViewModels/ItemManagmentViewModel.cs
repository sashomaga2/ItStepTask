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

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string CategoryName { get; set; }

        public string SupplierName { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:P2}")]
        public string Discount { get; set; }
    }
}