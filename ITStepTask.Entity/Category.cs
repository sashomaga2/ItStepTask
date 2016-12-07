using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItStepTask.Entity
{
    public class Category : BaseModel
    {
        public Category()
        {
            Items = new HashSet<Item>();
        }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Invalid")]
        public string Name { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}
