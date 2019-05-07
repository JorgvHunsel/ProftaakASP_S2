﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace ProftaakASP_S2.Models
{
    public class MessageViewModel
    {
        public int ChatLogID { get; set; }
        public string MessageContent { get; set; }
        public DateTime Timestamp { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public int UserId { get; set; }

        public string VolunteerName { get; set; }
        public string CareRecipientName { get; set; }
        

        public MessageViewModel(ChatMessage cM, int userId, string volunteerName, string careRecipientName)
        {
            UserId = userId;
            ChatLogID = cM.ChatID;
            SenderId = cM.SenderID;
            ReceiverId = cM.ReceiverID;
            MessageContent = cM.MessageContent;
            Timestamp = cM.TimeStamp;
            VolunteerName = volunteerName;
            CareRecipientName = careRecipientName;
        }
    }
}
