using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
        private readonly LogLogic _logLogic;

        public AdminController(QuestionLogic questionLogic, CategoryLogic categoryLogic, ReactionLogic reactionLogic, UserLogic userLogic, ChatLogic chatLogic, LogLogic logLogic)
        {
            _questionLogic = questionLogic;
            _categoryLogic = categoryLogic;
            _reactionLogic = reactionLogic;
            _userLogic = userLogic;
            _chatLogic = chatLogic;
            _logLogic = logLogic;
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
            List<QuestionViewModel> questionViews = new List<QuestionViewModel>();
            foreach (Question question in _questionLogic.GetAllQuestions())
            {
                questionViews.Add(new QuestionViewModel(question, _userLogic.GetUserById(question.CareRecipientId)));
            }

            return View("../Admin/QuestionOverview", questionViews);
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


        [HttpGet]
        public ActionResult UserOverview()
        {
            List<UserViewModel> userViewList = new List<UserViewModel>();
            foreach (User user in _userLogic.GetAllUsers())
            {
                userViewList.Add(new UserViewModel(user));
            }

            return View(userViewList);
        }

        public ActionResult BlockUser(int id)
        {
            User updatedUser = _userLogic.GetUserById(id);

            updatedUser.Status = !updatedUser.Status;

            _userLogic.EditUser(updatedUser, "");

            _logLogic.CreateUserLog(Convert.ToInt32(Request.Cookies["id"]), updatedUser);

            return RedirectToAction("UserOverview");
        }

        public ActionResult LogOverview()
        {
            List<LogViewModel> logList = new List<LogViewModel>();

            foreach (Log log in _logLogic.GetAllLogs())
            {
                logList.Add(new LogViewModel(log));
            }

            return View("LogOverview", logList);
        }

        [HttpGet]
        public ActionResult CreateProfessional()
        {
            return View("CreateProfessional");
        }

        [HttpPost]
        public ActionResult CreateProfessional(string emailaddress)
        {
            if (_userLogic.SendEmailProfessional(emailaddress))
            {
                return RedirectToAction("CreateProfessional");
            }
            

            return View("Error");
        }

    }
}