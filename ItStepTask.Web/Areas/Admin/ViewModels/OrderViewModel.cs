using ItStepTask.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ItStepTask.Web.Areas.Admin.ViewModels
{
    // TODO add user address for delivary
    public class OrderViewModel
    {
        public int Id { get; set; }

        public string Image { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public int OrderAmount { get; set; }

        public decimal Total { get; set; }

        public string CustomerNumber { get; set; }

        public string CustomerEmail { get; set; }

        //public IEnumerable<SelectListItem> StatusSelectListItems { get; set; }

        public int LastStatusSelected { get; set; }

        public OrderStatus StatusId { get; set; }

        //[Required]
        //public virtual string StatusId
        //{
        //    get
        //    {
        //        return ((int)this.StatusType).ToString();
        //    }
        //    set
        //    {
        //        StatusType = (OrderStatus)Int32.Parse(value);
        //    }
        //}

        //[EnumDataType(typeof(OrderStatus))]
        //public OrderStatus StatusType { get; set; }
    }
}