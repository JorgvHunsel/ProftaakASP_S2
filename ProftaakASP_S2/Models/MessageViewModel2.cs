using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace ProftaakASP_S2.Models
{
    public class MessageViewModel2
    {
        public List<MessageViewModel> Messages { get; set; }
        public string NewMessage { get; set; }
        public int ReceiverId { get; set; }
        public int SenderId { get; set; }
        public int ChatLogId { get; set; }

        public MessageViewModel2(int receiverId, int senderId, int chatLogId)
        {
            this.ReceiverId = receiverId;
            this.SenderId = senderId;
            ChatLogId = chatLogId;
        }

        public MessageViewModel2()
        {
            
        }
    }
}
