using System;
using System.Collections.Generic;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Models;
using ProftaakASP_S2.Models;

namespace ProftaakASP_S2.Controllers
{
    public class ProfessionalController : Controller
    {
        private readonly QuestionLogic _questionLogic;
        private readonly UserLogic _userLogic;

        public ProfessionalController(QuestionLogic questionLogic, UserLogic userLogic)
        {
            _questionLogic = questionLogic;
            _userLogic = userLogic;
        }

        public ActionResult QuestionOverview()
        {
            List<QuestionViewModel> questionView = new List<QuestionViewModel>();
            string status = "Open";

            List<Question> questionList =
                _questionLogic.GetAllQuestionsProfessional(Convert.ToInt32(Request.Cookies["id"]), status);
            foreach (Question question in questionList)
            {
                questionView.Add(new QuestionViewModel(question, _userLogic.GetUserById(question.CareRecipientId)));
            }

            return View("../Professional/Question/Overview", questionView);
        }
    }
}