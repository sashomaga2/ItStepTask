using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItStepTask.Entity
{
    public class UserData : BaseModel
    {
        public virtual Category SelectedCategory { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
