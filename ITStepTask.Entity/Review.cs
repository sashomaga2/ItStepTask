using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItStepTask.Entity
{
    public class Review : BaseModel
    {
        public string Content { get; set; }

        public virtual ApplicationUser Author { get; set; }
        
        public virtual Item Item { get; set; }
    }
}
