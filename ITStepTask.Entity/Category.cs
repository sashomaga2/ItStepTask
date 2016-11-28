using System;
using System.Collections.Generic;
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
        public string Name { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}
