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
        public bool Status { get; set; }

        public ChatViewModel(ChatLog chat)
        {
            ChatLogID = chat.ChatLogID;
            VolunteerName = chat.VolunteerFirstName + " " + chat.VolunteerLastName;
            CareRecipientName = chat.CareRecipientFirstName + " " + chat.CareRecipientLastName;
            TimeStamp = chat.TimeStamp;
            QuestionName = chat.QuestionTitle;
            QuestionId = chat.QuestionID;
            VolunteerId = chat.VolunteerID;
            CareRecipientId = chat.CareRecipientID;
            Status = chat.Status;
        }

        public ChatViewModel(int chatlogId)
        {
            ChatLogID = chatlogId;
        }

        public ChatViewModel()
        {
            
        }
    }

}
