using Logic;
using Microsoft.AspNetCore.Mvc;
using Models;
using ProftaakASP_S2.Models;
using System;
using System.Collections.Generic;

namespace ProftaakASP_S2.Controllers
{
    public class VolunteerController : Controller
    {
        private readonly QuestionLogic _questionLogic;
        private readonly UserLogic _userLogic;
        private readonly ReactionLogic _reactionLogic;
        private readonly ChatLogic _chatLogic;
        private readonly AppointmentLogic _appointmentLogic;
        private readonly ReviewLogic _reviewLogic;

        public VolunteerController(QuestionLogic questionLogic, UserLogic userLogic, ReactionLogic reactionLogic, ChatLogic chatLogic, AppointmentLogic appointmentLogic, ReviewLogic reviewLogic)
        {
            _questionLogic = questionLogic;
            _userLogic = userLogic;
            _reactionLogic = reactionLogic;
            _chatLogic = chatLogic;
            _appointmentLogic = appointmentLogic;
            _reviewLogic = reviewLogic;
        }

        public ActionResult QuestionOverview()
        {
            List<QuestionViewModel> questionView = new List<QuestionViewModel>();
            foreach (Question question in _questionLogic.GetAllOpenQuestions())
            {
                questionView.Add(new QuestionViewModel(question, _userLogic.GetUserById(question.CareRecipientId)));
            }

            return View("../Volunteer/Question/Overview", questionView);
        }

        public ActionResult QuestionClosedOverview()
        {
            List<QuestionViewModel> questionView = new List<QuestionViewModel>();
            foreach (Question question in _questionLogic.GetAllClosedQuestionsVolunteer(Convert.ToInt32(Request.Cookies["id"])))
            {
                questionView.Add(new QuestionViewModel(question, _userLogic.GetUserById(question.CareRecipientId)));
            }

            return View("../Volunteer/Question/OverviewClosed", questionView);
        }

        public ActionResult ReactionCreate(QuestionViewModel questionViewModel)
        {
            return View("../Volunteer/Question/ReactionCreate", new ReactionViewModel(questionViewModel.QuestionId, questionViewModel.Title, questionViewModel.CareRecipientName));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReactionCreate(ReactionViewModel reactionViewModel)
        {
            try
            {
                int questionId = reactionViewModel.QuestionId;
                string description = reactionViewModel.Description;

                int senderid = Convert.ToInt32(Request.Cookies["id"]);

                _reactionLogic.PostReaction(new Reaction(questionId, senderid, description));

                return RedirectToAction(nameof(QuestionOverview));
            }
            catch
            {
                return View("Error");
            }
        }

        public ActionResult ChatOverview()
        {
            List<ChatViewModel> chatView = new List<ChatViewModel>();
            foreach (ChatLog chatLog in _chatLogic.GetAllOpenChatsWithVolunteerId(Convert.ToInt32(Request.Cookies["id"])))
            {
                chatView.Add(new ChatViewModel(chatLog));
            }
            return View("../Volunteer/Chat/Overview", chatView);
        }

        public ActionResult ReviewOverview()
        {
            List<ReviewViewModel> reviewList = new List<ReviewViewModel>();

            int volunteerId = Convert.ToInt32(Request.Cookies["id"]);
            foreach (ReviewInfo review in _reviewLogic.GetAllReviewsWithVolunteerId(volunteerId))
            {
                reviewList.Add(new ReviewViewModel(review));
            }

            return View("../Review/ReviewOverview", reviewList);
        }

        [HttpGet]
        public ActionResult CreateAppointment(int careRecipientId, int questionId, int volunteerId)
        {
            ViewBag.Firstname = _userLogic.GetUserById(careRecipientId).FirstName;

            return View("Appointment/CreateAppointment");
        }


        public ActionResult CreateAppointment(AppointmentViewModel appointmentView)
        {
            _appointmentLogic.CreateAppointment(new Appointment(appointmentView.QuestionId, appointmentView.CareRecipientId, appointmentView.VolunteerId, DateTime.Now, appointmentView.TimeStamp, appointmentView.Location));
            return RedirectToAction("ChatOverview");
        }

        public ActionResult AppointmentOverview()
        {
            List<AppointmentViewModel> appointmentViews = new List<AppointmentViewModel>();
            foreach (Appointment appointment in _appointmentLogic.GetAllAppointmentsFromUser(Convert.ToInt32(Request.Cookies["id"])))
            {
                appointmentViews.Add(new AppointmentViewModel(appointment, _questionLogic.GetSingleQuestion(appointment.QuestionId), _userLogic.GetUserById(appointment.CareRecipientId)));
            }

            return View("Appointment/Overview", appointmentViews);
        }

        public ActionResult DeleteAppointment(int appointmentId)
        {
            _appointmentLogic.DeleteAppointment(appointmentId);
            return RedirectToAction("AppointmentOverview");
        }

        public ActionResult OpenChat(int id, string volunteerName, string careRecipientName, int careRecipientId)
        {
            List<MessageViewModel> messageView = new List<MessageViewModel>();
            MessageViewModel2 messageView2 = new MessageViewModel2(careRecipientId, Convert.ToInt32(Request.Cookies["id"]), id, _chatLogic.GetSingleChatLog(id).Status);

            foreach (ChatMessage cMessage in _chatLogic.LoadMessageListWithChatId(id))
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