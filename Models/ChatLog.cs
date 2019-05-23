using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ChatLog
    {
        public int ChatLogID { get; private set; }
        public string QuestionTitle { get; private set; }
        public int CareRecipientID { get; }
        public int VolunteerID { get; }
        public string CareRecipientFirstName { get; private set; }
        public string CareRecipientLastName { get; private set; }
        public string VolunteerFirstName { get; private set; }
        public string VolunteerLastName { get; private set; }
        public DateTime TimeStamp { get; private set; }
        public List<ChatMessage> Messages = new List<ChatMessage>();
        public int QuestionID { get; private set; }
        public bool Status { get; set; }


        public ChatLog(int chatLogId, string questionTitle, int careRecipientId, int volunteerId, string careRecipientFirstName, string careRecipientLastName, string volunteerFirstName, string volunteerLastName, DateTime timeStamp, int questionID, bool status)
        {
            ChatLogID = chatLogId;
            QuestionTitle = questionTitle;
            CareRecipientID = careRecipientId;
            VolunteerID = volunteerId;
            CareRecipientFirstName = careRecipientFirstName;
            CareRecipientLastName = careRecipientLastName;
            VolunteerFirstName = volunteerFirstName;
            VolunteerLastName = volunteerLastName;
            TimeStamp = timeStamp;
            QuestionID = questionID;
            Status = status;
        }

        public ChatLog(int chatLogId, int careRecipientId, int volunteerId, DateTime timeStamp, bool status)
        {
            ChatLogID = chatLogId;
            CareRecipientID = careRecipientId;
            VolunteerID = volunteerId;
            TimeStamp = timeStamp;
            Status = status;
        }

        public ChatLog(int chatLogId)
        {
            ChatLogID = chatLogId;
        }
    }
}
