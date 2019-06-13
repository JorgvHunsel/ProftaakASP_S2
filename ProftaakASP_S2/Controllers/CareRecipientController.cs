using Logic;
using Microsoft.AspNetCore.Mvc;
using Models;
using ProftaakASP_S2.Models;
using System;
using System.Collections.Generic;

namespace ProftaakASP_S2.Controllers
{
    public class CareRecipientController : Controller
    {
        private readonly QuestionLogic _questionLogic;
        private readonly CategoryLogic _categoryLogic;
        private readonly ReactionLogic _reactionLogic;
        private readonly UserLogic _userLogic;
        private readonly ChatLogic _chatLogic;
        private readonly AppointmentLogic _appointmentLogic;


        public CareRecipientController(QuestionLogic questionLogic, CategoryLogic categoryLogic, ReactionLogic reactionLogic, UserLogic userLogic, ChatLogic chatLogic, AppointmentLogic appointmentLogic)
        {
            _questionLogic = questionLogic;
            _categoryLogic = categoryLogic;
            _reactionLogic = reactionLogic;
            _userLogic = userLogic;
            _chatLogic = chatLogic;
            _appointmentLogic = appointmentLogic;
        }

        public ActionResult Overview()
        {
            ViewBag.Message = TempData["ErrorMessage"] as string;

            List<QuestionViewModel> questionView = new List<QuestionViewModel>();
            foreach (Question question in _questionLogic.GetAllOpenQuestionCareRecipientID(Convert.ToInt32(Request.Cookies["id"])))
            {
                questionView.Add(new QuestionViewModel(question));
            }

            return View("../CareRecipient/Question/Overview", questionView);
        }

        public ActionResult OverviewClosed()
        {
            List<QuestionViewModel> questionView = new List<QuestionViewModel>();
            foreach (Question question in _questionLogic.GetAllClosedQuestionsCareRecipientId(Convert.ToInt32(Request.Cookies["id"])))
            {
                questionView.Add(new QuestionViewModel(question));
            }

            return View("../CareRecipient/Question/OverviewClosed", questionView);
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

        [HttpGet]
        public ActionResult ReactionOverview(int id)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Sid).Value);

            List<ReactionViewModel> reactionViews = new List<ReactionViewModel>();

            if (_reactionLogic.GetAllCommentsWithQuestionId(id).Count > 0)
            {

                foreach (Reaction reaction in _reactionLogic.GetAllCommentsWithQuestionId(id))
                {
                    reactionViews.Add(new ReactionViewModel(reaction, _questionLogic.GetSingleQuestion(reaction.QuestionId),
                        _userLogic.GetUserById(userId)));
                }

                ViewBag.Message = null;

                return View("Reaction/Overview", reactionViews);
            }

            TempData["ErrorMessage"] = "Vraag heeft geen reacties";
            return RedirectToAction("Overviewclosed");
        }

        [HttpGet]
        public ActionResult ReactionOverviewClosed(int id)
        {

                ViewBag.Message = null;


            TempData["ErrorMessage"] = "Vraag heeft geen reacties";
            return RedirectToAction("Overview");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReactionOverview(int id, ReactionViewModel reaction)
        {
            try
            {
                return RedirectToAction("ReactionOverview");
            }
            catch
            {
                return RedirectToAction("Edit");
            }
        }

        public ActionResult Create()
        {
            ViewData["Categories"] = _categoryLogic.GetAllCategories();

            return View("../CareRecipient/Question/Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(QuestionViewModel question)
        {
            try
            {
                int userid = Convert.ToInt32(Request.Cookies["id"]);
                _questionLogic.WriteQuestionToDatabase(new Question(question.Title, question.Content, Question.QuestionStatus.Open, question.Urgency, question.CategoryId, userid));

                return RedirectToAction(nameof(Overview));
            }
            catch (Exception)
            {
                return View("../Shared/Error");
            }
        }

        public ActionResult ChangeStatus(int id, string status, string path)
        {
            string[] redirectUrl = path.Split("/");

            _questionLogic.ChangeStatus(id, status);
            if (redirectUrl[2] == "Overview")
            {
                return RedirectToAction(nameof(Overview));
            }

            return RedirectToAction(nameof(OverviewClosed));
        }

        public ActionResult CreateChat(int reactionId, int volunteerId)
        {
            int id = _chatLogic.CreateNewChatLog(reactionId, volunteerId, Convert.ToInt32(Request.Cookies["id"]));
            if (id != 0)
            {
                return RedirectToAction("OpenChat", new { id });
            }

            return RedirectToAction(nameof(Overview));
            ReviewViewModel reviewViewModel = new ReviewViewModel(volunteerId, questionId);

            return View("../Review/Index");

        }

        public ActionResult ChatOverview()
        {
            List<ChatViewModel> chatView = new List<ChatViewModel>();
            foreach (ChatLog chatLog in _chatLogic.GetAllOpenChatsByDate(Convert.ToInt32(Request.Cookies["id"])))
            {
                chatView.Add(new ChatViewModel(chatLog));
            }

            return View("../CareRecipient/Chat/Overview", chatView);
        }

        public ActionResult OpenChat(int id, string volunteerName, string careRecipientName, int volunteerId)
        {
            List<MessageViewModel> messageView = new List<MessageViewModel>();

            MessageViewModel2 messageView2 = new MessageViewModel2(volunteerId, Convert.ToInt32(Request.Cookies["id"]), id, _chatLogic.GetSingleChatLog(id).Status);

            foreach (ChatMessage cMessage in _chatLogic.LoadMessageListWithChatId(id))
            {
                messageView.Add(new MessageViewModel(cMessage, Convert.ToInt32(Request.Cookies["id"]), volunteerName, careRecipientName));
            }

            messageView2.Messages = messageView;
            return View("../CareRecipient/Chat/OpenChat", messageView2);
        }


        public ActionResult NewMessage(MessageViewModel2 mvMessageViewModel2)
        {
            _chatLogic.SendMessage(mvMessageViewModel2.ChatLogId, mvMessageViewModel2.ReceiverId, mvMessageViewModel2.SenderId, mvMessageViewModel2.NewMessage);
            return RedirectToAction(nameof(ChatOverview));
        }


        public ActionResult ChangeStatusChat(int chatlogId)
        {
            ChatLog chatLog = _chatLogic.GetSingleChatLog(chatlogId);

            chatLog.Status = !chatLog.Status;

            _chatLogic.ChangeChatStatus(chatLog);

            _appointmentLogic.DeleteAppointmentByChat(chatlogId);

            return RedirectToAction("ChatOverview");
        }

    }
}