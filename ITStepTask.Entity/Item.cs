using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItStepTask.Entity
{
    public class Item : BaseModel
    {
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Invalid")]
        public string Name { get; set; }

        [Required]
        [Range(0, 100000)]
        public decimal Price { get; set; }

        [Required]
        [Range(0, 100000)]
        public int Quantity { get; set; }

        [Column(TypeName = "image")]
        public byte[] Image { get; set; }

        //[ForeignKey("WarrantyId ")]
        public virtual Category Category { get; set; }

        //[ForeignKey("WarrantyId ")]
        public virtual Supplier Supplier { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public virtual Discount Discount { get; set; } 
    }
}
