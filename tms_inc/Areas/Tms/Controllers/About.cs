using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace tms_inc.Areas.Site.Controllers
{
    [Area("Tms")]
    public class About : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
