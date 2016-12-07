using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItStepTask.Entity
{
    public class Supplier : BaseModel
    {
        public Supplier()
        {
            Items = new HashSet<Item>();
        }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Invalid")]
        public string Name { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Invalid")]
        public string Address { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}
