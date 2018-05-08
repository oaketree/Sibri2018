using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sibri.WebPage.ViewComponents
{
    public class BottomViewComponent: ViewComponent
    {
        public IViewComponentResult InvokeAsync()
        {
            return View();
        }
    }
}
