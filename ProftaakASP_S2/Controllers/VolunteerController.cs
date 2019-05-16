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
        private readonly ReactionLogic _reactionLogic;
        private readonly ChatLogic _chatLogic;
        private readonly AppointmentLogic _appointmentLogic;
        

        public VolunteerController(QuestionLogic questionLogic, UserLogic userLogic, ReactionLogic reactionLogic, ChatLogic chatLogic, AppointmentLogic appointmentLogic)
        {
            _questionLogic = questionLogic;
            _userLogic = userLogic;
            _reactionLogic = reactionLogic;
            _chatLogic = chatLogic;
            _appointmentLogic = appointmentLogic;
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

        // GET: QuestionVolunteer/Create
        public ActionResult ReactionCreate(QuestionViewModel questionViewModel)
        {
            return View("../Volunteer/Question/ReactionCreate", new ReactionViewModel(questionViewModel.QuestionId, questionViewModel.Title, questionViewModel.CareRecipientName));
        }

        // POST: QuestionVolunteer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReactionCreate(ReactionViewModel reactionViewModel)
        {
            try
            {
                int questionID = reactionViewModel.QuestionId;
                string description = reactionViewModel.Description;

                //TODO
                int senderid = Convert.ToInt32(Request.Cookies["id"]);
                
                _reactionLogic.PostReaction(new Reaction(questionID, senderid, description));

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

        public ActionResult ChatOverview()
        {
            List<ChatViewModel> chatView = new List<ChatViewModel>();
            foreach (ChatLog chatLog in _chatLogic.GetAllOpenChatsWithVolunteerID(Convert.ToInt32(Request.Cookies["id"])))
            {
                chatView.Add(new ChatViewModel(chatLog));
            }
            return View("../Volunteer/Chat/Overview", chatView);
        }



        [HttpGet]
        public ActionResult CreateAppointment(int careRecipientId, int questionId, int volunteerId)
        {
            ViewBag.Firstname = _userLogic.GetUserById(careRecipientId).FirstName;

            return View("Chat/CreateAppointment");
        }

 
        public ActionResult CreateAppointment(AppointmentViewModel appointmentView)
        {
            _appointmentLogic.CreateAppointment(new Appointment(appointmentView.QuestionId, appointmentView.CareRecipientId, appointmentView.VolunteerId, appointmentView.TimeStamp));
            return RedirectToAction("ChatOverview");
        }

        public ActionResult OpenChat(int id, string volunteerName, string careRecipientName, int careRecipientId)
        {
            List<MessageViewModel> messageView = new List<MessageViewModel>();
            MessageViewModel2 messageView2 = new MessageViewModel2(careRecipientId, Convert.ToInt32(Request.Cookies["id"]), id);

            foreach (ChatMessage cMessage in _chatLogic.LoadMessageListWithChatID(id))
            {
                messageView.Add(new MessageViewModel(cMessage, Convert.ToInt32(Request.Cookies["id"]), volunteerName, careRecipientName));
            }

            messageView2.Messages = messageView;
            return View("../Volunteer/Chat/OpenChat", messageView2);
        }

        public ActionResult NewMessage(MessageViewModel2 mvMessageViewModel2)
        {
            _chatLogic.SendMessage(mvMessageViewModel2.ChatLogId, mvMessageViewModel2.ReceiverId, mvMessageViewModel2.SenderId, mvMessageViewModel2.NewMessage);
            return RedirectToAction(nameof(ChatOverview));
        }
    }
}