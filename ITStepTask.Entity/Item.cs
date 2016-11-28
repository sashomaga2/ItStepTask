using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItStepTask.Entity
{
    public class Item : BaseModel
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        //[ForeignKey("WarrantyId ")]
        public virtual Category Category { get; set; }

        //[ForeignKey("WarrantyId ")]
        public virtual Supplier Supplier { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}
