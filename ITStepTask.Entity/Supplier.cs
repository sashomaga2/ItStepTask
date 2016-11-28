using System;
using System.Collections.Generic;
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
        public string Name { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}
