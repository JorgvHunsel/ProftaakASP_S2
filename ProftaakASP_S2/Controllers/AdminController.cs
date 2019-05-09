using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Logic;

namespace ProftaakASP_S2.Controllers
{
    public class AdminController : Controller
    {

        private readonly QuestionLogic _questionLogic;
        private readonly CategoryLogic _categoryLogic;
        private readonly ReactionLogic _reactionLogic;
        private readonly UserLogic _userLogic;
        private readonly ChatLogic _chatLogic;

        public AdminController(QuestionLogic questionLogic, CategoryLogic categoryLogic, ReactionLogic reactionLogic, UserLogic userLogic, ChatLogic chatLogic)
        {
            _questionLogic = questionLogic;
            _categoryLogic = categoryLogic;
            _reactionLogic = reactionLogic;
            _userLogic = userLogic;
            _chatLogic = chatLogic;
        }

        public ActionResult ChatLogOverview()
        {
            return View();
        }

        public ActionResult QuestionOverview()
        {
            return View();
        }

        public ActionResult UserOverview()
        {
            return View();
        }
    }
}