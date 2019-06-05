using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using ProftaakASP_S2.Models;

namespace ProftaakASP_S2.Controllers
{
    public class ReviewController : Controller
    {
        private readonly QuestionLogic _questionLogic;
        private readonly CategoryLogic _categoryLogic;
        private readonly ReactionLogic _reactionLogic;
        private readonly UserLogic _userLogic;
        private readonly ChatLogic _chatLogic;

        public ReviewController(QuestionLogic questionLogic, CategoryLogic categoryLogic, ReactionLogic reactionLogic, UserLogic userLogic, ChatLogic chatLogic)
        {
            _questionLogic = questionLogic;
            _categoryLogic = categoryLogic;
            _reactionLogic = reactionLogic;
            _userLogic = userLogic;
            _chatLogic = chatLogic;
        }

        public IActionResult Index()
        {
            ReviewViewModel model = new ReviewViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(ReviewViewModel model)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cancel(int id)
        {
            List<ReactionViewModel> reactionViews = new List<ReactionViewModel>();

            if (_reactionLogic.GetAllCommentsWithQuestionId(id).Count > 0)
            {

                foreach (Reaction reaction in _reactionLogic.GetAllCommentsWithQuestionId(id))
                {
                    reactionViews.Add(new ReactionViewModel(reaction, _questionLogic.GetSingleQuestion(reaction.QuestionId),
                        _userLogic.GetUserById(Convert.ToInt32(Request.Cookies["id"]))));
                }

                ViewBag.Message = null;

                return View("Reaction/Overview", reactionViews);
            }
            TempData["ErrorMessage"] = "Vraag heeft geen reacties";
            return View("Reaction/Overview", reactionViews);
        }
    }
}