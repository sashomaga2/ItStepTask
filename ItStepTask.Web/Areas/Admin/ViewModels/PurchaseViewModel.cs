using ItStepTask.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ItStepTask.Web.Areas.Admin.ViewModels
{
    public class PurchaseViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Phone]
        public string CustomerNumber { get; set; }

        [Required]
        [EmailAddress]
        public string CustomerEmail { get; set; }

        [Required]
        public int LastStatusSelected { get; set; }

        [Required]
        [Range(0,100000000)]
        public decimal Total { get; set; }

        [Required]
        public string CreatedOn { get; set; }

        [Required]
        public OrderStatus StatusId { get; set; }

        public IEnumerable<OrderViewModel> Orders { get; set; }
    }
}