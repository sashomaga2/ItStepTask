using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ItStepTask.Web.Areas.Admin.ViewModels
{
    public class SupplierViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Required!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must be between 3 and 50 symbols!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Required!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must be between 3 and 50 symbols!")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Required!")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Required!")]
        [Phone]
        public string Phone { get; set; }
    }
}