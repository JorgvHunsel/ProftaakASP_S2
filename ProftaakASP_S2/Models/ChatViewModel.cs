using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace ProftaakASP_S2.Models
{
    public class ChatViewModel
    {
        public int ChatLogID { get; set; }
        public string VolunteerName { get; set; }
        public string CareRecipientName { get; set; }
        public DateTime TimeStamp { get; set; }
        public string QuestionName { get; set; }
        public int QuestionId { get; set; }
        public int VolunteerId { get; set; }
        public int CareRecipientId { get; set; }

        public ChatViewModel(ChatLog c)
        {
            ChatLogID = c.ChatLogID;
            VolunteerName = c.VolunteerFirstName + " " + c.VolunteerLastName;
            CareRecipientName = c.CareRecipientFirstName + " " + c.CareRecipientLastName;
            TimeStamp = c.TimeStamp;
            QuestionName = c.QuestionTitle;
            QuestionId = c.QuestionID;
            VolunteerId = c.VolunteerID;
            CareRecipientId = c.CareRecipientID;
        }
    }

}
