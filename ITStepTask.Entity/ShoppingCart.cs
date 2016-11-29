using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItStepTask.Entity
{
    public class ShoppingCart : BaseModel
    {
        public virtual Item Item { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
