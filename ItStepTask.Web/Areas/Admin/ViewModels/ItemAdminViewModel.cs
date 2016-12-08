using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ItStepTask.Web.Areas.Admin.ViewModels
{
    public class ItemAdminViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must be between 3 and 50 symbols!")]
        public string Name { get; set; }

        [Required]
        [Range(0, 10000)]
        public decimal Price { get; set; }

        [Required]
        [Range(0, 10000)]
        public int Quantity { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must be between 3 and 50 symbols!")]
        public string CategoryName { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must be between 3 and 50 symbols!")]
        public string SupplierName { get; set; }

        public byte[] Image { get; set; }

    }
}