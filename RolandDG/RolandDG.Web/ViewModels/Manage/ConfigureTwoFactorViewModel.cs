using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Roland_ASP_MVC.ViewModels.Manage
{
    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
}