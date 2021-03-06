﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItStepTask.Entity
{
    //public enum OrderStatus { New, Hold, Shipped, Delivered, Closed }
    public class Order : BaseModel
    {
        public virtual Item Item { get; set; }

        [Required]
        [Range(0, 1000)]
        public int OrderAmount { get; set; }
 
        public virtual ApplicationUser User { get; set; }

        public virtual Purchase Purchase { get; set; }

        //public int StatusId { get; set; }

        //public OrderStatus Status
        //{
        //    get { return (OrderStatus)StatusId; }
        //    set { StatusId = (int)value; }
        //}

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
