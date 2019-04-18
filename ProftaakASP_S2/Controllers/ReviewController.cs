using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProftaakASP_S2.Models;

namespace ProftaakASP_S2.Controllers
{
    public class ReviewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(ReviewViewModel model)
        {
            return View();
        }

        public IActionResult Cancel()
        {
            //TODO change to CareRecipient page
            return RedirectToAction("", "");
        }
    }
}