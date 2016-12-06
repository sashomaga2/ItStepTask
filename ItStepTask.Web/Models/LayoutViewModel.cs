using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ItStepTask.Web.Models
{
    public abstract class LayoutViewModel
    {
        public int ShoppingCartItemsCount { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
        public int? SelectedCategoryId { get; set; }
    }
}