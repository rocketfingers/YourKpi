using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouKpiBackend.Controllers
{
    public class PartsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
