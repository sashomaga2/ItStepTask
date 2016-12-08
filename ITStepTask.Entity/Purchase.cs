using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItStepTask.Entity
{
    public enum OrderStatus { New, Hold, Shipped, Delivered, Closed }

    public class Purchase : BaseModel
    {
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        [Required]
        public virtual int StatusId
        {
            get
            {
                return (int)this.StatusType;
            }
            set
            {
                StatusType = (OrderStatus)value;
            }
        }

        [EnumDataType(typeof(OrderStatus))]
        public OrderStatus StatusType { get; set; }
    }
}
