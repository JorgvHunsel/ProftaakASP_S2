using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Contexts;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Models;
using ProftaakASP_S2.Models;

namespace ProftaakASP_S2.Controllers
{
    public class ReviewController : Controller
    {
        private ReviewLogic reviewLogic = new ReviewLogic(new ReviewContextSQL());
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(ReviewViewModel model)
        {
            if (ModelState.IsValid)
            {
                ReviewInfo review = new ReviewInfo(model.ReviewId, model.VolunteerId, model.CareRecipientId, model.Review, model.starAmount);
                reviewLogic.InsertReview(review);
            }

            ViewBag.NotFilled = "Er moet een hoeveelheid sterren zijn ingevuld.";
            return View();
        }

        public IActionResult Cancel()
        {
            //TODO change to CareRecipient page
            return RedirectToAction("", "");
        }
    }
}