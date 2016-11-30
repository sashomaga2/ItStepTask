using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItStepTask.Entity
{
    public class Order : BaseModel
    {
        public Item Item { get; set; }

        public int Quantity { get; set; }

        public ApplicationUser User { get; set; }
    }
}
