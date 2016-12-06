using ItStepTask.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ItStepTask.Web.Models
{
    public class HomeViewModel : LayoutViewModel
    {
        public ItemViewModel Item { get; set; }
    }
}