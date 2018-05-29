using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sibri.WebPage.ViewComponents
{
    public class Bottom: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            //await Task.Delay(10);
            await Task.FromResult(0);
            return View();
        }
    }
}
