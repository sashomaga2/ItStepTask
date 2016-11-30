using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ItStepTask.Web.Models
{
    public class ItemViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        //public byte[] Image { get; set; }

        public string Image { get; set; } //{ return Image != null ? Convert.ToBase64String(Image) : null; }
    }
}