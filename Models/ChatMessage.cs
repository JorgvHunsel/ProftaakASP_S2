using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fontys.PTS2.Prototype.Classes
{
    public class ChatMessage
    {
        public int ChatID { get; private set; }
        public int ReceiverID { get; private set; }
        public int SenderID { get; private set; }
        public string MessageContent { get; private set; }
        public DateTime TimeStamp { get; private set; }
        

        public ChatMessage(int chatId, int receiverId, int senderId, string messageContent, DateTime timeStamp)
        {
            ChatID = chatId;
            ReceiverID = receiverId;
            SenderID = senderId;
            MessageContent = messageContent;
            TimeStamp = timeStamp;
        }
    }
}
