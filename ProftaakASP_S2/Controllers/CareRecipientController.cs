using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using ProftaakASP_S2.Models;

namespace ProftaakASP_S2.Controllers
{
    public class CareRecipientController : Controller
    {
        private readonly QuestionLogic _questionLogic;
        private readonly CategoryLogic _categoryLogic;

        public CareRecipientController(QuestionLogic questionLogic, CategoryLogic categoryLogic)
        {
            _questionLogic = questionLogic;
            _categoryLogic = categoryLogic;
        }

        public ActionResult Overview()
        {
            List<QuestionViewModel> questionView = new List<QuestionViewModel>();
            foreach (Question question in _questionLogic.GetAllOpenQuestionCareRecipientID(Convert.ToInt32(Request.Cookies["id"])))
            {
                questionView.Add(new QuestionViewModel(question));
            }

            return View("../CareRecipient/Question/Overview", questionView);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewData["Categories"] = _categoryLogic.GetAllCategories();
            return View("../CareRecipient/Question/Edit", new QuestionViewModel(_questionLogic.GetSingleQuestion(id)));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, QuestionViewModel question)
        {
            try
            {
                _questionLogic.EditQuestion(new Question(id, question.Title, question.Content, Question.QuestionStatus.Open, question.Date, question.Urgency, new Category(question.CategoryId), question.CareRecipientId));

                return RedirectToAction("Overview");
            }
            catch
            {
                return RedirectToAction("Edit");
            }
        }

        // GET: CareRecipient/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CareRecipient/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CareRecipient/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Overview));
            }
            catch
            {
                return View();
            }
        }

        

        

        // GET: CareRecipient/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CareRecipient/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Overview));
            }
            catch
            {
                return View();
            }
        }
    }
}