using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItStepTask.Entity
{
    public class Discount : BaseModel
    {
        [Range(0, 100)]
        public int Rate { get; set; }
    }
}
