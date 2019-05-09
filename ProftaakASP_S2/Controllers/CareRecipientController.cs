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
    public class CareRecipientController : Controller
    {
        private readonly QuestionLogic _questionLogic;
        private readonly CategoryLogic _categoryLogic;
        private readonly ReactionLogic _reactionLogic;
        private readonly UserLogic _userLogic;
        private readonly ChatLogic _chatLogic;

        public CareRecipientController(QuestionLogic questionLogic, CategoryLogic categoryLogic, ReactionLogic reactionLogic, UserLogic userLogic, ChatLogic chatLogic)
        {
            _questionLogic = questionLogic;
            _categoryLogic = categoryLogic;
            _reactionLogic = reactionLogic;
            _userLogic = userLogic;
            _chatLogic = chatLogic;
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

        private List<QuestionViewModel> GetListQuestionViewModel(int id)
        {
            List<QuestionViewModel> questionView = new List<QuestionViewModel>();
            foreach (Question question in _questionLogic.GetAllOpenQuestionCareRecipientID(Convert.ToInt32(Request.Cookies["id"])))
            {
                questionView.Add(new QuestionViewModel(question));
            }

            return questionView;
        }

        public ActionResult OverviewClosed()
        {
            List<QuestionViewModel> questionView = new List<QuestionViewModel>();
            foreach (Question question in _questionLogic.GetAllClosedQuestionsCareRecipientID(Convert.ToInt32(Request.Cookies["id"])))
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
            List<ReactionViewModel> reactionViews = new List<ReactionViewModel>();

            if (_reactionLogic.GetAllCommentsWithQuestionID(id).Count > 0)
            {

                foreach (Reaction reaction in _reactionLogic.GetAllCommentsWithQuestionID(id))
                {
                    reactionViews.Add(new ReactionViewModel(reaction, _questionLogic.GetSingleQuestion(reaction.QuestionId),
                        _userLogic.GetUserById(Convert.ToInt32(Request.Cookies["id"]))));
                }

                ViewBag.Message = null;

                return View("Reaction/Overview", reactionViews);
            }

            //ViewBag.Message = "Deze vraag heeft geen reacties";

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

        public ActionResult EditAccount()
        {
            List<string> userView = new List<string>();
            User currentUser = _userLogic.getCurrentUserInfo(Request.Cookies["email"]);
            userView.Add(currentUser.FirstName);
            userView.Add(currentUser.LastName);
            userView.Add(currentUser.EmailAddress);
            userView.Add(currentUser.Address);
            userView.Add(currentUser.PostalCode);
            userView.Add(currentUser.City);

            return View("../CareRecipient/Question/Overview", userView);
        }


        // GET: CareRecipient/Create
        public ActionResult Create()
        {
            ViewData["Categories"] = _categoryLogic.GetAllCategories();

            return View("../CareRecipient/Question/Create");
        }

        // POST: CareRecipient/Create
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
            catch
            {
                return View("../Shared/Error");
            }
        }
        

        public ActionResult ChangeStatus(int id, string status, string path)
        {
            string[] redirectUrl = path.Split("/");

            _questionLogic.ChangeStatus(id, status);
            if(redirectUrl[2] == "Overview")
            {
                return RedirectToAction(nameof(Overview));  
            }
            else
            {
                return RedirectToAction(nameof(OverviewClosed));
            }
        }

        
        public ActionResult CreateChat(int reactionId, int volunteerId)
        {
            int id = _chatLogic.CreateNewChatLog(reactionId, volunteerId, Convert.ToInt32(Request.Cookies["id"]));
            if(id == 0)
            {
                // error
            }
            else
            {
                return RedirectToAction("OpenChat", new {id});
            }
            return RedirectToAction(nameof(Overview));
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

        
        public ActionResult OpenChat(int id, string volunteerName, string careRecipientName)
        {
            List<MessageViewModel> messageView = new List<MessageViewModel>();
            foreach (ChatMessage cMessage in _chatLogic.LoadMessageListWithChatID(id))
            {
                messageView.Add(new MessageViewModel(cMessage, Convert.ToInt32(Request.Cookies["id"]), volunteerName, careRecipientName));
            }

            return View("../CareRecipient/Chat/OpenChat", messageView);
        }

    }
}