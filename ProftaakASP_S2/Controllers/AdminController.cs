using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Logic;
using ProftaakASP_S2.Models;
using Models;

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
            List<ChatViewModel> chatView = new List<ChatViewModel>();
            foreach (ChatLog chatLog in _chatLogic.GetAllChatLogs())
            {
                chatView.Add(new ChatViewModel(chatLog));
            }

            return View("../Admin/ChatLogOverview", chatView);
        }

        public ActionResult ChatLogDelete(ChatViewModel chat)
        {
            try
            {
                _chatLogic.DeleteMessagesFromDatabase(new ChatLog(chat.ChatLogID));
                _chatLogic.DeleteChatLogFromDatabase(new ChatLog(chat.ChatLogID));

                return RedirectToAction("ChatLogOverview");
            }
            catch
            {
                return View("../Shared/Error");
            }
        }

        public ActionResult QuestionOverview()
        {
            List<QuestionViewModel> questionView = new List<QuestionViewModel>();
            foreach (Question question in _questionLogic.GetAllQuestions())
            {
                questionView.Add(new QuestionViewModel(question, _userLogic.GetUserById(question.CareRecipientId)));
            }

            return View("../Admin/QuestionOverview", questionView);
        }

        public ActionResult QuestionDelete(QuestionViewModel question)
        {
            try
            {
                _questionLogic.DeleteQuestionFromDatabase(new Question(question.QuestionId));

                return RedirectToAction("QuestionOverview");
            }
            catch
            {
                return View("../Shared/Error");
            }
        }

        public ActionResult UserOverview()
        {
            return View();
        }
    }
}