using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Models;
using ProftaakASP_S2.Models;

namespace ProftaakASP_S2.Controllers
{
    public class ProfessionalController : Controller
    {
        private readonly QuestionLogic _questionLogic;
        private readonly CategoryLogic _categoryLogic;
        private readonly ReactionLogic _reactionLogic;
        private readonly UserLogic _userLogic;
        private readonly ChatLogic _chatLogic;

        public ProfessionalController(QuestionLogic questionLogic, CategoryLogic categoryLogic, ReactionLogic reactionLogic, UserLogic userLogic, ChatLogic chatLogic)
        {
            _questionLogic = questionLogic;
            _categoryLogic = categoryLogic;
            _reactionLogic = reactionLogic;
            _userLogic = userLogic;
            _chatLogic = chatLogic;
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