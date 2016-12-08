using ItStepTask.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ItStepTask.Web.ViewModels
{
    public class ApplicationUserViewModel
    {
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 symbols!")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }    
    }
}