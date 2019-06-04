using System;
using System.Collections.Generic;

namespace Models
{
    public class ChatLog
    {
        public int ChatLogId { get; }
        public string QuestionTitle { get; }
        public int CareRecipientId { get; }
        public int VolunteerId { get; }
        public string CareRecipientFirstName { get; }
        public string CareRecipientLastName { get; }
        public string VolunteerFirstName { get; }
        public string VolunteerLastName { get; }
        public DateTime TimeStamp { get; }
        public List<ChatMessage> Messages = new List<ChatMessage>();
        public int QuestionId { get; }
        public bool Status { get; set; }


        public ChatLog(int chatLogId, string questionTitle, int careRecipientId, int volunteerId, string careRecipientFirstName, string careRecipientLastName, string volunteerFirstName, string volunteerLastName, DateTime timeStamp, int questionId, bool status)
        {
            ChatLogId = chatLogId;
            QuestionTitle = questionTitle;
            CareRecipientId = careRecipientId;
            VolunteerId = volunteerId;
            CareRecipientFirstName = careRecipientFirstName;
            CareRecipientLastName = careRecipientLastName;
            VolunteerFirstName = volunteerFirstName;
            VolunteerLastName = volunteerLastName;
            TimeStamp = timeStamp;
            QuestionId = questionId;
            Status = status;
        }

        public ChatLog(int chatLogId, int careRecipientId, int volunteerId, DateTime timeStamp, bool status)
        {
            ChatLogId = chatLogId;
            CareRecipientId = careRecipientId;
            VolunteerId = volunteerId;
            TimeStamp = timeStamp;
            Status = status;
        }

        public ChatLog(int chatLogId)
        {
            ChatLogId = chatLogId;
        }
    }
}
