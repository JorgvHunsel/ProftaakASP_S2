using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Models;
using ProftaakASP_S2.Models;

namespace ProftaakASP_S2.Controllers
{
    public class VolunteerController : Controller
    {
        private readonly QuestionLogic _questionLogic;
        private readonly UserLogic _userLogic;

        public VolunteerController(QuestionLogic questionLogic, UserLogic userLogic)
        {
            _questionLogic = questionLogic;
            _userLogic = userLogic;
        }

        // GET: QuestionVolunteer
        public ActionResult QuestionOverview()
        {
            List<QuestionViewModel> questionView = new List<QuestionViewModel>();
            foreach (Question question in _questionLogic.GetAllOpenQuestions())
            {
                questionView.Add(new QuestionViewModel(question, _userLogic.GetUserById(question.CareRecipientId)));
            }

            return View("../Volunteer/Question/Overview", questionView);
        }

        // GET: QuestionVolunteer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: QuestionVolunteer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QuestionVolunteer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(QuestionOverview));
            }
            catch
            {
                return View();
            }
        }

        // GET: QuestionVolunteer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: QuestionVolunteer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(QuestionOverview));
            }
            catch
            {
                return View();
            }
        }

        // GET: QuestionVolunteer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: QuestionVolunteer/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(QuestionOverview));
            }
            catch
            {
                return View();
            }
        }
    }
}